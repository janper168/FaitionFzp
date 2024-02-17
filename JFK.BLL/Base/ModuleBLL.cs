using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Base
{
    public class ModuleBLL
    {
        private BaseDbContext _dbContext = null;
        private BaseBLL _baseBLL = new BaseBLL();
        private ModuleService _moduleService = null;
        private ModuleButtonService _moduleButtonService = null;
        private ModuleColumnService _moduleColumnService = null;
        private ModuleFormService _moduleFormService = null;
        private AuthorizeService _authorizeService = null;


        public ModuleBLL()
        {
             _dbContext = new BaseDbContext();
            _moduleService = new ModuleService(_dbContext);
            _moduleButtonService = new ModuleButtonService(_dbContext);
            _moduleColumnService = new ModuleColumnService(_dbContext);
            _moduleFormService = new ModuleFormService(_dbContext);
            _authorizeService = new AuthorizeService(_dbContext);

        }

        /// <summary>
        /// 首次增加一个功能模块配置项
        /// </summary>
        /// <param name="entity"></param>
        public void AddModule(JKF.DB.Models.Module entity)
        {
            entity.Create();
            
            if (entity.ModuleButtonList != null && entity.ModuleButtonList.Count() > 0)
            {
                foreach (var button in entity.ModuleButtonList)
                {
                    button.Create();
                }
            }

            if (entity.ModuleColumnList != null && entity.ModuleColumnList.Count() > 0)
            {
                foreach (var column in entity.ModuleColumnList)
                {
                    column.Create();
                }
            }

            if (entity.ModuleFormList != null && entity.ModuleFormList.Count() > 0)
            {
                foreach (var form in entity.ModuleFormList)
                {
                    form.Create();
                }
            }

            _moduleService.Add(entity);
        }

        public void UpdateModule(JKF.DB.Models.Module module)
        {
        
            _baseBLL.UpdateSubEntities(module,
                _moduleService.GetKeyName(),
                "Module", 
                "ModuleButtonId", 
                module.ModuleButtonList, 
                _moduleButtonService);

            _baseBLL.UpdateSubEntities(module,
                _moduleService.GetKeyName(),
               "Module",
               "ModuleColumnId",
               module.ModuleColumnList,
               _moduleColumnService);

            _baseBLL.UpdateSubEntities(module,
                _moduleService.GetKeyName(),
               "Module",
               "ModuleFormId",
               module.ModuleFormList,
               _moduleFormService);

            _moduleService.Update(module, module.GetPropInfoList(), module.GetReferenceInfoList(), false);
            _moduleService.Commit();

        }

        public void RemoveModule(JKF.DB.Models.Module module)
        {
            _moduleButtonService.RemoveRange(_moduleButtonService.FindList(a => a.Module == module, a => a.EnCode));
            _moduleColumnService.RemoveRange(_moduleColumnService.FindList(a => a.Module == module, a => a.EnCode));
            _moduleFormService.RemoveRange(_moduleFormService.FindList(a => a.Module == module, a => a.EnCode));

            _moduleService.Remove(module);
            _moduleService.Commit();
        }


        public JKF.DB.Models.Module GetModule(string keyValue)
        {
            
            //取第一个
            var module = _moduleService.FindList(a => a.ModuleId == keyValue,
                includeExpression1: a => a.ModuleButtonList,
                includeExpression2: a => a.ModuleColumnList,
                includeExpression3: a => a.ModuleFormList,
                a => a.ModuleId)[0];

            return module;

        }

        public JKF.DB.Models.Module GetModule(Expression<Func<JKF.DB.Models.Module, bool>> whereExpression, bool isTraced = false)
        {

            var module = _moduleService.FirstOrDefault(whereExpression, isTraced);
            return module;
        }

        public IList<JKF.DB.Models.Module> GetModules(Expression<Func<JKF.DB.Models.Module,bool>> whereExpression)
        {

            var modules = _moduleService.FindList(whereExpression,
                includeExpression1: a => a.ModuleButtonList,
                includeExpression2: a => a.ModuleColumnList,
                includeExpression3: a => a.ModuleFormList,
                a => a.Sort);

            return modules;
        }

        public IList<JKF.DB.Models.Module> GetModulesOnly(Expression<Func<JKF.DB.Models.Module, bool>> whereExpression)
        {

            var modules = _moduleService.FindList(whereExpression,
                a => a.Sort);

            return modules;
        }

        public IList<JKF.DB.Models.Module> GetAuthorizedModules()
        {
            var allModules = this.GetModulesOnly(a => a.EnabledMark == 1).ToList();
            OperatorModel om = new OperatorProvider().GetCurrent();

            List<string> roleIds = om.RoleIds.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            string userId = om.UserId;

            List<string> authorizeList = null;
            
            if (!om.IsSystem)
            {
                List<JKF.DB.Models.Module> resultModules = new List<JKF.DB.Models.Module>();
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
                    var module = allModules.SingleOrDefault(a => a.ModuleId == auth);
                    if (module != null)
                    {
                        resultModules.Add(module);
                    }
                }

                resultModules = resultModules.OrderBy(a => a.Sort).ToList();
                allModules = resultModules;
            }

            return allModules;

        }

        public IList<ModuleButton> GetAuthorizedModuleButtons(string moduleId)
        {
            var allModuleButtons = _moduleButtonService.FindList(a => a.EnabledMark == 1&& a.Module.ModuleId==moduleId, a=>a.EnCode).ToList();
            OperatorModel om = new OperatorProvider().GetCurrent();

            List<string> roleIds = om.RoleIds.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            string userId = om.UserId;

            List<string> authorizeList = null;

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
                    var moduleButton = allModuleButtons.SingleOrDefault(a => a.ModuleButtonId == auth);
                    if (moduleButton != null)
                    {
                        resultModuleButtons.Add(moduleButton);
                    }
                }

                resultModuleButtons = resultModuleButtons.OrderBy(a => a.EnCode).ToList();
                allModuleButtons = resultModuleButtons;
            }

            return allModuleButtons;

        }

        public IList<ModuleColumn> GetAuthorizedModuleColumns(string moduleId)
        {
            var allModuleColumns = _moduleColumnService.FindList(a => a.EnabledMark == 1&&a.Module.ModuleId == moduleId, a => a.EnCode).ToList();
            OperatorModel om = new OperatorProvider().GetCurrent();

            List<string> roleIds = om.RoleIds.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            string userId = om.UserId;

            List<string> authorizeList = null;

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
                                        a => a.AuthorizeId).Select(x => x.ItemId).Distinct().ToList();

                foreach (var auth in authorizeList)
                {
                    var moduleColumn = allModuleColumns.SingleOrDefault(a => a.ModuleColumnId == auth);
                    if (moduleColumn != null)
                    {
                        resultModuleColumns.Add(moduleColumn);
                    }
                }

                resultModuleColumns = resultModuleColumns.OrderBy(a => a.EnCode).ToList();
                allModuleColumns = resultModuleColumns;
            }

            return allModuleColumns;

        }

        public IList<ModuleForm> GetAuthorizedModuleForms(string moduleId)
        {
            var allModuleForms = _moduleFormService.FindList(a => a.EnabledMark == 1&& a.Module.ModuleId == moduleId, a => a.EnCode).ToList();
            OperatorModel om = new OperatorProvider().GetCurrent();

            List<string> roleIds = om.RoleIds.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            string userId = om.UserId;

            List<string> authorizeList = null;

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
                    var moduleForm = allModuleForms.SingleOrDefault(a => a.ModuleFormId == auth);
                    if (moduleForm != null)
                    {
                        resultModuleForms.Add(moduleForm);
                    }
                }

                resultModuleForms = resultModuleForms.OrderBy(a => a.EnCode).ToList();
                allModuleForms = resultModuleForms;
            }

            return allModuleForms;

        }


        public IList<JKF.DB.Models.Module> GetModules(string keyWord)
        {

            //var allModules = this.GetModules(a => true).ToList();

            //if (allModules == null || allModules.Count == 0)
            //{
            //    return null;
            //}
            //if (keyWord == "")
            //{
            //    return allModules;
            //}

            var keyWordmodules = this.GetModules(a =>
                a.EnCode.Contains(keyWord) ||
                a.Name.Contains(keyWord) ||
                a.UrlAddress.Contains(keyWord) ||
                a.Icon.Contains(keyWord) ||
                a.Description.Contains(keyWord)
                );
            return keyWordmodules;

            //if (keyWordmodules == null || keyWordmodules.Count == 0)
            //{
            //    return null;
            //}

            //var resultModuels = new List<JKF.DB.Models.Module>();

            //if (allModules.Count == keyWordmodules.Count)
            //{
            //    resultModuels = allModules;
            //}
            //else
            //{
            //    foreach (var keyWordModule in keyWordmodules)
            //    {
            //        AddParentModules(resultModuels, keyWordModule, allModules);
            //    }
            //}

            //return resultModuels;
        }

        /// <summary>
        /// 添加父亲节点到结果集中
        /// </summary>
        /// <param name="resuleModules"></param>
        /// <param name="keyModule"></param>
        /// <param name="allModules"></param>
        private void AddParentModules(List<JKF.DB.Models.Module> resultModules, JKF.DB.Models.Module keyModule, List<JKF.DB.Models.Module> allModules)
        {
            //先添加自身
            var self = allModules.Single(a => a.ModuleId == keyModule.ModuleId);
            if (!resultModules.Contains(self))
            {
                resultModules.Add(self);
            }


            //再添加父亲
            if (keyModule.ParentId == null || keyModule.ParentId == "0")
            {
                return;
            }
            else
            {
                var parentId = keyModule.ParentId;
                do
                {
                    var parentModule = allModules.Single(a => a.ModuleId == parentId);
                    if (!resultModules.Contains(parentModule))
                    {
                        resultModules.Add(parentModule);
                        parentId = parentModule.ParentId;
                    }
                }
                while (!(parentId == null || parentId == "0"));
            }
        }

        public bool IsExists(Expression<Func<JKF.DB.Models.Module, bool>> whereExpression)
        {
            return _moduleService.IsExists(whereExpression);
        }

    }
}
 