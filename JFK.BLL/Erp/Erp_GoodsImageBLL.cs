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
    public class Erp_GoodsImageBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_GoodsImageService _Erp_GoodsImageService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_GoodsImageBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_GoodsImageService = new Erp_GoodsImageService(_dbContext);
        }

        public void AddEntity(Erp_GoodsImage entity)
        {
            entity.Create();
            _Erp_GoodsImageService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_GoodsImage> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_GoodsImageService.AddRange(entities);
        }

        public void UpdateEntity(Erp_GoodsImage entity)
        {
            _Erp_GoodsImageService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_GoodsImage> entities)
        {
           foreach (var e in entities)
            {
                _Erp_GoodsImageService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_GoodsImage entity)
        {
            _Erp_GoodsImageService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_GoodsImage> entities)
        {
            _Erp_GoodsImageService.RemoveRange(entities);
        }

        public Erp_GoodsImage GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_GoodsImageService.FindList(a => a.Erp_GoodsImageId == keyValue,a => a.Erp_GoodsImageId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_GoodsImage> GetEntities(Expression<Func<Erp_GoodsImage, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_GoodsImageService.FindList(whereExpression, a => a.Erp_GoodsImageId, isSortAsc);
             return data;
        }

        public IList<Erp_GoodsImage> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_GoodsImage, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_GoodsImageService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_GoodsImage, bool>> whereExpression)
        {
            return _Erp_GoodsImageService.IsExists(whereExpression);
        }

    }
}
