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
    public class Module : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Module()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _moduleId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string ModuleId
        {
            get
            {
                return _moduleId;
            }
            set
            {
                _moduleId = value;
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
                propInfoList.PutProperty<Module, string>(a => a.EnCode);
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
                propInfoList.PutProperty<Module, string>(a => a.Name);
            }
        }

        private string _description;

        [MaxLength(1024)]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                propInfoList.PutProperty<Module, string>(a => a.Description);
            }
        }

        private string _urlAddress;

        [MaxLength(1024)]
        public string UrlAddress
        {
            get
            {
                return _urlAddress;
            }
            set
            {
                _urlAddress = value;
                propInfoList.PutProperty<Module, string>(a => a.UrlAddress);
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
                propInfoList.PutProperty<Module, string>(a => a.ParentId);
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
                propInfoList.PutProperty<Module, string>(a => a.Icon);
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
                propInfoList.PutProperty<Module, int>(a => a.Target);
            }
        }

        private int _sort;

        [Required]
        public int Sort
        {
            get
            {
                return _sort;
            }
            set
            {
                _sort = value;
                propInfoList.PutProperty<Module, int>(a => a.Sort);
            }
        }


        private ICollection<ModuleButton> _moduleButtonList;

        public virtual ICollection<ModuleButton> ModuleButtonList
        {
            get
            {
                return _moduleButtonList;
            }
            set
            {
                _moduleButtonList = value;
                referenceInfoList.PutProperty<Module, ICollection<ModuleButton>>(a => a.ModuleButtonList);
            }
        }

        private ICollection<ModuleColumn> _moduleColumnList;

        public virtual ICollection<ModuleColumn> ModuleColumnList
        {
            get
            {
                return _moduleColumnList;
            }
            set
            {
                _moduleColumnList = value;
                referenceInfoList.PutProperty<Module, ICollection<ModuleColumn>>(a => a.ModuleColumnList);
            }
        }

        private ICollection<ModuleForm> _moduleFormList;

        public virtual ICollection<ModuleForm> ModuleFormList
        {
            get
            {
                return _moduleFormList;
            }
            set
            {
                _moduleFormList = value;
                referenceInfoList.PutProperty<Module, ICollection<ModuleForm>>(a => a.ModuleFormList);
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
            ModuleId = Guid.NewGuid().ToString();
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
