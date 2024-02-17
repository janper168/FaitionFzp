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
    public class Erp_StockCheckGoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockCheckGoodsService _Erp_StockCheckGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockCheckGoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockCheckGoodsService = new Erp_StockCheckGoodsService(_dbContext);
        }

        public void AddEntity(Erp_StockCheckGoods entity)
        {
            entity.Create();
            _Erp_StockCheckGoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockCheckGoods> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_StockCheckGoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockCheckGoods entity)
        {
            _Erp_StockCheckGoodsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockCheckGoods> entities)
        {
           foreach (var e in entities)
            {
                _Erp_StockCheckGoodsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockCheckGoods entity)
        {
            _Erp_StockCheckGoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockCheckGoods> entities)
        {
            _Erp_StockCheckGoodsService.RemoveRange(entities);
        }

        public Erp_StockCheckGoods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockCheckGoodsService.FindList(a => a.Erp_StockCheckGoodsId == keyValue,a => a.Erp_StockCheckGoodsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_StockCheckGoods> GetEntities(Expression<Func<Erp_StockCheckGoods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockCheckGoodsService.FindList(whereExpression, a => a.Erp_StockCheckGoodsId, isSortAsc);
             return data;
        }

        public IList<Erp_StockCheckGoods> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_StockCheckGoods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockCheckGoodsService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_StockCheckGoods, bool>> whereExpression)
        {
            return _Erp_StockCheckGoodsService.IsExists(whereExpression);
        }

    }
}
