using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Base
{
    public class AuthorizeBLL
    {
        private BaseDbContext _dbContext = null;
        private AuthorizeService _authorizeService = null;
        private ModuleService _moduleService = null;
        private ModuleButtonService _moduleButtonService = null;
        private ModuleColumnService _moduleColumnService = null;
        private ModuleFormService _moduleFormService = null;

        public AuthorizeBLL()
        {
            _dbContext = new BaseDbContext();
            _authorizeService = new AuthorizeService(_dbContext);
            _moduleService = new ModuleService(_dbContext);
            _moduleButtonService = new ModuleButtonService(_dbContext);
            _moduleColumnService = new ModuleColumnService(_dbContext);
            _moduleFormService = new ModuleFormService(_dbContext);

        }

        public void GetAuthorizedModule(out List<Module> modules,
            out List<ModuleButton> moduleButtons,
            out List<ModuleColumn> moduleColumns,
            out List<ModuleForm> moduleForms)
        {
            OperatorModel om = new OperatorProvider().GetCurrent();

            List<string> roleIds = om.RoleIds.Split(",",StringSplitOptions.RemoveEmptyEntries).ToList();
            string userId = om.UserId;

            List<string> authorizeList = null;
            modules = _moduleService.FindList(a => a.EnabledMark == 1, a => a.Sort).ToList();
            if (!om.IsSystem)
            {
                List<Module> resultModules = new List<Module>();
                //找出"我"的所有权限
                authorizeList = _authorizeService.FindList(
                                        a => ((a.ObjectId == userId
                                          && a.ObjectType == 2) ||
                                        (roleIds.Contains(a.ObjectId) &&
                                          a.ObjectType == 1))
                                        && a.ItemType == 1,
                                        a => a.AuthorizeId).Select(x => x.ItemId).Distinct().ToList();

                foreach (var auth in authorizeList)
                {
                    var module = modules.SingleOrDefault(a => a.ModuleId == auth);
                    if (module != null)
                    {
                        resultModules.Add(module);
                    }
                }
                modules = resultModules.OrderBy(a => a.Sort).ToList();
                
            }

            moduleButtons = _moduleButtonService.FindList(a => a.EnabledMark == 1, a=>a.Module, a => a.EnCode).ToList();
            if (!om.IsSystem)
            {
                List<ModuleButton> resultModuleButtons = new List<ModuleButton>();
                //找出"我"的所有权限
                authorizeList = _authorizeService.FindList(
                                        a => ((a.ObjectId == userId
                                          && a.ObjectType == 2) ||
                                        (roleIds.Contains(a.ObjectId) &&
                                          a.ObjectType == 1))
                                        && a.ItemType == 2,
                                        a => a.AuthorizeId).Select(x => x.ItemId).Distinct().ToList();

                foreach (var auth in authorizeList)
                {
                    var moduleButton = moduleButtons.SingleOrDefault(a => a.ModuleButtonId == auth);
                    if (moduleButton != null)
                    {
                        resultModuleButtons.Add(moduleButton);
                    }
                }
                moduleButtons = resultModuleButtons;

            }


            moduleColumns = _moduleColumnService.FindList(a => a.EnabledMark == 1, a => a.Module, a => a.EnCode).ToList();

            if (!om.IsSystem)
            {
                List<ModuleColumn> resultModuleColumns = new List<ModuleColumn>();
                //找出"我"的所有权限
                authorizeList = _authorizeService.FindList(
                                        a => ((a.ObjectId == userId
                                          && a.ObjectType == 2) ||
                                        (roleIds.Contains(a.ObjectId) &&
                                          a.ObjectType == 1))
                                        && a.ItemType == 3,
                                        a => a.AuthorizeId).Select(x => x.ItemId).Distinct().ToList(); ;

                foreach (var auth in authorizeList)
                {
                    var moduleColumn = moduleColumns.SingleOrDefault(a => a.ModuleColumnId == auth);
                    if (moduleColumn != null)
                    {
                        resultModuleColumns.Add(moduleColumn);
                    }
                }
                moduleColumns = resultModuleColumns;

            }


            moduleForms = _moduleFormService.FindList(a => a.EnabledMark == 1, a => a.Module, a => a.EnCode).ToList();
            if (!om.IsSystem)
            {
                List<ModuleForm> resultModuleForms = new List<ModuleForm>();
                //找出"我"的所有权限
                authorizeList = _authorizeService.FindList(
                                        a => ((a.ObjectId == userId
                                          && a.ObjectType == 2) ||
                                        (roleIds.Contains(a.ObjectId) &&
                                          a.ObjectType == 1))
                                        && a.ItemType == 4,
                                        a => a.AuthorizeId).Select(x => x.ItemId).Distinct().ToList();

                foreach (var auth in authorizeList)
                {
                    var moduleForm = moduleForms.SingleOrDefault(a => a.ModuleFormId == auth);
                    if (moduleForm != null)
                    {
                        resultModuleForms.Add(moduleForm);
                    }
                }
                moduleForms = resultModuleForms;
            }
           
        }

        public void GetAuthorzieItems(string objectId,
            int objectType,
            out List<string> modules,
            out List<string> moduleButtons,
            out List<string> moduleColumns,
            out List<string> moduleForms
            )
        {
            modules = GetAuthorizeModulesByObject(objectId, objectType);
            moduleButtons = GetAuthorizeModuleButtonsByObject(objectId, objectType);
            moduleColumns = GetAuthorizeModuleColumnsByObject(objectId, objectType);
            moduleForms = GetAuthorizeModuleFormsByObject(objectId, objectType);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="objectType">1:角色，2.用户</param>
        /// <returns></returns>
        private List<string> GetAuthorizeModulesByObject(string objectId, int objectType)
        {
            var modules = _moduleService.FindList(a => true, a => a.Sort).ToList();
            if (modules == null || modules.Count <= 0)
            {
                return null;
            }

            //找出这个角色或用户所拥有的模块
            var authorizeList = _authorizeService.FindList(a => a.ObjectId == objectId
                                    && a.ObjectType == objectType
                                    && a.ItemType == 1,
                                    a => a.AuthorizeId);

            if (authorizeList == null || authorizeList.Count <= 0)
            {
                return null;
            }
            var authorizeModuleIdList = authorizeList.Select(a => a.ItemId).ToList();

            List<string> retModuleList = new List<string>();

            foreach (var module in modules)
            {
                if (authorizeModuleIdList.IndexOf(module.ModuleId) >= 0)
                {
                    retModuleList.Add(module.ModuleId);
                }
            }
 
            return retModuleList;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="objectType">1:角色，2.用户</param>
        /// <returns></returns>
        private List<string> GetAuthorizeModuleButtonsByObject(string objectId, int objectType)
        {
            var moduleButtons = _moduleButtonService.FindList(a => true, a => a.EnCode).ToList();
            if (moduleButtons == null || moduleButtons.Count <= 0)
            {
                return null;
            }

            //找出这个角色或用户所拥有的模块
            var authorizeList = _authorizeService.FindList(a => a.ObjectId == objectId
                                    && a.ObjectType == objectType
                                    && a.ItemType == 2,
                                    a => a.AuthorizeId);

            if (authorizeList == null || authorizeList.Count <= 0)
            {
                return null;
            }
            var authorizeModuleButtonIdList = authorizeList.Select(a => a.ItemId).ToList();

            List<string> retModuleButtonList = new List<string>();

            foreach (var moduleButton in moduleButtons)
            {
                if (authorizeModuleButtonIdList.IndexOf(moduleButton.ModuleButtonId) >= 0)
                {
                    retModuleButtonList.Add(moduleButton.ModuleButtonId);
                }
            }

            return retModuleButtonList;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="objectType">1:角色，2.用户</param>
        /// <returns></returns>
        private List<string> GetAuthorizeModuleColumnsByObject(string objectId, int objectType)
        {
            var moduleColumns = _moduleColumnService.FindList(a => true, a => a.EnCode).ToList();
            if (moduleColumns == null || moduleColumns.Count <= 0)
            {
                return null;
            }

            //找出这个角色或用户所拥有的模块
            var authorizeList = _authorizeService.FindList(a => a.ObjectId == objectId
                                    && a.ObjectType == objectType
                                    && a.ItemType == 3,
                                    a => a.AuthorizeId);

            if (authorizeList == null || authorizeList.Count <= 0)
            {
                return null;
            }
            var authorizeModuleColumnIdList = authorizeList.Select(a => a.ItemId).ToList();

            List<string> retModuleColumnList = new List<string>();

            foreach (var moduleColumn in moduleColumns)
            {
                if (authorizeModuleColumnIdList.IndexOf(moduleColumn.ModuleColumnId) >= 0)
                {
                    retModuleColumnList.Add(moduleColumn.ModuleColumnId);
                }
            }

            return retModuleColumnList;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="objectType">1:角色，2.用户</param>
        /// <returns></returns>
        private List<string> GetAuthorizeModuleFormsByObject(string objectId, int objectType)
        {
            var moduleForms = _moduleFormService.FindList(a => true, a => a.EnCode).ToList();
            if (moduleForms == null || moduleForms.Count <= 0)
            {
                return null;
            }

            //找出这个角色或用户所拥有的模块
            var authorizeList = _authorizeService.FindList(a => a.ObjectId == objectId
                                    && a.ObjectType == objectType
                                    && a.ItemType == 4,
                                    a => a.AuthorizeId);

            if (authorizeList == null || authorizeList.Count <= 0)
            {
                return null;
            }
            var authorizeModuleFormIdList = authorizeList.Select(a => a.ItemId).ToList();

            List<string> retModuleFormList = new List<string>();

            foreach (var moduleForm in moduleForms)
            {
                if (authorizeModuleFormIdList.IndexOf(moduleForm.ModuleFormId) >= 0)
                {
                    retModuleFormList.Add(moduleForm.ModuleFormId);
                }
            }

            return retModuleFormList;
        }


        public void UpdateAuthorize(string objectId,
            int objectType,List<Authorize> authorizes)
        {
            _authorizeService.RemoveRange(_authorizeService.FindList(a => a.ObjectId == objectId && a.ObjectType == objectType, a => a.AuthorizeId),false);
            _authorizeService.AddRange(authorizes,false);
            _authorizeService.Commit();
        }

    }
}
