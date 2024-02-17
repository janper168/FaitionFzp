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
    public class Erp_PurchaseReturnAccountBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_PurchaseReturnAccountService _Erp_PurchaseReturnAccountService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_PurchaseReturnAccountBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_PurchaseReturnAccountService = new Erp_PurchaseReturnAccountService(_dbContext);
        }

        public void AddEntity(Erp_PurchaseReturnAccount entity)
        {
            entity.Create();
            _Erp_PurchaseReturnAccountService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_PurchaseReturnAccount> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_PurchaseReturnAccountService.AddRange(entities);
        }

        public void UpdateEntity(Erp_PurchaseReturnAccount entity)
        {
            _Erp_PurchaseReturnAccountService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_PurchaseReturnAccount> entities)
        {
           foreach (var e in entities)
            {
                _Erp_PurchaseReturnAccountService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_PurchaseReturnAccount entity)
        {
            _Erp_PurchaseReturnAccountService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_PurchaseReturnAccount> entities)
        {
            _Erp_PurchaseReturnAccountService.RemoveRange(entities);
        }

        public Erp_PurchaseReturnAccount GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_PurchaseReturnAccountService.FindList(a => a.Erp_PurchaseReturnAccountId == keyValue,a => a.Erp_PurchaseReturnAccountId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_PurchaseReturnAccount> GetEntities(Expression<Func<Erp_PurchaseReturnAccount, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_PurchaseReturnAccountService.FindList(whereExpression, a => a.Erp_PurchaseReturnAccountId, isSortAsc);
             return data;
        }

        public IList<Erp_PurchaseReturnAccount> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_PurchaseReturnAccount, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_PurchaseReturnAccountService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_PurchaseReturnAccount, bool>> whereExpression)
        {
            return _Erp_PurchaseReturnAccountService.IsExists(whereExpression);
        }

    }
}
