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
    public class Erp_StockInGoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockInGoodsService _Erp_StockInGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockInGoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockInGoodsService = new Erp_StockInGoodsService(_dbContext);
        }

        public void AddEntity(Erp_StockInGoods entity)
        {
            entity.Create();
            _Erp_StockInGoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockInGoods> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_StockInGoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockInGoods entity)
        {
            _Erp_StockInGoodsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockInGoods> entities)
        {
           foreach (var e in entities)
            {
                _Erp_StockInGoodsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockInGoods entity)
        {
            _Erp_StockInGoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockInGoods> entities)
        {
            _Erp_StockInGoodsService.RemoveRange(entities);
        }

        public Erp_StockInGoods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockInGoodsService.FindList(a => a.Erp_StockInGoodsId == keyValue,a => a.Erp_StockInGoodsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_StockInGoods> GetEntities(Expression<Func<Erp_StockInGoods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockInGoodsService.FindList(whereExpression, a => a.Erp_StockInGoodsId, isSortAsc);
             return data;
        }

        public IList<Erp_StockInGoods> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_StockInGoods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockInGoodsService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_StockInGoods, bool>> whereExpression)
        {
            return _Erp_StockInGoodsService.IsExists(whereExpression);
        }

    }
}
