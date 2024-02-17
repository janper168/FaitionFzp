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
    public class Area : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Area()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _areaId;

        [Key]
        [Required]
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
                propInfoList.PutProperty<Area, string>(a => a.ParentId);
            }
        }

        private string _areaCode;

        [Required]
        [MaxLength(50)]
        public string AreaCode
        {
            get
            {
                return _areaCode;
            }
            set
            {
                _areaCode = value;
                propInfoList.PutProperty<Area, string>(a => a.AreaCode);
            }
        }

        private string _areaName;

        [Required]
        [MaxLength(50)]
        public string AreaName
        {
            get
            {
                return _areaName;
            }
            set
            {
                _areaName = value;
                propInfoList.PutProperty<Area, string>(a => a.AreaName);
            }
        }

        private string _quickQuery;

        [Required]
        [MaxLength(200)]
        public string QuickQuery
        {
            get
            {
                return _quickQuery;
            }
            set
            {
                _quickQuery = value;
                propInfoList.PutProperty<Area, string>(a => a.QuickQuery);
            }
        }

        private string _simpleSpelling;

        [Required]
        [MaxLength(200)]
        public string SimpleSpelling
        {
            get
            {
                return _simpleSpelling;
            }
            set
            {
                _simpleSpelling = value;
                propInfoList.PutProperty<Area, string>(a => a.SimpleSpelling);
            }
        }

        private int _layer;

        [Required]
        public int Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;
                propInfoList.PutProperty<Area, int>(a => a.Layer);
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
                propInfoList.PutProperty<Area, int>(a => a.SortCode);
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
                propInfoList.PutProperty<Area, int>(a => a.DeleteMark);
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
                propInfoList.PutProperty<Area, string>(a => a.Description);
            }
        }

        private DateTime? _createDate;

        public DateTime? CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
                propInfoList.PutProperty<Area, DateTime?>(a => a.CreateDate);
            }
        }

        private string _createUserId;

        [MaxLength(50)]
        public string CreateUserId
        {
            get
            {
                return _createUserId;
            }
            set
            {
                _createUserId = value;
                propInfoList.PutProperty<Area, string>(a => a.CreateUserId);
            }
        }

        private string _createUserName;

        [MaxLength(50)]
        public string CreateUserName
        {
            get
            {
                return _createUserName;
            }
            set
            {
                _createUserName = value;
                propInfoList.PutProperty<Area, string>(a => a.CreateUserName);
            }
        }

        private DateTime? _modifyDate;

        public DateTime? ModifyDate
        {
            get
            {
                return _modifyDate;
            }
            set
            {
                _modifyDate = value;
                propInfoList.PutProperty<Area, DateTime?>(a => a.ModifyDate);
            }
        }

        private string _modifyUserId;

        [MaxLength(50)]
        public string ModifyUserId
        {
            get
            {
                return _modifyUserId;
            }
            set
            {
                _modifyUserId = value;
                propInfoList.PutProperty<Area, string>(a => a.ModifyUserId);
            }
        }

        private string _modifyUserName;

        [MaxLength(50)]
        public string ModifyUserName
        {
            get
            {
                return _modifyUserName;
            }
            set
            {
                _createUserName = value;
                propInfoList.PutProperty<Area, string>(a => a.CreateUserName);
            }
        }

        public void Create()
        {
            AreaId = Guid.NewGuid().ToString();
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
