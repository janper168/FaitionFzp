using JKF.DB.extension;
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
    public class Erp_Batch : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_Batch()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_BatchId;

        //库存
        private Erp_Inventory _inventory;

        //库存id
        private string _inventoryId;

        //仓库
        private Erp_Warehouse _warehouse;

        //仓库id
        private string _warehouseId;

        //商品
        private Erp_Goods _goods;

        //商品id
        private string _goodsId;

        //初始库存
        private int _initialQuantity;

        //批次数量
        private int _totalQuantity;

        //批次剩余数量
        private int _remainQuantity;

        //生产日期
        private DateTime? _productionDate;

        //保质期天数
        private int? _shelfLifeDays;

        //预警天数
        private DateTime? _wraningDate;

        //过期天数
        private DateTime? _expirationDate;

        //库存状态
        private int _stockStatus;

        //创建时间
        private DateTime _createTime;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_BatchId
        {
            get
            {
                return _erp_BatchId;
            }
            set
            {
                _erp_BatchId = value;
            }
        }

        //库存
        [ForeignKey("InventoryId")]
        public virtual Erp_Inventory Inventory
        {
            get
            {
                return _inventory;
            }
            set
            {
                _inventory = value;
                referenceInfoList.PutProperty<Erp_Batch, Erp_Inventory>(a => a.Inventory);
            }
        }

        //库存id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Inventory")]
        public string InventoryId
        {
            get
            {
                return _inventoryId;
            }
            set
            {
                _inventoryId = value;
                propInfoList.PutProperty<Erp_Batch, string>(a => a.InventoryId);
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
                referenceInfoList.PutProperty<Erp_Batch, Erp_Warehouse>(a => a.Warehouse);
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
                propInfoList.PutProperty<Erp_Batch, string>(a => a.WarehouseId);
            }
        }

        //商品
        [ForeignKey("GoodsId")]
        public virtual Erp_Goods Goods
        {
            get
            {
                return _goods;
            }
            set
            {
                _goods = value;
                referenceInfoList.PutProperty<Erp_Batch, Erp_Goods>(a => a.Goods);
            }
        }

        //商品id
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
                propInfoList.PutProperty<Erp_Batch, string>(a => a.GoodsId);
            }
        }

        //初始库存
        [Required]
        public int InitialQuantity
        {
            get
            {
                return _initialQuantity;
            }
            set
            {
                _initialQuantity = value;
                propInfoList.PutProperty<Erp_Batch, int>(a => a.InitialQuantity);
            }
        }

        //批次数量
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
                propInfoList.PutProperty<Erp_Batch, int>(a => a.TotalQuantity);
            }
        }

        //批次剩余数量
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
                propInfoList.PutProperty<Erp_Batch, int>(a => a.RemainQuantity);
            }
        }

        //生产日期
        public DateTime? ProductionDate
        {
            get
            {
                return _productionDate;
            }
            set
            {
                _productionDate = value;
                propInfoList.PutProperty<Erp_Batch, DateTime?>(a => a.ProductionDate);
            }
        }

        //保质期天数
        public int? ShelfLifeDays
        {
            get
            {
                return _shelfLifeDays;
            }
            set
            {
                _shelfLifeDays = value;
                propInfoList.PutProperty<Erp_Batch, int?>(a => a.ShelfLifeDays);
            }
        }

        //预警天数
        public DateTime? WraningDate
        {
            get
            {
                return _wraningDate;
            }
            set
            {
                _wraningDate = value;
                propInfoList.PutProperty<Erp_Batch, DateTime?>(a => a.WraningDate);
            }
        }

        //过期天数
        public DateTime? ExpirationDate
        {
            get
            {
                return _expirationDate;
            }
            set
            {
                _expirationDate = value;
                propInfoList.PutProperty<Erp_Batch, DateTime?>(a => a.ExpirationDate);
            }
        }

        //库存状态
        [Required]
        public int StockStatus
        {
            get
            {
                return _stockStatus;
            }
            set
            {
                _stockStatus = value;
                propInfoList.PutProperty<Erp_Batch, int>(a => a.StockStatus);
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
                propInfoList.PutProperty<Erp_Batch, DateTime>(a => a.CreateTime);
            }
        }


        public void Create()
        {
            //Erp_BatchId = Guid.NewGuid().ToString();
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
