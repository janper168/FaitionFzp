
using JKF.BLL;
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
    public class GoodsController : BaseController
    {
        private Erp_GoodsBLL _Erp_GoodsBLL = new Erp_GoodsBLL();
        private Erp_PurchaseGoodsBLL _Erp_PurchaseGoodsBLL = new Erp_PurchaseGoodsBLL();
        private Erp_SalesGoodsBLL _Erp_SalesGoodsBLL = new Erp_SalesGoodsBLL();
        private Erp_BatchBLL _Erp_BatchBLL = new Erp_BatchBLL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Batch()
        {
            return View();
        }


        public IActionResult GetAll(string PurchaseOrderId,string SalesOrderId)
        {
            return this.ExecuteAction(() =>
            {
                dynamic datas = null;
                if (string.IsNullOrEmpty(PurchaseOrderId) && string.IsNullOrEmpty(PurchaseOrderId))
                {
                    datas = _Erp_GoodsBLL.GetEntities(a => true).ToList().
                        Select(a => new
                        {
                            ItemValue = a.Erp_GoodsId,
                            ItemName = a.Name + "(" + a.Number + ")"
                        });
                }
                else if (!string.IsNullOrEmpty(PurchaseOrderId))
                {
                    datas = _Erp_PurchaseGoodsBLL.GetEntities(a => a.PurchaseOrderId == PurchaseOrderId).ToList().
                        Select(a => new
                        {
                            ItemValue = a.GoodsId,
                            ItemName = a.Goods.Name + "(" + a.Goods.Number + ")"
                        });

                }
                else if (!string.IsNullOrEmpty(SalesOrderId))
                {
                    datas = _Erp_SalesGoodsBLL.GetEntities(a => a.SalesOrderId == SalesOrderId).ToList().
                        Select(a => new
                        {
                            ItemValue = a.GoodsId,
                            ItemName = a.Goods.Name + "(" + a.Goods.Number + ")"
                        });
                }

                return JsonSuccess(datas);
            });
        }

        public IActionResult GetBatchs(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var datas = _Erp_BatchBLL.GetEntities(pagination, sort, a =>
                     true
                     ).ToList();

                var jsonData = new { pagination, sort, datas = datas };
                return JsonSuccess(jsonData);
            });
        }
        

        public IActionResult GetErp_Goodss(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Goodss = _Erp_GoodsBLL.GetEntities(pagination, sort, a =>
                     a.Number.Contains(keyWord) ||
                     a.Name.Contains(keyWord) ||
                     a.Remark.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Goodss };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string Erp_GoodsId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_Goods = _Erp_GoodsBLL.GetEntity(Erp_GoodsId);
                return JsonSuccess(Erp_Goods);
            });
        }


        public IActionResult SaveForm(Erp_Goods Erp_Goods)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_Goods.Erp_GoodsId))
                {
                    _Erp_GoodsBLL.AddEntity(Erp_Goods);
                }
                else
                {
                    _Erp_GoodsBLL.UpdateEntity(Erp_Goods);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string Erp_GoodsId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_GoodsId))
                {
                    string[] arr = Erp_GoodsId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Erp_Goods> Erp_Goodss = new List<Erp_Goods>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        Erp_Goodss.Add(new Erp_Goods { Erp_GoodsId = id });
                    }
                    _Erp_GoodsBLL.RemoveEntities(Erp_Goodss);
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
                    var ret = _Erp_GoodsBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_GoodsBLL.IsExists(a => a.Name == paramValue && a.Erp_GoodsId != keyValue);
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
        public IActionResult IsExistNumber(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _Erp_GoodsBLL.IsExists(a => a.Number == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_GoodsBLL.IsExists(a => a.Number == paramValue && a.Erp_GoodsId != keyValue);
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
