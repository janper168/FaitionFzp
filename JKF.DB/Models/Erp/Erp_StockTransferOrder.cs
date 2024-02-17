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
    public class Erp_StockTransferOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockTransferOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockTransferOrderId;

        //单号
        private string _number;

        //处理时间
        private DateTime _processTime;

        //出库仓库
        private Erp_Warehouse _outWarehouse;

        //出库仓库Id
        private string _outWarehouseId;

        //入库仓库
        private Erp_Warehouse _inWarehouse;

        //入库仓库id
        private string _inWarehouseId;

        //备注
        private string _remark;

        //退货总数量
        private int? _totalQuantity;

        //作废状态
        private int _isVoid;

        //启用自动入库
        private int _enableAutoStockIn;

        //启用自动出库
        private int _enableAutoStockOut;

        //创建人
        private User _createUser;

        //创建人id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;

        private ICollection<Erp_StockTransferGoods> _stockTransferGoodsList;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockTransferOrderId
        {
            get
            {
                return _erp_StockTransferOrderId;
            }
            set
            {
                _erp_StockTransferOrderId = value;
            }
        }

        //单号
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
                propInfoList.PutProperty<Erp_StockTransferOrder, string>(a => a.Number);
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
                propInfoList.PutProperty<Erp_StockTransferOrder, DateTime>(a => a.ProcessTime);
            }
        }

        //出库仓库
        [ForeignKey("OutWarehouseId")]
        public virtual Erp_Warehouse OutWarehouse
        {
            get
            {
                return _outWarehouse;
            }
            set
            {
                _outWarehouse = value;
                referenceInfoList.PutProperty<Erp_StockTransferOrder, Erp_Warehouse>(a => a.OutWarehouse);
            }
        }

        //出库仓库Id
        [Required]
        [ForeignKey("OutWarehouse")]
        public string OutWarehouseId
        {
            get
            {
                return _outWarehouseId;
            }
            set
            {
                _outWarehouseId = value;
                propInfoList.PutProperty<Erp_StockTransferOrder, string>(a => a.OutWarehouseId);
            }
        }

        //入库仓库
        [ForeignKey("InWarehouseId")]
        public virtual Erp_Warehouse InWarehouse
        {
            get
            {
                return _inWarehouse;
            }
            set
            {
                _inWarehouse = value;
                referenceInfoList.PutProperty<Erp_StockTransferOrder, Erp_Warehouse>(a => a.InWarehouse);
            }
        }

        //入库仓库id
        [Required]
        [MaxLength(50)]
        [ForeignKey("InWarehouse")]
        public string InWarehouseId
        {
            get
            {
                return _inWarehouseId;
            }
            set
            {
                _inWarehouseId = value;
                propInfoList.PutProperty<Erp_StockTransferOrder, string>(a => a.InWarehouseId);
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
                propInfoList.PutProperty<Erp_StockTransferOrder, string>(a => a.Remark);
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
                propInfoList.PutProperty<Erp_StockTransferOrder, int?>(a => a.TotalQuantity);
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
                propInfoList.PutProperty<Erp_StockTransferOrder, int>(a => a.IsVoid);
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
                propInfoList.PutProperty<Erp_StockTransferOrder, int>(a => a.EnableAutoStockIn);
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
                propInfoList.PutProperty<Erp_StockTransferOrder, int>(a => a.EnableAutoStockOut);
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
                referenceInfoList.PutProperty<Erp_StockTransferOrder, User>(a => a.CreateUser);
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
                propInfoList.PutProperty<Erp_StockTransferOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_StockTransferOrder, DateTime>(a => a.CreateTime);
            }
        }

        public virtual ICollection<Erp_StockTransferGoods> StockTransferGoodsList
        {
            get
            {
                return _stockTransferGoodsList;
            }
            set
            {
                _stockTransferGoodsList = value;
                referenceInfoList.PutProperty<Erp_StockTransferOrder, ICollection<Erp_StockTransferGoods>>(a => a.StockTransferGoodsList);
            }
        }


        public void Create()
        {
            Erp_StockTransferOrderId = Guid.NewGuid().ToString();
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
