using JKF.BLL;
using JKF.BLL.Base;
using JKF.DB.Models;
using Microsoft.AspNetCore.SignalR;
using static LumiSoft.Net.MIME.MIME_MediaTypes;

namespace JKF.ChatApi
{
    public class ChatHub:Hub
    {
        private UserBLL _userBLL = new UserBLL();
        private ChatMessageBLL _chatMessageBLL = new ChatMessageBLL();
        private static Dictionary<string ,HashSet<string>> dicts = new Dictionary<string, HashSet<string>>();

        //private static Dictionary<string, HashSet<string>> dicts = new Dictionary<string, HashSet<string>>();

        //public override async System.Threading.Tasks.Task OnConnectedAsync()
        //{
            
        //    //await Clients.All.SendAsync("Send", $"{Context.ConnectionId} joined");
        //    await Clients.Client(Context.ConnectionId).SendAsync("KeepYourConnectionId", Context.ConnectionId);
        
        //}

        public class MessageData
        {
            public int MsgType { get; set; }
            public object? MsgData { get; set; }

        }

        public async System.Threading.Tasks.Task RegisterMySessionId(string conId)
        {

            if (dicts.Keys.Contains(conId))
            {
                dicts[conId].Add(Context.ConnectionId);
            }
            else
            {
                dicts.Add(conId, new HashSet<string>());
                dicts[conId].Add(Context.ConnectionId);
            }
            await Clients.Client(Context.ConnectionId).SendAsync("KeepYourConnectionId", Context.ConnectionId);

        }

        public async System.Threading.Tasks.Task SendMessage(string conId, string meAccount, string peerAccount, MessageData data)
        {

            var chatMessage = new ChatMessage();
            DateTime cTime = DateTime.Now;
            chatMessage.MessageData = data.MsgData!=null? data.MsgData.ToString():"";
            chatMessage.MessageType = data.MsgType.ToString();
            chatMessage.ChatSessionId = conId;
            chatMessage.SendUserId = _userBLL.GetUser(a=>a.Account == meAccount,false).UserId;
            _chatMessageBLL.AddEntity(chatMessage);

            //if (dicts.Keys.Contains(conId))
            //{
            //    dicts[conId].Add(Context.ConnectionId);
            //}
            //else
            //{
            //    dicts.Add(conId, new HashSet<string>());
            //    dicts[conId].Add(Context.ConnectionId);
            //}

            foreach (var connectionId in dicts[conId])
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", conId, meAccount, peerAccount, data, cTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
          
        }

    }
}
