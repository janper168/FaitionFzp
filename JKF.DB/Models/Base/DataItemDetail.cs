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
    public class DataItemDetail : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public DataItemDetail()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _dataItemDetailId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string DataItemDetailId
        {
            get
            {
                return _dataItemDetailId;
            }
            set
            {
                _dataItemDetailId = value;
            }
        }

        //private string _dataItemId;

        //[Required]
        //[MaxLength(50)]
        //public string DataItemId
        //{
        //    get
        //    {
        //        return _dataItemId;
        //    }
        //    set
        //    {
        //        _dataItemId = value;
        //        propInfoList.PutProperty<DataItemDetail, string>(a => a.DataItemId);
        //    }
        //}

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
                propInfoList.PutProperty<DataItemDetail, string>(a => a.ParentId);
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
                propInfoList.PutProperty<DataItemDetail, string>(a => a.ItemName);
            }
        }

        private string _itemValue;

        [Required]
        [MaxLength(50)]
        public string ItemValue
        {
            get
            {
                return _itemValue;
            }
            set
            {
                _itemValue = value;
                propInfoList.PutProperty<DataItemDetail, string>(a => a.ItemValue);
            }
        }

        private int _isDefault;

        [Required]
        public int IsDefault
        {
            get
            {
                return _isDefault;
            }
            set
            {
                _isDefault = value;
                propInfoList.PutProperty<DataItemDetail, int>(a => a.IsDefault);
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
                propInfoList.PutProperty<DataItemDetail, int>(a => a.SortCode);
            }
        }

        private DataItem _dataItem;

        public virtual DataItem DataItem
        {
            get
            {
                return _dataItem;
            }
            set
            {
                _dataItem = value;
                referenceInfoList.PutProperty<DataItemDetail, DataItem>(a => a.DataItem);
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
                propInfoList.PutProperty<DataItemDetail, int>(a => a.DeleteMark);
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
                propInfoList.PutProperty<DataItemDetail, int>(a => a.EnabledMark);
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
                propInfoList.PutProperty<DataItemDetail, string>(a => a.Description);
            }
        }

        public void Create()
        {
            DataItemDetailId = Guid.NewGuid().ToString();
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
