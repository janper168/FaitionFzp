
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
    public class ChargeController : BaseController
    {
        private Erp_ChargeOrderBLL _Erp_ChargeOrderBLL = new Erp_ChargeOrderBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult GetChargeOrderNo()
        {
            return this.ExecuteAction(() =>
            {
                string ret = new Erp_BillNoBLL().GetNextBillNo("SZ");
                return JsonSuccess(ret);
            });
        }

        public IActionResult GetChargeOrders(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_ChargeOrders = _Erp_ChargeOrderBLL.GetEntities(pagination, sort, a =>
                     a.Number.Contains(keyWord) ||
                     a.Remark.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_ChargeOrders };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string Erp_ChargeOrderId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_ChargeOrder = _Erp_ChargeOrderBLL.GetEntity(Erp_ChargeOrderId);
                return JsonSuccess(Erp_ChargeOrder);
            });
        }

        public IActionResult SaveForm(Erp_ChargeOrder Erp_ChargeOrder)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_ChargeOrder.Erp_ChargeOrderId))
                {
                    _Erp_ChargeOrderBLL.AddEntity(Erp_ChargeOrder);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult SetVoid(string Erp_ChargeOrderId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_ChargeOrderId))
                {
                    _Erp_ChargeOrderBLL.SetVoid(Erp_ChargeOrderId);
                    return JsonSuccess(null);
                }
                return JsonSuccess(null);
            });
        }

    }
}
