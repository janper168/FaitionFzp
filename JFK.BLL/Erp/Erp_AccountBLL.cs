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
    public class Erp_AccountBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_AccountService _Erp_AccountService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_AccountBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_AccountService = new Erp_AccountService(_dbContext);
        }

        public void AddEntity(Erp_Account entity)
        {
            entity.Create();
            _Erp_AccountService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_Account> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_AccountService.AddRange(entities);
        }

        public void UpdateEntity(Erp_Account entity)
        {
            _Erp_AccountService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_Account> entities)
        {
           foreach (var e in entities)
            {
                _Erp_AccountService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_Account entity)
        {
            _Erp_AccountService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_Account> entities)
        {
            _Erp_AccountService.RemoveRange(entities);
        }

        public Erp_Account GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_AccountService.FindList(a => a.Erp_AccountId == keyValue,a => a.Erp_AccountId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_Account> GetEntities(Expression<Func<Erp_Account, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_AccountService.FindList(whereExpression, a => a.Erp_AccountId, isSortAsc);
             return data;
        }

        public IList<Erp_Account> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_Account, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_AccountService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_Account, bool>> whereExpression)
        {
            return _Erp_AccountService.IsExists(whereExpression);
        }

    }
}
