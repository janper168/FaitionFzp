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
    public class Erp_PurchaseReturnAccount : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_PurchaseReturnAccount()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_PurchaseReturnAccountId;

        //采购退货订单
        private Erp_PurchaseOrder _purchaseReturnOrder;

        //采购退货订单id
        private string _purchaseReturnOrderId;

        //账户
        private Erp_Account _account;

        //账户id
        private string _accountId	;

        //付款金额
        private decimal _collectionAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_PurchaseReturnAccountId
        {
            get
            {
                return _erp_PurchaseReturnAccountId;
            }
            set
            {
                _erp_PurchaseReturnAccountId = value;
            }
        }

        //采购退货订单
        [ForeignKey("PurchaseOrderId")]
        public virtual Erp_PurchaseOrder PurchaseReturnOrder
        {
            get
            {
                return _purchaseReturnOrder;
            }
            set
            {
                _purchaseReturnOrder = value;
                referenceInfoList.PutProperty<Erp_PurchaseReturnAccount, Erp_PurchaseOrder>(a => a.PurchaseReturnOrder);
            }
        }

        //采购退货订单id
        [Required]
        [MaxLength(50)]
        [ForeignKey("PurchaseOrder")]
        public string PurchaseReturnOrderId
        {
            get
            {
                return _purchaseReturnOrderId;
            }
            set
            {
                _purchaseReturnOrderId = value;
                propInfoList.PutProperty<Erp_PurchaseReturnAccount, string>(a => a.PurchaseReturnOrderId);
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
                referenceInfoList.PutProperty<Erp_PurchaseReturnAccount, Erp_Account>(a => a.Account);
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
                propInfoList.PutProperty<Erp_PurchaseReturnAccount, string>(a => a.AccountId	);
            }
        }

        //付款金额
        [Required]
        public decimal CollectionAmount
        {
            get
            {
                return _collectionAmount;
            }
            set
            {
                _collectionAmount = value;
                propInfoList.PutProperty<Erp_PurchaseReturnAccount, decimal>(a => a.CollectionAmount);
            }
        }


        public void Create()
        {
            Erp_PurchaseReturnAccountId = Guid.NewGuid().ToString();

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
