using JKF.BLL.Base;
using JKF.BLL.Models;
using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Nist;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL
{
    public class Erp_SalesGoodsBLL
    {
        private BaseDbContext _dbContext = null;
        private Erp_SalesGoodsService _Erp_SalesGoodsService = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public Erp_SalesGoodsBLL()
        {
            _dbContext = new BaseDbContext();
            _Erp_SalesGoodsService = new Erp_SalesGoodsService(_dbContext);
        }

        public void AddEntity(Erp_SalesGoods entity)
        {
            entity.Create();
            _Erp_SalesGoodsService.Add(entity);
        }

        public void AddEntities(IEnumerable<Erp_SalesGoods> entities)
        {
            foreach (var e in entities)
            {
                 e.Create();
            }
            _Erp_SalesGoodsService.AddRange(entities);
        }

        public void UpdateEntity(Erp_SalesGoods entity)
        {
            _Erp_SalesGoodsService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void UpdateEntities(IEnumerable<Erp_SalesGoods> entities)
        {
           foreach (var e in entities)
            {
                _Erp_SalesGoodsService.Update(e,e.GetPropInfoList(),e.GetReferenceInfoList());
            }
        }

        public void RemoveEntity(Erp_SalesGoods entity)
        {
            _Erp_SalesGoodsService.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<Erp_SalesGoods> entities)
        {
            _Erp_SalesGoodsService.RemoveRange(entities);
        }

        public Erp_SalesGoods GetEntity(Expression<Func<Erp_SalesGoods, bool>> whereExpression, bool beTraced = false)
        {
            var entity = _Erp_SalesGoodsService.FindList(whereExpression, a => a.Goods,a=>a.GoodsId, true, beTraced)[0];
            return entity;
        }

        public Erp_SalesGoods GetEntity(string keyValue, bool beTraced = false)
        {
            var entity = _Erp_SalesGoodsService.FindList(a => a.Erp_SalesGoodsId == keyValue,a => a.Erp_SalesGoodsId, true, beTraced)[0];
             return entity;
        }

        public IList<Erp_SalesGoods> GetEntities(Expression<Func<Erp_SalesGoods, bool>> whereExpression, bool isSortAsc = true)
        {
            var data = _Erp_SalesGoodsService.FindList(whereExpression, a=>a.Goods,a => a.Erp_SalesGoodsId, isSortAsc);
             return data;
        }

        public IList<Erp_SalesGoods> GetEntites(Pagination pagination, Sort sort, Expression<Func<Erp_SalesGoods, bool>> whereExpression)
        {
            return _baseBLL.GetObjects(_Erp_SalesGoodsService, pagination, sort, whereExpression);
        }

        public bool IsExists(Expression<Func<Erp_SalesGoods, bool>> whereExpression)
        {
            return _Erp_SalesGoodsService.IsExists(whereExpression);
        }


        public dynamic SumSalesGoods(StaticsSalesQueryModel queryModel)
        {
            Expression<Func<Erp_SalesGoods, bool>> expression = t => true;
            // 操作时间
            if (queryModel.StartTime != null && queryModel.EndTime != null)
            {
                DateTime startTime = queryModel.StartTime.Value;
                DateTime endTime = queryModel.EndTime.Value.AddDays(1);
                expression = expression.And(a => a.SalesOrder.ProcessTime >= startTime && a.SalesOrder.ProcessTime <= endTime);
            }

            if (queryModel.CategoryId != null)
            {
                expression = expression.And(t => t.Goods.GoodsCategoryId == queryModel.CategoryId);
            }

            var iquery = _dbContext.Erp_SalesGoods.Where(expression);

            var salesCount = iquery.Select(a => a.SalesOrder.Erp_SalesOrderId).Count();
            var salesNumber = iquery.Select(a => a.SalesQuantity).Sum();
            var salesAmount = iquery.Select(a => a.TotalAmount).Sum();

            return new { salesCount, salesNumber, salesAmount };
        }
        public dynamic GetSalesGoodsList(Pagination pagination, Sort sort,
                StaticsSalesQueryModel queryModel)
        {
            Expression<Func<Erp_SalesGoods, bool>> expression = t => true;
            // 操作时间
            if (queryModel.StartTime != null && queryModel.EndTime != null)
            {
                DateTime startTime = queryModel.StartTime.Value;
                DateTime endTime = queryModel.EndTime.Value.AddDays(1);
                expression = expression.And(a => a.SalesOrder.ProcessTime >= startTime && a.SalesOrder.ProcessTime <= endTime);
            }


            if (queryModel.CategoryId != null)
            {
                expression = expression.And(t => t.Goods.GoodsCategoryId == queryModel.CategoryId);
            }


            return _baseBLL.GetObjects(_Erp_SalesGoodsService, pagination, sort, expression, a => a.Goods, a => a.Goods.GoodsCategory, a => a.SalesOrder, a => a.SalesOrder.Customer, a => a.SalesOrder.Warehouse);

        }

        public dynamic GetSalesStatics(int year)
        {
            List<dynamic> datas = new List<dynamic>();
            datas.Clear();
            for (var i = 1; i < 12; i++)
            {
                var data = GetSalesStaticByMonth(year, i);
                datas.Add(data);
            }
            return datas;
        }

        public dynamic GetSalesStaticByMonth(int year, int month) 
        {
            string yearStr = year.ToString();
            string monthStr = month > 9 ? month.ToString() : "0"+month;
            string firstDay = "01";
            DateTime beginDate = DateTime.Parse((yearStr+"-" +monthStr+"-"+firstDay));

            month++;
            string yearStrNext = month < 13 ? year.ToString() : (year+1).ToString();
            if (month == 13) month = 1;
            string monthStrNext = month < 10 ? "0" + (month).ToString() :  ""+(month);        
            string firstDayNext = "01";
            DateTime endDate = DateTime.Parse((yearStrNext + "-" + monthStrNext + "-" + firstDayNext));

            var qurey = _dbContext.Erp_SalesGoods.Where(a => a.SalesOrder.IsVoid == 0 &&
                  a.SalesOrder.ProcessTime >= beginDate && a.SalesOrder.ProcessTime < endDate);

            var quantity = qurey.Select(a => a.SalesQuantity).Sum();
            var amount = qurey.Select(a => a.TotalAmount).Sum();

            return new { quantity, amount };

        }
    }
}
