using JKF.BLL;
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
    public class ChatMessageController : BaseController
    {
        //private ChatSessionBLL _ChatSessionBLL = new ChatSessionBLL();
        //private ChatUserSessionBLL _ChatUserSessionBLL = new ChatUserSessionBLL();
        private ChatMessageBLL _ChatMessageBLL = new ChatMessageBLL();
        //private UserBLL _UserBLL = new UserBLL();

        [HttpPost(Name = "GetChatMessage")]
        public dynamic GetChatMessage(string conId)
        {
            var list = _ChatMessageBLL.GetEntities(a => a.ChatSessionId == conId);
            return list;
        }
    }
}
