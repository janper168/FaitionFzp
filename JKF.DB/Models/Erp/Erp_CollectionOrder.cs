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
    public class Erp_CollectionOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_CollectionOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_CollectionOrderId;

        //编号
        private string _number;

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

        //备注
        private string _remark;

        //总金额
        private decimal? _totalAmount;

        //优惠金额
        private decimal _discountAmount;

        //作废状态
        private int _isVoid;

        //创建人
        private User _createUser;

        //创建人id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;

        private ICollection<Erp_CollectionAccount> _collectionAccountList;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_CollectionOrderId
        {
            get
            {
                return _erp_CollectionOrderId;
            }
            set
            {
                _erp_CollectionOrderId = value;
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
                propInfoList.PutProperty<Erp_CollectionOrder, string>(a => a.Number);
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
                referenceInfoList.PutProperty<Erp_CollectionOrder, Erp_Customer>(a => a.Customer);
            }
        }

        //客户id
        [Required]
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
                propInfoList.PutProperty<Erp_CollectionOrder, string>(a => a.CustomerId);
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
                referenceInfoList.PutProperty<Erp_CollectionOrder, User>(a => a.Processor);
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
                propInfoList.PutProperty<Erp_CollectionOrder, string>(a => a.ProcessorId);
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
                propInfoList.PutProperty<Erp_CollectionOrder, DateTime?>(a => a.ProcessTime);
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
                propInfoList.PutProperty<Erp_CollectionOrder, string>(a => a.Remark);
            }
        }

        //总金额
        public decimal? TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
                propInfoList.PutProperty<Erp_CollectionOrder, decimal?>(a => a.TotalAmount);
            }
        }

        //优惠金额
        [Required]
        public decimal DiscountAmount
        {
            get
            {
                return _discountAmount;
            }
            set
            {
                _discountAmount = value;
                propInfoList.PutProperty<Erp_CollectionOrder, decimal>(a => a.DiscountAmount);
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
                propInfoList.PutProperty<Erp_CollectionOrder, int>(a => a.IsVoid);
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
                referenceInfoList.PutProperty<Erp_CollectionOrder, User>(a => a.CreateUser);
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
                propInfoList.PutProperty<Erp_CollectionOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_CollectionOrder, DateTime>(a => a.CreateTime);
            }
        }

        public virtual ICollection<Erp_CollectionAccount> CollectionAccountList
        {
            get
            {
                return _collectionAccountList;
            }
            set
            {
                _collectionAccountList = value;
                referenceInfoList.PutProperty<Erp_CollectionOrder, ICollection<Erp_CollectionAccount>>(a => a.CollectionAccountList);
            }
        }


        public void Create()
        {
            Erp_CollectionOrderId = Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
            CreateUserId = new OperatorProvider().GetCurrent().UserId;
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
