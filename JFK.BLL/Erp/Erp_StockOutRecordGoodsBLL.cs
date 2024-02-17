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
    public class Erp_StockOutRecordGoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockOutRecordGoodsService _Erp_StockOutRecordGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockOutRecordGoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockOutRecordGoodsService = new Erp_StockOutRecordGoodsService(_dbContext);
        }

        public void AddEntity(Erp_StockOutRecordGoods entity)
        {
            entity.Create();
            _Erp_StockOutRecordGoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockOutRecordGoods> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_StockOutRecordGoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockOutRecordGoods entity)
        {
            _Erp_StockOutRecordGoodsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockOutRecordGoods> entities)
        {
           foreach (var e in entities)
            {
                _Erp_StockOutRecordGoodsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockOutRecordGoods entity)
        {
            _Erp_StockOutRecordGoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockOutRecordGoods> entities)
        {
            _Erp_StockOutRecordGoodsService.RemoveRange(entities);
        }

        public Erp_StockOutRecordGoods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockOutRecordGoodsService.FindList(a => a.Erp_StockOutRecordGoodsId == keyValue,a => a.Erp_StockOutRecordGoodsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_StockOutRecordGoods> GetEntities(Expression<Func<Erp_StockOutRecordGoods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockOutRecordGoodsService.FindList(whereExpression, a => a.StockOutGoods, a => a.StockOutGoods.Goods,a => a.Erp_StockOutRecordGoodsId, isSortAsc);
             return data;
        }

        public IList<Erp_StockOutRecordGoods> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_StockOutRecordGoods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockOutRecordGoodsService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_StockOutRecordGoods, bool>> whereExpression)
        {
            return _Erp_StockOutRecordGoodsService.IsExists(whereExpression);
        }

    }
}
