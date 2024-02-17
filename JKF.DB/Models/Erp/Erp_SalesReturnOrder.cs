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
    public class Erp_SalesReturnOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_SalesReturnOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_SalesReturnOrderId;

        //编号
        private string _number;

        //采购单据
        private Erp_SalesOrder _salesOrder;

        //采购单据Id
        private string _salesOrderId;

        //客户
        private Erp_Customer _customer;

        //客户id
        private string _customerId;

        //处理时间
        private DateTime _processTime;

        //仓库
        private Erp_Warehouse _warehouse;

        //仓库id
        private string _warehouseId;

        //备注
        private string _remark;

        //退货总数量
        private int? _totalQuantity;

        //其他费用
        private decimal _otherAmount;

        //退货总金额
        private decimal? _totalAmount;

        //付款金额
        private decimal? _paymentAmount;

        //欠款金额
        private decimal? _arearsAmount;

        //作废状态
        private int _isVoid;

        //启用自动入库
        private int _enableAutoStockIn;

        //创建人
        private User _createUser;

        //创建人id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;

        private ICollection<Erp_SalesReturnGoods> _salesReturnGoodsList;

        private Erp_SalesReturnAccount _salesReturnAccount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_SalesReturnOrderId
        {
            get
            {
                return _erp_SalesReturnOrderId;
            }
            set
            {
                _erp_SalesReturnOrderId = value;
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
                propInfoList.PutProperty<Erp_SalesReturnOrder, string>(a => a.Number);
            }
        }

        //采购单据
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
                referenceInfoList.PutProperty<Erp_SalesReturnOrder, Erp_SalesOrder>(a => a.SalesOrder);
            }
        }

        //采购单据Id
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
                propInfoList.PutProperty<Erp_SalesReturnOrder, string>(a => a.SalesOrderId);
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
                referenceInfoList.PutProperty<Erp_SalesReturnOrder, Erp_Customer>(a => a.Customer);
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
                propInfoList.PutProperty<Erp_SalesReturnOrder, string>(a => a.CustomerId);
            }
        }

        //处理时间
        [Required]
        public DateTime ProcessTime
        {
            get
            {
                return _processTime;
            }
            set
            {
                _processTime = value;
                propInfoList.PutProperty<Erp_SalesReturnOrder, DateTime>(a => a.ProcessTime);
            }
        }

        //仓库
        [ForeignKey("WarehouseId")]
        public virtual Erp_Warehouse Warehouse
        {
            get
            {
                return _warehouse;
            }
            set
            {
                _warehouse = value;
                referenceInfoList.PutProperty<Erp_SalesReturnOrder, Erp_Warehouse>(a => a.Warehouse);
            }
        }

        //仓库id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Warehouse")]
        public string WarehouseId
        {
            get
            {
                return _warehouseId;
            }
            set
            {
                _warehouseId = value;
                propInfoList.PutProperty<Erp_SalesReturnOrder, string>(a => a.WarehouseId);
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
                propInfoList.PutProperty<Erp_SalesReturnOrder, string>(a => a.Remark);
            }
        }

        //退货总数量
        public int? TotalQuantity
        {
            get
            {
                return _totalQuantity;
            }
            set
            {
                _totalQuantity = value;
                propInfoList.PutProperty<Erp_SalesReturnOrder, int?>(a => a.TotalQuantity);
            }
        }

        //其他费用
        [Required]
        public decimal otherAmount
        {
            get
            {
                return _otherAmount;
            }
            set
            {
                _otherAmount = value;
                propInfoList.PutProperty<Erp_SalesReturnOrder, decimal>(a => a.otherAmount);
            }
        }

        //退货总金额
        public decimal? TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
                propInfoList.PutProperty<Erp_SalesReturnOrder, decimal?>(a => a.TotalAmount);
            }
        }

        //付款金额
        public decimal? PaymentAmount
        {
            get
            {
                return _paymentAmount;
            }
            set
            {
                _paymentAmount = value;
                propInfoList.PutProperty<Erp_SalesReturnOrder, decimal?>(a => a.PaymentAmount);
            }
        }

        //欠款金额
        public decimal? ArearsAmount
        {
            get
            {
                return _arearsAmount;
            }
            set
            {
                _arearsAmount = value;
                propInfoList.PutProperty<Erp_SalesReturnOrder, decimal?>(a => a.ArearsAmount);
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
                propInfoList.PutProperty<Erp_SalesReturnOrder, int>(a => a.IsVoid);
            }
        }

        //启用自动入库
        [Required]
        public int EnableAutoStockIn
        {
            get
            {
                return _enableAutoStockIn;
            }
            set
            {
                _enableAutoStockIn = value;
                propInfoList.PutProperty<Erp_SalesReturnOrder, int>(a => a.EnableAutoStockIn);
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
                referenceInfoList.PutProperty<Erp_SalesReturnOrder, User>(a => a.CreateUser);
            }
        }

        //创建人id
        [Required]
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
                propInfoList.PutProperty<Erp_SalesReturnOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_SalesReturnOrder, DateTime>(a => a.CreateTime);
            }
        }

        public virtual ICollection<Erp_SalesReturnGoods> SalesReturnGoodsList
        {
            get
            {
                return _salesReturnGoodsList;
            }
            set
            {
                _salesReturnGoodsList = value;
                referenceInfoList.PutProperty<Erp_SalesReturnOrder, ICollection<Erp_SalesReturnGoods>>(a => a.SalesReturnGoodsList);
            }
        }

        public virtual Erp_SalesReturnAccount SalesReturnAccount
        {
            get
            {
                return _salesReturnAccount;
            }
            set
            {
                _salesReturnAccount = value;
                referenceInfoList.PutProperty<Erp_SalesReturnOrder, Erp_SalesReturnAccount>(a => a.SalesReturnAccount);
            }
        }


        public void Create()
        {
            Erp_SalesReturnOrderId = Guid.NewGuid().ToString();
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
