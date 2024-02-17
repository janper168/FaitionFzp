using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using NPOI.POIFS.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class WorkFlowDesignBLL
    {
        private BaseDbContext _dbContext = null;
        private WorkFlowDesignService _WorkFlowDesignService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public WorkFlowDesignBLL()
        {
            _dbContext = new BaseDbContext();
            _WorkFlowDesignService = new WorkFlowDesignService(_dbContext);
        }

        public void AddEntity(WorkFlowDesign entity)
        {
            entity.Create();
            _WorkFlowDesignService.Add(entity);
        }

        public void AddEntities(IEnumerable<WorkFlowDesign> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _WorkFlowDesignService.AddRange(entities);
        }

        public void UpdateEntity(WorkFlowDesign entity)
        {
            entity.Update();
            _WorkFlowDesignService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<WorkFlowDesign> entities)
        {
           foreach (var e in entities)
            {
                e.Update();
                _WorkFlowDesignService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(WorkFlowDesign entity)
        {
            _WorkFlowDesignService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<WorkFlowDesign> entities)
        {
            _WorkFlowDesignService.RemoveRange(entities);
        }

        public WorkFlowDesign GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _WorkFlowDesignService.FindList(a => a.WorkFlowDesignId == keyValue,a=>a.CustomizedForm,a => a.WorkFlowDesignId, true, beTraced)[0];
             return entity;
        }

        public IList<WorkFlowDesign> GetEntities(Expression<Func<WorkFlowDesign, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _WorkFlowDesignService.FindList(whereExpression, a => a.WorkFlowDesignId, isSortAsc);
             return data;
        }

        public IList<WorkFlowDesign> GetEntities(Pagination pagination, Sort sort, Expression<Func<WorkFlowDesign, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_WorkFlowDesignService, pagination, sort, whereExpression,a=>a.CreateUser,a=>a.UpdateUser,a=>a.CustomizedForm);
        }

        public bool IsExists(Expression<Func<WorkFlowDesign, bool>> whereExpression)
        {
            return _WorkFlowDesignService.IsExists(whereExpression);
        }

    }
}
