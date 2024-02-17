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
    public class TaskNodeBLL
    {
        private BaseDbContext _dbContext = null;
        private TaskNodeService _TaskNodeService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public TaskNodeBLL()
        {
            _dbContext = new BaseDbContext();
            _TaskNodeService = new TaskNodeService(_dbContext);
        }

        public void AddEntity(TaskNode entity)
        {
            entity.Create();
            _TaskNodeService.Add(entity);
        }

        public void AddEntities(IEnumerable<TaskNode> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _TaskNodeService.AddRange(entities);
        }

        public void UpdateEntity(TaskNode entity)
        {
            _TaskNodeService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<TaskNode> entities)
        {
           foreach (var e in entities)
            {
                _TaskNodeService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(TaskNode entity)
        {
            _TaskNodeService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<TaskNode> entities)
        {
            _TaskNodeService.RemoveRange(entities);
        }

        public TaskNode GetEntity(Expression<Func<TaskNode, bool>> whereExpression, bool beTraced = false)
        {
            var entity = _TaskNodeService.FindList(whereExpression, a => a.TaskNodeId, true, beTraced)[0];
            return entity;
        }
        public TaskNode GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _TaskNodeService.FindList(a => a.TaskNodeId == keyValue,a => a.TaskNodeId, true, beTraced)[0];
             return entity;
        }

        public IList<TaskNode> GetEntities(Expression<Func<TaskNode, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _TaskNodeService.FindList(whereExpression, a => a.TaskNodeId, isSortAsc);
             return data;
        }

        public IList<TaskNode> GetEntites(Pagination pagination, Sort sort, Expression<Func<TaskNode, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_TaskNodeService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<TaskNode, bool>> whereExpression)
        {
            return _TaskNodeService.IsExists(whereExpression);
        }

    }
}
