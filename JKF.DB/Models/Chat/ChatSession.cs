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
    public class ChatSession : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public ChatSession()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //会话id
        private string _chatSessionId;

        //群组名
        private string _groupName;

        //创建时间
        private DateTime _createTime;

        //备注
        private string _remark;

        private ICollection<ChatUserSession> _chatUserSessionList;


        //会话id
        [Key]
        [MaxLength(50)]
        public string ChatSessionId
        {
            get
            {
                return _chatSessionId;
            }
            set
            {
                _chatSessionId = value;
            }
        }

        //群组名
        [MaxLength(200)]
        public string GroupName
        {
            get
            {
                return _groupName;
            }
            set
            {
                _groupName = value;
                propInfoList.PutProperty<ChatSession, string>(a => a.GroupName);
            }
        }

        //创建时间
        [Required]
        public DateTime CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
                propInfoList.PutProperty<ChatSession, DateTime>(a => a.CreateTime);
            }
        }

        //备注
        [MaxLength(1000)]
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
                propInfoList.PutProperty<ChatSession, string>(a => a.Remark);
            }
        }

        public virtual ICollection<ChatUserSession> ChatUserSessionList
        {
            get
            {
                return _chatUserSessionList;
            }
            set
            {
                _chatUserSessionList = value;
                referenceInfoList.PutProperty<ChatSession, ICollection<ChatUserSession>>(a => a.ChatUserSessionList);
            }
        }


        public void Create()
        {
            ChatSessionId = Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
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
