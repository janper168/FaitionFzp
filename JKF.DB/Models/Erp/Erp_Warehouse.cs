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
    public class Erp_Warehouse : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_Warehouse()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_WarehouseId;

        //编号
        private string _number;

        //名称
        private string _name;

        //管理员
        private User _manager;

        //管理员id
        private string _managerId;

        //手机
        private string _phone;

        //地址
        private string _address;

        //备注
        private string _remark;

        //激活状态（0、1）
        private int _actived;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_WarehouseId
        {
            get
            {
                return _erp_WarehouseId;
            }
            set
            {
                _erp_WarehouseId = value;
            }
        }

        //编号
        [Required]
        [MaxLength(50)]
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                propInfoList.PutProperty<Erp_Warehouse, string>(a => a.Number);
            }
        }

        //名称
        [Required]
        [MaxLength(250)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                propInfoList.PutProperty<Erp_Warehouse, string>(a => a.Name);
            }
        }

        //管理员
        [ForeignKey("ManagerId")]
        public virtual User Manager
        {
            get
            {
                return _manager;
            }
            set
            {
                _manager = value;
                referenceInfoList.PutProperty<Erp_Warehouse, User>(a => a.Manager);
            }
        }

        //管理员id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Manager")]
        public string ManagerId
        {
            get
            {
                return _managerId;
            }
            set
            {
                _managerId = value;
                propInfoList.PutProperty<Erp_Warehouse, string>(a => a.ManagerId);
            }
        }

        //手机
        [MaxLength(250)]
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                propInfoList.PutProperty<Erp_Warehouse, string>(a => a.Phone);
            }
        }

        //地址
        [MaxLength(250)]
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                propInfoList.PutProperty<Erp_Warehouse, string>(a => a.Address);
            }
        }

        //备注
        [MaxLength(2000)]
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
                propInfoList.PutProperty<Erp_Warehouse, string>(a => a.Remark);
            }
        }

        //激活状态（0、1）
        [Required]
        public int Actived
        {
            get
            {
                return _actived;
            }
            set
            {
                _actived = value;
                propInfoList.PutProperty<Erp_Warehouse, int>(a => a.Actived);
            }
        }


        public void Create()
        {
            Erp_WarehouseId = Guid.NewGuid().ToString();
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
