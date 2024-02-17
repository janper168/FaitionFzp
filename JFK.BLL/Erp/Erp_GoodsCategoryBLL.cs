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
    public class Erp_GoodsCategoryBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_GoodsCategoryService _Erp_GoodsCategoryService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_GoodsCategoryBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_GoodsCategoryService = new Erp_GoodsCategoryService(_dbContext);
        }

        public void AddEntity(Erp_GoodsCategory entity)
        {
            entity.Create();
            _Erp_GoodsCategoryService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_GoodsCategory> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_GoodsCategoryService.AddRange(entities);
        }

        public void UpdateEntity(Erp_GoodsCategory entity)
        {
            _Erp_GoodsCategoryService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_GoodsCategory> entities)
        {
           foreach (var e in entities)
            {
                _Erp_GoodsCategoryService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_GoodsCategory entity)
        {
            _Erp_GoodsCategoryService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_GoodsCategory> entities)
        {
            _Erp_GoodsCategoryService.RemoveRange(entities);
        }

        public Erp_GoodsCategory GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_GoodsCategoryService.FindList(a => a.Erp_GoodsCategoryId == keyValue,a => a.Erp_GoodsCategoryId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_GoodsCategory> GetEntities(Expression<Func<Erp_GoodsCategory, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_GoodsCategoryService.FindList(whereExpression, a => a.Erp_GoodsCategoryId, isSortAsc);
             return data;
        }

        public IList<Erp_GoodsCategory> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_GoodsCategory, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_GoodsCategoryService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_GoodsCategory, bool>> whereExpression)
        {
            return _Erp_GoodsCategoryService.IsExists(whereExpression);
        }

    }
}
