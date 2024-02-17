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
    public class Erp_StockInRecordGoods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockInRecordGoods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockInRecordGoodsId;

        //入库产品
        private Erp_StockInGoods _stockInGoods;

        //入库产品id
        private string _stockInGoodsId;

        //入库记录
        private Erp_StockInRecord _stockInRecord;

        //入库记录id
        private string _stockInRecordId;

        //产品
        private Erp_Goods _goods;

        //产品id
        private string _goodsId;

        //批次
        private Erp_Batch _batch;

        //批次id
        private string _batchId;

        //入库数量
        private int _stockInQuantity;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockInRecordGoodsId
        {
            get
            {
                return _erp_StockInRecordGoodsId;
            }
            set
            {
                _erp_StockInRecordGoodsId = value;
            }
        }

        //入库产品
        [ForeignKey("StockInGoodsId")]
        public virtual Erp_StockInGoods StockInGoods
        {
            get
            {
                return _stockInGoods;
            }
            set
            {
                _stockInGoods = value;
                referenceInfoList.PutProperty<Erp_StockInRecordGoods, Erp_StockInGoods>(a => a.StockInGoods);
            }
        }

        //入库产品id
        [Required]
        [MaxLength(50)]
        [ForeignKey("StockInGoods")]
        public string StockInGoodsId
        {
            get
            {
                return _stockInGoodsId;
            }
            set
            {
                _stockInGoodsId = value;
                propInfoList.PutProperty<Erp_StockInRecordGoods, string>(a => a.StockInGoodsId);
            }
        }

        //入库记录
        [ForeignKey("StockInRecordId")]
        public virtual Erp_StockInRecord StockInRecord
        {
            get
            {
                return _stockInRecord;
            }
            set
            {
                _stockInRecord = value;
                referenceInfoList.PutProperty<Erp_StockInRecordGoods, Erp_StockInRecord>(a => a.StockInRecord);
            }
        }

        //入库记录id
        [Required]
        [MaxLength(50)]
        [ForeignKey("StockInRecord")]
        public string StockInRecordId
        {
            get
            {
                return _stockInRecordId;
            }
            set
            {
                _stockInRecordId = value;
                propInfoList.PutProperty<Erp_StockInRecordGoods, string>(a => a.StockInRecordId);
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
                referenceInfoList.PutProperty<Erp_StockInRecordGoods, Erp_Goods>(a => a.Goods);
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
                propInfoList.PutProperty<Erp_StockInRecordGoods, string>(a => a.GoodsId);
            }
        }

        //批次
        [ForeignKey("BatchId")]
        public virtual Erp_Batch Batch
        {
            get
            {
                return _batch;
            }
            set
            {
                _batch = value;
                referenceInfoList.PutProperty<Erp_StockInRecordGoods, Erp_Batch>(a => a.Batch);
            }
        }

        //批次id
        [MaxLength(50)]
        [ForeignKey("Batch")]
        public string BatchId
        {
            get
            {
                return _batchId;
            }
            set
            {
                _batchId = value;
                propInfoList.PutProperty<Erp_StockInRecordGoods, string>(a => a.BatchId);
            }
        }

        //入库数量
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
                propInfoList.PutProperty<Erp_StockInRecordGoods, int>(a => a.StockInQuantity);
            }
        }


        public void Create()
        {
            Erp_StockInRecordGoodsId = Guid.NewGuid().ToString();
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
