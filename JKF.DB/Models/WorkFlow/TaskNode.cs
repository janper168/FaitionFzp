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
    public class TaskNode : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public TaskNode()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _taskNodeId;

        //任务id：外键
        private string _taskId;

        //任务：导航
        private Task _task;

        //任务节点名称
        private string _taskNodeName;

        //节点id
        private string _nodeId;

        //处理人id
        private string _processerIds;

 
        private int _nodeResult;

        //创建时间
        private DateTime _cerateTime;


        //关键字
        [Key]
        [MaxLength(50)]
        public string TaskNodeId
        {
            get
            {
                return _taskNodeId;
            }
            set
            {
                _taskNodeId = value;
            }
        }

        //任务id：外键
        [Required]
        [MaxLength(50)]
        [ForeignKey("Task")]
        public string TaskId
        {
            get
            {
                return _taskId;
            }
            set
            {
                _taskId = value;
                propInfoList.PutProperty<TaskNode, string>(a => a.TaskId);
            }
        }

        //任务：导航
        [ForeignKey("TaskId")]
        public virtual Task Task
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
                referenceInfoList.PutProperty<TaskNode, Task>(a => a.Task);
            }
        }

        //任务节点名称
        [Required]
        [MaxLength(500)]
        public string TaskNodeName
        {
            get
            {
                return _taskNodeName;
            }
            set
            {
                _taskNodeName = value;
                propInfoList.PutProperty<TaskNode, string>(a => a.TaskNodeName);
            }
        }

        //节点id
        [Required]
        [MaxLength(250)]
        public string NodeId
        {
            get
            {
                return _nodeId;
            }
            set
            {
                _nodeId = value;
                propInfoList.PutProperty<TaskNode, string>(a => a.NodeId);
            }
        }

        //处理人id
        public string ProcesserIds
        {
            get
            {
                return _processerIds;
            }
            set
            {
                _processerIds = value;
                propInfoList.PutProperty<TaskNode, string>(a => a.ProcesserIds);
            }
        }

        
        [Required]
        public int NodeResult
        {
            get
            {
                return _nodeResult;
            }
            set
            {
                _nodeResult = value;
                propInfoList.PutProperty<TaskNode, int>(a => a.NodeResult);
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
                propInfoList.PutProperty<TaskNode, DateTime>(a => a.CerateTime);
            }
        }


        public void Create()
        {
            TaskNodeId = Guid.NewGuid().ToString();
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
