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
    public class Erp_Account : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_Account()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_AccountId;

        //账户类型
        private string _type;

        //编号
        private string _number;

        //名称
        private string _name;

        //开户人
        private string _holder;

        //开户账户
        private string _cardNumber;

        //备注
        private string _remark;

        //激活状态（0，1）
        private int? _actived;

        //期初金额
        private decimal _initialBalanceAmount;

        //余额
        private decimal _balanceAmount;

        //余额状态
        private int _balanceStatus;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_AccountId
        {
            get
            {
                return _erp_AccountId;
            }
            set
            {
                _erp_AccountId = value;
            }
        }

        //账户类型
        [Required]
        [MaxLength(100)]
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                propInfoList.PutProperty<Erp_Account, string>(a => a.Type);
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
                propInfoList.PutProperty<Erp_Account, string>(a => a.Number);
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
                propInfoList.PutProperty<Erp_Account, string>(a => a.Name);
            }
        }

        //开户人
        [MaxLength(50)]
        public string Holder
        {
            get
            {
                return _holder;
            }
            set
            {
                _holder = value;
                propInfoList.PutProperty<Erp_Account, string>(a => a.Holder);
            }
        }

        //开户账户
        [MaxLength(250)]
        public string CardNumber
        {
            get
            {
                return _cardNumber;
            }
            set
            {
                _cardNumber = value;
                propInfoList.PutProperty<Erp_Account, string>(a => a.CardNumber);
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
                propInfoList.PutProperty<Erp_Account, string>(a => a.Remark);
            }
        }

        //激活状态（0，1）
        public int? Actived
        {
            get
            {
                return _actived;
            }
            set
            {
                _actived = value;
                propInfoList.PutProperty<Erp_Account, int?>(a => a.Actived);
            }
        }

        //期初金额
        [Required]
        public decimal InitialBalanceAmount
        {
            get
            {
                return _initialBalanceAmount;
            }
            set
            {
                _initialBalanceAmount = value;
                propInfoList.PutProperty<Erp_Account, decimal>(a => a.InitialBalanceAmount);
            }
        }

        //余额
        [Required]
        public decimal BalanceAmount
        {
            get
            {
                return _balanceAmount;
            }
            set
            {
                _balanceAmount = value;
                propInfoList.PutProperty<Erp_Account, decimal>(a => a.BalanceAmount);
            }
        }

        //余额状态
        [Required]
        public int BalanceStatus
        {
            get
            {
                return _balanceStatus;
            }
            set
            {
                _balanceStatus = value;
                propInfoList.PutProperty<Erp_Account, int>(a => a.BalanceStatus);
            }
        }


        public void Create()
        {
            Erp_AccountId = Guid.NewGuid().ToString();
            BalanceAmount = InitialBalanceAmount;
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
