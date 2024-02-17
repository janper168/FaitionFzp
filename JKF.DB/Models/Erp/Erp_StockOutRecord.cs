using JKF.DB.extension;
using JKF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Models
{
    public class Erp_StockOutRecord : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_StockOutRecord()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_StockOutRecordId;

        //仓库
        private Erp_Warehouse _warehouse;

        //仓库d
        private string _warehouseId;

        //出库单据
        private Erp_StockOutOrder _stockOutOrder;

        //出库单据id
        private string _stockOutOrderId;

        //处理人
        private User _processor;

        //处理人id
        private string _processorId;

        //处理时间
        private DateTime? _processTime;

        //出库总数
        private int _totalQuantity;

        //备注
        private string _remark;

        //作废状态
        private int _isVoid;

        //创建人
        private User _createUser;

        //创建人id
        private string _createUserId;

        //创建时间
        private DateTime _createTime;


        //关键字
        [Key]
        [MaxLength(50)]
        public string Erp_StockOutRecordId
        {
            get
            {
                return _erp_StockOutRecordId;
            }
            set
            {
                _erp_StockOutRecordId = value;
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
                referenceInfoList.PutProperty<Erp_StockOutRecord, Erp_Warehouse>(a => a.Warehouse);
            }
        }

        //仓库d
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
                propInfoList.PutProperty<Erp_StockOutRecord, string>(a => a.WarehouseId);
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
                referenceInfoList.PutProperty<Erp_StockOutRecord, Erp_StockOutOrder>(a => a.StockOutOrder);
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
                propInfoList.PutProperty<Erp_StockOutRecord, string>(a => a.StockOutOrderId);
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
                referenceInfoList.PutProperty<Erp_StockOutRecord, User>(a => a.Processor);
            }
        }

        //处理人id
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
                propInfoList.PutProperty<Erp_StockOutRecord, string>(a => a.ProcessorId);
            }
        }

        //处理时间
        public DateTime? ProcessTime
        {
            get
            {
                return _processTime;
            }
            set
            {
                _processTime = value;
                propInfoList.PutProperty<Erp_StockOutRecord, DateTime?>(a => a.ProcessTime);
            }
        }

        //出库总数
        [Required]
        public int TotalQuantity
        {
            get
            {
                return _totalQuantity;
            }
            set
            {
                _totalQuantity = value;
                propInfoList.PutProperty<Erp_StockOutRecord, int>(a => a.TotalQuantity);
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
                propInfoList.PutProperty<Erp_StockOutRecord, string>(a => a.Remark);
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
                propInfoList.PutProperty<Erp_StockOutRecord, int>(a => a.IsVoid);
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
                referenceInfoList.PutProperty<Erp_StockOutRecord, User>(a => a.CreateUser);
            }
        }

        //创建人id
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
                propInfoList.PutProperty<Erp_StockOutRecord, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_StockOutRecord, DateTime>(a => a.CreateTime);
            }
        }


        public void Create()
        {
            Erp_StockOutRecordId = Guid.NewGuid().ToString();
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
