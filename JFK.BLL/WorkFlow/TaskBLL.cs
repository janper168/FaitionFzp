using JKF.BLL.Base;
using JKF.BLL.Models;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Models.WorkFlow;
using JKF.DB.Operation;
using JKF.Utils;
using Microsoft.CSharp;
using Newtonsoft.Json;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Formula;
using NPOI.SS.Formula.Functions;
using System;
using System.Runtime;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Match = System.Text.RegularExpressions.Match;
using Task = JKF.DB.Models.Task;
using Microsoft.EntityFrameworkCore.Metadata;
using System.CodeDom;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace JKF.BLL
{
    public class TaskBLL
    {
        private BaseDbContext _dbContext = null;
        private TaskService _TaskService = null;
        private TaskNodeService _TaskNodeService = null;
        private WorkFlowDesignService _WorkFlowDesignService = null;
        private TaskNodeLogService _TaskNodeLogService = null;
        private WorkFlowLineService _WorkFlowLineService = null;
        private WorkFlowNodeService _WorkFlowNodeService = null;
        private ConditionRuleService _ConditionRuleService = null;
        private TaskNodeProcesserService _TaskNodeProcesserService = null;
        private PostUserService _PostUserService = null;
        private PostService _PostService = null;
        private UserService _UserService = null;
        private DepartmentService _DepartmentService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public TaskBLL()
        {
            _dbContext = new BaseDbContext();
            _TaskService = new TaskService(_dbContext);
            _TaskNodeService = new TaskNodeService(_dbContext);
            _TaskNodeLogService = new TaskNodeLogService(_dbContext);
            _WorkFlowDesignService = new WorkFlowDesignService(_dbContext);
            _WorkFlowLineService = new WorkFlowLineService(_dbContext);
            _WorkFlowNodeService = new WorkFlowNodeService(_dbContext);
            _ConditionRuleService = new ConditionRuleService(_dbContext);
            _TaskNodeProcesserService = new TaskNodeProcesserService(_dbContext);
            _PostUserService = new PostUserService(_dbContext);

            _PostService = new PostService(_dbContext);
            _UserService = new UserService(_dbContext);
            _DepartmentService = new DepartmentService(_dbContext);
        }

        public void AddEntity(Task entity)
        {
            entity.Create();
            _TaskService.Add(entity, false);
            var workFlowDesign = _WorkFlowDesignService.SingleOrDefault(entity.WorkFlowDesignId, false);
            FlowDesign? model = (FlowDesign)JsonConvert.DeserializeObject<FlowDesign>(workFlowDesign.FlowDesignJson);
            if (model != null)
            {
                //添加开始节点和结束节点的workflownode
                var n1 = new WorkFlowNode();
                n1.Create();
                n1.NodeId = "startEndon";
                n1.NodeType = "start";
                n1.NodeName = "开始";
                n1.WorkFlowDesignId = entity.WorkFlowDesignId;
                _WorkFlowNodeService.Add(n1, false);

                var n2 = new WorkFlowNode();
                n2.Create();
                n2.NodeId = "endEndon";
                n2.NodeType = "end";
                n2.NodeName = "结束";
                n2.WorkFlowDesignId = entity.WorkFlowDesignId;
                _WorkFlowNodeService.Add(n2, false);

                foreach (var item in model.nodeList)//对每个节点初始化
                {
                    var taskNode = new TaskNode();
                    taskNode.Create();
                    taskNode.NodeId = item.id; //节点的id
                    taskNode.NodeResult = TaskNodeStatus.UnReached;//都初始化为未到达
                    taskNode.TaskId = entity.TaskId;
                    if (item.id == "startEndon")
                    {
                        taskNode.TaskNodeName = "开始";
                    }
                    else if (item.id == "endEndon")
                    {
                        taskNode.TaskNodeName = "结束";
                    }
                    else
                    {
                        taskNode.TaskNodeName = _WorkFlowNodeService.FirstOrDefault(a => a.NodeId == item.id &&
                            a.WorkFlowDesignId == workFlowDesign.WorkFlowDesignId).NodeName;
                    }
                    _TaskNodeService.Add(taskNode, false);

                }
                _TaskNodeService.Commit();
            }
        }

        public void AddEntities(IEnumerable<Task> entities)
        {
            foreach (var e in entities)
            {
                e.Create();
            }
            _TaskService.AddRange(entities);
        }

        public void UpdateEntity(Task entity)
        {
            _TaskService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void UpdateEntity(Task entity, bool rightNow)
        {
            //更新任务状态
            _TaskService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList(), rightNow);

            //更新开始节点为草稿状态
            var taskNodeStart = _TaskNodeService.FirstOrDefault(a => a.TaskId == entity.TaskId &&
                a.NodeId == "startEndon", true);

            if (taskNodeStart != null)
            {
                taskNodeStart.NodeResult = TaskNodeStatus.Draft;
                _TaskNodeService.Update(taskNodeStart, taskNodeStart.GetPropInfoList(),
                    taskNodeStart.GetReferenceInfoList(), false);
            }

            Commit();
        }

        public void UpdateEntities(IEnumerable<Task> entities)
        {
            foreach (var e in entities)
            {
                _TaskService.Update(e, e.GetPropInfoList(), e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Task entity)
        {
            _TaskService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Task> entities)
        {
            _TaskService.RemoveRange(entities);
        }

        public Task GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _TaskService.FindList(a => a.TaskId == keyValue, a => a.WorkFlowDesign, a => a.WorkFlowDesign.CustomizedForm, a => a.TaskId, true, beTraced)[0];
            return entity;
        }

        public IList<Task> GetEntities(Expression<Func<Task, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _TaskService.FindList(whereExpression, a => a.TaskId, isSortAsc);
            return data;
        }

        public IList<Task> GetEntities(Pagination pagination, Sort sort, Expression<Func<Task, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_TaskService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Task, bool>> whereExpression)
        {
            return _TaskService.IsExists(whereExpression);
        }


        private List<node> GetNextNodeList(FlowDesign flowDesign, Task task, string nodeId)
        {
            //从开始节点开始，根据线的方法找到下一个节点，并处理。
            List<node> nextNodeList = new List<node>();
            foreach (var line in flowDesign.lineList)
            {
                if (line.startNodeId == nodeId)
                {
                    var lineObject = _WorkFlowLineService.FirstOrDefault(a => a.WorkFlowDesignId == task.WorkFlowDesignId &&
                         a.LineId == line.id && a.LineType == 2);//线条类型是通过的
                    if (lineObject == null) continue;

                    var nextNode = flowDesign.nodeList.FirstOrDefault(a => a.id == line.endNodeId);
                    if (nextNode != null)
                    {
                        nextNodeList.Add(nextNode);
                    }
                }
            }
            return nextNodeList;
        }

        private List<node> GetNextYesNodeList(FlowDesign flowDesign, Task task, string nodeId)
        {
            //从开始节点开始，根据线的方法找到下一个节点，并处理。
            List<node> nextNodeList = new List<node>();
            foreach (var line in flowDesign.lineList)
            {
                if (line.startNodeId == nodeId)
                {
                    var lineObject = _WorkFlowLineService.FirstOrDefault(a => a.WorkFlowDesignId == task.WorkFlowDesignId &&
                         a.LineId == line.id && a.LineType == 1);//线条类型是是的
                    if (lineObject == null) continue;

                    var nextNode = flowDesign.nodeList.FirstOrDefault(a => a.id == line.endNodeId);
                    if (nextNode != null)
                    {
                        nextNodeList.Add(nextNode);
                    }
                }
            }
            return nextNodeList;
        }

        private List<node> GetPrevNodeList(FlowDesign flowDesign, Task task, string nodeId)
        {
            //从开始节点开始，根据线的方法找到下一个节点，并处理。
            List<node> nextNodeList = new List<node>();
            foreach (var line in flowDesign.lineList)
            {
                if (line.startNodeId == nodeId)
                {
                    var lineObject = _WorkFlowLineService.FirstOrDefault(a => a.WorkFlowDesignId == task.WorkFlowDesignId &&
                         a.LineId == line.id && a.LineType == 3);//线条类型是驳回的
                    if (lineObject == null) continue;

                    var nextNode = flowDesign.nodeList.FirstOrDefault(a => a.id == line.endNodeId);
                    if (nextNode != null)
                    {
                        nextNodeList.Add(nextNode);
                    }
                }
            }
            return nextNodeList;
        }

        private List<node> GetNextNoNodeList(FlowDesign flowDesign, Task task, string nodeId)
        {
            //从开始节点开始，根据线的方法找到下一个节点，并处理。
            List<node> nextNodeList = new List<node>();
            foreach (var line in flowDesign.lineList)
            {
                if (line.startNodeId == nodeId)
                {
                    var lineObject = _WorkFlowLineService.FirstOrDefault(a => a.WorkFlowDesignId == task.WorkFlowDesignId &&
                         a.LineId == line.id && a.LineType == 0);//线条类型是否的
                    if (lineObject == null) continue;

                    var nextNode = flowDesign.nodeList.FirstOrDefault(a => a.id == line.endNodeId);
                    if (nextNode != null)
                    {
                        nextNodeList.Add(nextNode);
                    }
                }
            }
            return nextNodeList;
        }

        //获得上级主管
        private List<string> GetProcesserUpPost(string applyerId, TaskNode taskNode, WorkFlowNode workFlowNode)
        {
            List<string> processers = new List<string>();
            //获得申请人的postid
            var applyUserList = _UserService.FindList(a => a.UserId == applyerId, a => a.PostUserList, a => a.UserId, true);
            if (applyUserList != null && applyUserList.Count == 1)
            {
                var applyUser = applyUserList[0];

                var postUserList = applyUser.PostUserList;
                foreach (var postUser in postUserList)//我的每一个岗位都遍历
                {
                    var myPosts = _PostUserService.FindList(a => a.UserId == postUser.UserId, a => a.UserId);
                    foreach (var myPost in myPosts)
                    {

                        var post = _PostService.SingleOrDefault(myPost.PostId);
                        var fatherPost = _PostService.FirstOrDefault(a => a.PostId == post.ParentId);
                        if (fatherPost == null)//已经是这个部门的顶级岗位了，则加入
                        {
                            //获得当前岗位的用户
                            var postUserList2 = _PostUserService.FindList(a => a.PostId == post.PostId, a => a.PostId);
                            if (postUserList2 != null && postUserList2.Count > 0)
                            {
                                foreach (var p in postUserList2)
                                {
                                    if (!processers.Contains(p.UserId))
                                        processers.Add(p.UserId);

                                }
                            }
                        }
                        else//有父亲岗位
                        {
                            //获得当前岗位的用户
                            var postUserList2 = _PostUserService.FindList(a => a.PostId == post.ParentId, a => a.PostId);
                            if (postUserList2 != null && postUserList2.Count > 0)
                            {
                                foreach (var p in postUserList2)
                                {
                                    if (!processers.Contains(p.UserId))
                                        processers.Add(p.UserId);
                                }
                            }
                        }
                    }
                }
            }

            return processers;
        }

        //获得部门经理
        private List<string> GetProcesserDepartmentManager(string applyerId, TaskNode taskNode, WorkFlowNode workFlowNode)
        {
            List<string> processers = new List<string>();
            //获得申请人的departmentid
            var applyUserList = _UserService.FindList(a => a.UserId == applyerId, a => a.Department, a => a.UserId, true);

            if (applyUserList != null && applyUserList.Count == 1)
            {
                var applyUser = applyUserList[0];

                var departmentId = applyUser.Department.DepartmentId;//部门只有一个
                //获得部门下的每个岗位
                var posts = _PostService.FindList(a => a.Department.DepartmentId == departmentId, a => a.PostId);

                foreach (var post in posts)//每一个岗位都遍历
                {
                    //如果是部门下的顶级岗位，并且岗位是经理或主管的
                    if (post.ParentId == "0" && (post.Name.Contains("经理") || post.Name.Contains("主管") || post.Name.Contains("总监")))
                    {
                        //找出这些人
                        var postUserList2 = _PostUserService.FindList(a => a.PostId == post.PostId, a => a.PostId);
                        if (postUserList2 != null && postUserList2.Count > 0)
                        {
                            foreach (var p in postUserList2)
                            {
                                if (!processers.Contains(p.UserId))
                                    processers.Add(p.UserId);
                            }
                        }
                    }
                }
            }
            return processers;
        }

        private List<string> GetProcesserCompanyManager(string applyerId, TaskNode taskNode, WorkFlowNode workFlowNode)
        {
            List<string> processers = new List<string>();

            //获得申请人的departmentid
            var applyUserList = _UserService.FindList(a => a.UserId == applyerId, a => a.Department.Company, a => a.UserId, true);

            if (applyUserList != null && applyUserList.Count == 1)
            {
                var applyUser = applyUserList[0];


                var companyId = applyUser.Department.Company.CompanyId;//公司有多个部门

                var departments = _DepartmentService.FindList(a => a.Company.CompanyId == companyId, a => a.DepartmentId);
                foreach (var department in departments)
                {
                    //获得部门下的每个岗位
                    var posts = _PostService.FindList(a => a.Department.DepartmentId == department.DepartmentId, a => a.PostId);

                    foreach (var post in posts)//每一个岗位都遍历
                    {
                        //如果是部门下的顶级岗位，并且岗位是经理或主管的
                        if (post.ParentId == "0" && (post.Name.Contains("总经理") || post.Name.Contains("总监")))
                        {
                            //找出这些人
                            var postUserList2 = _PostUserService.FindList(a => a.PostId == post.PostId, a => a.PostId);
                            if (postUserList2 != null && postUserList2.Count > 0)
                            {
                                foreach (var p in postUserList2)
                                {
                                    if (!processers.Contains(p.UserId))
                                        processers.Add(p.UserId);
                                }
                            }
                        }
                    }
                }
            }
            return processers;

        }

        private List<string> GetProcesserDefined(string applyerId, TaskNode taskNode, WorkFlowNode workFlowNode)
        {
            List<string> ret = new List<string>();

            if (workFlowNode.ProcessorIds != null)
            {
                //指定id可能包含公司id和部门id,需要过滤掉
                List<string> processers =
                    workFlowNode.ProcessorIds.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var processer in processers)
                {
                    if (_UserService.IsExists(a => a.UserId == processer))
                    {
                        ret.Add(processer);
                    }
                }
            }

            return ret;
        }



        //获得下一个处理人（可多个）
        private List<string> GetProcesser(Task task, node node, FlowDesign flowDesign)
        {
            var taskNode = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId && a.NodeId == node.id);

            List<string> processers = new List<string>();

            var workFlowNode = _WorkFlowNodeService.FirstOrDefault(a => a.WorkFlowDesignId == task.WorkFlowDesignId
                && a.NodeId == node.id);
            if (workFlowNode != null)
            {
                //处理者类型：1上级主管，2.部门经理，3：总经理, 4：指定人
                int processTypeRule = workFlowNode.ProcessorTypeRule.Value;
                switch (processTypeRule)
                {
                    case 1:
                        processers = GetProcesserUpPost(task.ApplyerId, taskNode, workFlowNode);
                        break;
                    case 2:
                        processers = GetProcesserDepartmentManager(task.ApplyerId, taskNode, workFlowNode);
                        break;
                    case 3:
                        processers = GetProcesserCompanyManager(task.ApplyerId, taskNode, workFlowNode);
                        break;
                    case 4:
                        processers = GetProcesserDefined(task.ApplyerId, taskNode, workFlowNode);
                        break;
                    default:
                        break;
                }
            }
            return processers;
        }

        private bool SetShenpiForProcesser(string currentNodeId, string processer, Task task, TaskNode taskNode, WorkFlowNode workFlowNode)
        {
            var ret = false;

            //处理自动通过
            //如果处理者和提单者相同，则通过;如果处理人上一次节点也是处理通过则这次也是处理通过
            var isTheSameToApplerThenPass = workFlowNode.IsProcessorTheSameToApplyerPassRule;
            var isTheSameToLastProcesserPass = workFlowNode.IsProcessorTheSameToLastProcessorRule;
            var isAllPassThenPass = workFlowNode.IsAllPassThenPassRule;

            if (isTheSameToApplerThenPass.Value == 1)
            {
                //记录一个处理人审批通过的日志点
                if (task.ApplyerId == processer)
                {
                    TaskNodeProcesser taskNodeProcesser = new TaskNodeProcesser();
                    taskNodeProcesser.Create();
                    taskNodeProcesser.ProcesserId = processer;
                    taskNodeProcesser.ProcessResult = 1;//同意
                    taskNodeProcesser.ProcessTime = DateTime.Now;
                    taskNodeProcesser.ProcessComment = "默认通过";
                    taskNodeProcesser.TaskNodeId = taskNode.TaskNodeId;
                    taskNodeProcesser.TaskNodeName = "流程任务_" +
                        task.WorkFlowDesign.Name + "_" +
                        workFlowNode.NodeName + "_" +
                        _UserService.SingleOrDefault(processer).RealName + "_(已处理)_" +
                        DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");

                    _TaskNodeProcesserService.Add(taskNodeProcesser);

                    TaskNodeLog taskNodeLog = new TaskNodeLog();
                    taskNodeLog.Create();
                    taskNodeLog.TaskNodeId = taskNode.TaskNodeId;
                    taskNodeLog.ProcessResult = 1;//同意
                    taskNodeLog.ProcessComment = "默认通过";
                    taskNodeLog.TaskNodeProcesserId = taskNodeProcesser.TaskNodeProcesserId;
                    taskNodeLog.ProcessTime = DateTime.Now;
                    taskNodeLog.TaskNodeName = "流程任务_" +
                        task.WorkFlowDesign.Name + "_" +
                        workFlowNode.NodeName + "_" +
                        _UserService.SingleOrDefault(processer).RealName + "_(已处理)_" +
                        DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");

                    _TaskNodeLogService.Add(taskNodeLog);
                    ret = true; // 到这里已经处理
                }
            }
            else if (isTheSameToLastProcesserPass.Value == 1)
            {
                TaskNode lastTaskNode = _TaskNodeService.FirstOrDefault(a => a.NodeId == currentNodeId);
                var currentProcessIds = lastTaskNode.ProcesserIds;//从这里取处理人
                if (currentProcessIds.IndexOf(processer) >= 0)
                {
                    TaskNodeProcesser taskNodeProcesser = new TaskNodeProcesser();
                    taskNodeProcesser.Create();
                    taskNodeProcesser.ProcesserId = processer;
                    taskNodeProcesser.ProcessResult = 1;//同意
                    taskNodeProcesser.ProcessTime = DateTime.Now;
                    taskNodeProcesser.ProcessComment = "默认通过";
                    taskNodeProcesser.TaskNodeId = taskNode.TaskNodeId;
                    taskNodeProcesser.TaskNodeName = "流程任务_" +
                        task.WorkFlowDesign.Name + "_" +
                        workFlowNode.NodeName + "_" +
                        _UserService.SingleOrDefault(processer).RealName + "_(已处理)_" +
                        DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");

                    _TaskNodeProcesserService.Add(taskNodeProcesser);

                    TaskNodeLog taskNodeLog = new TaskNodeLog();
                    taskNodeLog.Create();
                    taskNodeLog.TaskNodeId = taskNode.TaskNodeId;
                    taskNodeLog.ProcessResult = 1;//同意
                    taskNodeLog.ProcessComment = "默认通过";
                    taskNodeLog.TaskNodeProcesserId = taskNodeProcesser.TaskNodeProcesserId;
                    taskNodeLog.ProcessTime = DateTime.Now;
                    taskNodeLog.TaskNodeName = "流程任务_" +
                        task.WorkFlowDesign.Name + "_" +
                        workFlowNode.NodeName + "_" +
                        _UserService.SingleOrDefault(processer).RealName +
                         "_(已处理)_" +
                        DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");

                    _TaskNodeLogService.Add(taskNodeLog);
                    ret = true;
                }
            }
            if (!ret) //没有自动通过
            {
                //创建一个处理人的当前审批任务
                TaskNodeProcesser taskNodeProcesser = new TaskNodeProcesser();
                taskNodeProcesser.Create();
                taskNodeProcesser.ProcesserId = processer;
                taskNodeProcesser.ProcessResult = 0;
                taskNodeProcesser.TaskNodeId = taskNode.TaskNodeId;
                taskNodeProcesser.TaskNodeName = "流程任务_" +
                            task.WorkFlowDesign.Name + "_" +
                            workFlowNode.NodeName + "_" +
                            _UserService.SingleOrDefault(processer).RealName +
                             "_(待处理)_" +
                            DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");

                _TaskNodeProcesserService.Add(taskNodeProcesser);
                return false;
            }
            else if (isAllPassThenPass == 2 && ret)//自动通过了，并且一个通过就通过。
            {
                return ret;
            }
            return ret;

        }

        private void SetZhihuiForProcesser(string currentNodeId, string processer, Task task, TaskNode taskNode, WorkFlowNode workFlowNode)
        {

            //创建一个处理人的当前审批任务
            TaskNodeProcesser taskNodeProcesser = new TaskNodeProcesser();
            taskNodeProcesser.Create();
            taskNodeProcesser.ProcesserId = processer;
            taskNodeProcesser.ProcessResult = 0;
            taskNodeProcesser.TaskNodeId = taskNode.TaskNodeId;
            taskNodeProcesser.TaskNodeName = "流程任务_" +
                        task.WorkFlowDesign.Name + "_" +
                        workFlowNode.NodeName + "_" +
                        _UserService.SingleOrDefault(processer).RealName +
                            "_(待处理)_" +
                        DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");

            _TaskNodeProcesserService.Add(taskNodeProcesser);

        }


        //处理审批节点
        private void ProcessNextNodeShenpi(string currentNodeId, Task task, node node, FlowDesign flowDesign)
        {
            var processers = GetProcesser(task, node, flowDesign);

            var taskNode = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId && a.NodeId == node.id);
            var workFlowNode = _WorkFlowNodeService.FirstOrDefault(a => a.WorkFlowDesignId == task.WorkFlowDesignId
                && a.NodeId == node.id);

            var allTrue = true;
            if (processers.Count > 0)
            {
                //对每一个处理人进行审批设置
                foreach (var processer in processers)
                {
                    var ret = SetShenpiForProcesser(currentNodeId, processer, task, taskNode, workFlowNode);
                    if (!ret)
                    {
                        allTrue = false;
                    }
                    else
                    {
                        allTrue = true;
                    }
                }
                if (allTrue)//都通过了，处理下一节点(如果是一个通过就通过，也是这种情况)
                {
                    //把当前节点置为处理通过
                    var taskNode2 = _TaskNodeService.FirstOrDefault(a => a.Task.TaskId == task.TaskId &&
                        a.NodeId == node.id, true);
                    taskNode2.NodeResult = TaskNodeStatus.ProcessPass;
                    _TaskNodeService.Update(taskNode2, taskNode2.GetPropInfoList(), taskNode2.GetReferenceInfoList());

                    List<node> nextNodeList = GetNextNodeList(flowDesign, task, node.id);
                    if (nextNodeList != null && nextNodeList.Count > 0)
                    {
                        foreach (var n in nextNodeList)
                        {
                            ProcessNextNode(node.id, task, n, flowDesign);
                        }
                    }
                }
                else
                {
                    //把当前节点置为待处理
                    var taskNode2 = _TaskNodeService.FirstOrDefault(a => a.Task.TaskId == task.TaskId &&
                       a.NodeId == node.id, true);
                    taskNode2.NodeResult = TaskNodeStatus.Waiting;
                    _TaskNodeService.Update(taskNode2, taskNode2.GetPropInfoList(), taskNode2.GetReferenceInfoList());
                }
            }

        }

        //处理知会节点
        private void ProcessNextNodeZhihui(string currentNodeId, Task task, node node, FlowDesign flowDesign)
        {
            var processers = GetProcesser(task, node, flowDesign);
            var taskNode = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId && a.NodeId == node.id);
            var workFlowNode = _WorkFlowNodeService.FirstOrDefault(a => a.WorkFlowDesignId == task.WorkFlowDesignId
                && a.NodeId == node.id);

            if (processers.Count > 0)
            {
                //对每一个处理人进行知会设置
                foreach (var processer in processers)
                {
                    SetZhihuiForProcesser(currentNodeId, processer, task, taskNode, workFlowNode);
                }

                //把当前节点置为待处理
                var taskNode2 = _TaskNodeService.FirstOrDefault(a => a.Task.TaskId == task.TaskId &&
                    a.NodeId == node.id, true);
                taskNode2.NodeResult = TaskNodeStatus.Waiting;
                _TaskNodeService.Update(taskNode2, taskNode2.GetPropInfoList(), taskNode2.GetReferenceInfoList());

            }

        }

        //处理子流程节点
        private void ProcessNextNodeChildflow(string currentNodeId, Task task, node node, FlowDesign flowDesign)
        {
            var processers = GetProcesser(task, node, flowDesign);
            var taskNode = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId && a.NodeId == node.id);
            var workFlowNode = _WorkFlowNodeService.FirstOrDefault(a => a.WorkFlowDesignId == task.WorkFlowDesignId
               && a.NodeId == node.id);

            // 为每一个用户提交一个子流程任务
            var childFlowDesignId = workFlowNode.ChildFlowDesignId;
            if (childFlowDesignId != null)
            {
                foreach (var processer in processers)
                {
                    var workFlowDesignId = childFlowDesignId;

                    var wfd = _WorkFlowDesignService.SingleOrDefault(workFlowDesignId);
                    var flowName = wfd.Name;//流程的名字
                    var userRealName = _UserService.SingleOrDefault(processer).RealName;
                    var currentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                    var taskName = "子流程任务_" + wfd.Name + "_发起人：" + userRealName + "_" + currentTime;

                    Task taskChild = new Task();
                    taskChild.Create();
                    taskChild.TaskStatus = 1;
                    taskChild.ApplyerId = processer;
                    taskChild.WorkFlowDesignId = workFlowDesignId;
                    taskChild.TaskName = taskName;
                    taskChild.ParentTaskId = task.TaskId;

                    _TaskService.Add(taskChild, false);
                    var workFlowDesign = _WorkFlowDesignService.SingleOrDefault(workFlowDesignId, false);
                    FlowDesign? model = (FlowDesign)JsonConvert.DeserializeObject<FlowDesign>(workFlowDesign.FlowDesignJson);
                    if (model != null)
                    {
                        //添加开始节点和结束节点的workflownode
                        var n1 = new WorkFlowNode();
                        n1.Create();
                        n1.NodeId = "startEndon";
                        n1.NodeType = "start";
                        n1.NodeName = "开始";
                        n1.WorkFlowDesignId = workFlowDesignId;
                        _WorkFlowNodeService.Add(n1, false);

                        var n2 = new WorkFlowNode();
                        n2.Create();
                        n2.NodeId = "endEndon";
                        n2.NodeType = "end";
                        n2.NodeName = "结束";
                        n2.WorkFlowDesignId = workFlowDesignId;
                        _WorkFlowNodeService.Add(n2, false);

                        foreach (var item in model.nodeList)//对每个节点初始化
                        {
                            var taskNode2 = new TaskNode();
                            taskNode2.Create();
                            taskNode2.NodeId = item.id; //节点的id
                            taskNode2.NodeResult = TaskNodeStatus.UnReached;//都初始化为未到达
                            taskNode2.TaskId = taskChild.TaskId;
                            if (item.id == "startEndon")
                            {
                                taskNode2.TaskNodeName = "开始";
                            }
                            else if (item.id == "endEndon")
                            {
                                taskNode2.TaskNodeName = "结束";
                            }
                            else
                            {
                                taskNode2.TaskNodeName = _WorkFlowNodeService.FirstOrDefault(a => a.NodeId == item.id &&
                                    a.WorkFlowDesignId == workFlowDesignId).NodeName;
                            }
                            _TaskNodeService.Add(taskNode2, false);

                        }

                        var tn = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId && a.NodeId == node.id, true);

                        tn.NodeResult = TaskNodeStatus.Waiting;//待处理
                        _TaskNodeService.Update(tn, tn.GetPropInfoList(), tn.GetReferenceInfoList(), false);

                        _TaskNodeService.Commit();
                    }
                }
            }
        }

        //处理条件节点
        private void ProcessNextNodeCondition(string currentNodeId, Task task, node node, FlowDesign flowDesign)
        {
            var taskNode = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId && a.NodeId == node.id);
            var workFlowNode = _WorkFlowNodeService.FirstOrDefault(a => a.WorkFlowDesignId == task.WorkFlowDesignId
               && a.NodeId == node.id);
            var conditionRule = _ConditionRuleService.FirstOrDefault(a => a.WorkFlowNodeId == workFlowNode.WorkFlowNodeId);

            if (conditionRule != null)
            {
                var ret = GetConditionResult(taskNode, workFlowNode,
                    node, task.WorkFlowDesign.CustomizedForm.FormCfg,
                    conditionRule.RulesJson, task.FormContentJson);
                //如果是走是的分支,否走否的分支
                if (ret)
                {
                    List<node> nextNodeList = GetNextYesNodeList(flowDesign, task, node.id);
                    //如果节点存在才做操作
                    if (nextNodeList != null && nextNodeList.Count > 0)
                    {
                        foreach (var n in nextNodeList)
                        {
                            ProcessNextNode(node.id, task, n, flowDesign);
                        }

                        //设置条件节点为是状态
                        TaskNode taskNode2 = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId &&
                            a.NodeId == node.id, true);

                        taskNode2.NodeResult = TaskNodeStatus.ConditionYes;
                        _TaskNodeService.Update(taskNode2, taskNode2.GetPropInfoList(), taskNode2.GetReferenceInfoList());

                    }
                }
                else
                {
                    List<node> nextNodeList = GetNextNoNodeList(flowDesign, task, node.id);
                    //如果节点存在才做操作
                    if (nextNodeList != null && nextNodeList.Count > 0)
                    {
                        foreach (var n in nextNodeList)
                        {
                            ProcessNextNode(node.id, task, n, flowDesign);
                        }

                        //设置条件节点为否状态
                        TaskNode taskNode2 = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId &&
                            a.NodeId == node.id, true);

                        taskNode2.NodeResult = TaskNodeStatus.ConditionNo;
                        _TaskNodeService.Update(taskNode2, taskNode2.GetPropInfoList(), taskNode2.GetReferenceInfoList());

                    }
                }
            }

        }

        private bool GetConditionResult(TaskNode taskNode, WorkFlowNode workFlowNode,
            node node, string FormCfg,
            string RulesJson, string FormContentJson)
        {
            bool ret = true;
            
            var Rules = JsonConvert.DeserializeObject<JKF.BLL.Models.Rules>(RulesJson);
            List<FormContentItem> formItems = JsonConvert.DeserializeObject<List<FormContentItem>>(FormContentJson);

            var Formula = Rules.Formula;

            Formula = Formula.Replace("or", "||").Replace("and", "&&");
            List<DataCondition> conditions = Rules.Conditions;

            var matches = Regex.Matches(Formula, @"{(\d+)}");

            Dictionary<string,string> dictCondition = new Dictionary<string, string>();
            foreach (var condition in conditions)
            {
                dictCondition.Add(condition.Sort, condition.ConditionText);
            }

            foreach (Match match in matches) 
            {
                string sort = match.Groups[1].Value;
                string con = match.Groups[0].Value;

                Formula = Formula.Replace(con, dictCondition[sort]);// 替换成条件文本
            }

            foreach (var item in formItems) 
            {
                Formula = Formula.Replace(item.Id, "\""+ item.value+ "\""); 
            }

            string code1 = @"
                using System;

                public class ScriptedClass
                {
                    public bool HelloWorld { get; set; }
                    public ScriptedClass()
                    {
                        HelloWorld = " + Formula + @";
                    }
                }";

            var script1 = CSharpScript.RunAsync(code1).Result;

            var result1 = script1.ContinueWithAsync<bool>("new ScriptedClass().HelloWorld").Result;

            return result1.ReturnValue;
        }


       

        private void ProcessNextNodeEndEndon(string currentNodeId, Task task, node node, FlowDesign flowDesign)
        {
            //置当前节点为已处理
            //设置开始节点已提交状态
            TaskNode taskNode = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId &&
                a.NodeId == currentNodeId, true);

            taskNode.NodeResult = TaskNodeStatus.ProcessPass;
            _TaskNodeService.Update(taskNode, taskNode.GetPropInfoList(), taskNode.GetReferenceInfoList(), false);

            //置结束节点为已结束
            TaskNode taskNode2 = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId &&
                a.NodeId == "endEndon", true);

            taskNode2.NodeResult = TaskNodeStatus.End;
            _TaskNodeService.Update(taskNode2, taskNode2.GetPropInfoList(), taskNode2.GetReferenceInfoList(), false);


            //整个流程完毕
            var editTask = _TaskService.SingleOrDefault(task.TaskId, true);
            editTask.TaskStatus = 3;//处理结束
            _TaskService.Update(editTask, editTask.GetPropInfoList(), editTask.GetReferenceInfoList());

            if (!string.IsNullOrEmpty(editTask.ParentTaskId))
            {
                //有父亲任务，看这个父亲下的所有子流程任务都完成了没有。有的话总的子流程才算走完。
                //查看有没有对应的父流程节点，置为已处理
              
                var allChildsCount = _TaskService.Count(a => a.ParentTaskId == editTask.ParentTaskId);
                var okChildsCount = _TaskService.Count(a => a.ParentTaskId == editTask.ParentTaskId &&
                   a.TaskStatus == 3);

                if (allChildsCount == okChildsCount)
                {
                    var parentWLN = _WorkFlowNodeService.FirstOrDefault(a => a.ChildFlowDesignId == task.WorkFlowDesignId);
                    var nodeId = parentWLN.NodeId;

                    //找到子流程节点做操作
                    var tn = _TaskNodeService.FirstOrDefault(a => a.NodeId == nodeId && a.TaskId == editTask.ParentTaskId,true);
                    tn.NodeResult = TaskNodeStatus.ProcessPass;
                    _TaskNodeService.Update(tn, tn.GetPropInfoList(), tn.GetReferenceInfoList());

                    Task parentTask = _TaskService.FirstOrDefault(a => a.TaskId == editTask.ParentTaskId);
                    var parentDesign =_WorkFlowDesignService.FirstOrDefault(a => a.WorkFlowDesignId == parentTask.WorkFlowDesignId);

                    var parentDesignObject = JsonConvert.DeserializeObject<FlowDesign>(parentDesign.FlowDesignJson);
                    
                    //处理父流程下一节点           
                    List<node> nextNodeList = GetNextNodeList(parentDesignObject, parentTask, parentWLN.NodeId);

                    //如果节点存在才做操作
                    if (nextNodeList != null && nextNodeList.Count > 0)
                    {
                        foreach (var n in nextNodeList)
                        {
                            ProcessNextNode(parentWLN.NodeId, parentTask, n, parentDesignObject);
                        }
                    }
                }
            }
        }

        //回退到之前的节点
        private void ProcessPrevNode(string currentNodeId, Task task, node node, FlowDesign flowDesign)
        {
            switch (node.type)
            {
                case "start"://目前只支持回退到首节点
                    ProcessStartNode(currentNodeId, task, node, flowDesign);
                    break;              
                default:
                    break;

            }
        }

        private void ProcessStartNode(string currentNodeId, Task task, node node, FlowDesign flowDesign)
        {
            //首节点为未提交状态
            TaskNode taskNode = _TaskNodeService.FirstOrDefault(a => a.TaskId == task.TaskId &&
                           a.NodeId == node.id, true);

            taskNode.NodeResult = TaskNodeStatus.Draft; //草稿
            _TaskNodeService.Update(taskNode, taskNode.GetPropInfoList(), taskNode.GetReferenceInfoList());

            Task task1 = _TaskService.FirstOrDefault(a => a.TaskId == task.TaskId,  true);

            task1.TaskStatus = 1; //未提交
            _TaskService.Update(task1, task1.GetPropInfoList(), task1.GetReferenceInfoList());


            
        }

        //处理下一节点
        private void ProcessNextNode(string currentNodeId, Task task, node node, FlowDesign flowDesign)
        {
            switch (node.type)
            {
                case "shenpi":
                    ProcessNextNodeShenpi(currentNodeId, task, node, flowDesign);
                    break;
                case "zhihui":
                    ProcessNextNodeZhihui(currentNodeId, task, node, flowDesign);
                    break;
                case "childflow":
                    ProcessNextNodeChildflow(currentNodeId, task, node, flowDesign);
                    break;
                case "condition":
                    ProcessNextNodeCondition(currentNodeId, task, node, flowDesign);
                    break;
                case "end":
                    ProcessNextNodeEndEndon(currentNodeId, task, node, flowDesign);
                    break;
                default:
                    break;

            }
        }

        public void StartFlow(Task entity)
        {
            _TaskService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

            FlowDesign? flowDesign = (FlowDesign)JsonConvert.DeserializeObject<FlowDesign>(entity.WorkFlowDesign.FlowDesignJson);

            List<node> nextNodeList = GetNextNodeList(flowDesign, entity, "startEndon");

            //如果节点存在才做操作
            if (nextNodeList != null && nextNodeList.Count > 0)
            {
                foreach (var node in nextNodeList)
                {
                    ProcessNextNode("startEndon", entity, node, flowDesign);
                }
                //设置开始节点已提交状态
                TaskNode taskNode = _TaskNodeService.FirstOrDefault(a => a.TaskId == entity.TaskId &&
                    a.NodeId == "startEndon", true);

                taskNode.NodeResult = TaskNodeStatus.Submit;
                _TaskNodeService.Update(taskNode, taskNode.GetPropInfoList(), taskNode.GetReferenceInfoList());

            }
        }

        public bool CanMoveNextProcess(TaskNodeProcesser taskNodeProcesser)
        {
            var workFlowNode = _WorkFlowNodeService.FirstOrDefault(a => a.NodeId == taskNodeProcesser.TaskNode.NodeId &&
                      taskNodeProcesser.TaskNode.Task.WorkFlowDesign.WorkFlowDesignId == a.WorkFlowDesignId);

            if (workFlowNode.IsAllPassThenPassRule == 2)
            { //不需要全部
                return true;
            }
            else if (workFlowNode.IsAllPassThenPassRule == 1 || taskNodeProcesser.ProcessResult == 3) 
            {// 需要全部或者是知会（全部已阅才行）
                if (_TaskNodeProcesserService.IsExists(a => a.ProcessResult == 0 &&
                    a.TaskNode.TaskNodeId == taskNodeProcesser.TaskNode.TaskNodeId))
                    return false;
                else return true;//我就是最后一个返回true 
            }

            return false;
        }

        public bool CanMovePrevProcess(TaskNodeProcesser taskNodeProcesser)
        {
            var workFlowNode = _WorkFlowNodeService.FirstOrDefault(a => a.NodeId == taskNodeProcesser.TaskNode.NodeId &&
                      taskNodeProcesser.TaskNode.Task.WorkFlowDesign.WorkFlowDesignId == a.WorkFlowDesignId);

            if (workFlowNode.IsAllPassThenPassRule == 1)//有一个不通过就返回了
            { 
                return true;
            }
            else if (workFlowNode.IsAllPassThenPassRule == 2) //所有返回不通过才可以
            {
                if (_TaskNodeProcesserService.IsExists(a => a.ProcessResult == 0 &&
                    a.TaskNode.TaskNodeId == taskNodeProcesser.TaskNode.TaskNodeId))
                    return false;
                else return true;//我就是最后一个返回true 
            }

            return false;
        }


        public void SubmitProcesserCheck(TaskNodeProcesser taskNodeProcesser)
        {
            if (taskNodeProcesser != null) 
            {
                //处理
                taskNodeProcesser.TaskNodeName = taskNodeProcesser.TaskNodeName.Replace("待处理", "已处理");
                _TaskNodeProcesserService.Update(taskNodeProcesser, taskNodeProcesser.GetPropInfoList(), taskNodeProcesser.GetReferenceInfoList(), false);

                //日志
                TaskNodeLog taskNodeLog = new TaskNodeLog();
                taskNodeLog.Create();
                taskNodeLog.TaskNodeId = taskNodeProcesser.TaskNodeId;
                taskNodeLog.ProcessResult = taskNodeProcesser.ProcessResult;
                taskNodeLog.ProcessComment = taskNodeProcesser.ProcessComment;
                taskNodeLog.TaskNodeProcesserId = taskNodeProcesser.TaskNodeProcesserId;
                taskNodeLog.ProcessTime = DateTime.Now;
                taskNodeLog.TaskNodeName = "流程任务_" +
                    taskNodeProcesser.TaskNode.Task.WorkFlowDesign.Name + "_" +
                    _WorkFlowNodeService.FirstOrDefault(a=>a.NodeId == taskNodeProcesser.TaskNode.NodeId &&
                      taskNodeProcesser.TaskNode.Task.WorkFlowDesign.WorkFlowDesignId == a.WorkFlowDesignId).NodeName + "_" +
                    _UserService.SingleOrDefault(taskNodeProcesser.ProcesserId).RealName +
                     "_(已处理)_" +
                    DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");

                _TaskNodeLogService.Add(taskNodeLog,false);

                _TaskNodeLogService.Commit();

                //获得下一节点
                if (taskNodeProcesser.ProcessResult == 1 || taskNodeProcesser.ProcessResult == 3)//同意和已阅
                {
                    //如果能移动到下一节点
                    if (!CanMoveNextProcess(taskNodeProcesser)) { return; }

                    var flowDesign = JsonConvert.DeserializeObject<FlowDesign> (taskNodeProcesser.TaskNode.Task.WorkFlowDesign.FlowDesignJson);

                    List<node> nextNodeList = GetNextNodeList(flowDesign, taskNodeProcesser.TaskNode.Task, taskNodeProcesser.TaskNode.NodeId);

                    //如果节点存在才做操作
                    if (nextNodeList != null && nextNodeList.Count > 0)
                    {
                        foreach (var node in nextNodeList)
                        {
                            ProcessNextNode(taskNodeProcesser.TaskNode.NodeId, 
                                taskNodeProcesser.TaskNode.Task, node, flowDesign);
                        }
                        //设置当前节点处理通过状态
                        TaskNode taskNode = _TaskNodeService.FirstOrDefault(a => a.TaskId == taskNodeProcesser.TaskNode.Task.TaskId &&
                            a.NodeId == taskNodeProcesser.TaskNode.NodeId, true);

                        taskNode.NodeResult = TaskNodeStatus.ProcessPass;
                        _TaskNodeService.Update(taskNode, taskNode.GetPropInfoList(), taskNode.GetReferenceInfoList());

                    }
                }
                else if (taskNodeProcesser.ProcessResult == 2)//不同意
                {
                    //回退
                    //如果能移动到上一节点
                    if (!CanMovePrevProcess(taskNodeProcesser)) { return; }

                    var flowDesign = JsonConvert.DeserializeObject<FlowDesign>(taskNodeProcesser.TaskNode.Task.WorkFlowDesign.FlowDesignJson);

                    List<node> nextNodeList = GetPrevNodeList(flowDesign, taskNodeProcesser.TaskNode.Task, taskNodeProcesser.TaskNode.NodeId);

                    //如果节点存在才做操作
                    if (nextNodeList != null && nextNodeList.Count > 0)
                    {
                        foreach (var node in nextNodeList)
                        {
                            ProcessPrevNode(taskNodeProcesser.TaskNode.NodeId,
                                taskNodeProcesser.TaskNode.Task, node, flowDesign);
                        }
                        //设置当前节点处理返回状态
                        TaskNode taskNode = _TaskNodeService.FirstOrDefault(a => a.TaskId == taskNodeProcesser.TaskNode.Task.TaskId &&
                            a.NodeId == taskNodeProcesser.TaskNode.NodeId, true);

                        taskNode.NodeResult = TaskNodeStatus.ProcessBack;
                        _TaskNodeService.Update(taskNode, taskNode.GetPropInfoList(), taskNode.GetReferenceInfoList());

                    }
                }
            }
  
        }

        public void Commit()
        {
            _TaskService.Commit();
        }

    }
}
