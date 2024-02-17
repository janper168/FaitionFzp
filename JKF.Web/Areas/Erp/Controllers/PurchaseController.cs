
using JKF.BLL;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Web.Controllers;
using LumiSoft.Net.IMAP;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
namespace JKF.Web.Areas.Erp.Controllers
{
    [Area("Erp")]
    public class PurchaseController : BaseController
    {

        private Erp_PurchaseOrderBLL _Erp_PurchaseOrderBLL = new Erp_PurchaseOrderBLL();
        private Erp_PurchaseGoodsBLL _Erp_PurchaseGoodsBLL = new Erp_PurchaseGoodsBLL();    

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult AddGoodsForm()
        {
            return View();
        }

        public IActionResult DetailForm()
        {
            return View();
        }

        public IActionResult GetPurchaseOrderNo()
        {
            return this.ExecuteAction(() =>
            {
                string ret = new Erp_BillNoBLL().GetNextBillNo("PI");
                return JsonSuccess(ret);
            });
        }

        public IActionResult GetPuchaseGoodsForm(string Erp_GoodsId)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_PurchaseGoodsBLL.GetEntity(a=>a.GoodsId == Erp_GoodsId);
                return JsonSuccess(datas);


            });

        }

        public IActionResult GetAll(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_PurchaseOrderBLL.GetEntities(a => true
                     ).ToList().Select(a => new
                     {
                         ItemValue = a.Erp_PurchaseOrderId
                     ,
                         ItemName = a.Number
                     });
                return JsonSuccess(datas);


            });
        }
        public IActionResult GetErp_PurchaseOrders(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_PurchaseOrders = _Erp_PurchaseOrderBLL.GetEntities(pagination, sort, a =>
                     a.Suppiler.Number.Contains(keyWord) ||
                      a.Suppiler.Name.Contains(keyWord) ||
                      a.Warehouse.Name.Contains(keyWord) || 
                      a.Warehouse.Number.Contains(keyWord) ||
                      a.Number.Contains(keyWord)               
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_PurchaseOrders };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string Erp_PurchaseOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_PurchaseOrder = _Erp_PurchaseOrderBLL.GetEntity(Erp_PurchaseOrderId);
                return JsonSuccess(Erp_PurchaseOrder);
            });
        }

        public IActionResult SaveForm(Erp_PurchaseOrder Erp_PurchaseOrder)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_PurchaseOrder.Erp_PurchaseOrderId))
                {
                    _Erp_PurchaseOrderBLL.AddPurchaseOrder(Erp_PurchaseOrder);
                }

                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string Erp_PurchaseOrderId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_PurchaseOrderId))
                {

                    _Erp_PurchaseOrderBLL.SetVoid(Erp_PurchaseOrderId);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });
        }

    }

}
