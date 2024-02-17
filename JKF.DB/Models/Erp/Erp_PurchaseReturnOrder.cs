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
    public class Erp_PurchaseReturnOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_PurchaseReturnOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_PurchaseReturnOrderId;

        //编号
        private string _number;

        //采购单据
        private Erp_PurchaseOrder _purchaseOrder;

        //采购单据Id
        private string _purchaseOrderId;

        //供应商
        private Erp_Suppiler _suppiler;

        //供应商id
        private string _suppilerId;

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
        private decimal? _collectionAmount;

        //欠款金额
        private decimal? _arearsAmount;

        //作废状态
        private int _isVoid;

        //启用自动出库
        private int _enableAutoStockOut;

        //创建人
        private User _createUser;

        //创建人id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;

        //明细
        private ICollection<Erp_PurchaseReturnGoods> _purchaseReturnGoodsList;

        //账户
        private Erp_PurchaseReturnAccount _purchaseReturnAccount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_PurchaseReturnOrderId
        {
            get
            {
                return _erp_PurchaseReturnOrderId;
            }
            set
            {
                _erp_PurchaseReturnOrderId = value;
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, string>(a => a.Number);
            }
        }

        //采购单据
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
                referenceInfoList.PutProperty<Erp_PurchaseReturnOrder, Erp_PurchaseOrder>(a => a.PurchaseOrder);
            }
        }

        //采购单据Id
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, string>(a => a.PurchaseOrderId);
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
                referenceInfoList.PutProperty<Erp_PurchaseReturnOrder, Erp_Suppiler>(a => a.Suppiler);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, string>(a => a.SuppilerId);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, DateTime>(a => a.ProcessTime);
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
                referenceInfoList.PutProperty<Erp_PurchaseReturnOrder, Erp_Warehouse>(a => a.Warehouse);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, string>(a => a.WarehouseId);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, string>(a => a.Remark);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, int?>(a => a.TotalQuantity);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, decimal>(a => a.otherAmount);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, decimal?>(a => a.TotalAmount);
            }
        }

        //收款金额
        public decimal? CollectionAmount
        {
            get
            {
                return _collectionAmount;
            }
            set
            {
                _collectionAmount = value;
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, decimal?>(a => a.CollectionAmount);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, decimal?>(a => a.ArearsAmount);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, int>(a => a.IsVoid);
            }
        }

        //启用自动出库
        [Required]
        public int EnableAutoStockOut
        {
            get
            {
                return _enableAutoStockOut;
            }
            set
            {
                _enableAutoStockOut = value;
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, int>(a => a.EnableAutoStockOut);
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
                referenceInfoList.PutProperty<Erp_PurchaseReturnOrder, User>(a => a.CreateUser);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_PurchaseReturnOrder, DateTime>(a => a.CreateTime);
            }
        }

        //明细
        public virtual ICollection<Erp_PurchaseReturnGoods> PurchaseReturnGoodsList
        {
            get
            {
                return _purchaseReturnGoodsList;
            }
            set
            {
                _purchaseReturnGoodsList = value;
                referenceInfoList.PutProperty<Erp_PurchaseReturnOrder, ICollection<Erp_PurchaseReturnGoods>>(a => a.PurchaseReturnGoodsList);
            }
        }

        //账户
        public virtual Erp_PurchaseReturnAccount PurchaseReturnAccount
        {
            get
            {
                return _purchaseReturnAccount;
            }
            set
            {
                _purchaseReturnAccount = value;
                referenceInfoList.PutProperty<Erp_PurchaseReturnOrder, Erp_PurchaseReturnAccount>(a => a.PurchaseReturnAccount);
            }
        }


        public void Create()
        {
            Erp_PurchaseReturnOrderId = Guid.NewGuid().ToString();
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
