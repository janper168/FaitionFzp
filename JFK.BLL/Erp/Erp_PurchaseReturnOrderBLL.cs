using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using NPOI.POIFS.FileSystem;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class Erp_PurchaseReturnOrderBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_PurchaseReturnOrderService _Erp_PurchaseReturnOrderService = null;
        private Erp_PurchaseReturnGoodsService _Erp_PurchaseReturnGoodsService = null;
        private Erp_PurchaseOrderService _Erp_PurchaseOrderService = null;
        private Erp_PurchaseGoodsService _Erp_PurchaseGoodsService = null;
        private Erp_InventoryService _Erp_InventoryService = null;
        private Erp_InventoryFlowService _Erp_InventoryFlowService = null;
        private Erp_StockOutOrderService _Erp_StockOutOrderService = null;
        private Erp_StockOutGoodsService _Erp_StockOutGoodsService = null;
        private Erp_SuppilerService _Erp_SupplilerService = null;
        private Erp_PurchaseReturnAccountService _Erp_PurchaseReturnAccountService = null;
        private Erp_FinanceFlowService _Erp_FinanceFlowService = null;
        private Erp_AccountService _Erp_AccountService = null;
        private Erp_BillNoBLL _Erp_BillNoBLL = new Erp_BillNoBLL();
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_PurchaseReturnOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_PurchaseReturnOrderService = new Erp_PurchaseReturnOrderService(_dbContext);
            _Erp_PurchaseReturnGoodsService = new Erp_PurchaseReturnGoodsService(_dbContext);
            _Erp_PurchaseOrderService = new Erp_PurchaseOrderService(_dbContext);
            _Erp_PurchaseGoodsService = new Erp_PurchaseGoodsService(_dbContext);
            _Erp_InventoryService = new Erp_InventoryService(_dbContext);
            _Erp_InventoryFlowService = new Erp_InventoryFlowService(_dbContext); 
            _Erp_StockOutOrderService = new Erp_StockOutOrderService(_dbContext);
            _Erp_StockOutGoodsService = new Erp_StockOutGoodsService(_dbContext);
            _Erp_SupplilerService = new Erp_SuppilerService(_dbContext);
            _Erp_PurchaseReturnAccountService = new Erp_PurchaseReturnAccountService(_dbContext);
            _Erp_FinanceFlowService = new Erp_FinanceFlowService(_dbContext);
            _Erp_AccountService = new Erp_AccountService(_dbContext);
         }

        public void AddEntity(Erp_PurchaseReturnOrder entity)
        {
            entity.Create();
            _Erp_PurchaseReturnOrderService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_PurchaseReturnOrder> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_PurchaseReturnOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_PurchaseReturnOrder entity)
        {
            _Erp_PurchaseReturnOrderService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_PurchaseReturnOrder> entities)
        {
           foreach (var e in entities)
            {
                _Erp_PurchaseReturnOrderService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_PurchaseReturnOrder entity)
        {
            _Erp_PurchaseReturnOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_PurchaseReturnOrder> entities)
        {
            _Erp_PurchaseReturnOrderService.RemoveRange(entities);
        }

        public Erp_PurchaseReturnOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_PurchaseReturnOrderService.FindList(a => a.Erp_PurchaseReturnOrderId == keyValue, a => a.Suppiler, a => a.Warehouse, a => a.PurchaseReturnGoodsList, a => a.Erp_PurchaseReturnOrderId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_PurchaseReturnOrder> GetEntities(Expression<Func<Erp_PurchaseReturnOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_PurchaseReturnOrderService.FindList(whereExpression, a => a.Erp_PurchaseReturnOrderId, isSortAsc);
             return data;
        }

        public IList<Erp_PurchaseReturnOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_PurchaseReturnOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_PurchaseReturnOrderService, pagination, sort, whereExpression,a=>a.PurchaseOrder,a=>a.CreateUser,a=>a.Suppiler,a=>a.Warehouse);
        }

        public bool IsExists(Expression<Func<Erp_PurchaseReturnOrder, bool>> whereExpression)
        {
            return _Erp_PurchaseReturnOrderService.IsExists(whereExpression);
        }

        //采购退货单
        public void AddPurchaseReturnOrder(Erp_PurchaseReturnOrder entity)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);

            bs.Execute((db, td) =>
            {

                entity.Create();
                entity.EnableAutoStockOut = int.Parse(SysSettingsConfig.configs["AutoOutStock"]);

                //商品
                if (entity.PurchaseReturnGoodsList != null && entity.PurchaseReturnGoodsList.Count() > 0)
                {
                    foreach (var goods in entity.PurchaseReturnGoodsList)
                    {
                        goods.Create();
                    }
                }

                //付款账户
                if (entity.PurchaseReturnAccount != null)
                {
                    entity.PurchaseReturnAccount.Create();
                    entity.PurchaseReturnAccount.PurchaseReturnOrderId = entity.Erp_PurchaseReturnOrderId;
                }

                //采购退货订单
                _Erp_PurchaseReturnOrderService.Add(entity);
                var total_return_quantity = 0;
                var total_return_amount = 0.0m;

                foreach (var item in entity.PurchaseReturnGoodsList)
                {
                    var purchaseItem = _Erp_PurchaseGoodsService.FirstOrDefault(a => a.GoodsId == item.GoodsId &&
                        a.PurchaseOrderId == item.PurchaseReturnOrder.PurchaseOrderId,true);

                    if (purchaseItem!= null) 
                    {
                        purchaseItem.ReturnQuantity += item.ReturnQuantity;
                        total_return_quantity += item.ReturnQuantity;
                        total_return_amount += item.TotalAmount;
                    }
                    _Erp_PurchaseGoodsService.Update(purchaseItem, purchaseItem.GetPropInfoList(), purchaseItem.GetReferenceInfoList());
                }

                //更新收款金额
                //entity.CollectionAmount = total_return_amount;
                //entity.ArearsAmount = entity.CollectionAmount - entity.otherAmount;

                //_Erp_PurchaseReturnOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

                //自动入库
                if (entity.EnableAutoStockOut == 1)
                {
                    foreach (var goods in entity.PurchaseReturnGoodsList)
                    {
                        var inventorys = _Erp_InventoryService.FindList(a =>
                            a.GoodsId == goods.GoodsId &&
                            entity.WarehouseId == a.WarehouseId,
                            a => a.Erp_InventoryId, true, true);

                        var inventory = new Erp_Inventory();
                        if (inventorys.Count == 1)
                        {
                            inventory = inventorys[0];
                            inventory.TotalQuantity -= goods.ReturnQuantity;
                            inventory.StockStatus = 1;
                            _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());
                        }

                        var inventoryFlow = new Erp_InventoryFlow();
                        inventoryFlow.Create();

                        var quantity_before = inventory.TotalQuantity;
                        var quantity_change = goods.ReturnQuantity;
                        var quantity_after = quantity_before - quantity_change;

                        inventoryFlow.QuantityBefore = quantity_before;
                        inventoryFlow.QuantityAfter = quantity_after;
                        inventoryFlow.QuantityChange = quantity_change;

                        inventoryFlow.Type = "PurchaseReturn";
                        inventoryFlow.PurchaseReturnOrderId = entity.Erp_PurchaseReturnOrderId;

                        _Erp_InventoryFlowService.Add(inventoryFlow);
                    }
                }
                else
                {
                    //创建出库通知单据
                    string nextBillNo = _Erp_BillNoBLL.GetNextBillNo("SO");
                    var stockOutOrder = new Erp_StockOutOrder();
                    stockOutOrder.Create();
                    stockOutOrder.Number = nextBillNo;
                    stockOutOrder.PurchaseReturnOrderId = entity.Erp_PurchaseReturnOrderId;
                    stockOutOrder.Type = "PurchaseReturn";
                    stockOutOrder.WarehouseId = entity.WarehouseId;
                    stockOutOrder.TotalQuantity = 0;
                    stockOutOrder.RemainQuantity = entity.TotalQuantity.Value;
                    stockOutOrder.IsVoid = 0;
                    stockOutOrder.Completed = 0;
                    _Erp_StockOutOrderService.Add(stockOutOrder);

                    foreach (var goods in entity.PurchaseReturnGoodsList)
                    {
                        var stockOutGoods = new Erp_StockOutGoods();
                        stockOutGoods.Create();
                        stockOutGoods.GoodsId = goods.GoodsId;
                        stockOutGoods.RemainQuantity = goods.ReturnQuantity;
                        stockOutGoods.StockOutOrderId = stockOutOrder.Erp_StockOutOrderId;
                        _Erp_StockOutGoodsService.Add(stockOutGoods);
                    }
                }

                //供应商欠款
                var suppiler = _Erp_SupplilerService.SingleOrDefault(entity.SuppilerId, true);

                if (entity.ArearsAmount != null)
                {
                    suppiler.ArearsAmount -= entity.ArearsAmount.Value;
                    suppiler.ArearsStatus = suppiler.ArearsAmount > 0 ? 1 : 0;
                }
                _Erp_SupplilerService.Update(suppiler, suppiler.GetPropInfoList(), suppiler.GetReferenceInfoList());

                //同步账户和流水
                if (entity.CollectionAmount > 0)
                {
                    var account = _Erp_PurchaseReturnAccountService.FindList(a => a.AccountId == entity.PurchaseReturnAccount.AccountId, a => a.Account, a => a.AccountId, true)[0];
                    if (account != null)
                    {
                        var amount_before = account.Account.BalanceAmount;
                        var amount_change = entity.PurchaseReturnAccount.CollectionAmount;
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
                        flowEnitity.PurchaseReturnOrderId = entity.Erp_PurchaseReturnOrderId;
                        flowEnitity.Type = "PurchaseReturn";

                        _Erp_FinanceFlowService.Add(flowEnitity);
                        _Erp_AccountService.Update(account.Account, account.Account.GetPropInfoList(), account.Account.GetReferenceInfoList());

                    }
                }
            });
        }

        public void SetVoid(string Erp_PurchaseReturnOrderId)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, td) =>
            {
                var entity = _Erp_PurchaseReturnOrderService.FindList(a => a.Erp_PurchaseReturnOrderId == Erp_PurchaseReturnOrderId,
                    a => a.Erp_PurchaseReturnOrderId, true, true)[0];

                if (entity != null)
                {
                    if (entity.IsVoid == 1)
                    {
                        throw new Exception("采购单据【" + entity.Number + "】已经作废，无法操作！");
                    }
                    entity.IsVoid = 1;
                    _Erp_PurchaseReturnOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

                    if (entity.EnableAutoStockOut == 1)
                    {
                        var stockOutOrder =
                            _Erp_StockOutOrderService.FirstOrDefault(a => a.PurchaseReturnOrderId == entity.Erp_PurchaseReturnOrderId, true);

                        if (stockOutOrder.TotalQuantity != stockOutOrder.RemainQuantity)
                        {
                            throw new Exception("采购单据【" + entity.Number + "】无法作废,已存在入库记录！");
                        }

                        stockOutOrder.IsVoid = 1;
                        _Erp_StockOutOrderService.Update(stockOutOrder, stockOutOrder.GetPropInfoList(), stockOutOrder.GetReferenceInfoList());
                    }

                    //供应商欠款
                    var suppiler = _Erp_SupplilerService.SingleOrDefault(entity.SuppilerId, true);

                    if (entity.ArearsAmount != null)
                    {
                        suppiler.ArearsAmount += entity.ArearsAmount.Value;
                        suppiler.ArearsStatus = suppiler.ArearsAmount > 0 ? 1 : 0;
                    }
                    _Erp_SupplilerService.Update(suppiler, suppiler.GetPropInfoList(), suppiler.GetReferenceInfoList());

                    //同步账户和流水
                    if (entity.CollectionAmount > 0)
                    {
                        var account = _Erp_PurchaseReturnAccountService.FindList(a => a.AccountId == entity.PurchaseReturnAccount.AccountId, a => a.Account, a => a.AccountId, true)[0];
                        if (account != null)
                        {
                            var amount_before = account.Account.BalanceAmount;
                            var amount_change = entity.PurchaseReturnAccount.CollectionAmount;
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
                            flowEnitity.PurchaseReturnOrderId = entity.Erp_PurchaseReturnOrderId;
                            flowEnitity.Type = "VoidPurchaseReturn";

                            _Erp_FinanceFlowService.Add(flowEnitity);
                            _Erp_AccountService.Update(account.Account, account.Account.GetPropInfoList(), account.Account.GetReferenceInfoList());

                        }
                    }
                }
            });
        }
    }
}
