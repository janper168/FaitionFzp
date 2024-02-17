using JKF.BLL.Base;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class Erp_BillNoBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_BillNoService _Erp_BillNoService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_BillNoBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_BillNoService = new Erp_BillNoService(_dbContext);
        }

        public void AddEntity(Erp_BillNo entity)
        {
            entity.Create();
            _Erp_BillNoService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_BillNo> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_BillNoService.AddRange(entities);
        }

        public void UpdateEntity(Erp_BillNo entity)
        {
            _Erp_BillNoService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_BillNo> entities)
        {
           foreach (var e in entities)
            {
                _Erp_BillNoService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_BillNo entity)
        {
            _Erp_BillNoService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_BillNo> entities)
        {
            _Erp_BillNoService.RemoveRange(entities);
        }

        public Erp_BillNo GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_BillNoService.FindList(a => a.Erp_BillNoId == keyValue,a => a.Erp_BillNoId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_BillNo> GetEntities(Expression<Func<Erp_BillNo, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_BillNoService.FindList(whereExpression, a => a.Erp_BillNoId, isSortAsc);
             return data;
        }

        public IList<Erp_BillNo> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_BillNo, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_BillNoService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_BillNo, bool>> whereExpression)
        {
            return _Erp_BillNoService.IsExists(whereExpression);
        }

        public string GetNextBillNo(string type)
        {
            var date = DateTime.Now.ToString("yyyyMMdd");
            var entity = _Erp_BillNoService.FirstOrDefault(a => a.BillType == type && a.DateString == date);
            if (entity == null)
            {
                entity = new Erp_BillNo();
                entity.Create();
                entity.BillType = type;
                entity.DateString = date;
                entity.CurrentNo = "0001";
                _Erp_BillNoService.Add(entity);
                return type + date + entity.CurrentNo ; 
            }
            else
            {
                var inter = int.Parse(entity.CurrentNo);
                inter++;
                entity.CurrentNo = string.Format("{0:D4}", inter);
                _Erp_BillNoService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
                return type + date + entity.CurrentNo;

            }
        }

    }
}
