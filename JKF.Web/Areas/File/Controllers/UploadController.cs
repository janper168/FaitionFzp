using JKF.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace JKF.Web.Areas.File.Controllers
{
    [Area("File")]
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> KindEditorImgUpload()
        {

            Dictionary<string, string> extTable = new Dictionary<string, string>();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb,mp4");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");
            //最大文件大小
            int maxSize = 100000000;
            var context = Request.HttpContext;
            var imgFile = Request.Form.Files[0];

            //文件类型
            string dirName = Request.Query["dir"];
            if (string.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }
            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile == null || imgFile.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }
            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }
            string saveDir = Request.Query["saveDir"];
            string saveDirStr = null;
            if (saveDir == null)
            {
                saveDirStr = "tmp";
            }
            else
            {
                saveDirStr = saveDir.ToString();
            }
            //文件保存目录
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string savePath = "/wwwroot/upload/kindeditor/" + dirName + "/" + saveDirStr;
            string dirPath = contentRootPath + savePath;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String newFileName = DateTime.Now.ToString("_yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            String filePath = dirPath + "/" + newFileName;
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                await imgFile.CopyToAsync(fs);
                fs.Flush();
            }
            Dictionary<string, object> hash = new Dictionary<string, object>();

            hash["url"] = (savePath + "/" + newFileName).Replace("/wwwroot", "");
            hash["url"] = Request.Scheme + "://" + Request.Host.Value + hash["url"];
            hash["error"] = 0;
            Response.Headers.Add("Content-Type", "text/html; charset=UTF-8");
            return Json(hash);
        }
        private IActionResult showError(string message)
        {
            Dictionary<string, object> hash = new Dictionary<string, object>();

            hash["error"] = 1;
            hash["message"] = message;
            Response.Headers.Add("Content-Type", "text/html; charset=UTF-8");
            return Json(hash);
        }

        [HttpPost]
        public IActionResult OnPostUpload(IFormFile file,string dir)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            if (file.Length > 0)
            {
                var filePath = webRootPath + "/resources/wfUpload/" + dir +"/";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                filePath += file.FileName;

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                string savePath = "/resources/wfUpload/" + dir;
                string dirPath = contentRootPath + savePath;

                Dictionary<string, object> hash = new Dictionary<string, object>();
                hash["url"] = (savePath + "/" + file.FileName);
                hash["url"] = Request.Scheme + "://" + Request.Host.Value + hash["url"];
                hash["error"] = 0;
                Response.Headers.Add("Content-Type", "text/html; charset=UTF-8");
                return Json(hash);

            }
            else
            {
                Dictionary<string, object> hash = new Dictionary<string, object>();
                hash["error"] = 1;
                hash["message"] = "文件不能为空";
                Response.Headers.Add("Content-Type", "text/html; charset=UTF-8");
                return Json(hash);
            }
        }
    }
}
