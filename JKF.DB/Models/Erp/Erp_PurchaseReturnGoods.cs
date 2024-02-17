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
    public class Erp_PurchaseReturnGoods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_PurchaseReturnGoods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_PurchaseReturnGoodsId;

        //采购退货订单
        private Erp_PurchaseReturnOrder _purchaseReturnOrder;

        //采购退货订单id
        private string _purchaseReturnOrderId;

        //商品
        private Erp_Goods _goods;

        //商品id
        private string _goodsId;

        //退货数量
        private int _returnQuantity;

        //退货单价
        private decimal _returnPrice;

        //总金额
        private decimal _totalAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_PurchaseReturnGoodsId
        {
            get
            {
                return _erp_PurchaseReturnGoodsId;
            }
            set
            {
                _erp_PurchaseReturnGoodsId = value;
            }
        }

        //采购退货订单
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
                referenceInfoList.PutProperty<Erp_PurchaseReturnGoods, Erp_PurchaseReturnOrder>(a => a.PurchaseReturnOrder);
            }
        }

        //采购退货订单id
        [Required]
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
                propInfoList.PutProperty<Erp_PurchaseReturnGoods, string>(a => a.PurchaseReturnOrderId);
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
                referenceInfoList.PutProperty<Erp_PurchaseReturnGoods, Erp_Goods>(a => a.Goods);
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
                propInfoList.PutProperty<Erp_PurchaseReturnGoods, string>(a => a.GoodsId);
            }
        }

        //退货数量
        [Required]
        public int ReturnQuantity
        {
            get
            {
                return _returnQuantity;
            }
            set
            {
                _returnQuantity = value;
                propInfoList.PutProperty<Erp_PurchaseReturnGoods, int>(a => a.ReturnQuantity);
            }
        }

        //退货单价
        [Required]
        public decimal ReturnPrice
        {
            get
            {
                return _returnPrice;
            }
            set
            {
                _returnPrice = value;
                propInfoList.PutProperty<Erp_PurchaseReturnGoods, decimal>(a => a.ReturnPrice);
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
                propInfoList.PutProperty<Erp_PurchaseReturnGoods, decimal>(a => a.TotalAmount);
            }
        }


        public void Create()
        {
            Erp_PurchaseReturnGoodsId = Guid.NewGuid().ToString();
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
