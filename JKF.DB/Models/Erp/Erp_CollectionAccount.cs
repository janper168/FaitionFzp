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
    public class Erp_CollectionAccount : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_CollectionAccount()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_CollectionAccountId;

        //付款订单
        private Erp_CollectionOrder _collectionOrder;

        //付款订单id
        private string _collectionOrderId;

        //账户
        private Erp_Account _account;

        //账户id
        private string _accountId	;

        //收款金额
        private decimal _collectionAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_CollectionAccountId
        {
            get
            {
                return _erp_CollectionAccountId;
            }
            set
            {
                _erp_CollectionAccountId = value;
            }
        }

        //付款订单
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
                referenceInfoList.PutProperty<Erp_CollectionAccount, Erp_CollectionOrder>(a => a.CollectionOrder);
            }
        }

        //付款订单id
        [Required]
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
                propInfoList.PutProperty<Erp_CollectionAccount, string>(a => a.CollectionOrderId);
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
                referenceInfoList.PutProperty<Erp_CollectionAccount, Erp_Account>(a => a.Account);
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
                propInfoList.PutProperty<Erp_CollectionAccount, string>(a => a.AccountId	);
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
                propInfoList.PutProperty<Erp_CollectionAccount, decimal>(a => a.CollectionAmount);
            }
        }


        public void Create()
        {
            Erp_CollectionAccountId = Guid.NewGuid().ToString();
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
