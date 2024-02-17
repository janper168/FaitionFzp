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
using static NPOI.HSSF.Util.HSSFColor;

namespace JKF.BLL
{
    public class Erp_StockTransferOrderBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockTransferOrderService _Erp_StockTransferOrderService = null;
        private Erp_StockTransferGoodsService _Erp_StockTransferGoodsService = null;
        private Erp_InventoryService _Erp_InventoryService = null;
        private Erp_InventoryFlowService _Erp_InventoryFlowService = null;
        private Erp_StockOutOrderService _Erp_StockOutOrderService = null;
        private Erp_StockOutGoodsService _Erp_StockOutGoodsService = null;
        private Erp_StockInOrderService _Erp_StockInOrderService = null;
        private Erp_StockInGoodsService _Erp_StockInGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();
        private Erp_BillNoBLL _Erp_BillNoBLL = new Erp_BillNoBLL();

        public Erp_StockTransferOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockTransferOrderService = new Erp_StockTransferOrderService(_dbContext);
            _Erp_InventoryService = new Erp_InventoryService(_dbContext);
            _Erp_StockTransferGoodsService = new Erp_StockTransferGoodsService(_dbContext);
            _Erp_InventoryFlowService = new Erp_InventoryFlowService(_dbContext);
            _Erp_StockOutOrderService = new Erp_StockOutOrderService(_dbContext);
            _Erp_StockOutGoodsService = new Erp_StockOutGoodsService(_dbContext);
            _Erp_StockInOrderService = new Erp_StockInOrderService(_dbContext);
            _Erp_StockInGoodsService = new Erp_StockInGoodsService(_dbContext);
        }

        public void AddEntity(Erp_StockTransferOrder entity)
        {
            entity.Create();
            _Erp_StockTransferOrderService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockTransferOrder> entities)
        {
            foreach (var e in entities)
            {
                e.Create();
            }
            _Erp_StockTransferOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockTransferOrder entity)
        {
            _Erp_StockTransferOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockTransferOrder> entities)
        {
            foreach (var e in entities)
            {
                _Erp_StockTransferOrderService.Update(e, e.GetPropInfoList(), e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockTransferOrder entity)
        {
            _Erp_StockTransferOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockTransferOrder> entities)
        {
            _Erp_StockTransferOrderService.RemoveRange(entities);
        }

        public Erp_StockTransferOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockTransferOrderService.FindList(a => a.Erp_StockTransferOrderId == keyValue, a => a.Erp_StockTransferOrderId, true, beTraced)[0];
            return entity;
        }

        public IList<Erp_StockTransferOrder> GetEntities(Expression<Func<Erp_StockTransferOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockTransferOrderService.FindList(whereExpression, a => a.Erp_StockTransferOrderId, isSortAsc);
            return data;
        }

        public IList<Erp_StockTransferOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_StockTransferOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockTransferOrderService, pagination, sort, whereExpression, a => a.InWarehouse, a => a.OutWarehouse, a => a.CreateUser);
        }

        public bool IsExists(Expression<Func<Erp_StockTransferOrder, bool>> whereExpression)
        {
            return _Erp_StockTransferOrderService.IsExists(whereExpression);
        }


        public void SaveStockTransferGoodsForm(Erp_StockTransferOrder entity)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);

            bs.Execute((db, td) =>
            {

                entity.Create();
                entity.EnableAutoStockOut = int.Parse(SysSettingsConfig.configs["AutoOutStock"]);
                entity.EnableAutoStockIn = int.Parse(SysSettingsConfig.configs["AutoInStock"]);

                //商品
                if (entity.StockTransferGoodsList != null && entity.StockTransferGoodsList.Count() > 0)
                {
                    foreach (var goods in entity.StockTransferGoodsList)
                    {
                        goods.Create();
                    }
                }

                //调拨订单
                _Erp_StockTransferOrderService.Add(entity);

                //如果是自动出库
                if (entity.EnableAutoStockOut == 1)
                {
                    foreach (var goods in entity.StockTransferGoodsList)
                    {
                        var inventorys = _Erp_InventoryService.FindList(a =>
                                a.GoodsId == goods.GoodsId &&
                                entity.OutWarehouseId == a.WarehouseId,
                                a => a.Erp_InventoryId, true, true);
                        if (inventorys != null && inventorys.Count == 1)
                        {
                            var inventory = inventorys[0];


                            var quantityBefore = inventory.TotalQuantity;
                            var quantityChange = goods.StockTransferQuantity;
                            var quantityAfter = quantityBefore - quantityChange;

                            inventory.TotalQuantity = quantityAfter;

                            if (inventory.TotalQuantity < 0)
                            {
                                throw new Exception("产品【" + goods.GoodsId + "】库存不足！");
                            }
                            inventory.StockStatus = inventory.TotalQuantity < 0 ? 1 : 0;

                            _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());

                            var inventoryFlow = new Erp_InventoryFlow();
                            inventoryFlow.Create();
                            inventoryFlow.WarehouseId = entity.OutWarehouseId;
                            inventoryFlow.GoodsId = goods.GoodsId;
                            inventoryFlow.Type = "StockTransfer";
                            inventoryFlow.QuantityBefore = quantityBefore;
                            inventoryFlow.QuantityChange = quantityChange;
                            inventoryFlow.QuantityAfter = quantityAfter;
                            inventoryFlow.StockTransferOrderId = entity.Erp_StockTransferOrderId;

                            _Erp_InventoryFlowService.Add(inventoryFlow);
                        }
                    }
                }
                else//不是自动出库
                {
                    foreach (var goods in entity.StockTransferGoodsList)
                    {
                        //创建出库通知单据
                        string nextBillNo = _Erp_BillNoBLL.GetNextBillNo("SO");
                        var stockOutOrder = new Erp_StockOutOrder();
                        stockOutOrder.Create();
                        stockOutOrder.Number = nextBillNo;
                        stockOutOrder.StockTransferOrderId = entity.Erp_StockTransferOrderId;
                        stockOutOrder.Type = "StockTransfer";
                        stockOutOrder.WarehouseId = entity.OutWarehouseId;
                        stockOutOrder.TotalQuantity = 0;
                        stockOutOrder.RemainQuantity = entity.TotalQuantity.Value;
                        stockOutOrder.IsVoid = 0;
                        stockOutOrder.Completed = 0;
                        _Erp_StockOutOrderService.Add(stockOutOrder);

                        foreach (var goods2 in entity.StockTransferGoodsList)
                        {
                            var stockOutGood = new Erp_StockOutGoods();
                            stockOutGood.Create();
                            stockOutGood.GoodsId = goods2.GoodsId;
                            stockOutGood.RemainQuantity = goods2.StockTransferQuantity;
                            stockOutGood.StockOutOrderId = stockOutOrder.Erp_StockOutOrderId;
                            _Erp_StockOutGoodsService.Add(stockOutGood);
                        }
                    }
                    //如果是自动入库
                    if (entity.EnableAutoStockIn == 1)
                    {
                        foreach (var goods2 in entity.StockTransferGoodsList)
                        {
                            var inventorys = _Erp_InventoryService.FindList(a =>
                                a.GoodsId == goods2.GoodsId &&
                                entity.InWarehouseId == a.WarehouseId,
                                a => a.Erp_InventoryId, true, true);
                            if (inventorys != null && inventorys.Count == 1)
                            {
                                var inventory = inventorys[0];

                                var quantityBefore = inventory.TotalQuantity;
                                var quantityChange = goods2.StockTransferQuantity;
                                var quantityAfter = quantityBefore - +quantityChange;

                                inventory.TotalQuantity = quantityAfter;

                                if (inventory.TotalQuantity < 0)
                                {
                                    throw new Exception("产品【" + goods2.GoodsId + "】库存不足！");
                                }
                                inventory.StockStatus = inventory.TotalQuantity < 0 ? 1 : 0;

                                _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());

                                var inventoryFlow = new Erp_InventoryFlow();
                                inventoryFlow.Create();
                                inventoryFlow.WarehouseId = entity.InWarehouseId;
                                inventoryFlow.GoodsId = goods2.GoodsId;
                                inventoryFlow.Type = "StockTransfer";
                                inventoryFlow.QuantityBefore = quantityBefore;
                                inventoryFlow.QuantityChange = quantityChange;
                                inventoryFlow.QuantityAfter = quantityAfter;
                                inventoryFlow.StockTransferOrderId = entity.Erp_StockTransferOrderId;

                                _Erp_InventoryFlowService.Add(inventoryFlow);
                            }
                            else
                            {
                                var inventory = new Erp_Inventory();
                                inventory.Create();
                                inventory.GoodsId = goods2.GoodsId;
                                inventory.WarehouseId = entity.InWarehouseId;
                                inventory.TotalQuantity = goods2.StockTransferQuantity;
                                inventory.StockStatus = inventory.TotalQuantity > 0 ? 1 : 0;
                                _Erp_InventoryService.Add(inventory);

                                var inventoryFlow = new Erp_InventoryFlow();
                                inventoryFlow.Create();
                                inventoryFlow.WarehouseId = entity.InWarehouseId;
                                inventoryFlow.GoodsId = goods2.GoodsId;
                                inventoryFlow.Type = "StockTransfer";
                                inventoryFlow.QuantityBefore = 0;
                                inventoryFlow.QuantityChange = goods2.StockTransferQuantity;
                                inventoryFlow.QuantityAfter = goods2.StockTransferQuantity;
                                inventoryFlow.StockTransferOrderId = entity.Erp_StockTransferOrderId;

                                _Erp_InventoryFlowService.Add(inventoryFlow);
                            }
                        }
                    }
                    else//如果不是自动入库
                    {
                        //创建入库通知单据
                        string nextBillNo = _Erp_BillNoBLL.GetNextBillNo("SI");
                        var stockInOrder = new Erp_StockInOrder();
                        stockInOrder.Create();
                        stockInOrder.Number = nextBillNo;
                        stockInOrder.StockTransferOrderId = entity.Erp_StockTransferOrderId;
                        stockInOrder.Type = "StockTransfer";
                        stockInOrder.WarehouseId = entity.InWarehouseId;
                        stockInOrder.TotalQuantity = 0;
                        stockInOrder.RemainQuantity = entity.TotalQuantity.Value;
                        stockInOrder.IsVoid = 0;
                        stockInOrder.Completed = 0;
                        _Erp_StockInOrderService.Add(stockInOrder);

                        foreach (var goods2 in entity.StockTransferGoodsList)
                        {
                            var stockInGood = new Erp_StockInGoods();
                            stockInGood.Create();
                            stockInGood.GoodsId = goods2.GoodsId;
                            stockInGood.RemainQuantity = goods2.StockTransferQuantity;
                            stockInGood.StockInOrderId = stockInOrder.Erp_StockInOrderId;
                            _Erp_StockInGoodsService.Add(stockInGood);
                        }
                    }
                }

            });
        }

        public void SetVoid(string StockTransferOrderId)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);

            bs.Execute((db, td) =>
            {

                var entity = _Erp_StockTransferOrderService.SingleOrDefault(StockTransferOrderId, true);
                if (entity.IsVoid == 1)
                {
                    throw new Exception("单据【" + entity.Number + "】库存不足！");
                }
                entity.IsVoid = 1;

                //调拨订单
                _Erp_StockTransferOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

                //如果是自动出库
                if (entity.EnableAutoStockOut == 1)
                {
                    foreach (var goods in entity.StockTransferGoodsList)
                    {
                        var inventorys = _Erp_InventoryService.FindList(a =>
                                a.GoodsId == goods.GoodsId &&
                                entity.OutWarehouseId == a.WarehouseId,
                                a => a.Erp_InventoryId, true, true);
                        if (inventorys != null && inventorys.Count == 1)
                        {
                            var inventory = inventorys[0];

                            var quantityBefore = inventory.TotalQuantity;
                            var quantityChange = goods.StockTransferQuantity;
                            var quantityAfter = quantityBefore + quantityChange;

                            inventory.TotalQuantity = quantityAfter;

                            if (inventory.TotalQuantity < 0)
                            {
                                throw new Exception("产品【" + goods.GoodsId + "】库存不足！");
                            }
                            inventory.StockStatus = inventory.TotalQuantity < 0 ? 1 : 0;

                            _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());

                            var inventoryFlow = new Erp_InventoryFlow();
                            inventoryFlow.Create();
                            inventoryFlow.WarehouseId = entity.OutWarehouseId;
                            inventoryFlow.GoodsId = goods.GoodsId;
                            inventoryFlow.Type = "VoidStockTransfer";
                            inventoryFlow.QuantityBefore = quantityBefore;
                            inventoryFlow.QuantityChange = quantityChange;
                            inventoryFlow.QuantityAfter = quantityAfter;
                            inventoryFlow.StockTransferOrderId = entity.Erp_StockTransferOrderId;

                            _Erp_InventoryFlowService.Add(inventoryFlow);
                        }
                    }
                }
                else//不是自动出库
                {

                    var stockOut = _Erp_StockOutOrderService.FirstOrDefault(a => a.StockTransferOrderId == entity.Erp_StockTransferOrderId, true);
                    if (stockOut.TotalQuantity != stockOut.RemainQuantity)
                    {
                        throw new Exception("单据【" + entity.Number + "】存在出库记录");
                    }
                    stockOut.IsVoid = 1;
                    _Erp_StockOutOrderService.Update(stockOut, stockOut.GetPropInfoList(), stockOut.GetReferenceInfoList());
                }
                
                //如果是自动入库
                if (entity.EnableAutoStockIn == 1)
                {
                    foreach (var goods2 in entity.StockTransferGoodsList)
                    {
                        var inventorys = _Erp_InventoryService.FindList(a =>
                            a.GoodsId == goods2.GoodsId &&
                            entity.InWarehouseId == a.WarehouseId,
                            a => a.Erp_InventoryId, true, true);
                        if (inventorys != null && inventorys.Count == 1)
                        {
                            var inventory = inventorys[0];

                            var quantityBefore = inventory.TotalQuantity;
                            var quantityChange = goods2.StockTransferQuantity;
                            var quantityAfter = quantityBefore - quantityChange;

                            inventory.TotalQuantity = quantityAfter;

                            if (inventory.TotalQuantity < 0)
                            {
                                throw new Exception("产品【" + goods2.GoodsId + "】库存不足！");
                            }
                            inventory.StockStatus = inventory.TotalQuantity < 0 ? 1 : 0;

                            _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());

                            var inventoryFlow = new Erp_InventoryFlow();
                            inventoryFlow.Create();
                            inventoryFlow.WarehouseId = entity.InWarehouseId;
                            inventoryFlow.GoodsId = goods2.GoodsId;
                            inventoryFlow.Type = "VoidStockTransfer";
                            inventoryFlow.QuantityBefore = quantityBefore;
                            inventoryFlow.QuantityChange = quantityChange;
                            inventoryFlow.QuantityAfter = quantityAfter;
                            inventoryFlow.StockTransferOrderId = entity.Erp_StockTransferOrderId;

                            _Erp_InventoryFlowService.Add(inventoryFlow);
                        }
                        
                    }

                }
                else//如果不是自动入库
                {
                    var stockIn = _Erp_StockInOrderService.FirstOrDefault(a => a.StockTransferOrderId == entity.Erp_StockTransferOrderId, true);
                    if (stockIn.TotalQuantity != stockIn.RemainQuantity)
                    {
                        throw new Exception("单据【" + entity.Number + "】存在入库记录");
                    }
                    stockIn.IsVoid = 1;
                    _Erp_StockInOrderService.Update(stockIn, stockIn.GetPropInfoList(), stockIn.GetReferenceInfoList());
                }
            });
        }
    }
}
