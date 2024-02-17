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
    public class Erp_InventoryFlow : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_InventoryFlow()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_InventoryFlowId;

        //类型
        private string _type;

        //仓库
        private Erp_Warehouse _warehouse;

        //仓库id
        private string _warehouseId;

        //产品
        private Erp_Goods _goods ;

        //产品id
        private string _goodsId;

        //变化前数量
        private int _quantityBefore;

        //变化数量
        private int _quantityChange;

        //变化后数量
        private int _quantityAfter;

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

        private Erp_StockInOrder _stockInOrder;

        private string _stockInOrderId;

        private Erp_StockInOrder _voidStockInOrder;

        private string _voidStockInOrderId;

        private Erp_StockOutOrder _stockOutOrder;

        private string _stockOutOrderId;

        private Erp_StockOutOrder _voidStockOutOrder;

        private string _voidStockOutOrderId;

        private Erp_StockTransferOrder _stockTransferOrder;

        private string _stockTransferOrderId;

        private Erp_StockTransferOrder _voidStockTransferOrder;

        private string _voidStockTransferOrderId;

        private Erp_StockCheckOrder _stockCheckOrder;

        private string _stockCheckOrderId;

        private Erp_StockCheckOrder _voidStockCheckOrder;

        private string _voidStockCheckOrderId;

        //创建人
        private User _createUser;

        //创建人id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_InventoryFlowId
        {
            get
            {
                return _erp_InventoryFlowId;
            }
            set
            {
                _erp_InventoryFlowId = value;
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.Type);
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
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_Warehouse>(a => a.Warehouse);
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.WarehouseId);
            }
        }

        //产品
        [ForeignKey("GoodsId")]
        public virtual Erp_Goods Goods 
        {
            get
            {
                return _goods ;
            }
            set
            {
                _goods  = value;
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_Goods>(a => a.Goods );
            }
        }

        //产品id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Goods")]
        public string GoodsId
        {
            get
            {
                return _goodsId;
            }
            set
            {
                _goodsId = value;
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.GoodsId);
            }
        }

        //变化前数量
        [Required]
        public int QuantityBefore
        {
            get
            {
                return _quantityBefore;
            }
            set
            {
                _quantityBefore = value;
                propInfoList.PutProperty<Erp_InventoryFlow, int>(a => a.QuantityBefore);
            }
        }

        //变化数量
        [Required]
        public int QuantityChange
        {
            get
            {
                return _quantityChange;
            }
            set
            {
                _quantityChange = value;
                propInfoList.PutProperty<Erp_InventoryFlow, int>(a => a.QuantityChange);
            }
        }

        //变化后数量
        [Required]
        public int QuantityAfter
        {
            get
            {
                return _quantityAfter;
            }
            set
            {
                _quantityAfter = value;
                propInfoList.PutProperty<Erp_InventoryFlow, int>(a => a.QuantityAfter);
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
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_PurchaseOrder>(a => a.PurchaseOrder);
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.PurchaseOrderId);
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
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_PurchaseOrder>(a => a.VoidPurchaseOrder);
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.VoidPurchaseOrderId);
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
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_PurchaseReturnOrder>(a => a.PurchaseReturnOrder);
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.PurchaseReturnOrderId);
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
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_PurchaseReturnOrder>(a => a.VoidPurchaseReturnOrder);
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.VoidPurchaseReturnOrderId);
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
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_SalesOrder>(a => a.SalesOrder);
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.SalesOrderId);
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
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_SalesOrder>(a => a.VoidSalesOrder);
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.VoidSalesOrderId);
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
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_SalesReturnOrder>(a => a.SalesReturnOrder);
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.SalesReturnOrderId);
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
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_SalesReturnOrder>(a => a.VoidSalesReturnOrder);
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.VoidSalesReturnOrderId);
            }
        }

        [ForeignKey("StockInOrderId")]
        public virtual Erp_StockInOrder StockInOrder
        {
            get
            {
                return _stockInOrder;
            }
            set
            {
                _stockInOrder = value;
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_StockInOrder>(a => a.StockInOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("StockInOrder")]
        public string StockInOrderId
        {
            get
            {
                return _stockInOrderId;
            }
            set
            {
                _stockInOrderId = value;
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.StockInOrderId);
            }
        }

        [ForeignKey("VoidStockInOrderId")]
        public virtual Erp_StockInOrder VoidStockInOrder
        {
            get
            {
                return _voidStockInOrder;
            }
            set
            {
                _voidStockInOrder = value;
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_StockInOrder>(a => a.VoidStockInOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidStockInOrder")]
        public string VoidStockInOrderId
        {
            get
            {
                return _voidStockInOrderId;
            }
            set
            {
                _voidStockInOrderId = value;
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.VoidStockInOrderId);
            }
        }

        [ForeignKey("StockOutOrderId")]
        public virtual Erp_StockOutOrder StockOutOrder
        {
            get
            {
                return _stockOutOrder;
            }
            set
            {
                _stockOutOrder = value;
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_StockOutOrder>(a => a.StockOutOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("StockOutOrder")]
        public string StockOutOrderId
        {
            get
            {
                return _stockOutOrderId;
            }
            set
            {
                _stockOutOrderId = value;
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.StockOutOrderId);
            }
        }

        [ForeignKey("VoidStockOutOrderId")]
        public virtual Erp_StockOutOrder VoidStockOutOrder
        {
            get
            {
                return _voidStockOutOrder;
            }
            set
            {
                _voidStockOutOrder = value;
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_StockOutOrder>(a => a.VoidStockOutOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidStockOutOrder")]
        public string VoidStockOutOrderId
        {
            get
            {
                return _voidStockOutOrderId;
            }
            set
            {
                _voidStockOutOrderId = value;
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.VoidStockOutOrderId);
            }
        }

        [ForeignKey("StockTransferOrderId")]
        public virtual Erp_StockTransferOrder StockTransferOrder
        {
            get
            {
                return _stockTransferOrder;
            }
            set
            {
                _stockTransferOrder = value;
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_StockTransferOrder>(a => a.StockTransferOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("StockTransferOrder")]
        public string StockTransferOrderId
        {
            get
            {
                return _stockTransferOrderId;
            }
            set
            {
                _stockTransferOrderId = value;
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.StockTransferOrderId);
            }
        }

        [ForeignKey("VoidStockTransferOrderId")]
        public virtual Erp_StockTransferOrder VoidStockTransferOrder
        {
            get
            {
                return _voidStockTransferOrder;
            }
            set
            {
                _voidStockTransferOrder = value;
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_StockTransferOrder>(a => a.VoidStockTransferOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidStockTransferOrder")]
        public string VoidStockTransferOrderId
        {
            get
            {
                return _voidStockTransferOrderId;
            }
            set
            {
                _voidStockTransferOrderId = value;
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.VoidStockTransferOrderId);
            }
        }

        [ForeignKey("StockCheckOrderId")]
        public virtual Erp_StockCheckOrder StockCheckOrder
        {
            get
            {
                return _stockCheckOrder;
            }
            set
            {
                _stockCheckOrder = value;
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_StockCheckOrder>(a => a.StockCheckOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("StockCheckOrder")]
        public string StockCheckOrderId
        {
            get
            {
                return _stockCheckOrderId;
            }
            set
            {
                _stockCheckOrderId = value;
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.StockCheckOrderId);
            }
        }

        [ForeignKey("VoidStockCheckOrderId")]
        public virtual Erp_StockCheckOrder VoidStockCheckOrder
        {
            get
            {
                return _voidStockCheckOrder;
            }
            set
            {
                _voidStockCheckOrder = value;
                referenceInfoList.PutProperty<Erp_InventoryFlow, Erp_StockCheckOrder>(a => a.VoidStockCheckOrder);
            }
        }

        [MaxLength(50)]
        [ForeignKey("VoidStockCheckOrder")]
        public string VoidStockCheckOrderId
        {
            get
            {
                return _voidStockCheckOrderId;
            }
            set
            {
                _voidStockCheckOrderId = value;
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.VoidStockCheckOrderId);
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
                referenceInfoList.PutProperty<Erp_InventoryFlow, User>(a => a.CreateUser);
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
                propInfoList.PutProperty<Erp_InventoryFlow, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_InventoryFlow, DateTime>(a => a.CreateTime);
            }
        }


        public void Create()
        {
            Erp_InventoryFlowId = Guid.NewGuid().ToString();
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
