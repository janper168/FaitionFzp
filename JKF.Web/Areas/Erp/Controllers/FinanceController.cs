
using JKF.BLL;
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
    public class FinanceController : BaseController
    {
        private Erp_SuppilerBLL _Erp_SuppilerBLL = new Erp_SuppilerBLL();
        private Erp_CustomerBLL _Erp_CustomerBLL = new Erp_CustomerBLL();
        private Erp_PaymentOrderBLL _Erp_PaymentOrderBLL = new Erp_PaymentOrderBLL();
        private Erp_CollectionOrderBLL _Erp_CollectionOrderBLL = new Erp_CollectionOrderBLL();
        private Erp_FinanceFlowBLL _Erp_FinanceFlowBLL = new Erp_FinanceFlowBLL();
        public IActionResult ArearsPayable()
        {
            return View();
        }
        public IActionResult ArearsReceivable()
        {
            return View();
        }

        public IActionResult PaymentIndex()
        {
            return View();
        }

        public IActionResult CollectionIndex()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult AddPaymentAccountIndex()
        {
            return View();
        }

        public IActionResult AddPaymentAccountForm()
        {
            return View();
        }

        public IActionResult AddCollectionAccountIndex()
        {
            return View();
        }

        public IActionResult AddCollectionAccountForm()
        {
            return View();
        }

        public IActionResult FinanceFlowForm()
        {
            return View();
        }

        public IActionResult GetPaymentOrderNo()
        {
            return this.ExecuteAction(() =>
            {
                string ret = new Erp_BillNoBLL().GetNextBillNo("FK");
                return JsonSuccess(ret);
            });
        }

        public IActionResult GetCollectionOrderNo()
        {
            return this.ExecuteAction(() =>
            {
                string ret = new Erp_BillNoBLL().GetNextBillNo("SK");
                return JsonSuccess(ret);
            });
        }

        public IActionResult GetErp_FinanceFlows(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";
                var datas = _Erp_FinanceFlowBLL.GetEntities(pagination, sort, 
                    a => 
                    a.Account.Number.Contains(keyWord)||
                    a.Account.Name.Contains(keyWord)
                    );

                var jsonData = new { pagination, sort, datas = datas };
                return JsonSuccess(jsonData);
            });
        }


        public IActionResult GetArearsPayable(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allFinances = _Erp_SuppilerBLL.GetEntities(pagination, sort, a =>
                     a.Number.Contains(keyWord) ||
                     a.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allFinances };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetArearsReceivable(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allFinances = _Erp_CustomerBLL.GetEntities(pagination, sort, a =>
                     a.Number.Contains(keyWord)||
                     a.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allFinances };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetErp_PaymentOrders(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allFinances = _Erp_PaymentOrderBLL.GetEntities(pagination, sort, a =>
                     a.Number.Contains(keyWord)||
                     a.Suppiler.Number.Contains(keyWord) ||
                     a.Suppiler.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allFinances };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetErp_CollectionOrders(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allFinances = _Erp_CollectionOrderBLL.GetEntities(pagination, sort, a =>
                      a.Number.Contains(keyWord) ||
                     a.Customer.Number.Contains(keyWord) ||
                     a.Customer.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allFinances };
                return JsonSuccess(jsonData);
            });
        }



        public IActionResult GetForm(string FinanceId)
        {
            return this.ExecuteAction(() =>
            {
                var Finance = _Erp_SuppilerBLL.GetEntity(FinanceId);
                return JsonSuccess(Finance);
            });
        }
        public IActionResult SavePaymentOrdeForm(Erp_PaymentOrder Erp_PaymentOrder)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_PaymentOrderBLL.SavePaymentOrderForm(Erp_PaymentOrder);
                return JsonSuccess(null);
            });
        }

        public IActionResult SetPaymentVoid(string Erp_PaymentOrderId)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_PaymentOrderBLL.SetVoid(Erp_PaymentOrderId);
                return JsonSuccess(null);
            });
        }

        public IActionResult SaveCollectionOrderForm(Erp_CollectionOrder Erp_CollectionOrder)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_CollectionOrderBLL.SaveCollectionOrderForm(Erp_CollectionOrder);
                return JsonSuccess(null);
            });
        }

        public IActionResult SetCollectionVoid(string Erp_CollectionOrderId)
        {
            return this.ExecuteAction(() =>
            {
                _Erp_CollectionOrderBLL.SetVoid(Erp_CollectionOrderId);
                return JsonSuccess(null);
            });
        }


    }
}