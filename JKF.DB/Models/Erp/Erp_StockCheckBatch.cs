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
    public class Erp_StockCheckBatch : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockCheckBatch()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockCheckBatchId;

        //盘点单据
        private Erp_StockCheckOrder _stockCheckOrder;

        //盘点单据id
        private string _stockCheckOrderId;

        //盘点商品
        private Erp_StockCheckGoods _stockCheckGoods;

        //盘点商品id
        private string _stockCheckGoodsId;

        //产品
        private Erp_Goods _goods;

        //产品id
        private string _goodsId;

        //批次编号
        private string _batchNumber;

        //状态
        private string _status;

        //账面数量
        private int _bookQuantity;

        //实际数量
        private int _actualQuantity;

        //盘盈数量
        private int _surplusQuantity;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockCheckBatchId
        {
            get
            {
                return _erp_StockCheckBatchId;
            }
            set
            {
                _erp_StockCheckBatchId = value;
            }
        }

        //盘点单据
        [ForeignKey("StockCheckOrderId")]
        public virtual Erp_StockCheckOrder StockCheckOrder
        {
            get
            {
                return _stockCheckOrder;
            }
            set
            {
                _stockCheckOrder = value;
                referenceInfoList.PutProperty<Erp_StockCheckBatch, Erp_StockCheckOrder>(a => a.StockCheckOrder);
            }
        }

        //盘点单据id
        [Required]
        [MaxLength(50)]
        [ForeignKey("StockCheckOrder")]
        public string StockCheckOrderId
        {
            get
            {
                return _stockCheckOrderId;
            }
            set
            {
                _stockCheckOrderId = value;
                propInfoList.PutProperty<Erp_StockCheckBatch, string>(a => a.StockCheckOrderId);
            }
        }

        //盘点商品
        [ForeignKey("StockCheckGoodsId")]
        public virtual Erp_StockCheckGoods StockCheckGoods
        {
            get
            {
                return _stockCheckGoods;
            }
            set
            {
                _stockCheckGoods = value;
                referenceInfoList.PutProperty<Erp_StockCheckBatch, Erp_StockCheckGoods>(a => a.StockCheckGoods);
            }
        }

        //盘点商品id
        [Required]
        [MaxLength(50)]
        [ForeignKey("StockCheckGoods")]
        public string StockCheckGoodsId
        {
            get
            {
                return _stockCheckGoodsId;
            }
            set
            {
                _stockCheckGoodsId = value;
                propInfoList.PutProperty<Erp_StockCheckBatch, string>(a => a.StockCheckGoodsId);
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
                referenceInfoList.PutProperty<Erp_StockCheckBatch, Erp_Goods>(a => a.Goods);
            }
        }

        //产品id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Good")]
        public string GoodsId
        {
            get
            {
                return _goodsId;
            }
            set
            {
                _goodsId = value;
                propInfoList.PutProperty<Erp_StockCheckBatch, string>(a => a.GoodsId);
            }
        }

        //批次编号
        [Required]
        [MaxLength(50)]
        public string BatchNumber
        {
            get
            {
                return _batchNumber;
            }
            set
            {
                _batchNumber = value;
                propInfoList.PutProperty<Erp_StockCheckBatch, string>(a => a.BatchNumber);
            }
        }

        //状态
        [Required]
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                propInfoList.PutProperty<Erp_StockCheckBatch, string>(a => a.Status);
            }
        }

        //账面数量
        [Required]
        public int BookQuantity
        {
            get
            {
                return _bookQuantity;
            }
            set
            {
                _bookQuantity = value;
                propInfoList.PutProperty<Erp_StockCheckBatch, int>(a => a.BookQuantity);
            }
        }

        //实际数量
        [Required]
        public int ActualQuantity
        {
            get
            {
                return _actualQuantity;
            }
            set
            {
                _actualQuantity = value;
                propInfoList.PutProperty<Erp_StockCheckBatch, int>(a => a.ActualQuantity);
            }
        }

        //盘盈数量
        [Required]
        public int SurplusQuantity
        {
            get
            {
                return _surplusQuantity;
            }
            set
            {
                _surplusQuantity = value;
                propInfoList.PutProperty<Erp_StockCheckBatch, int>(a => a.SurplusQuantity);
            }
        }


        public void Create()
        {
            Erp_StockCheckBatchId = Guid.NewGuid().ToString();
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
