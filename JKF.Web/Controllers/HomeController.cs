using JKF.BLL.Base;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web;

namespace JKF.Web.Controllers
{
    [HandlerLogin(FilterMode.Enforce)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LogBLL _logBLL = new LogBLL();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.RealName = new OperatorProvider().GetCurrent().RealName;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            string ip = Request.HttpContext.Connection.RemoteIpAddress != null ? Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString():"";
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"路径：{exceptionHandlerPathFeature.Path},产生了一个错误：{exceptionHandlerPathFeature.Error}");

            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message + ":" + (exceptionHandlerPathFeature.Error.InnerException != null ? exceptionHandlerPathFeature.Error.InnerException.Message : "");
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            var userInfo = new OperatorProvider().GetCurrent();

            Log logEntity = new Log();
            logEntity.CategoryId = 4;
            logEntity.OperateTypeId = "1";
            logEntity.OperateType = "异常";
            logEntity.OperateAccount = userInfo.UserAccount + "(" + userInfo.RealName + ")";
            logEntity.OperateUserId = userInfo.UserId;
            logEntity.ExecuteResult = 0;
            logEntity.ExecuteResultJson = ViewBag.ExceptionMessage;
            logEntity.Module = exceptionHandlerPathFeature.Path;
            _logBLL.AddLog(logEntity, ip);

            return View();
        }
       


    }
}
