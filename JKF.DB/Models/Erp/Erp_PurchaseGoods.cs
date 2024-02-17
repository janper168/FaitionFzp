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
    public class Erp_PurchaseGoods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_PurchaseGoods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_PurchaseGoodsId;

        //采购订单
        private Erp_PurchaseOrder _purchaseOrder;

        //采购订单id
        private string _purchaseOrderId;

        //商品
        private Erp_Goods _goods;

        //商品id
        private string _goodsId;

        //采购数量
        private int _purchaseQuantity;

        //采购单价
        private decimal _purchasePrice;

        //总金额
        private decimal _totalAmount;

        //退货数量
        private decimal _returnQuantity;

        //欠款金额
        private decimal? _arearsAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_PurchaseGoodsId
        {
            get
            {
                return _erp_PurchaseGoodsId;
            }
            set
            {
                _erp_PurchaseGoodsId = value;
            }
        }

        //采购订单
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
                referenceInfoList.PutProperty<Erp_PurchaseGoods, Erp_PurchaseOrder>(a => a.PurchaseOrder);
            }
        }

        //采购订单id
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
                propInfoList.PutProperty<Erp_PurchaseGoods, string>(a => a.PurchaseOrderId);
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
                referenceInfoList.PutProperty<Erp_PurchaseGoods, Erp_Goods>(a => a.Goods);
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
                propInfoList.PutProperty<Erp_PurchaseGoods, string>(a => a.GoodsId);
            }
        }

        //采购数量
        [Required]
        public int PurchaseQuantity
        {
            get
            {
                return _purchaseQuantity;
            }
            set
            {
                _purchaseQuantity = value;
                propInfoList.PutProperty<Erp_PurchaseGoods, int>(a => a.PurchaseQuantity);
            }
        }

        //采购单价
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
                propInfoList.PutProperty<Erp_PurchaseGoods, decimal>(a => a.PurchasePrice);
            }
        }

        //总金额
        [Required]
        public decimal TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
                propInfoList.PutProperty<Erp_PurchaseGoods, decimal>(a => a.TotalAmount);
            }
        }

        //退货数量
        [Required]
        public decimal ReturnQuantity
        {
            get
            {
                return _returnQuantity;
            }
            set
            {
                _returnQuantity = value;
                propInfoList.PutProperty<Erp_PurchaseGoods, decimal>(a => a.ReturnQuantity);
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
                propInfoList.PutProperty<Erp_PurchaseGoods, decimal?>(a => a.ArearsAmount);
            }
        }


        public void Create()
        {
            Erp_PurchaseGoodsId = Guid.NewGuid().ToString();
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
