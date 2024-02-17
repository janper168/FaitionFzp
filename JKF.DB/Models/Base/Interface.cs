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
    public class Interface : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Interface()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _interfaceId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string InterfaceId
        {
            get
            {
                return _interfaceId;
            }
            set
            {
                _interfaceId = value;
            }
        }

        private string _name;

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
                propInfoList.PutProperty<Interface, string>(a => a.Name);
            }
        }

        private string _controllerName;

        [Required]
        [MaxLength(300)]
        public string ControllerName
        {
            get
            {
                return _controllerName;
            }
            set
            {
                _controllerName = value;
                propInfoList.PutProperty<Interface, string>(a => a.ControllerName);
            }
        }

        private string _actionName;

        [Required]
        [MaxLength(100)]
        public string ActionName
        {
            get
            {
                return _actionName;
            }
            set
            {
                _actionName = value;
                propInfoList.PutProperty<Interface, string>(a => a.ActionName);
            }
        }

        private string _returnTypeName;

        [Required]
        [MaxLength(300)]
        public string ReturnTypeName
        {
            get
            {
                return _returnTypeName;
            }
            set
            {
                _returnTypeName = value;
                propInfoList.PutProperty<Interface, string>(a => a.ReturnTypeName);
            }
        }

        private string _address;

        [Required]
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                propInfoList.PutProperty<Interface, string>(a => a.Address);
            }
        }

        private string _propertiesJson;

        [Required]
        public string PropertiesJson
        {
            get
            {
                return _propertiesJson;
            }
            set
            {
                _propertiesJson = value;
                propInfoList.PutProperty<Interface, string>(a => a.PropertiesJson);
            }
        }

        public void Create()
        {
            InterfaceId = Guid.NewGuid().ToString();
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
