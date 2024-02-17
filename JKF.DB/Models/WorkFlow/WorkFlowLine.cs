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
    public class WorkFlowLine : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public WorkFlowLine()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字：流程线条id
        private string _workFlowLineId;

        //外键：流程设计id
        private string _workFlowDesignId;

        //导航属性：流程设计id
        private WorkFlowDesign _workFlowDesign;

        //线条id
        private string _lineId;

        //线条名称
        private string _lineName;

        //1：是，0：否，2：通过，3 ：驳回
        private int? _lineType;


        //关键字：流程线条id
        [Key]
        [MaxLength(50)]
        public string WorkFlowLineId
        {
            get
            {
                return _workFlowLineId;
            }
            set
            {
                _workFlowLineId = value;
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
                propInfoList.PutProperty<WorkFlowLine, string>(a => a.WorkFlowDesignId);
            }
        }

        //导航属性：流程设计id
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
                referenceInfoList.PutProperty<WorkFlowLine, WorkFlowDesign>(a => a.WorkFlowDesign);
            }
        }

        //线条id
        [Required]
        [MaxLength(100)]
        public string LineId
        {
            get
            {
                return _lineId;
            }
            set
            {
                _lineId = value;
                propInfoList.PutProperty<WorkFlowLine, string>(a => a.LineId);
            }
        }

        //线条名称
        [Required]
        [MaxLength(250)]
        public string LineName
        {
            get
            {
                return _lineName;
            }
            set
            {
                _lineName = value;
                propInfoList.PutProperty<WorkFlowLine, string>(a => a.LineName);
            }
        }

        //1：是，0：否，2：通过，3 ：驳回
        public int? LineType
        {
            get
            {
                return _lineType;
            }
            set
            {
                _lineType = value;
                propInfoList.PutProperty<WorkFlowLine, int?>(a => a.LineType);
            }
        }


        public void Create()
        {
            WorkFlowLineId = Guid.NewGuid().ToString();
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
