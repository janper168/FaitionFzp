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
    public class ModuleExcelImportConfig : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public ModuleExcelImportConfig()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _moduleExcelImportConfigId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string ModuleExcelImportConfigId
        {
            get
            {
                return _moduleExcelImportConfigId;
            }
            set
            {
                _moduleExcelImportConfigId = value;
            }
        }

        private string _propertyName;

        [Required]
        [MaxLength(50)]
        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                _propertyName = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, string>(a => a.PropertyName);
            }
        }

        private string _displayName;

        [Required]
        [MaxLength(50)]
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, string>(a => a.DisplayName);
            }
        }

        private string _propertyType;

        [Required]
        [MaxLength(100)]
        public string PropertyType
        {
            get
            {
                return _propertyType;
            }
            set
            {
                _propertyType = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, string>(a => a.PropertyType);
            }
        }

        private Module _module;

        [Required]
        public virtual Module Module
        {
            get
            {
                return _module;
            }
            set
            {
                _module = value;
                referenceInfoList.PutProperty<ModuleExcelImportConfig, Module>(a => a.Module);
            }
        }

        private int _isPhysics;
        
        [Required]
        public int IsPhysics
        {
            get
            {
                return _isPhysics;
            }
            set
            {
                _isPhysics = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, int>(a => a.IsPhysics);
            }
        }


        private int _isParentId;

        [Required]
        public int IsParentId
        {
            get
            {
                return _isParentId;
            }
            set
            {
                _isParentId = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, int>(a => a.IsParentId);
            }
        }

        private int _isRefer;

        [Required]
        public int IsRefer
        {
            get
            {
                return _isRefer;
            }
            set
            {
                _isRefer = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, int>(a => a.IsRefer);
            }
        }

        private string _refEntityName;

        [MaxLength(100)]
        public string RefEntityName
        {
            get
            {
                return _refEntityName;
            }
            set
            {
                _refEntityName = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, string>(a => a.RefEntityName);
            }
        }

        private string _refPropertyName;

        [MaxLength(50)]
        public string RefPropertyName
        {
            get
            {
                return _refPropertyName;
            }
            set
            {
                _refPropertyName = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, string>(a => a.RefPropertyName);
            }
        }

        private string _refEntityServiceName;

        [MaxLength(100)]
        public string RefEntityServiceName
        {
            get
            {
                return _refEntityServiceName;
            }
            set
            {
                _refEntityServiceName = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, string>(a => a.RefEntityServiceName);
            }
        }


        private int _isValid;

        [Required]
        public int IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, int>(a => a.IsValid);
            }
        }

        private int _isArea;

        [Required]
        public int IsArea
        {
            get
            {
                return _isArea;
            }
            set
            {
                _isArea = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, int>(a => a.IsArea);
            }
        }

        private int _areaLayer;

        [Required]
        public int AreaLayer
        {
            get
            {
                return _areaLayer;
            }
            set
            {
                _areaLayer = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, int>(a => a.AreaLayer);
            }
        }

        private int _isDataItem;

        [Required]
        public int IsDataItem
        {
            get
            {
                return _isDataItem;
            }
            set
            {
                _isDataItem = value;
                propInfoList.PutProperty<ModuleExcelImportConfig, int>(a => a.IsDataItem);
            }
        }

        private string _itemCode;

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
                propInfoList.PutProperty<Company, int>(a => a.SortCode);
            }
        }



        public void Create()
        {
            ModuleExcelImportConfigId = Guid.NewGuid().ToString();
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
