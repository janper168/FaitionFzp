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
    public class TaskNodeLogBLL
    {
        private BaseDbContext _dbContext = null;
        private TaskNodeLogService _TaskNodeLogService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public TaskNodeLogBLL()
        {
            _dbContext = new BaseDbContext();
            _TaskNodeLogService = new TaskNodeLogService(_dbContext);
        }

        public void AddEntity(TaskNodeLog entity)
        {
            entity.Create();
            _TaskNodeLogService.Add(entity);
        }

        public void AddEntities(IEnumerable<TaskNodeLog> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _TaskNodeLogService.AddRange(entities);
        }

        public void UpdateEntity(TaskNodeLog entity)
        {
            _TaskNodeLogService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<TaskNodeLog> entities)
        {
           foreach (var e in entities)
            {
                _TaskNodeLogService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(TaskNodeLog entity)
        {
            _TaskNodeLogService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<TaskNodeLog> entities)
        {
            _TaskNodeLogService.RemoveRange(entities);
        }

        public TaskNodeLog GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _TaskNodeLogService.FindList(a => a.TaskNodeLogId == keyValue,a => a.TaskNodeLogId, true, beTraced)[0];
             return entity;
        }

        public IList<TaskNodeLog> GetEntities(Expression<Func<TaskNodeLog, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _TaskNodeLogService.FindList(whereExpression, a=>a.TaskNodeProcesser.Processer, a => a.ProcessTime, isSortAsc);
             return data;
        }

        public IList<TaskNodeLog> GetEntities(Pagination pagination, Sort sort, Expression<Func<TaskNodeLog, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_TaskNodeLogService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<TaskNodeLog, bool>> whereExpression)
        {
            return _TaskNodeLogService.IsExists(whereExpression);
        }

    }
}
