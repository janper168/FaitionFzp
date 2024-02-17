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
    public class Erp_SalesAccountBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_SalesAccountService _Erp_SalesAccountService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_SalesAccountBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_SalesAccountService = new Erp_SalesAccountService(_dbContext);
        }

        public void AddEntity(Erp_SalesAccount entity)
        {
            entity.Create();
            _Erp_SalesAccountService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_SalesAccount> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_SalesAccountService.AddRange(entities);
        }

        public void UpdateEntity(Erp_SalesAccount entity)
        {
            _Erp_SalesAccountService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_SalesAccount> entities)
        {
           foreach (var e in entities)
            {
                _Erp_SalesAccountService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_SalesAccount entity)
        {
            _Erp_SalesAccountService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_SalesAccount> entities)
        {
            _Erp_SalesAccountService.RemoveRange(entities);
        }

        public Erp_SalesAccount GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_SalesAccountService.FindList(a => a.Erp_SalesAccountId == keyValue,a => a.Erp_SalesAccountId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_SalesAccount> GetEntities(Expression<Func<Erp_SalesAccount, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_SalesAccountService.FindList(whereExpression, a => a.Erp_SalesAccountId, isSortAsc);
             return data;
        }

        public IList<Erp_SalesAccount> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_SalesAccount, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_SalesAccountService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_SalesAccount, bool>> whereExpression)
        {
            return _Erp_SalesAccountService.IsExists(whereExpression);
        }

    }
}
