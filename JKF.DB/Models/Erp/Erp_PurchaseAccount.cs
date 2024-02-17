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
    public class Erp_PurchaseAccount : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_PurchaseAccount()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_PurchaseAccountId;

        //采购订单
        private Erp_PurchaseOrder _purchaseOrder;

        //采购订单id
        private string _purchaseOrderId;

        //账户
        private Erp_Account _account;

        //账户id
        private string _accountId;

        //付款金额
        private decimal _paymentAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_PurchaseAccountId
        {
            get
            {
                return _erp_PurchaseAccountId;
            }
            set
            {
                _erp_PurchaseAccountId = value;
            }
        }

        //采购订单
        [ForeignKey("PurchaseOrderId")]
        public virtual Erp_PurchaseOrder PurchaseOrder
        {
            get
            {
                return _purchaseOrder;
            }
            set
            {
                _purchaseOrder = value;
                referenceInfoList.PutProperty<Erp_PurchaseAccount, Erp_PurchaseOrder>(a => a.PurchaseOrder);
            }
        }

        //采购订单id
        [Required]
        [MaxLength(50)]
        [ForeignKey("PurchaseOrder")]
        public string PurchaseOrderId
        {
            get
            {
                return _purchaseOrderId;
            }
            set
            {
                _purchaseOrderId = value;
                propInfoList.PutProperty<Erp_PurchaseAccount, string>(a => a.PurchaseOrderId);
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
                referenceInfoList.PutProperty<Erp_PurchaseAccount, Erp_Account>(a => a.Account);
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
                propInfoList.PutProperty<Erp_PurchaseAccount, string>(a => a.AccountId	);
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
                propInfoList.PutProperty<Erp_PurchaseAccount, decimal>(a => a.PaymentAmount);
            }
        }


        public void Create()
        {
            Erp_PurchaseAccountId = Guid.NewGuid().ToString();
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
