using JKF.DB.extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Models
{
    public class ChatUserSession : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public ChatUserSession()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _chatUserSessionId;

        //导航：聊天会话
        private ChatSession _chatSession;

        //外键：聊天会话id
        private string _chatSessionId	;

        //导航：聊天用户
        private User _chatUser;

        //外键：聊天用户id
        private string _chatUserId;


        //关键字
        [Key]
        [MaxLength(50)]
        public string ChatUserSessionId
        {
            get
            {
                return _chatUserSessionId;
            }
            set
            {
                _chatUserSessionId = value;
            }
        }

        //导航：聊天会话
        [ForeignKey("ChatSessionId")]
        public virtual ChatSession ChatSession
        {
            get
            {
                return _chatSession;
            }
            set
            {
                _chatSession = value;
                referenceInfoList.PutProperty<ChatUserSession, ChatSession>(a => a.ChatSession);
            }
        }

        //外键：聊天会话id
        [Required]
        [MaxLength(50)]
        [ForeignKey("ChatSession")]
        public string ChatSessionId	
        {
            get
            {
                return _chatSessionId	;
            }
            set
            {
                _chatSessionId	 = value;
                propInfoList.PutProperty<ChatUserSession, string>(a => a.ChatSessionId	);
            }
        }

        //导航：聊天用户
        [ForeignKey("ChatUserId")]
        public virtual User ChatUser
        {
            get
            {
                return _chatUser;
            }
            set
            {
                _chatUser = value;
                referenceInfoList.PutProperty<ChatUserSession, User>(a => a.ChatUser);
            }
        }

        //外键：聊天用户id
        [Required]
        [MaxLength(50)]
        [ForeignKey("ChatUser")]
        public string ChatUserId
        {
            get
            {
                return _chatUserId;
            }
            set
            {
                _chatUserId = value;
                propInfoList.PutProperty<ChatUserSession, string>(a => a.ChatUserId);
            }
        }


        public void Create()
        {
            ChatUserSessionId = Guid.NewGuid().ToString();
        }

        public void ClearModifyInfo()
        {
            propInfoList.Clear();
            referenceInfoList.Clear();
        }
        public List<PropertyInfo> GetPropInfoList()
        {
            return propInfoList;
        }

        public List<PropertyInfo> GetReferenceInfoList()
        {
            return referenceInfoList;
        }
    }
}
