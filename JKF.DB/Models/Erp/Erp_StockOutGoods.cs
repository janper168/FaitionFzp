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
    public class Erp_StockOutGoods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockOutGoods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockOutGoodsId;

        //产品
        private Erp_Goods _goods;

        //产品id
        private string _goodsId;

        //出库单据
        private Erp_StockOutOrder _stockOutOrder;

        //出库单据id
        private string _stockOutOrderId;

        //出库库总数
        private int _stockOutQuantity;

        //剩余数量
        private int _remainQuantity;

        //完成状态
        private int _completed;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockOutGoodsId
        {
            get
            {
                return _erp_StockOutGoodsId;
            }
            set
            {
                _erp_StockOutGoodsId = value;
            }
        }

        //产品
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
                referenceInfoList.PutProperty<Erp_StockOutGoods, Erp_Goods>(a => a.Goods);
            }
        }

        //产品id
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
                propInfoList.PutProperty<Erp_StockOutGoods, string>(a => a.GoodsId);
            }
        }

        //出库单据
        [ForeignKey("StockOutOrderId")]
        public virtual Erp_StockOutOrder StockOutOrder
        {
            get
            {
                return _stockOutOrder;
            }
            set
            {
                _stockOutOrder = value;
                referenceInfoList.PutProperty<Erp_StockOutGoods, Erp_StockOutOrder>(a => a.StockOutOrder);
            }
        }

        //出库单据id
        [Required]
        [MaxLength(50)]
        [ForeignKey("StockOutOrder")]
        public string StockOutOrderId
        {
            get
            {
                return _stockOutOrderId;
            }
            set
            {
                _stockOutOrderId = value;
                propInfoList.PutProperty<Erp_StockOutGoods, string>(a => a.StockOutOrderId);
            }
        }

        //出库库总数
        [Required]
        public int StockOutQuantity
        {
            get
            {
                return _stockOutQuantity;
            }
            set
            {
                _stockOutQuantity = value;
                propInfoList.PutProperty<Erp_StockOutGoods, int>(a => a.StockOutQuantity);
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
                propInfoList.PutProperty<Erp_StockOutGoods, int>(a => a.RemainQuantity);
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
                propInfoList.PutProperty<Erp_StockOutGoods, int>(a => a.Completed);
            }
        }


        public void Create()
        {
            Erp_StockOutGoodsId = Guid.NewGuid().ToString();
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
