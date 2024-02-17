using JKF.BLL.Base;
using JKF.DB.Models;
using JKF.Utils;
using JKF.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JKF.Web.Areas.Base.Controllers
{
    [Area("Base")]
    public class DataItemsController : BaseController
    {
        private DataItemsBLL _dataItemsBLL = new DataItemsBLL();


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DataItemForm()
        {
            return View();
        }
        public IActionResult DataItemDetailForm()
        {
            return View();
        }

        public IActionResult GetDataItems(string keyWord)
        {
            return ExecuteAction(() =>
            {
                keyWord = keyWord ?? "";

                var resultDatas = _dataItemsBLL.GetDataItems(keyWord);

                return JsonSuccess(resultDatas);
            });

        }


        [HttpPost]
        public IActionResult SaveDataItemForm(DataItem dataItem)
        {
            return ExecuteAction(() =>
            {
                if (string.IsNullOrEmpty(dataItem.DataItemId))
                {
                    _dataItemsBLL.AddDataItem(dataItem);
                }
                else
                {
                    _dataItemsBLL.UpdateDataItem(dataItem);
                }
                return JsonSuccess(null);

            });

        }

        public IActionResult RemoveDataItemForm(string dataItemId)
        {

            return ExecuteAction(() =>
            {
                int code = 0;
                string errMsg = "";

                if (!string.IsNullOrEmpty(dataItemId))
                {
                    if (_dataItemsBLL.IsExistsDataItem(a => a.ParentId == dataItemId))
                    {
                        code = 400;
                        errMsg = "存在下级字典项，无法删除！";
                        return JsonFailure(code, errMsg);
                    }
                    else
                    {
                        _dataItemsBLL.RemoveDataItem(new DataItem { DataItemId = dataItemId });
                        return JsonSuccess(null);
                    }
                }
                return JsonSuccess(null);
            });

            
        }

        public IActionResult GetDataItemDetails(string keyWord, string dataItemId)
        {
            return ExecuteAction(() =>
            {

                keyWord = keyWord ?? "";

                var resultDatas = _dataItemsBLL.GetDataItemDetails(keyWord, dataItemId);

                return JsonSuccess(resultDatas);
            });

        }


        public IActionResult GetDataItemDetailsByItemCode(string itemCode)
        {
            return ExecuteAction(() =>
            {
                var datas = _dataItemsBLL.GetDataItemDetails(a => a.DataItem.ItemCode == itemCode).ToList(); ;

                return JsonSuccess(datas);
            });

        }

        [HttpPost]
        public IActionResult SaveDataItemDetailForm(DataItemDetail dataItemDetail, string dataItemId)
        {
            return ExecuteAction(() =>
            {
                var dataItem = _dataItemsBLL.GetDataItem(dataItemId, beTracked: true);
                if (dataItem == null)
                {
                    return JsonFailure(400, "找不到所属的字典项.");
                }

                if (string.IsNullOrEmpty(dataItemDetail.DataItemDetailId))
                {
                    dataItemDetail.DataItem = dataItem;
                    _dataItemsBLL.AddDataItemDetail(dataItemDetail);
                }
                else
                {
                    dataItemDetail.DataItem = dataItem;
                    _dataItemsBLL.UpdateDataItemDetail(dataItemDetail);
                }
                return JsonSuccess(null);

            });


        }

        public IActionResult RemoveDataItemDetailForm(string dataItemDetailId)
        {
            return ExecuteAction(() =>
            {
                if (!string.IsNullOrEmpty(dataItemDetailId))
                {
                    if (_dataItemsBLL.IsExistsDataItemDetail(a => a.ParentId == dataItemDetailId))
                    {

                        return JsonFailure(400, "存在下级字典明细项，无法删除！");
                    }
                    else
                    {
                        _dataItemsBLL.RemoveDataItemDetail(new DataItemDetail { DataItemDetailId = dataItemDetailId });
                        return JsonSuccess(null);
                    }
                }
                return JsonSuccess(null);
            });
           
        }

        public IActionResult IsExistItemCode(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _dataItemsBLL.IsExists(a => a.ItemCode == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _dataItemsBLL.IsExists(a => a.ItemCode == paramValue && a.DataItemId != keyValue);
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
        public IActionResult IsExistItemName(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _dataItemsBLL.IsExists(a => a.ItemName == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _dataItemsBLL.IsExists(a => a.ItemName == paramValue && a.DataItemId != keyValue);
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

        public IActionResult IsExistDetailItemName(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _dataItemsBLL.IsExistDetails(a => a.ItemName == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _dataItemsBLL.IsExistDetails(a => a.ItemName == paramValue && a.DataItemDetailId != keyValue);
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
        public IActionResult IsExistDetailItemValue(string keyValue, string paramValue)
        {
            try
            {
                if (keyValue.IsEmpty())
                {
                    var ret = _dataItemsBLL.IsExistDetails(a => a.ItemValue == paramValue);
                    if (ret)
                    {
                        return JsonFailure(400, "已经存在");
                    }
                    return JsonSuccess(null);
                }
                else
                {
                    var ret = _dataItemsBLL.IsExistDetails(a => a.ItemValue == paramValue && a.DataItemDetailId != keyValue);
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
