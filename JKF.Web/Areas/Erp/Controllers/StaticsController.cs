
using JKF.BLL;
using JKF.BLL.Base;
using JKF.BLL.Models;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace JKF.Web.Areas.Erp.Controllers
{
    [Area("Erp")]
    public class StaticsController : BaseController
    {
        private Erp_GoodsBLL _Erp_GoodsBLL = new Erp_GoodsBLL();
        private Erp_PurchaseOrderBLL _Erp_PurchaseOrderBLL = new Erp_PurchaseOrderBLL();
        private Erp_PurchaseGoodsBLL _Erp_PurchaseGoodsBLL = new Erp_PurchaseGoodsBLL();
        private Erp_PurchaseReturnGoodsBLL _Erp_PurchaseReturnGoodsBLL = new Erp_PurchaseReturnGoodsBLL();
        private Erp_SalesGoodsBLL _Erp_SalesGoodsBLL = new Erp_SalesGoodsBLL();
        private Erp_SalesReturnGoodsBLL _Erp_SalesReturnGoodsBLL = new Erp_SalesReturnGoodsBLL();
        private Erp_InventoryBLL _Erp_InventoryBLL = new Erp_InventoryBLL();

        public IActionResult PurchaseIndex()
        {
            return View();
        }

        public IActionResult SalesIndex()
        {
            return View();
        }

        public IActionResult PurchaseReturnIndex()
        {
            return View();
        }

        public IActionResult SalesReturnIndex()
        {
            return View();
        }

        public IActionResult InventoryIndex()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult SalesChart()
        {
            return View();
        }

        public IActionResult PurchaseReturnChart()
        {
            return View();
        }

        public IActionResult SalesReturnChart()
        {
            return View();
        }

        public IActionResult PurchaseChart()
        {
            return View();
        }


        public IActionResult GetInventorys(Pagination pagination, Sort sort, string keyWords)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_InventoryBLL.GetEntities(pagination, sort,a => true);
                var jsonData = new { pagination, sort, datas = datas };

                return JsonSuccess(jsonData);
            });
        }
        public IActionResult GetPurchaseGoodss(Pagination pagination, Sort sort, StaticsPurchaseQueryModel queryModel)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_PurchaseGoodsBLL.GetPurchseGoodsList(pagination, sort, queryModel);
                var jsonData = new { pagination, sort, datas = datas };

                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetPurchaseReturnGoodss(Pagination pagination, Sort sort, StaticsPurchaseReturnQueryModel queryModel)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_PurchaseReturnGoodsBLL.GetPurchaseReutrnGoodsList(pagination, sort, queryModel);
                var jsonData = new { pagination, sort, datas = datas };

                return JsonSuccess(jsonData);
            });
        }

        public IActionResult SumPurchase(StaticsPurchaseQueryModel queryModel)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_PurchaseGoodsBLL.SumPurchaseGoods( queryModel);
              
                return JsonSuccess(datas);
            });
        }

        public IActionResult SumPurchaseReturn(StaticsPurchaseReturnQueryModel queryModel)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_PurchaseReturnGoodsBLL.SumPurchaseReturnGoods(queryModel);

                return JsonSuccess(datas);
            });
        }


        public IActionResult GetSalesGoodss(Pagination pagination, Sort sort, StaticsSalesQueryModel queryModel)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_SalesGoodsBLL.GetSalesGoodsList(pagination, sort, queryModel);
                var jsonData = new { pagination, sort, datas = datas };

                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetSalesReturnGoodss(Pagination pagination, Sort sort, StaticsSalesReturnQueryModel queryModel)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_SalesReturnGoodsBLL.GetSalesReturnGoodsList(pagination, sort, queryModel);
                var jsonData = new { pagination, sort, datas = datas };

                return JsonSuccess(jsonData);
            });
        }

        public IActionResult SumSales(StaticsSalesQueryModel queryModel)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_SalesGoodsBLL.SumSalesGoods(queryModel);

                return JsonSuccess(datas);
            });
        }

        public IActionResult SumSalesReturn(StaticsSalesReturnQueryModel queryModel)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_SalesReturnGoodsBLL.SumSalesReturnGoods(queryModel);

                return JsonSuccess(datas);
            });
        }
        public IActionResult GetSalesStatics(int year)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_SalesGoodsBLL.GetSalesStatics(year);

                return JsonSuccess(datas);
            });
        }

        public IActionResult GetSalesReturnStatics(int year)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_SalesReturnGoodsBLL.GetSalesReturnStatics(year);

                return JsonSuccess(datas);
            });
        }

        public IActionResult GetPurchaseStatics(int year)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_PurchaseGoodsBLL.GetPurchaseStatics(year);

                return JsonSuccess(datas);
            });
        }

        public IActionResult GetPurchaseReturnStatics(int year)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_PurchaseReturnGoodsBLL.GetPurchaseReturnStatics(year);

                return JsonSuccess(datas);
            });
        }

    }
}
