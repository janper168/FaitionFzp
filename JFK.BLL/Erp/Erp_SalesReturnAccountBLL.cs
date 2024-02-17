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
    public class Erp_SalesReturnAccountBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_SalesReturnAccountService _Erp_SalesReturnAccountService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_SalesReturnAccountBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_SalesReturnAccountService = new Erp_SalesReturnAccountService(_dbContext);
        }

        public void AddEntity(Erp_SalesReturnAccount entity)
        {
            entity.Create();
            _Erp_SalesReturnAccountService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_SalesReturnAccount> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_SalesReturnAccountService.AddRange(entities);
        }

        public void UpdateEntity(Erp_SalesReturnAccount entity)
        {
            _Erp_SalesReturnAccountService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_SalesReturnAccount> entities)
        {
           foreach (var e in entities)
            {
                _Erp_SalesReturnAccountService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_SalesReturnAccount entity)
        {
            _Erp_SalesReturnAccountService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_SalesReturnAccount> entities)
        {
            _Erp_SalesReturnAccountService.RemoveRange(entities);
        }

        public Erp_SalesReturnAccount GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_SalesReturnAccountService.FindList(a => a.Erp_SalesReturnAccountId == keyValue,a => a.Erp_SalesReturnAccountId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_SalesReturnAccount> GetEntities(Expression<Func<Erp_SalesReturnAccount, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_SalesReturnAccountService.FindList(whereExpression, a => a.Erp_SalesReturnAccountId, isSortAsc);
             return data;
        }

        public IList<Erp_SalesReturnAccount> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_SalesReturnAccount, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_SalesReturnAccountService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_SalesReturnAccount, bool>> whereExpression)
        {
            return _Erp_SalesReturnAccountService.IsExists(whereExpression);
        }

    }
}
