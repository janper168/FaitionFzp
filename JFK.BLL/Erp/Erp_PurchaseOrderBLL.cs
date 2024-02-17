using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using Microsoft.EntityFrameworkCore.Internal;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static NPOI.HSSF.Util.HSSFColor;

namespace JKF.BLL
{
    public class Erp_PurchaseOrderBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_PurchaseOrderService _Erp_PurchaseOrderService = null;
        private BaseBLL _baseBLL = new BaseBLL();
        private Erp_SuppilerService _Erp_SuppilerService = null;
        private Erp_FinanceFlowService _Erp_FinanceFlowService = null;
        private Erp_PurchaseAccountService _Erp_PurchaseAccountService = null;
        private Erp_AccountService _Erp_AccountService = null;
        private Erp_InventoryService _Erp_InventoryService = null;
        private Erp_InventoryFlowService _Erp_InventoryFlowService = null;
        private Erp_StockInOrderService _Erp_StockInOrderService = null;
        private Erp_StockInGoodsService _Erp_StockInGoodsService = null;
        private Erp_BillNoBLL _Erp_BillNoBLL = new Erp_BillNoBLL();
        public Erp_PurchaseOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_SuppilerService = new Erp_SuppilerService(_dbContext);
            _Erp_PurchaseOrderService = new Erp_PurchaseOrderService(_dbContext);
            _Erp_PurchaseAccountService = new Erp_PurchaseAccountService(_dbContext);
            _Erp_FinanceFlowService = new Erp_FinanceFlowService(_dbContext);
            _Erp_AccountService = new Erp_AccountService(_dbContext);
            _Erp_InventoryService = new Erp_InventoryService(_dbContext);
            _Erp_InventoryFlowService = new Erp_InventoryFlowService(_dbContext);
            _Erp_StockInGoodsService = new Erp_StockInGoodsService(_dbContext);
            _Erp_StockInOrderService = new Erp_StockInOrderService(_dbContext);
        }

        public void AddEntity(Erp_PurchaseOrder entity)
        {
            entity.Create();
            _Erp_PurchaseOrderService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_PurchaseOrder> entities)
        {
            foreach (var e in entities)
            {
                e.Create();
            }
            _Erp_PurchaseOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_PurchaseOrder entity)
        {
            _Erp_PurchaseOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_PurchaseOrder> entities)
        {
            foreach (var e in entities)
            {
                _Erp_PurchaseOrderService.Update(e, e.GetPropInfoList(), e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_PurchaseOrder entity)
        {
            _Erp_PurchaseOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_PurchaseOrder> entities)
        {
            _Erp_PurchaseOrderService.RemoveRange(entities);
        }

        public Erp_PurchaseOrder GetEntity(Expression<Func<Erp_PurchaseOrder, bool>> whereExpression, bool beTraced = false)
        {
            var entity = _Erp_PurchaseOrderService.FindList(whereExpression, a => a.Suppiler, a => a.Warehouse, a => a.PurchaseGoodsList, a => a.Erp_PurchaseOrderId, true, beTraced)[0];
            return entity;
        }

        public Erp_PurchaseOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_PurchaseOrderService.FindList(a => a.Erp_PurchaseOrderId == keyValue,a=>a.Suppiler,a=>a.Warehouse, a => a.PurchaseGoodsList, a => a.Erp_PurchaseOrderId, true, beTraced)[0];
            return entity;
        }

        public IList<Erp_PurchaseOrder> GetEntities(Expression<Func<Erp_PurchaseOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_PurchaseOrderService.FindList(whereExpression, a => a.Erp_PurchaseOrderId, isSortAsc);
            return data;
        }

        public IList<Erp_PurchaseOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_PurchaseOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_PurchaseOrderService, pagination, sort, whereExpression, a => a.CreateUser, a => a.Suppiler);
        }

        public bool IsExists(Expression<Func<Erp_PurchaseOrder, bool>> whereExpression)
        {
            return _Erp_PurchaseOrderService.IsExists(whereExpression);
        }

        //采购订单
        public void AddPurchaseOrder(Erp_PurchaseOrder entity)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);

            bs.Execute((db, td) =>
            {

                entity.Create();
                entity.EnableAutoStockIn = int.Parse(SysSettingsConfig.configs["AutoInStock"]);

                //商品
                if (entity.PurchaseGoodsList != null && entity.PurchaseGoodsList.Count() > 0)
                {
                    foreach (var goods in entity.PurchaseGoodsList)
                    {
                        goods.Create();
                    }
                }

                //付款账户
                if (entity.PurchaseAccount != null)
                {
                    entity.PurchaseAccount.Create();
                    entity.PurchaseAccount.PurchaseOrderId = entity.Erp_PurchaseOrderId;
                }

                //采购订单
                _Erp_PurchaseOrderService.Add(entity);

                //自动入库
                if (entity.EnableAutoStockIn == 1)
                {
                    foreach (var goods in entity.PurchaseGoodsList)
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
                            inventory.TotalQuantity = goods.PurchaseQuantity;
                            inventory.StockStatus = 1;
                            _Erp_InventoryService.Add(inventory);
                        }
                        else if (inventorys.Count == 1)
                        {
                            inventory = inventorys[0];
                            inventory.TotalQuantity += goods.PurchaseQuantity;
                            inventory.StockStatus = 1;
                            _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());
                        }

                        var inventoryFlow = new Erp_InventoryFlow();
                        inventoryFlow.Create();

                        var quantity_before = inventory.TotalQuantity;
                        var quantity_change = goods.PurchaseQuantity;
                        var quantity_after = quantity_before + quantity_change;

                        inventoryFlow.QuantityBefore = quantity_before;
                        inventoryFlow.QuantityAfter = quantity_after;
                        inventoryFlow.QuantityChange = goods.PurchaseQuantity;

                        inventoryFlow.Type = "purchase";
                        inventoryFlow.PurchaseOrderId = entity.Erp_PurchaseOrderId;

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
                    stockInOrder.PurchaseOrderId = entity.Erp_PurchaseOrderId;
                    stockInOrder.Type = "Purchase";
                    stockInOrder.WarehouseId = entity.WarehouseId;
                    stockInOrder.TotalQuantity = 0;// entity.TotalQuantity.Value;
                    stockInOrder.RemainQuantity = entity.TotalQuantity.Value;
                    stockInOrder.IsVoid = 0;
                    stockInOrder.Completed = 0;
                    _Erp_StockInOrderService.Add(stockInOrder);

                    foreach (var goods in entity.PurchaseGoodsList)
                    {
                        var stockInGood = new Erp_StockInGoods();
                        stockInGood.Create();
                        stockInGood.GoodsId = goods.GoodsId;
                        stockInGood.RemainQuantity = goods.PurchaseQuantity;
                        stockInGood.StockInOrderId = stockInOrder.Erp_StockInOrderId;
                        _Erp_StockInGoodsService.Add(stockInGood);
                    }
                }

                //供应商欠款
                var suppiler = _Erp_SuppilerService.SingleOrDefault(entity.SuppilerId, true);

                if (entity.ArearsAmount != null)
                {
                    suppiler.ArearsAmount += entity.ArearsAmount.Value;
                    suppiler.ArearsStatus = suppiler.ArearsAmount > 0 ? 1 : 0;
                }
                _Erp_SuppilerService.Update(suppiler, suppiler.GetPropInfoList(), suppiler.GetReferenceInfoList());

                //同步账户和流水
                if (entity.PaymentAmount > 0)
                {
                    var account = _Erp_PurchaseAccountService.FindList(a => a.AccountId == entity.PurchaseAccount.AccountId, a => a.Account, a => a.AccountId, true)[0];
                    if (account != null)
                    {
                        var amount_before = account.Account.BalanceAmount;
                        var amount_change = entity.PurchaseAccount.PaymentAmount;
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
                        flowEnitity.PurchaseOrderId = entity.Erp_PurchaseOrderId;
                        flowEnitity.Type = "Purchase";

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
                var entity = _Erp_PurchaseOrderService.FindList(a => a.Erp_PurchaseOrderId == Erp_PurchaseOrderId,
                    a => a.Erp_PurchaseOrderId, true, true)[0];

                if (entity != null)
                {
                    if (entity.IsVoid == 1)
                    {
                        throw new Exception("采购单据【" + entity.Number + "】已经作废，无法操作！");
                    }
                    entity.IsVoid = 1;
                    _Erp_PurchaseOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

                    if (entity.EnableAutoStockIn == 1)
                    {
                        var stockInOrder =
                            _Erp_StockInOrderService.FirstOrDefault(a => a.PurchaseOrderId == entity.Erp_PurchaseOrderId,true);

                        if (stockInOrder.TotalQuantity != stockInOrder.RemainQuantity)
                        {
                            throw new Exception("采购单据【" + entity.Number + "】无法作废,已存在入库记录！");
                        }

                        stockInOrder.IsVoid = 1;
                        _Erp_StockInOrderService.Update(stockInOrder, stockInOrder.GetPropInfoList(), stockInOrder.GetReferenceInfoList());
                    }

                    //供应商欠款
                    var suppiler = _Erp_SuppilerService.SingleOrDefault(entity.SuppilerId, true);

                    if (entity.ArearsAmount != null)
                    {
                        suppiler.ArearsAmount -= entity.ArearsAmount.Value;
                        suppiler.ArearsStatus = suppiler.ArearsAmount > 0 ? 1 : 0;
                    }
                    _Erp_SuppilerService.Update(suppiler, suppiler.GetPropInfoList(), suppiler.GetReferenceInfoList());

                    //同步账户和流水
                    if (entity.PaymentAmount > 0)
                    {
                        var account = _Erp_PurchaseAccountService.FindList(a => a.AccountId == entity.PurchaseAccount.AccountId, a => a.Account, a => a.AccountId, true)[0];
                        if (account != null)
                        {
                            var amount_before = account.Account.BalanceAmount;
                            var amount_change = entity.PurchaseAccount.PaymentAmount;
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
                            flowEnitity.PurchaseOrderId = entity.Erp_PurchaseOrderId;
                            flowEnitity.Type = "VoidPurchase";

                            _Erp_FinanceFlowService.Add(flowEnitity);
                            _Erp_AccountService.Update(account.Account, account.Account.GetPropInfoList(), account.Account.GetReferenceInfoList());

                        }
                    }
                }
            });
        }
    }
}
