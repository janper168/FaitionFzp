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
    public class Erp_SalesGoods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_SalesGoods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_SalesGoodsId;

        //销售订单
        private Erp_SalesOrder _salesOrder;

        //销售订单id
        private string _salesOrderId;

        //商品
        private Erp_Goods _goods;

        //商品id
        private string _goodsId;

        //采购数量
        private int _salesQuantity;

        //采购单价
        private decimal _salesPrice;

        //总金额
        private decimal _totalAmount;

        //退货数量
        private decimal _returnQuantity;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_SalesGoodsId
        {
            get
            {
                return _erp_SalesGoodsId;
            }
            set
            {
                _erp_SalesGoodsId = value;
            }
        }

        //销售订单
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
                referenceInfoList.PutProperty<Erp_SalesGoods, Erp_SalesOrder>(a => a.SalesOrder);
            }
        }

        //销售订单id
        [Required]
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
                propInfoList.PutProperty<Erp_SalesGoods, string>(a => a.SalesOrderId);
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
                referenceInfoList.PutProperty<Erp_SalesGoods, Erp_Goods>(a => a.Goods);
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
                propInfoList.PutProperty<Erp_SalesGoods, string>(a => a.GoodsId);
            }
        }

        //采购数量
        [Required]
        public int SalesQuantity
        {
            get
            {
                return _salesQuantity;
            }
            set
            {
                _salesQuantity = value;
                propInfoList.PutProperty<Erp_SalesGoods, int>(a => a.SalesQuantity);
            }
        }

        //采购单价
        [Required]
        public decimal SalesPrice
        {
            get
            {
                return _salesPrice;
            }
            set
            {
                _salesPrice = value;
                propInfoList.PutProperty<Erp_SalesGoods, decimal>(a => a.SalesPrice);
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
                propInfoList.PutProperty<Erp_SalesGoods, decimal>(a => a.TotalAmount);
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
                propInfoList.PutProperty<Erp_SalesGoods, decimal>(a => a.ReturnQuantity);
            }
        }


        public void Create()
        {
            Erp_SalesGoodsId = Guid.NewGuid().ToString();
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
