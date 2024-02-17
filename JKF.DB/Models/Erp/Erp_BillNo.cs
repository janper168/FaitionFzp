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
    public class Erp_BillNo : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_BillNo()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _erp_BillNoId;

        private string _billType;

        private string _dateString;

        private string _currentNo;


        [Key]
        [MaxLength(50)]
        public string Erp_BillNoId
        {
            get
            {
                return _erp_BillNoId;
            }
            set
            {
                _erp_BillNoId = value;
            }
        }

        [Required]
        [MaxLength(50)]
        public string BillType
        {
            get
            {
                return _billType;
            }
            set
            {
                _billType = value;
                propInfoList.PutProperty<Erp_BillNo, string>(a => a.BillType);
            }
        }

        [Required]
        [MaxLength(50)]
        public string DateString
        {
            get
            {
                return _dateString;
            }
            set
            {
                _dateString = value;
                propInfoList.PutProperty<Erp_BillNo, string>(a => a.DateString);
            }
        }

        [Required]
        [MaxLength(50)]
        public string CurrentNo
        {
            get
            {
                return _currentNo;
            }
            set
            {
                _currentNo = value;
                propInfoList.PutProperty<Erp_BillNo, string>(a => a.CurrentNo);
            }
        }


        public void Create()
        {
            Erp_BillNoId = Guid.NewGuid().ToString();
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
