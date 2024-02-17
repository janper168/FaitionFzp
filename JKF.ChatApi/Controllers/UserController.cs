using JKF.BLL.Base;
using JKF.DB.Models;
using JKF.Utils;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cmp;
using Web;

namespace JKF.ChatApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private UserBLL _UserBLL = new UserBLL();

        [HttpGet(Name = "GetUser")]
        public User GetUser(string account)
        {
            var userList = _UserBLL.GetUsers(a => a.Account == account);
            if (userList != null && userList.Count == 1)
            {
                return userList[0];
            }
            else
            {
                throw new Exception("后台用户数据异常！");
            }
        }
    }
}
