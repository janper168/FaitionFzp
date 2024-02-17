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
    public class WorkFlowLineBLL
    {
        private BaseDbContext _dbContext = null;
        private WorkFlowLineService _WorkFlowLineService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public WorkFlowLineBLL()
        {
            _dbContext = new BaseDbContext();
            _WorkFlowLineService = new WorkFlowLineService(_dbContext);
        }

        public void AddEntity(WorkFlowLine entity)
        {
            entity.Create();
            _WorkFlowLineService.Add(entity);
        }

        public void AddEntities(IEnumerable<WorkFlowLine> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _WorkFlowLineService.AddRange(entities);
        }

        public void UpdateEntity(WorkFlowLine entity)
        {
            _WorkFlowLineService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<WorkFlowLine> entities)
        {
           foreach (var e in entities)
            {
                _WorkFlowLineService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(WorkFlowLine entity)
        {
            _WorkFlowLineService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<WorkFlowLine> entities)
        {
            _WorkFlowLineService.RemoveRange(entities);
        }

        public WorkFlowLine GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _WorkFlowLineService.FindList(a => a.WorkFlowLineId == keyValue,a => a.WorkFlowLineId, true, beTraced)[0];
             return entity;
        }

        public WorkFlowLine GetEntity(Expression<Func<WorkFlowLine, bool>> whereExpression, bool beTraced = false)
        {
            var entity = _WorkFlowLineService.FindList(whereExpression, a => a.WorkFlowLineId, true, beTraced)[0];
            return entity;
        }

        public IList<WorkFlowLine> GetEntities(Expression<Func<WorkFlowLine, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _WorkFlowLineService.FindList(whereExpression, a => a.WorkFlowLineId, isSortAsc);
             return data;
        }

        public IList<WorkFlowLine> GetEntities(Pagination pagination, Sort sort, Expression<Func<WorkFlowLine, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_WorkFlowLineService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<WorkFlowLine, bool>> whereExpression)
        {
            return _WorkFlowLineService.IsExists(whereExpression);
        }

    }
}
