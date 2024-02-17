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
    public class Erp_StockTransferGoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_StockTransferGoodsService _Erp_StockTransferGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_StockTransferGoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_StockTransferGoodsService = new Erp_StockTransferGoodsService(_dbContext);
        }

        public void AddEntity(Erp_StockTransferGoods entity)
        {
            entity.Create();
            _Erp_StockTransferGoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_StockTransferGoods> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_StockTransferGoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_StockTransferGoods entity)
        {
            _Erp_StockTransferGoodsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_StockTransferGoods> entities)
        {
           foreach (var e in entities)
            {
                _Erp_StockTransferGoodsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_StockTransferGoods entity)
        {
            _Erp_StockTransferGoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_StockTransferGoods> entities)
        {
            _Erp_StockTransferGoodsService.RemoveRange(entities);
        }

        public Erp_StockTransferGoods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_StockTransferGoodsService.FindList(a => a.Erp_StockTransferGoodsId == keyValue,a => a.Erp_StockTransferGoodsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_StockTransferGoods> GetEntities(Expression<Func<Erp_StockTransferGoods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_StockTransferGoodsService.FindList(whereExpression, a => a.Erp_StockTransferGoodsId, isSortAsc);
             return data;
        }

        public IList<Erp_StockTransferGoods> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_StockTransferGoods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_StockTransferGoodsService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_StockTransferGoods, bool>> whereExpression)
        {
            return _Erp_StockTransferGoodsService.IsExists(whereExpression);
        }
        public dynamic GetStockTransferGoods(string Erp_StockTransferOrderId)
        {
            return _Erp_StockTransferGoodsService.FindList(a => a.StockTransferOrderId == Erp_StockTransferOrderId, a => a.Goods, a => a.GoodsId);
        }

    }
}
