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
    public class Erp_Inventory : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_Inventory()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_InventoryId;

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

        //库存总数
        private int _totalQuantity;

        //库存状态
        private int _stockStatus;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_InventoryId
        {
            get
            {
                return _erp_InventoryId;
            }
            set
            {
                _erp_InventoryId = value;
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
                referenceInfoList.PutProperty<Erp_Inventory, Erp_Warehouse>(a => a.Warehouse);
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
                propInfoList.PutProperty<Erp_Inventory, string>(a => a.WarehouseId);
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
                referenceInfoList.PutProperty<Erp_Inventory, Erp_Goods>(a => a.Goods);
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
                propInfoList.PutProperty<Erp_Inventory, string>(a => a.GoodsId);
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
                propInfoList.PutProperty<Erp_Inventory, int>(a => a.InitialQuantity);
            }
        }

        //库存总数
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
                propInfoList.PutProperty<Erp_Inventory, int>(a => a.TotalQuantity);
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
                propInfoList.PutProperty<Erp_Inventory, int>(a => a.StockStatus);
            }
        }


        public void Create()
        {
            Erp_InventoryId = Guid.NewGuid().ToString();
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
