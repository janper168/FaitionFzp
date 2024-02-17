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
    public class Erp_InventoryBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_InventoryService _Erp_InventoryService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_InventoryBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_InventoryService = new Erp_InventoryService(_dbContext);
        }

        public void AddEntity(Erp_Inventory entity)
        {
            entity.Create();
            _Erp_InventoryService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_Inventory> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_InventoryService.AddRange(entities);
        }

        public void UpdateEntity(Erp_Inventory entity)
        {
            _Erp_InventoryService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_Inventory> entities)
        {
           foreach (var e in entities)
            {
                _Erp_InventoryService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_Inventory entity)
        {
            _Erp_InventoryService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_Inventory> entities)
        {
            _Erp_InventoryService.RemoveRange(entities);
        }

        public Erp_Inventory GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_InventoryService.FindList(a => a.Erp_InventoryId == keyValue,a => a.Erp_InventoryId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_Inventory> GetEntities(Expression<Func<Erp_Inventory, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_InventoryService.FindList(whereExpression, a => a.Erp_InventoryId, isSortAsc);
             return data;
        }

        public IList<Erp_Inventory> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_Inventory, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_InventoryService, pagination, sort, whereExpression,a=>a.Warehouse,a=>a.Goods);
        }

        public bool IsExists(Expression<Func<Erp_Inventory, bool>> whereExpression)
        {
            return _Erp_InventoryService.IsExists(whereExpression);
        }

    }
}
