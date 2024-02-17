using JKF.BLL.Base;
using JKF.Commons;
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
    public class RoleController : BaseController
    {
        private RoleBLL _roleBLL = new RoleBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult RoleMemberForm()
        {
            return View();
        }

        [InterfaceDefine("/Base/User/GetRoles", "JKF.DB.Models.Role")]
        public IActionResult GetRoles(Pagination pagination, Sort sort,string keyWord)
        {
            return this.ExecuteAction(() =>
            {

                keyWord = keyWord ?? "";

                var allRoles = _roleBLL.GetRoles(pagination, sort, a =>
                    (a.DeleteMark == 0 && a.EnabledMark == 1) && (
                     a.EnCode.Contains(keyWord) ||
                     a.Name.Contains(keyWord) ||
                     a.Description.Contains(keyWord)
                     )).ToList();


                var jsonData =  new { pagination, sort, datas = allRoles  };
                return JsonSuccess(jsonData);
            });
 
        }

        public IActionResult GetRoleUsers(string roleId)
        {
            return this.ExecuteAction(() =>
            {
                var users = _roleBLL.GetRoleUsers(roleId);

                return JsonSuccess(users);
            });

              
        }

        public IActionResult GetForm(string roleId)
        {
            return this.ExecuteAction(() =>
            {
                var role = _roleBLL.GetRole(roleId);
                return JsonSuccess(role);
            });

 
        }

        public IActionResult SaveForm(Role role)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(role.RoleId))
                {
                    _roleBLL.AddRole(role);
                }
                else
                {
                    _roleBLL.UpdateRole(role);
                }
                return JsonSuccess(null);
            });
               
        }

        public IActionResult RemoveForm(string roleId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(roleId))
                {
                    string[] arr = roleId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Role> roles = new List<Role>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        roles.Add(new Role { RoleId = id });
                    }
                    _roleBLL.RemoveRoles(roles);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });

        }

        public IActionResult SaveMemberForm(Role role)
        {
            return this.ExecuteAction(() =>
            {
                _roleBLL.UpdateRoleUsers(role);
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
                    var ret = _roleBLL.IsExists(a => a.EnCode == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _roleBLL.IsExists(a => a.EnCode == paramValue && a.RoleId != keyValue);
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
                    var ret = _roleBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _roleBLL.IsExists(a => a.Name == paramValue && a.RoleId != keyValue);
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
