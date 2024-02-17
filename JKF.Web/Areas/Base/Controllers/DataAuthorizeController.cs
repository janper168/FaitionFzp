using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JKF.Web.Areas.Base.Controllers
{
    [Area("Base")]
    public class DataAuthorizeController : BaseController
    {
        private DataAuthorizeBLL _dataAuthorizeBLL = new DataAuthorizeBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult GetDataAuthorizes(Pagination pagination, Sort sort, string keyWord, string objectId, int objectType)
        {
            return this.ExecuteAction(() =>
            {

                keyWord = keyWord ?? "";

                var datas = _dataAuthorizeBLL.GetDataAuthorizes(pagination, sort, a =>
                    (a.DeleteMark == 0 &&
                        a.ObjectId == objectId && a.ObjectType == objectType
                        ) && (a.Interface.Name.Contains(keyWord) ||
                            a.Interface.Address.Contains(keyWord))
                    ).ToList();


                var jsonData = new { pagination, sort, datas };
                return JsonSuccess(jsonData);
            });

        }

        public IActionResult GetForm(string dataAuthorizeId)
        {
            return this.ExecuteAction(() =>
            {
                var dataAuthorize = _dataAuthorizeBLL.GetDataAuthorize(dataAuthorizeId);
                return JsonSuccess(dataAuthorize);
            });
        }

        public IActionResult SaveForm(DataAuthorize dataAuthorize)
        {
            return this.ExecuteAction(() =>
            {

                var interfaceObject = _dataAuthorizeBLL.GetInterface(dataAuthorize.Interface.InterfaceId, beTraced: true);

                if (string.IsNullOrEmpty(dataAuthorize.DataAuthorizeId))
                {
                    dataAuthorize.Interface = interfaceObject;
                    _dataAuthorizeBLL.AddDataAuthorize(dataAuthorize);
                }
                else
                {
                    dataAuthorize.Interface = interfaceObject;
                    _dataAuthorizeBLL.UpdateDataAuthorize(dataAuthorize);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string dataAuthorizeId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(dataAuthorizeId))
                {
                    string[] arr = dataAuthorizeId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<DataAuthorize> dataAuthorizes = new List<DataAuthorize>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        dataAuthorizes.Add(new DataAuthorize { DataAuthorizeId = id });
                    }
                    _dataAuthorizeBLL.RemoveDataAuthorizes(dataAuthorizes);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });

        }

       

        //[HttpPost]
        //public IActionResult IsExistEnCode(string keyValue, string paramValue)
        //{
        //    try
        //    {
        //        if (keyValue.IsEmpty())
        //        {
        //            var ret = _roleBLL.IsExists(a => a.EnCode == paramValue);
        //            if (ret)
        //            {
        //                return JsonFailure(400, "已经存在");
        //            }
        //            return JsonSuccess(null);
        //        }
        //        else
        //        {
        //            var ret = _roleBLL.IsExists(a => a.EnCode == paramValue && a.RoleId != keyValue);
        //            if (ret)
        //            {
        //                return JsonFailure(400, "已经存在");
        //            }
        //            return JsonSuccess(null);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonFailure(500, ex.Message);
        //    }
        //}

        //[HttpPost]
        //public IActionResult IsExistName(string keyValue, string paramValue)
        //{
        //    try
        //    {
        //        if (keyValue.IsEmpty())
        //        {
        //            var ret = _roleBLL.IsExists(a => a.Name == paramValue);
        //            if (ret)
        //            {
        //                return JsonFailure(400, "已经存在");
        //            }
        //            return JsonSuccess(null);
        //        }
        //        else
        //        {
        //            var ret = _roleBLL.IsExists(a => a.Name == paramValue && a.RoleId != keyValue);
        //            if (ret)
        //            {
        //                return JsonFailure(400, "已经存在");
        //            }
        //            return JsonSuccess(null);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonFailure(500, ex.Message);
        //    }
        //}

    }
}
