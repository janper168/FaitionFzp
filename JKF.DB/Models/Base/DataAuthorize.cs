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
    public class DataAuthorize : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public DataAuthorize()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _dataAuthorizeId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string DataAuthorizeId
        {
            get
            {
                return _dataAuthorizeId;
            }
            set
            {
                _dataAuthorizeId = value;
            }
        }

        private Interface _interface;

        public virtual Interface Interface
        {
            get
            {
                return _interface;
            }
            set
            {
                _interface = value;
                referenceInfoList.PutProperty<DataAuthorize, Interface>(a => a.Interface);
            }
        }

        private int _objectType;

        [Required]
        public int ObjectType
        {
            get
            {
                return _objectType;
            }
            set
            {
                _objectType = value;
                propInfoList.PutProperty<DataAuthorize, int>(a => a.ObjectType);
            }
        }

        private string _objectId;

        [Required]
        [MaxLength(50)]
        public string ObjectId
        {
            get
            {
                return _objectId;
            }
            set
            {
                _objectId = value;
                propInfoList.PutProperty<DataAuthorize, string>(a => a.ObjectId);
            }
        }

        private string _conditionsJson;

        [Required]
        public string ConditionsJson
        {
            get
            {
                return _conditionsJson;
            }
            set
            {
                _conditionsJson = value;
                propInfoList.PutProperty<DataAuthorize, string>(a => a.ConditionsJson);
            }
        }

        private string _formula;

        [Required]
        public string Formula
        {
            get
            {
                return _formula;
            }
            set
            {
                _formula = value;
                propInfoList.PutProperty<DataAuthorize, string>(a => a.Formula);
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
                propInfoList.PutProperty<DataAuthorize, int>(a => a.EnabledMark);
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
                propInfoList.PutProperty<DataAuthorize, int>(a => a.DeleteMark);
            }
        }

        public void Create()
        {
            DataAuthorizeId = Guid.NewGuid().ToString();
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
