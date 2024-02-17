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
    public class Erp_CollectionAccountBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_CollectionAccountService _Erp_CollectionAccountService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_CollectionAccountBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_CollectionAccountService = new Erp_CollectionAccountService(_dbContext);
        }

        public void AddEntity(Erp_CollectionAccount entity)
        {
            entity.Create();
            _Erp_CollectionAccountService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_CollectionAccount> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_CollectionAccountService.AddRange(entities);
        }

        public void UpdateEntity(Erp_CollectionAccount entity)
        {
            _Erp_CollectionAccountService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_CollectionAccount> entities)
        {
           foreach (var e in entities)
            {
                _Erp_CollectionAccountService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_CollectionAccount entity)
        {
            _Erp_CollectionAccountService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_CollectionAccount> entities)
        {
            _Erp_CollectionAccountService.RemoveRange(entities);
        }

        public Erp_CollectionAccount GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_CollectionAccountService.FindList(a => a.Erp_CollectionAccountId == keyValue,a => a.Erp_CollectionAccountId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_CollectionAccount> GetEntities(Expression<Func<Erp_CollectionAccount, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_CollectionAccountService.FindList(whereExpression, a => a.Erp_CollectionAccountId, isSortAsc);
             return data;
        }

        public IList<Erp_CollectionAccount> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_CollectionAccount, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_CollectionAccountService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_CollectionAccount, bool>> whereExpression)
        {
            return _Erp_CollectionAccountService.IsExists(whereExpression);
        }

    }
}
