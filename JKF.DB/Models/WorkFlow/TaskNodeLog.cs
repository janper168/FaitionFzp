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
    public class TaskNodeLog : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public TaskNodeLog()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _taskNodeLogId;

        //外键：任务节点
        private string _taskNodeId;

        //导肮：节点
        private TaskNode _taskNode;

        private string _taskNodeName;

        //外键：节点处理Id
        private string _taskNodeProcesserId;

        //导肮：节点处理
        private TaskNodeProcesser _taskNodeProcesser;

        //1：同意2：不同意：3已阅
        private int _processResult;

        //处理时间
        private DateTime _processTime;

        //评论
        private string _processComment;


        //关键字
        [Key]
        [MaxLength(50)]
        public string TaskNodeLogId
        {
            get
            {
                return _taskNodeLogId;
            }
            set
            {
                _taskNodeLogId = value;
            }
        }

        //外键：节点处理Id
        [MaxLength(50)]
        [ForeignKey("TaskNodeProcesser")]
        public string TaskNodeProcesserId
        {
            get
            {
                return _taskNodeProcesserId;
            }
            set
            {
                _taskNodeProcesserId = value;
                propInfoList.PutProperty<TaskNodeLog, string>(a => a.TaskNodeProcesserId);
            }
        }

        //导肮：节点处理
        [ForeignKey("TaskNodeProcesserId")]
        public virtual TaskNodeProcesser TaskNodeProcesser
        {
            get
            {
                return _taskNodeProcesser;
            }
            set
            {
                _taskNodeProcesser = value;
                referenceInfoList.PutProperty<TaskNodeLog, TaskNodeProcesser>(a => a.TaskNodeProcesser);
            }
        }

        //外键：节点处理Id
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
                propInfoList.PutProperty<TaskNodeLog, string>(a => a.TaskNodeId);
            }
        }

        //导肮：节点处理
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
                referenceInfoList.PutProperty<TaskNodeLog, TaskNode>(a => a.TaskNode);
            }
        }

        //节点名称
        [MaxLength(500)]
        public string  TaskNodeName
        {
            get
            {
                return _taskNodeName;
            }
            set
            {
                _taskNodeName = value;
                propInfoList.PutProperty<TaskNodeLog, string>(a => a.TaskNodeName);
            }
        }

        //1：同意2：不同意：3已阅
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
                propInfoList.PutProperty<TaskNodeLog, int>(a => a.ProcessResult);
            }
        }

        //处理时间
        [Required]
        public DateTime ProcessTime
        {
            get
            {
                return _processTime;
            }
            set
            {
                _processTime = value;
                propInfoList.PutProperty<TaskNodeLog, DateTime>(a => a.ProcessTime);
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
                propInfoList.PutProperty<TaskNodeLog, string>(a => a.ProcessComment);
            }
        }


        public void Create()
        {
            TaskNodeLogId = Guid.NewGuid().ToString();
            ProcessTime = DateTime.Now;
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
