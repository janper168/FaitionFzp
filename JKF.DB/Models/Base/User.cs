using JKF.DB.extension;
using JKF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Models
{
    public class User : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public User()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _userId;




        [Key]
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
                propInfoList.PutProperty<User, string>(a => a.EnCode);
            }
        }

        private ICollection<RoleUser> _roleUserList;

        public virtual ICollection<RoleUser> RoleUserList
        {
            get
            {
                return _roleUserList;
            }
            set
            {
                _roleUserList = value;
                referenceInfoList.PutProperty<Role, ICollection<RoleUser>>(a => a.RoleUserList);
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
                referenceInfoList.PutProperty<User, Department>(a => a.Department);
            }
        }

        private string _account;

        [Required]
        [MaxLength(50)]
        public string Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
                propInfoList.PutProperty<User, string>(a => a.Account);
            }
        }

        private string _password;

        [Required]
        [MaxLength(50)]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                propInfoList.PutProperty<User, string>(a => a.Password);
            }
        }

        private string _realName;

        [Required]
        [MaxLength(50)]
        public string RealName
        {
            get
            {
                return _realName;
            }
            set
            {
                _realName = value;
                propInfoList.PutProperty<User, string>(a => a.RealName);
            }
        }

        private string _nickName ;

        [MaxLength(50)]
        public string NickName 
        {
            get
            {
                return _nickName ;
            }
            set
            {
                _nickName  = value;
                propInfoList.PutProperty<User, string>(a => a.NickName );
            }
        }

        private string _headIcon;

        [MaxLength(200)]
        public string HeadIcon
        {
            get
            {
                return _headIcon;
            }
            set
            {
                _headIcon = value;
                propInfoList.PutProperty<User, string>(a => a.HeadIcon);
            }
        }

        private string _quickQuery;

        [MaxLength(200)]
        public string QuickQuery
        {
            get
            {
                return _quickQuery;
            }
            set
            {
                _quickQuery = value;
                propInfoList.PutProperty<User, string>(a => a.QuickQuery);
            }
        }

        private string _simpleSpelling;

        [MaxLength(200)]
        public string SimpleSpelling
        {
            get
            {
                return _simpleSpelling;
            }
            set
            {
                _simpleSpelling = value;
                propInfoList.PutProperty<User, string>(a => a.SimpleSpelling);
            }
        }

        private int? _gender;

        public int? Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                propInfoList.PutProperty<User, int?>(a => a.Gender);
            }
        }

        private int? _loginStatus;
        public int? LoginStatus
        {
            get
            {
                return _loginStatus;
            }
            set
            {
                _loginStatus = value;
                propInfoList.PutProperty<User, int?>(a => a.LoginStatus);
            }
        }

        private DateTime? _birthday;

        public DateTime? Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
                propInfoList.PutProperty<User, DateTime?>(a => a.Birthday);
            }
        }

        private string _mobile;

        [MaxLength(50)]
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                _mobile = value;
                propInfoList.PutProperty<User, string>(a => a.Mobile);
            }
        }

        private string _telephone;

        [MaxLength(50)]
        public string Telephone
        {
            get
            {
                return _telephone;
            }
            set
            {
                _telephone = value;
                propInfoList.PutProperty<User, string>(a => a.Telephone);
            }
        }

        private string _email;

        [MaxLength(50)]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                propInfoList.PutProperty<User, string>(a => a.Email);
            }
        }

        private string _oICQ;

        [MaxLength(50)]
        public string OICQ
        {
            get
            {
                return _oICQ;
            }
            set
            {
                _oICQ = value;
                propInfoList.PutProperty<User, string>(a => a.OICQ);
            }
        }

        private string _weChat;

        [MaxLength(50)]
        public string WeChat
        {
            get
            {
                return _weChat;
            }
            set
            {
                _weChat = value;
                propInfoList.PutProperty<User, string>(a => a.WeChat);
            }
        }

        private string _mSN;

        [MaxLength(50)]
        public string MSN
        {
            get
            {
                return _mSN;
            }
            set
            {
                _mSN = value;
                propInfoList.PutProperty<User, string>(a => a.MSN);
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
                propInfoList.PutProperty<User, int>(a => a.DeleteMark);
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
                propInfoList.PutProperty<User, int>(a => a.EnabledMark);
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
                propInfoList.PutProperty<User, string>(a => a.Description);
            }
        }

        //主题颜色
        private string _colorCssName;

        [MaxLength(50)]
        public string ColorCssName
        {
            get
            {
                return _colorCssName;
            }
            set
            {
                _colorCssName = value;
                propInfoList.PutProperty<User, string>(a => a.ColorCssName);
            }
        }

        public void Create()
        {
            UserId = Guid.NewGuid().ToString();
            EnabledMark = 1;
            DeleteMark = 0;
            Password = Md5Helper.Encrypt("123", 32);//默认123
            ColorCssName = "bg-color-lightblue";//主题颜色为浅蓝色默认
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
