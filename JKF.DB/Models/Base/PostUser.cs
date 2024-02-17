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
    public class PostUser : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public PostUser()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _postId;


        [Required]
        [MaxLength(50)]
        public string PostId
        {
            get
            {
                return _postId;
            }
            set
            {
                _postId = value;
            }
        }

        private Post _post;

        public virtual Post Post
        {
            get
            {
                return _post;
            }
            set
            {
                _post = value;
                referenceInfoList.PutProperty<PostUser, Post>(a => a.Post);
            }
        }

        private string _userId;


        [Required]
        [MaxLength(50)]
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
                propInfoList.PutProperty<PostUser, string>(a => a.UserId);
            }
        }

        private User _user;

        public virtual User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                referenceInfoList.PutProperty<PostUser, User>(a => a.User);
            }
        }

        public void Create(string PostId, string UserId)
        {
            this.PostId = PostId;
            this.UserId = UserId;
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
