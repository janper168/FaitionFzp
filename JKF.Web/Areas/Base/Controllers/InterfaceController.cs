using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web;

namespace JKF.Web.Areas.Base.Controllers
{
    [Area("Base")]
    public class InterfaceController : BaseController
    {
        private InterfaceBLL _interfaceBLL = new InterfaceBLL();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Form()
        {
            return View();
        }

        public IActionResult GetInterfaces(Pagination pagination, Sort sort, string keyWord)
        {
            return this.ExecuteAction(() =>
            {

                keyWord = keyWord ?? "";

                var datas = _interfaceBLL.GetInterfaces(pagination, sort, a =>
                    (
                     a.Name.Contains(keyWord) ||
                     a.Address.Contains(keyWord)
                     )).ToList();


                var jsonData = new { pagination, sort, datas = datas };
                return JsonSuccess(jsonData);
            });

        }

        public IActionResult GetAllInterfaces()
        {
            return this.ExecuteAction(() =>
            {
                var datas = _interfaceBLL.GetInterfaces(a=>true).ToList();
                return JsonSuccess(datas);
            });
        }

        public IActionResult GetControllers() 
        {

            return this.ExecuteAction(() =>
            {
                List<string> datas = new List<string>();

                Assembly assembly = Assembly.Load("JKF.Web");
                Type[] types =  assembly.GetExportedTypes();
                foreach (var type in types)
                {
                    if (type.Name.EndsWith("Controller"))
                    {
                        datas.Add(type.FullName);
                    }
                }

                var controllers = datas.Select( a => new { controllerName = a, controllerId= Guid.NewGuid().ToString() });
                
                return JsonSuccess(controllers);
            });

        }

        public IActionResult GetControllerActions(string controllerName)
        {

            return this.ExecuteAction(() =>
            {
                List<string> datas = new List<string>();

                Type type = Type.GetType(controllerName);
                MethodInfo[] methodInfos = type.GetMethods();
                foreach (var m in methodInfos) {
                    if (m.ReturnType == typeof(IActionResult)) {
                        datas.Add(m.Name);
                    }
                }

                var controllerActions = datas.Select(a => new { controllerActionName = a, controllerActionId = Guid.NewGuid().ToString() });

                return JsonSuccess(controllerActions);
            });

        }

        public IActionResult getActionInfo(string controllerName,string controllerActionName)
        {

            return this.ExecuteAction(() =>
            {

                Type type = Type.GetType(controllerName);
                MethodInfo methodInfo = type.GetMethod(controllerActionName);

                InterfaceDefineAttribute info = (InterfaceDefineAttribute)methodInfo.GetCustomAttribute(typeof(InterfaceDefineAttribute));
                if (info == null) return JsonFailure(404, "找不到接口的定义！");
                var address = info.Url;
                var returnTypeName = info.ReturnTypeName;

                List<string> datas = new List<string>();
                Assembly asm = Assembly.Load("JKF.DB");

                Type returnType = asm.GetType(returnTypeName);
                PropertyInfo[] props = returnType.GetProperties();
                foreach (var prop in props)
                {
                    string propName = prop.Name;
                    datas.Add(propName);
                    if (checkIsSystemType(prop)) continue;
                    GetSubProps(datas, propName, prop, prop);
                }
                var propertiesInfo = datas.Select(a => new { propertyName = a, propertyId = Guid.NewGuid().ToString() });
                var jsonData = new { address, returnTypeName, propertiesInfo };
                return JsonSuccess(jsonData);

            });

        }

        private bool checkIsSystemType(PropertyInfo propInfo)
        {
            return propInfo.PropertyType.Name.Contains("string", StringComparison.OrdinalIgnoreCase)||
                propInfo.PropertyType.Name.Contains("int", StringComparison.OrdinalIgnoreCase)||
                propInfo.PropertyType.Name.Contains("byte", StringComparison.OrdinalIgnoreCase) ||
                propInfo.PropertyType.Name.Contains("short", StringComparison.OrdinalIgnoreCase) ||
                propInfo.PropertyType.Name.Contains("long", StringComparison.OrdinalIgnoreCase) ||
                propInfo.PropertyType.Name.Contains("float", StringComparison.OrdinalIgnoreCase) ||
                propInfo.PropertyType.Name.Contains("double", StringComparison.OrdinalIgnoreCase) ||
                propInfo.PropertyType.Name.Contains("decimal", StringComparison.OrdinalIgnoreCase) ||
                propInfo.PropertyType.Name.Contains("DateTime", StringComparison.OrdinalIgnoreCase) ||
                propInfo.PropertyType.Name.Contains("int", StringComparison.OrdinalIgnoreCase)||
                propInfo.PropertyType.Name.Contains("bool", StringComparison.OrdinalIgnoreCase);

        }

        private void GetSubProps(List<string>datas, string propName, PropertyInfo prop, PropertyInfo topProp)
        {

            Type propType = prop.PropertyType;
            PropertyInfo[] props = propType.GetProperties();
            foreach (var subProp in props)
            {
                string subPropName = subProp.Name;
                if (subProp != topProp)
                {
                    datas.Add(propName + "." + subPropName);

                    if (checkIsSystemType(subProp)) continue;
                    GetSubProps(datas, propName + "." + subPropName, subProp, topProp);
                }                   
            }
        }




        public IActionResult GetForm(string interfaceId)
        {
            return this.ExecuteAction(() =>
            {
                var entity = _interfaceBLL.GetInterface(interfaceId);
                return JsonSuccess(entity);
            });


        }

        public IActionResult SaveForm(Interface entity)
        {
            return this.ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(entity.InterfaceId))
                {
                    _interfaceBLL.AddInterface(entity);
                }
                else
                {
                    _interfaceBLL.UpdateInterface(entity);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveForm(string interfaceId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(interfaceId))
                {
                    string[] arr = interfaceId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<Interface> roles = new List<Interface>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        roles.Add(new Interface { InterfaceId = id });
                    }
                    _interfaceBLL.RemoveInterfaces(roles);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });

        }

        [HttpPost]
        public IActionResult IsExistName(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _interfaceBLL.IsExists(a => a.Name == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _interfaceBLL.IsExists(a => a.Name == paramValue && a.InterfaceId != keyValue);
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
