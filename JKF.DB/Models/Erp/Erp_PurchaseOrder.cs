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
    public class Erp_PurchaseOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_PurchaseOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_PurchaseOrderId;

        //编号
        private string _number;

        //仓库
        private Erp_Warehouse _warehouse;

        //仓库id
        private string _warehouseId;

        //供应商
        private Erp_Suppiler _suppiler;

        //供应商id
        private string _suppilerId;

        //处理时间
        private DateTime _processTime;

        //备注
        private string _remark;

        //采购总数量
        private int? _totalQuantity;

        //其他费用
        private decimal _otherAmount;

        //采购总金额
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

        //商品列表
        private ICollection<Erp_PurchaseGoods> _purchaseGoodsList;

        //采购账户
        private Erp_PurchaseAccount _purchaseAccount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_PurchaseOrderId
        {
            get
            {
                return _erp_PurchaseOrderId;
            }
            set
            {
                _erp_PurchaseOrderId = value;
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
                propInfoList.PutProperty<Erp_PurchaseOrder, string>(a => a.Number);
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
                referenceInfoList.PutProperty<Erp_PurchaseOrder, Erp_Warehouse>(a => a.Warehouse);
            }
        }

        //仓库id
        [Required]
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
                propInfoList.PutProperty<Erp_PurchaseOrder, string>(a => a.WarehouseId);
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
                referenceInfoList.PutProperty<Erp_PurchaseOrder, Erp_Suppiler>(a => a.Suppiler);
            }
        }

        //供应商id
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
                propInfoList.PutProperty<Erp_PurchaseOrder, string>(a => a.SuppilerId);
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
                propInfoList.PutProperty<Erp_PurchaseOrder, DateTime>(a => a.ProcessTime);
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
                propInfoList.PutProperty<Erp_PurchaseOrder, string>(a => a.Remark);
            }
        }

        //采购总数量
        public int? TotalQuantity
        {
            get
            {
                return _totalQuantity;
            }
            set
            {
                _totalQuantity = value;
                propInfoList.PutProperty<Erp_PurchaseOrder, int?>(a => a.TotalQuantity);
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
                propInfoList.PutProperty<Erp_PurchaseOrder, decimal>(a => a.otherAmount);
            }
        }

        //采购总金额
        public decimal? TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
                propInfoList.PutProperty<Erp_PurchaseOrder, decimal?>(a => a.TotalAmount);
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
                propInfoList.PutProperty<Erp_PurchaseOrder, decimal?>(a => a.PaymentAmount);
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
                propInfoList.PutProperty<Erp_PurchaseOrder, decimal?>(a => a.ArearsAmount);
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
                propInfoList.PutProperty<Erp_PurchaseOrder, int>(a => a.IsVoid);
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
                propInfoList.PutProperty<Erp_PurchaseOrder, int>(a => a.EnableAutoStockIn);
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
                referenceInfoList.PutProperty<Erp_PurchaseOrder, User>(a => a.CreateUser);
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
                propInfoList.PutProperty<Erp_PurchaseOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_PurchaseOrder, DateTime>(a => a.CreateTime);
            }
        }

        //商品列表
        public virtual ICollection<Erp_PurchaseGoods> PurchaseGoodsList
        {
            get
            {
                return _purchaseGoodsList;
            }
            set
            {
                _purchaseGoodsList = value;
                referenceInfoList.PutProperty<Erp_PurchaseOrder, ICollection<Erp_PurchaseGoods>>(a => a.PurchaseGoodsList);
            }
        }

        //采购账户
        public virtual Erp_PurchaseAccount PurchaseAccount
        {
            get
            {
                return _purchaseAccount;
            }
            set
            {
                _purchaseAccount = value;
                referenceInfoList.PutProperty<Erp_PurchaseOrder, Erp_PurchaseAccount>(a => a.PurchaseAccount);
            }
        }


        public void Create()
        {
            Erp_PurchaseOrderId = Guid.NewGuid().ToString();
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
