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
    public class WorkFlowNode : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public WorkFlowNode()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //流程节点id：关键字
        private string _workFlowNodeId;

        //流程设计id：外键
        private string _workFlowDesignId;

        //导航属性：关联的流程设计id
        private WorkFlowDesign _workFlowDesign;

        //流程节点名称
        private string _nodeName;

        //流程节点代码
        private string _nodeId;

        //节点类型（shenpi审核、zhihui知会、childflow子流程、condition条件）
        private string _nodeType;

        //多人通过才通过（1:是，2:不是）
        private int? _isAllPassThenPassRule;

        //处理人同提单人通过（1:是，2:不是）
        private int? _isProcessorTheSameToApplyerPassRule;

        //处理人同上一节点处理人通过（1:是，2:不是）
        private int? _isProcessorTheSameToLastProcessorRule;

        //处理者类型：1上级主管，2.部门经理，3：总经理，4：指定岗位，5：指定人
        private int? _processorTypeRule;

        //处理人id列表（分号隔开）
        private string _processorIds;

        //当前节点是子流程，指定子流程Id：外键
        private string _childFlowDesignId;

        //条件节点对应条件规则id
        private string _conditionRuleId;


        //流程节点id：关键字
        [Key]
        [MaxLength(50)]
        public string WorkFlowNodeId
        {
            get
            {
                return _workFlowNodeId;
            }
            set
            {
                _workFlowNodeId = value;
            }
        }

        //流程设计id：外键
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
                propInfoList.PutProperty<WorkFlowNode, string>(a => a.WorkFlowDesignId);
            }
        }

        //导航属性：关联的流程设计id
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
                referenceInfoList.PutProperty<WorkFlowNode, WorkFlowDesign>(a => a.WorkFlowDesign);
            }
        }

        //流程节点名称
        [Required]
        [MaxLength(500)]
        public string NodeName
        {
            get
            {
                return _nodeName;
            }
            set
            {
                _nodeName = value;
                propInfoList.PutProperty<WorkFlowNode, string>(a => a.NodeName);
            }
        }

        //流程节点代码
        [Required]
        [MaxLength(100)]
        public string NodeId
        {
            get
            {
                return _nodeId;
            }
            set
            {
                _nodeId = value;
                propInfoList.PutProperty<WorkFlowNode, string>(a => a.NodeId);
            }
        }

        //节点类型（shenpi审核、zhihui知会、childflow子流程、condition条件）
        [Required]
        [MaxLength(100)]
        public string NodeType
        {
            get
            {
                return _nodeType;
            }
            set
            {
                _nodeType = value;
                propInfoList.PutProperty<WorkFlowNode, string>(a => a.NodeType);
            }
        }

        //多人通过才通过（1:是，2:不是）
        public int? IsAllPassThenPassRule
        {
            get
            {
                return _isAllPassThenPassRule;
            }
            set
            {
                _isAllPassThenPassRule = value;
                propInfoList.PutProperty<WorkFlowNode, int?>(a => a.IsAllPassThenPassRule);
            }
        }

        //处理人同提单人通过（1:是，2:不是）
        public int? IsProcessorTheSameToApplyerPassRule
        {
            get
            {
                return _isProcessorTheSameToApplyerPassRule;
            }
            set
            {
                _isProcessorTheSameToApplyerPassRule = value;
                propInfoList.PutProperty<WorkFlowNode, int?>(a => a.IsProcessorTheSameToApplyerPassRule);
            }
        }

        //处理人同上一节点处理人通过（1:是，2:不是）
        public int? IsProcessorTheSameToLastProcessorRule
        {
            get
            {
                return _isProcessorTheSameToLastProcessorRule;
            }
            set
            {
                _isProcessorTheSameToLastProcessorRule = value;
                propInfoList.PutProperty<WorkFlowNode, int?>(a => a.IsProcessorTheSameToLastProcessorRule);
            }
        }

        //处理者类型：1上级主管，2.部门经理，3：总经理，4：指定岗位，5：指定人
        public int? ProcessorTypeRule
        {
            get
            {
                return _processorTypeRule;
            }
            set
            {
                _processorTypeRule = value;
                propInfoList.PutProperty<WorkFlowNode, int?>(a => a.ProcessorTypeRule);
            }
        }

        //处理人id列表（分号隔开）
        public string ProcessorIds
        {
            get
            {
                return _processorIds;
            }
            set
            {
                _processorIds = value;
                propInfoList.PutProperty<WorkFlowNode, string>(a => a.ProcessorIds);
            }
        }

        //当前节点是子流程，指定子流程Id：外键
        [MaxLength(50)]
        public string ChildFlowDesignId
        {
            get
            {
                return _childFlowDesignId;
            }
            set
            {
                _childFlowDesignId = value;
                propInfoList.PutProperty<WorkFlowNode, string>(a => a.ChildFlowDesignId);
            }
        }

        //条件节点对应条件规则id
        [MaxLength(50)]
        public string ConditionRuleId
        {
            get
            {
                return _conditionRuleId;
            }
            set
            {
                _conditionRuleId = value;
                propInfoList.PutProperty<WorkFlowNode, string>(a => a.ConditionRuleId);
            }
        }


        public void Create()
        {
            WorkFlowNodeId = Guid.NewGuid().ToString();
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
