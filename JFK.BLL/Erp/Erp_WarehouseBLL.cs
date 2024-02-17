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
    public class Erp_WarehouseBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_WarehouseService _Erp_WarehouseService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_WarehouseBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_WarehouseService = new Erp_WarehouseService(_dbContext);
        }

        public void AddEntity(Erp_Warehouse entity)
        {
            entity.Create();
            _Erp_WarehouseService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_Warehouse> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_WarehouseService.AddRange(entities);
        }

        public void UpdateEntity(Erp_Warehouse entity)
        {
            _Erp_WarehouseService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_Warehouse> entities)
        {
           foreach (var e in entities)
            {
                _Erp_WarehouseService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_Warehouse entity)
        {
            _Erp_WarehouseService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_Warehouse> entities)
        {
            _Erp_WarehouseService.RemoveRange(entities);
        }

        public Erp_Warehouse GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_WarehouseService.FindList(a => a.Erp_WarehouseId == keyValue,a => a.Erp_WarehouseId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_Warehouse> GetEntities(Expression<Func<Erp_Warehouse, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_WarehouseService.FindList(whereExpression, a => a.Erp_WarehouseId, isSortAsc);
             return data;
        }

        public IList<Erp_Warehouse> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_Warehouse, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_WarehouseService, pagination, sort, whereExpression,a=>a.Manager,a=>a.Manager);
        }

        public bool IsExists(Expression<Func<Erp_Warehouse, bool>> whereExpression)
        {
            return _Erp_WarehouseService.IsExists(whereExpression);
        }

    }
}
