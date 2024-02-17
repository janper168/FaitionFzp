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
    public class Erp_StockOutGoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockOutGoodsService _Erp_StockOutGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockOutGoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockOutGoodsService = new Erp_StockOutGoodsService(_dbContext);
        }

        public void AddEntity(Erp_StockOutGoods entity)
        {
            entity.Create();
            _Erp_StockOutGoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockOutGoods> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_StockOutGoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockOutGoods entity)
        {
            _Erp_StockOutGoodsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockOutGoods> entities)
        {
           foreach (var e in entities)
            {
                _Erp_StockOutGoodsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockOutGoods entity)
        {
            _Erp_StockOutGoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockOutGoods> entities)
        {
            _Erp_StockOutGoodsService.RemoveRange(entities);
        }

        public Erp_StockOutGoods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockOutGoodsService.FindList(a => a.Erp_StockOutGoodsId == keyValue,a => a.Erp_StockOutGoodsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_StockOutGoods> GetEntities(Expression<Func<Erp_StockOutGoods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockOutGoodsService.FindList(whereExpression, a => a.Erp_StockOutGoodsId, isSortAsc);
             return data;
        }

        public IList<Erp_StockOutGoods> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_StockOutGoods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockOutGoodsService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_StockOutGoods, bool>> whereExpression)
        {
            return _Erp_StockOutGoodsService.IsExists(whereExpression);
        }

    }
}
