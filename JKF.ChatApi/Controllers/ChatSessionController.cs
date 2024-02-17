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
    public class ChatSessionController : BaseController
    {
        private ChatSessionBLL _ChatSessionBLL = new ChatSessionBLL();
        private ChatUserSessionBLL _ChatUserSessionBLL = new ChatUserSessionBLL();
        private UserBLL _UserBLL = new UserBLL();

        [HttpPost(Name = "GetChatSession")]
        public dynamic GetChatSession(string meAccount, string peerAccount)
        {

            var firstUserList = _ChatUserSessionBLL.GetEntities(a => a.ChatUser.Account == meAccount);
            var firstSessionIds = firstUserList.Select(a => new { SessionId = a.ChatSessionId }).Distinct();

            var secondUserList = _ChatUserSessionBLL.GetEntities(a => a.ChatUser.Account == peerAccount);
            var secondSessionIds = secondUserList.Select(a => new { SessionId = a.ChatSessionId }).Distinct();

            var existSessionId = "";
            foreach (var sessionId in firstSessionIds)
            {
                foreach (var sessionId2 in secondSessionIds)
                {
                    if (sessionId2.SessionId== sessionId.SessionId)
                    {
                        existSessionId = sessionId.SessionId;
                        break;
                    }
                }
                if (existSessionId != "") break;
            }
            if (existSessionId != "") return existSessionId;
            else
            {
                var ChatSession = new ChatSession();
                ChatSession.Create();
                _ChatSessionBLL.AddEntity(ChatSession);

                var ChatUserSession = new ChatUserSession();
                ChatUserSession.Create();
                var meUser = _UserBLL.GetUser(a => a.Account == meAccount);
                ChatUserSession.ChatUserId = meUser.UserId;
                ChatUserSession.ChatSessionId = ChatSession.ChatSessionId;
                _ChatUserSessionBLL.AddEntity(ChatUserSession);

                var ChatUserSession2 = new ChatUserSession();
                ChatUserSession2.Create();
                var peerUser = _UserBLL.GetUser(a => a.Account == peerAccount);
                ChatUserSession2.ChatUserId = peerUser.UserId;
                ChatUserSession2.ChatSessionId = ChatSession.ChatSessionId;
                _ChatUserSessionBLL.AddEntity(ChatUserSession2);

                return ChatSession.ChatSessionId;
            }
        }
    }
}
