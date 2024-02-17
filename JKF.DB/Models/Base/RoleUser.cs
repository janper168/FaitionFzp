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
    public class RoleUser : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public RoleUser()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _roleId;


        [Required]
        [MaxLength(50)]
        public string RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                _roleId = value;
            }
        }

        private Role _role;

        public virtual Role Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
                referenceInfoList.PutProperty<RoleUser, Role>(a => a.Role);
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
                propInfoList.PutProperty<RoleUser, string>(a => a.UserId);
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
                referenceInfoList.PutProperty<RoleUser, User>(a => a.User);
            }
        }

        public void Create()
        {
            RoleId = Guid.NewGuid().ToString();
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
