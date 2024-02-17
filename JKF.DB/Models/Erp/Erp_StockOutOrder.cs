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
    public class Erp_StockOutOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockOutOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockOutOrderId;

        //出库类型
        private string _type;

        //编号
        private string _number;

        //仓库
        private Erp_Warehouse _warehouse;

        //仓库id
        private string _warehouseId;

        //销售单据
        private Erp_SalesOrder _salesOrder;

        //销售单据id
        private string _salesOrderId;

        //采购退货单据
        private Erp_PurchaseReturnOrder _purchaseReturnOrder;

        //退货单据id
        private string _purchaseReturnOrderId;

        //调拨单据
        private Erp_StockTransferOrder _stockTransferOrder;

        //调拨单据id
        private string _stockTransferOrderId;

        //出库总数
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
        public string Erp_StockOutOrderId
        {
            get
            {
                return _erp_StockOutOrderId;
            }
            set
            {
                _erp_StockOutOrderId = value;
            }
        }

        //出库类型
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
                propInfoList.PutProperty<Erp_StockOutOrder, string>(a => a.Type);
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
                propInfoList.PutProperty<Erp_StockOutOrder, string>(a => a.Number);
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
                referenceInfoList.PutProperty<Erp_StockOutOrder, Erp_Warehouse>(a => a.Warehouse);
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
                propInfoList.PutProperty<Erp_StockOutOrder, string>(a => a.WarehouseId);
            }
        }

        //销售单据
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
                referenceInfoList.PutProperty<Erp_StockOutOrder, Erp_SalesOrder>(a => a.SalesOrder);
            }
        }

        //销售单据id
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
                propInfoList.PutProperty<Erp_StockOutOrder, string>(a => a.SalesOrderId);
            }
        }

        //采购退货单据
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
                referenceInfoList.PutProperty<Erp_StockOutOrder, Erp_PurchaseReturnOrder>(a => a.PurchaseReturnOrder);
            }
        }

        //退货单据id
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
                propInfoList.PutProperty<Erp_StockOutOrder, string>(a => a.PurchaseReturnOrderId);
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
                referenceInfoList.PutProperty<Erp_StockOutOrder, Erp_StockTransferOrder>(a => a.StockTransferOrder);
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
                propInfoList.PutProperty<Erp_StockOutOrder, string>(a => a.StockTransferOrderId);
            }
        }

        //出库总数
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
                propInfoList.PutProperty<Erp_StockOutOrder, int>(a => a.TotalQuantity);
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
                propInfoList.PutProperty<Erp_StockOutOrder, int>(a => a.RemainQuantity);
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
                propInfoList.PutProperty<Erp_StockOutOrder, int>(a => a.Completed);
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
                propInfoList.PutProperty<Erp_StockOutOrder, int>(a => a.IsVoid);
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
                referenceInfoList.PutProperty<Erp_StockOutOrder, User>(a => a.CreateUser);
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
                propInfoList.PutProperty<Erp_StockOutOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_StockOutOrder, DateTime>(a => a.CreateTime);
            }
        }


        public void Create()
        {
            Erp_StockOutOrderId = Guid.NewGuid().ToString();
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
