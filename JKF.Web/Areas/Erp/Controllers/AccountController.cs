
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
    public class AccountController : BaseController
    {
        private Erp_AccountBLL _Erp_AccountBLL = new Erp_AccountBLL();
        private Erp_AccountTransferRecordBLL _Erp_AccountTransferRecordBLL = new Erp_AccountTransferRecordBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult AccountTransferRecordIndex()
        {
            return View();
        }

        public IActionResult AccountTransferRecordForm()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            return this.ExecuteAction(() =>
            {

                var datas = _Erp_AccountBLL.GetEntities(a =>
                true
                ).ToList().Select(a => new { ItemValue = a.Erp_AccountId, ItemName = a.Name });

                return JsonSuccess(datas);
            });
        }


        public IActionResult GetErp_Accounts(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Accounts = _Erp_AccountBLL.GetEntities(pagination, sort, a =>
                     a.Number.Contains(keyWord) ||
                     a.Name.Contains(keyWord)
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Accounts };
                return JsonSuccess(jsonData);
            });
        }
        public IActionResult GetAccountTransferRecords(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var allErp_Accounts = _Erp_AccountTransferRecordBLL.GetEntities(pagination, sort, a =>
                      a.InAccount.Number.Contains(keyWord) ||
                     a.InAccount.Name.Contains(keyWord) ||
                     a.OutAccount.Number.Contains(keyWord)|| 
                     a.OutAccount.Name.Contains(keyWord) 
                     ).ToList();

                var jsonData = new { pagination, sort, datas = allErp_Accounts };
                return JsonSuccess(jsonData);
            });
        }

        public IActionResult SaveAccountTransferRecordForm(Erp_AccountTransferRecord AccountTransferRecord)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(AccountTransferRecord.Erp_AccountTransferRecordId))
                {
                    _Erp_AccountTransferRecordBLL.AddEntity(AccountTransferRecord);
                }
              
                return JsonSuccess(null);
            });

        }

        public IActionResult SetAccountTransferRecordVoid(string Erp_AccountTransferRecordId )
        {
            return this.ExecuteAction(() =>
            {
                
                _Erp_AccountTransferRecordBLL.SetVoid(Erp_AccountTransferRecordId);

                return JsonSuccess(null);
            });

        }




        public IActionResult GetForm(string Erp_AccountId)
        {
            return this.ExecuteAction(() =>
            {
                var Erp_Account = _Erp_AccountBLL.GetEntity(Erp_AccountId);
                return JsonSuccess(Erp_Account);
            });
        }

        public IActionResult SaveForm(Erp_Account Erp_Account)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(Erp_Account.Erp_AccountId))
                {
                    _Erp_AccountBLL.AddEntity(Erp_Account);
                }
                else
                {
                    _Erp_AccountBLL.UpdateEntity(Erp_Account);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string Erp_AccountId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(Erp_AccountId))
                {
                    string[] arr = Erp_AccountId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Erp_Account> Erp_Accounts = new List<Erp_Account>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        Erp_Accounts.Add(new Erp_Account { Erp_AccountId = id });
                    }
                    _Erp_AccountBLL.RemoveEntities(Erp_Accounts);
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
                    var ret = _Erp_AccountBLL.IsExists(a => a.Number == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_AccountBLL.IsExists(a => a.Number == paramValue && a.Erp_AccountId != keyValue);
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
                    var ret = _Erp_AccountBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _Erp_AccountBLL.IsExists(a => a.Name == paramValue && a.Erp_AccountId != keyValue);
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
