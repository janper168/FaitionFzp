using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class Erp_CollectionOrderBLL
    {
        private BaseDbContext _dbContext = null;

        private Erp_CollectionOrderService _Erp_CollectionOrderService = null;
        private Erp_CollectionAccountService _Erp_CollectionAccountService = null;
        private Erp_CustomerService _Erp_CustomerService = null;
        private Erp_AccountService _Erp_AccountService = null;
        private Erp_FinanceFlowService _Erp_FinanceFlowService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_CollectionOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_CollectionOrderService = new Erp_CollectionOrderService(_dbContext);
            _Erp_CollectionAccountService = new Erp_CollectionAccountService(_dbContext);
            _Erp_CustomerService = new Erp_CustomerService(_dbContext);
            _Erp_AccountService = new Erp_AccountService(_dbContext);
            _Erp_FinanceFlowService = new Erp_FinanceFlowService(_dbContext);
        }

        public void AddEntity(Erp_CollectionOrder entity)
        {
            entity.Create();
            _Erp_CollectionOrderService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_CollectionOrder> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_CollectionOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_CollectionOrder entity)
        {
            _Erp_CollectionOrderService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_CollectionOrder> entities)
        {
           foreach (var e in entities)
            {
                _Erp_CollectionOrderService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_CollectionOrder entity)
        {
            _Erp_CollectionOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_CollectionOrder> entities)
        {
            _Erp_CollectionOrderService.RemoveRange(entities);
        }

        public Erp_CollectionOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_CollectionOrderService.FindList(a => a.Erp_CollectionOrderId == keyValue,a => a.Erp_CollectionOrderId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_CollectionOrder> GetEntities(Expression<Func<Erp_CollectionOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_CollectionOrderService.FindList(whereExpression, a => a.Erp_CollectionOrderId, isSortAsc);
             return data;
        }

        public IList<Erp_CollectionOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_CollectionOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_CollectionOrderService, pagination, sort, whereExpression,a=>a.Processor,a=>a.Customer);
        }

        public bool IsExists(Expression<Func<Erp_CollectionOrder, bool>> whereExpression)
        {
            return _Erp_CollectionOrderService.IsExists(whereExpression);
        }

        public void SaveCollectionOrderForm(Erp_CollectionOrder entity)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, td) =>
            {
                entity.Create();
                entity.TotalAmount = -(entity.DiscountAmount > 0 ? entity.DiscountAmount : 0);
                entity.ProcessorId = new OperatorProvider().GetCurrent().UserId;

                //收款账户
                if (entity.CollectionAccountList != null && entity.CollectionAccountList.Count() > 0)
                {
                    foreach (var account in entity.CollectionAccountList)
                    {
                        account.Create();
                        entity.TotalAmount += account.CollectionAmount;
                    }
                }

                _Erp_CollectionOrderService.Add(entity);

                var customer = _Erp_CustomerService.SingleOrDefault(entity.CustomerId, true);
                if (customer != null)
                {
                    customer.ArearsAmount -= entity.TotalAmount.Value;
                    customer.ArearsStatus = customer.ArearsAmount > 0 ? 1 : 0;

                    _Erp_CustomerService.Update(customer, customer.GetPropInfoList(), customer.GetReferenceInfoList());
                }

                foreach (var CollectionAccount in entity.CollectionAccountList)
                {
                    var account = _Erp_AccountService.SingleOrDefault(CollectionAccount.AccountId, true);
                    var amountBefore = account.BalanceAmount;
                    var amountChange = CollectionAccount.CollectionAmount;
                    var amountAfter = amountBefore + amountChange;

                    var financeFlow = new Erp_FinanceFlow();
                    financeFlow.Create();
                    financeFlow.AccountId = CollectionAccount.AccountId;
                    financeFlow.AmountChange = amountChange;
                    financeFlow.AmountBefore = amountBefore;
                    financeFlow.AmountAfter = amountAfter;
                    financeFlow.CollectionOrderId = entity.Erp_CollectionOrderId;
                    financeFlow.Type = "Collection";
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

        public void SetVoid(string Erp_CollectionOrderId)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, td) =>
            {
                var entity = _Erp_CollectionOrderService.FindList(a => a.Erp_CollectionOrderId == Erp_CollectionOrderId,
                    a => a.CollectionAccountList, a => a.Erp_CollectionOrderId, true, true)[0];

                if (entity.IsVoid == 1)
                {
                    throw new Exception("该付款单[" + entity.Number + "]已作废,无需重复操作");
                }

                entity.IsVoid = 1;
                //收款账户
                if (entity.CollectionAccountList != null && entity.CollectionAccountList.Count() > 0)
                {
                    foreach (var account in entity.CollectionAccountList)
                    {
                        entity.TotalAmount -= account.CollectionAmount;
                    }
                }

                _Erp_CollectionOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

                var customer = _Erp_CustomerService.SingleOrDefault(entity.CustomerId, true);
                if (customer != null)
                {
                    customer.ArearsAmount += entity.TotalAmount.Value;
                    customer.ArearsStatus = customer.ArearsAmount > 0 ? 1 : 0;

                    _Erp_CustomerService.Update(customer, customer.GetPropInfoList(), customer.GetReferenceInfoList());
                }

                foreach (var CollectionAccount in entity.CollectionAccountList)
                {
                    var account = _Erp_AccountService.SingleOrDefault(CollectionAccount.AccountId, true);
                    var amountBefore = account.BalanceAmount;
                    var amountChange = CollectionAccount.CollectionAmount;
                    var amountAfter = amountBefore - amountChange;

                    var financeFlow = new Erp_FinanceFlow();
                    financeFlow.Create();
                    financeFlow.AccountId = CollectionAccount.AccountId;
                    financeFlow.AmountChange = amountChange;
                    financeFlow.AmountBefore = amountBefore;
                    financeFlow.AmountAfter = amountAfter;
                    financeFlow.CollectionOrderId = entity.Erp_CollectionOrderId;
                    financeFlow.Type = "VoidCollection";
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
