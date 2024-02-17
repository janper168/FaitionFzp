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
    public class Erp_SysSettings : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_SysSettings()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_SysSettingsId;

        //KeyName
        private string _keyName;

        //KeyValue
        private string _keyValue;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_SysSettingsId
        {
            get
            {
                return _erp_SysSettingsId;
            }
            set
            {
                _erp_SysSettingsId = value;
            }
        }

        //KeyName
        [Required]
        [MaxLength(100)]
        public string KeyName
        {
            get
            {
                return _keyName;
            }
            set
            {
                _keyName = value;
                propInfoList.PutProperty<Erp_SysSettings, string>(a => a.KeyName);
            }
        }

        //KeyValue
        [Required]
        [MaxLength(200)]
        public string KeyValue
        {
            get
            {
                return _keyValue;
            }
            set
            {
                _keyValue = value;
                propInfoList.PutProperty<Erp_SysSettings, string>(a => a.KeyValue);
            }
        }


        public void Create()
        {
            Erp_SysSettingsId = Guid.NewGuid().ToString();
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
