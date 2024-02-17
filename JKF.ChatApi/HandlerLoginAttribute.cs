using JKF.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text;

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

            var session = context.HttpContext.Session;

            // 从Session中取出数据
            byte[] bytes;
            session.TryGetValue("userAccount", out bytes);
            if (bytes == null)
                return;
            // throw new Exception("用户会话超时！");
            else
            {
                var userAccount = Encoding.UTF8.GetString(bytes);
                if (userAccount != null)
                {
                    return;
                }
                else
                {
                    return;//throw new Exception("用户会话超时！");
                }
            }

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
