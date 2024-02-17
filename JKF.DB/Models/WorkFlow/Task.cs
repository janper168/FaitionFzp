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
    public class Task : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Task()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _taskId;

        //任务名
        private string _taskName;

        //外键：流程设计id
        private string _workFlowDesignId;

        //导航：流程设计
        private WorkFlowDesign _workFlowDesign;

        //导航：申请人
        private User _applyer;

        //外键：申请人id
        private string _applyerId;

        //表单内容Json（项目名值对）
        private string _formContentJson;

        //流程状态：1.未提交2.处理中3.处理完毕
        private int _taskStatus;

        //任务备注描述
        private string _description;

        //父亲任务的id
        private string _parentTaskId;

        //创建时间
        private DateTime _createDate;


        //关键字
        [Key]
        [MaxLength(50)]
        public string TaskId
        {
            get
            {
                return _taskId;
            }
            set
            {
                _taskId = value;
            }
        }

        //任务名
        [Required]
        [MaxLength(200)]
        public string TaskName
        {
            get
            {
                return _taskName;
            }
            set
            {
                _taskName = value;
                propInfoList.PutProperty<Task, string>(a => a.TaskName);
            }
        }

        //外键：流程设计id
        [Required]
        [MaxLength(50)]
        [ForeignKey("WorkFlowDesign")]
        public string WorkFlowDesignId
        {
            get
            {
                return _workFlowDesignId;
            }
            set
            {
                _workFlowDesignId = value;
                propInfoList.PutProperty<Task, string>(a => a.WorkFlowDesignId);
            }
        }

        //导航：流程设计
        [ForeignKey("WorkFlowDesignId")]
        public virtual WorkFlowDesign WorkFlowDesign
        {
            get
            {
                return _workFlowDesign;
            }
            set
            {
                _workFlowDesign = value;
                referenceInfoList.PutProperty<Task, WorkFlowDesign>(a => a.WorkFlowDesign);
            }
        }

        //导航：申请人
        [ForeignKey("ApplyerId")]
        public virtual User Applyer
        {
            get
            {
                return _applyer;
            }
            set
            {
                _applyer = value;
                referenceInfoList.PutProperty<Task, User>(a => a.Applyer);
            }
        }

        //外键：申请人id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Applyer")]
        public string ApplyerId
        {
            get
            {
                return _applyerId;
            }
            set
            {
                _applyerId = value;
                propInfoList.PutProperty<Task, string>(a => a.ApplyerId);
            }
        }

        //表单内容Json（项目名值对）
        public string FormContentJson
        {
            get
            {
                return _formContentJson;
            }
            set
            {
                _formContentJson = value;
                propInfoList.PutProperty<Task, string>(a => a.FormContentJson);
            }
        }

        //流程状态：1.未提交2.处理中3.处理完毕
        [Required]
        public int TaskStatus
        {
            get
            {
                return _taskStatus;
            }
            set
            {
                _taskStatus = value;
                propInfoList.PutProperty<Task, int>(a => a.TaskStatus);
            }
        }

        public string ParentTaskId 
        {
            get
            {
                return _parentTaskId;
            }
            set
            {
                _parentTaskId = value;
                propInfoList.PutProperty<Task, string>(a => a.ParentTaskId);
            }
        }

        //任务备注描述
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                propInfoList.PutProperty<Task, string>(a => a.Description);
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
                propInfoList.PutProperty<Task, DateTime>(a => a.CreateDate);
            }
        }


        public void Create()
        {
            TaskId = Guid.NewGuid().ToString();
            CreateDate = DateTime.Now;
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
