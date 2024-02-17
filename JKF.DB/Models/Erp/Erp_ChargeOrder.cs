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
    public class Erp_ChargeOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_ChargeOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_ChargeOrderId;

        //收支类型
        private string _type;

        //编号
        private string _number;

        //供应商
        private Erp_Suppiler _suppiler;

        //工业商id
        private string _suppilerId;

        //客户
        private Erp_Customer _customer;

        //客户id
        private string _customerId;

        //处理人
        private User _processor;

        //处理人id
        private string _processorId;

        //处理时间
        private DateTime? _processTime;

        //收支项目
        private Erp_ChargeItem _chargeItem;

        //收支项目id
        private string _chargeItemId;

        //收支项目名称
        private string _chargeItemName;

        //备注
        private string _remark;

        //账户
        private Erp_Account _account;

        //账户id
        private string _accountId;

        //应金额
        private decimal? _totalAmount;

        //实金额
        private decimal? _chargeAmount;

        //作废状态
        private int _isVoid;

        //创建人
        private User _createUser;

        //创建人id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_ChargeOrderId
        {
            get
            {
                return _erp_ChargeOrderId;
            }
            set
            {
                _erp_ChargeOrderId = value;
            }
        }

        //收支类型
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
                propInfoList.PutProperty<Erp_ChargeOrder, string>(a => a.Type);
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
                propInfoList.PutProperty<Erp_ChargeOrder, string>(a => a.Number);
            }
        }

        //供应商
        [ForeignKey("SuppilerId")]
        public virtual Erp_Suppiler Suppiler
        {
            get
            {
                return _suppiler;
            }
            set
            {
                _suppiler = value;
                referenceInfoList.PutProperty<Erp_ChargeOrder, Erp_Suppiler>(a => a.Suppiler);
            }
        }

        //工业商id
        [MaxLength(50)]
        [ForeignKey("Suppiler")]
        public string SuppilerId
        {
            get
            {
                return _suppilerId;
            }
            set
            {
                _suppilerId = value;
                propInfoList.PutProperty<Erp_ChargeOrder, string>(a => a.SuppilerId);
            }
        }

        //客户
        [ForeignKey("CustomerId")]
        public virtual Erp_Customer Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                referenceInfoList.PutProperty<Erp_ChargeOrder, Erp_Customer>(a => a.Customer);
            }
        }

        //客户id
        [MaxLength(50)]
        [ForeignKey("Customer")]
        public string CustomerId
        {
            get
            {
                return _customerId;
            }
            set
            {
                _customerId = value;
                propInfoList.PutProperty<Erp_ChargeOrder, string>(a => a.CustomerId);
            }
        }

        //处理人
        [ForeignKey("ProcessorId")]
        public virtual User Processor
        {
            get
            {
                return _processor;
            }
            set
            {
                _processor = value;
                referenceInfoList.PutProperty<Erp_ChargeOrder, User>(a => a.Processor);
            }
        }

        //处理人id
        [MaxLength(50)]
        [ForeignKey("Processor")]
        public string ProcessorId
        {
            get
            {
                return _processorId;
            }
            set
            {
                _processorId = value;
                propInfoList.PutProperty<Erp_ChargeOrder, string>(a => a.ProcessorId);
            }
        }

        //处理时间
        public DateTime? ProcessTime
        {
            get
            {
                return _processTime;
            }
            set
            {
                _processTime = value;
                propInfoList.PutProperty<Erp_ChargeOrder, DateTime?>(a => a.ProcessTime);
            }
        }

        //收支项目
        [ForeignKey("ChargeItemId")]
        public virtual Erp_ChargeItem ChargeItem
        {
            get
            {
                return _chargeItem;
            }
            set
            {
                _chargeItem = value;
                referenceInfoList.PutProperty<Erp_ChargeOrder, Erp_ChargeItem>(a => a.ChargeItem);
            }
        }

        //收支项目id
        [MaxLength(50)]
        public string ChargeItemId
        {
            get
            {
                return _chargeItemId;
            }
            set
            {
                _chargeItemId = value;
                propInfoList.PutProperty<Erp_ChargeOrder, string>(a => a.ChargeItemId);
            }
        }

        //收支项目名称
        [MaxLength(100)]
        public string ChargeItemName
        {
            get
            {
                return _chargeItemName;
            }
            set
            {
                _chargeItemName = value;
                propInfoList.PutProperty<Erp_ChargeOrder, string>(a => a.ChargeItemName);
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
                propInfoList.PutProperty<Erp_ChargeOrder, string>(a => a.Remark);
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
                referenceInfoList.PutProperty<Erp_ChargeOrder, Erp_Account>(a => a.Account);
            }
        }

        //账户id
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
                propInfoList.PutProperty<Erp_ChargeOrder, string>(a => a.AccountId);
            }
        }

        //应金额
        public decimal? TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
                propInfoList.PutProperty<Erp_ChargeOrder, decimal?>(a => a.TotalAmount);
            }
        }

        //实金额
        public decimal? ChargeAmount
        {
            get
            {
                return _chargeAmount;
            }
            set
            {
                _chargeAmount = value;
                propInfoList.PutProperty<Erp_ChargeOrder, decimal?>(a => a.ChargeAmount);
            }
        }

        //作废状态
        [Required]
        public int IsVoid
        {
            get
            {
                return _isVoid;
            }
            set
            {
                _isVoid = value;
                propInfoList.PutProperty<Erp_ChargeOrder, int>(a => a.IsVoid);
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
                referenceInfoList.PutProperty<Erp_ChargeOrder, User>(a => a.CreateUser);
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
                propInfoList.PutProperty<Erp_ChargeOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_ChargeOrder, DateTime>(a => a.CreateTime);
            }
        }


        public void Create()
        {
            Erp_ChargeOrderId = Guid.NewGuid().ToString();

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
