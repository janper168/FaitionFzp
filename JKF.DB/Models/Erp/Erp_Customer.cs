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
    public class Erp_Customer : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_Customer()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_CustomerId;

        //客户等级（1，2，3，4）
        private string _level;

        //编号
        private string _number;

        //名称
        private string _name;

        //联系人
        private string _contact;

        //手机
        private string _phone;

        //邮箱
        private string _email;

        //备注
        private string _remark;

        //激活状态
        private int _actived;

        //初期欠款金额
        private decimal _initialArearsAmount;

        //欠款状态（0，1）
        private int _arearsStatus;

        //欠款金额
        private decimal _arearsAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_CustomerId
        {
            get
            {
                return _erp_CustomerId;
            }
            set
            {
                _erp_CustomerId = value;
            }
        }

        //客户等级（1，2，3，4）
        [Required]
        [MaxLength(50)]
        public string Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                propInfoList.PutProperty<Erp_Customer, string>(a => a.Level);
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
                propInfoList.PutProperty<Erp_Customer, string>(a => a.Number);
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
                propInfoList.PutProperty<Erp_Customer, string>(a => a.Name);
            }
        }

        //联系人
        [MaxLength(250)]
        public string Contact
        {
            get
            {
                return _contact;
            }
            set
            {
                _contact = value;
                propInfoList.PutProperty<Erp_Customer, string>(a => a.Contact);
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
                propInfoList.PutProperty<Erp_Customer, string>(a => a.Phone);
            }
        }

        //邮箱
        [MaxLength(250)]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                propInfoList.PutProperty<Erp_Customer, string>(a => a.Email);
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
                propInfoList.PutProperty<Erp_Customer, string>(a => a.Remark);
            }
        }

        //激活状态
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
                propInfoList.PutProperty<Erp_Customer, int>(a => a.Actived);
            }
        }

        //初期欠款金额
        [Required]
        public decimal InitialArearsAmount
        {
            get
            {
                return _initialArearsAmount;
            }
            set
            {
                _initialArearsAmount = value;
                propInfoList.PutProperty<Erp_Customer, decimal>(a => a.InitialArearsAmount);
            }
        }

        //欠款状态（0，1）
        [Required]
        public int ArearsStatus
        {
            get
            {
                return _arearsStatus;
            }
            set
            {
                _arearsStatus = value;
                propInfoList.PutProperty<Erp_Customer, int>(a => a.ArearsStatus);
            }
        }

        //欠款金额
        [Required]
        public decimal ArearsAmount
        {
            get
            {
                return _arearsAmount;
            }
            set
            {
                _arearsAmount = value;
                propInfoList.PutProperty<Erp_Customer, decimal>(a => a.ArearsAmount);
            }
        }


        public void Create()
        {
            Erp_CustomerId = Guid.NewGuid().ToString();
            ArearsAmount = InitialArearsAmount;
            ArearsStatus = ArearsAmount > 0 ? 1 : 0;
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
