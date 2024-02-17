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
    public class Erp_SalesReturnGoods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_SalesReturnGoods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_SalesReturnGoodsId;

        //销售退货订单
        private Erp_SalesReturnOrder _salesReturnOrder;

        //采购退货订单id
        private string _salesReturnOrderId;

        //商品
        private Erp_Goods _goods;

        //商品id
        private string _goodsId;

        ////销售产品
        //private Erp_SalesGoods _salesGoods;

        ////销售产品id
        //private string _salesGoodsId;

        //退货数量
        private int _returnQuantity;

        //退货单价
        private decimal _returnPrice;

        //总金额
        private decimal _totalAmount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_SalesReturnGoodsId
        {
            get
            {
                return _erp_SalesReturnGoodsId;
            }
            set
            {
                _erp_SalesReturnGoodsId = value;
            }
        }

        //销售退货订单
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
                referenceInfoList.PutProperty<Erp_SalesReturnGoods, Erp_SalesReturnOrder>(a => a.SalesReturnOrder);
            }
        }

        //采购退货订单id
        [Required]
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
                propInfoList.PutProperty<Erp_SalesReturnGoods, string>(a => a.SalesReturnOrderId);
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
                referenceInfoList.PutProperty<Erp_SalesReturnGoods, Erp_Goods>(a => a.Goods);
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
                propInfoList.PutProperty<Erp_SalesReturnGoods, string>(a => a.GoodsId);
            }
        }

        ////销售产品
        //[ForeignKey("SalesGoodsId")]
        //public virtual Erp_SalesGoods SalesGoods
        //{
        //    get
        //    {
        //        return _salesGoods;
        //    }
        //    set
        //    {
        //        _salesGoods = value;
        //        referenceInfoList.PutProperty<Erp_SalesReturnGoods, Erp_SalesGoods>(a => a.SalesGoods);
        //    }
        //}

        ////销售产品id
        //[Required]
        //[MaxLength(50)]
        //[ForeignKey("SalesGoods")]
        //public string SalesGoodsId
        //{
        //    get
        //    {
        //        return _salesGoodsId;
        //    }
        //    set
        //    {
        //        _salesGoodsId = value;
        //        propInfoList.PutProperty<Erp_SalesReturnGoods, string>(a => a.SalesGoodsId);
        //    }
        //}

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
                propInfoList.PutProperty<Erp_SalesReturnGoods, int>(a => a.ReturnQuantity);
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
                propInfoList.PutProperty<Erp_SalesReturnGoods, decimal>(a => a.ReturnPrice);
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
                propInfoList.PutProperty<Erp_SalesReturnGoods, decimal>(a => a.TotalAmount);
            }
        }


        public void Create()
        {
            Erp_SalesReturnGoodsId = Guid.NewGuid().ToString();
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
