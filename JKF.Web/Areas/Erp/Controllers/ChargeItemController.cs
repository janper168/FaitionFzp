
using JKF.BLL;
using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace JKF.Web.Areas.Erp.Controllers
{
    [Area("Erp")]
    public class ChargeItemController : BaseController
    {
        private Erp_ChargeItemBLL _Erp_ChargeItemBLL = new Erp_ChargeItemBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            return this.ExecuteAction(() =>
            {
                var datas = _Erp_ChargeItemBLL.GetEntities(a => true).Select(a=>new { ItemValue = a.Erp_ChargeItemId, ItemName = a.Name });

                return JsonSuccess(datas);
            });
        }

        public IActionResult GetErp_ChargeItems(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_ChargeItems = _Erp_ChargeItemBLL.GetEntities(pagination, sort, a =>
                     a.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_ChargeItems };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string Erp_ChargeItemId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_ChargeItem = _Erp_ChargeItemBLL.GetEntity(Erp_ChargeItemId);
                return JsonSuccess(Erp_ChargeItem);
            });
        }

        public IActionResult SaveForm(Erp_ChargeItem Erp_ChargeItem)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_ChargeItem.Erp_ChargeItemId))
                {
                    _Erp_ChargeItemBLL.AddEntity(Erp_ChargeItem);
                }
                else
                {
                    _Erp_ChargeItemBLL.UpdateEntity(Erp_ChargeItem);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string Erp_ChargeItemId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_ChargeItemId))
                {
                    string[] arr = Erp_ChargeItemId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Erp_ChargeItem> Erp_ChargeItems = new List<Erp_ChargeItem>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        Erp_ChargeItems.Add(new Erp_ChargeItem { Erp_ChargeItemId = id });
                    }
                    _Erp_ChargeItemBLL.RemoveEntities(Erp_ChargeItems);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });

        }


        [HttpPost]
        public IActionResult IsExistName(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _Erp_ChargeItemBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_ChargeItemBLL.IsExists(a => a.Name == paramValue && a.Erp_ChargeItemId != keyValue);
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
    }
}
