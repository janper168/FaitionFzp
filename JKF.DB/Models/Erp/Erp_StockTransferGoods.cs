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
    public class Erp_StockTransferGoods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockTransferGoods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockTransferGoodsId;

        //调拨订单
        private Erp_StockTransferOrder _stockTransferOrder;

        //挑拨订单id
        private string _stockTransferOrderId;

        //商品
        private Erp_Goods _goods;

        //商品id
        private string _goodsId;

        //调拨数量
        private int _stockTransferQuantity;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockTransferGoodsId
        {
            get
            {
                return _erp_StockTransferGoodsId;
            }
            set
            {
                _erp_StockTransferGoodsId = value;
            }
        }

        //调拨订单
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
                referenceInfoList.PutProperty<Erp_StockTransferGoods, Erp_StockTransferOrder>(a => a.StockTransferOrder);
            }
        }

        //挑拨订单id
        [Required]
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
                propInfoList.PutProperty<Erp_StockTransferGoods, string>(a => a.StockTransferOrderId);
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
                referenceInfoList.PutProperty<Erp_StockTransferGoods, Erp_Goods>(a => a.Goods);
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
                propInfoList.PutProperty<Erp_StockTransferGoods, string>(a => a.GoodsId);
            }
        }

        //调拨数量
        [Required]
        public int StockTransferQuantity
        {
            get
            {
                return _stockTransferQuantity;
            }
            set
            {
                _stockTransferQuantity = value;
                propInfoList.PutProperty<Erp_StockTransferGoods, int>(a => a.StockTransferQuantity);
            }
        }


        public void Create()
        {
            Erp_StockTransferGoodsId = Guid.NewGuid().ToString();
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
