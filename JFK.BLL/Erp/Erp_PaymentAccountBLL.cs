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
    public class Erp_PaymentAccountBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_PaymentAccountService _Erp_PaymentAccountService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_PaymentAccountBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_PaymentAccountService = new Erp_PaymentAccountService(_dbContext);
        }

        public void AddEntity(Erp_PaymentAccount entity)
        {
            entity.Create();
            _Erp_PaymentAccountService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_PaymentAccount> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_PaymentAccountService.AddRange(entities);
        }

        public void UpdateEntity(Erp_PaymentAccount entity)
        {
            _Erp_PaymentAccountService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_PaymentAccount> entities)
        {
           foreach (var e in entities)
            {
                _Erp_PaymentAccountService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_PaymentAccount entity)
        {
            _Erp_PaymentAccountService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_PaymentAccount> entities)
        {
            _Erp_PaymentAccountService.RemoveRange(entities);
        }

        public Erp_PaymentAccount GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_PaymentAccountService.FindList(a => a.Erp_PaymentAccountId == keyValue,a => a.Erp_PaymentAccountId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_PaymentAccount> GetEntities(Expression<Func<Erp_PaymentAccount, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_PaymentAccountService.FindList(whereExpression, a => a.Erp_PaymentAccountId, isSortAsc);
             return data;
        }

        public IList<Erp_PaymentAccount> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_PaymentAccount, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_PaymentAccountService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_PaymentAccount, bool>> whereExpression)
        {
            return _Erp_PaymentAccountService.IsExists(whereExpression);
        }

    }
}
