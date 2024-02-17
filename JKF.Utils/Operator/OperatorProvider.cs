using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace JKF.Utils
{
    public class OperatorProvider
    {
        public static OperatorProvider Provider
        {
            get { return new OperatorProvider(); }
        }

        public OperatorModel GetCurrent()
        {
            HttpContext context = new HttpContextAccessor().HttpContext;

            OperatorModel operatorModel = null;
            if (context.User == null) return null;

            var identity = context.User.Identities;

            foreach (var iden in identity)
            {
                foreach (var claim in iden.Claims)
                {
                    if (claim.Type == "OperatorModel")
                    {
                        operatorModel = Newtonsoft.Json.JsonConvert.DeserializeObject<OperatorModel>(claim.Value);
                    }
                }
            }
            return operatorModel;
        }

        /// <summary>
        /// 设置当前登录用户的信息
        /// </summary>
        /// <param name="operatorModel"></param>
        /// <returns></returns>
        public void AddCurrent(OperatorModel operatorModel)
        {
            HttpContext context = new HttpContextAccessor().HttpContext;

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, operatorModel.UserId),
                new Claim("OperatorModel",
                Newtonsoft.Json.JsonConvert.SerializeObject(operatorModel))
            }, "IdentityCookieAuthenScheme"));

            context.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   principal, new AuthenticationProperties()
                   {
                       IsPersistent = true,
                       AllowRefresh = true
                   });

        }

        /// <summary>
        /// 注销当前用户
        /// </summary>
        public void RemoveCurrent()
        {
            HttpContext context = new HttpContextAccessor().HttpContext;
            context.SignOutAsync();
        }
    }
}
