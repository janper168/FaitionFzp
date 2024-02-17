using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Base
{
    public class DataItemsBLL
    {
        private BaseDbContext _dbContext = null;
        //private BaseBLL _baseBLL = new BaseBLL();
        private DataItemService _dataItemService = null;
        private DataItemDetailService _dataItemDetailService = null;
      
        public DataItemsBLL()
        {
             _dbContext = new BaseDbContext();
            _dataItemService = new DataItemService(_dbContext);
            _dataItemDetailService = new DataItemDetailService(_dbContext);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void AddDataItem(DataItem entity)
        {
            entity.Create();

            _dataItemService.Add(entity);
        }

        public void UpdateDataItem(DataItem entity)
        {

            _dataItemService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());

        }

        public void RemoveDataItem(DataItem entity)
        {
            _dataItemDetailService.RemoveRange(_dataItemDetailService.FindList(a => a.DataItem == entity, a => a.SortCode),false);

            _dataItemService.Remove(entity, false);
            _dataItemService.Commit();
        }


        public DataItem GetDataItem(string keyValue,bool beTracked = false)
        {
           
            var dataItem = _dataItemService.FindList(a => a.DataItemId == keyValue,
              
            a => a.SortCode,true, beTracked)[0];

            return dataItem;
            
        }

        public IList<DataItem> GetDataItems(Expression<Func<DataItem, bool>> whereExpression)
        {

            var dataItem = _dataItemService.FindList(whereExpression,
                a => a.SortCode);

            return dataItem;
        }

        public IList<DataItem> GetDataItems(string keyWord)
        {
            
            //var allDatas = this.GetDataItems(a => true).ToList();
            //if (allDatas == null || allDatas.Count <= 0)
            //{
            //    return null;
            //}
            //if (keyWord == "")
            //{
            //    return allDatas;
            //}
            var datas = this.GetDataItems(a => (a.ItemCode.Contains(keyWord) ||
                                                         a.ItemName.Contains(keyWord) ||
                                                         a.Description.Contains(keyWord)
                                                         ) && a.DeleteMark == 0 && a.EnabledMark == 1);

            //if (datas == null || datas.Count <= 0)
            //{
            //    return null;
            //}
            //var resultDatas = new List<DataItem>();
            //if (allDatas.Count == datas.Count)
            //{
            //    resultDatas = allDatas;
            //}
            //else
            //{
            //    foreach (var data in datas)
            //    {
            //        AddParentDataItems(resultDatas, data, allDatas);
            //    }
            //}
            return datas;
        }

        private void AddParentDataItems(List<DataItem> resultDatas, DataItem data, List<DataItem> allDatas)
        {
            //先添加自身
            var self = allDatas.Single(a => a.DataItemId == data.DataItemId);
            if (!resultDatas.Contains(self))
            {
                resultDatas.Add(self);
            }


            //再添加父亲
            if (data.ParentId == null || data.ParentId == "0")
            {
                return;
            }
            else
            {
                var parentId = data.ParentId;
                do
                {
                    var parentData = allDatas.Single(a => a.DataItemId == parentId);
                    if (!resultDatas.Contains(parentData))
                    {
                        resultDatas.Add(parentData);
                        parentId = parentData.ParentId;
                    }
                }
                while (!(parentId == null || parentId == "0"));
            }
        }


        public bool IsExistsDataItem(Expression<Func<DataItem, bool>> whereExpression)
        {
            return _dataItemService.IsExists(whereExpression);
        }

        public void AddDataItemDetail(DataItemDetail entity)
        {
            entity.Create();

            _dataItemDetailService.Add(entity);
        }

        public void UpdateDataItemDetail(DataItemDetail entity)
        {
            _dataItemDetailService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void RemoveDataItemDetail(DataItemDetail entity)
        {         
            _dataItemDetailService.Remove(entity);
        }

        public bool IsExistsDataItemDetail(Expression<Func<DataItemDetail, bool>> whereExpression)
        {
            return _dataItemDetailService.IsExists(whereExpression);
        }

        public IList<DataItemDetail> GetDataItemDetails(Expression<Func<DataItemDetail, bool>> whereExpression)
        {

            var dataDetailItems = _dataItemDetailService.FindList(whereExpression,
                a => a.SortCode);

            return dataDetailItems;
        }


        public IList<DataItemDetail> GetDataItemDetails(string keyWord,string dataItemId)
        {
            
            //var allDatas = this.GetDataItemDetails(a => a.DataItem.DataItemId == dataItemId).ToList();
            //if (allDatas == null || allDatas.Count <= 0)
            //{
            //    return null;
            //}
            //if (keyWord == "")
            //{
            //    return allDatas;
            //}
            var datas = this.GetDataItemDetails(a => (a.ItemName.Contains(keyWord) ||
                                                         a.ItemValue.Contains(keyWord) ||
                                                         a.Description.Contains(keyWord)
                                                         ) &&
                                                         a.DataItem.DataItemId == dataItemId &&
                                                         a.DeleteMark == 0 &&
                                                         a.EnabledMark == 1);
            //if (datas == null || datas.Count <= 0)
            //{
            //    return datas;
            //}
            //var resultDatas = new List<DataItemDetail>();
            //if (allDatas.Count == datas.Count)
            //{
            //    resultDatas = allDatas;
            //}
            //else
            //{
            //    foreach (var data in datas)
            //    {
            //        AddParentDataItemDetails(resultDatas, data, allDatas);
            //    }
            //}
            return datas;

        }
        private void AddParentDataItemDetails(List<DataItemDetail> resultDatas, DataItemDetail data, List<DataItemDetail> allDatas)
        {
            //先添加自身
            var self = allDatas.Single(a => a.DataItemDetailId == data.DataItemDetailId);
            if (!resultDatas.Contains(self))
            {
                resultDatas.Add(self);
            }

            //再添加父亲
            if (data.ParentId == null || data.ParentId == "0")
            {
                return;
            }
            else
            {
                var parentId = data.ParentId;
                do
                {
                    var parentData = allDatas.Single(a => a.DataItemDetailId == parentId);
                    if (!resultDatas.Contains(parentData))
                    {
                        resultDatas.Add(parentData);
                        parentId = parentData.ParentId;
                    }
                }
                while (!(parentId == null || parentId == "0"));
            }
        }

        public bool IsExists(Expression<Func<DataItem, bool>> whereExpression)
        {
            return _dataItemService.IsExists(whereExpression);
        }

        public bool IsExistDetails(Expression<Func<DataItemDetail, bool>> whereExpression)
        {
            return _dataItemDetailService.IsExists(whereExpression);
        }



    }
}
 