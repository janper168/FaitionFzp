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
    public class Erp_AccountTransferRecord : BaseEntity
    {
        private List<PropertyInfo> propInfoList = null;

        private List<PropertyInfo> referenceInfoList = null;

        public Erp_AccountTransferRecord()
        {
            propInfoList = DBUtils.GetPropertyInfoList();
            referenceInfoList = DBUtils.GetPropertyInfoList();
        }

        //关键字
        private string _erp_AccountTransferRecordId;

        //手续费支付方
        private string _chargePayer;

        //转出账户
        private Erp_Account _outAccount;

        //转出账户id
        private string _outAccountId;

        //转入账户
        private Erp_Account _inAccount;

        //转入账户id
        private string _inAccountId;

        //转出时间
        private DateTime _outTime;

        //转入时间
        private DateTime _inTime;

        //手续金额
        private decimal _chargeAmount;

        //手续金额
        private decimal _totalAmount;

        //处理人
        private User _processor;

        //处理人id
        private string _processorId;

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
        public string Erp_AccountTransferRecordId
        {
            get
            {
                return _erp_AccountTransferRecordId;
            }
            set
            {
                _erp_AccountTransferRecordId = value;
            }
        }

        //手续费支付方
        [Required]
        [MaxLength(50)]
        public string ChargePayer
        {
            get
            {
                return _chargePayer;
            }
            set
            {
                _chargePayer = value;
                propInfoList.PutProperty<Erp_AccountTransferRecord, string>(a => a.ChargePayer);
            }
        }

        //转出账户
        [ForeignKey("OutAccountId")]
        public virtual Erp_Account OutAccount
        {
            get
            {
                return _outAccount;
            }
            set
            {
                _outAccount = value;
                referenceInfoList.PutProperty<Erp_AccountTransferRecord, Erp_Account>(a => a.OutAccount);
            }
        }

        //转出账户id
        [Required]
        [MaxLength(50)]
        [ForeignKey("OutAccount")]
        public string OutAccountId
        {
            get
            {
                return _outAccountId;
            }
            set
            {
                _outAccountId = value;
                propInfoList.PutProperty<Erp_AccountTransferRecord, string>(a => a.OutAccountId);
            }
        }

        //转入账户
        [ForeignKey("InAccountId")]
        public virtual Erp_Account InAccount
        {
            get
            {
                return _inAccount;
            }
            set
            {
                _inAccount = value;
                referenceInfoList.PutProperty<Erp_AccountTransferRecord, Erp_Account>(a => a.InAccount);
            }
        }

        //转入账户id
        [Required]
        [MaxLength(50)]
        [ForeignKey("InAccount")]
        public string InAccountId
        {
            get
            {
                return _inAccountId;
            }
            set
            {
                _inAccountId = value;
                propInfoList.PutProperty<Erp_AccountTransferRecord, string>(a => a.InAccountId);
            }
        }

        //转出时间
        [Required]
        public DateTime OutTime
        {
            get
            {
                return _outTime;
            }
            set
            {
                _outTime = value;
                propInfoList.PutProperty<Erp_AccountTransferRecord, DateTime>(a => a.OutTime);
            }
        }

        //转入时间
        [Required]
        public DateTime InTime
        {
            get
            {
                return _inTime;
            }
            set
            {
                _inTime = value;
                propInfoList.PutProperty<Erp_AccountTransferRecord, DateTime>(a => a.InTime);
            }
        }

        //手续金额
        [Required]
        public decimal ChargeAmount
        {
            get
            {
                return _chargeAmount;
            }
            set
            {
                _chargeAmount = value;
                propInfoList.PutProperty<Erp_AccountTransferRecord, decimal>(a => a.ChargeAmount);
            }
        }


        //手续金额
        [Required]
        public decimal TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
                propInfoList.PutProperty<Erp_AccountTransferRecord, decimal>(a => a.TotalAmount);
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
                referenceInfoList.PutProperty<Erp_AccountTransferRecord, User>(a => a.Processor);
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
                propInfoList.PutProperty<Erp_AccountTransferRecord, string>(a => a.ProcessorId);
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
                propInfoList.PutProperty<Erp_AccountTransferRecord, string>(a => a.Remark);
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
                propInfoList.PutProperty<Erp_AccountTransferRecord, int>(a => a.IsVoid);
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
                referenceInfoList.PutProperty<Erp_AccountTransferRecord, User>(a => a.CreateUser);
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
                propInfoList.PutProperty<Erp_AccountTransferRecord, string>(a => a.CreateUserId);
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
                propInfoList.PutProperty<Erp_AccountTransferRecord, DateTime>(a => a.CreateTime);
            }
        }


        public void Create()
        {
            Erp_AccountTransferRecordId = Guid.NewGuid().ToString();
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
