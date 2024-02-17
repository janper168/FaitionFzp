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
    public class Erp_SalesReturnGoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_SalesReturnGoodsService _Erp_SalesReturnGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_SalesReturnGoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_SalesReturnGoodsService = new Erp_SalesReturnGoodsService(_dbContext);
        }

        public void AddEntity(Erp_SalesReturnGoods entity)
        {
            entity.Create();
            _Erp_SalesReturnGoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_SalesReturnGoods> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_SalesReturnGoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_SalesReturnGoods entity)
        {
            _Erp_SalesReturnGoodsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_SalesReturnGoods> entities)
        {
           foreach (var e in entities)
            {
                _Erp_SalesReturnGoodsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_SalesReturnGoods entity)
        {
            _Erp_SalesReturnGoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_SalesReturnGoods> entities)
        {
            _Erp_SalesReturnGoodsService.RemoveRange(entities);
        }

        public Erp_SalesReturnGoods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_SalesReturnGoodsService.FindList(a => a.Erp_SalesReturnGoodsId == keyValue,a => a.Erp_SalesReturnGoodsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_SalesReturnGoods> GetEntities(Expression<Func<Erp_SalesReturnGoods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_SalesReturnGoodsService.FindList(whereExpression, a => a.Erp_SalesReturnGoodsId, isSortAsc);
             return data;
        }

        public IList<Erp_SalesReturnGoods> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_SalesReturnGoods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_SalesReturnGoodsService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_SalesReturnGoods, bool>> whereExpression)
        {
            return _Erp_SalesReturnGoodsService.IsExists(whereExpression);
        }

        public dynamic SumSalesReturnGoods(StaticsSalesReturnQueryModel queryModel)
        {
            Expression<Func<Erp_SalesReturnGoods, bool>> expression = t => true;
            // 操作时间
            if (queryModel.StartTime != null && queryModel.EndTime != null)
            {
                DateTime startTime = queryModel.StartTime.Value;
                DateTime endTime = queryModel.EndTime.Value.AddDays(1);
                expression = expression.And(a => a.SalesReturnOrder.ProcessTime >= startTime && a.SalesReturnOrder.ProcessTime <= endTime);
            }

            if (queryModel.CategoryId != null)
            {
                expression = expression.And(t => t.Goods.GoodsCategoryId == queryModel.CategoryId);
            }

            var iquery = _dbContext.Erp_SalesReturnGoods.Where(expression);

            var salesReturnCount = iquery.Select(a => a.SalesReturnOrder.Erp_SalesReturnOrderId).Count();
            var salesReturnNumber = iquery.Select(a => a.ReturnQuantity).Sum();
            var salesReturnAmount = iquery.Select(a => a.TotalAmount).Sum();

            return new { salesReturnCount, salesReturnNumber, salesReturnAmount };
        }
        public dynamic GetSalesReturnGoodsList(Pagination pagination, Sort sort,
                StaticsSalesReturnQueryModel queryModel)
        {
            Expression<Func<Erp_SalesReturnGoods, bool>> expression = t => true;
            // 操作时间
            if (queryModel.StartTime != null && queryModel.EndTime != null)
            {
                DateTime startTime = queryModel.StartTime.Value;
                DateTime endTime = queryModel.EndTime.Value.AddDays(1);
                expression = expression.And(a => a.SalesReturnOrder.ProcessTime >= startTime && a.SalesReturnOrder.ProcessTime <= endTime);
            }


            if (queryModel.CategoryId != null)
            {
                expression = expression.And(t => t.Goods.GoodsCategoryId == queryModel.CategoryId);
            }


            return _baseBLL.GetObjects(_Erp_SalesReturnGoodsService, pagination, sort, expression, a => a.Goods, a => a.Goods.GoodsCategory, a => a.SalesReturnOrder, a => a.SalesReturnOrder.SalesOrder,a => a.SalesReturnOrder.Customer, a => a.SalesReturnOrder.Warehouse);

        }

        public dynamic GetSalesReturnStatics(int year)
        {
            List<dynamic> datas = new List<dynamic>();
            datas.Clear();
            for (var i = 1; i < 12; i++)
            {
                var data = GetSalesStaticReturnByMonth(year, i);
                datas.Add(data);
            }
            return datas;
        }

        public dynamic GetSalesStaticReturnByMonth(int year, int month)
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

            var qurey = _dbContext.Erp_SalesReturnGoods.Where(a => a.SalesReturnOrder.IsVoid == 0 &&
                  a.SalesReturnOrder.ProcessTime >= beginDate && a.SalesReturnOrder.ProcessTime < endDate);

            var quantity = qurey.Select(a => a.ReturnQuantity).Sum();
            var amount = qurey.Select(a => a.TotalAmount).Sum();

            return new { quantity, amount };

        }



    }






}
