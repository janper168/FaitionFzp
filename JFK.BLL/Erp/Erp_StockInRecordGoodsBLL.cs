using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class Erp_StockInRecordGoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockInRecordGoodsService _Erp_StockInRecordGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockInRecordGoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockInRecordGoodsService = new Erp_StockInRecordGoodsService(_dbContext);
        }

        public void AddEntity(Erp_StockInRecordGoods entity)
        {
            entity.Create();
            _Erp_StockInRecordGoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockInRecordGoods> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_StockInRecordGoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockInRecordGoods entity)
        {
            _Erp_StockInRecordGoodsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockInRecordGoods> entities)
        {
           foreach (var e in entities)
            {
                _Erp_StockInRecordGoodsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockInRecordGoods entity)
        {
            _Erp_StockInRecordGoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockInRecordGoods> entities)
        {
            _Erp_StockInRecordGoodsService.RemoveRange(entities);
        }

        public Erp_StockInRecordGoods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockInRecordGoodsService.FindList(a => a.Erp_StockInRecordGoodsId == keyValue,a => a.Erp_StockInRecordGoodsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_StockInRecordGoods> GetEntities(Expression<Func<Erp_StockInRecordGoods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockInRecordGoodsService.FindList(whereExpression, a => a.StockInGoods,a => a.StockInGoods.Goods, a => a.Erp_StockInRecordGoodsId, isSortAsc);
             return data;
        }

        public IList<Erp_StockInRecordGoods> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_StockInRecordGoods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockInRecordGoodsService, pagination, sort, whereExpression,a=>a.StockInGoods,a=>a.StockInGoods);
        }

        public bool IsExists(Expression<Func<Erp_StockInRecordGoods, bool>> whereExpression)
        {
            return _Erp_StockInRecordGoodsService.IsExists(whereExpression);
        }

    }
}
