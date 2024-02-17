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
    [HandlerLogin(FilterMode.Ignore)]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private UserBLL _UserBLL = new UserBLL();

        [HttpGet(Name = "Login")]
        public dynamic Login(string userAccount, string password)
        {
            var user = _UserBLL.GetUser(a => a.Account == userAccount,true);
            if (user == null)
            {
                throw new Exception("用户【" + userAccount + "】不存在！");
            }
            var pwd = Md5Helper.Encrypt(password, 32);
            if (pwd != user.Password)
            {
                throw new Exception("用户密码不正确！");
            }
            //账号作为会话的标识
            HttpContext.Session.Set("userAccount", Encoding.UTF8.GetBytes(user.Account));
            user.LoginStatus = 1;
            _UserBLL.UpdateUser(user);
            return true;

        }
    }
}
