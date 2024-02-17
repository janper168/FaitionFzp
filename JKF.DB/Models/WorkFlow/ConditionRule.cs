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
    public class ConditionRule : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public ConditionRule()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字：条件规则id
        private string _conditionRuleId;

        //外键：流程节点id
        private string _workFlowNodeId;

        //导航属性：流程节点
        private WorkFlowNode _workFlowNode;

        //规则配置
        private string _rulesJson;


        //关键字：条件规则id
        [Key]
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
            }
        }

        //外键：流程节点id
        [Required]
        [MaxLength(50)]
        [ForeignKey("WorkFlowNode")]
        public string WorkFlowNodeId
        {
            get
            {
                return _workFlowNodeId;
            }
            set
            {
                _workFlowNodeId = value;
                propInfoList.PutProperty<ConditionRule, string>(a => a.WorkFlowNodeId);
            }
        }

        //导航属性：流程节点
        [ForeignKey("WorkFlowNodeId")]
        public virtual WorkFlowNode WorkFlowNode
        {
            get
            {
                return _workFlowNode;
            }
            set
            {
                _workFlowNode = value;
                referenceInfoList.PutProperty<ConditionRule, WorkFlowNode>(a => a.WorkFlowNode);
            }
        }

        //规则配置
        public string RulesJson
        {
            get
            {
                return _rulesJson;
            }
            set
            {
                _rulesJson = value;
                propInfoList.PutProperty<ConditionRule, string>(a => a.RulesJson);
            }
        }


        public void Create()
        {
            ConditionRuleId = Guid.NewGuid().ToString();
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
