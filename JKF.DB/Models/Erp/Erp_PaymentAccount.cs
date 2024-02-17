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
    public class Erp_PaymentAccount : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_PaymentAccount()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_PaymentAccountId;

        //付款订单
        private Erp_PaymentOrder _paymentOrder;

        //付款订单id
        private string _paymentOrderId;

        //账户
        private Erp_Account _account;

        //账户id
        private string _accountId	;

        //付款金额
        private decimal _paymentAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_PaymentAccountId
        {
            get
            {
                return _erp_PaymentAccountId;
            }
            set
            {
                _erp_PaymentAccountId = value;
            }
        }

        //付款订单
        [ForeignKey("PaymentOrderId")]
        public virtual Erp_PaymentOrder PaymentOrder
        {
            get
            {
                return _paymentOrder;
            }
            set
            {
                _paymentOrder = value;
                referenceInfoList.PutProperty<Erp_PaymentAccount, Erp_PaymentOrder>(a => a.PaymentOrder);
            }
        }

        //付款订单id
        [Required]
        [MaxLength(50)]
        [ForeignKey("PaymentOrder")]
        public string PaymentOrderId
        {
            get
            {
                return _paymentOrderId;
            }
            set
            {
                _paymentOrderId = value;
                propInfoList.PutProperty<Erp_PaymentAccount, string>(a => a.PaymentOrderId);
            }
        }

        //账户
        [ForeignKey("AccountId")]
        public virtual Erp_Account Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
                referenceInfoList.PutProperty<Erp_PaymentAccount, Erp_Account>(a => a.Account);
            }
        }

        //账户id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Account")]
        public string AccountId	
        {
            get
            {
                return _accountId	;
            }
            set
            {
                _accountId	 = value;
                propInfoList.PutProperty<Erp_PaymentAccount, string>(a => a.AccountId	);
            }
        }

        //付款金额
        [Required]
        public decimal PaymentAmount
        {
            get
            {
                return _paymentAmount;
            }
            set
            {
                _paymentAmount = value;
                propInfoList.PutProperty<Erp_PaymentAccount, decimal>(a => a.PaymentAmount);
            }
        }


        public void Create()
        {
            Erp_PaymentAccountId = Guid.NewGuid().ToString();
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
