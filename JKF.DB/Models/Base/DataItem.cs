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
    [Table("DataItem")]
    public class DataItem : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public DataItem()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _dataItemId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string DataItemId
        {
            get
            {
                return _dataItemId;
            }
            set
            {
                _dataItemId = value;
            }
        }

        private string _parentId;

        [Required]
        [MaxLength(50)]
        public string ParentId
        {
            get
            {
                return _parentId;
            }
            set
            {
                _parentId = value;
                propInfoList.PutProperty<DataItem, string>(a => a.ParentId);
            }
        }

        private string _itemCode;

        [Required]
        [MaxLength(50)]
        public string ItemCode
        {
            get
            {
                return _itemCode;
            }
            set
            {
                _itemCode = value;
                propInfoList.PutProperty<DataItem, string>(a => a.ItemCode);
            }
        }

        private string _itemName;

        [Required]
        [MaxLength(50)]
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                _itemName = value;
                propInfoList.PutProperty<DataItem, string>(a => a.ItemName);
            }
        }

        private int _target;

        [Required]
        public int Target
        {
            get
            {
                return _target;
            }
            set
            {
                _target = value;
                propInfoList.PutProperty<DataItem, int>(a => a.Target);
            }
        }

        private int _sortCode;

        [Required]
        public int SortCode
        {
            get
            {
                return _sortCode;
            }
            set
            {
                _sortCode = value;
                propInfoList.PutProperty<DataItem, int>(a => a.SortCode);
            }
        }

        private ICollection<DataItemDetail> _dataItemDetailList;

        public virtual ICollection<DataItemDetail> DataItemDetailList
        {
            get
            {
                return _dataItemDetailList;
            }
            set
            {
                _dataItemDetailList = value;
                referenceInfoList.PutProperty<DataItem, ICollection<DataItemDetail>>(a => a.DataItemDetailList);
            }
        }

        private int _deleteMark;

        [Required]
        public int DeleteMark
        {
            get
            {
                return _deleteMark;
            }
            set
            {
                _deleteMark = value;
                propInfoList.PutProperty<DataItem, int>(a => a.DeleteMark);
            }
        }

        private int _enabledMark;

        [Required]
        public int EnabledMark
        {
            get
            {
                return _enabledMark;
            }
            set
            {
                _enabledMark = value;
                propInfoList.PutProperty<DataItem, int>(a => a.EnabledMark);
            }
        }

        private string _description;

        [MaxLength(200)]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                propInfoList.PutProperty<DataItem, string>(a => a.Description);
            }
        }

        public void Create()
        {
            DataItemId = Guid.NewGuid().ToString();
            DeleteMark = 0;
            EnabledMark = 1;
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
