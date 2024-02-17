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
    public class Erp_PaymentOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_PaymentOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_PaymentOrderId;

        //编号
        private string _number;

        //供应商
        private Erp_Suppiler _suppiler;

        //工业商id
        private string _suppilerId;

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

        private ICollection<Erp_PaymentAccount> _paymentAccountList;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_PaymentOrderId
        {
            get
            {
                return _erp_PaymentOrderId;
            }
            set
            {
                _erp_PaymentOrderId = value;
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
                propInfoList.PutProperty<Erp_PaymentOrder, string>(a => a.Number);
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
                referenceInfoList.PutProperty<Erp_PaymentOrder, Erp_Suppiler>(a => a.Suppiler);
            }
        }

        //工业商id
        [Required]
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
                propInfoList.PutProperty<Erp_PaymentOrder, string>(a => a.SuppilerId);
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
                referenceInfoList.PutProperty<Erp_PaymentOrder, User>(a => a.Processor);
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
                propInfoList.PutProperty<Erp_PaymentOrder, string>(a => a.ProcessorId);
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
                propInfoList.PutProperty<Erp_PaymentOrder, DateTime?>(a => a.ProcessTime);
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
                propInfoList.PutProperty<Erp_PaymentOrder, string>(a => a.Remark);
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
                propInfoList.PutProperty<Erp_PaymentOrder, decimal?>(a => a.TotalAmount);
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
                propInfoList.PutProperty<Erp_PaymentOrder, decimal>(a => a.DiscountAmount);
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
                propInfoList.PutProperty<Erp_PaymentOrder, int>(a => a.IsVoid);
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
                referenceInfoList.PutProperty<Erp_PaymentOrder, User>(a => a.CreateUser);
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
                propInfoList.PutProperty<Erp_PaymentOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_PaymentOrder, DateTime>(a => a.CreateTime);
            }
        }

        public virtual ICollection<Erp_PaymentAccount> PaymentAccountList
        {
            get
            {
                return _paymentAccountList;
            }
            set
            {
                _paymentAccountList = value;
                referenceInfoList.PutProperty<Erp_PaymentOrder, ICollection<Erp_PaymentAccount>>(a => a.PaymentAccountList);
            }
        }


        public void Create()
        {
            Erp_PaymentOrderId = Guid.NewGuid().ToString();
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
