
using JKF.BLL;
using JKF.BLL.Base;
using JKF.BLL.Models;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JKF.Web.Areas.Erp.Controllers
{
    [Area("Erp")]
    public class WarehouseController : BaseController
    {
        private Erp_WarehouseBLL _Erp_WarehouseBLL = new Erp_WarehouseBLL();
        private Erp_StockInOrderBLL _Erp_StockInOrderBLL = new Erp_StockInOrderBLL();
        private Erp_StockInRecordBLL erp_StockInRecordBLL = new Erp_StockInRecordBLL();
        private Erp_StockInRecordGoodsBLL _Erp_StockInRecordGoodsBLL = new Erp_StockInRecordGoodsBLL();
        private Erp_InventoryFlowBLL _Erp_InventoryFlowBLL = new Erp_InventoryFlowBLL();
        private Erp_StockOutOrderBLL _Erp_StockOutOrderBLL = new Erp_StockOutOrderBLL();
        private Erp_StockOutRecordBLL erp_StockOutRecordBLL = new Erp_StockOutRecordBLL();
        private Erp_StockCheckOrderBLL _Erp_StockCheckOrderBLL = new Erp_StockCheckOrderBLL();
        private Erp_StockOutRecordGoodsBLL _Erp_StockOutRecordGoodsBLL = new Erp_StockOutRecordGoodsBLL();
        private Erp_StockTransferGoodsBLL _Erp_StockTransferGoodsBLL = new Erp_StockTransferGoodsBLL();
        private Erp_StockTransferOrderBLL _Erp_StockTransferOrderBLL = new Erp_StockTransferOrderBLL();


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult InStock()
        {
            return View();
        }

        public IActionResult InStockRecord()
        {
            return View();
        }

        public IActionResult InStockDetailForm()
        {
            return View();
        }

        public IActionResult InStockPostForm()
        {
            return View();
        }
        public IActionResult InStockRecordDetailForm()
        {
            return View();
        }

        public IActionResult OutStock()
        {
            return View();
        }

        public IActionResult OutStockRecord()
        {
            return View();
        }

        public IActionResult OutStockDetailForm()
        {
            return View();
        }
        public IActionResult TransferStockDetailForm()
        {
            return View();
        }
        

        public IActionResult OutStockPostForm()
        {
            return View();
        }
        public IActionResult OutStockRecordDetailForm()
        {
            return View();
        }

        public IActionResult CheckStock()
        {
            return View();
        }

        public IActionResult TransferStockPostForm()
        {
            return View();
        }

        public IActionResult CheckStockPostForm()
        {
            return View();
        }

        public IActionResult AddTransferGoodsForm()
        {
            return View();
        }
        
        public IActionResult AddCheckGoodsForm()
        {
            return View();
        }

        public IActionResult InventoryFlowForm()
        {
            return View();
        }

        public IActionResult TransferStock()
        {
            return View();
        }

        public IActionResult GetTransferOrderNo()
        {
            return this.ExecuteAction(() =>
            {
                string ret = new Erp_BillNoBLL().GetNextBillNo("DB");
                return JsonSuccess(ret);
            });
        }

        public IActionResult GetCheckOrderNo()
        {
            return this.ExecuteAction(() =>
            {
                string ret = new Erp_BillNoBLL().GetNextBillNo("SC");
                return JsonSuccess(ret);
            });
        }

        public IActionResult CheckStockDetailForm()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_WarehouseBLL.GetEntities(a =>
                     true
                     ).ToList().Select(a => new { ItemValue = a.Erp_WarehouseId, ItemName = a.Name + "(" + a.Number + ")" });

                return JsonSuccess(datas);
            });
        }
        public IActionResult GetErp_Warehouses(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Warehouses = _Erp_WarehouseBLL.GetEntities(pagination, sort, a =>
                     a.Name.Contains(keyWord)   ||
                     a.Number.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Warehouses };
                return JsonSuccess(jsonData);
            });
        }


        public IActionResult GetErp_StockInRecords(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Warehouses = erp_StockInRecordBLL.GetEntities(pagination, sort, a =>
                    a.StockInOrder.Number.Contains(keyWord) ||
                
                     a.Warehouse.Number.Contains(keyWord)||
                     a.Warehouse.Name.Contains(keyWord)
                    
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Warehouses };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetErp_StockOutRecords(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Warehouses = erp_StockOutRecordBLL.GetEntities(pagination, sort, a =>
                        a.StockOutOrder.Number.Contains(keyWord) ||
                       a.Warehouse.Number.Contains(keyWord) ||
                     a.Warehouse.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Warehouses };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetErp_StockInRecord(string Erp_StockInRecordId)
        {
            return this.ExecuteAction(() =>
            {
                var datas = erp_StockInRecordBLL.GetEntities(a => a.Erp_StockInRecordId == Erp_StockInRecordId).ToList()[0];
                return JsonSuccess(datas);
            });
        }

        public IActionResult GetErp_StockOutRecord(string Erp_StockOutRecordId)
        {
            return this.ExecuteAction(() =>
            {
                var datas = erp_StockOutRecordBLL.GetEntities(a => a.Erp_StockOutRecordId == Erp_StockOutRecordId).ToList()[0];
                return JsonSuccess(datas);
            });
        }

        public IActionResult GetErp_StockCheckOrders(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Warehouses = _Erp_StockCheckOrderBLL.GetEntities(pagination, sort, a =>
                        a.Number.Contains(keyWord) ||
                     a.Warehouse.Number.Contains(keyWord) ||
                     a.Warehouse.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Warehouses };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetErp_StockTransferOrders(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Warehouses = _Erp_StockTransferOrderBLL.GetEntities(pagination, sort, a =>
                    a.Number.Contains(keyWord)||
                       a.InWarehouse.Number.Contains(keyWord) ||
                     a.InWarehouse.Name.Contains(keyWord)||
                     a.OutWarehouse.Number.Contains(keyWord) ||
                     a.OutWarehouse.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Warehouses };
                return JsonSuccess(jsonData);
            });
        }


        public IActionResult GetErp_StockOutOrders(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Warehouses = _Erp_StockOutOrderBLL.GetEntities(pagination, sort, a =>
                a.Number.Contains(keyWord)||
                     a.Warehouse.Number.Contains(keyWord) ||
                     a.Warehouse.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Warehouses };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetErp_StockInOrders(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Warehouses = _Erp_StockInOrderBLL.GetEntities(pagination, sort, a =>
                a.Number.Contains(keyWord)||
                     a.Warehouse.Number.Contains(keyWord) ||
                     a.Warehouse.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Warehouses };
                return JsonSuccess(jsonData);
            });
        }
        public IActionResult GetStockInRecordGoods(string Erp_StockInRecordId)
        {
            return this.ExecuteAction(() =>
            {


                var datas = _Erp_StockInRecordGoodsBLL.GetEntities(a => a.StockInRecordId == Erp_StockInRecordId
                     ).ToList();


                return JsonSuccess(datas);
            });
        }

        public IActionResult GetStockOutRecordGoods(string Erp_StockOutRecordId)
        {
            return this.ExecuteAction(() =>
            {


                var datas = _Erp_StockOutRecordGoodsBLL.GetEntities(a => a.StockOutRecordId == Erp_StockOutRecordId
                     ).ToList();


                return JsonSuccess(datas);
            });
        }

        public IActionResult GetForm(string Erp_WarehouseId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_Warehouse = _Erp_WarehouseBLL.GetEntity(Erp_WarehouseId);
                return JsonSuccess(Erp_Warehouse);
            });
        }

        public IActionResult SaveForm(Erp_Warehouse Erp_Warehouse)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_Warehouse.Erp_WarehouseId))
                {
                    _Erp_WarehouseBLL.AddEntity(Erp_Warehouse);
                }
                else
                {
                    _Erp_WarehouseBLL.UpdateEntity(Erp_Warehouse);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string Erp_WarehouseId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_WarehouseId))
                {
                    string[] arr = Erp_WarehouseId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Erp_Warehouse> Erp_Warehouses = new List<Erp_Warehouse>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        Erp_Warehouses.Add(new Erp_Warehouse { Erp_WarehouseId = id });
                    }
                    _Erp_WarehouseBLL.RemoveEntities(Erp_Warehouses);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });

        }

        [HttpPost]
        public IActionResult IsExistNumber(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _Erp_WarehouseBLL.IsExists(a => a.Number == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_WarehouseBLL.IsExists(a => a.Number == paramValue && a.Erp_WarehouseId != keyValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }


            }
            catch (Exception ex)
            {
                return JsonFailure(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult IsExistName(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _Erp_WarehouseBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_WarehouseBLL.IsExists(a => a.Name == paramValue && a.Erp_WarehouseId != keyValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }


            }
            catch (Exception ex)
            {
                return JsonFailure(500, ex.Message);
            }
        }
        public IActionResult GetStockOutOrder(string Erp_StockOutOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var data = _Erp_StockOutOrderBLL.GetEntity(Erp_StockOutOrderId);

                return JsonSuccess(data);
            });
        }

        public IActionResult GetStockInOrder(string Erp_StockInOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var data = _Erp_StockInOrderBLL.GetEntity(Erp_StockInOrderId);

                return JsonSuccess(data);
            });
        }

        public IActionResult GetStockCheckOrder(string Erp_StockCheckOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var data = _Erp_StockCheckOrderBLL.GetEntity(Erp_StockCheckOrderId);

                return JsonSuccess(data);
            });
        }
        
         public IActionResult GetStockTransferOrder(string Erp_StockTransferOrderId)
         {
            return this.ExecuteAction(() =>
            {
                var data = _Erp_StockTransferOrderBLL.GetEntity(Erp_StockTransferOrderId);

                return JsonSuccess(data);
            });
        }

        public IActionResult GetStockTransferGoods(string Erp_StockTransferOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var data = _Erp_StockTransferGoodsBLL.GetStockTransferGoods(Erp_StockTransferOrderId);

                return JsonSuccess(data);
            });
        }
        public IActionResult GetStockCheckGoods(string Erp_StockCheckOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var data = _Erp_StockCheckOrderBLL.GetStockCheckGoods(Erp_StockCheckOrderId);

                return JsonSuccess(data);
            });
        }
        public IActionResult GetStockOutGoods(string Erp_StockOutOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var data = _Erp_StockOutOrderBLL.GetStockOutGoods(Erp_StockOutOrderId);

                return JsonSuccess(data);
            });
        }
        public IActionResult GetStockInGoods(string Erp_StockInOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var data = _Erp_StockInOrderBLL.GetStockInGoods(Erp_StockInOrderId);

                return JsonSuccess(data);
            });
        }

        public IActionResult SaveStockInGoodsForm(string Erp_StockInOrderId, List<Erp_StockInItem> InStockGoodsList)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_StockInOrderBLL.AddStockIn(Erp_StockInOrderId, InStockGoodsList);

                return JsonSuccess(null);
            });

        }

        public IActionResult SaveStockOutGoodsForm(string Erp_StockOutOrderId, List<Erp_StockOutItem> OutStockGoodsList)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_StockOutOrderBLL.AddStockOut(Erp_StockOutOrderId, OutStockGoodsList);

                return JsonSuccess(null);
            });

        }


        public IActionResult SetVoidStockIn(string Erp_StockInRecordId)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_StockInOrderBLL.SetVoidStockIn(Erp_StockInRecordId);

                return JsonSuccess(null);
            });

        }

        public IActionResult SetVoidStockOut(string Erp_StockOutRecordId)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_StockOutOrderBLL.SetVoidStockOut(Erp_StockOutRecordId);

                return JsonSuccess(null);
            });

        }

        public IActionResult SaveStockCheckGoodsForm(Erp_StockCheckOrder stockCheckOrder)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_StockCheckOrderBLL.SaveStockCheckGoodsForm(stockCheckOrder);

                return JsonSuccess(null);
            });

        }

        public IActionResult SetStockCheckVoid(string Erp_StockCheckOrderId)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_StockCheckOrderBLL.SetVoid(Erp_StockCheckOrderId);

                return JsonSuccess(null);
            });

        }

        public IActionResult GetErp_InventoryFlows(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";
                var datas = _Erp_InventoryFlowBLL.GetEntities(pagination, sort,
                    a =>
                    a.Warehouse.Number.Contains(keyWord) ||
                    a.Warehouse.Name.Contains(keyWord)||
                     a.Goods.Number.Contains(keyWord) ||
                    a.Goods.Name.Contains(keyWord)
                    );

                var jsonData = new { pagination, sort, datas = datas };
                return JsonSuccess(jsonData);
            });
        }
        public IActionResult SaveStockTransferGoodsForm(Erp_StockTransferOrder stockTransferOrder)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_StockTransferOrderBLL.SaveStockTransferGoodsForm(stockTransferOrder);

                return JsonSuccess(null);
            });

        }

        public IActionResult SetStockTransferVoid(string Erp_StockTransferOrderId)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_StockTransferOrderBLL.SetVoid(Erp_StockTransferOrderId);

                return JsonSuccess(null);
            });

        }

    }
}
