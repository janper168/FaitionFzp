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
    public class Erp_Goods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_Goods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_GoodsId;

        //编号
        private string _number;

        //名称
        private string _name;

        //条码
        private string _barCode;

        //产品分类
        private Erp_GoodsCategory _goodsCategory;

        //产品分类id
        private string _goodsCategoryId;

        //单位
        private string _unit;

        //规格
        private string _spec;

        //启用批次控制
        private int _enableBatchControl;

        //保质期天数
        private int? _shelfLifeDays;

        //保质期预警天数
        private int _shelfLifeWarningDays;

        //启用库存告警
        private int _enableInventoryWarning;

        //库存上限
        private int? _inventoryUpper;

        //库存下限
        private int? _inventoryLower;

        //采购价
        private decimal _retailPrice;

        //零售价
        private decimal _purchasePrice;

        //等级价一
        private decimal _levelPrice1;

        //等级价二
        private decimal _levelPrice2;

        //等级价三
        private decimal _levelPrice3;

        //备注
        private string _remark;

        //激活状态
        private int _actived;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_GoodsId
        {
            get
            {
                return _erp_GoodsId;
            }
            set
            {
                _erp_GoodsId = value;
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
                propInfoList.PutProperty<Erp_Goods, string>(a => a.Number);
            }
        }

        //名称
        [Required]
        [MaxLength(500)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                propInfoList.PutProperty<Erp_Goods, string>(a => a.Name);
            }
        }

        //条码
        [MaxLength(500)]
        public string BarCode
        {
            get
            {
                return _barCode;
            }
            set
            {
                _barCode = value;
                propInfoList.PutProperty<Erp_Goods, string>(a => a.BarCode);
            }
        }

        //产品分类
        [ForeignKey("GoodsCategoryId")]
        public virtual Erp_GoodsCategory GoodsCategory
        {
            get
            {
                return _goodsCategory;
            }
            set
            {
                _goodsCategory = value;
                referenceInfoList.PutProperty<Erp_Goods, Erp_GoodsCategory>(a => a.GoodsCategory);
            }
        }

        //产品分类id
        [MaxLength(50)]
        [ForeignKey("GoodCategory")]
        public string GoodsCategoryId
        {
            get
            {
                return _goodsCategoryId;
            }
            set
            {
                _goodsCategoryId = value;
                propInfoList.PutProperty<Erp_Goods, string>(a => a.GoodsCategoryId);
            }
        }

        //单位
        [MaxLength(50)]
        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
                propInfoList.PutProperty<Erp_Goods, string>(a => a.Unit);
            }
        }

        //规格
        [MaxLength(250)]
        public string Spec
        {
            get
            {
                return _spec;
            }
            set
            {
                _spec = value;
                propInfoList.PutProperty<Erp_Goods, string>(a => a.Spec);
            }
        }

        //启用批次控制
        [Required]
        public int EnableBatchControl
        {
            get
            {
                return _enableBatchControl;
            }
            set
            {
                _enableBatchControl = value;
                propInfoList.PutProperty<Erp_Goods, int>(a => a.EnableBatchControl);
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
                propInfoList.PutProperty<Erp_Goods, int?>(a => a.ShelfLifeDays);
            }
        }

        //保质期预警天数
        [Required]
        public int ShelfLifeWarningDays
        {
            get
            {
                return _shelfLifeWarningDays;
            }
            set
            {
                _shelfLifeWarningDays = value;
                propInfoList.PutProperty<Erp_Goods, int>(a => a.ShelfLifeWarningDays);
            }
        }

        //启用库存告警
        [Required]
        public int EnableInventoryWarning
        {
            get
            {
                return _enableInventoryWarning;
            }
            set
            {
                _enableInventoryWarning = value;
                propInfoList.PutProperty<Erp_Goods, int>(a => a.EnableInventoryWarning);
            }
        }

        //库存上限
        public int? InventoryUpper
        {
            get
            {
                return _inventoryUpper;
            }
            set
            {
                _inventoryUpper = value;
                propInfoList.PutProperty<Erp_Goods, int?>(a => a.InventoryUpper);
            }
        }

        //库存下限
        public int? InventoryLower
        {
            get
            {
                return _inventoryLower;
            }
            set
            {
                _inventoryLower = value;
                propInfoList.PutProperty<Erp_Goods, int?>(a => a.InventoryLower);
            }
        }

        //采购价
        [Required]
        public decimal RetailPrice
        {
            get
            {
                return _retailPrice;
            }
            set
            {
                _retailPrice = value;
                propInfoList.PutProperty<Erp_Goods, decimal>(a => a.RetailPrice);
            }
        }

        //零售价
        [Required]
        public decimal PurchasePrice
        {
            get
            {
                return _purchasePrice;
            }
            set
            {
                _purchasePrice = value;
                propInfoList.PutProperty<Erp_Goods, decimal>(a => a.PurchasePrice);
            }
        }

        //等级价一
        [Required]
        public decimal LevelPrice1
        {
            get
            {
                return _levelPrice1;
            }
            set
            {
                _levelPrice1 = value;
                propInfoList.PutProperty<Erp_Goods, decimal>(a => a.LevelPrice1);
            }
        }

        //等级价二
        [Required]
        public decimal LevelPrice2
        {
            get
            {
                return _levelPrice2;
            }
            set
            {
                _levelPrice2 = value;
                propInfoList.PutProperty<Erp_Goods, decimal>(a => a.LevelPrice2);
            }
        }

        //等级价三
        [Required]
        public decimal LevelPrice3
        {
            get
            {
                return _levelPrice3;
            }
            set
            {
                _levelPrice3 = value;
                propInfoList.PutProperty<Erp_Goods, decimal>(a => a.LevelPrice3);
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
                propInfoList.PutProperty<Erp_Goods, string>(a => a.Remark);
            }
        }

        //激活状态
        [Required]
        public int Actived
        {
            get
            {
                return _actived;
            }
            set
            {
                _actived = value;
                propInfoList.PutProperty<Erp_Goods, int>(a => a.Actived);
            }
        }


        public void Create()
        {
            Erp_GoodsId = Guid.NewGuid().ToString();
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
