using JKF.BLL;
using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static NPOI.HSSF.Util.HSSFColor;

namespace JKF.BLL
{
    public class Erp_StockCheckOrderBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockCheckOrderService _Erp_StockCheckOrderService = null;
        private Erp_StockCheckGoodsService _Erp_StockCheckGoodsService = null;
        private Erp_InventoryService _Erp_InventoryService = null;
        private Erp_InventoryFlowService _Erp_InventoryFlowService = null;

        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockCheckOrderBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockCheckOrderService = new Erp_StockCheckOrderService(_dbContext);
            _Erp_StockCheckGoodsService = new Erp_StockCheckGoodsService(_dbContext);
            _Erp_InventoryService = new Erp_InventoryService(_dbContext);
            _Erp_InventoryFlowService = new Erp_InventoryFlowService(_dbContext);
        }

        public void AddEntity(Erp_StockCheckOrder entity)
        {
            entity.Create();
            _Erp_StockCheckOrderService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockCheckOrder> entities)
        {
            foreach (var e in entities)
            {
                e.Create();
            }
            _Erp_StockCheckOrderService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockCheckOrder entity)
        {
            _Erp_StockCheckOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockCheckOrder> entities)
        {
            foreach (var e in entities)
            {
                _Erp_StockCheckOrderService.Update(e, e.GetPropInfoList(), e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockCheckOrder entity)
        {
            _Erp_StockCheckOrderService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockCheckOrder> entities)
        {
            _Erp_StockCheckOrderService.RemoveRange(entities);
        }

        public Erp_StockCheckOrder GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockCheckOrderService.FindList(a => a.Erp_StockCheckOrderId == keyValue, a => a.Erp_StockCheckOrderId, true, beTraced)[0];
            return entity;
        }

        public IList<Erp_StockCheckOrder> GetEntities(Expression<Func<Erp_StockCheckOrder, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockCheckOrderService.FindList(whereExpression, a => a.Erp_StockCheckOrderId, isSortAsc);
            return data;
        }

        public IList<Erp_StockCheckOrder> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_StockCheckOrder, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockCheckOrderService, pagination, sort, whereExpression, a => a.Processor, a => a.Warehouse);
        }

        public bool IsExists(Expression<Func<Erp_StockCheckOrder, bool>> whereExpression)
        {
            return _Erp_StockCheckOrderService.IsExists(whereExpression);
        }

        public dynamic GetStockCheckGoods(string Erp_StockCheckOrderId)
        {
            return _Erp_StockCheckGoodsService.FindList(a => a.StockCheckOrderId == Erp_StockCheckOrderId, a => a.Goods, a => a.GoodsId);
        }

        public void SaveStockCheckGoodsForm(Erp_StockCheckOrder entity)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);

            bs.Execute((db, td) =>
            {

                entity.Create();
                entity.ProcessorId = new OperatorProvider().GetCurrent().UserId;

                //商品
                if (entity.StockCheckGoodsList != null && entity.StockCheckGoodsList.Count() > 0)
                {
                    foreach (var goods in entity.StockCheckGoodsList)
                    {
                        goods.Create();
                    }
                }

                //采购订单
                _Erp_StockCheckOrderService.Add(entity);

                var totalBookQuantity = 0;
                var totalActualQuantity = 0;
                var totalSurplusAmount = 0m;

                foreach (var goods in entity.StockCheckGoodsList)
                {

                    var inventorys = _Erp_InventoryService.FindList(a =>
                            a.GoodsId == goods.GoodsId &&
                            entity.WarehouseId == a.WarehouseId,
                            a => a.Erp_InventoryId, true, true);
                    if (inventorys != null && inventorys.Count == 1)
                    {
                        var inventory = inventorys[0];
                        var bookQuantity = inventory.TotalQuantity;
                        var actualQuantity = goods.ActualQuantity;
                        var surpluQuantity = actualQuantity - bookQuantity;
                        var purchaseTrice = goods.PurchasePrice;
                        var surplusAmount = surpluQuantity * purchaseTrice;

                        if (surpluQuantity > 0)
                            goods.Status = "Surplus";
                        else if (surpluQuantity < 0)
                            goods.Status = "Loss";
                        else
                            goods.Status = "unchange";

                        goods.StockCheckOrderId = entity.Erp_StockCheckOrderId;
                        goods.BookQuantity = bookQuantity;
                        goods.ActualQuantity = actualQuantity;
                        goods.SurplusQuantity = surpluQuantity;
                        goods.PurchasePrice = purchaseTrice;
                        goods.SurplusAmount = surplusAmount;


                        _Erp_StockCheckGoodsService.Update(goods, goods.GetPropInfoList(), goods.GetReferenceInfoList());

                        totalBookQuantity += bookQuantity;
                        totalActualQuantity += actualQuantity;
                        totalSurplusAmount += surplusAmount;

                        var quantityBefore = inventory.TotalQuantity;
                        var quantityChange = goods.SurplusQuantity;
                        var quantityAfter = goods.ActualQuantity;

                        inventory.TotalQuantity = quantityAfter;
                        if (inventory.TotalQuantity < 0)
                        {
                            throw new Exception("产品【" + goods.GoodsId + "】库存不足！");
                        }
                        inventory.StockStatus = inventory.TotalQuantity < 0 ? 1 : 0;

                        _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());

                        var inventoryFlow = new Erp_InventoryFlow();
                        inventoryFlow.Create();
                        inventoryFlow.WarehouseId = entity.WarehouseId;
                        inventoryFlow.GoodsId = goods.GoodsId;
                        inventoryFlow.Type = "StockCheck";
                        inventoryFlow.QuantityBefore = quantityBefore;
                        inventoryFlow.QuantityChange = quantityChange;
                        inventoryFlow.QuantityAfter = quantityAfter;
                        inventoryFlow.StockCheckOrderId = entity.Erp_StockCheckOrderId;

                        _Erp_InventoryFlowService.Add(inventoryFlow);
                    }
                }
                entity.TotalBookQuantity = totalBookQuantity;
                entity.TotalActualQuantity = totalActualQuantity;
                entity.TotalSurplusQuantity = totalActualQuantity - totalBookQuantity;
                entity.TotalSurplusAmount = totalSurplusAmount;

                if (entity.TotalSurplusQuantity > 0)
                    entity.Status = "Surplus";
                else if (entity.TotalSurplusQuantity < 0)
                    entity.Status = "Loss";
                else
                    entity.Status = "unchange";

                _Erp_StockCheckOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());



            });
        }

        public void SetVoid(string Erp_StockCheckOrderId)
        {
            BaseTransaction bs = new BaseTransaction(_dbContext);
            bs.Execute((db, td) =>
            {
                var entity = _Erp_StockCheckOrderService.FindList(a => a.Erp_StockCheckOrderId == Erp_StockCheckOrderId,
                    a=>a.StockCheckGoodsList,a => a.Erp_StockCheckOrderId, true, true)[0];

                if (entity != null)
                {
                    if (entity.IsVoid == 1)
                    {
                        throw new Exception("盘点单据【" + entity.Number + "】已经作废，无法操作！");
                    }
                    entity.IsVoid = 1;
                    _Erp_StockCheckOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());


                    var totalBookQuantity = 0;
                    var totalActualQuantity = 0;
                    var totalSurplusAmount = 0m;

                    foreach (var goods in entity.StockCheckGoodsList)
                    {

                        var inventorys = _Erp_InventoryService.FindList(a =>
                                a.GoodsId == goods.GoodsId &&
                                entity.WarehouseId == a.WarehouseId,
                                a => a.Erp_InventoryId, true, true);
                        if (inventorys != null && inventorys.Count == 1)
                        {
                            var inventory = inventorys[0];
                            
                            var actualQuantity = goods.BookQuantity;
                            var bookQuantity = goods.ActualQuantity;
                            var surpluQuantity = -goods.SurplusQuantity;
                            var purchaseTrice = goods.PurchasePrice;
                            var surplusAmount = surpluQuantity * purchaseTrice;

                            if (surpluQuantity > 0)
                                goods.Status = "Surplus";
                            else if (surpluQuantity < 0)
                                goods.Status = "Loss";
                            else
                                goods.Status = "unchange";

                            goods.StockCheckOrderId = entity.Erp_StockCheckOrderId;
                            goods.BookQuantity = bookQuantity;
                            goods.ActualQuantity = actualQuantity;
                            goods.SurplusQuantity = surpluQuantity;
                            goods.PurchasePrice = purchaseTrice;
                            goods.SurplusAmount = surplusAmount;


                            _Erp_StockCheckGoodsService.Update(goods, goods.GetPropInfoList(), goods.GetReferenceInfoList());

                            totalBookQuantity += bookQuantity;
                            totalActualQuantity += actualQuantity;
                            totalSurplusAmount += surplusAmount;

                            var quantityBefore = inventory.TotalQuantity;
                            var quantityChange = goods.SurplusQuantity;
                            var quantityAfter = goods.ActualQuantity;

                            inventory.TotalQuantity = quantityAfter;
                            if (inventory.TotalQuantity < 0)
                            {
                                throw new Exception("产品【" + goods.GoodsId + "】库存不足！");
                            }
                            inventory.StockStatus = inventory.TotalQuantity < 0 ? 1 : 0;

                            _Erp_InventoryService.Update(inventory, inventory.GetPropInfoList(), inventory.GetReferenceInfoList());

                            var inventoryFlow = new Erp_InventoryFlow();
                            inventoryFlow.Create();
                            inventoryFlow.WarehouseId = entity.WarehouseId;
                            inventoryFlow.GoodsId = goods.GoodsId;
                            inventoryFlow.Type = "VoidStockCheck";
                            inventoryFlow.QuantityBefore = quantityBefore;
                            inventoryFlow.QuantityChange = quantityChange;
                            inventoryFlow.QuantityAfter = quantityAfter;
                            inventoryFlow.StockCheckOrderId = entity.Erp_StockCheckOrderId;

                            _Erp_InventoryFlowService.Add(inventoryFlow);
                        }
                    }
                    entity.TotalBookQuantity = totalBookQuantity;
                    entity.TotalActualQuantity = totalActualQuantity;
                    entity.TotalSurplusQuantity = totalActualQuantity - totalBookQuantity;
                    entity.TotalSurplusAmount = totalSurplusAmount;

                    if (entity.TotalSurplusQuantity > 0)
                        entity.Status = "Surplus";
                    else if (entity.TotalSurplusQuantity < 0)
                        entity.Status = "Loss";
                    else
                        entity.Status = "unchange";

                    _Erp_StockCheckOrderService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

                }

            });
        }
    }
}