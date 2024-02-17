using JKF.BLL.Base;
using JKF.BLL.Models;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class Erp_PurchaseReturnGoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_PurchaseReturnGoodsService _Erp_PurchaseReturnGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_PurchaseReturnGoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_PurchaseReturnGoodsService = new Erp_PurchaseReturnGoodsService(_dbContext);
        }

        public void AddEntity(Erp_PurchaseReturnGoods entity)
        {
            entity.Create();
            _Erp_PurchaseReturnGoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_PurchaseReturnGoods> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_PurchaseReturnGoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_PurchaseReturnGoods entity)
        {
            _Erp_PurchaseReturnGoodsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_PurchaseReturnGoods> entities)
        {
           foreach (var e in entities)
            {
                _Erp_PurchaseReturnGoodsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_PurchaseReturnGoods entity)
        {
            _Erp_PurchaseReturnGoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_PurchaseReturnGoods> entities)
        {
            _Erp_PurchaseReturnGoodsService.RemoveRange(entities);
        }

        public Erp_PurchaseReturnGoods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_PurchaseReturnGoodsService.FindList(a => a.Erp_PurchaseReturnGoodsId == keyValue,a => a.Erp_PurchaseReturnGoodsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_PurchaseReturnGoods> GetEntities(Expression<Func<Erp_PurchaseReturnGoods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_PurchaseReturnGoodsService.FindList(whereExpression, a => a.Erp_PurchaseReturnGoodsId, isSortAsc);
             return data;
        }

        public IList<Erp_PurchaseReturnGoods> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_PurchaseReturnGoods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_PurchaseReturnGoodsService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_PurchaseReturnGoods, bool>> whereExpression)
        {
            return _Erp_PurchaseReturnGoodsService.IsExists(whereExpression);
        }

        public dynamic GetPurchaseReutrnGoodsList(Pagination pagination, Sort sort,
                 StaticsPurchaseReturnQueryModel queryModel)
        {
            Expression<Func<Erp_PurchaseReturnGoods, bool>> expression = t => true;
            // 操作时间
            if (queryModel.StartTime != null && queryModel.EndTime != null)
            {
                DateTime startTime = queryModel.StartTime.Value;
                DateTime endTime = queryModel.EndTime.Value.AddDays(1);
                expression = expression.And(a => a.PurchaseReturnOrder.ProcessTime >= startTime && a.PurchaseReturnOrder.ProcessTime <= endTime);
            }


            if (queryModel.CategoryId != null)
            {
                expression = expression.And(t => t.Goods.GoodsCategoryId == queryModel.CategoryId);
            }


            return _baseBLL.GetObjects(_Erp_PurchaseReturnGoodsService, pagination, sort, expression,
                a => a.Goods, 
                a => a.Goods.GoodsCategory,
                a => a.PurchaseReturnOrder, 
                a => a.PurchaseReturnOrder.PurchaseOrder, 
                a => a.PurchaseReturnOrder.Suppiler, 
                a => a.PurchaseReturnOrder.Warehouse);

        }

        public dynamic SumPurchaseReturnGoods(StaticsPurchaseReturnQueryModel queryModel)
        {
            Expression<Func<Erp_PurchaseReturnGoods, bool>> expression = t => true;
            // 操作时间
            if (queryModel.StartTime != null && queryModel.EndTime != null)
            {
                DateTime startTime = queryModel.StartTime.Value;
                DateTime endTime = queryModel.EndTime.Value.AddDays(1);
                expression = expression.And(a => a.PurchaseReturnOrder.ProcessTime >= startTime && a.PurchaseReturnOrder.ProcessTime <= endTime);
            }

            if (queryModel.CategoryId != null)
            {
                expression = expression.And(t => t.Goods.GoodsCategoryId == queryModel.CategoryId);
            }

            var iquery = _dbContext.Erp_PurchaseReturnGoods.Where(expression);

            var purchaseReturnCount = iquery.Select(a => a.PurchaseReturnOrder.Erp_PurchaseReturnOrderId).Count();
            var purchaseReturnNumber = iquery.Select(a => a.ReturnQuantity).Sum();
            var purchaseReturnAmount = iquery.Select(a => a.TotalAmount).Sum();

            return new { purchaseReturnCount, purchaseReturnNumber, purchaseReturnAmount };
        }


        public dynamic GetPurchaseReturnStatics(int year)
        {
            List<dynamic> datas = new List<dynamic>();
            datas.Clear();
            for (var i = 1; i < 12; i++)
            {
                var data = GetPurchaseStaticReturnByMonth(year, i);
                datas.Add(data);
            }
            return datas;
        }

        public dynamic GetPurchaseStaticReturnByMonth(int year, int month)
        {
            string yearStr = year.ToString();
            string monthStr = month > 9 ? month.ToString() : "0" + month;
            string firstDay = "01";
            DateTime beginDate = DateTime.Parse((yearStr + "-" + monthStr + "-" + firstDay));

            month++;
            string yearStrNext = month < 13 ? year.ToString() : (year + 1).ToString();
            if (month == 13) month = 1;
            string monthStrNext = month < 10 ? "0" + (month).ToString() : "" + (month);
            string firstDayNext = "01";
            DateTime endDate = DateTime.Parse((yearStrNext + "-" + monthStrNext + "-" + firstDayNext));

            var qurey = _dbContext.Erp_PurchaseReturnGoods.Where(a => a.PurchaseReturnOrder.IsVoid == 0 &&
                  a.PurchaseReturnOrder.ProcessTime >= beginDate && a.PurchaseReturnOrder.ProcessTime < endDate);

            var quantity = qurey.Select(a => a.ReturnQuantity).Sum();
            var amount = qurey.Select(a => a.TotalAmount).Sum();

            return new { quantity, amount };

        }
    }
}
