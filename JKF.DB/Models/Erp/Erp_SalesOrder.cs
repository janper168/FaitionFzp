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
    public class Erp_SalesOrder : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_SalesOrder()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_SalesOrderId;

        //编号
        private string _number;

        //仓库
        private Erp_Warehouse _warehouse;

        //仓库id
        private string _warehouseId;

        //客户
        private Erp_Customer _customer;

        //客户id
        private string _customerId;

        //处理时间
        private DateTime _processTime;

        //备注
        private string _remark;

        //采购总数量
        private int? _totalQuantity;

        //折扣
        private float _discount;

        //其他费用
        private decimal _otherAmount;

        //采购总金额
        private decimal? _totalAmount;

        //收款金额
        private decimal? _collectionAmount;

        //欠款金额
        private decimal? _arearsAmount;

        //作废状态
        private int _isVoid;

        //启用自动出库
        private int _enableAutoStockOut;

        //创建人
        private User _createUser;

        //创建人id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;

        //商品列表
        private ICollection<Erp_SalesGoods> _salesGoodsList;

        //销售账户
        private Erp_SalesAccount _salesAccount;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_SalesOrderId
        {
            get
            {
                return _erp_SalesOrderId;
            }
            set
            {
                _erp_SalesOrderId = value;
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
                propInfoList.PutProperty<Erp_SalesOrder, string>(a => a.Number);
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
                referenceInfoList.PutProperty<Erp_SalesOrder, Erp_Warehouse>(a => a.Warehouse);
            }
        }

        //仓库id
        [Required]
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
                propInfoList.PutProperty<Erp_SalesOrder, string>(a => a.WarehouseId);
            }
        }

        //客户
        [ForeignKey("CustomerId")]
        public virtual Erp_Customer Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                referenceInfoList.PutProperty<Erp_SalesOrder, Erp_Customer>(a => a.Customer);
            }
        }

        //客户id
        [Required]
        [MaxLength(50)]
        [ForeignKey("Customer ")]
        public string CustomerId
        {
            get
            {
                return _customerId;
            }
            set
            {
                _customerId = value;
                propInfoList.PutProperty<Erp_SalesOrder, string>(a => a.CustomerId);
            }
        }

        //处理时间
        [Required]
        public DateTime ProcessTime
        {
            get
            {
                return _processTime;
            }
            set
            {
                _processTime = value;
                propInfoList.PutProperty<Erp_SalesOrder, DateTime>(a => a.ProcessTime);
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
                propInfoList.PutProperty<Erp_SalesOrder, string>(a => a.Remark);
            }
        }

        //采购总数量
        public int? TotalQuantity
        {
            get
            {
                return _totalQuantity;
            }
            set
            {
                _totalQuantity = value;
                propInfoList.PutProperty<Erp_SalesOrder, int?>(a => a.TotalQuantity);
            }
        }

        //折扣
        [Required]
        public float Discount
        {
            get
            {
                return _discount;
            }
            set
            {
                _discount = value;
                propInfoList.PutProperty<Erp_SalesOrder, float>(a => a.Discount);
            }
        }

        //其他费用
        [Required]
        public decimal otherAmount
        {
            get
            {
                return _otherAmount;
            }
            set
            {
                _otherAmount = value;
                propInfoList.PutProperty<Erp_SalesOrder, decimal>(a => a.otherAmount);
            }
        }

        //采购总金额
        public decimal? TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
                propInfoList.PutProperty<Erp_SalesOrder, decimal?>(a => a.TotalAmount);
            }
        }

        //收款金额
        public decimal? CollectionAmount
        {
            get
            {
                return _collectionAmount;
            }
            set
            {
                _collectionAmount = value;
                propInfoList.PutProperty<Erp_SalesOrder, decimal?>(a => a.CollectionAmount);
            }
        }

        //欠款金额
        public decimal? ArearsAmount
        {
            get
            {
                return _arearsAmount;
            }
            set
            {
                _arearsAmount = value;
                propInfoList.PutProperty<Erp_SalesOrder, decimal?>(a => a.ArearsAmount);
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
                propInfoList.PutProperty<Erp_SalesOrder, int>(a => a.IsVoid);
            }
        }

        //启用自动出库
        [Required]
        public int EnableAutoStockOut
        {
            get
            {
                return _enableAutoStockOut;
            }
            set
            {
                _enableAutoStockOut = value;
                propInfoList.PutProperty<Erp_SalesOrder, int>(a => a.EnableAutoStockOut);
            }
        }

        //创建人
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
                referenceInfoList.PutProperty<Erp_SalesOrder, User>(a => a.CreateUser);
            }
        }

        //创建人id
        [Required]
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
                propInfoList.PutProperty<Erp_SalesOrder, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_SalesOrder, DateTime>(a => a.CreateTime);
            }
        }

        //商品列表
        public virtual ICollection<Erp_SalesGoods> SalesGoodsList
        {
            get
            {
                return _salesGoodsList;
            }
            set
            {
                _salesGoodsList = value;
                referenceInfoList.PutProperty<Erp_SalesOrder, ICollection<Erp_SalesGoods>>(a => a.SalesGoodsList);
            }
        }

        //销售账户
        public virtual Erp_SalesAccount SalesAccount
        {
            get
            {
                return _salesAccount;
            }
            set
            {
                _salesAccount = value;
                referenceInfoList.PutProperty<Erp_SalesOrder, Erp_SalesAccount>(a => a.SalesAccount);
            }
        }


        public void Create()
        {
            Erp_SalesOrderId = Guid.NewGuid().ToString();
            CreateUserId = new OperatorProvider().GetCurrent().UserId;
            CreateTime = DateTime.Now;
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
