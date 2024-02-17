using JKF.BLL.Base;
using JKF.BLL.Models;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web;

namespace JKF.Web.Areas.Base.Controllers
{
    [Area("Base")]
    public class LogController : BaseController
    {
        private LogBLL _logBLL = new LogBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DetailForm()
        {
            return View();
        }
        public IActionResult RemoveLogsForm()
        {
            return View();
        }

        [InterfaceDefine("/Base/Log/GetLogList", "JKF.DB.Models.Log")]
        public IActionResult GetLogList(Pagination pagination, Sort sort,LogQueryModel queryModel)
        {
            try
            {

                var datas = _logBLL.GetLogList(pagination, sort,queryModel, "").ToList();
                var jsonData = new { pagination, sort, datas = datas };

                return JsonSuccess(jsonData);
            } 
            catch (Exception ex)
            {
                return JsonFailure(500,ex.Message);
            }

        }
        public IActionResult GetLogListForMe(Pagination pagination, Sort sort, LogQueryModel queryModel)
        {
            try
            {
                var userId = new OperatorProvider().GetCurrent().UserId;
                var datas = _logBLL.GetLogList(pagination, sort, queryModel, userId).ToList();
                var jsonData = new { pagination, sort, datas = datas };

                return JsonSuccess(jsonData);
            }
            catch (Exception ex)
            {
                return JsonFailure(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetForm(string logId) 
        {
            return ExecuteAction(() =>
            {
                var data = _logBLL.GetLog(logId);

                return JsonSuccess(data);
            });
        }

        public IActionResult RemoveLog(int categoryId, string keepTime)
        {
            return ExecuteAction(() =>
            {
                _logBLL.RemoveLog(categoryId, keepTime);

                return JsonSuccess(null);
            });

               

        }

        [HttpPost]
        public IActionResult VisitModule(string moduleName, string moduleUrl)
        {
            string ip = Request.HttpContext.Connection.RemoteIpAddress != null ? Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString():"";
            try
            {
                Log logEntity = new Log();
                var userInfo = new OperatorProvider().GetCurrent();

                logEntity.CategoryId = 2;
                logEntity.OperateTypeId = "3";
                logEntity.OperateType = "访问";

                logEntity.OperateAccount = userInfo.UserAccount + "(" + userInfo.RealName + ")";
                logEntity.OperateUserId = userInfo.UserId;
                logEntity.Module = moduleName;
                logEntity.ExecuteResult = 1;
                logEntity.ExecuteResultJson = "访问地址：" + moduleUrl;
                _logBLL.AddLog(logEntity, ip);

                return JsonSuccess(null);
            }
            catch (Exception ex)
            {
                return JsonFailure(500, ex.Message);
            }
                

           

        }


    }
}
