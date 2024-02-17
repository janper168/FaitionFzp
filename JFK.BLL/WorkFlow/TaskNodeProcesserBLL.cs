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
    public class TaskNodeProcesserBLL
    {
        private BaseDbContext _dbContext = null;
        private TaskNodeProcesserService _TaskNodeProcesserService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public TaskNodeProcesserBLL()
        {
            _dbContext = new BaseDbContext();
            _TaskNodeProcesserService = new TaskNodeProcesserService(_dbContext);
        }

        public void AddEntity(TaskNodeProcesser entity)
        {
            entity.Create();
            _TaskNodeProcesserService.Add(entity);
        }

        public void AddEntities(IEnumerable<TaskNodeProcesser> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _TaskNodeProcesserService.AddRange(entities);
        }

        public void UpdateEntity(TaskNodeProcesser entity)
        {
            _TaskNodeProcesserService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<TaskNodeProcesser> entities)
        {
           foreach (var e in entities)
            {
                _TaskNodeProcesserService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(TaskNodeProcesser entity)
        {
            _TaskNodeProcesserService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<TaskNodeProcesser> entities)
        {
            _TaskNodeProcesserService.RemoveRange(entities);
        }

        public TaskNodeProcesser GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _TaskNodeProcesserService.FindList(a => a.TaskNodeProcesserId == keyValue,
                a => a.TaskNode, a => a.TaskNode.Task, a => a.TaskNode.Task.WorkFlowDesign, a => a.TaskNode.Task.WorkFlowDesign.CustomizedForm,
                a => a.TaskNodeProcesserId, true, beTraced)[0];
            return entity;
        }

        public TaskNodeProcesser GetEntity(Expression<Func<TaskNodeProcesser, bool>> whereExpression, bool isSortAsc = true)
        {
            var entity = _TaskNodeProcesserService.FindList(whereExpression,a=>a.TaskNode,a=>a.TaskNodeProcesserId)[0];
             return entity;
        }

        public IList<TaskNodeProcesser> GetEntities(Expression<Func<TaskNodeProcesser, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _TaskNodeProcesserService.FindList(whereExpression, a => a.TaskNodeProcesserId, isSortAsc);
             return data;
        }

        public IList<TaskNodeProcesser> GetEntities(Pagination pagination, Sort sort, Expression<Func<TaskNodeProcesser, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_TaskNodeProcesserService, pagination, sort, whereExpression,a=>a.TaskNode,a => a.TaskNode);
        }

        public bool IsExists(Expression<Func<TaskNodeProcesser, bool>> whereExpression)
        {
            return _TaskNodeProcesserService.IsExists(whereExpression);
        }

    }
}
