using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using Microsoft.CodeAnalysis.CSharp;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Formula.UDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class Erp_PaymentOrderBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_PaymentOrderService _Erp_PaymentOrderService = null;
        private Erp_PaymentAccountService _Erp_PaymentAccountService = null;
        private Erp_SuppilerService _Erp_SuppilerService= null;
        private Erp_AccountService _Erp_AccountService = null;
        private Erp_FinanceFlowService _Erp_FinanceFlowService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_PaymentOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_PaymentOrderService = new Erp_PaymentOrderService(_dbContext);
            _Erp_SuppilerService = new Erp_SuppilerService(_dbContext);
            _Erp_PaymentAccountService = new Erp_PaymentAccountService(_dbContext); 
            _Erp_AccountService  = new Erp_AccountService(_dbContext);
            _Erp_FinanceFlowService = new Erp_FinanceFlowService    (_dbContext);
        }

        public void AddEntity(Erp_PaymentOrder entity)
        {
            entity.Create();
            _Erp_PaymentOrderService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_PaymentOrder> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_PaymentOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_PaymentOrder entity)
        {
            _Erp_PaymentOrderService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_PaymentOrder> entities)
        {
           foreach (var e in entities)
            {
                _Erp_PaymentOrderService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_PaymentOrder entity)
        {
            _Erp_PaymentOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_PaymentOrder> entities)
        {
            _Erp_PaymentOrderService.RemoveRange(entities);
        }

        public Erp_PaymentOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_PaymentOrderService.FindList(a => a.Erp_PaymentOrderId == keyValue,a => a.Erp_PaymentOrderId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_PaymentOrder> GetEntities(Expression<Func<Erp_PaymentOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_PaymentOrderService.FindList(whereExpression, a => a.Processor, a => a.Suppiler,a => a.Erp_PaymentOrderId, isSortAsc);
             return data;
        }

        public IList<Erp_PaymentOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_PaymentOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_PaymentOrderService, pagination, sort, whereExpression,a => a.Processor, a => a.Suppiler);
        }

        public bool IsExists(Expression<Func<Erp_PaymentOrder, bool>> whereExpression)
        {
            return _Erp_PaymentOrderService.IsExists(whereExpression);
        }

        public void SavePaymentOrderForm(Erp_PaymentOrder entity)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, td) =>
            {
                entity.Create();
                entity.TotalAmount = (entity.DiscountAmount > 0 ? entity.DiscountAmount: 0);
                entity.ProcessorId = new OperatorProvider().GetCurrent().UserId;

                //付款账户
                if (entity.PaymentAccountList != null && entity.PaymentAccountList.Count() > 0)
                {
                    foreach (var account in entity.PaymentAccountList)
                    {
                        account.Create();
                        entity.TotalAmount += account.PaymentAmount;
                    }
                }

                _Erp_PaymentOrderService.Add(entity);

                var suppiler = _Erp_SuppilerService.SingleOrDefault(entity.SuppilerId, true);
                if (suppiler != null)
                { 
                    suppiler.ArearsAmount -= entity.TotalAmount.Value;
                    suppiler.ArearsStatus = suppiler.ArearsAmount > 0 ? 1 : 0;

                    _Erp_SuppilerService.Update(suppiler, suppiler.GetPropInfoList(), suppiler.GetReferenceInfoList());
                }

                foreach (var paymentAccount in entity.PaymentAccountList)
                {
                    var account = _Erp_AccountService.SingleOrDefault(paymentAccount.AccountId, true);
                    var amountBefore = account.BalanceAmount;
                    var amountChange = paymentAccount.PaymentAmount;
                    var amountAfter = amountBefore - amountChange;

                    var financeFlow = new Erp_FinanceFlow();
                    financeFlow.Create();
                    financeFlow.AccountId = paymentAccount.AccountId;
                    financeFlow.AmountChange = amountChange;
                    financeFlow.AmountBefore = amountBefore;
                    financeFlow.AmountAfter = amountAfter;
                    financeFlow.PaymentOrderId = entity.Erp_PaymentOrderId;
                    financeFlow.Type = "Payment";
                    _Erp_FinanceFlowService.Add(financeFlow);
                    account.BalanceAmount = amountAfter;
                    if (amountAfter < 0) 
                    {
                        throw new Exception("账户[" + account.Name + "]余额不足");
                    }
                    account.BalanceStatus = amountAfter > 0 ? 1 : 0;
                    _Erp_AccountService.Update(account,account.GetPropInfoList(),account.GetReferenceInfoList());

                }

            });
         }

        public void SetVoid(string Erp_PaymentOrderId)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, td) =>
            {
                var entity = _Erp_PaymentOrderService.FindList(a=>a.Erp_PaymentOrderId == Erp_PaymentOrderId,
                    a=>a.PaymentAccountList,a=>a.Erp_PaymentOrderId,true,true)[0];

                if (entity.IsVoid == 1)
                {
                    throw new Exception("该付款单[" + entity.Number + "]已作废,无需重复操作");
                }
                
                entity.IsVoid = 1;
                //付款账户
                if (entity.PaymentAccountList != null && entity.PaymentAccountList.Count() > 0)
                {
                    foreach (var account in entity.PaymentAccountList)
                    {
                        entity.TotalAmount -= account.PaymentAmount;
                    }
                }

                _Erp_PaymentOrderService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());

                var suppiler = _Erp_SuppilerService.SingleOrDefault(entity.SuppilerId, true);
                if (suppiler != null)
                {
                    suppiler.ArearsAmount += entity.TotalAmount.Value;
                    suppiler.ArearsStatus = suppiler.ArearsAmount > 0 ? 1 : 0;

                    _Erp_SuppilerService.Update(suppiler, suppiler.GetPropInfoList(), suppiler.GetReferenceInfoList());
                }

                foreach (var paymentAccount in entity.PaymentAccountList)
                {
                    var account = _Erp_AccountService.SingleOrDefault(paymentAccount.AccountId, true);
                    var amountBefore = account.BalanceAmount;
                    var amountChange = paymentAccount.PaymentAmount;
                    var amountAfter = amountBefore + amountChange;

                    var financeFlow = new Erp_FinanceFlow();
                    financeFlow.Create();
                    financeFlow.AccountId = paymentAccount.AccountId;
                    financeFlow.AmountChange = amountChange;
                    financeFlow.AmountBefore = amountBefore;
                    financeFlow.AmountAfter = amountAfter;
                    financeFlow.PaymentOrderId = entity.Erp_PaymentOrderId;
                    financeFlow.Type = "VoidPayment";
                    _Erp_FinanceFlowService.Add(financeFlow);
                    account.BalanceAmount = amountAfter;
                    if (amountAfter < 0)
                    {
                        throw new Exception("账户[" + account.Name + "]余额不足");
                    }
                    account.BalanceStatus = amountAfter > 0 ? 1 : 0;
                    _Erp_AccountService.Update(account, account.GetPropInfoList(), account.GetReferenceInfoList());

                }

            });
        }
    }
}
