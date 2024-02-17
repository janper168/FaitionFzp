
using JKF.BLL;
using JKF.BLL.Models;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Task = JKF.DB.Models.Task;



namespace JKF.Web.Areas.WorkFlow.Controllers
{
    [Area("WorkFlow")]
    public class TaskController : BaseController
    {
        private TaskBLL _TaskBLL = new TaskBLL();
        private TaskNodeProcesserBLL _TaskNodeProcesserBLL = new TaskNodeProcesserBLL();
        private WorkFlowNodeBLL _WorkFlowNodeBLL = new WorkFlowNodeBLL();
        private WorkFlowDesignBLL _WorkFlowDesignBLL = new WorkFlowDesignBLL();
        private TaskNodeLogBLL _TaskNodeLogBLL = new TaskNodeLogBLL();
        private TaskNodeBLL _TaskNodeBLL = new TaskNodeBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WaitingIndex()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult EditForm()
        {
            return View();
        }
        public IActionResult CheckForm()
        {
            return View();
        }
        public IActionResult FinishForm()
        {
            return View();
        }
        public IActionResult detailsForm()
        {
            return View();
        }

        public IActionResult FlowForm()
        {
            return View();
        }


        public IActionResult getTaskId(string TaskNodeProcesserId)
        {
            return this.ExecuteAction(() =>
            {

                var allTasks = _TaskNodeProcesserBLL.GetEntity(a =>
                  a.TaskNodeProcesserId == TaskNodeProcesserId
                     );

                var jsonData = allTasks.TaskNode.TaskId;
                return JsonSuccess(jsonData);
            });

        }

        public IActionResult getTaskForm(string TaskId)
        {
            return this.ExecuteAction(() =>
            {

                var allTasks = _TaskBLL.GetEntity(TaskId);
             
                return JsonSuccess(allTasks);
            });

        }

        public IActionResult GetTaskNodeLog(string TaskId )
        {
            return this.ExecuteAction(() =>
            {
            

                var allTasks = _TaskNodeLogBLL.GetEntities(a =>
                  a.TaskNode.TaskId == TaskId
                     ).ToList();

                var jsonData = allTasks;
                return JsonSuccess(jsonData);
            });

        }

        public IActionResult GetFlowNode(string workFlowDesignId, string nodeId) 
        {
            return this.ExecuteAction(() =>
            {
                var data = _WorkFlowNodeBLL.GetEntity(a => a.NodeId == nodeId && a.WorkFlowDesignId == workFlowDesignId);
                return JsonSuccess(data);
            });
            
        }
        public IActionResult GetTaskNodeForm(string nodeId, string taskId)
        {
            return this.ExecuteAction(() =>
            {
                var data = _TaskNodeBLL.GetEntity(a => a.NodeId == nodeId && a.TaskId == taskId);
                return JsonSuccess(data);
            });
            
        }

        public IActionResult GetTaskNodeProcesser(string TaskNodeProcesserId)
        {
            return this.ExecuteAction(() =>
            {
                var data = _TaskNodeProcesserBLL.GetEntity(TaskNodeProcesserId);
                return JsonSuccess(data);
            });
        }

        public IActionResult GetFinishTasks(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var userId = new OperatorProvider().GetCurrent().UserId;
                var allTasks = _TaskNodeProcesserBLL.GetEntities(pagination, sort, a =>
                     a.ProcesserId == userId //是自己的待处理任务
                     && a.ProcessResult != 0 //并且已处理的
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allTasks };
                return JsonSuccess(jsonData);
            });
        }


        public IActionResult GetWaitingTasks(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var userId = new OperatorProvider().GetCurrent().UserId;
                var allTasks = _TaskNodeProcesserBLL.GetEntities(pagination, sort, a =>
                     a.ProcesserId == userId //是自己的待处理任务
                     && a.ProcessResult == 0 //并且未处理的
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allTasks };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetTasks(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var userId = new OperatorProvider().GetCurrent().UserId;
                var allTasks = _TaskBLL.GetEntities(pagination, sort, a =>
                     a.ApplyerId == userId//提单人
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allTasks };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string TaskId)
        {
            return this.ExecuteAction(() =>
            {
                var Task = _TaskBLL.GetEntity(TaskId);
                return JsonSuccess(Task);
            });
        }

        public IActionResult SaveDraft(Task Task)
        {
            return this.ExecuteAction(() =>
            {

                Task.TaskStatus = 1;//未提交
                _TaskBLL.UpdateEntity(Task);

                return JsonSuccess(null);
            });

        }

        public IActionResult Submit(Task Task)
        {
            return this.ExecuteAction(() =>
            {

                Task.TaskStatus = 2;//处理中
                _TaskBLL.StartFlow(Task);

                return JsonSuccess(null);
            });

        }

        public IActionResult SaveForm(Task Task)
        {
            return this.ExecuteAction(() =>
            {

                var user = new OperatorProvider().GetCurrent();

                var workFlowDesignId = Task.WorkFlowDesignId;

                var wfd = _WorkFlowDesignBLL.GetEntity(Task.WorkFlowDesignId);
                var flowName = wfd.Name;//流程的名字
                var userRealName = user.RealName;
                var currentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                var taskName = "流程任务_" + wfd.Name + "_发起人：" + userRealName + "_" + currentTime;
                Task.TaskStatus = 1;//未提交
                Task.ApplyerId = user.UserId;
                //Task.WorkFlowDesignId = workFlowDesignId;
                Task.WorkFlowDesign = null;
                if (string.IsNullOrEmpty(Task.TaskId))
                {
                    Task.TaskName = taskName;
                    _TaskBLL.AddEntity(Task);

                }
                else
                {
                    Task.TaskName = taskName;
                    _TaskBLL.UpdateEntity(Task);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult SubmitProcesserCheck(TaskNodeProcesser taskNodeProcesser)
        {
            return this.ExecuteAction(() =>
            {
                _TaskBLL.SubmitProcesserCheck(taskNodeProcesser);

                return JsonSuccess(null);
            });
        }

        public IActionResult RemoveForm(string TaskId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(TaskId))
                {
                    string[] arr = TaskId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Task> Tasks = new List<Task>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        Tasks.Add(new Task { TaskId = id });
                    }
                    _TaskBLL.RemoveEntities(Tasks);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });
        }
    }
}
