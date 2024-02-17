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
    public class Erp_CustomerBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_CustomerService _Erp_CustomerService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_CustomerBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_CustomerService = new Erp_CustomerService(_dbContext);
        }

        public void AddEntity(Erp_Customer entity)
        {
            entity.Create();
            _Erp_CustomerService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_Customer> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_CustomerService.AddRange(entities);
        }

        public void UpdateEntity(Erp_Customer entity)
        {
            _Erp_CustomerService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_Customer> entities)
        {
           foreach (var e in entities)
            {
                _Erp_CustomerService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_Customer entity)
        {
            _Erp_CustomerService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_Customer> entities)
        {
            _Erp_CustomerService.RemoveRange(entities);
        }

        public Erp_Customer GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_CustomerService.FindList(a => a.Erp_CustomerId == keyValue,a => a.Erp_CustomerId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_Customer> GetEntities(Expression<Func<Erp_Customer, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_CustomerService.FindList(whereExpression, a => a.Erp_CustomerId, isSortAsc);
             return data;
        }

        public IList<Erp_Customer> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_Customer, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_CustomerService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_Customer, bool>> whereExpression)
        {
            return _Erp_CustomerService.IsExists(whereExpression);
        }

    }
}
