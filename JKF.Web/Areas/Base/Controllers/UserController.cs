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
    public class UserController : BaseController
    {
        private UserBLL _userBLL = new UserBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        [InterfaceDefine("/Base/User/GetUsers","JKF.DB.Models.User")]
        public IActionResult GetUsers(Pagination pagination, Sort sort, string keyWord, string departmentId)
        {
            return this.ExecuteAction(() =>
            {

                keyWord = keyWord ?? "";


                var keyWordUsers = _userBLL.GetUsers(pagination,  sort, a =>
                  (a.EnCode.Contains(keyWord) ||
                  a.Account.Contains(keyWord) ||
                  a.RealName.Contains(keyWord) ||
                  //a.RoleList.Where(a=>a.Name.Contains(keyWord)).Count() > 0 ||
                  a.Description.Contains(keyWord)) &&
                  (a.EnabledMark == 1 && a.DeleteMark == 0) &&
                   a.Department.DepartmentId == departmentId &&
                   a.Account != "admin"
                 );


                var jsonData = new { pagination, sort, datas = keyWordUsers };

                return JsonSuccess(jsonData);
            });
        }


        public IActionResult GetAllUsersWithParam(string keyWord, string departmentId)
        {

            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var keyWordUsers = _userBLL.GetUsers(a =>
                 (a.EnCode.Contains(keyWord) ||
                 a.Account.Contains(keyWord) ||
                 a.RealName.Contains(keyWord) ||
                 //a.RoleList.Where(a => a.Name.Contains(keyWord)).Count() > 0 ||
                 a.Description.Contains(keyWord)) &&
                 (a.EnabledMark == 1 && a.DeleteMark == 0) &&
                  a.Department.DepartmentId == departmentId &&
                  a.Account != "admin"
                 );

                return JsonSuccess(keyWordUsers);
            });

        }
        [HttpPost]
        public IActionResult GetAllUsers()
        {

            return this.ExecuteAction(() =>
            {

                var allDatas = _userBLL.GetUsers(a => a.Account != "admin" );
                var retDatas = allDatas.Select(a => new { UserId = a.UserId, UserName = a.RealName + "(" + a.EnCode + "," + a.Department.FullName + "," + a.Department.Company.FullName + ")" });

                return JsonSuccess(retDatas);
            });

        }

        public IActionResult GetForm(string userId)
        {
            return this.ExecuteAction(() =>
            {
                var user = _userBLL.GetUser(userId);
                return JsonSuccess(user);
            });
        }

        public IActionResult SaveForm(User user, string departmentId)
        {

            return this.ExecuteAction(() =>
            {
                var department = _userBLL.GetDepartment(departmentId, beTraced: true);
                if (department == null)
                {
                    throw new Exception("部门数据出错：找不到");
                }

                if (string.IsNullOrEmpty(user.UserId))
                {
                    user.Department = department;
                    _userBLL.AddUser(user);
                }
                else
                {
                    user.Department = department;
                    _userBLL.UpdateUser(user);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string userId)
        {
            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    string[] arr = userId.Split(',',StringSplitOptions.RemoveEmptyEntries);
                    List<User> users = new List<User>();
                    if (arr.Length <= 0) {
                        return JsonSuccess(null);
                    }
                    
                    foreach (var id in arr)
                    {
                        users.Add(new User { UserId = id });
                    }
                    _userBLL.RemoveUsers(users);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });

        }

        public IActionResult IsExistEnCode(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _userBLL.IsExists(a => a.EnCode == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _userBLL.IsExists(a => a.EnCode == paramValue && a.UserId != keyValue);
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
        public IActionResult IsExistAccount(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _userBLL.IsExists(a => a.Account == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _userBLL.IsExists(a => a.Account == paramValue && a.UserId != keyValue);
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
