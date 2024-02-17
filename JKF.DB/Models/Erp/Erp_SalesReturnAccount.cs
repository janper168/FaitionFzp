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
    public class Erp_SalesReturnAccount : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_SalesReturnAccount()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_SalesReturnAccountId;

        //退货订单
        private Erp_SalesReturnOrder _salesReturnOrder;

        //退回订单id
        private string _salesReturnOrderId;

        //账户
        private Erp_Account _account;

        //账户id
        private string _accountId	;

        //付款金额
        private decimal _paymentAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_SalesReturnAccountId
        {
            get
            {
                return _erp_SalesReturnAccountId;
            }
            set
            {
                _erp_SalesReturnAccountId = value;
            }
        }

        //退货订单
        [ForeignKey("SalesReturnOrderId")]
        public virtual Erp_SalesReturnOrder SalesReturnOrder
        {
            get
            {
                return _salesReturnOrder;
            }
            set
            {
                _salesReturnOrder = value;
                referenceInfoList.PutProperty<Erp_SalesReturnAccount, Erp_SalesReturnOrder>(a => a.SalesReturnOrder);
            }
        }

        //退回订单id
        [Required]
        [MaxLength(50)]
        [ForeignKey("SalesReturnOrder")]
        public string SalesReturnOrderId
        {
            get
            {
                return _salesReturnOrderId;
            }
            set
            {
                _salesReturnOrderId = value;
                propInfoList.PutProperty<Erp_SalesReturnAccount, string>(a => a.SalesReturnOrderId);
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
                referenceInfoList.PutProperty<Erp_SalesReturnAccount, Erp_Account>(a => a.Account);
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
                propInfoList.PutProperty<Erp_SalesReturnAccount, string>(a => a.AccountId	);
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
                propInfoList.PutProperty<Erp_SalesReturnAccount, decimal>(a => a.PaymentAmount);
            }
        }


        public void Create()
        {
            Erp_SalesReturnAccountId = Guid.NewGuid().ToString();
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
