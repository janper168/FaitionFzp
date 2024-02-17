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
    public class Erp_SalesReturnOrderBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_SalesReturnOrderService _Erp_SalesReturnOrderService = null;
        private Erp_SuppilerService _Erp_SuppilerService = null;
        private Erp_FinanceFlowService _Erp_FinanceFlowService = null;
        private Erp_PurchaseAccountService _Erp_PurchaseAccountService = null;
        private Erp_SalesReturnAccountService _Erp_SalesReturnAccountService = null;
        private Erp_AccountService _Erp_AccountService = null;
        private Erp_InventoryService _Erp_InventoryService = null;
        private Erp_InventoryFlowService _Erp_InventoryFlowService = null;
        private Erp_StockInOrderService _Erp_StockInOrderService = null;
        private Erp_StockInGoodsService _Erp_StockInGoodsService = null;
        private Erp_CustomerService _Erp_CustomerService = null;
        private Erp_BillNoBLL _Erp_BillNoBLL = new Erp_BillNoBLL();
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_SalesReturnOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_SalesReturnOrderService = new Erp_SalesReturnOrderService(_dbContext);
            _Erp_SuppilerService = new Erp_SuppilerService(_dbContext);
            _Erp_FinanceFlowService = new Erp_FinanceFlowService(_dbContext);
            _Erp_PurchaseAccountService = new Erp_PurchaseAccountService(_dbContext);
            _Erp_AccountService = new Erp_AccountService(_dbContext);
            _Erp_InventoryService =new Erp_InventoryService(_dbContext);
            _Erp_InventoryFlowService = new Erp_InventoryFlowService(_dbContext);
            _Erp_StockInOrderService = new Erp_StockInOrderService(_dbContext);
            _Erp_StockInGoodsService = new Erp_StockInGoodsService(_dbContext);
            _Erp_CustomerService = new Erp_CustomerService(_dbContext);

            _Erp_SalesReturnAccountService = new Erp_SalesReturnAccountService(_dbContext); 
        }


        public void AddEntity(Erp_SalesReturnOrder entity)
        {
            entity.Create();
            _Erp_SalesReturnOrderService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_SalesReturnOrder> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_SalesReturnOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_SalesReturnOrder entity)
        {
            _Erp_SalesReturnOrderService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_SalesReturnOrder> entities)
        {
           foreach (var e in entities)
            {
                _Erp_SalesReturnOrderService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_SalesReturnOrder entity)
        {
            _Erp_SalesReturnOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_SalesReturnOrder> entities)
        {
            _Erp_SalesReturnOrderService.RemoveRange(entities);
        }

        public Erp_SalesReturnOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_SalesReturnOrderService.FindList(a => a.Erp_SalesReturnOrderId == keyValue,a=>a.SalesOrder,a=>a.SalesReturnGoodsList, a => a.Erp_SalesReturnOrderId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_SalesReturnOrder> GetEntities(Expression<Func<Erp_SalesReturnOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_SalesReturnOrderService.FindList(whereExpression, a => a.Erp_SalesReturnOrderId, isSortAsc);
             return data;
        }

        public IList<Erp_SalesReturnOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_SalesReturnOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_SalesReturnOrderService, pagination, sort, whereExpression,a=>a.SalesOrder,a=>a.CreateUser,a=>a.Customer);
        }

        public bool IsExists(Expression<Func<Erp_SalesReturnOrder, bool>> whereExpression)
        {
            return _Erp_SalesReturnOrderService.IsExists(whereExpression);
        }

   
        //退货订单
        public void AddSalesReturnOrder(Erp_SalesReturnOrder entity)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);

            bs.Execute((db, td) =>
            {

                entity.Create();
                entity.EnableAutoStockIn = int.Parse(SysSettingsConfig.configs["AutoInStock"]);

                //商品
                if (entity.SalesReturnGoodsList != null && entity.SalesReturnGoodsList.Count() > 0)
                {
                    foreach (var goods in entity.SalesReturnGoodsList)
                    {
                        goods.Create();                       
                    }
                }

                //付款账户（还）
                if (entity.SalesReturnAccount != null)
                {
                    entity.SalesReturnAccount.Create();
                    entity.SalesReturnAccount.SalesReturnOrderId = entity.Erp_SalesReturnOrderId;
                }

                //采购订单
                _Erp_SalesReturnOrderService.Add(entity);

                //自动入库
                if (entity.EnableAutoStockIn == 1)
                {
                    foreach (var goods in entity.SalesReturnGoodsList)
                    {
                        var inventorys = _Erp_InventoryService.FindList(a =>
                            a.GoodsId == goods.GoodsId &&
                            entity.WarehouseId == a.WarehouseId,
                            a => a.Erp_InventoryId, true, true);

                        var inventory = new Erp_Inventory();
                        if (inventorys == null || inventorys.Count <= 0)
                        {
                            inventory.Create();
                            inventory.GoodsId = goods.GoodsId;
                            inventory.WarehouseId = entity.WarehouseId;
                            inventory.InitialQuantity = 0;
                            inventory.TotalQuantity = goods.ReturnQuantity;
                            inventory.StockStatus = 1;
                            _Erp_InventoryService.Add(inventory);
                        }
                        else if (inventorys.Count == 1)
                        {
                            inventory = inventorys[0];
                            inventory.TotalQuantity += goods.ReturnQuantity;
                            inventory.StockStatus = 1;
                            _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());
                        }

                        var inventoryFlow = new Erp_InventoryFlow();
                        inventoryFlow.Create();

                        var quantity_before = inventory.TotalQuantity;
                        var quantity_change = goods.ReturnQuantity;
                        var quantity_after = quantity_before + quantity_change;

                        inventoryFlow.QuantityBefore = quantity_before;
                        inventoryFlow.QuantityAfter = quantity_after;
                        inventoryFlow.QuantityChange = quantity_change;

                        inventoryFlow.Type = "SalesReturn";
                        inventoryFlow.SalesReturnOrderId = entity.Erp_SalesReturnOrderId;

                        _Erp_InventoryFlowService.Add(inventoryFlow);
                    }
                }
                else
                {
                    //创建入库通知单据
                    string nextBillNo = _Erp_BillNoBLL.GetNextBillNo("SI");
                    var stockInOrder = new Erp_StockInOrder();
                    stockInOrder.Create();
                    stockInOrder.Number = nextBillNo;
                    stockInOrder.SalesReturnOrderId = entity.Erp_SalesReturnOrderId;
                    stockInOrder.Type = "SalesReturn";
                    stockInOrder.WarehouseId = entity.WarehouseId;
                    stockInOrder.TotalQuantity = 0;// entity.TotalQuantity.Value;
                    stockInOrder.RemainQuantity = entity.TotalQuantity.Value;
                    stockInOrder.IsVoid = 0;
                    stockInOrder.Completed = 0;
                    _Erp_StockInOrderService.Add(stockInOrder);

                    foreach (var goods in entity.SalesReturnGoodsList)
                    {
                        var stockInGood = new Erp_StockInGoods();
                        stockInGood.Create();
                        stockInGood.GoodsId = goods.GoodsId;
                        stockInGood.RemainQuantity = goods.ReturnQuantity;
                        stockInGood.StockInOrderId = stockInOrder.Erp_StockInOrderId;
                        _Erp_StockInGoodsService.Add(stockInGood);
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
                if (entity.PaymentAmount > 0)
                {
                    var account = _Erp_SalesReturnAccountService.FindList(a => a.AccountId == entity.SalesReturnAccount.AccountId, a => a.Account, a => a.AccountId, true)[0];
                    if (account != null)
                    {
                        var amount_before = account.Account.BalanceAmount;
                        var amount_change = entity.PaymentAmount;
                        var amount_after = amount_before - amount_change;

                        account.Account.BalanceAmount = amount_after.Value;
                        if (account.Account.BalanceAmount < 0)
                        {
                            throw new Exception("结算账户【" + account.Account.Name + "】余额不足！");
                        }
                        account.Account.BalanceStatus = account.Account.BalanceAmount > 0 ? 1 : 0;

                        var flowEnitity = new Erp_FinanceFlow();
                        flowEnitity.Create();
                        flowEnitity.AccountId = account.AccountId;
                        flowEnitity.AmountBefore = amount_before;
                        flowEnitity.AmountAfter = amount_after.Value;
                        flowEnitity.AmountChange = amount_change.Value;
                        flowEnitity.SalesReturnOrderId = entity.Erp_SalesReturnOrderId;
                        flowEnitity.Type = "SalesReturn";

                        _Erp_FinanceFlowService.Add(flowEnitity);
                        _Erp_AccountService.Update(account.Account, account.Account.GetPropInfoList(), account.Account.GetReferenceInfoList());

                    }
                }
            });
        }

        public void SetVoid(string Erp_PurchaseOrderId)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, td) =>
            {
                var entity = _Erp_SalesReturnOrderService.FindList(a => a.Erp_SalesReturnOrderId == Erp_PurchaseOrderId,
                    a => a.Erp_SalesReturnOrderId, true, true)[0];

                if (entity != null)
                {
                    if (entity.IsVoid == 1)
                    {
                        throw new Exception("退货单据【" + entity.Number + "】已经作废，无法操作！");
                    }
                    entity.IsVoid = 1;
                    _Erp_SalesReturnOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

                    if (entity.EnableAutoStockIn == 1)
                    {
                        var stockInOrder =
                            _Erp_StockInOrderService.FirstOrDefault(a => a.SalesReturnOrderId == entity.Erp_SalesReturnOrderId, true);

                        if (stockInOrder.TotalQuantity != stockInOrder.RemainQuantity)
                        {
                            throw new Exception("退货单据【" + entity.Number + "】无法作废,已存在入库记录！");
                        }

                        stockInOrder.IsVoid = 1;
                        _Erp_StockInOrderService.Update(stockInOrder, stockInOrder.GetPropInfoList(), stockInOrder.GetReferenceInfoList());
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
                    if (entity.PaymentAmount > 0)
                    {
                        var account = _Erp_SalesReturnAccountService.FindList(a => a.AccountId == entity.SalesReturnAccount.AccountId, a => a.Account, a => a.AccountId, true)[0];
                        if (account != null)
                        {
                            var amount_before = account.Account.BalanceAmount;
                            var amount_change = entity.SalesReturnAccount.PaymentAmount;
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
                            flowEnitity.SalesReturnOrderId = entity.Erp_SalesReturnOrderId;
                            flowEnitity.Type = "VoidSalesReturn";

                            _Erp_FinanceFlowService.Add(flowEnitity);
                            _Erp_AccountService.Update(account.Account, account.Account.GetPropInfoList(), account.Account.GetReferenceInfoList());

                        }
                    }
                }
            });
        }

    }
}
