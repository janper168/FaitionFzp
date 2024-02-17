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
    public class Erp_SalesOrderBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_SalesOrderService _Erp_SalesOrderService = null;
        private Erp_InventoryService _Erp_InventoryService = null;
        private Erp_InventoryFlowService _Erp_InventoryFlowService = null;
        private Erp_StockOutOrderService _Erp_StockOutOrderService = null;
        private Erp_StockOutGoodsService _Erp_StockOutGoodsService = null;
        private Erp_CustomerService _Erp_CustomerService = null;
        private Erp_SalesAccountService _Erp_SalesAccountService = null;
        private Erp_FinanceFlowService _Erp_FinanceFlowService = null;
        private Erp_AccountService _Erp_AccountService = null;
        private BaseBLL _baseBLL = new BaseBLL();
        private Erp_BillNoBLL _Erp_BillNoBLL = new Erp_BillNoBLL();

        public Erp_SalesOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_SalesOrderService = new Erp_SalesOrderService(_dbContext); 
            _Erp_InventoryService = new Erp_InventoryService(_dbContext);
            _Erp_InventoryFlowService = new Erp_InventoryFlowService(_dbContext);
            _Erp_StockOutOrderService = new Erp_StockOutOrderService(_dbContext);
            _Erp_StockOutGoodsService = new Erp_StockOutGoodsService(_dbContext);
            _Erp_CustomerService = new Erp_CustomerService(_dbContext);
            _Erp_SalesAccountService = new Erp_SalesAccountService(_dbContext);
            _Erp_FinanceFlowService = new Erp_FinanceFlowService(_dbContext);
            _Erp_AccountService = new Erp_AccountService(_dbContext);   

        }

        public void AddEntity(Erp_SalesOrder entity)
        {
            entity.Create();
            _Erp_SalesOrderService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_SalesOrder> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_SalesOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_SalesOrder entity)
        {
            _Erp_SalesOrderService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_SalesOrder> entities)
        {
           foreach (var e in entities)
            {
                _Erp_SalesOrderService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_SalesOrder entity)
        {
            _Erp_SalesOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_SalesOrder> entities)
        {
            _Erp_SalesOrderService.RemoveRange(entities);
        }


        public Erp_SalesOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_SalesOrderService.FindList(a => a.Erp_SalesOrderId == keyValue,a=>a.SalesGoodsList,a=>a.Customer,a=>a.Warehouse,a => a.Erp_SalesOrderId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_SalesOrder> GetEntities(Expression<Func<Erp_SalesOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_SalesOrderService.FindList(whereExpression, a => a.Erp_SalesOrderId, isSortAsc);
             return data;
        }

        public IList<Erp_SalesOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_SalesOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_SalesOrderService, pagination, sort, whereExpression,a=>a.Customer,a=>a.CreateUser);
        }

        public bool IsExists(Expression<Func<Erp_SalesOrder, bool>> whereExpression)
        {
            return _Erp_SalesOrderService.IsExists(whereExpression);
        }

        public void AddSalesOrder(Erp_SalesOrder entity)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);

            bs.Execute((db, td) =>
            {

                entity.Create();
                entity.EnableAutoStockOut = int.Parse(SysSettingsConfig.configs["AutoOutStock"]);

                //商品
                if (entity.SalesGoodsList != null && entity.SalesGoodsList.Count() > 0)
                {
                    foreach (var goods in entity.SalesGoodsList)
                    {
                        goods.Create();
                    }
                }

                //付款账户
                if (entity.SalesAccount != null)
                {
                    entity.SalesAccount.Create();
                    entity.SalesAccount.SalesOrderId = entity.Erp_SalesOrderId;
                }

                //销售出货订单
                _Erp_SalesOrderService.Add(entity);


                //自动入库
                if (entity.EnableAutoStockOut == 1)
                {
                    foreach (var goods in entity.SalesGoodsList)
                    {
                        var inventorys = _Erp_InventoryService.FindList(a =>
                            a.GoodsId == goods.GoodsId &&
                            entity.WarehouseId == a.WarehouseId,
                            a => a.Erp_InventoryId, true, true);

                        var inventory = new Erp_Inventory();
                        if (inventorys.Count == 1)
                        {
                            inventory = inventorys[0];
                            inventory.TotalQuantity -= goods.SalesQuantity;
                            inventory.StockStatus = 1;
                            _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());
                        }

                        var inventoryFlow = new Erp_InventoryFlow();
                        inventoryFlow.Create();

                        var quantity_before = inventory.TotalQuantity;
                        var quantity_change = goods.SalesQuantity; 
                        var quantity_after = quantity_before - quantity_change;

                        inventoryFlow.QuantityBefore = quantity_before;
                        inventoryFlow.QuantityAfter = quantity_after;
                        inventoryFlow.QuantityChange = quantity_change;

                        inventoryFlow.Type = "Sales";
                        inventoryFlow.SalesOrderId = entity.Erp_SalesOrderId;

                        _Erp_InventoryFlowService.Add(inventoryFlow);
                    }
                }
                else
                {
                    //创建销售出库通知单据
                    string nextBillNo = _Erp_BillNoBLL.GetNextBillNo("SO");
                    var stockOutOrder = new Erp_StockOutOrder();
                    stockOutOrder.Create();
                    stockOutOrder.Number = nextBillNo;
                    stockOutOrder.SalesOrderId = entity.Erp_SalesOrderId;
                    stockOutOrder.Type = "Sales";
                    stockOutOrder.WarehouseId = entity.WarehouseId;
                    stockOutOrder.TotalQuantity = 0;
                    stockOutOrder.RemainQuantity = entity.TotalQuantity.Value;
                    stockOutOrder.IsVoid = 0;
                    stockOutOrder.Completed = 0;
                    _Erp_StockOutOrderService.Add(stockOutOrder);

                    foreach (var goods in entity.SalesGoodsList)
                    {
                        var stockOutGoods = new Erp_StockOutGoods();
                        stockOutGoods.Create();
                        stockOutGoods.GoodsId = goods.GoodsId;
                        stockOutGoods.RemainQuantity = goods.SalesQuantity;
                        stockOutGoods.StockOutOrderId = stockOutOrder.Erp_StockOutOrderId;
                        _Erp_StockOutGoodsService.Add(stockOutGoods);
                    }
                }

                //客户欠款
                var customer = _Erp_CustomerService.SingleOrDefault(entity.CustomerId, true);

                if (entity.ArearsAmount != null)
                {
                    customer.ArearsAmount += entity.ArearsAmount.Value;
                    customer.ArearsStatus = customer.ArearsAmount > 0 ? 1 : 0;
                }
                _Erp_CustomerService.Update(customer, customer.GetPropInfoList(), customer.GetReferenceInfoList());

                //同步账户和流水
                if (entity.CollectionAmount > 0)
                {
                    var account = _Erp_SalesAccountService.FindList(a => a.AccountId == entity.SalesAccount.AccountId, a => a.Account, a => a.AccountId, true)[0];
                    if (account != null)
                    {
                        var amount_before = account.Account.BalanceAmount;
                        var amount_change = entity.SalesAccount.CollectionAmount;
                        var amount_after = amount_before + amount_change;

                        account.Account.BalanceAmount = amount_after;
                        if (account.Account.BalanceAmount < 0)
                        {
                            throw new Exception("结算账户【" + account.Account.Name + "】余额不足！");
                        }
                        account.Account.BalanceStatus = account.Account.BalanceAmount > 0 ? 1 : 0;

                        var flowEnitity = new Erp_FinanceFlow();
                        flowEnitity.Create();
                        flowEnitity.AccountId = account.AccountId;
                        flowEnitity.AmountBefore = amount_before;
                        flowEnitity.AmountAfter = amount_after;
                        flowEnitity.AmountChange = amount_change;
                        flowEnitity.SalesOrderId = entity.Erp_SalesOrderId;
                        flowEnitity.Type = "Sales";

                        _Erp_FinanceFlowService.Add(flowEnitity);
                        _Erp_AccountService.Update(account.Account, account.Account.GetPropInfoList(), account.Account.GetReferenceInfoList());

                    }
                }
            });
        }

        public void SetVoid(string Erp_SalesOrderId)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, td) =>
            {
                var entity = _Erp_SalesOrderService.FindList(a => a.Erp_SalesOrderId == Erp_SalesOrderId,
                    a => a.Erp_SalesOrderId, true, true)[0];

                if (entity != null)
                {
                    if (entity.IsVoid == 1)
                    {
                        throw new Exception("销售订单【" + entity.Number + "】已经作废，无法操作！");
                    }
                    entity.IsVoid = 1;
                    _Erp_SalesOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

                    if (entity.EnableAutoStockOut == 1)
                    {
                        var stockOutOrder =
                            _Erp_StockOutOrderService.FirstOrDefault(a => a.SalesOrderId == entity.Erp_SalesOrderId, true);

                        if (stockOutOrder.TotalQuantity != stockOutOrder.RemainQuantity)
                        {
                            throw new Exception("销售订单【" + entity.Number + "】无法作废,已存在入库记录！");
                        }

                        stockOutOrder.IsVoid = 1;
                        _Erp_StockOutOrderService.Update(stockOutOrder, stockOutOrder.GetPropInfoList(), stockOutOrder.GetReferenceInfoList());
                    }

                    //客户欠款
                    var customer = _Erp_CustomerService.SingleOrDefault(entity.CustomerId, true);

                    if (entity.ArearsAmount != null)
                    {
                        customer.ArearsAmount -= entity.ArearsAmount.Value;
                        customer.ArearsStatus = customer.ArearsAmount > 0 ? 1 : 0;
                    }
                    _Erp_CustomerService.Update(customer, customer.GetPropInfoList(), customer.GetReferenceInfoList());

                    //同步账户和流水
                    if (entity.CollectionAmount > 0)
                    {
                        var account = _Erp_SalesAccountService.FindList(a => a.AccountId == entity.SalesAccount.AccountId, a => a.Account, a => a.AccountId, true)[0];
                        if (account != null)
                        {
                            var amount_before = account.Account.BalanceAmount;
                            var amount_change = entity.SalesAccount.CollectionAmount;
                            var amount_after = amount_before - amount_change;

                            account.Account.BalanceAmount = amount_after;
                            if (account.Account.BalanceAmount < 0)
                            {
                                throw new Exception("结算账户【" + account.Account.Name + "】余额不足！");
                            }
                            account.Account.BalanceStatus = account.Account.BalanceAmount > 0 ? 1 : 0;

                            var flowEnitity = new Erp_FinanceFlow();
                            flowEnitity.Create();
                            flowEnitity.AccountId = account.AccountId;
                            flowEnitity.AmountBefore = amount_before;
                            flowEnitity.AmountAfter = amount_after;
                            flowEnitity.AmountChange = amount_change;
                            flowEnitity.SalesOrderId = entity.Erp_SalesOrderId;
                            flowEnitity.Type = "VoidSales";

                            _Erp_FinanceFlowService.Add(flowEnitity);
                            _Erp_AccountService.Update(account.Account, account.Account.GetPropInfoList(), account.Account.GetReferenceInfoList());

                        }
                    }
                }
            });
        }
    }
}
