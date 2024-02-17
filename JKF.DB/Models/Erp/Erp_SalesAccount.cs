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
    public class Erp_SalesAccount : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_SalesAccount()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_SalesAccountId;

        //采购订单
        private Erp_SalesOrder _salesOrder;

        //采购订单id
        private string _salesOrderId;

        //账户
        private Erp_Account _account;

        //账户id
        private string _accountId	;

        //收款金额
        private decimal _collectionAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_SalesAccountId
        {
            get
            {
                return _erp_SalesAccountId;
            }
            set
            {
                _erp_SalesAccountId = value;
            }
        }

        //采购订单
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
                referenceInfoList.PutProperty<Erp_SalesAccount, Erp_SalesOrder>(a => a.SalesOrder);
            }
        }

        //采购订单id
        [Required]
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
                propInfoList.PutProperty<Erp_SalesAccount, string>(a => a.SalesOrderId);
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
                referenceInfoList.PutProperty<Erp_SalesAccount, Erp_Account>(a => a.Account);
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
                propInfoList.PutProperty<Erp_SalesAccount, string>(a => a.AccountId	);
            }
        }

        //收款金额
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
                propInfoList.PutProperty<Erp_SalesAccount, decimal>(a => a.CollectionAmount);
            }
        }


        public void Create()
        {
            Erp_SalesAccountId = Guid.NewGuid().ToString();
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
