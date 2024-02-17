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
    public class Erp_StockCheckOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockCheckOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockCheckOrderId;

        //编号
        private string _number;

        //仓库
        private Erp_Warehouse _warehouse;

        //仓库id
        private string _warehouseId;

        //处理人
        private User _processor;

        //处理人id
        private string _processorId;

        //备注
        private string _remark;

        //状态
        private string _status;

        //账面总数量
        private int? _totalBookQuantity;

        //实际总数量
        private int? _totalActualQuantity;

        //盘盈总数量
        private int? _totalSurplusQuantity;

        //盘盈总金额
        private decimal? _totalSurplusAmount;

        //作废状态
        private int _isVoid;

        //创建者
        private User _createUser;

        //创建者id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;

        private ICollection<Erp_StockCheckGoods> _stockCheckGoodsList;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockCheckOrderId
        {
            get
            {
                return _erp_StockCheckOrderId;
            }
            set
            {
                _erp_StockCheckOrderId = value;
            }
        }

        //编号
        [Required]
        [MaxLength(50)]
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                propInfoList.PutProperty<Erp_StockCheckOrder, string>(a => a.Number);
            }
        }

        //仓库
        [ForeignKey("WarehouseId")]
        public virtual Erp_Warehouse Warehouse
        {
            get
            {
                return _warehouse;
            }
            set
            {
                _warehouse = value;
                referenceInfoList.PutProperty<Erp_StockCheckOrder, Erp_Warehouse>(a => a.Warehouse);
            }
        }

        //仓库id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Warehouse")]
        public string WarehouseId
        {
            get
            {
                return _warehouseId;
            }
            set
            {
                _warehouseId = value;
                propInfoList.PutProperty<Erp_StockCheckOrder, string>(a => a.WarehouseId);
            }
        }

        //处理人
        [ForeignKey("ProcessorId")]
        public virtual User Processor
        {
            get
            {
                return _processor;
            }
            set
            {
                _processor = value;
                referenceInfoList.PutProperty<Erp_StockCheckOrder, User>(a => a.Processor);
            }
        }

        //处理人id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Processor")]
        public string ProcessorId
        {
            get
            {
                return _processorId;
            }
            set
            {
                _processorId = value;
                propInfoList.PutProperty<Erp_StockCheckOrder, string>(a => a.ProcessorId);
            }
        }

        //备注
        [MaxLength(2000)]
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
                propInfoList.PutProperty<Erp_StockCheckOrder, string>(a => a.Remark);
            }
        }

        //状态
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                propInfoList.PutProperty<Erp_StockCheckOrder, string>(a => a.Status);
            }
        }

        //账面总数量
        public int? TotalBookQuantity
        {
            get
            {
                return _totalBookQuantity;
            }
            set
            {
                _totalBookQuantity = value;
                propInfoList.PutProperty<Erp_StockCheckOrder, int?>(a => a.TotalBookQuantity);
            }
        }

        //实际总数量
        public int? TotalActualQuantity
        {
            get
            {
                return _totalActualQuantity;
            }
            set
            {
                _totalActualQuantity = value;
                propInfoList.PutProperty<Erp_StockCheckOrder, int?>(a => a.TotalActualQuantity);
            }
        }

        //盘盈总数量
        public int? TotalSurplusQuantity
        {
            get
            {
                return _totalSurplusQuantity;
            }
            set
            {
                _totalSurplusQuantity = value;
                propInfoList.PutProperty<Erp_StockCheckOrder, int?>(a => a.TotalSurplusQuantity);
            }
        }

        //盘盈总金额
        public decimal? TotalSurplusAmount
        {
            get
            {
                return _totalSurplusAmount;
            }
            set
            {
                _totalSurplusAmount = value;
                propInfoList.PutProperty<Erp_StockCheckOrder, decimal?>(a => a.TotalSurplusAmount);
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
                propInfoList.PutProperty<Erp_StockCheckOrder, int>(a => a.IsVoid);
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
                referenceInfoList.PutProperty<Erp_StockCheckOrder, User>(a => a.CreateUser);
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
                propInfoList.PutProperty<Erp_StockCheckOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_StockCheckOrder, DateTime>(a => a.CreateTime);
            }
        }

        public virtual ICollection<Erp_StockCheckGoods> StockCheckGoodsList
        {
            get
            {
                return _stockCheckGoodsList;
            }
            set
            {
                _stockCheckGoodsList = value;
                referenceInfoList.PutProperty<Erp_StockCheckOrder, ICollection<Erp_StockCheckGoods>>(a => a.StockCheckGoodsList);
            }
        }


        public void Create()
        {
            Erp_StockCheckOrderId = Guid.NewGuid().ToString();
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
