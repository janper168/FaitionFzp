using JKF.BLL.Base;
using JKF.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection.Emit;
using System.Text;
using Web;

namespace JKF.ChatApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : BaseController
    {
        private UserBLL _UserBLL = new UserBLL();

        [HttpGet(Name = "Logout")]
        public dynamic Logout(string userAccount)
        {
            var user = _UserBLL.GetUser(a => a.Account == userAccount,true);
            if (user == null)
            {
                throw new Exception("用户【" + userAccount + "】不存在！");
            }
            user.LoginStatus = 0;
            _UserBLL.UpdateUser(user);
            //HttpContext.Session.Remove("userAccount");
           
            return true;

        }
    }
}
