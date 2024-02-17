using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JKF.Web.Controllers
{
    public class UtilityController : BaseController
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public UtilityController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Icon()
        {
            return View();
        }

        public IActionResult ChooseThemeForm()
        {
            return View();
        }

        public IActionResult ChangePwdForm()
        {
            return View();
        }

        public IActionResult DownLoadFile(string filePath)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            string downLoadFilePath = webRootPath + filePath;

            byte[] fileBytes = System.IO.File.ReadAllBytes(downLoadFilePath);

            string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
