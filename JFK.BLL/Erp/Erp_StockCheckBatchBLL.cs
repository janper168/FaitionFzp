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
    public class Erp_StockCheckBatchBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockCheckBatchService _Erp_StockCheckBatchService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockCheckBatchBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockCheckBatchService = new Erp_StockCheckBatchService(_dbContext);
        }

        public void AddEntity(Erp_StockCheckBatch entity)
        {
            entity.Create();
            _Erp_StockCheckBatchService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockCheckBatch> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_StockCheckBatchService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockCheckBatch entity)
        {
            _Erp_StockCheckBatchService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockCheckBatch> entities)
        {
           foreach (var e in entities)
            {
                _Erp_StockCheckBatchService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockCheckBatch entity)
        {
            _Erp_StockCheckBatchService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockCheckBatch> entities)
        {
            _Erp_StockCheckBatchService.RemoveRange(entities);
        }

        public Erp_StockCheckBatch GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockCheckBatchService.FindList(a => a.Erp_StockCheckBatchId == keyValue,a => a.Erp_StockCheckBatchId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_StockCheckBatch> GetEntities(Expression<Func<Erp_StockCheckBatch, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockCheckBatchService.FindList(whereExpression, a => a.Erp_StockCheckBatchId, isSortAsc);
             return data;
        }

        public IList<Erp_StockCheckBatch> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_StockCheckBatch, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockCheckBatchService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_StockCheckBatch, bool>> whereExpression)
        {
            return _Erp_StockCheckBatchService.IsExists(whereExpression);
        }

    }
}
