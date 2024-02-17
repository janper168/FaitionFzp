using JKF.DB.extension;
using JKF.Utils;
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
    public class Erp_FinanceFlow : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_FinanceFlow()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_FinanceFlowId;

        //类型
        private string _type;

        //账户
        private Erp_Account _account;

        private string _accountId;

        //变化前金额
        private decimal _amountBefore;

        //变化金额
        private decimal _amountChange;

        //变化后金额
        private decimal _amountAfter;

        private Erp_PurchaseOrder _purchaseOrder;

        private string _purchaseOrderId;

        private Erp_PurchaseOrder _voidPurchaseOrder;

        private string _voidPurchaseOrderId;

        private Erp_PurchaseReturnOrder _purchaseReturnOrder;

        private string _purchaseReturnOrderId;

        private Erp_PurchaseReturnOrder _voidPurchaseReturnOrder;

        private string _voidPurchaseReturnOrderId;

        private Erp_SalesOrder _salesOrder;

        private string _salesOrderId;

        private Erp_SalesOrder _voidSalesOrder;

        private string _voidSalesOrderId;

        private Erp_SalesReturnOrder _salesReturnOrder;

        private string _salesReturnOrderId;

        private Erp_SalesReturnOrder _voidSalesReturnOrder;

        private string _voidSalesReturnOrderId;

        private Erp_PaymentOrder _paymentOrder;

        private string _paymentOrderId;

        private Erp_PaymentOrder _voidPaymentOrder;

        private string _voidPaymentOrderId;

        private Erp_CollectionOrder _collectionOrder;

        private string _collectionOrderId;

        private Erp_CollectionOrder _voidCollectionOrder;

        private string _voidCollectionOrderId;

        private Erp_AccountTransferRecord _accountTransferRecord;

        private string _accountTransferRecordId;

        private Erp_AccountTransferRecord _voidAccountTransferRecord;

        private string _voidAccountTransferRecordId;

        //创建人
        private User _createUser;

        //创建人id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_FinanceFlowId
        {
            get
            {
                return _erp_FinanceFlowId;
            }
            set
            {
                _erp_FinanceFlowId = value;
            }
        }

        //类型
        [Required]
        [MaxLength(50)]
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.Type);
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
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_Account>(a => a.Account);
            }
        }

        [Required]
        [MaxLength(50)]
        [ForeignKey("Account")]
        public string AccountId
        {
            get
            {
                return _accountId;
            }
            set
            {
                _accountId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.AccountId);
            }
        }

        //变化前金额
        [Required]
        public decimal AmountBefore
        {
            get
            {
                return _amountBefore;
            }
            set
            {
                _amountBefore = value;
                propInfoList.PutProperty<Erp_FinanceFlow, decimal>(a => a.AmountBefore);
            }
        }

        //变化金额
        [Required]
        public decimal AmountChange
        {
            get
            {
                return _amountChange;
            }
            set
            {
                _amountChange = value;
                propInfoList.PutProperty<Erp_FinanceFlow, decimal>(a => a.AmountChange);
            }
        }

        //变化后金额
        [Required]
        public decimal AmountAfter
        {
            get
            {
                return _amountAfter;
            }
            set
            {
                _amountAfter = value;
                propInfoList.PutProperty<Erp_FinanceFlow, decimal>(a => a.AmountAfter);
            }
        }

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
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_PurchaseOrder>(a => a.PurchaseOrder);
            }
        }

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
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.PurchaseOrderId);
            }
        }

        [ForeignKey("VoidPurchaseOrderId")]
        public virtual Erp_PurchaseOrder VoidPurchaseOrder
        {
            get
            {
                return _voidPurchaseOrder;
            }
            set
            {
                _voidPurchaseOrder = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_PurchaseOrder>(a => a.VoidPurchaseOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidPurchaseOrder")]
        public string VoidPurchaseOrderId
        {
            get
            {
                return _voidPurchaseOrderId;
            }
            set
            {
                _voidPurchaseOrderId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.VoidPurchaseOrderId);
            }
        }

        [ForeignKey("PurchaseReturnOrderId")]
        public virtual Erp_PurchaseReturnOrder PurchaseReturnOrder
        {
            get
            {
                return _purchaseReturnOrder;
            }
            set
            {
                _purchaseReturnOrder = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_PurchaseReturnOrder>(a => a.PurchaseReturnOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("PurchaseReturnOrder")]
        public string PurchaseReturnOrderId
        {
            get
            {
                return _purchaseReturnOrderId;
            }
            set
            {
                _purchaseReturnOrderId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.PurchaseReturnOrderId);
            }
        }

        [ForeignKey("VoidPurchaseReturnOrderId")]
        public virtual Erp_PurchaseReturnOrder VoidPurchaseReturnOrder
        {
            get
            {
                return _voidPurchaseReturnOrder;
            }
            set
            {
                _voidPurchaseReturnOrder = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_PurchaseReturnOrder>(a => a.VoidPurchaseReturnOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidPurchaseReturnOrder")]
        public string VoidPurchaseReturnOrderId
        {
            get
            {
                return _voidPurchaseReturnOrderId;
            }
            set
            {
                _voidPurchaseReturnOrderId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.VoidPurchaseReturnOrderId);
            }
        }

        [ForeignKey("SalesOrderId")]
        public virtual Erp_SalesOrder SalesOrder
        {
            get
            {
                return _salesOrder;
            }
            set
            {
                _salesOrder = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_SalesOrder>(a => a.SalesOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("SalesOrder")]
        public string SalesOrderId
        {
            get
            {
                return _salesOrderId;
            }
            set
            {
                _salesOrderId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.SalesOrderId);
            }
        }

        [ForeignKey("VoidSalesOrderId")]
        public virtual Erp_SalesOrder VoidSalesOrder
        {
            get
            {
                return _voidSalesOrder;
            }
            set
            {
                _voidSalesOrder = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_SalesOrder>(a => a.VoidSalesOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidSalesOrder")]
        public string VoidSalesOrderId
        {
            get
            {
                return _voidSalesOrderId;
            }
            set
            {
                _voidSalesOrderId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.VoidSalesOrderId);
            }
        }

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
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_SalesReturnOrder>(a => a.SalesReturnOrder);
            }
        }

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
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.SalesReturnOrderId);
            }
        }

        [ForeignKey("VoidSalesReturnOrderId")]
        public virtual Erp_SalesReturnOrder VoidSalesReturnOrder
        {
            get
            {
                return _voidSalesReturnOrder;
            }
            set
            {
                _voidSalesReturnOrder = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_SalesReturnOrder>(a => a.VoidSalesReturnOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidSalesReturnOrder")]
        public string VoidSalesReturnOrderId
        {
            get
            {
                return _voidSalesReturnOrderId;
            }
            set
            {
                _voidSalesReturnOrderId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.VoidSalesReturnOrderId);
            }
        }

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
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_PaymentOrder>(a => a.PaymentOrder);
            }
        }

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
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.PaymentOrderId);
            }
        }

        [ForeignKey("VoidPaymentOrderId")]
        public virtual Erp_PaymentOrder VoidPaymentOrder
        {
            get
            {
                return _voidPaymentOrder;
            }
            set
            {
                _voidPaymentOrder = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_PaymentOrder>(a => a.VoidPaymentOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidPaymentOrder")]
        public string VoidPaymentOrderId
        {
            get
            {
                return _voidPaymentOrderId;
            }
            set
            {
                _voidPaymentOrderId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.VoidPaymentOrderId);
            }
        }

        [ForeignKey("CollectionOrderId")]
        public virtual Erp_CollectionOrder CollectionOrder
        {
            get
            {
                return _collectionOrder;
            }
            set
            {
                _collectionOrder = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_CollectionOrder>(a => a.CollectionOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("CollectionOrder")]
        public string CollectionOrderId
        {
            get
            {
                return _collectionOrderId;
            }
            set
            {
                _collectionOrderId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.CollectionOrderId);
            }
        }

        [ForeignKey("VoidCollectionOrderId")]
        public virtual Erp_CollectionOrder VoidCollectionOrder
        {
            get
            {
                return _voidCollectionOrder;
            }
            set
            {
                _voidCollectionOrder = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_CollectionOrder>(a => a.VoidCollectionOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidCollectionOrder")]
        public string VoidCollectionOrderId
        {
            get
            {
                return _voidCollectionOrderId;
            }
            set
            {
                _voidCollectionOrderId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.VoidCollectionOrderId);
            }
        }

        [ForeignKey("AccountTransferRecordId")]
        public virtual Erp_AccountTransferRecord AccountTransferRecord
        {
            get
            {
                return _accountTransferRecord;
            }
            set
            {
                _accountTransferRecord = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_AccountTransferRecord>(a => a.AccountTransferRecord);
            }
        }

        [MaxLength(50)]
        [ForeignKey("AccountTransferRecord")]
        public string AccountTransferRecordId
        {
            get
            {
                return _accountTransferRecordId;
            }
            set
            {
                _accountTransferRecordId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.AccountTransferRecordId);
            }
        }

        [ForeignKey("VoidAccountTransferRecordId")]
        public virtual Erp_AccountTransferRecord VoidAccountTransferRecord
        {
            get
            {
                return _voidAccountTransferRecord;
            }
            set
            {
                _voidAccountTransferRecord = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, Erp_AccountTransferRecord>(a => a.VoidAccountTransferRecord);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidAccountTransferRecord")]
        public string VoidAccountTransferRecordId
        {
            get
            {
                return _voidAccountTransferRecordId;
            }
            set
            {
                _voidAccountTransferRecordId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.VoidAccountTransferRecordId);
            }
        }

        //创建人
        [ForeignKey("CreateUserId")]
        public virtual User CreateUser
        {
            get
            {
                return _createUser;
            }
            set
            {
                _createUser = value;
                referenceInfoList.PutProperty<Erp_FinanceFlow, User>(a => a.CreateUser);
            }
        }

        //创建人id
        [Required]
        [MaxLength(50)]
        [ForeignKey("CreateUser")]
        public string CreateUserId
        {
            get
            {
                return _createUserId;
            }
            set
            {
                _createUserId = value;
                propInfoList.PutProperty<Erp_FinanceFlow, string>(a => a.CreateUserId);
            }
        }

        //创建时间
        [Required]
        public DateTime CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
                propInfoList.PutProperty<Erp_FinanceFlow, DateTime>(a => a.CreateTime);
            }
        }


        public void Create()
        {
            Erp_FinanceFlowId = Guid.NewGuid().ToString();
            CreateUserId = new OperatorProvider().GetCurrent().UserId;
            CreateTime = DateTime.Now;
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
