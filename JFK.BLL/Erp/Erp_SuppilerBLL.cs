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
    public class Erp_SuppilerBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_SuppilerService _Erp_SuppilerService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_SuppilerBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_SuppilerService = new Erp_SuppilerService(_dbContext);
        }

        public void AddEntity(Erp_Suppiler entity)
        {
            entity.Create();
            _Erp_SuppilerService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_Suppiler> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_SuppilerService.AddRange(entities);
        }

        public void UpdateEntity(Erp_Suppiler entity)
        {
            _Erp_SuppilerService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_Suppiler> entities)
        {
           foreach (var e in entities)
            {
                _Erp_SuppilerService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_Suppiler entity)
        {
            _Erp_SuppilerService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_Suppiler> entities)
        {
            _Erp_SuppilerService.RemoveRange(entities);
        }

        public Erp_Suppiler GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_SuppilerService.FindList(a => a.Erp_SuppilerId == keyValue,a => a.Erp_SuppilerId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_Suppiler> GetEntities(Expression<Func<Erp_Suppiler, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_SuppilerService.FindList(whereExpression, a => a.Erp_SuppilerId, isSortAsc);
             return data;
        }

        public IList<Erp_Suppiler> GetEntities(Pagination pagination, Sort sort, Expression<Func<Erp_Suppiler, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_SuppilerService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_Suppiler, bool>> whereExpression)
        {
            return _Erp_SuppilerService.IsExists(whereExpression);
        }

    }
}
