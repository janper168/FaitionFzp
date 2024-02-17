
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
namespace  JKF.Web.Areas.Erp.Controllers
{
    [Area("Erp")]
    public class GoodsCategoryController : BaseController
    {
        private Erp_GoodsCategoryBLL _Erp_GoodsCategoryBLL = new Erp_GoodsCategoryBLL();

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

                var datas = _Erp_GoodsCategoryBLL.GetEntities(a =>
                true
                ).ToList().Select(a => new { ItemValue = a.Erp_GoodsCategoryId, ItemName=a.Name});

                return JsonSuccess(datas);
            });
        }

        public IActionResult GetErp_GoodsCategorys(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_GoodsCategorys = _Erp_GoodsCategoryBLL.GetEntities(pagination, sort, a =>
                     a.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_GoodsCategorys };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string Erp_GoodsCategoryId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_GoodsCategory = _Erp_GoodsCategoryBLL.GetEntity(Erp_GoodsCategoryId);
                return JsonSuccess(Erp_GoodsCategory);
            });
        }

        public IActionResult SaveForm(Erp_GoodsCategory Erp_GoodsCategory)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_GoodsCategory.Erp_GoodsCategoryId))
                {
                    _Erp_GoodsCategoryBLL.AddEntity(Erp_GoodsCategory);
                }
                else
                {
                    _Erp_GoodsCategoryBLL.UpdateEntity(Erp_GoodsCategory);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string Erp_GoodsCategoryId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_GoodsCategoryId))
                {
                    string[] arr = Erp_GoodsCategoryId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Erp_GoodsCategory> Erp_GoodsCategorys = new List<Erp_GoodsCategory>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        Erp_GoodsCategorys.Add(new Erp_GoodsCategory { Erp_GoodsCategoryId = id });
                    }
                    _Erp_GoodsCategoryBLL.RemoveEntities(Erp_GoodsCategorys);
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
                    var ret = _Erp_GoodsCategoryBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_GoodsCategoryBLL.IsExists(a => a.Name == paramValue && a.Erp_GoodsCategoryId == keyValue);
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
