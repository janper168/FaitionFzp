using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class Erp_AccountTransferRecordBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_AccountTransferRecordService _Erp_AccountTransferRecordService = null;
        private Erp_AccountService _Erp_AccountService = null;
        private Erp_FinanceFlowService _Erp_FinanceFlowService = null;
        private BaseBLL _baseBLL = new BaseBLL(); 

        public Erp_AccountTransferRecordBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_AccountTransferRecordService = new Erp_AccountTransferRecordService(_dbContext);
            _Erp_AccountService = new Erp_AccountService(_dbContext);
            _Erp_FinanceFlowService = new Erp_FinanceFlowService(_dbContext);
        }

        public void AddEntity(Erp_AccountTransferRecord entity)
        {
            BaseTransaction bt = new BaseTransaction(_dbContext);

            bt.Execute((_db, _ts) =>{

                entity.Create();

                _Erp_AccountTransferRecordService.Add(entity);

                var totalAmount = entity.TotalAmount;
                var chargeAmount = entity.ChargeAmount;

                var outAccount = _Erp_AccountService.SingleOrDefault(entity.OutAccountId, true);
                var inAccount = _Erp_AccountService.SingleOrDefault(entity.InAccountId, true);

                var outAaccountBA = outAccount.BalanceAmount;
                var inAaccountBA = inAccount.BalanceAmount;
                var changeOut = 0m;
                var changeIn=0m;

                if (entity.ChargePayer == "1")//转出方付
                {
                    changeOut = (totalAmount + chargeAmount);
                    changeIn = totalAmount;

                    outAccount.BalanceAmount -= (totalAmount + chargeAmount);
                    if (outAccount.BalanceAmount < 0) 
                    {
                        throw new Exception("转账【" + outAccount.Name + "】余额不足");
                    }
                    inAccount.BalanceAmount += totalAmount;
                    if (inAccount.BalanceAmount < 0)
                    {
                        throw new Exception("转账【" + inAccount.Name + "】余额不足");
                    }
                }
                else//转入方付
                {
                    changeOut = totalAmount;
                    changeIn = (totalAmount - chargeAmount);

                    outAccount.BalanceAmount -= totalAmount;
                    if (outAccount.BalanceAmount < 0)
                    {
                        throw new Exception("转账【" + outAccount.Name + "】余额不足");
                    }
                    inAccount.BalanceAmount += (totalAmount - chargeAmount);
                    if (inAccount.BalanceAmount < 0)
                    {
                        throw new Exception("转账【" + inAccount.Name + "】余额不足");
                    }
                }

                Erp_FinanceFlow f1 = new Erp_FinanceFlow();
                f1.Create();
                f1.AmountChange = 0;
                f1.AccountId = entity.OutAccountId;
                f1.AmountBefore = outAaccountBA;
                f1.AmountChange = changeOut;
                f1.AmountAfter = outAccount.BalanceAmount;
                f1.Type = "Transfer";

                _Erp_FinanceFlowService.Add(f1);

                Erp_FinanceFlow f2 = new Erp_FinanceFlow();
                f2.Create();
                f2.AmountChange = 0;
                f2.AccountId = entity.InAccountId;
                f2.AmountBefore = inAaccountBA;
                f2.AmountChange = changeIn;
                f2.AmountAfter = inAccount.BalanceAmount;
                f2.Type = "Transfer";

                _Erp_FinanceFlowService.Add(f2);

                _Erp_AccountService.Update(outAccount, outAccount.GetPropInfoList(), outAccount.GetReferenceInfoList());
                _Erp_AccountService.Update(inAccount, inAccount.GetPropInfoList(), inAccount.GetReferenceInfoList());
            });

         
        }

        public void SetVoid(string Erp_AccountTransferRecordId )
        {
            BaseTransaction bt = new BaseTransaction(_dbContext);

            bt.Execute((_db, _ts) => {

                var entity = _Erp_AccountTransferRecordService.SingleOrDefault(Erp_AccountTransferRecordId, true);
                if (entity.IsVoid == 1)
                {
                    throw new Exception("该记录已作废，无需重复操作");
                }
                entity.IsVoid = 1;
                _Erp_AccountTransferRecordService.Update(entity, entity.GetPropInfoList(),entity.GetReferenceInfoList());

                var totalAmount = entity.TotalAmount;
                var chargeAmount = entity.ChargeAmount;

                var outAccount = _Erp_AccountService.SingleOrDefault(entity.OutAccountId, true);
                var inAccount = _Erp_AccountService.SingleOrDefault(entity.InAccountId, true);

                var outAaccountBA = outAccount.BalanceAmount;
                var inAaccountBA = inAccount.BalanceAmount;
                var changeOut = 0m;
                var changeIn = 0m;

                if (entity.ChargePayer == "1")//转出方付
                {
                    changeOut = (totalAmount + chargeAmount);
                    changeIn = totalAmount;

                    outAccount.BalanceAmount += (totalAmount + chargeAmount);
                    if (outAccount.BalanceAmount < 0)
                    {
                        throw new Exception("转账【" + outAccount.Name + "】余额不足");
                    }
                    outAccount.BalanceStatus = outAccount.BalanceAmount > 0 ? 1 : 0;
                    inAccount.BalanceAmount -= totalAmount;
                    if (inAccount.BalanceAmount < 0)
                    {
                        throw new Exception("转账【" + inAccount.Name + "】余额不足");
                    }
                    inAccount.BalanceStatus = inAccount.BalanceAmount > 0 ? 1 : 0;
                }
                else//转入方付
                {
                    changeOut = totalAmount;
                    changeIn = (totalAmount - chargeAmount);

                    outAccount.BalanceAmount += totalAmount;
                    outAccount.BalanceStatus = outAccount.BalanceAmount > 0 ? 1 : 0;
                    if (outAccount.BalanceAmount < 0)
                    {
                        throw new Exception("转账【" + outAccount.Name + "】余额不足");
                    }
                    inAccount.BalanceAmount -= (totalAmount - chargeAmount);
                    inAccount.BalanceStatus = inAccount.BalanceAmount > 0 ? 1 : 0;
                    if (inAccount.BalanceAmount < 0)
                    {
                        throw new Exception("转账【" + inAccount.Name + "】余额不足");
                    }
                    
                }

                Erp_FinanceFlow f1 = new Erp_FinanceFlow();
                f1.Create();
                f1.AmountChange = 0;
                f1.AccountId = entity.OutAccountId;
                f1.AmountAfter = outAccount.BalanceAmount;
                f1.AmountChange = changeOut;
                f1.AmountBefore = f1.AmountAfter + changeOut;
                f1.Type = "VoidTransfer";

                _Erp_FinanceFlowService.Add(f1);

                Erp_FinanceFlow f2 = new Erp_FinanceFlow();
                f2.Create();
                f2.AmountChange = 0;
                f2.AccountId = entity.InAccountId;
                f2.AmountAfter = inAccount.BalanceAmount;
                f2.AmountChange = changeIn;
                f2.AmountBefore = f2.AmountAfter - changeOut;
                f2.Type = "VoidTransfer";

                _Erp_FinanceFlowService.Add(f2);

                _Erp_AccountService.Update(outAccount, outAccount.GetPropInfoList(), outAccount.GetReferenceInfoList());
                _Erp_AccountService.Update(inAccount, inAccount.GetPropInfoList(), inAccount.GetReferenceInfoList());
            });


        }

        public void AddEntities(IEnumerable<Erp_AccountTransferRecord> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_AccountTransferRecordService.AddRange(entities);
        }

        public void UpdateEntity(Erp_AccountTransferRecord entity)
        {
            _Erp_AccountTransferRecordService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_AccountTransferRecord> entities)
        {
           foreach (var e in entities)
            {
                _Erp_AccountTransferRecordService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_AccountTransferRecord entity)
        {
            _Erp_AccountTransferRecordService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_AccountTransferRecord> entities)
        {
            _Erp_AccountTransferRecordService.RemoveRange(entities);
        }

        public Erp_AccountTransferRecord GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_AccountTransferRecordService.FindList(a => a.Erp_AccountTransferRecordId == keyValue,a => a.Erp_AccountTransferRecordId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_AccountTransferRecord> GetEntities(Expression<Func<Erp_AccountTransferRecord, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_AccountTransferRecordService.FindList(whereExpression, a => a.Erp_AccountTransferRecordId, isSortAsc);
             return data;
        }

        public IList<Erp_AccountTransferRecord> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_AccountTransferRecord, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_AccountTransferRecordService, pagination, sort, whereExpression,a=>a.Processor,a=>a.OutAccount,a=>a.InAccount);
        }

        public bool IsExists(Expression<Func<Erp_AccountTransferRecord, bool>> whereExpression)
        {
            return _Erp_AccountTransferRecordService.IsExists(whereExpression);
        }

    }
}
