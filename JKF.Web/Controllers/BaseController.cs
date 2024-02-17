using JKF.BLL.Base;
using JKF.DB.Models;
using JKF.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web;

namespace JKF.Web.Controllers
{
    [HandlerLogin(FilterMode.Enforce)]
    public class BaseController : Controller
    {
        protected JsonSerializerSettings _settings = null;
        private LogBLL _logBLL = new LogBLL();

        public BaseController()
        {
            _settings = new JsonSerializerSettings();
            _settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;//忽略循环引用
        }

        public IActionResult JsonString(object data)
        {
            return Content(JsonConvert.SerializeObject(data, _settings), "text/json",Encoding.UTF8);
        }

        public IActionResult JsonSuccess(object datas)
        {
            var data = new { code = 200, datas = datas };
            return Content(JsonConvert.SerializeObject(data, _settings), "text/json", Encoding.UTF8);
        }

        public IActionResult JsonFailure(int code, string errMsg)
        {
            var data = new { code, errMsg };
            return Content(JsonConvert.SerializeObject(data, _settings), "text/json", Encoding.UTF8);
        }


        private void WriteExceptionLog(Exception ex,string ip) {
            var userInfo = new OperatorProvider().GetCurrent();

            Log logEntity = new Log();
            logEntity.CategoryId = 4;
            logEntity.OperateTypeId = "1";
            logEntity.OperateType = "异常";
            logEntity.OperateAccount = userInfo.UserAccount + "(" + userInfo.RealName + ")";
            logEntity.OperateUserId = userInfo.UserId;
            logEntity.ExecuteResult = 0;
            logEntity.ExecuteResultJson = ex.Message + ":\n" + (ex.InnerException != null ? ex.InnerException.Message : "") + ":\n" + ex.StackTrace;
            logEntity.Module = HttpContext.Request.Path;
            _logBLL.AddLog(logEntity,ip);

           
        }

        protected IActionResult ExecuteAction(Func<IActionResult> action) 
        {
            string ip = Request.HttpContext.Connection.RemoteIpAddress != null ? Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString():"";
            var userInfo = new OperatorProvider().GetCurrent();

            Log logEntity = new Log();
            logEntity.CategoryId = 3;
            logEntity.OperateTypeId = "1";
            logEntity.OperateType = "操作";
            logEntity.OperateAccount = userInfo.UserAccount + "(" + userInfo.RealName + ")";
            logEntity.OperateUserId = userInfo.UserId;
            logEntity.ExecuteResult = 1;
            logEntity.ExecuteResultJson = "操作地址：" + HttpContext.Request.Path;
            logEntity.Module = HttpContext.Request.Path;
            
            try
            { 

                IActionResult t = action();
                logEntity.ExecuteResult = 1;
                _logBLL.AddLog(logEntity, ip);
                return t;
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = 0;
                _logBLL.AddLog(logEntity, ip);

                WriteExceptionLog(ex, ip);

                return JsonFailure(500, ex.Message);               
            }

        }

       
    }
}
