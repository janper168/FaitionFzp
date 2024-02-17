using JKF.BLL.Base;
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
    public class AuthorizeController : BaseController
    {
        private AuthorizeBLL _authorizeBLL = new AuthorizeBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetModules()
        {
            return this.ExecuteAction(() =>
            {
                List<Module> modules;
                List<ModuleButton> moduleButtons;
                List<ModuleColumn> moduleColumns;
                List<ModuleForm> moduleForms;

                _authorizeBLL.GetAuthorizedModule(out modules,
                    out moduleButtons,
                    out moduleColumns,
                    out moduleForms);

                var datas = new { modules, moduleButtons, moduleColumns, moduleForms };

                return JsonSuccess(datas);

            });

        }

        public IActionResult GetAuthorizes(string objectId,int objectType)
        {
            return this.ExecuteAction(() =>
            {
                List<string> moduleIds;
                List<string> moduleButtonIds;
                List<string> moduleColumnIds;
                List<string> moduleFormIds;

                _authorizeBLL.GetAuthorzieItems(objectId, objectType, out moduleIds,
                    out moduleButtonIds,
                    out moduleColumnIds,
                    out moduleFormIds);

                var datas = new { moduleIds, moduleButtonIds, moduleColumnIds, moduleFormIds };

                return JsonSuccess(datas);

            });
        }


        public IActionResult SaveAuthorizeForm(string objectId, int objectType, List<Authorize> authorizeList)
        {
            return this.ExecuteAction(() =>
            {

                _authorizeBLL.UpdateAuthorize(objectId,
                        objectType, authorizeList);

                var jsonData = new { code = 200 };

                return JsonString(jsonData);

            });
        }
    }
}
