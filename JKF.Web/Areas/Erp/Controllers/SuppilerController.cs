
using JKF.BLL;
using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JKF.Web.Areas.Erp.Controllers
{
    [Area("Erp")]
    public class SuppilerController : BaseController
    {
        private Erp_SuppilerBLL _Erp_SuppilerBLL = new Erp_SuppilerBLL();

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
                
                var datas = _Erp_SuppilerBLL.GetEntities( a =>
                    true
                     ).ToList().Select(a=>new { ItemValue = a.Erp_SuppilerId,ItemName=a.Name+"("+a.Number+")"});

            
                return JsonSuccess(datas);
            });
        }



        public IActionResult GetErp_Suppilers(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Suppilers = _Erp_SuppilerBLL.GetEntities(pagination, sort, a =>
                     a.Number.Contains(keyWord) ||
                     a.Name.Contains(keyWord)

                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Suppilers };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string Erp_SuppilerId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_Suppiler = _Erp_SuppilerBLL.GetEntity(Erp_SuppilerId);
                return JsonSuccess(Erp_Suppiler);
            });
        }

        public IActionResult SaveForm(Erp_Suppiler Erp_Suppiler)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_Suppiler.Erp_SuppilerId))
                {
                    _Erp_SuppilerBLL.AddEntity(Erp_Suppiler);
                }
                else
                {
                    _Erp_SuppilerBLL.UpdateEntity(Erp_Suppiler);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string Erp_SuppilerId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_SuppilerId))
                {
                    string[] arr = Erp_SuppilerId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Erp_Suppiler> Erp_Suppilers = new List<Erp_Suppiler>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        Erp_Suppilers.Add(new Erp_Suppiler { Erp_SuppilerId = id });
                    }
                    _Erp_SuppilerBLL.RemoveEntities(Erp_Suppilers);
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
                    var ret = _Erp_SuppilerBLL.IsExists(a => a.Number == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_SuppilerBLL.IsExists(a => a.Number == paramValue && a.Erp_SuppilerId != keyValue);
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
                    var ret = _Erp_SuppilerBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_SuppilerBLL.IsExists(a => a.Name == paramValue && a.Erp_SuppilerId != keyValue);
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
