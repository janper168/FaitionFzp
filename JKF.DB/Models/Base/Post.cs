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
    public class Post : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Post()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _postId;

        [Key]
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

        private string _parentId;

        [Required]
        [MaxLength(50)]
        public string ParentId
        {
            get
            {
                return _parentId;
            }
            set
            {
                _parentId = value;
                propInfoList.PutProperty<Post, string>(a => a.ParentId);
            }
        }

        private string _name;

        [Required]
        [MaxLength(50)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                propInfoList.PutProperty<Post, string>(a => a.Name);
            }
        }

        private string _enCode;

        [Required]
        [MaxLength(50)]
        public string EnCode
        {
            get
            {
                return _enCode;
            }
            set
            {
                _enCode = value;
                propInfoList.PutProperty<Post, string>(a => a.EnCode);
            }
        }

        private Department _department;

        public virtual Department Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                referenceInfoList.PutProperty<Post, Department>(a => a.Department);
            }
        }

        private ICollection<PostUser> _postUserList;

        public virtual ICollection<PostUser> PostUserList
        {
            get
            {
                return _postUserList;
            }
            set
            {
                _postUserList = value;
                referenceInfoList.PutProperty<Post, ICollection<PostUser>>(a => a.PostUserList);
            }
        }

        private int _deleteMark;

        [Required]
        public int DeleteMark
        {
            get
            {
                return _deleteMark;
            }
            set
            {
                _deleteMark = value;
                propInfoList.PutProperty<Post, int>(a => a.DeleteMark);
            }
        }

        private int _enabledMark;

        [Required]
        public int EnabledMark
        {
            get
            {
                return _enabledMark;
            }
            set
            {
                _enabledMark = value;
                propInfoList.PutProperty<Post, int>(a => a.EnabledMark);
            }
        }

        private string _description;

        [MaxLength(200)]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                propInfoList.PutProperty<Post, string>(a => a.Description);
            }
        }

        public void Create()
        {
            PostId = Guid.NewGuid().ToString();
            DeleteMark = 0;
            EnabledMark = 1;
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
