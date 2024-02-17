using JKF.BLL.Base;
using JKF.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using JKF.DB.Models;

namespace JKF.Web.Controllers
{
    [HandlerLogin(FilterMode.Ignore)]
    public class LoginController : BaseController
    {
        private UserBLL _userBLL = new UserBLL();
        private LogBLL _logBLL = new LogBLL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckLogin(string userAccount, string userPassword)
        {
            string ip = Request.HttpContext.Connection.RemoteIpAddress != null ? Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString():"";
            //Console.WriteLine("ip addr:" + ip);
            try
            {
             
                string errMsg = _userBLL.CheckLogin(userAccount, userPassword,ip);

                if (errMsg == "")
                {
                    return JsonString(new { code = 200, errMsg });
                }
                else
                {
                    return JsonString(new { code = 400, errMsg });
                }

            }
            catch (Exception ex)
            {
                Log logEntity = new Log();
                logEntity.CategoryId = 4;
                logEntity.OperateTypeId = "1";
                logEntity.OperateType = "异常";
                logEntity.OperateAccount = userAccount;
                logEntity.OperateUserId = "";
                logEntity.ExecuteResult = 0;
                logEntity.ExecuteResultJson = ex.Message + "\r\n" + ex.StackTrace;
                logEntity.Module = "登录";
                _logBLL.AddLog(logEntity, ip);


                return JsonString(new { code = 500, errMsg = ex.Message });

            }
            
        }

        public IActionResult OutLogin()
        {
            string ip = Request.HttpContext.Connection.RemoteIpAddress != null ? Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString():"";
            var userInfo = new OperatorProvider().GetCurrent();

            try
            {
                new OperatorProvider().RemoveCurrent();

                Log logEntity = new Log();
                logEntity.CategoryId = 1;
                logEntity.OperateTypeId = "2";
                logEntity.OperateType = "登出";
                logEntity.OperateAccount = userInfo.UserAccount + "(" + userInfo.RealName + ")";
                logEntity.OperateUserId = userInfo.UserId;
                logEntity.ExecuteResult = 1;
                logEntity.ExecuteResultJson = "退出系统";
                logEntity.Module = "JKF管理系统";
                _logBLL.AddLog(logEntity, ip);
                return JsonString(new { code = 200 });
            }
            catch (Exception ex)
            {
                return JsonString(new { code = 500, errMsg = ex.Message });
            }
        }

        public IActionResult ChangeTheme(string colorCssName)
        {
            var userInfo = new OperatorProvider().GetCurrent();
            try
            {
               var user = _userBLL.GetUser(userInfo.UserId,true);
                user.ColorCssName = colorCssName;
                _userBLL.UpdateUser(user);
            }
           catch(Exception ex) 
            {
                JsonString(new { code = 500, errMsg = ex.Message });
            }
            return JsonString(new { code = 200 });
        }
        public IActionResult GetMyThemeConfig(string colorCssName)
        {
            var userInfo = new OperatorProvider().GetCurrent();
            try
            {
                var user = _userBLL.GetUser(userInfo.UserId);
                return JsonString(new { code = 200,datas=user.ColorCssName });
            }
            catch (Exception ex)
            {
                return JsonString(new { code = 500, errMsg = ex.Message });
            }

        }

        public IActionResult ChangePwd(string userPassword)
        {
            var userInfo = new OperatorProvider().GetCurrent();
            try
            {
                var user = _userBLL.GetUser(userInfo.UserId,true);

                user.Password = Md5Helper.Encrypt(userPassword, 32);
                _userBLL.UpdateUser(user);
                return JsonString(new { code = 200 });
            }
            catch (Exception ex)
            {
                return JsonString(new { code = 500, errMsg = ex.Message });
            }

        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
