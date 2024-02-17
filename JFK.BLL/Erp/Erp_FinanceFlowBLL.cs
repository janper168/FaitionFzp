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
    public class Erp_FinanceFlowBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_FinanceFlowService _Erp_FinanceFlowService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_FinanceFlowBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_FinanceFlowService = new Erp_FinanceFlowService(_dbContext);
        }

        public void AddEntity(Erp_FinanceFlow entity)
        {
            entity.Create();
            _Erp_FinanceFlowService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_FinanceFlow> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_FinanceFlowService.AddRange(entities);
        }

        public void UpdateEntity(Erp_FinanceFlow entity)
        {
            _Erp_FinanceFlowService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_FinanceFlow> entities)
        {
           foreach (var e in entities)
            {
                _Erp_FinanceFlowService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_FinanceFlow entity)
        {
            _Erp_FinanceFlowService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_FinanceFlow> entities)
        {
            _Erp_FinanceFlowService.RemoveRange(entities);
        }

        public Erp_FinanceFlow GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_FinanceFlowService.FindList(a => a.Erp_FinanceFlowId == keyValue,a => a.Erp_FinanceFlowId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_FinanceFlow> GetEntities(Expression<Func<Erp_FinanceFlow, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_FinanceFlowService.FindList(whereExpression, a => a.Erp_FinanceFlowId, isSortAsc);
             return data;
        }

        public IList<Erp_FinanceFlow> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_FinanceFlow, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_FinanceFlowService, pagination, sort, whereExpression,a=>a.Account,a=>a.CreateUser);
        }

        public bool IsExists(Expression<Func<Erp_FinanceFlow, bool>> whereExpression)
        {
            return _Erp_FinanceFlowService.IsExists(whereExpression);
        }

    }
}
