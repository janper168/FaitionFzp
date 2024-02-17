using Castle.Core.Internal;
using JKF.BLL.Base;
using JKF.BLL.Models;
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
    public class Erp_StockOutOrderBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockOutOrderService _Erp_StockOutOrderService = null;
        private Erp_StockOutGoodsService _Erp_StockOutGoodsService = null;
        private Erp_StockOutRecordService _Erp_StockOutRecordService = null;
        private Erp_StockOutRecordGoodsService _Erp_StockOutRecordGoodsService = null;
        private Erp_BatchService _Erp_BatchService = null;
        private Erp_InventoryService _Erp_InventoryService = null;
        private Erp_InventoryFlowService _Erp_InventoryFlowService = null;
        private Erp_GoodsService _Erp_GoodsService = null;
        private Erp_PurchaseReturnOrderService _Erp_PurchaseReturnOrderSerivce = null;
        private Erp_SalesOrderService _Erp_SalesOrderService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockOutOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockOutOrderService = new Erp_StockOutOrderService(_dbContext);
            _Erp_StockOutGoodsService = new Erp_StockOutGoodsService(_dbContext);
            _Erp_StockOutRecordGoodsService = new Erp_StockOutRecordGoodsService (_dbContext);
            _Erp_StockOutRecordService = new Erp_StockOutRecordService(_dbContext);
            _Erp_GoodsService = new Erp_GoodsService(_dbContext);
            _Erp_BatchService = new Erp_BatchService(_dbContext);
            _Erp_InventoryService = new Erp_InventoryService(_dbContext);
            _Erp_InventoryFlowService = new Erp_InventoryFlowService(_dbContext);
            _Erp_PurchaseReturnOrderSerivce = new Erp_PurchaseReturnOrderService(_dbContext);
            _Erp_SalesOrderService = new Erp_SalesOrderService(_dbContext);
        }


        public void AddEntity(Erp_StockOutOrder entity)
        {
            entity.Create();
            _Erp_StockOutOrderService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockOutOrder> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_StockOutOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockOutOrder entity)
        {
            _Erp_StockOutOrderService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockOutOrder> entities)
        {
           foreach (var e in entities)
            {
                _Erp_StockOutOrderService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockOutOrder entity)
        {
            _Erp_StockOutOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockOutOrder> entities)
        {
            _Erp_StockOutOrderService.RemoveRange(entities);
        }

        public Erp_StockOutOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockOutOrderService.FindList(a => a.Erp_StockOutOrderId == keyValue, a => a.SalesOrder, a=>a.PurchaseReturnOrder,a => a.Warehouse,a=>a.StockTransferOrder, a => a.Erp_StockOutOrderId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_StockOutOrder> GetEntities(Expression<Func<Erp_StockOutOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockOutOrderService.FindList(whereExpression, a => a.Erp_StockOutOrderId, isSortAsc);
             return data;
        }

        public IList<Erp_StockOutOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_StockOutOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockOutOrderService, pagination, sort, whereExpression,a=>a.Warehouse,a=>a.CreateUser);
        }

        public bool IsExists(Expression<Func<Erp_StockOutOrder, bool>> whereExpression)
        {
            return _Erp_StockOutOrderService.IsExists(whereExpression);
        }

        public dynamic GetStockOutGoods(string Erp_StockOutOrderId)
        {
            return _Erp_StockOutGoodsService.FindList(a => a.StockOutOrderId == Erp_StockOutOrderId, a => a.Goods, a => a.GoodsId);
        }

        public void AddStockOut(string Erp_StockOutOrderId, List<Erp_StockOutItem> stockItems)
        {

            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, ts) =>
            {

                var stockOut= _Erp_StockOutOrderService.SingleOrDefault(Erp_StockOutOrderId, true);
                if (stockOut.Completed == 1)
                {
                    throw new Exception("单据【" + stockOut.Number + "】已完成,无需重复操作");
                }

                if (stockOut.IsVoid == 1)
                {
                    throw new Exception("单据【" + stockOut.Number + "】已作废，无法操作！");
                }

                //看 销售出库单  或采购退货 单 是否已经作废，作废了，不能操作了。
                if (stockOut.PurchaseReturnOrderId != null)
                {
                    var purchaseReturnOrder = _Erp_PurchaseReturnOrderSerivce.SingleOrDefault(stockOut.PurchaseReturnOrderId);
                    if (purchaseReturnOrder.IsVoid == 1)
                    {
                        throw new Exception("对应的采购退货单【" + purchaseReturnOrder.Number + "】已经作废，无法操作！");
                    }
                }

                if (stockOut.SalesOrderId != null)
                {
                    var salesOrder = _Erp_SalesOrderService.SingleOrDefault(stockOut.SalesOrderId);
                    if (salesOrder.IsVoid == 1)
                    {
                        throw new Exception("对应的销售单【" + salesOrder.Number + "】已经作废，无法操作！");
                    }
                }

                var totalStockOutQuantity = 0;

                var record = new Erp_StockOutRecord();
                record.Create();
                record.ProcessorId = new OperatorProvider().GetCurrent().UserId;
                record.StockOutOrderId = stockOut.Erp_StockOutOrderId;
                record.ProcessTime = DateTime.Now;
                record.WarehouseId = stockOut.WarehouseId;

                _Erp_StockOutRecordService.Add(record);

                foreach (var item in stockItems)
                {
                    Erp_Batch batch = null;
                    var goods = _Erp_GoodsService.SingleOrDefault(item.GoodsId, true);

                    if (goods.EnableBatchControl == 1)
                    {
                        if (!string.IsNullOrEmpty(item.BatchNo))
                        {
                            batch = _Erp_BatchService.FirstOrDefault(a => a.WarehouseId == stockOut.WarehouseId &&
                               a.GoodsId == goods.Erp_GoodsId && a.Erp_BatchId == item.BatchNo, true);
                            if (batch != null)
                            {
                                batch.TotalQuantity -= item.StockOutQuantity;
                                batch.RemainQuantity -= item.StockOutQuantity;
                                batch.StockStatus = batch.RemainQuantity > 0 ? 1 : 0;
                                _Erp_BatchService.Update(batch, batch.GetPropInfoList(), batch.GetReferenceInfoList());
                            }

                            var inventory = _Erp_InventoryService.FirstOrDefault(a => a.WarehouseId == stockOut.WarehouseId &&
                                a.GoodsId == goods.Erp_GoodsId, true);
                            if (inventory == null)
                            {
                                inventory = new Erp_Inventory();
                                inventory.Create();

                                inventory.GoodsId = goods.Erp_GoodsId;
                                inventory.WarehouseId = stockOut.WarehouseId;
                                inventory.InitialQuantity = 0;
                                inventory.TotalQuantity = 0;
                                inventory.StockStatus = 1;
                                _Erp_InventoryService.Add(inventory);
                            }
                        }
                    }

                    var stockOutQuantity = item.StockOutQuantity;
                    var stockOutGoodsId = item.StockOutGoodsId;
                    var goodsId = item.GoodsId;
                    var batchId = batch == null ? null : batch.Erp_BatchId;
                    var stockOutRecordGoods = new Erp_StockOutRecordGoods();
                    stockOutRecordGoods.Create();
                    stockOutRecordGoods.StockOutGoodsId = stockOutGoodsId;
                    stockOutRecordGoods.GoodsId = goodsId;
                    stockOutRecordGoods.StockOutRecordId = record.Erp_StockOutRecordId;
                    stockOutRecordGoods.StockOutQuantity = stockOutQuantity;
                    stockOutRecordGoods.BatchId = batchId;
                    stockOutRecordGoods.StockOutQuantity = stockOutQuantity;
                    totalStockOutQuantity += stockOutQuantity;
                    _Erp_StockOutRecordGoodsService.Add(stockOutRecordGoods);

                }

                record.TotalQuantity = totalStockOutQuantity;
                _Erp_StockOutRecordService.Update(record, record.GetPropInfoList(), record.GetReferenceInfoList());

                var stockOutRecordGoodsLsit = _Erp_StockOutRecordGoodsService.FindList(a => a.StockOutGoods.StockOutOrderId == Erp_StockOutOrderId, a => a.Goods, a => a.GoodsId, true, true);

                foreach (var goods in stockOutRecordGoodsLsit)
                {
                    var inventory = _Erp_InventoryService.FirstOrDefault(a => a.WarehouseId == stockOut.WarehouseId &&
                            a.GoodsId == goods.GoodsId, true);

                    var quantity_before = 0;
                    var quantity_change = 0;
                    var quantity_after = 0;

                    if (inventory != null)
                    {
                        quantity_before = inventory.TotalQuantity;
                        quantity_change = goods.StockOutQuantity;
                        quantity_after = quantity_before - quantity_change;

                        inventory.TotalQuantity = quantity_after;
                        if (inventory.TotalQuantity < 0)
                        {
                            throw new Exception("产品【" + goods.Goods.Name + "】库存不足");
                        }
                        inventory.StockStatus = 1;

                        _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());

                    }
  
                    var inventoryFlow = new Erp_InventoryFlow();
                    inventoryFlow.Create();
                    inventoryFlow.QuantityChange = quantity_change;
                    inventoryFlow.QuantityBefore = quantity_before;
                    inventoryFlow.QuantityAfter = quantity_after;
                    inventoryFlow.Type = "stockOut";
                    inventoryFlow.GoodsId = goods.GoodsId;
                    inventoryFlow.WarehouseId = record.WarehouseId;
                    inventoryFlow.StockOutOrderId = Erp_StockOutOrderId;

                    _Erp_InventoryFlowService.Add(inventoryFlow);

                    //# 同步入库产品
                    var stockOutGoods = _Erp_StockOutGoodsService.SingleOrDefault(goods.StockOutGoodsId, true);
                    stockOutGoods.RemainQuantity -= quantity_change;
                    stockOutGoods.StockOutQuantity += quantity_change;

                    stockOut.TotalQuantity += quantity_change;
                    stockOut.RemainQuantity -= quantity_change;
                    _Erp_StockOutGoodsService.Update(stockOutGoods, stockOutGoods.GetPropInfoList(), stockOutGoods.GetReferenceInfoList());
                }

                stockOut.Completed = stockOut.RemainQuantity == 0 ? 1 : 0;

                _Erp_StockOutOrderService.Update(stockOut, stockOut.GetPropInfoList(), stockOut.GetReferenceInfoList());

            });

        }
        public void SetVoidStockOut(string Erp_StockOutRecordId)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, ts) =>
            {
                var list = _Erp_StockOutRecordService.FindList(a => a.Erp_StockOutRecordId == Erp_StockOutRecordId, a => a.Processor, a => a.StockOutOrder, a => a.Warehouse, a => a.Erp_StockOutRecordId, true, true);
                if (list != null && list.Count == 1)
                {
                    var entity = list[0];
                    if (entity.IsVoid == 1)
                    {
                        throw new Exception("单据【" + entity.StockOutOrder.Number + "】已经作废,无需重复操作");
                    }
                    entity.IsVoid = 1;
                    _Erp_StockOutRecordService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

                    var StockOutRecordGoodsLsit = _Erp_StockOutRecordGoodsService.FindList(a => a.StockOutRecordId == Erp_StockOutRecordId, a => a.Goods, a => a.StockOutGoods, a => a.GoodsId, true, true);

                    foreach (var goods in StockOutRecordGoodsLsit)
                    {
                        var inventory = _Erp_InventoryService.FirstOrDefault(a => a.WarehouseId == entity.WarehouseId &&
                            a.GoodsId == goods.GoodsId, true);

                        var batch = _Erp_BatchService.FirstOrDefault(a => a.Erp_BatchId == goods.BatchId, true);
                        if (batch != null)
                        {
                            batch.TotalQuantity -= goods.StockOutQuantity;
                            batch.RemainQuantity -= goods.StockOutQuantity;
                            batch.StockStatus = batch.RemainQuantity > 0 ? 1 : 0;
                        }

                        if (inventory == null)
                        {
                            inventory = new Erp_Inventory();
                            inventory.Create();

                            inventory.GoodsId = goods.Goods.Erp_GoodsId;
                            inventory.WarehouseId = entity.WarehouseId;
                            inventory.InitialQuantity = 0;
                            inventory.TotalQuantity = 0;
                            inventory.StockStatus = 1;
                            _Erp_InventoryService.Add(inventory);
                        }
                        else
                        {
                            if (batch != null)
                            {
                                batch.TotalQuantity -= goods.StockOutQuantity;
                                batch.RemainQuantity += goods.StockOutQuantity;
                                batch.StockStatus = batch.RemainQuantity > 0 ? 1 : 0;
                                batch.InventoryId = inventory.Erp_InventoryId;
                                batch.WarehouseId = entity.WarehouseId;
                                batch.GoodsId = goods.Goods.Erp_GoodsId;

                                _Erp_BatchService.Update(batch, batch.GetPropInfoList(), batch.GetReferenceInfoList());
                            }
                        }

                        var quantity_before = 0;
                        var quantity_change = 0;
                        var quantity_after = 0;

                        if (inventory != null)
                        {
                            quantity_before = inventory.TotalQuantity;
                            quantity_change = goods.StockOutQuantity;
                            quantity_after = quantity_before + quantity_change;

                            inventory.TotalQuantity = quantity_after;
                            if (inventory.TotalQuantity < 0)
                            {
                                throw new Exception("产品【" + goods.Goods.Name + "】库存不足");
                            }
                            inventory.StockStatus = 1;

                            _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());

                        }
                        else
                        {
                            inventory = new Erp_Inventory();
                            inventory.Create();

                            inventory.GoodsId = goods.GoodsId;
                            inventory.WarehouseId = entity.WarehouseId;
                            inventory.InitialQuantity = 0;
                            inventory.TotalQuantity = 0;
                            inventory.StockStatus = 1;
                            _Erp_InventoryService.Add(inventory);

                            quantity_before = 0;
                            quantity_change = goods.StockOutQuantity;
                            quantity_after = quantity_before - quantity_change;
                        }


                        var inventoryFlow = new Erp_InventoryFlow();
                        inventoryFlow.Create();
                        inventoryFlow.QuantityChange = quantity_change;
                        inventoryFlow.QuantityBefore = quantity_before;
                        inventoryFlow.QuantityAfter = quantity_after;
                        inventoryFlow.Type = "VoidStockOut";
                        inventoryFlow.GoodsId = goods.GoodsId;
                        inventoryFlow.WarehouseId = entity.WarehouseId;
                        inventoryFlow.StockOutOrderId = entity.StockOutOrderId;

                        _Erp_InventoryFlowService.Add(inventoryFlow);

                        //# 同步入库产品
                        goods.StockOutGoods.RemainQuantity += quantity_change;
                        goods.StockOutGoods.StockOutQuantity -= quantity_change;

                        entity.StockOutOrder.TotalQuantity -= quantity_change;
                        entity.StockOutOrder.RemainQuantity += quantity_change;
                        _Erp_StockOutGoodsService.Update(goods.StockOutGoods, goods.StockOutGoods.GetPropInfoList(), goods.StockOutGoods.GetReferenceInfoList());

                    }

                    entity.StockOutOrder.Completed = entity.StockOutOrder.RemainQuantity == 0 ? 1 : 0;
                    entity.StockOutOrder.IsVoid = 1;
                    _Erp_StockOutOrderService.Update(entity.StockOutOrder, entity.StockOutOrder.GetPropInfoList(), entity.StockOutOrder.GetReferenceInfoList());
                }
            });
        }
    }
}
