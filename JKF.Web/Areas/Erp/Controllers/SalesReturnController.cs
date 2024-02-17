
using Castle.Core.Resource;
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
    public class SalesReturnController : BaseController
    {

        private Erp_SalesReturnOrderBLL _Erp_SalesReturnOrderBLL = new Erp_SalesReturnOrderBLL();
        private Erp_SalesOrderBLL _Erp_SalesOrderBLL = new Erp_SalesOrderBLL();
        private Erp_SalesGoodsBLL _Erp_SalesGoodsBLL = new Erp_SalesGoodsBLL();

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

        public IActionResult GetAll(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_SalesOrderBLL.GetEntities(a => true
                     ).ToList().Select(a => new
                     {
                         ItemValue = a.Erp_SalesOrderId
                     ,
                         ItemName = a.Number
                     });
                return JsonSuccess(datas);


            });
        }

        public IActionResult GetSalesReturnOrderNo()
        {
            return this.ExecuteAction(() =>
            {
                string ret = new Erp_BillNoBLL().GetNextBillNo("XT"); //销售退货
                return JsonSuccess(ret);
            });
        }


        public IActionResult GetErp_SalesReturnOrders(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var datas = _Erp_SalesReturnOrderBLL.GetEntities(pagination, sort, a =>
                      a.Customer.Number.Contains(keyWord) ||
                      a.Customer.Name.Contains(keyWord) ||
                      a.Warehouse.Name.Contains(keyWord) ||
                      a.Warehouse.Number.Contains(keyWord) ||
                      a.Number.Contains(keyWord)||
                      a.SalesOrder.Number.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = datas };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string Erp_SalesReturnOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_PurchaseOrder = _Erp_SalesReturnOrderBLL.GetEntity(Erp_SalesReturnOrderId);
                return JsonSuccess(Erp_PurchaseOrder);
            });
        }

        public IActionResult SaveForm(Erp_SalesReturnOrder Erp_SalesReturnOrder)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_SalesReturnOrder.Erp_SalesReturnOrderId))
                {
                    _Erp_SalesReturnOrderBLL.AddSalesReturnOrder(Erp_SalesReturnOrder);
                }
                //}
                //else
                //{
                //    _Erp_PurchaseOrderBLL.UpdateEntity(Erp_PurchaseOrder);

                return JsonSuccess(null);
            });

        }

        public IActionResult GetSalesGoodsForm(string Erp_GoodsId)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_SalesGoodsBLL.GetEntity(a => a.GoodsId == Erp_GoodsId);
                return JsonSuccess(datas);


            });

        }

        public IActionResult RemoveForm(string Erp_SalesReturnOrderId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_SalesReturnOrderId))
                {

                    _Erp_SalesReturnOrderBLL.SetVoid(Erp_SalesReturnOrderId);
                    //return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });
        }

    }

}

