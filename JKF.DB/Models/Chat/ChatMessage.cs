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
    public class ChatMessage : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public ChatMessage()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _chatMessageId;

        //会话id
        private string _chatSessionId;

        //会话
        private ChatSession _chatSession;

        //发送人id
        private string _sendUserId;

        //发送人
        private User _sendUser;

        //消息内容
        private string _messageData;

        //消息类型
        private string _messageType;

        //时间
        private DateTime _time;


        //关键字
        [Key]
        [MaxLength(50)]
        public string ChatMessageId
        {
            get
            {
                return _chatMessageId;
            }
            set
            {
                _chatMessageId = value;
            }
        }

        //会话id
        [Required]
        [MaxLength(50)]
        [ForeignKey("ChatSession")]
        public string ChatSessionId
        {
            get
            {
                return _chatSessionId;
            }
            set
            {
                _chatSessionId = value;
                propInfoList.PutProperty<ChatMessage, string>(a => a.ChatSessionId);
            }
        }

        //会话
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
                referenceInfoList.PutProperty<ChatMessage, ChatSession>(a => a.ChatSession);
            }
        }

        //发送人id
        [Required]
        [MaxLength(50)]
        [ForeignKey("SendUser")]
        public string SendUserId
        {
            get
            {
                return _sendUserId;
            }
            set
            {
                _sendUserId = value;
                propInfoList.PutProperty<ChatMessage, string>(a => a.SendUserId);
            }
        }

        //发送人
        [ForeignKey("SendUserId")]
        public virtual User SendUser
        {
            get
            {
                return _sendUser;
            }
            set
            {
                _sendUser = value;
                referenceInfoList.PutProperty<ChatMessage, User>(a => a.SendUser);
            }
        }

        //消息内容
        [Required]
        public string MessageData
        {
            get
            {
                return _messageData;
            }
            set
            {
                _messageData = value;
                propInfoList.PutProperty<ChatMessage, string>(a => a.MessageData);
            }
        }

        //消息类型
        [Required]
        [MaxLength(50)]
        public string MessageType
        {
            get
            {
                return _messageType;
            }
            set
            {
                _messageType = value;
                propInfoList.PutProperty<ChatMessage, string>(a => a.MessageType);
            }
        }

        //时间
        [Required]
        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                propInfoList.PutProperty<ChatMessage, DateTime>(a => a.Time);
            }
        }


        public void Create()
        {
            ChatMessageId = Guid.NewGuid().ToString();
            Time = DateTime.Now;
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
