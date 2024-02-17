using JKF.BLL.Base;
using JKF.BLL.Models;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using LumiSoft.Net.Mime.vCard;
using NPOI.POIFS.FileSystem;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static NPOI.HSSF.Util.HSSFColor;

namespace JKF.BLL
{
    public class Erp_StockInOrderBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockInOrderService _Erp_StockInOrderService = null;
        private Erp_StockInGoodsService _Erp_StockInGoodsService = null;
        private Erp_InventoryService _Erp_InventoryService = null;
        private Erp_InventoryFlowService _Erp_InventoryFlowService = null;
        private Erp_StockInRecordService _Erp_StockInRecordService = null;
        private Erp_StockInRecordGoodsService _Erp_StockInRecordGoodsService = null;
        private Erp_GoodsService _Erp_GoodsService = null;
        private Erp_BatchService _Erp_BatchService = null;
        private Erp_PurchaseOrderService _Erp_PurchaseOrderSerivce = null;
        private Erp_SalesReturnOrderService _Erp_SalesReturnOrderService = null;

        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockInOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockInOrderService = new Erp_StockInOrderService(_dbContext);
            _Erp_StockInGoodsService = new Erp_StockInGoodsService(_dbContext);
            _Erp_InventoryService = new Erp_InventoryService(_dbContext);
            _Erp_InventoryFlowService = new Erp_InventoryFlowService(_dbContext);
            _Erp_StockInRecordService = new Erp_StockInRecordService(_dbContext);
            _Erp_StockInRecordGoodsService = new Erp_StockInRecordGoodsService(_dbContext);
            _Erp_GoodsService = new Erp_GoodsService(_dbContext);
            _Erp_BatchService = new Erp_BatchService(_dbContext);
            _Erp_PurchaseOrderSerivce = new Erp_PurchaseOrderService(_dbContext);
            _Erp_SalesReturnOrderService = new Erp_SalesReturnOrderService(_dbContext);
        }

        public void AddEntity(Erp_StockInOrder entity)
        {
            entity.Create();
            _Erp_StockInOrderService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockInOrder> entities)
        {
            foreach (var e in entities)
            {
                e.Create();
            }
            _Erp_StockInOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockInOrder entity)
        {
            _Erp_StockInOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockInOrder> entities)
        {
            foreach (var e in entities)
            {
                _Erp_StockInOrderService.Update(e, e.GetPropInfoList(), e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockInOrder entity)
        {
            _Erp_StockInOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockInOrder> entities)
        {
            _Erp_StockInOrderService.RemoveRange(entities);
        }

        public Erp_StockInOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockInOrderService.FindList(a => a.Erp_StockInOrderId == keyValue, a => a.PurchaseOrder, a=>a.SalesReturnOrder,a=>a.StockTransferOrder,a => a.Warehouse, a => a.Erp_StockInOrderId, true, beTraced)[0];
            return entity;
        }

        public IList<Erp_StockInOrder> GetEntities(Expression<Func<Erp_StockInOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockInOrderService.FindList(whereExpression, a => a.Erp_StockInOrderId, isSortAsc);
            return data;
        }

        public IList<Erp_StockInOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_StockInOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockInOrderService, pagination, sort, whereExpression, a => a.Warehouse, a => a.Warehouse);
        }

        public bool IsExists(Expression<Func<Erp_StockInOrder, bool>> whereExpression)
        {
            return _Erp_StockInOrderService.IsExists(whereExpression);
        }

        public dynamic GetStockInGoods(string Erp_StockInOrderId)
        {
            return _Erp_StockInGoodsService.FindList(a => a.StockInOrderId == Erp_StockInOrderId, a => a.Goods, a => a.GoodsId);
        }

        public void AddStockIn(string Erp_StockInOrderId, List<Erp_StockInItem> stockItems)
        {

            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, ts) =>
            {

                var stockIn = _Erp_StockInOrderService.SingleOrDefault(Erp_StockInOrderId, true);
                if (stockIn.Completed == 1)
                {
                    throw new Exception("单据【" + stockIn.Number + "】已完成,无需重复操作");
                }

                if (stockIn.IsVoid == 1)
                {
                    throw new Exception("单据【" + stockIn.Number + "】已作废，无法操作！");
                }

                //看 采购入库单  或销售退货 单 是否已经作废，作废了，不能操作了。
                if (stockIn.PurchaseOrderId != null)
                {
                    var purchaseOrder = _Erp_PurchaseOrderSerivce.SingleOrDefault(stockIn.PurchaseOrderId);
                    if (purchaseOrder.IsVoid == 1)
                    {
                        throw new Exception("对应的采购入库单【" + purchaseOrder.Number + "】已经作废，无法操作！");
                    }          
                }

                if (stockIn.SalesReturnOrderId != null)
                {
                    var salesReturnOrder = _Erp_SalesReturnOrderService.SingleOrDefault(stockIn.SalesReturnOrderId);
                    if (salesReturnOrder.IsVoid == 1)
                    {
                        throw new Exception("对应的销售退货库单【" + salesReturnOrder.Number + "】已经作废，无法操作！");
                    }
                }

                var totalStockInQuantity = 0;

                var record = new Erp_StockInRecord();
                record.Create();
                record.ProcessorId = new OperatorProvider().GetCurrent().UserId;
                record.StockInOrderId = stockIn.Erp_StockInOrderId;
                record.ProcessTime = DateTime.Now;
                record.WarehouseId = stockIn.WarehouseId;

                _Erp_StockInRecordService.Add(record);

                foreach (var item in stockItems)
                {
                    Erp_Batch batch = null;
                    var goods = _Erp_GoodsService.SingleOrDefault(item.GoodsId, true);

                    if (goods.EnableBatchControl == 1)
                    {
                        if (!string.IsNullOrEmpty(item.ProductionDate) &&
                            goods.ShelfLifeDays > 0)
                        {
                            var expirationDate = Convert.ToDateTime(item.ProductionDate).AddDays(goods.ShelfLifeDays.Value);
                            var warningDate = Convert.ToDateTime(item.ProductionDate).AddDays(goods.ShelfLifeWarningDays);

                            batch = _Erp_BatchService.FirstOrDefault(a => a.WarehouseId == stockIn.WarehouseId &&
                               a.GoodsId == goods.Erp_GoodsId && a.Erp_BatchId == item.BatchNo, true);
                            if (batch != null)
                            {
                                batch.TotalQuantity += item.StockInQuantity;
                                batch.RemainQuantity += item.StockInQuantity;
                                batch.StockStatus = batch.RemainQuantity > 0 ? 1 : 0;
                            }

                            var inventory = _Erp_InventoryService.FirstOrDefault(a => a.WarehouseId == stockIn.WarehouseId &&
                                a.GoodsId == goods.Erp_GoodsId, true);
                            if (inventory == null)
                            {
                                inventory = new Erp_Inventory();
                                inventory.Create();

                                inventory.GoodsId = goods.Erp_GoodsId;
                                inventory.WarehouseId = stockIn.WarehouseId;
                                inventory.InitialQuantity = 0;
                                inventory.TotalQuantity = 0;
                                inventory.StockStatus = 1;
                                _Erp_InventoryService.Add(inventory);

                                batch = new Erp_Batch();
                                batch.Create();
                                batch.Erp_BatchId = item.BatchNo;//直接赋号
                                batch.TotalQuantity += item.StockInQuantity;
                                batch.RemainQuantity += item.StockInQuantity;
                                batch.StockStatus = batch.RemainQuantity > 0 ? 1 : 0;
                                batch.InventoryId = inventory.Erp_InventoryId;
                                batch.ExpirationDate = expirationDate;
                                batch.WraningDate = warningDate;
                                batch.WarehouseId = stockIn.WarehouseId;
                                batch.GoodsId = goods.Erp_GoodsId;

                                _Erp_BatchService.Add(batch);
                            }
                            else
                            {
                                batch.TotalQuantity += item.StockInQuantity;
                                batch.RemainQuantity += item.StockInQuantity;
                                batch.StockStatus = batch.RemainQuantity > 0 ? 1 : 0;
                                batch.InventoryId = inventory.Erp_InventoryId;
                                batch.ExpirationDate = expirationDate;
                                batch.WraningDate = warningDate;
                                batch.WarehouseId = stockIn.WarehouseId;
                                batch.GoodsId = goods.Erp_GoodsId;

                                _Erp_BatchService.Update(batch, batch.GetPropInfoList(), batch.GetReferenceInfoList());
                            }

                        }
                    }

                    var stockInQuanqity = item.StockInQuantity;
                    var stockInGoodsId = item.StockInGoodsId;
                    var goodsId = item.GoodsId;
                    var batchId = batch == null ? null : batch.Erp_BatchId;
                    var stockInRecordGoods = new Erp_StockInRecordGoods();
                    stockInRecordGoods.Create();
                    stockInRecordGoods.StockInGoodsId = stockInGoodsId;
                    stockInRecordGoods.GoodsId = goodsId;
                    stockInRecordGoods.StockInRecordId = record.Erp_StockInRecordId;
                    stockInRecordGoods.StockInQuantity = stockInQuanqity;
                    stockInRecordGoods.BatchId = batchId;
                    totalStockInQuantity += stockInQuanqity;
                    _Erp_StockInRecordGoodsService.Add(stockInRecordGoods);

                }

                record.TotalQuantity = totalStockInQuantity;
                _Erp_StockInRecordService.Update(record, record.GetPropInfoList(), record.GetReferenceInfoList());

                var stockInRecordGoodsLsit = _Erp_StockInRecordGoodsService.FindList(a => a.StockInGoods.StockInOrderId == Erp_StockInOrderId, a => a.Goods, a => a.GoodsId, true, true);

                foreach (var goods in stockInRecordGoodsLsit)
                {
                    var inventory = _Erp_InventoryService.FirstOrDefault(a => a.WarehouseId == stockIn.WarehouseId &&
                            a.GoodsId == goods.GoodsId, true);

                    var quantity_before = 0;
                    var quantity_change = 0;
                    var quantity_after = 0;

                    if (inventory != null)
                    {
                        quantity_before = inventory.TotalQuantity;
                        quantity_change = goods.StockInQuantity;
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
                        inventory.WarehouseId = stockIn.WarehouseId;
                        inventory.InitialQuantity = 0;
                        inventory.TotalQuantity = goods.StockInQuantity;
                        inventory.StockStatus = 1;
                        _Erp_InventoryService.Add(inventory);

                        quantity_before = 0;
                        quantity_change = goods.StockInQuantity;
                        quantity_after = quantity_before + quantity_change;
                    }


                    var inventoryFlow = new Erp_InventoryFlow();
                    inventoryFlow.Create();
                    inventoryFlow.QuantityChange = quantity_change;
                    inventoryFlow.QuantityBefore = quantity_before;
                    inventoryFlow.QuantityAfter = quantity_after;
                    inventoryFlow.Type = "StockIn";
                    inventoryFlow.GoodsId = goods.GoodsId;
                    inventoryFlow.WarehouseId = record.WarehouseId;
                    inventoryFlow.StockInOrderId = Erp_StockInOrderId;

                    _Erp_InventoryFlowService.Add(inventoryFlow);

                    //# 同步入库产品

                    var stockInGoods = _Erp_StockInGoodsService.SingleOrDefault(goods.StockInGoodsId,true);
                    stockInGoods.RemainQuantity -= quantity_change;
                    stockInGoods.StockInQuantity += quantity_change;
                    //goods.StockInGoods.RemainQuantity -= quantity_change;
                    //goods.StockInGoods.StockInQuantity += quantity_change;
                    stockIn.TotalQuantity += quantity_change;
                    stockIn.RemainQuantity -= quantity_change;
                    _Erp_StockInGoodsService.Update(stockInGoods, stockInGoods.GetPropInfoList(), stockInGoods.GetReferenceInfoList());
                }

                stockIn.Completed = stockIn.RemainQuantity == 0 ? 1 : 0;

                _Erp_StockInOrderService.Update(stockIn, stockIn.GetPropInfoList(), stockIn.GetReferenceInfoList());

            });

        }

        public void SetVoidStockIn(string Erp_StockInRecordId)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, ts) =>
            {
                var list = _Erp_StockInRecordService.FindList(a => a.Erp_StockInRecordId == Erp_StockInRecordId, a => a.Processor, a => a.StockInOrder, a => a.Warehouse, a => a.Erp_StockInRecordId, true, true);
                if (list != null && list.Count == 1)
                {
                    var entity = list[0];
                    if (entity.IsVoid == 1)
                    {
                        throw new Exception("单据【" + entity.StockInOrder.Number + "】已经作废,无需重复操作");
                    }
                    entity.IsVoid = 1;
                    _Erp_StockInRecordService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

                    var stockInRecordGoodsLsit = _Erp_StockInRecordGoodsService.FindList(a => a.StockInRecordId == Erp_StockInRecordId, a => a.Goods, a => a.StockInGoods, a => a.GoodsId, true, true);

                    foreach (var goods in stockInRecordGoodsLsit)
                    {
                        var inventory = _Erp_InventoryService.FirstOrDefault(a => a.WarehouseId == entity.WarehouseId &&
                            a.GoodsId == goods.GoodsId, true);

                        var batch = _Erp_BatchService.FirstOrDefault(a => a.Erp_BatchId == goods.BatchId, true);
                        if (batch != null)
                        {
                            batch.TotalQuantity -= goods.StockInQuantity;
                            batch.RemainQuantity -= goods.StockInQuantity;
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

                            //batch = new Erp_Batch();
                            //batch.Create();
                            //batch.Erp_BatchId = goods.BatchId;//直接赋号
                            //batch.TotalQuantity -= goods.StockInQuantity;
                            //batch.RemainQuantity -= goods.StockInQuantity;
                            //batch.StockStatus = batch.RemainQuantity > 0 ? 1 : 0;
                            //batch.InventoryId = inventory.Erp_InventoryId;
                            //batch.WarehouseId = entity.WarehouseId;
                            //batch.GoodsId = goods.Goods.Erp_GoodsId;

                            //_Erp_BatchService.Add(batch);
                        }
                        else
                        {
                            if (batch != null)
                            {
                                batch.TotalQuantity -= goods.StockInQuantity;
                                batch.RemainQuantity -= goods.StockInQuantity;
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
                            quantity_change = goods.StockInQuantity;
                            quantity_after = quantity_before - quantity_change;

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
                            quantity_change = goods.StockInQuantity;
                            quantity_after = quantity_before - quantity_change;
                        }


                        var inventoryFlow = new Erp_InventoryFlow();
                        inventoryFlow.Create();
                        inventoryFlow.QuantityChange = quantity_change;
                        inventoryFlow.QuantityBefore = quantity_before;
                        inventoryFlow.QuantityAfter = quantity_after;
                        inventoryFlow.Type = "VoidStockIn";
                        inventoryFlow.GoodsId = goods.GoodsId;
                        inventoryFlow.WarehouseId = entity.WarehouseId;
                        inventoryFlow.StockInOrderId = entity.StockInOrderId;

                        _Erp_InventoryFlowService.Add(inventoryFlow);

                        //# 同步入库产品
                        goods.StockInGoods.RemainQuantity += quantity_change;
                        goods.StockInGoods.StockInQuantity -= quantity_change;

                        entity.StockInOrder.TotalQuantity -= quantity_change;
                        entity.StockInOrder.RemainQuantity += quantity_change;
                        _Erp_StockInGoodsService.Update(goods.StockInGoods, goods.StockInGoods.GetPropInfoList(), goods.StockInGoods.GetReferenceInfoList());

                    }
                    //entity.StockInOrder.TotalQuantity -= entity.StockInOrder.RemainQuantity;
                    entity.StockInOrder.Completed = entity.StockInOrder.RemainQuantity == 0 ? 1 : 0;
                    entity.StockInOrder.IsVoid = 1;
                    _Erp_StockInOrderService.Update(entity.StockInOrder, entity.StockInOrder.GetPropInfoList(), entity.StockInOrder.GetReferenceInfoList());
                }
            });
        }
    }
}
