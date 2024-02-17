using JKF.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Web
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerLoginAttribute : ActionFilterAttribute
    {
        
        private FilterMode _customMode;

        public HandlerLoginAttribute(FilterMode customMode)
        {
            _customMode = customMode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            //登录拦截是否忽略
            if (_customMode == FilterMode.Ignore)
            {
                return;
            }

            var request = context.HttpContext.Request;

            var isAjax = request.Headers["X-Requested-With"] == "XMLHttpRequest";

            OperatorModel op = new OperatorProvider().GetCurrent();

            if (op == null)
            {
                if (isAjax)
                {
                    context.Result = new ContentResult { Content = new { code = 405,errMsg = "没有找到登录信息" }.ToJson() };
                }
                else
                {
                    context.Result = new RedirectResult("~/Login/Error?error=nologin");
                }
                return;
            }
            else 
            {
                DateTime Now = DateTime.Now;
                DateTime LoginTime = DateTime.Parse(op.LoginTime);
                TimeSpan span = Now - LoginTime;
                if (span.TotalMinutes >= 60 * 24)
                {
                    if (isAjax)
                    {
                        context.Result = new ContentResult { Content = new { code = 406, errMsg = "登录超时" }.ToJson() };
                    }
                    else
                    {
                        context.Result = new RedirectResult("~/Login/Error?error=loginTimeout");
                    }
                    return;
                }
            }

            
            return;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }

    }
}
