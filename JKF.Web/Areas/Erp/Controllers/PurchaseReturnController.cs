
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
    public class PurchaseReturnController : BaseController
    {

        private Erp_PurchaseReturnOrderBLL _Erp_PurchaseReturnOrderBLL = new Erp_PurchaseReturnOrderBLL();

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

        public IActionResult GetPurchaseReturnOrderNo()
        {
            return this.ExecuteAction(() =>
            {
                string ret = new Erp_BillNoBLL().GetNextBillNo("PR"); //采购退货
                return JsonSuccess(ret);
            });
        }


        public IActionResult GetErp_PurchaseReturnOrders(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var datas = _Erp_PurchaseReturnOrderBLL.GetEntities(pagination, sort, a =>
                      a.Suppiler.Number.Contains(keyWord) ||
                      a.Suppiler.Name.Contains(keyWord) ||
                      a.Warehouse.Name.Contains(keyWord) ||
                      a.Warehouse.Number.Contains(keyWord) ||
                      a.Number.Contains(keyWord)||
                      a.PurchaseOrder.Number.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = datas };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string Erp_PurchaseReturnOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_PurchaseOrder = _Erp_PurchaseReturnOrderBLL.GetEntity(Erp_PurchaseReturnOrderId);
                return JsonSuccess(Erp_PurchaseOrder);
            });
        }

        public IActionResult SaveForm(Erp_PurchaseReturnOrder Erp_PurchaseReturnOrder)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_PurchaseReturnOrder.Erp_PurchaseReturnOrderId))
                {
                    _Erp_PurchaseReturnOrderBLL.AddPurchaseReturnOrder(Erp_PurchaseReturnOrder);
                }
                //}
                //else
                //{
                //    _Erp_PurchaseOrderBLL.UpdateEntity(Erp_PurchaseOrder);

                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string Erp_PurchaseReturnOrderId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_PurchaseReturnOrderId))
                {

                    _Erp_PurchaseReturnOrderBLL.SetVoid(Erp_PurchaseReturnOrderId);
                    //return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });
        }

    }

}
