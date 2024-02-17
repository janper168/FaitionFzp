using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
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
    public class Erp_PurchaseGoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_PurchaseGoodsService _Erp_PurchaseGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_PurchaseGoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_PurchaseGoodsService = new Erp_PurchaseGoodsService(_dbContext);
        }

        public void AddEntity(Erp_PurchaseGoods entity)
        {
            entity.Create();
            _Erp_PurchaseGoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_PurchaseGoods> entities)
        {
            foreach (var e in entities)
            {
                e.Create();
            }
            _Erp_PurchaseGoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_PurchaseGoods entity)
        {
            _Erp_PurchaseGoodsService.Update(entity, entity.GetPropInfoList(), entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_PurchaseGoods> entities)
        {
            foreach (var e in entities)
            {
                _Erp_PurchaseGoodsService.Update(e, e.GetPropInfoList(), e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_PurchaseGoods entity)
        {
            _Erp_PurchaseGoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_PurchaseGoods> entities)
        {
            _Erp_PurchaseGoodsService.RemoveRange(entities);
        }

        public Erp_PurchaseGoods GetEntity(Expression<Func<Erp_PurchaseGoods, bool>> whereExpression, bool beTraced = false)
        {
            var entity = _Erp_PurchaseGoodsService.FindList(whereExpression, a => a.Goods, a => a.Erp_PurchaseGoodsId, true, beTraced)[0];
            return entity;
        }

        public Erp_PurchaseGoods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_PurchaseGoodsService.FindList(a => a.Erp_PurchaseGoodsId == keyValue, a => a.Erp_PurchaseGoodsId, true, beTraced)[0];
            return entity;
        }

        public IList<Erp_PurchaseGoods> GetEntities(Expression<Func<Erp_PurchaseGoods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_PurchaseGoodsService.FindList(whereExpression, a => a.Goods, a => a.Erp_PurchaseGoodsId, isSortAsc);
            return data;
        }

        public IList<Erp_PurchaseGoods> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_PurchaseGoods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_PurchaseGoodsService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_PurchaseGoods, bool>> whereExpression)
        {
            return _Erp_PurchaseGoodsService.IsExists(whereExpression);
        }

        public dynamic SumPurchaseGoods(StaticsPurchaseQueryModel queryModel)
        {
            Expression<Func<Erp_PurchaseGoods, bool>> expression = t => true;
            // 操作时间
            if (queryModel.StartTime != null && queryModel.EndTime != null)
            {
                DateTime startTime = queryModel.StartTime.Value;
                DateTime endTime = queryModel.EndTime.Value.AddDays(1);
                expression = expression.And(a => a.PurchaseOrder.ProcessTime >= startTime && a.PurchaseOrder.ProcessTime <= endTime);
            }

            if (queryModel.CategoryId != null)
            {
                expression = expression.And(t => t.Goods.GoodsCategoryId == queryModel.CategoryId);
            }

            var iquery = _dbContext.Erp_PurchaseGoods.Where(expression);

            var purchaseCount = iquery.Select(a => a.PurchaseOrder.Erp_PurchaseOrderId).Count();
            var purchaseNumber = iquery.Select(a => a.PurchaseQuantity).Sum();
            var purchaseAmount = iquery.Select(a => a.TotalAmount).Sum();

            return new { purchaseCount, purchaseNumber, purchaseAmount };
        }
        public dynamic GetPurchseGoodsList(Pagination pagination, Sort sort,
                StaticsPurchaseQueryModel queryModel)
        {
            Expression<Func<Erp_PurchaseGoods, bool>> expression = t => true;
            // 操作时间
            if (queryModel.StartTime != null && queryModel.EndTime != null)
            {
                DateTime startTime = queryModel.StartTime.Value;
                DateTime endTime = queryModel.EndTime.Value.AddDays(1);
                expression = expression.And(a => a.PurchaseOrder.ProcessTime >= startTime && a.PurchaseOrder.ProcessTime <= endTime);
            }


            if (queryModel.CategoryId != null)
            {
                expression = expression.And(t => t.Goods.GoodsCategoryId == queryModel.CategoryId);
            }


            return _baseBLL.GetObjects(_Erp_PurchaseGoodsService, pagination, sort, expression, a => a.Goods, a => a.Goods.GoodsCategory, a => a.PurchaseOrder, a => a.PurchaseOrder.Suppiler, a => a.PurchaseOrder.Warehouse);

        }

        public dynamic GetPurchaseStatics(int year)
        {
            List<dynamic> datas = new List<dynamic>();
            datas.Clear();
            for (var i = 1; i < 12; i++)
            {
                var data = GetPurchaseStaticByMonth(year, i);
                datas.Add(data);
            }
            return datas;
        }

        public dynamic GetPurchaseStaticByMonth(int year, int month)
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

            var qurey = _dbContext.Erp_PurchaseGoods.Where(a => a.PurchaseOrder.IsVoid == 0 &&
                  a.PurchaseOrder.ProcessTime >= beginDate && a.PurchaseOrder.ProcessTime < endDate);

            var quantity = qurey.Select(a => a.PurchaseQuantity).Sum();
            var amount = qurey.Select(a => a.TotalAmount).Sum();

            return new { quantity, amount };

        }

    }
}
