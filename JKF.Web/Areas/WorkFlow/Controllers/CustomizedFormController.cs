using JKF.Utils;
using JKF.BLL;
using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.Formula.Functions;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace JKF.Web.Areas.WorkFlow.Controllers {
   
    [Area("WorkFlow")]
    public class CustomizedFormController : BaseController
    {
        private CustomizedFormBLL _CustomizedFormBLL = new CustomizedFormBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult DesignForm()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            return this.ExecuteAction(() =>
            {

                var datas = _CustomizedFormBLL.GetEntities(a => true);

                var ret = datas.Select(a => new { ItemValue = a.CustomizedFormId, ItemName=a.FormName}).ToList();
                
                return JsonSuccess(ret);
            });
        }


        public IActionResult GetCustomizedForms(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allCustomizedForms = _CustomizedFormBLL.GetEntities(pagination, sort, a =>
                     true
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allCustomizedForms };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string CustomizedFormId)
        {
            return this.ExecuteAction(() =>
            {
                var CustomizedForm = _CustomizedFormBLL.GetEntity(CustomizedFormId);
                return JsonSuccess(CustomizedForm);
            });
        }

        public IActionResult SaveForm(CustomizedForm CustomizedForm)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(CustomizedForm.CustomizedFormId))
                {
                    _CustomizedFormBLL.AddEntity(CustomizedForm);
                }
                else
                {
                    _CustomizedFormBLL.UpdateEntity(CustomizedForm);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string CustomizedFormId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(CustomizedFormId))
                {
                    string[] arr = CustomizedFormId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<CustomizedForm> CustomizedForms = new List<CustomizedForm>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        CustomizedForms.Add(new CustomizedForm { CustomizedFormId = id });
                    }
                    _CustomizedFormBLL.RemoveEntities(CustomizedForms);
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
                    var ret = _CustomizedFormBLL.IsExists(a => a.FormName == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _CustomizedFormBLL.IsExists(a => a.FormName == paramValue && a.CustomizedFormId != keyValue);
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


