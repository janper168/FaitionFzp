using JKF.BLL.Base;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web;

namespace JKF.Web.Areas.Base.Controllers
{
    [Area("Base")]
    public class CompanyController : BaseController
    {
        private CompanyBLL _companyBLL = new CompanyBLL();

        public IActionResult Index()
        {
            ViewBag.IsAdmin = new OperatorProvider().GetCurrent().IsSystem == true; 
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }
        [InterfaceDefine("/Base/User/GetCompanys", "JKF.DB.Models.Company")]
        public IActionResult GetCompanys(string keyWord)
        {
            return ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var resultDatas = _companyBLL.GetCompanys(keyWord);

                return JsonSuccess(resultDatas);
            });

        }
        public IActionResult GetCompanyForSelections(string keyWord)
        {
            return ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var resultDatas = _companyBLL.GetCompanys(keyWord);

                return JsonSuccess(resultDatas);
            });

        }

        public IActionResult GetForm(string companyId)
        {
            return ExecuteAction(() =>
            {
                Company company =  _companyBLL.GetCompany(companyId);
                return JsonSuccess(company);
            });
        }

        public IActionResult SaveForm(Company company)
        {
            return ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(company.CompanyId))
                {
                    _companyBLL.AddCompany(company);
                }
                else
                {
                    _companyBLL.UpdateCompany(company);
                }

                return JsonSuccess(null);
            });
        }

        public IActionResult RemoveForm(string companyId)
        {
            return ExecuteAction(() =>
            {
                int code = 0;
                string errMsg = "";

                if (!string.IsNullOrEmpty(companyId))
                {
                    if (_companyBLL.IsExists(a => a.ParentId == companyId))
                    {
                        code = 400;
                        errMsg = "存在下级公司，无法删除！";
                        return JsonFailure(code, errMsg);
                    }
                    else
                    {
                        _companyBLL.RemoveCompany(new Company { CompanyId = companyId });

                        return JsonSuccess(null);
                    }
                }
                return JsonSuccess(null);
            });

        }
        [HttpPost]
        public IActionResult IsExistEnCode(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _companyBLL.IsExists(a => a.EnCode == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _companyBLL.IsExists(a => a.EnCode == paramValue && a.CompanyId != keyValue);
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
        public IActionResult IsExistFullName(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _companyBLL.IsExists(a => a.FullName == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _companyBLL.IsExists(a => a.FullName == paramValue && a.CompanyId != keyValue);
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
        public IActionResult IsExistShortName(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _companyBLL.IsExists(a => a.ShortName == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _companyBLL.IsExists(a => a.ShortName == paramValue && a.CompanyId != keyValue);
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
