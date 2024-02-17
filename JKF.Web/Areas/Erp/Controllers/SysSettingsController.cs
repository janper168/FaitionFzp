
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
    public class SysSettingsController : BaseController
    {
        private Erp_SysSettingsBLL _Erp_SysSettingsBLL = new Erp_SysSettingsBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult GetSysSettingss(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_SysSettingss = _Erp_SysSettingsBLL.GetEntities(pagination, sort, a =>
                     a.KeyName.Contains(keyWord)||
                     a.KeyValue.Contains(keyWord)

                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_SysSettingss };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult GetForm(string Erp_SysSettingsId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_SysSettings = _Erp_SysSettingsBLL.GetEntity(Erp_SysSettingsId);
                return JsonSuccess(Erp_SysSettings);
            });
        }

        public IActionResult SaveForm(Erp_SysSettings Erp_SysSettings)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_SysSettings.Erp_SysSettingsId))
                {
                    _Erp_SysSettingsBLL.AddEntity(Erp_SysSettings);
                }
                else
                {
                    _Erp_SysSettingsBLL.UpdateEntity(Erp_SysSettings);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string Erp_SysSettingsId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_SysSettingsId))
                {
                    string[] arr = Erp_SysSettingsId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Erp_SysSettings> Erp_SysSettingss = new List<Erp_SysSettings>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        Erp_SysSettingss.Add(new Erp_SysSettings { Erp_SysSettingsId = id });
                    }
                    _Erp_SysSettingsBLL.RemoveEntities(Erp_SysSettingss);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });
        }

        public IActionResult IsExistKeyName(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _Erp_SysSettingsBLL.IsExists(a => a.KeyName == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_SysSettingsBLL.IsExists(a => a.KeyName == paramValue && a.Erp_SysSettingsId != keyValue);
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

