
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
    public class CustomerController : BaseController
    {
        private Erp_CustomerBLL _Erp_CustomerBLL = new Erp_CustomerBLL();

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

                var datas = _Erp_CustomerBLL.GetEntities(a =>
                     true
                     ).ToList().Select(a => new { ItemValue = a.Erp_CustomerId, ItemName = a.Name + "(" + a.Number + ")" });


                return JsonSuccess(datas);
            });
        }



        public IActionResult GetErp_Customers(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Customers = _Erp_CustomerBLL.GetEntities(pagination, sort, a =>
                     a.Number.Contains(keyWord)||
                     a.Name.Contains(keyWord) || 
                     a.Remark.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Customers };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string Erp_CustomerId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_Customer = _Erp_CustomerBLL.GetEntity(Erp_CustomerId);
                return JsonSuccess(Erp_Customer);
            });
        }

        public IActionResult SaveForm(Erp_Customer Erp_Customer)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_Customer.Erp_CustomerId))
                {
                    _Erp_CustomerBLL.AddEntity(Erp_Customer);
                }
                else
                {
                    _Erp_CustomerBLL.UpdateEntity(Erp_Customer);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string Erp_CustomerId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_CustomerId))
                {
                    string[] arr = Erp_CustomerId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Erp_Customer> Erp_Customers = new List<Erp_Customer>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        Erp_Customers.Add(new Erp_Customer { Erp_CustomerId = id });
                    }
                    _Erp_CustomerBLL.RemoveEntities(Erp_Customers);
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
                    var ret = _Erp_CustomerBLL.IsExists(a => a.Number == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_CustomerBLL.IsExists(a => a.Number == paramValue && a.Erp_CustomerId != keyValue);
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
                    var ret = _Erp_CustomerBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_CustomerBLL.IsExists(a => a.Name == paramValue && a.Erp_CustomerId != keyValue);
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
