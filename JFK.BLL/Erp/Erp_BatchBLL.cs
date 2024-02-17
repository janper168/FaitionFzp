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
    public class Erp_BatchBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_BatchService _Erp_BatchService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_BatchBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_BatchService = new Erp_BatchService(_dbContext);
        }

        public void AddEntity(Erp_Batch entity)
        {
            entity.Create();
            _Erp_BatchService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_Batch> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_BatchService.AddRange(entities);
        }

        public void UpdateEntity(Erp_Batch entity)
        {
            _Erp_BatchService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_Batch> entities)
        {
           foreach (var e in entities)
            {
                _Erp_BatchService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_Batch entity)
        {
            _Erp_BatchService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_Batch> entities)
        {
            _Erp_BatchService.RemoveRange(entities);
        }

        public Erp_Batch GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_BatchService.FindList(a => a.Erp_BatchId == keyValue,a => a.Erp_BatchId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_Batch> GetEntities(Expression<Func<Erp_Batch, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_BatchService.FindList(whereExpression, a => a.Erp_BatchId, isSortAsc);
             return data;
        }

        public IList<Erp_Batch> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_Batch, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_BatchService, pagination, sort, whereExpression,a=>a.Goods,a=>a.Warehouse);
        }

        public bool IsExists(Expression<Func<Erp_Batch, bool>> whereExpression)
        {
            return _Erp_BatchService.IsExists(whereExpression);
        }

    }
}
