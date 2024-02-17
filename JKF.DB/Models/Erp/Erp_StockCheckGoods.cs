using JKF.DB.extension;
using JKF.Utils;
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
    public class Erp_StockCheckGoods : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockCheckGoods()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockCheckGoodsId;

        //盘点单据
        private Erp_StockCheckOrder _stockCheckOrder;

        //盘点单据id
        private string _stockCheckOrderId;

        //产品
        private Erp_Goods _goods;

        //产品id
        private string _goodsId;

        //状态
        private string _status;

        //账面数量
        private int _bookQuantity;

        //实际数量
        private int _actualQuantity;

        //采购单价
        private decimal _purchasePrice;

        //盘盈数量
        private int _surplusQuantity;

        //盘盈金额
        private decimal _surplusAmount;

        //作废状态
        private int _isVoid;

        //创建者
        private User _createUser;

        //创建者id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockCheckGoodsId
        {
            get
            {
                return _erp_StockCheckGoodsId;
            }
            set
            {
                _erp_StockCheckGoodsId = value;
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
                referenceInfoList.PutProperty<Erp_StockCheckGoods, Erp_StockCheckOrder>(a => a.StockCheckOrder);
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
                propInfoList.PutProperty<Erp_StockCheckGoods, string>(a => a.StockCheckOrderId);
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
                referenceInfoList.PutProperty<Erp_StockCheckGoods, Erp_Goods>(a => a.Goods);
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
                propInfoList.PutProperty<Erp_StockCheckGoods, string>(a => a.GoodsId);
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
                propInfoList.PutProperty<Erp_StockCheckGoods, string>(a => a.Status);
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
                propInfoList.PutProperty<Erp_StockCheckGoods, int>(a => a.BookQuantity);
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
                propInfoList.PutProperty<Erp_StockCheckGoods, int>(a => a.ActualQuantity);
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
                propInfoList.PutProperty<Erp_StockCheckGoods, decimal>(a => a.PurchasePrice);
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
                propInfoList.PutProperty<Erp_StockCheckGoods, int>(a => a.SurplusQuantity);
            }
        }

        //盘盈金额
        [Required]
        public decimal SurplusAmount
        {
            get
            {
                return _surplusAmount;
            }
            set
            {
                _surplusAmount = value;
                propInfoList.PutProperty<Erp_StockCheckGoods, decimal>(a => a.SurplusAmount);
            }
        }

        //作废状态
        [Required]
        public int IsVoid
        {
            get
            {
                return _isVoid;
            }
            set
            {
                _isVoid = value;
                propInfoList.PutProperty<Erp_StockCheckGoods, int>(a => a.IsVoid);
            }
        }

        //创建者
        [ForeignKey("CreateUserId")]
        public virtual User CreateUser
        {
            get
            {
                return _createUser;
            }
            set
            {
                _createUser = value;
                referenceInfoList.PutProperty<Erp_StockCheckGoods, User>(a => a.CreateUser);
            }
        }

        //创建者id
        [Required]
        [MaxLength(50)]
        [ForeignKey("CreateUser")]
        public string CreateUserId
        {
            get
            {
                return _createUserId;
            }
            set
            {
                _createUserId = value;
                propInfoList.PutProperty<Erp_StockCheckGoods, string>(a => a.CreateUserId);
            }
        }

        //创建时间
        [Required]
        public DateTime CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
                propInfoList.PutProperty<Erp_StockCheckGoods, DateTime>(a => a.CreateTime);
            }
        }


        public void Create()
        {
            Erp_StockCheckGoodsId = Guid.NewGuid().ToString();
            CreateUserId = new OperatorProvider().GetCurrent().UserId;
            CreateTime = DateTime.Now;
            Status = "unchange";
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
