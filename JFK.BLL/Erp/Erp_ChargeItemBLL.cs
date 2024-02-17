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
    public class Erp_ChargeItemBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_ChargeItemService _Erp_ChargeItemService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_ChargeItemBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_ChargeItemService = new Erp_ChargeItemService(_dbContext);
        }

        public void AddEntity(Erp_ChargeItem entity)
        {
            entity.Create();
            _Erp_ChargeItemService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_ChargeItem> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_ChargeItemService.AddRange(entities);
        }

        public void UpdateEntity(Erp_ChargeItem entity)
        {
            _Erp_ChargeItemService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_ChargeItem> entities)
        {
           foreach (var e in entities)
            {
                _Erp_ChargeItemService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_ChargeItem entity)
        {
            _Erp_ChargeItemService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_ChargeItem> entities)
        {
            _Erp_ChargeItemService.RemoveRange(entities);
        }

        public Erp_ChargeItem GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_ChargeItemService.FindList(a => a.Erp_ChargeItemId == keyValue,a => a.Erp_ChargeItemId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_ChargeItem> GetEntities(Expression<Func<Erp_ChargeItem, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_ChargeItemService.FindList(whereExpression, a => a.Erp_ChargeItemId, isSortAsc);
             return data;
        }

        public IList<Erp_ChargeItem> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_ChargeItem, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_ChargeItemService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_ChargeItem, bool>> whereExpression)
        {
            return _Erp_ChargeItemService.IsExists(whereExpression);
        }

    }
}
