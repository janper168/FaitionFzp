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
    public class Company : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Company()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _companyId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string CompanyId
        {
            get
            {
                return _companyId;
            }
            set
            {
                _companyId = value;
            }
        }

        private int _category;

        [Required]
        public int Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                propInfoList.PutProperty<Company, int>(a => a.Category);
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
                propInfoList.PutProperty<Company, string>(a => a.ParentId);
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
                propInfoList.PutProperty<Company, string>(a => a.EnCode);
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
                propInfoList.PutProperty<Company, string>(a => a.ShortName);
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
                propInfoList.PutProperty<Company, string>(a => a.FullName);
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
                propInfoList.PutProperty<Company, string>(a => a.Nature);
            }
        }

        private string _outerPhone;

        [MaxLength(50)]
        public string OuterPhone
        {
            get
            {
                return _outerPhone;
            }
            set
            {
                _outerPhone = value;
                propInfoList.PutProperty<Company, string>(a => a.OuterPhone);
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
                propInfoList.PutProperty<Company, string>(a => a.InnerPhone);
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
                propInfoList.PutProperty<Company, string>(a => a.Fax);
            }
        }

        private string _postalcode;

        [MaxLength(50)]
        public string Postalcode
        {
            get
            {
                return _postalcode;
            }
            set
            {
                _postalcode = value;
                propInfoList.PutProperty<Company, string>(a => a.Postalcode);
            }
        }

        private string _email;

        [MaxLength(50)]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                propInfoList.PutProperty<Company, string>(a => a.Email);
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
                propInfoList.PutProperty<Company, string>(a => a.Manager);
            }
        }

        private string _provinceId;

        [MaxLength(50)]
        public string ProvinceId
        {
            get
            {
                return _provinceId;
            }
            set
            {
                _provinceId = value;
                propInfoList.PutProperty<Company, string>(a => a.ProvinceId);
            }
        }

        private string _cityId;

        [MaxLength(50)]
        public string CityId
        {
            get
            {
                return _cityId;
            }
            set
            {
                _cityId = value;
                propInfoList.PutProperty<Company, string>(a => a.CityId);
            }
        }

        private string _areaId;

        [MaxLength(50)]
        public string AreaId
        {
            get
            {
                return _areaId;
            }
            set
            {
                _areaId = value;
                propInfoList.PutProperty<Company, string>(a => a.AreaId);
            }
        }

        private string _streetId;

        [MaxLength(50)]
        public string StreetId
        {
            get
            {
                return _streetId;
            }
            set
            {
                _streetId = value;
                propInfoList.PutProperty<Company, string>(a => a.StreetId);
            }
        }

        private string _address;

        [MaxLength(50)]
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                propInfoList.PutProperty<Company, string>(a => a.Address);
            }
        }

        private string _webAddress;

        [MaxLength(200)]
        public string WebAddress
        {
            get
            {
                return _webAddress;
            }
            set
            {
                _webAddress = value;
                propInfoList.PutProperty<Company, string>(a => a.WebAddress);
            }
        }

        private DateTime? _foundedTime;

        public DateTime? FoundedTime
        {
            get
            {
                return _foundedTime;
            }
            set
            {
                _foundedTime = value;
                propInfoList.PutProperty<Company, DateTime?>(a => a.FoundedTime);
            }
        }

        private string _businessScope;

        [MaxLength(200)]
        public string BusinessScope
        {
            get
            {
                return _businessScope;
            }
            set
            {
                _businessScope = value;
                propInfoList.PutProperty<Company, string>(a => a.BusinessScope);
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
                propInfoList.PutProperty<Company, int>(a => a.DeleteMark);
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
                propInfoList.PutProperty<Company, int>(a => a.EnabledMark);
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
                propInfoList.PutProperty<Company, string>(a => a.Description);
            }
        }

        public void Create()
        {
            CompanyId = Guid.NewGuid().ToString();
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
