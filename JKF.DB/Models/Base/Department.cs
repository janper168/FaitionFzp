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
    public class Department : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Department()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _departmentId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string DepartmentId
        {
            get
            {
                return _departmentId;
            }
            set
            {
                _departmentId = value;
            }
        }

        private Company _company;

        [MaxLength(50)]
        public virtual Company Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
                referenceInfoList.PutProperty<Department, Company>(a => a.Company);
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
                propInfoList.PutProperty<Department, string>(a => a.ParentId);
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
                propInfoList.PutProperty<Department, string>(a => a.EnCode);
            }
        }

        private string _fullName;

        [Required]
        [MaxLength(50)]
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
                propInfoList.PutProperty<Department, string>(a => a.FullName);
            }
        }

        private string _shortName;

        [Required]
        [MaxLength(50)]
        public string ShortName
        {
            get
            {
                return _shortName;
            }
            set
            {
                _shortName = value;
                propInfoList.PutProperty<Department, string>(a => a.ShortName);
            }
        }

        private string _nature;

        [Required]
        [MaxLength(50)]
        public string Nature
        {
            get
            {
                return _nature;
            }
            set
            {
                _nature = value;
                propInfoList.PutProperty<Department, string>(a => a.Nature);
            }
        }

        private string _manager;

        [MaxLength(50)]
        public string Manager
        {
            get
            {
                return _manager;
            }
            set
            {
                _manager = value;
                propInfoList.PutProperty<Department, string>(a => a.Manager);
            }
        }

        private string _ourPhone;

        [MaxLength(50)]
        public string OurPhone
        {
            get
            {
                return _ourPhone;
            }
            set
            {
                _ourPhone = value;
                propInfoList.PutProperty<Department, string>(a => a.OurPhone);
            }
        }

        private string _innerPhone;

        [MaxLength(50)]
        public string InnerPhone
        {
            get
            {
                return _innerPhone;
            }
            set
            {
                _innerPhone = value;
                propInfoList.PutProperty<Department, string>(a => a.InnerPhone);
            }
        }

        private string _fax;

        [MaxLength(50)]
        public string Fax
        {
            get
            {
                return _fax;
            }
            set
            {
                _fax = value;
                propInfoList.PutProperty<Department, string>(a => a.Fax);
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
                propInfoList.PutProperty<Department, int>(a => a.SortCode);
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
                propInfoList.PutProperty<Department, int>(a => a.DeleteMark);
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
                propInfoList.PutProperty<Department, int>(a => a.EnabledMark);
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
                propInfoList.PutProperty<Department, string>(a => a.Description);
            }
        }

        public void Create()
        {
            DepartmentId = Guid.NewGuid().ToString();
            EnabledMark = 1;
            DeleteMark = 0;
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
