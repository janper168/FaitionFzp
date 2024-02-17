using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static NPOI.HSSF.Util.HSSFColor;

namespace JKF.BLL
{
    public class Erp_ChargeOrderBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_ChargeOrderService _Erp_ChargeOrderService = null;
        private Erp_ChargeItemService _Erp_ChargeItemService = null;
        private Erp_AccountService _Erp_AccountService = null;
        private Erp_SuppilerService _Erp_SuppilerService = null;
        private Erp_CustomerService _Erp_CustomerService = null;
        private Erp_FinanceFlowService _Erp_FinanceFlowService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_ChargeOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_ChargeOrderService = new Erp_ChargeOrderService(_dbContext);
            _Erp_ChargeItemService = new Erp_ChargeItemService(_dbContext);
            _Erp_AccountService = new Erp_AccountService(_dbContext);
            _Erp_SuppilerService = new Erp_SuppilerService(_dbContext);
            _Erp_CustomerService = new Erp_CustomerService(_dbContext);
            _Erp_FinanceFlowService = new Erp_FinanceFlowService(_dbContext);
        }

        public void AddEntity(Erp_ChargeOrder entity)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, td) =>
            {
                entity.Create();
                var chargeItemType = _Erp_ChargeItemService.SingleOrDefault(entity.ChargeItemId, false);
                entity.Type = chargeItemType.Type.ToString();

                _Erp_ChargeOrderService.Add(entity);

                var account = _Erp_AccountService.SingleOrDefault(entity.AccountId, true);
                var amountBefore = account.BalanceAmount;
                var arrearAmount = entity.TotalAmount - entity.ChargeAmount;
                var amountChange = 0.0m;
                if (entity.Type == "1")
                { //收入
                    amountChange = entity.ChargeAmount.Value; ;
                    if (!string.IsNullOrEmpty(entity.SuppilerId)) //供应商
                    {
                        var suppiler = _Erp_SuppilerService.SingleOrDefault(entity.SuppilerId, true);
                        suppiler.ArearsAmount = suppiler.ArearsAmount - arrearAmount.Value;
                        suppiler.ArearsStatus = suppiler.ArearsAmount > 0 ? 1 : 0;
                        _Erp_SuppilerService.Update(suppiler, suppiler.GetPropInfoList(), suppiler.GetReferenceInfoList());
                    }
                    else if (!string.IsNullOrEmpty(entity.CustomerId))
                    {
                        var customer = _Erp_CustomerService.SingleOrDefault(entity.CustomerId, true);
                        customer.ArearsAmount = customer.ArearsAmount + arrearAmount.Value;
                        customer.ArearsStatus = customer.ArearsAmount > 0 ? 1 : 0;
                        _Erp_CustomerService.Update(customer, customer.GetPropInfoList(), customer.GetReferenceInfoList());
                    }

                }
                else
                { //支出

                    amountChange = -entity.ChargeAmount.Value;
                    if (!string.IsNullOrEmpty(entity.SuppilerId)) //供应商
                    {
                        var suppiler = _Erp_SuppilerService.SingleOrDefault(entity.SuppilerId, true);
                        suppiler.ArearsAmount = suppiler.ArearsAmount + arrearAmount.Value;
                        suppiler.ArearsStatus = suppiler.ArearsAmount > 0 ? 1 : 0;
                        _Erp_SuppilerService.Update(suppiler, suppiler.GetPropInfoList(), suppiler.GetReferenceInfoList());
                    }
                    else if (!string.IsNullOrEmpty(entity.CustomerId))
                    {
                        var customer = _Erp_CustomerService.SingleOrDefault(entity.CustomerId, true);
                        customer.ArearsAmount = customer.ArearsAmount - arrearAmount.Value;
                        customer.ArearsStatus = customer.ArearsAmount > 0 ? 1 : 0;
                        _Erp_CustomerService.Update(customer, customer.GetPropInfoList(), customer.GetReferenceInfoList());
                    }

                }
                var amountAfter = amountBefore + amountChange;

                var flow = new Erp_FinanceFlow();
                flow.Create();
                flow.AmountChange = amountChange;
                flow.AmountBefore = amountBefore;
                flow.AmountAfter = amountAfter;
                flow.AccountId = entity.AccountId;
                flow.Type = "Charge";

                _Erp_FinanceFlowService.Add(flow);

                account.BalanceAmount = amountAfter;
                if (account.BalanceAmount < 0)
                {
                    throw new Exception("账号[" + account.Name + "]余额不足!");
                }
                account.BalanceStatus = account.BalanceAmount > 0 ? 1 : 0;
                _Erp_AccountService.Update(account, account.GetPropInfoList(), account.GetReferenceInfoList());
            });
        }


        public void AddEntities(IEnumerable<Erp_ChargeOrder> entities)
        {
            foreach (var e in entities)
            {
                e.Create();
            }
            _Erp_ChargeOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_ChargeOrder entity)
        {
            _Erp_ChargeOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_ChargeOrder> entities)
        {
            foreach (var e in entities)
            {
                _Erp_ChargeOrderService.Update(e, e.GetPropInfoList(), e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_ChargeOrder entity)
        {
            _Erp_ChargeOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_ChargeOrder> entities)
        {
            _Erp_ChargeOrderService.RemoveRange(entities);
        }

        public Erp_ChargeOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_ChargeOrderService.FindList(a => a.Erp_ChargeOrderId == keyValue, a => a.Erp_ChargeOrderId, true, beTraced)[0];
            return entity;
        }

        public IList<Erp_ChargeOrder> GetEntities(Expression<Func<Erp_ChargeOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_ChargeOrderService.FindList(whereExpression, a => a.Erp_ChargeOrderId, isSortAsc);
            return data;
        }

        public IList<Erp_ChargeOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_ChargeOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_ChargeOrderService, pagination, sort, whereExpression, a => a.Suppiler, a => a.Customer, a => a.Processor, a => a.ChargeItem, a => a.Account);
        }

        public bool IsExists(Expression<Func<Erp_ChargeOrder, bool>> whereExpression)
        {
            return _Erp_ChargeOrderService.IsExists(whereExpression);
        }

        public void SetVoid(string Erp_ChargeOrderId)
        {

            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, td) =>
            {
                var entity = _Erp_ChargeOrderService.SingleOrDefault(Erp_ChargeOrderId,true);

                if (entity.IsVoid == 1)
                {
                    throw new Exception("单号[" + entity.Number + "]已作废,不用重复操作!");
                }
                entity.IsVoid = 1;

                _Erp_ChargeOrderService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());

                var account = _Erp_AccountService.SingleOrDefault(entity.AccountId, true);
                var amountBefore = account.BalanceAmount;
                var arrearAmount = entity.TotalAmount - entity.ChargeAmount;
                var amountChange = 0.0m;
                if (entity.Type == "1")
                { //收入
                    amountChange = entity.ChargeAmount.Value; ;
                    if (!string.IsNullOrEmpty(entity.SuppilerId)) //供应商
                    {
                        var suppiler = _Erp_SuppilerService.SingleOrDefault(entity.SuppilerId, true);
                        suppiler.ArearsAmount = suppiler.ArearsAmount + arrearAmount.Value;
                        suppiler.ArearsStatus = suppiler.ArearsAmount > 0 ? 1 : 0;
                        _Erp_SuppilerService.Update(suppiler, suppiler.GetPropInfoList(), suppiler.GetReferenceInfoList());
                    }
                    else if (!string.IsNullOrEmpty(entity.CustomerId))
                    {
                        var customer = _Erp_CustomerService.SingleOrDefault(entity.CustomerId, true);
                        customer.ArearsAmount = customer.ArearsAmount - arrearAmount.Value;
                        customer.ArearsStatus = customer.ArearsAmount > 0 ? 1 : 0;
                        _Erp_CustomerService.Update(customer, customer.GetPropInfoList(), customer.GetReferenceInfoList());
                    }

                }
                else
                { //支出

                    amountChange = -entity.ChargeAmount.Value;
                    if (!string.IsNullOrEmpty(entity.SuppilerId)) //供应商
                    {
                        var suppiler = _Erp_SuppilerService.SingleOrDefault(entity.SuppilerId, true);
                        suppiler.ArearsAmount = suppiler.ArearsAmount - arrearAmount.Value;
                        suppiler.ArearsStatus = suppiler.ArearsAmount > 0 ? 1 : 0;
                        _Erp_SuppilerService.Update(suppiler, suppiler.GetPropInfoList(), suppiler.GetReferenceInfoList());
                    }
                    else if (!string.IsNullOrEmpty(entity.CustomerId))
                    {
                        var customer = _Erp_CustomerService.SingleOrDefault(entity.CustomerId, true);
                        customer.ArearsAmount = customer.ArearsAmount + arrearAmount.Value;
                        customer.ArearsStatus = customer.ArearsAmount > 0 ? 1 : 0;
                        _Erp_CustomerService.Update(customer, customer.GetPropInfoList(), customer.GetReferenceInfoList());
                    }

                }
                var amountAfter = amountBefore - amountChange;

                var flow = new Erp_FinanceFlow();
                flow.Create();
                flow.AmountChange = amountChange;
                flow.AmountBefore = amountBefore;
                flow.AmountAfter = amountAfter;
                flow.AccountId = entity.AccountId;
                flow.Type = "VoidCharge";

                _Erp_FinanceFlowService.Add(flow);

                account.BalanceAmount = amountAfter;
                if (account.BalanceAmount < 0)
                {
                    throw new Exception("账号[" + account.Name + "]余额不足!");
                }
                account.BalanceStatus = account.BalanceAmount > 0 ? 1 : 0;
                _Erp_AccountService.Update(account, account.GetPropInfoList(), account.GetReferenceInfoList());
            });
        }
    }
}
