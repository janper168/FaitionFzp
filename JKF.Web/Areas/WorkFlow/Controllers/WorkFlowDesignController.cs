
using JKF.BLL;
using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Controllers;
using LumiSoft.Net.IMAP;
using LumiSoft.Net.Mime.vCard;
using Microsoft.AspNetCore.Mvc;
using NPOI.POIFS.Properties;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace JKF.Web.Areas.WorkFlow.Controllers
{

    [Area("WorkFlow")]
    public class WorkFlowDesignController : BaseController
    {
        private WorkFlowDesignBLL _WorkFlowDesignBLL = new WorkFlowDesignBLL();
        private WorkFlowNodeBLL _WorkFlowNodeBLL = new WorkFlowNodeBLL();
        private WorkFlowLineBLL _WorkFlowLineBLL = new WorkFlowLineBLL();
        private ConditionRuleBLL _ConditionRuleBLL = new ConditionRuleBLL();
        private UserBLL _UserBLL = new UserBLL();
        private PostBLL _PostBLL = new PostBLL();


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult DesignForm()
        {
            return View();
        }

        public IActionResult DesignFormNodeShenpi()
        {
            return View();
        }

        public IActionResult DesignFormNodeZhihui()
        {
            return View();
        }

        public IActionResult DesignFormNodeChildflow()
        {
            return View();
        }

        public IActionResult DesignFormNodeCondition()
        {
            return View();
        }

        public IActionResult DesignFormLine()
        { 
            return View();
        }


        public IActionResult DesignFormNodeUserSelect()
        {
            return View();
        }

        public IActionResult DesignFormNodeConditionCfg()
        {
            return View();
        }

        public IActionResult getAll()
        {
            return this.ExecuteAction(() =>
            {
                var allDatas = _WorkFlowDesignBLL.GetEntities( a =>
                true
                     ).ToList().Select(a=>new { ItemValue = a.WorkFlowDesignId , ItemName=a.Name});

                return JsonSuccess(allDatas);
            });
        }

        public IActionResult GetWorkFlowDesigns(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allWorkFlowDesigns = _WorkFlowDesignBLL.GetEntities(pagination, sort, a =>
                     true
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allWorkFlowDesigns };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string WorkFlowDesignId)
        {
            return this.ExecuteAction(() =>
            {
                var WorkFlowDesign = _WorkFlowDesignBLL.GetEntity(WorkFlowDesignId);
                return JsonSuccess(WorkFlowDesign);
            });
        }

        public IActionResult SaveForm(WorkFlowDesign WorkFlowDesign)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(WorkFlowDesign.WorkFlowDesignId))
                {
                    _WorkFlowDesignBLL.AddEntity(WorkFlowDesign);
                }
                else
                {
                    _WorkFlowDesignBLL.UpdateEntity(WorkFlowDesign);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string WorkFlowDesignId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(WorkFlowDesignId))
                {
                    string[] arr = WorkFlowDesignId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<WorkFlowDesign> WorkFlowDesigns = new List<WorkFlowDesign>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        WorkFlowDesigns.Add(new WorkFlowDesign { WorkFlowDesignId = id });
                    }
                    _WorkFlowDesignBLL.RemoveEntities(WorkFlowDesigns);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });
        }

        [HttpPost]
        public IActionResult IsExistName(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _WorkFlowDesignBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _WorkFlowDesignBLL.IsExists(a => a.Name == paramValue && a.WorkFlowDesignId != keyValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }


            }
            catch (Exception ex)
            {
                return JsonFailure(500, ex.Message);
            }
        }


        public IActionResult SaveFormNode(WorkFlowNode node)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(node.WorkFlowNodeId))
                {
                    _WorkFlowNodeBLL.AddEntity(node);
                }
                else
                {
                    _WorkFlowNodeBLL.UpdateEntity(node);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult SaveFormLine(WorkFlowLine line)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(line.WorkFlowLineId))
                {
                    _WorkFlowLineBLL.AddEntity(line);
                }
                else
                {
                    _WorkFlowLineBLL.UpdateEntity(line);
                }
                return JsonSuccess(null);
            });

        }


        public IActionResult GetNodeForm(string nodeId, string workFlowDesignId)
        {
            return this.ExecuteAction(() =>
            {
                var workFlowNode = _WorkFlowNodeBLL.GetEntity(a=>a.NodeId == nodeId &&
                    a.WorkFlowDesignId == workFlowDesignId);
                return JsonSuccess(workFlowNode);
            });
        }

        public IActionResult GetLineForm(string lineId, string workFlowDesignId)
        {
            return this.ExecuteAction(() =>
            {
                var workFlowNode = _WorkFlowLineBLL.GetEntity(a => a.LineId == lineId &&
                    a.WorkFlowDesignId == workFlowDesignId);
                return JsonSuccess(workFlowNode);
            });
        }
        public class userTreeModel
        {
            public string UserId { get; set; }
            public string UserName { get; set; } 
            public string ParentId { get; set; }
            public bool IsLeaf { get; set; }
           
        }
        public IActionResult GetUsersTree()
        {
            return this.ExecuteAction(() =>
            {
                List<userTreeModel> userTreeModels = new List<userTreeModel>();

               var users =  _UserBLL.GetUsers(a=>a.Account !="admin");

                var companys = users.Select(a=>new {Id =a.Department.Company.CompanyId, Company = a.Department.Company.FullName}).Distinct().ToList();
                var departments = users.Select(a => new { Id = a.Department.DepartmentId, CompanyId = a.Department.Company.CompanyId, Department = a.Department.FullName }).Distinct().ToList();

                foreach (var item in companys) 
                {
                    userTreeModel userTreeModel = new userTreeModel();
                    userTreeModel.ParentId = "0";
                    userTreeModel.UserId = item.Id;
                    userTreeModel.UserName = item.Company;
                    userTreeModel.IsLeaf = false;

                    userTreeModels.Add(userTreeModel);
                }

                foreach (var item in departments)
                {
                    userTreeModel userTreeModel = new userTreeModel();
                   
                    userTreeModel.UserId = item.Id;
                    userTreeModel.UserName = item.Department;

                    foreach (var item2 in companys)
                    {
                        if (item2.Id == item.CompanyId)
                        {
                            userTreeModel.ParentId= item.CompanyId;
                            break;
                        }
                    }
                    userTreeModel.IsLeaf = false;
                    userTreeModels.Add(userTreeModel);
                }
                
                foreach (var item in users)
                {
                    userTreeModel userTreeModel = new userTreeModel();
                    var postName = "";
                    if (item.PostUserList.Count > 0)
                    {
                        postName = "(";
                        foreach (var post in item.PostUserList)
                        {
                            postName += _PostBLL.GetPost(post.PostId).Name + ",";
                        }
                        postName = postName.Remove(postName.Length - 1);
                        postName += ")";
                    }
                    

                    userTreeModel.UserId = item.UserId;
                    userTreeModel.UserName = item.RealName+ postName;
                    foreach (var item2 in departments)
                    {
                        if (item.Department.DepartmentId == item2.Id) 
                        {
                            userTreeModel.ParentId = item2.Id;
                            break;
                        }
                    }
                    userTreeModel.IsLeaf = true;
                    userTreeModels.Add(userTreeModel);
                }

                return JsonSuccess(userTreeModels);
            });
        }

        public IActionResult GetFlowDesign(string exceptWorkFlowDesignId)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _WorkFlowDesignBLL.GetEntities(a => a.WorkFlowDesignId != exceptWorkFlowDesignId).Select(a=>new { ItemValue = a.WorkFlowDesignId, ItemName=a.Name});
                return JsonSuccess(datas);
            });
        }

        public IActionResult GetConditionRule(string workFlowNodeId)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _ConditionRuleBLL.GetEntity(a => a.WorkFlowNodeId == workFlowNodeId);
                return JsonSuccess(datas);
            });
        }

        public IActionResult SaveConditionRule(ConditionRule conditionRule)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(conditionRule.ConditionRuleId))
                {
                    _ConditionRuleBLL.AddEntity(conditionRule);
                }
                else
                {
                    _ConditionRuleBLL.UpdateEntity(conditionRule);
                }
                return JsonSuccess(conditionRule.ConditionRuleId);//返回主键
            });
        }
    }

}
