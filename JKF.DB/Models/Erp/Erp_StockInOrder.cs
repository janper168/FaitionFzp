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
    public class Erp_StockInOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockInOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockInOrderId;

        //入库类型
        private string _type;

        //编号
        private string _number;

        //仓库
        private Erp_Warehouse _warehouse;

        //仓库id
        private string _warehouseId;

        //采购单据
        private Erp_PurchaseOrder _purchaseOrder;

        //采购单据id
        private string _purchaseOrderId;

        //退货单据
        private Erp_SalesReturnOrder _salesReturnOrder;

        //退货单据id
        private string _salesReturnOrderId;

        //调拨单据
        private Erp_StockTransferOrder _stockTransferOrder;

        //调拨单据id
        private string _stockTransferOrderId;

        //入库总数
        private int _totalQuantity;

        //剩余数量
        private int _remainQuantity;

        //完成状态
        private int _completed;

        //作废状态
        private int _isVoid;

        //创建者
        private User _createUser;

        //创建者id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockInOrderId
        {
            get
            {
                return _erp_StockInOrderId;
            }
            set
            {
                _erp_StockInOrderId = value;
            }
        }

        //入库类型
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
                propInfoList.PutProperty<Erp_StockInOrder, string>(a => a.Type);
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
                propInfoList.PutProperty<Erp_StockInOrder, string>(a => a.Number);
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
                referenceInfoList.PutProperty<Erp_StockInOrder, Erp_Warehouse>(a => a.Warehouse);
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
                propInfoList.PutProperty<Erp_StockInOrder, string>(a => a.WarehouseId);
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
                referenceInfoList.PutProperty<Erp_StockInOrder, Erp_PurchaseOrder>(a => a.PurchaseOrder);
            }
        }

        //采购单据id
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
                propInfoList.PutProperty<Erp_StockInOrder, string>(a => a.PurchaseOrderId);
            }
        }

        //退货单据
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
                referenceInfoList.PutProperty<Erp_StockInOrder, Erp_SalesReturnOrder>(a => a.SalesReturnOrder);
            }
        }

        //退货单据id
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
                propInfoList.PutProperty<Erp_StockInOrder, string>(a => a.SalesReturnOrderId);
            }
        }

        //调拨单据
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
                referenceInfoList.PutProperty<Erp_StockInOrder, Erp_StockTransferOrder>(a => a.StockTransferOrder);
            }
        }

        //调拨单据id
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
                propInfoList.PutProperty<Erp_StockInOrder, string>(a => a.StockTransferOrderId);
            }
        }

        //入库总数
        [Required]
        public int TotalQuantity
        {
            get
            {
                return _totalQuantity;
            }
            set
            {
                _totalQuantity = value;
                propInfoList.PutProperty<Erp_StockInOrder, int>(a => a.TotalQuantity);
            }
        }

        //剩余数量
        [Required]
        public int RemainQuantity
        {
            get
            {
                return _remainQuantity;
            }
            set
            {
                _remainQuantity = value;
                propInfoList.PutProperty<Erp_StockInOrder, int>(a => a.RemainQuantity);
            }
        }

        //完成状态
        [Required]
        public int Completed
        {
            get
            {
                return _completed;
            }
            set
            {
                _completed = value;
                propInfoList.PutProperty<Erp_StockInOrder, int>(a => a.Completed);
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
                propInfoList.PutProperty<Erp_StockInOrder, int>(a => a.IsVoid);
            }
        }

        //创建者
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
                referenceInfoList.PutProperty<Erp_StockInOrder, User>(a => a.CreateUser);
            }
        }

        //创建者id
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
                propInfoList.PutProperty<Erp_StockInOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_StockInOrder, DateTime>(a => a.CreateTime);
            }
        }


        public void Create()
        {
            Erp_StockInOrderId = Guid.NewGuid().ToString();
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
