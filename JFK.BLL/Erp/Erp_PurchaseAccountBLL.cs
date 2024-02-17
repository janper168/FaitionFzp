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
    public class Erp_PurchaseAccountBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_PurchaseAccountService _Erp_PurchaseAccountService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_PurchaseAccountBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_PurchaseAccountService = new Erp_PurchaseAccountService(_dbContext);
        }

        public void AddEntity(Erp_PurchaseAccount entity)
        {
            entity.Create();
            _Erp_PurchaseAccountService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_PurchaseAccount> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_PurchaseAccountService.AddRange(entities);
        }

        public void UpdateEntity(Erp_PurchaseAccount entity)
        {
            _Erp_PurchaseAccountService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_PurchaseAccount> entities)
        {
           foreach (var e in entities)
            {
                _Erp_PurchaseAccountService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_PurchaseAccount entity)
        {
            _Erp_PurchaseAccountService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_PurchaseAccount> entities)
        {
            _Erp_PurchaseAccountService.RemoveRange(entities);
        }

        public Erp_PurchaseAccount GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_PurchaseAccountService.FindList(a => a.Erp_PurchaseAccountId == keyValue,a => a.Erp_PurchaseAccountId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_PurchaseAccount> GetEntities(Expression<Func<Erp_PurchaseAccount, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_PurchaseAccountService.FindList(whereExpression, a => a.Erp_PurchaseAccountId, isSortAsc);
             return data;
        }

        public IList<Erp_PurchaseAccount> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_PurchaseAccount, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_PurchaseAccountService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_PurchaseAccount, bool>> whereExpression)
        {
            return _Erp_PurchaseAccountService.IsExists(whereExpression);
        }

    }
}
