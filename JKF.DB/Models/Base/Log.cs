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
    public class Log : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Log()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        private string _logId;

        [Key]
        [Required]
        [MaxLength(50)]
        public string LogId
        {
            get
            {
                return _logId;
            }
            set
            {
                _logId = value;
            }
        }

        private int _categoryId;

        [Required]
        public int CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                _categoryId = value;
                propInfoList.PutProperty<Log, int>(a => a.CategoryId);
            }
        }

        private string _sourceObjectId;

        [MaxLength(50)]
        public string SourceObjectId
        {
            get
            {
                return _sourceObjectId;
            }
            set
            {
                _sourceObjectId = value;
                propInfoList.PutProperty<Log, string>(a => a.SourceObjectId);
            }
        }

        private string _sourceContentJson;

        public string SourceContentJson
        {
            get
            {
                return _sourceContentJson;
            }
            set
            {
                _sourceContentJson = value;
                propInfoList.PutProperty<Log, string>(a => a.SourceContentJson);
            }
        }

        private DateTime? _operateTime;

        public DateTime? OperateTime
        {
            get
            {
                return _operateTime;
            }
            set
            {
                _operateTime = value;
                propInfoList.PutProperty<Log, DateTime?>(a => a.OperateTime);
            }
        }

        private string _operateUserId;

        [Required]
        [MaxLength(50)]
        public string OperateUserId
        {
            get
            {
                return _operateUserId;
            }
            set
            {
                _operateUserId = value;
                propInfoList.PutProperty<Log, string>(a => a.OperateUserId);
            }
        }

        private string _operateAccount;

        [Required]
        [MaxLength(200)]
        public string OperateAccount
        {
            get
            {
                return _operateAccount;
            }
            set
            {
                _operateAccount = value;
                propInfoList.PutProperty<Log, string>(a => a.OperateAccount);
            }
        }

        private string _operateTypeId;

        [Required]
        [MaxLength(50)]
        public string OperateTypeId
        {
            get
            {
                return _operateTypeId;
            }
            set
            {
                _operateTypeId = value;
                propInfoList.PutProperty<Log, string>(a => a.OperateTypeId);
            }
        }

        private string _operateType;

        [Required]
        [MaxLength(50)]
        public string OperateType
        {
            get
            {
                return _operateType;
            }
            set
            {
                _operateType = value;
                propInfoList.PutProperty<Log, string>(a => a.OperateType);
            }
        }

        private string _module;

        [Required]
        [MaxLength(200)]
        public string Module
        {
            get
            {
                return _module;
            }
            set
            {
                _module = value;
                propInfoList.PutProperty<Log, string>(a => a.Module);
            }
        }

        private string _iPAddress;

        [Required]
        [MaxLength(50)]
        public string IPAddress
        {
            get
            {
                return _iPAddress;
            }
            set
            {
                _iPAddress = value;
                propInfoList.PutProperty<Log, string>(a => a.IPAddress);
            }
        }

        private string _iPAddressName;

        [MaxLength(200)]
        public string IPAddressName
        {
            get
            {
                return _iPAddressName;
            }
            set
            {
                _iPAddressName = value;
                propInfoList.PutProperty<Log, string>(a => a.IPAddressName);
            }
        }

        private string _host;

        [MaxLength(200)]
        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
                propInfoList.PutProperty<Log, string>(a => a.Host);
            }
        }

        private string _browser;

        [MaxLength(200)]
        public string Browser
        {
            get
            {
                return _browser;
            }
            set
            {
                _browser = value;
                propInfoList.PutProperty<Log, string>(a => a.Browser);
            }
        }

        private int? _executeResult;

        public int? ExecuteResult
        {
            get
            {
                return _executeResult;
            }
            set
            {
                _executeResult = value;
                propInfoList.PutProperty<Log, int?>(a => a.ExecuteResult);
            }
        }

        private string _executeResultJson;

        [Required]
        public string ExecuteResultJson
        {
            get
            {
                return _executeResultJson;
            }
            set
            {
                _executeResultJson = value;
                propInfoList.PutProperty<Log, string>(a => a.ExecuteResultJson);
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
                propInfoList.PutProperty<Log, string>(a => a.Description);
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
                propInfoList.PutProperty<Log, int>(a => a.DeleteMark);
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
                propInfoList.PutProperty<Log, int>(a => a.EnabledMark);
            }
        }

        public void Create()
        {
            LogId = Guid.NewGuid().ToString();
            DeleteMark = 0;
            EnabledMark = 1;
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
