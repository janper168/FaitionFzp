using JKF.BLL.Base;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Areas.Base.Models;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JKF.Web.Areas.Base.Controllers
{
    [Area("Base")]
    public class ModuleController : BaseController
    {
        private readonly ModuleBLL _moduleBLL = new ModuleBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult ModuleButtonForm()
        {
            return View();
        }

        public IActionResult ModuleColumnForm()
        {
            return View();
        }

        public IActionResult ModuleFormForm()
        {
            return View();
        }

        public IActionResult GetModules(string keyWord)
        {
            return ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";
                var resultModuels = _moduleBLL.GetModules(keyWord);
                return JsonSuccess(resultModuels);
            });
        }


        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMenuList()
        {
            return ExecuteAction(() =>
            {
                var data = _moduleBLL.GetAuthorizedModules().ToList();

                var modules = ConvertMouleListToMenus(data);

                return JsonSuccess(modules);
            });
        }


        private List<MenuModel> ConvertMouleListToMenus(List<Module> modules, string parentId = "0")
        {
            List<MenuModel> retData = new List<MenuModel>();

            var data = modules.FindAll(a => a.ParentId == parentId);
            if (data != null && data.Count > 0)
            {
                for (var i = 0; i < data.Count; i++)
                {
                    var menuModel = ConvertModuleToMenu(modules, data[i]);
                    retData.Add(menuModel);
                }
            }

            return retData;
        }

        private MenuModel ConvertModuleToMenu(List<Module> list, Module module)
        {
            if (module != null)
            {
                MenuModel model = new MenuModel();
                model.menuId = module.ModuleId;
                model.menuName = module.Name;
                model.icon = module.Icon != null ? module.Icon.Replace("fa ", "") : null;
                model.url = module.UrlAddress;
                model.children = ConvertMouleListToMenus(list, module.ModuleId);
                return model;
            }
            return null;
        }

        public IActionResult GetAuthorizedObjects(string moduleId)
        {
            return this.ExecuteAction(() =>
            {
                List<ModuleButton> authorizedButtons = _moduleBLL.GetAuthorizedModuleButtons(moduleId).ToList();
                List<ModuleColumn> authorizedColumns = _moduleBLL.GetAuthorizedModuleColumns(moduleId).ToList();
                List<ModuleForm> authorizedForms = _moduleBLL.GetAuthorizedModuleForms(moduleId).ToList();

                return JsonSuccess(new { authorizedButtons, authorizedColumns, authorizedForms });

            });

        }

        public IActionResult SaveForm(Module module)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(module.ModuleId))
                {
                    _moduleBLL.AddModule(module);
                }
                else
                {
                    _moduleBLL.UpdateModule(module);
                }

                return JsonSuccess(null);

            });
        }




        public IActionResult RemoveForm(string moduleId)
        {
            return this.ExecuteAction(() =>
            {
                int code = 0;
                string errMsg = "";
                if (!string.IsNullOrEmpty(moduleId))
                {
                    if (_moduleBLL.IsExists(a => a.ParentId == moduleId))
                    {
                        code = 400;
                        errMsg = "存在下级菜单，无法删除！";
                        return JsonFailure(code, errMsg);
                    }
                    else
                    {
                        _moduleBLL.RemoveModule(new Module { ModuleId = moduleId });
                        return JsonSuccess(null);
                    }
                }
                return JsonSuccess(null);

            });

        }

        public IActionResult GetForm(string moduleId)
        {
            return this.ExecuteAction(() =>
            {

                Module module = null;

                module = _moduleBLL.GetModule(moduleId);


                return JsonSuccess(module);

            });

        }
        
        [HttpPost]
        public IActionResult IsExistEnCode(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _moduleBLL.IsExists(a => a.EnCode == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _moduleBLL.IsExists(a => a.EnCode == paramValue && a.ModuleId != keyValue);
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
                    var ret = _moduleBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _moduleBLL.IsExists(a => a.Name == paramValue && a.ModuleId != keyValue);
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
