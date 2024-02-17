using JKF.BLL.Base;
using JKF.DB.Models;
using JKF.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Org.BouncyCastle.Asn1.Cmp;
using System.Text;
using Web;

namespace JKF.ChatApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserListController : BaseController
    {
        private UserBLL _UserBLL = new UserBLL();

        [HttpGet(Name = "GetUserLsit")]
        public dynamic GetUser(string account)
        {

            var userList = _UserBLL.GetUsers(a => a.Account != account);
            if (userList != null && userList.Count >=0)
            {
                return userList;
            }
            else
            {
                throw new Exception("后台用户数据异常！");
            }
        }
    }
}
