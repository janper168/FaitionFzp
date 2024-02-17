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
    public class ModuleColumn : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public ModuleColumn()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _moduleColumnId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string ModuleColumnId
        {
            get
            {
                return _moduleColumnId;
            }
            set
            {
                _moduleColumnId = value;
            }
        }

        private string _enCode;

        [Required]
        [MaxLength(50)]
        public string EnCode
        {
            get
            {
                return _enCode;
            }
            set
            {
                _enCode = value;
                propInfoList.PutProperty<ModuleColumn, string>(a => a.EnCode);
            }
        }

        private string _name;

        [Required]
        [MaxLength(50)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                propInfoList.PutProperty<ModuleColumn, string>(a => a.Name);
            }
        }

        private Module _module;

        public virtual Module Module
        {
            get
            {
                return _module;
            }
            set
            {
                _module = value;
                referenceInfoList.PutProperty<ModuleColumn, Module>(a => a.Module);
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
                propInfoList.PutProperty<Area, int>(a => a.EnabledMark);
            }
        }

        public void Create()
        {
            ModuleColumnId = Guid.NewGuid().ToString();
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
