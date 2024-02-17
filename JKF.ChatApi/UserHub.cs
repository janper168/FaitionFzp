using Microsoft.AspNetCore.SignalR;
using NPOI.POIFS.Crypt.Dsig;

namespace JKF.ChatApi
{
    public class UserHub:Hub
    {

        //public class MessageData
        //{
        //    public int MsgType { get; set; }
        //    public object? MsgData { get; set; }
  
        //}

        public Task Login(string account)
        { 
            return Clients.All.SendAsync("LoginNotify", account);
        }

        public Task Logout(string account)
        {
            return Clients.All.SendAsync("LogoutNotify", account);
        }
    }
}
