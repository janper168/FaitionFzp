using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class Erp_StockOutRecordBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockOutRecordService _Erp_StockOutRecordService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockOutRecordBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockOutRecordService = new Erp_StockOutRecordService(_dbContext);
        }

        public void AddEntity(Erp_StockOutRecord entity)
        {
            entity.Create();
            _Erp_StockOutRecordService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockOutRecord> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_StockOutRecordService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockOutRecord entity)
        {
            _Erp_StockOutRecordService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockOutRecord> entities)
        {
           foreach (var e in entities)
            {
                _Erp_StockOutRecordService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockOutRecord entity)
        {
            _Erp_StockOutRecordService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockOutRecord> entities)
        {
            _Erp_StockOutRecordService.RemoveRange(entities);
        }

        public Erp_StockOutRecord GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockOutRecordService.FindList(a => a.Erp_StockOutRecordId == keyValue,a => a.Erp_StockOutRecordId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_StockOutRecord> GetEntities(Expression<Func<Erp_StockOutRecord, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockOutRecordService.FindList(whereExpression,a=>a.StockOutOrder,a=>a.Warehouse,a=>a.Processor, a => a.Erp_StockOutRecordId, isSortAsc);
             return data;
        }

        public IList<Erp_StockOutRecord> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_StockOutRecord, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockOutRecordService, pagination, sort, whereExpression,a=>a.Warehouse,a=>a.Processor,a=>a.StockOutOrder);
        }

        public bool IsExists(Expression<Func<Erp_StockOutRecord, bool>> whereExpression)
        {
            return _Erp_StockOutRecordService.IsExists(whereExpression);
        }

    }
}
