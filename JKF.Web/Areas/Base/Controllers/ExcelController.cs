using JKF.BLL;
using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.Models;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JKF.Web.Areas.Base.Controllers
{
    [Area("Base")]
    public class ExcelController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExcelController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        private ModuleExcelImportConfigBLL _ModuleExcelImportConfigBLL = new ModuleExcelImportConfigBLL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImportConfigIndex()
        {
            return View();
        }

        public IActionResult ImportConfigForm()
        {
            return View();
        }


        public IActionResult GetModuleExcelImportConfigs(Pagination pagination, Sort sort, string keyWord, string moduleName)
        {
            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var datas = _ModuleExcelImportConfigBLL.GetEntities(pagination, sort, a =>
                     a.Module.Name == moduleName && (
                     a.PropertyName.Contains(keyWord) ||
                     a.DisplayName.Contains(keyWord) ||
                     a.RefEntityName.Contains(keyWord) ||
                     a.RefPropertyName.Contains(keyWord) ||
                     a.RefEntityServiceName.Contains(keyWord) 
                     )).ToList();


                var jsonData = new { pagination, sort, datas = datas };
                return JsonSuccess(jsonData);
            });

        }

        public IActionResult GetImortConfigForm(string moduleExcelImportConfigId)
        {
            return this.ExecuteAction(() =>
            {
                var data = _ModuleExcelImportConfigBLL.GetEntity(moduleExcelImportConfigId);
                return JsonSuccess(data);
            });


        }

        public IActionResult SaveImportConfigForm(ModuleExcelImportConfig moduleExcelImportConfig, string moduleName)
        {
            return this.ExecuteAction(() =>
            {
                var module = _ModuleExcelImportConfigBLL.GetModule(a => a.Name == moduleName, true);

                if (string.IsNullOrEmpty(moduleExcelImportConfig.ModuleExcelImportConfigId))
                {
                    moduleExcelImportConfig.Module = module;
                    _ModuleExcelImportConfigBLL.AddEntity(moduleExcelImportConfig);
                }
                else
                {
                    moduleExcelImportConfig.Module = module;
                    _ModuleExcelImportConfigBLL.UpdateEntity(moduleExcelImportConfig);
                }
                return JsonSuccess(null);
            });

        }

        public IActionResult RemoveModuleExcelImportForm(string moduleExcelImportConfigId)
        {

            return this.ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(moduleExcelImportConfigId))
                {
                    string[] arr = moduleExcelImportConfigId.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    List<ModuleExcelImportConfig> datas = new List<ModuleExcelImportConfig>();
                    if (arr.Length <= 0)
                    {
                        return JsonSuccess(null);
                    }

                    foreach (var id in arr)
                    {
                        datas.Add(new ModuleExcelImportConfig { ModuleExcelImportConfigId = id });
                    }
                    _ModuleExcelImportConfigBLL.RemoveEntities(datas);
                    return JsonSuccess(null);

                }
                return JsonSuccess(null);
            });

        }


        public IActionResult OnPostUpload(IFormFile file,string moduleName,string moduleKey, string moduleType,string serviceType)
        {

            return this.ExecuteAction(() =>
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string contentRootPath = _webHostEnvironment.ContentRootPath;

                if (file.Length > 0)
                {
                    var filePath = webRootPath + "/resources/excel/import/";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    filePath += (Guid.NewGuid().ToString() + "_"+ file.FileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }

                    _ModuleExcelImportConfigBLL.ImportExcel(filePath,moduleName, moduleKey,moduleType, serviceType);

                    return JsonSuccess(null);
                }
                else
                {
                    return JsonFailure(400, "文件内容不能为空！");
                }
            });

        }

        public IActionResult DownloadImportExcelTemplate(string moduleName)
        {

            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            var filePath = webRootPath + "/resources/excel/import/template/";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath += moduleName + "导入模板.xlsx";
         
            var datas = _ModuleExcelImportConfigBLL.GetEntities(a => a.Module.Name == moduleName && a.IsValid == 1).ToList();

            var displayNameArray = datas.Select(a => a.DisplayName).ToList();

            XSSFWorkbook resWorkbook;

            FileStream fileTemp = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            resWorkbook = new XSSFWorkbook();

            if (resWorkbook == null)
            {
                throw new Exception("Excel处理异常！");
            }

            XSSFSheet resSheet = (XSSFSheet)resWorkbook.CreateSheet();
            if (resSheet == null)
            {
                throw new Exception("Excel处理异常！");
            }

            var row0 = resSheet.CreateRow(0);

            for (int i = 0; i < displayNameArray.Count; i++)
            {
                var cell = row0.CreateCell(i);
                cell.SetCellValue(displayNameArray[i]);
            }

            resWorkbook.Write(fileTemp);
            resWorkbook.Close();
            fileTemp.Close();


            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return File(fileStream, "application/x-msexcel", moduleName + "导入模板.xlsx");
        }


    }
}
