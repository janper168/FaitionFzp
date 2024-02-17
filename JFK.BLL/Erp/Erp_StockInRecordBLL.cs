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
    public class Erp_StockInRecordBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockInRecordService _Erp_StockInRecordService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockInRecordBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockInRecordService = new Erp_StockInRecordService(_dbContext);
        }

        public void AddEntity(Erp_StockInRecord entity)
        {
            entity.Create();
            _Erp_StockInRecordService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockInRecord> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_StockInRecordService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockInRecord entity)
        {
            _Erp_StockInRecordService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockInRecord> entities)
        {
           foreach (var e in entities)
            {
                _Erp_StockInRecordService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockInRecord entity)
        {
            _Erp_StockInRecordService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockInRecord> entities)
        {
            _Erp_StockInRecordService.RemoveRange(entities);
        }

        public Erp_StockInRecord GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockInRecordService.FindList(a => a.Erp_StockInRecordId == keyValue,a => a.Erp_StockInRecordId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_StockInRecord> GetEntities(Expression<Func<Erp_StockInRecord, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockInRecordService.FindList(whereExpression, a=>a.StockInOrder, a=>a.Warehouse,a=>a.Processor, a => a.Erp_StockInRecordId, isSortAsc);
             return data;
        }

        public IList<Erp_StockInRecord> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_StockInRecord, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockInRecordService, pagination, sort, whereExpression, a => a.StockInOrder, a => a.Warehouse, a => a.Processor);
        }

        public bool IsExists(Expression<Func<Erp_StockInRecord, bool>> whereExpression)
        {
            return _Erp_StockInRecordService.IsExists(whereExpression);
        }

    }
}
