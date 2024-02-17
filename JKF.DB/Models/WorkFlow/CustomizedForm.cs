using JKF.DB.extension;
using JKF.Utils;
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
    public class CustomizedForm : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public CustomizedForm()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _customizedFormId;

        //表单名称
        private string _formName;

        //表单配置字符串
        private string _formCfg;

        //导航：表单创建人
        private User _createUser;

        //创建人id
        private string _createUserId;

        //创建日期
        private DateTime? _createDate;

        //导航：表单修改人
        private User _updateUser;

        //修改人id
        private string _updateUserId;

        //修该日期
        private DateTime? _updateDate;

        //表单的描述
        private string _description;


        //关键字
        [Key]
        [MaxLength(50)]
        public string CustomizedFormId
        {
            get
            {
                return _customizedFormId;
            }
            set
            {
                _customizedFormId = value;
            }
        }

        //表单名称
        [Required]
        [MaxLength(500)]
        public string FormName
        {
            get
            {
                return _formName;
            }
            set
            {
                _formName = value;
                propInfoList.PutProperty<CustomizedForm, string>(a => a.FormName);
            }
        }

        //表单配置字符串
        [Required]
        public string FormCfg
        {
            get
            {
                return _formCfg;
            }
            set
            {
                _formCfg = value;
                propInfoList.PutProperty<CustomizedForm, string>(a => a.FormCfg);
            }
        }

        //导航：表单创建人
        [ForeignKey("CreateUserId")]
        public virtual User CreateUser
        {
            get
            {
                return _createUser;
            }
            set
            {
                _createUser = value;
                referenceInfoList.PutProperty<CustomizedForm, User>(a => a.CreateUser);
            }
        }

        //创建人id
        [Required]
        [ForeignKey("CreateUser")]
        public string CreateUserId
        {
            get
            {
                return _createUserId;
            }
            set
            {
                _createUserId = value;
                propInfoList.PutProperty<CustomizedForm, string>(a => a.CreateUserId);
            }
        }

        //创建日期
        public DateTime? CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
                propInfoList.PutProperty<CustomizedForm, DateTime?>(a => a.CreateDate);
            }
        }

        //导航：表单修改人
        [ForeignKey("UpdateUserId")]
        public virtual User UpdateUser
        {
            get
            {
                return _updateUser;
            }
            set
            {
                _updateUser = value;
                referenceInfoList.PutProperty<CustomizedForm, User>(a => a.UpdateUser);
            }
        }

        //修改人id
        [ForeignKey("UpdateUser")]
        public string UpdateUserId
        {
            get
            {
                return _updateUserId;
            }
            set
            {
                _updateUserId = value;
                propInfoList.PutProperty<CustomizedForm, string>(a => a.UpdateUserId);
            }
        }

        //修该日期
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                _updateDate = value;
                propInfoList.PutProperty<CustomizedForm, DateTime?>(a => a.UpdateDate);
            }
        }

        //表单的描述
        [MaxLength(2000)]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                propInfoList.PutProperty<CustomizedForm, string>(a => a.Description);
            }
        }


        public void Create()
        {
            CustomizedFormId = Guid.NewGuid().ToString();
            FormCfg = "";
            var current = new OperatorProvider().GetCurrent();
            CreateDate = DateTime.Now;
            CreateUserId = current.UserId;  
        }

        public void Update()
        {         
            var current = new OperatorProvider().GetCurrent();
            UpdateDate = DateTime.Now;
            UpdateUserId = current.UserId;
            if (FormCfg == null) FormCfg = "";
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
