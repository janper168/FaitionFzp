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
    public class Erp_StockInGoods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockInGoods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockInGoodsId;

        //产品
        private Erp_Goods _goods;

        //产品id
        private string _goodsId;

        //采购单据
        private Erp_StockInOrder _stockInOrder;

        //采购单据id
        private string _stockInOrderId;

        //入库总数
        private int _stockInQuantity;

        //剩余数量
        private int _remainQuantity;

        //完成状态
        private int _completed;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockInGoodsId
        {
            get
            {
                return _erp_StockInGoodsId;
            }
            set
            {
                _erp_StockInGoodsId = value;
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
                referenceInfoList.PutProperty<Erp_StockInGoods, Erp_Goods>(a => a.Goods);
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
                propInfoList.PutProperty<Erp_StockInGoods, string>(a => a.GoodsId);
            }
        }

        //采购单据
        [ForeignKey("StockInOrderId")]
        public virtual Erp_StockInOrder StockInOrder
        {
            get
            {
                return _stockInOrder;
            }
            set
            {
                _stockInOrder = value;
                referenceInfoList.PutProperty<Erp_StockInGoods, Erp_StockInOrder>(a => a.StockInOrder);
            }
        }

        //采购单据id
        [Required]
        [MaxLength(50)]
        [ForeignKey("StockInOrder")]
        public string StockInOrderId
        {
            get
            {
                return _stockInOrderId;
            }
            set
            {
                _stockInOrderId = value;
                propInfoList.PutProperty<Erp_StockInGoods, string>(a => a.StockInOrderId);
            }
        }

        //入库总数
        [Required]
        public int StockInQuantity
        {
            get
            {
                return _stockInQuantity;
            }
            set
            {
                _stockInQuantity = value;
                propInfoList.PutProperty<Erp_StockInGoods, int>(a => a.StockInQuantity);
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
                propInfoList.PutProperty<Erp_StockInGoods, int>(a => a.RemainQuantity);
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
                propInfoList.PutProperty<Erp_StockInGoods, int>(a => a.Completed);
            }
        }


        public void Create()
        {
            Erp_StockInGoodsId = Guid.NewGuid().ToString();
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
