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
    public class WorkFlowNodeBLL
    {
        private BaseDbContext _dbContext = null;
        private WorkFlowNodeService _WorkFlowNodeService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public WorkFlowNodeBLL()
        {
            _dbContext = new BaseDbContext();
            _WorkFlowNodeService = new WorkFlowNodeService(_dbContext);
        }

        public void AddEntity(WorkFlowNode entity)
        {
            entity.Create();
            _WorkFlowNodeService.Add(entity);
        }

        public void AddEntities(IEnumerable<WorkFlowNode> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _WorkFlowNodeService.AddRange(entities);
        }

        public void UpdateEntity(WorkFlowNode entity)
        {
            _WorkFlowNodeService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<WorkFlowNode> entities)
        {
           foreach (var e in entities)
            {
                _WorkFlowNodeService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(WorkFlowNode entity)
        {
            _WorkFlowNodeService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<WorkFlowNode> entities)
        {
            _WorkFlowNodeService.RemoveRange(entities);
        }

        public WorkFlowNode GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _WorkFlowNodeService.FindList(a => a.WorkFlowNodeId == keyValue,a => a.WorkFlowNodeId, true, beTraced)[0];
             return entity;
        }

        public WorkFlowNode GetEntity(Expression<Func<WorkFlowNode, bool>> whereExpression, bool beTraced = false)
        {
            var entityList = _WorkFlowNodeService.FindList(whereExpression, a => a.WorkFlowDesign, a => a.WorkFlowNodeId, true, beTraced);
            if (entityList == null || entityList.Count() == 0) 
            {
                return null;
            }

            return entityList[0];
        }

        public IList<WorkFlowNode> GetEntities(Expression<Func<WorkFlowNode, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _WorkFlowNodeService.FindList(whereExpression, a => a.WorkFlowNodeId, isSortAsc);
             return data;
        }

        public IList<WorkFlowNode> GetEntities(Pagination pagination, Sort sort, Expression<Func<WorkFlowNode, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_WorkFlowNodeService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<WorkFlowNode, bool>> whereExpression)
        {
            return _WorkFlowNodeService.IsExists(whereExpression);
        }

    }
}
