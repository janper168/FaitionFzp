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
    public class TaskNodeProcesser : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public TaskNodeProcesser()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _taskNodeProcesserId;

        //外键：任务节点id
        private string _taskNodeId;

        //导航：任务节点
        private TaskNode _taskNode;

        //节点名称
        private string _taskNodeName;


        //外键：处理人id
        private string _processerId;

        //导航：处理人
        private User _processer;

        //创建时间
        private DateTime _cerateTime;

        //处理时间
        private DateTime? _processTime;

        //1：同意2：不同意3：已阅
        private int _processResult;

        //评论
        private string _processComment;


        //关键字
        [Key]
        [MaxLength(50)]
        public string TaskNodeProcesserId
        {
            get
            {
                return _taskNodeProcesserId;
            }
            set
            {
                _taskNodeProcesserId = value;
            }
        }

        //外键：任务节点id
        [Required]
        [MaxLength(50)]
        [ForeignKey("TaskNode")]
        public string TaskNodeId
        {
            get
            {
                return _taskNodeId;
            }
            set
            {
                _taskNodeId = value;
                propInfoList.PutProperty<TaskNodeProcesser, string>(a => a.TaskNodeId);
            }
        }

        //导航：任务节点
        [ForeignKey("TaskNodeId")]
        public virtual TaskNode TaskNode
        {
            get
            {
                return _taskNode;
            }
            set
            {
                _taskNode = value;
                referenceInfoList.PutProperty<TaskNodeProcesser, TaskNode>(a => a.TaskNode);
            }
        }

        //外键：处理人id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Processer")]
        public string ProcesserId
        {
            get
            {
                return _processerId;
            }
            set
            {
                _processerId = value;
                propInfoList.PutProperty<TaskNodeProcesser, string>(a => a.ProcesserId);
            }
        }


        //节点名称
        public  string  TaskNodeName
        {
            get
            {
                return _taskNodeName;
            }
            set
            {
                _taskNodeName = value;
                propInfoList.PutProperty<TaskNodeProcesser, string>(a => a.TaskNodeName);
            }
        }


        //导航：处理人
        [ForeignKey("ProcesserId")]
        public virtual User Processer
        {
            get
            {
                return _processer;
            }
            set
            {
                _processer = value;
                referenceInfoList.PutProperty<TaskNodeProcesser, User>(a => a.Processer);
            }
        }

        //创建时间
        [Required]
        public DateTime CerateTime
        {
            get
            {
                return _cerateTime;
            }
            set
            {
                _cerateTime = value;
                propInfoList.PutProperty<TaskNodeProcesser, DateTime>(a => a.CerateTime);
            }
        }

        //处理时间
        public DateTime? ProcessTime
        {
            get
            {
                return _processTime;
            }
            set
            {
                _processTime = value;
                propInfoList.PutProperty<TaskNodeProcesser, DateTime?>(a => a.ProcessTime);
            }
        }

        //1：同意2：不同意3：已阅，0：未操作
        [Required]
        public int ProcessResult
        {
            get
            {
                return _processResult;
            }
            set
            {
                _processResult = value;
                propInfoList.PutProperty<TaskNodeProcesser, int>(a => a.ProcessResult);
            }
        }

        //评论
        public string ProcessComment
        {
            get
            {
                return _processComment;
            }
            set
            {
                _processComment = value;
                propInfoList.PutProperty<TaskNodeProcesser, string>(a => a.ProcessComment);
            }
        }


        public void Create()
        {
            TaskNodeProcesserId = Guid.NewGuid().ToString();
            CerateTime = DateTime.Now;      
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
