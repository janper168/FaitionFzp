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
    public class WorkFlowDesign : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public WorkFlowDesign()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字：工作流设计id
        private string _workFlowDesignId;

        //名称
        private string _name;

        //关联的自定义表单id：外键
        private string _customizedFormId;

        //关联的自定义表单：导航属性
        private CustomizedForm _customizedForm;

        //备注
        private string _description;

        //工作流设计JSON
        private string _flowDesignJson;

        //创建人id：外键
        private string _createUserId;

        //创建人：导航属性
        private User _createUser;

        //创建时间
        private DateTime _createDate;

        //修改人id：外键
        private string _updateUserId;

        //修改人：导航属性
        private User _updateUser;

        //修改时间
        private DateTime? _updateDate;


        //关键字：工作流设计id
        [Key]
        [MaxLength(50)]
        public string WorkFlowDesignId
        {
            get
            {
                return _workFlowDesignId;
            }
            set
            {
                _workFlowDesignId = value;
            }
        }

        //名称
        [Required]
        [MaxLength(1000)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                propInfoList.PutProperty<WorkFlowDesign, string>(a => a.Name);
            }
        }

        //关联的自定义表单id：外键
        [Required]
        [ForeignKey("CustomizedForm")]
        public string CustomizedFormId
        {
            get
            {
                return _customizedFormId;
            }
            set
            {
                _customizedFormId = value;
                propInfoList.PutProperty<WorkFlowDesign, string>(a => a.CustomizedFormId);
            }
        }

        //关联的自定义表单：导航属性
        [ForeignKey("CustomizedFormId")]
        public virtual CustomizedForm CustomizedForm
        {
            get
            {
                return _customizedForm;
            }
            set
            {
                _customizedForm = value;
                referenceInfoList.PutProperty<WorkFlowDesign, CustomizedForm>(a => a.CustomizedForm);
            }
        }

        //备注
        [MaxLength(2500)]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                propInfoList.PutProperty<WorkFlowDesign, string>(a => a.Description);
            }
        }

        //工作流设计JSON
        public string FlowDesignJson
        {
            get
            {
                return _flowDesignJson;
            }
            set
            {
                _flowDesignJson = value;
                propInfoList.PutProperty<WorkFlowDesign, string>(a => a.FlowDesignJson);
            }
        }

        //创建人id：外键
        [Required]
        [MaxLength(50)]
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
                propInfoList.PutProperty<WorkFlowDesign, string>(a => a.CreateUserId);
            }
        }

        //创建人：导航属性
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
                referenceInfoList.PutProperty<WorkFlowDesign, User>(a => a.CreateUser);
            }
        }

        //创建时间
        [Required]
        public DateTime CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
                propInfoList.PutProperty<WorkFlowDesign, DateTime>(a => a.CreateDate);
            }
        }

        //修改人id：外键
        [MaxLength(50)]
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
                propInfoList.PutProperty<WorkFlowDesign, string>(a => a.UpdateUserId);
            }
        }

        //修改人：导航属性
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
                referenceInfoList.PutProperty<WorkFlowDesign, User>(a => a.UpdateUser);
            }
        }

        //修改时间
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                _updateDate = value;
                propInfoList.PutProperty<WorkFlowDesign, DateTime?>(a => a.UpdateDate);
            }
        }


        public void Create()
        {
            WorkFlowDesignId = Guid.NewGuid().ToString();
            var current = new OperatorProvider().GetCurrent();
            CreateDate = DateTime.Now;
            CreateUserId = current.UserId;
        }

        public void Update()
        {
           
            var current = new OperatorProvider().GetCurrent();
            UpdateDate = DateTime.Now;
            UpdateUserId = current.UserId;
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
