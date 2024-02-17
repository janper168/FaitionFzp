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
    public class AreaController : BaseController
    {
        private AreaBLL _AreaBLL = new AreaBLL();

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 按区域层级来模糊查询匹配到的区域
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public IActionResult GetAreas(string keyWord, int layer)
        {

            return this.ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";
                var datas = _AreaBLL.GetAreas(a => a.Layer == layer &&
                                 a.EnabledMark == 1 &&
                                 a.DeleteMark == 0 &&
                                 (a.AreaCode.Contains(keyWord) ||
                                  a.AreaName.Contains(keyWord) ||
                                  a.SimpleSpelling.Contains(keyWord) ||
                                  a.QuickQuery.Contains(keyWord)));

                return JsonSuccess(datas);
            });

        }

        public IActionResult GetDownLevelAreas(string parentId, int layer)
        {
            return this.ExecuteAction(() =>
            {
                var datas = _AreaBLL.GetAreas(a => a.ParentId == parentId && a.Layer == layer);
                return JsonSuccess(datas);

            });
        }

        [HttpPost]
        public IActionResult GetSpecialArea(string areaId, int layer)
        {
            return this.ExecuteAction(() =>
            {
                var retDictionary = new Dictionary<int, List<Area>>();

                int downLevelLayer = layer + 1;

                if (downLevelLayer <= 4)
                {
                    List<Area> downList = _AreaBLL.GetAreas(a => a.Layer == downLevelLayer &&
                                        a.EnabledMark == 1 &&
                                        a.DeleteMark == 0 && a.ParentId == areaId).ToList();

                    retDictionary.Add(downLevelLayer, downList);
                }

                Area thisArea = _AreaBLL.GetArea(areaId);
                Area upArea = thisArea;
                int upLevel = layer - 1;
                while (upLevel > 0)
                {
                    upArea = _AreaBLL.GetArea(upArea.ParentId);
                    retDictionary.Add(upLevel, new List<Area> { upArea });
                    upLevel--;

                }

                return JsonSuccess(retDictionary);


            });
        }
    }
}
