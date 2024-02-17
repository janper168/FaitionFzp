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
    public class Erp_Suppiler : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_Suppiler()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_SuppilerId;

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

        //地址
        private string _address;

        //开户行名称
        private string _bankName;

        //开户行账号
        private string _bankAccount;

        //备注
        private string _remark;

        //激活状态（0，1）
        private int _actived;

        //期初欠款金额
        private decimal _initialArearsAmount;

        //欠款状态
        private int _arearsStatus;

        //欠款金额
        private decimal _arearsAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_SuppilerId
        {
            get
            {
                return _erp_SuppilerId;
            }
            set
            {
                _erp_SuppilerId = value;
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
                propInfoList.PutProperty<Erp_Suppiler, string>(a => a.Number);
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
                propInfoList.PutProperty<Erp_Suppiler, string>(a => a.Name);
            }
        }

        //联系人
        [MaxLength(50)]
        public string Contact
        {
            get
            {
                return _contact;
            }
            set
            {
                _contact = value;
                propInfoList.PutProperty<Erp_Suppiler, string>(a => a.Contact);
            }
        }

        //手机
        [MaxLength(50)]
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                propInfoList.PutProperty<Erp_Suppiler, string>(a => a.Phone);
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
                propInfoList.PutProperty<Erp_Suppiler, string>(a => a.Email);
            }
        }

        //地址
        [MaxLength(1000)]
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                propInfoList.PutProperty<Erp_Suppiler, string>(a => a.Address);
            }
        }

        //开户行名称
        [MaxLength(250)]
        public string BankName
        {
            get
            {
                return _bankName;
            }
            set
            {
                _bankName = value;
                propInfoList.PutProperty<Erp_Suppiler, string>(a => a.BankName);
            }
        }

        //开户行账号
        [MaxLength(250)]
        public string BankAccount
        {
            get
            {
                return _bankAccount;
            }
            set
            {
                _bankAccount = value;
                propInfoList.PutProperty<Erp_Suppiler, string>(a => a.BankAccount);
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
                propInfoList.PutProperty<Erp_Suppiler, string>(a => a.Remark);
            }
        }

        //激活状态（0，1）
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
                propInfoList.PutProperty<Erp_Suppiler, int>(a => a.Actived);
            }
        }

        //期初欠款金额
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
                propInfoList.PutProperty<Erp_Suppiler, decimal>(a => a.InitialArearsAmount);
            }
        }

        //欠款状态
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
                propInfoList.PutProperty<Erp_Suppiler, int>(a => a.ArearsStatus);
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
                propInfoList.PutProperty<Erp_Suppiler, decimal>(a => a.ArearsAmount);
            }
        }


        public void Create()
        {
            Erp_SuppilerId = Guid.NewGuid().ToString();
            ArearsAmount = InitialArearsAmount;
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
