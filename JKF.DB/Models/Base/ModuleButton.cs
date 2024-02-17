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
    public class ModuleButton : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;
        public ModuleButton()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _moduleButtonId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string ModuleButtonId
        {
            get
            {
                return _moduleButtonId;
            }
            set
            {
                _moduleButtonId = value;
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
                propInfoList.PutProperty<ModuleButton, string>(a => a.EnCode);
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
                propInfoList.PutProperty<ModuleButton, string>(a => a.Name);
            }
        }

        private string _parentId;

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
                propInfoList.PutProperty<ModuleButton, string>(a => a.ParentId);
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
                propInfoList.PutProperty<ModuleButton, int>(a => a.Target);
            }
        }

        private string _icon;

        [MaxLength(50)]
        public string Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                propInfoList.PutProperty<ModuleButton, string>(a => a.Icon);
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
                referenceInfoList.PutProperty<ModuleButton, Module>(a => a.Module);
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
            ModuleButtonId = Guid.NewGuid().ToString();
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
