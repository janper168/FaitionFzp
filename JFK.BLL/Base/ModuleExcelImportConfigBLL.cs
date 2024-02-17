using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class ModuleExcelImportConfigBLL
    {
        private BaseDbContext _dbContext = null;
        private ModuleExcelImportConfigService _ModuleExcelImportConfigService = null;
        private ModuleService _moduleService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public ModuleExcelImportConfigBLL()
        {
            _dbContext = new BaseDbContext();
            _ModuleExcelImportConfigService = new ModuleExcelImportConfigService(_dbContext);
            _moduleService = new ModuleService(_dbContext);
        }

        public void AddEntity(ModuleExcelImportConfig entity)
        {
            entity.Create();
            _ModuleExcelImportConfigService.Add(entity);
        }

        public void AddEntities(IEnumerable<ModuleExcelImportConfig> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _ModuleExcelImportConfigService.AddRange(entities);
        }

        public void UpdateEntity(ModuleExcelImportConfig entity)
        {
            _ModuleExcelImportConfigService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<ModuleExcelImportConfig> entities)
        {
           foreach (var e in entities)
            {
                _ModuleExcelImportConfigService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(ModuleExcelImportConfig entity)
        {
            _ModuleExcelImportConfigService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<ModuleExcelImportConfig> entities)
        {
            _ModuleExcelImportConfigService.RemoveRange(entities);
        }

        public ModuleExcelImportConfig GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _ModuleExcelImportConfigService.FindList(a => a.ModuleExcelImportConfigId == keyValue,a => a.ModuleExcelImportConfigId, true, beTraced)[0];
             return entity;
        }

        public IList<ModuleExcelImportConfig> GetEntities(Expression<Func<ModuleExcelImportConfig, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _ModuleExcelImportConfigService.FindList(whereExpression, a => a.SortCode, isSortAsc);
             return data;
        }

        public IList<ModuleExcelImportConfig> GetEntities(Pagination pagination, Sort sort, Expression<Func<ModuleExcelImportConfig, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_ModuleExcelImportConfigService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<ModuleExcelImportConfig, bool>> whereExpression)
        {
            return _ModuleExcelImportConfigService.IsExists(whereExpression);
        }


        public JKF.DB.Models.Module GetModule(Expression<Func<JKF.DB.Models.Module, bool>> whereExpression, bool isTraced = false)
        {

            var module = _moduleService.FirstOrDefault(whereExpression, isTraced);
            return module;
        }

        public void ImportExcel(string filePath, string moduleName, string moduleKey, string moduleTypeName, string serviceTypeName)
        {
            BaseDbContext context = new BaseDbContext();
            Assembly asm = Assembly.Load("JKF.DB");

            Type modelType = asm.GetType(moduleTypeName);
            Type serviceType = asm.GetType(serviceTypeName);

            var serviceObject = Activator.CreateInstance(serviceType, context);
            var dataItemDetailSerivce = new DataItemDetailService(context);
            AreaService areaService = new AreaService(context);

            XSSFWorkbook resWorkbook;
            var tempPath = filePath;
            FileStream fileTemp = new FileStream(tempPath, FileMode.Open, FileAccess.ReadWrite);
            resWorkbook = new XSSFWorkbook(fileTemp);

            if (resWorkbook == null)
            {
                Console.WriteLine("文件不存在");
                return;
            }

            XSSFSheet resSheet = (XSSFSheet)resWorkbook.GetSheetAt(0);
            if (resSheet == null)
            {
                Console.WriteLine("表单不存在");
                return;
            }

            var row0 = resSheet.GetRow(0);

            var configService = new ModuleExcelImportConfigService(context);
            var propList = configService.FindList(a => a.Module.Name == moduleName, a => a.ModuleExcelImportConfigId);

            Dictionary<int, ModuleExcelImportConfig> propDict = new Dictionary<int, ModuleExcelImportConfig>();
            int cellIndex = 0;
            var cellValue = row0.GetCell(cellIndex).ToString();

            var physicCellIndex = 0;
            while (!string.IsNullOrWhiteSpace(cellValue))
            {
                var prop = propList.Where(a => a.DisplayName == cellValue).ToList()[0];

                if (prop.IsPhysics == 1)
                {
                    physicCellIndex = cellIndex;
                }

                propDict.Add(cellIndex++, prop);

                var cell = row0.GetCell(cellIndex);
                if (cell == null) break;
                cellValue = cell.ToString();
            }



            BaseTransaction bt = new BaseTransaction(context);
            bt.Execute((context, ts) =>
            {
                var rowIndex = 1;
                var cellIndex = 0;
                var row = resSheet.GetRow(rowIndex);

                var isAdd = true;
                var firstCellValue = row.GetCell(0).ToString();
                while (!string.IsNullOrWhiteSpace(firstCellValue))
                {
                    cellIndex = 0;
                    cellValue = row.GetCell(cellIndex).ToString();

                    var physicsValue = row.GetCell(physicCellIndex).ToString();

                    ParameterExpression tEntityParam = Expression.Parameter(modelType, "a");
                    MemberExpression memberExpression = Expression.Property(tEntityParam, propDict[physicCellIndex].PropertyName);
                    ConstantExpression constExpr = Expression.Constant(physicsValue,typeof(string));
                    BinaryExpression binaryExpression = Expression.Equal(memberExpression, constExpr);
                    LambdaExpression lambdaExpr = Expression.Lambda(binaryExpression, new List<ParameterExpression>() { tEntityParam });

                    MethodInfo? mInfo = serviceType.GetMethod("FirstOrDefault", new Type[] { lambdaExpr.GetType(), typeof(bool) });
                    var entity = Activator.CreateInstance(modelType); 
                    if (mInfo != null)
                    {
                        var pE = mInfo.Invoke(serviceObject, new object?[] { lambdaExpr, true });
                        if (pE != null)
                        {
                            isAdd = false;
                            entity = pE;
                        }
                    }

                    var keyProp = modelType.GetProperty(moduleKey);
                    //keyProp.SetValue(entity, Guid.NewGuid().ToString());

                    while (cellIndex < propDict.Count)
                    {
                        var prop = propDict[cellIndex++];
                        if (prop.IsValid == 1)
                        {
                            var pName = prop.PropertyName;

                            PropertyInfo? pInfo = modelType.GetProperty(pName);
                            if (prop.IsParentId != 1)
                            {
                                if (prop.IsRefer != 1)
                                {
                                    if (prop.IsArea != 1)
                                    {
                                        if (prop.IsDataItem != 1)
                                        {
                                            if (pInfo != null)
                                            {
                                                object? targetValue = ChangeToTargetTypeValue(cellValue, prop.PropertyType);

                                                pInfo.SetValue(entity, targetValue);
                                            }
                                        }
                                        else if (prop.IsDataItem == 1)
                                        {
                                            
                                            var itemDetails = dataItemDetailSerivce.FindList(a => a.DataItem.ItemCode == prop.ItemCode,a=>a.SortCode).ToList();

                                            var targetValue = itemDetails.Where(a => a.ItemName.Contains(cellValue)).ToList()[0].ItemValue;

                                            pInfo.SetValue(entity, targetValue);
                                        }

                                    }
                                    else if (prop.IsArea == 1)
                                    {
                                        if (!string.IsNullOrWhiteSpace(cellValue))
                                        {  
                                            var area = areaService.FirstOrDefault(a => a.AreaName.StartsWith(cellValue) && a.Layer == prop.AreaLayer);

                                            if (pInfo != null && area != null)
                                            {
                                                pInfo.SetValue(entity, area.AreaId);
                                            }
                                        }

                                    }

                                }
                                else if (prop.IsRefer == 1)
                                {

                                    var refEntityName = prop.RefEntityName;
                                    var refEntityType = asm.GetType(refEntityName);
                                    var refPropertyName = prop.RefPropertyName;
                                    var refEntityServiceType = asm.GetType(prop.RefEntityServiceName);

                                    tEntityParam = Expression.Parameter(refEntityType, "a");
                                    memberExpression = Expression.Property(tEntityParam, refPropertyName);
                                    constExpr = Expression.Constant(cellValue, typeof(string));
                                    binaryExpression = Expression.Equal(memberExpression, constExpr);
                                    lambdaExpr = Expression.Lambda(binaryExpression, new List<ParameterExpression>() { tEntityParam });

                                    mInfo = refEntityServiceType.GetMethod("FirstOrDefault", new Type[] { lambdaExpr.GetType(), typeof(bool) });

                                    var refServiceObject = Activator.CreateInstance(refEntityServiceType, context);

                                    if (mInfo != null)
                                    {
                                        var pE = mInfo.Invoke(refServiceObject, new object?[] { lambdaExpr, true });
                                        pInfo.SetValue(entity, pE);

                                        var IdPropertyName = prop.PropertyName + "Id";
                                        PropertyInfo? pInfoKey = modelType.GetProperty(IdPropertyName);
                                        if (pInfoKey != null)
                                        {
                                            var keyId = "";
                                            var keyValue = "";
                                            var properties = refEntityType.GetProperties();
                                            for (var i = 0; i < properties.Count(); i++)
                                            {
                                                var prop2 = properties[i];
                                                Attribute? attribute = prop2.GetCustomAttribute(typeof(KeyAttribute));
                                                if (attribute != null)
                                                {
                                                    keyId = prop2.Name;
                                                    keyValue = (string)prop2.GetValue(pE);
                                                    pInfoKey.SetValue(entity, keyValue);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (prop.IsParentId == 1)
                            {

                                var pValue = cellValue;
                                if (cellValue.IsEmpty())
                                {
                                    pValue = "0";
                                }
                                else
                                {
                                    var ptype = pInfo.PropertyType;

                                    tEntityParam = Expression.Parameter(modelType, "a");
                                    memberExpression = Expression.Property(tEntityParam, prop.PropertyName);
                                    constExpr = Expression.Constant(pValue, ptype);
                                    binaryExpression = Expression.Equal(memberExpression, constExpr);
                                    lambdaExpr = Expression.Lambda(binaryExpression, new List<ParameterExpression>() { tEntityParam });

                                    mInfo = serviceType.GetMethod("FirstOrDefault", new Type[] { lambdaExpr.GetType(), typeof(bool) });

                                    if (mInfo != null)
                                    {
                                        var pE = mInfo.Invoke(serviceObject, new object?[] { lambdaExpr, true });

                                        pValue = (string)keyProp.GetValue(pE);
                                    }
                                }

                                PropertyInfo? pInfoParentId = modelType.GetProperty("ParentId");
                                if (pInfoParentId != null)
                                {
                                    pInfoParentId.SetValue(entity, pValue);
                                }
                            }

                        }


                        var cell = row.GetCell(cellIndex);
                        if (cell == null)
                        {
                            cellValue = "";
                        }
                        else
                        {
                            cellValue = cell.ToString();
                        }


                    }

                    if (isAdd)
                    {
                        MethodInfo? aM = modelType.GetMethod("Create", new Type[] { });
                        if (aM != null)
                        {
                            aM.Invoke(entity, new object[] { });
                        }

                        aM = serviceType.GetMethod("Add", new Type[] { modelType, typeof(bool) });
                        if (aM != null)
                        {
                            aM.Invoke(serviceObject, new object[] { entity, true });
                        }
                    }
                    else
                    {
                        MethodInfo? aM = serviceType.GetMethod("Update", new Type[] { modelType, typeof(IEnumerable<PropertyInfo>), typeof(IEnumerable<PropertyInfo>), typeof(bool) });
                        if (aM != null)
                        {
                            MethodInfo? aM1 = modelType.GetMethod("GetPropInfoList");
                            var p1 = aM1.Invoke(entity,new object[] { });

                            MethodInfo? aM2 = modelType.GetMethod("GetReferenceInfoList");
                            var p2 = aM2.Invoke(entity, new object[] { });

                            aM.Invoke(serviceObject, new object[] { entity, p1, p2,true });
                        }
                    }

                    row = resSheet.GetRow(++rowIndex);
                    if (row != null)
                        firstCellValue = row.GetCell(0).ToString();
                    else
                        break;
                }

            });

        }

        private object? ChangeToTargetTypeValue(string orginValue, string typeName)
        {
            if (string.IsNullOrWhiteSpace(orginValue))
            {
                return null;
            }

            switch (typeName) 
            {
                case "System.String":
                    return orginValue;
                case "System.Int32":
                case "System.Nullable`1[System.Int32]":
                    return int.Parse(orginValue);
                case "System.DateTime":
                case "System.Nullable`1[System.DateTime]":
                    return DateTime.Parse(orginValue);
                case "System.Decimal":
                    return decimal.Parse(orginValue);
                case "System.Nullable`1[System.Decimal]":
                    return decimal.Parse(orginValue);

                default:
                    return null;
            }

        }
    }
}
