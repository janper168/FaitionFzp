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
    public class DepartmentController : BaseController
    {
        private DepartmentBLL _departmentBLL = new DepartmentBLL();
        private CompanyBLL _companyBLL = new CompanyBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        [InterfaceDefine("/Base/User/GetDepartments", "JKF.DB.Models.Department")]
        public IActionResult GetDepartments(string keyWord, string companyId)
        {
            return ExecuteAction(() =>
            {
                var resultDepartments = _departmentBLL.GetDepartments(keyWord, companyId);
                return JsonSuccess(resultDepartments);

            });
        }

        public IActionResult GetDepartmentsForSelection(string keyWord, string companyId)
        {
            return ExecuteAction(() =>
            {
                var resultDepartments = _departmentBLL.GetDepartments(keyWord, companyId);
                return JsonSuccess(resultDepartments);

            });
        }

        public IActionResult GetForm(string departmentId)
        {
            return ExecuteAction(() =>
            {

                Department department = null;

                department = _departmentBLL.GetDepartment(departmentId);

                return JsonSuccess(department);


            });

        }

        public IActionResult SaveForm(Department department, string companyId)
        {

            return ExecuteAction(() =>
            {

                var company = _departmentBLL.GetCompany(companyId, beTraced: true);
                if (company == null)
                {
                    throw new Exception("公司数据出错：找不到");
                }

                if (string.IsNullOrEmpty(department.DepartmentId))
                {
                    department.Company = company;
                    _departmentBLL.AddDepartment(department);
                }
                else
                {
                    department.Company = company;
                    _departmentBLL.UpdateDepartment(department);
                }


                return JsonSuccess(null);

            });

        }

        public IActionResult RemoveForm(string departmentId)
        {

            return ExecuteAction(() =>
            {

                int code = 0;
                string errMsg = "";

                if (!string.IsNullOrEmpty(departmentId))
                {
                    if (_departmentBLL.IsExists(a => a.ParentId == departmentId))
                    {
                        code = 400;
                        errMsg = "存在下级部门，无法删除！";
                        return JsonFailure(code, errMsg);

                    }
                    else
                    {
                        _departmentBLL.RemoveDepartment(new Department { DepartmentId = departmentId });
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
                    var ret = _departmentBLL.IsExists(a => a.EnCode == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _departmentBLL.IsExists(a => a.EnCode == paramValue && a.DepartmentId != keyValue);
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
                    var ret = _departmentBLL.IsExists(a => a.FullName == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _departmentBLL.IsExists(a => a.FullName == paramValue && a.DepartmentId != keyValue);
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
                    var ret = _departmentBLL.IsExists(a => a.ShortName == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _departmentBLL.IsExists(a => a.ShortName == paramValue && a.DepartmentId != keyValue);
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
