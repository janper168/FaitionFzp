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
    public class Erp_InventoryFlowBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_InventoryFlowService _Erp_InventoryFlowService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_InventoryFlowBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_InventoryFlowService = new Erp_InventoryFlowService(_dbContext);
        }

        public void AddEntity(Erp_InventoryFlow entity)
        {
            entity.Create();
            _Erp_InventoryFlowService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_InventoryFlow> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_InventoryFlowService.AddRange(entities);
        }

        public void UpdateEntity(Erp_InventoryFlow entity)
        {
            _Erp_InventoryFlowService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_InventoryFlow> entities)
        {
           foreach (var e in entities)
            {
                _Erp_InventoryFlowService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_InventoryFlow entity)
        {
            _Erp_InventoryFlowService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_InventoryFlow> entities)
        {
            _Erp_InventoryFlowService.RemoveRange(entities);
        }

        public Erp_InventoryFlow GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_InventoryFlowService.FindList(a => a.Erp_InventoryFlowId == keyValue,a => a.Erp_InventoryFlowId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_InventoryFlow> GetEntities(Expression<Func<Erp_InventoryFlow, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_InventoryFlowService.FindList(whereExpression, a => a.Erp_InventoryFlowId, isSortAsc);
             return data;
        }

        public IList<Erp_InventoryFlow> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_InventoryFlow, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_InventoryFlowService, pagination, sort, whereExpression,a=>a.Goods,a=>a.Warehouse);
        }

        public bool IsExists(Expression<Func<Erp_InventoryFlow, bool>> whereExpression)
        {
            return _Erp_InventoryFlowService.IsExists(whereExpression);
        }

    }
}
