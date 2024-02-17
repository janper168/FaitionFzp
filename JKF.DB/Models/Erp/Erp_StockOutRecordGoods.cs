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
    public class Erp_StockOutRecordGoods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockOutRecordGoods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockOutRecordGoodsId;

        //出库产品
        private Erp_StockOutGoods _stockOutGoods;

        //出库产品id
        private string _stockOutGoodsId;

        //出库记录
        private Erp_StockOutRecord _stockOutecord;

        //出库记录id
        private string _stockOutRecordId;

        //产品
        private Erp_Goods _goods;

        //产品id
        private string _goodsId;

        //批次
        private Erp_Batch _batch;

        //批次id
        private string _batchId;

        //出库数量
        private int _stockOutQuantity;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockOutRecordGoodsId
        {
            get
            {
                return _erp_StockOutRecordGoodsId;
            }
            set
            {
                _erp_StockOutRecordGoodsId = value;
            }
        }

        //出库产品
        [ForeignKey("StockOutGoodsId")]
        public virtual Erp_StockOutGoods StockOutGoods
        {
            get
            {
                return _stockOutGoods;
            }
            set
            {
                _stockOutGoods = value;
                referenceInfoList.PutProperty<Erp_StockOutRecordGoods, Erp_StockOutGoods>(a => a.StockOutGoods);
            }
        }

        //出库产品id
        [Required]
        [MaxLength(50)]
        [ForeignKey("StockOutGoods")]
        public string StockOutGoodsId
        {
            get
            {
                return _stockOutGoodsId;
            }
            set
            {
                _stockOutGoodsId = value;
                propInfoList.PutProperty<Erp_StockOutRecordGoods, string>(a => a.StockOutGoodsId);
            }
        }

        //出库记录
        [ForeignKey("StockOutRecordId")]
        public virtual Erp_StockOutRecord StockOutecord
        {
            get
            {
                return _stockOutecord;
            }
            set
            {
                _stockOutecord = value;
                referenceInfoList.PutProperty<Erp_StockOutRecordGoods, Erp_StockOutRecord>(a => a.StockOutecord);
            }
        }

        //出库记录id
        [Required]
        [MaxLength(50)]
        [ForeignKey("StockOutRecord")]
        public string StockOutRecordId
        {
            get
            {
                return _stockOutRecordId;
            }
            set
            {
                _stockOutRecordId = value;
                propInfoList.PutProperty<Erp_StockOutRecordGoods, string>(a => a.StockOutRecordId);
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
                referenceInfoList.PutProperty<Erp_StockOutRecordGoods, Erp_Goods>(a => a.Goods);
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
                propInfoList.PutProperty<Erp_StockOutRecordGoods, string>(a => a.GoodsId);
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
                referenceInfoList.PutProperty<Erp_StockOutRecordGoods, Erp_Batch>(a => a.Batch);
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
                propInfoList.PutProperty<Erp_StockOutRecordGoods, string>(a => a.BatchId);
            }
        }

        //出库数量
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
                propInfoList.PutProperty<Erp_StockOutRecordGoods, int>(a => a.StockOutQuantity);
            }
        }


        public void Create()
        {
            Erp_StockOutRecordGoodsId = Guid.NewGuid().ToString();
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
