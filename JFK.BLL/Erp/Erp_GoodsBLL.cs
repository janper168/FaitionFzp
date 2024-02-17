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
    public class Erp_GoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_GoodsService _Erp_GoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_GoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_GoodsService = new Erp_GoodsService(_dbContext);
        }

        public void AddEntity(Erp_Goods entity)
        {
            entity.Create();
            _Erp_GoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_Goods> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_GoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_Goods entity)
        {
            _Erp_GoodsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_Goods> entities)
        {
           foreach (var e in entities)
            {
                _Erp_GoodsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_Goods entity)
        {
            _Erp_GoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_Goods> entities)
        {
            _Erp_GoodsService.RemoveRange(entities);
        }

        public Erp_Goods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_GoodsService.FindList(a => a.Erp_GoodsId == keyValue,a => a.Erp_GoodsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_Goods> GetEntities(Expression<Func<Erp_Goods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_GoodsService.FindList(whereExpression, a => a.Erp_GoodsId, isSortAsc);
             return data;
        }

        public IList<Erp_Goods> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_Goods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_GoodsService, pagination, sort, whereExpression,a=>a.GoodsCategory,a=>a.GoodsCategory);
        }

        public bool IsExists(Expression<Func<Erp_Goods, bool>> whereExpression)
        {
            return _Erp_GoodsService.IsExists(whereExpression);
        }

    }
}
