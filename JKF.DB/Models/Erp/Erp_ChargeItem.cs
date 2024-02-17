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
    public class Erp_ChargeItem : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_ChargeItem()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_ChargeItemId;

        //名称
        private string _name;

        //收支类型（收入，支出）
        private int _type;

        //备注
        private string _remark;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_ChargeItemId
        {
            get
            {
                return _erp_ChargeItemId;
            }
            set
            {
                _erp_ChargeItemId = value;
            }
        }

        //名称
        [Required]
        [MaxLength(100)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                propInfoList.PutProperty<Erp_ChargeItem, string>(a => a.Name);
            }
        }

        //收支类型（收入，支出）
        [Required]
        public int Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                propInfoList.PutProperty<Erp_ChargeItem, int>(a => a.Type);
            }
        }

        //备注
        [MaxLength(2000)]
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
                propInfoList.PutProperty<Erp_ChargeItem, string>(a => a.Remark);
            }
        }


        public void Create()
        {
            Erp_ChargeItemId = Guid.NewGuid().ToString();
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
