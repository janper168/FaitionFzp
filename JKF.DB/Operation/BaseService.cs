using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class BaseService<TEntity> where TEntity : BaseEntity
    {
        protected BaseDbContext _baseDbContext = null;
        protected DbSet<TEntity> _dbSet = null;
        private string _tEntityRealTypeName = "";
        private string _primaryKeyName = "";

        public BaseService(BaseDbContext baseDbContext)
        {
            _tEntityRealTypeName = typeof(TEntity).ToString();
            _tEntityRealTypeName = _tEntityRealTypeName.Substring(_tEntityRealTypeName.LastIndexOf(".") + 1);
            if (baseDbContext == null) throw new Exception("BaseService传入的baseDbContext不能为空！");
            _baseDbContext = baseDbContext;
            _dbSet = GetDbSet();
            var properties = typeof(TEntity).GetProperties();
            for (var i = 0; i < properties.Count(); i++)
            {
                var prop = properties[i];
                Attribute? attribute = prop.GetCustomAttribute(typeof(KeyAttribute));
                if (attribute != null)
                {
                    _primaryKeyName = prop.Name;
                    break;
                }
            }
        }

        public string GetKeyName()
        {
            return _primaryKeyName;
        }

        private DbSet<TEntity> GetDbSet()
        {
            PropertyInfo? prop = typeof(BaseDbContext).GetProperty(_tEntityRealTypeName);
            if (prop != null)
            {
                return (DbSet<TEntity>)prop.GetValue(_baseDbContext);
            }
            throw new Exception("BaseDbContext上下文不存在DBSet<" + _tEntityRealTypeName + ">.");
        }

        public void Add(TEntity entity, bool changeToDbRightRow = true)
        {

            _dbSet.Add(entity);

            if (changeToDbRightRow)
                _baseDbContext.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entities, bool changeToDbRightRow = true)
        {
            _dbSet.AddRange(entities);

            if (changeToDbRightRow)
                _baseDbContext.SaveChanges();
        }

        public void Update(TEntity entity, IEnumerable<PropertyInfo> modifyProperties, IEnumerable<PropertyInfo> modifyRefrence, bool changeToDbRightRow = true)
        {
            _baseDbContext.Attach(entity);
            if (modifyProperties != null)
            {
                foreach (var prop in modifyProperties)
                {
                    _baseDbContext.Entry(entity).Property(prop.Name).IsModified = true;
                }
            }

            if (modifyRefrence != null)
            {
                foreach (var reference in modifyRefrence)
                {
                    _baseDbContext.Entry(entity).Navigation(reference.Name).IsModified = true;
                }
            }


            if (changeToDbRightRow)
                _baseDbContext.SaveChanges();

        }

        public void UpdateRange(IEnumerable<TEntity> entities,
            IList<IEnumerable<PropertyInfo>> modifyPropertiesList,
            IList<IEnumerable<PropertyInfo>> modifyRefrenceList,
            bool changeToDbRightRow = true)
        {

            int index = 0;
            foreach (var entity in entities)
            {
                _baseDbContext.Attach(entity);

                if (modifyPropertiesList != null && modifyPropertiesList.Count() > 0)
                {
                    IEnumerable<PropertyInfo> modifyProperties = modifyPropertiesList[index];
                    if (modifyProperties != null && modifyProperties.Count() > 0)
                    {
                        foreach (var prop in modifyProperties)
                        {
                            _baseDbContext.Entry(entity).Property(prop.Name).IsModified = true;
                        }
                    }
                }
                if (modifyRefrenceList != null && modifyRefrenceList.Count() > 0)
                {
                    IEnumerable<PropertyInfo> modifyRefrence = modifyRefrenceList[index];
                    if (modifyRefrence != null && modifyRefrence.Count() > 0)
                    {
                        foreach (var refrence in modifyRefrence)
                        {
                            _baseDbContext.Entry(entity).Navigation(refrence.Name).IsModified = true;
                        }
                    }
                }

                index++;
            }

            if (changeToDbRightRow)
                _baseDbContext.SaveChanges();
        }

        public void Update(TEntity entity, bool changeToDbRightRow = true)
        {
            _baseDbContext.Update(entity);

            if (changeToDbRightRow)
                _baseDbContext.SaveChanges();
        }

        public void UpdateRange(IEnumerable<TEntity> entities, bool changeToDbRightRow = true)
        {
            _baseDbContext.UpdateRange(entities);

            if (changeToDbRightRow)
                _baseDbContext.SaveChanges();
        }


        public void Remove(TEntity entity, bool changeToDbRightRow = true)
        {
            _baseDbContext.Remove(entity);

            if (changeToDbRightRow)
                _baseDbContext.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities, bool changeToDbRightRow = true)
        {
            _baseDbContext.RemoveRange(entities);

            if (changeToDbRightRow)
                _baseDbContext.SaveChanges();
        }

        public IList<TEntity> FindList(bool beTracked = false)
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();

            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }

            return query.ToList();
        }

        public IList<TEntity> FindList(Pagination pagination, bool beTracked = false)
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();

            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());
            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }

            var totalCount = query.LongCount();
            pagination.TotalCount = totalCount;
            var skipCount = pagination.SkipCount;
            return query.Skip(skipCount).Take(pagination.PageSize).ToList();
        }

        public IList<TEntity> FindList<TKey>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TKey>> orderExpression,
            bool isOrderAsc = true, bool beTracked = false)
        {

            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());
            query = query.Where(expression);
            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }

            if (isOrderAsc)
                return query.OrderBy(orderExpression).ToList();
            else
                return query.OrderByDescending(orderExpression).ToList();
        }

        public IList<TEntity> FindList<TKey>(Pagination pagination, Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TKey>> orderExpression,
            bool isOrderAsc = true, bool beTracked = false)
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();

            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());
            query = query.Where(expression);
            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }

            if (isOrderAsc)
                query = query.OrderBy(orderExpression);
            else
                query = query.OrderByDescending(orderExpression);

            var totalCount = query.LongCount();
            pagination.TotalCount = totalCount;
            var skipCount = pagination.SkipCount;

            return query.Skip(skipCount).Take(pagination.PageSize).ToList();

        }



        public IList<TEntity> FindList<TParam, TKey>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TParam>> includeExpressoin,
            Expression<Func<TEntity, TKey>> orderExpression,
            bool isOrderAsc = true, bool beTracked = false)
            where TParam : class
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());

            query = query.Include(includeExpressoin).Where(expression);

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }

            if (isOrderAsc)
                return query.OrderBy(orderExpression).ToList();
            else
                return query.OrderByDescending(orderExpression).ToList();
        }

        public IList<TEntity> FindList<TParam, TKey>(Pagination pagination, Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TParam>> includeExpressoin,
            Expression<Func<TEntity, TKey>> orderExpression,
            bool isOrderAsc = true, bool beTracked = false)
            where TParam : class
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());
            query = query.Include(includeExpressoin).Where(expression);

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }
            if (isOrderAsc)
                query = query.OrderBy(orderExpression);
            else
                query = query.OrderByDescending(orderExpression);

            var totalCount = query.LongCount();
            pagination.TotalCount = totalCount;
            var skipCount = pagination.SkipCount;

            return query.Skip(skipCount).Take(pagination.PageSize).ToList();
        }

        public IList<TEntity> FindList<TParam1, TParam2, TKey>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TParam1>> includeExpression1,
            Expression<Func<TEntity, TParam2>> includeExpression2,
            Expression<Func<TEntity, TKey>> orderExpression,
            bool isOrderAsc = true, bool beTracked = false)
            where TParam1 : class
            where TParam2 : class
        {

            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();

            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());

            query = query.
                Include(includeExpression1).
                Include(includeExpression2).
                Where(expression);

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }

            query.OrderBy(orderExpression);

            if (isOrderAsc)
                return query.OrderBy(orderExpression).ToList();
            else
                return query.OrderByDescending(orderExpression).ToList();
        }

        public IList<TEntity> FindList<TParam1, TParam2, TKey>(Pagination pagination, Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TParam1>> includeExpression1,
            Expression<Func<TEntity, TParam2>> includeExpression2,
            Expression<Func<TEntity, TKey>> orderExpression,
            bool isOrderAsc = true, bool beTracked = false)
            where TParam1 : class
            where TParam2 : class
        {

            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();

            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());

            query = query.
                Include(includeExpression1).
                Include(includeExpression2).
                Where(expression);

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }

            query = query.OrderBy(orderExpression);

            if (isOrderAsc)
                query = query.OrderBy(orderExpression);
            else
                query = query.OrderByDescending(orderExpression);

            var totalCount = query.LongCount();
            pagination.TotalCount = totalCount;
            var skipCount = pagination.SkipCount;

            return query.Skip(skipCount).Take(pagination.PageSize).ToList();
        }

        public IList<TEntity> FindList<TParam1, TParam2, TParam3, TKey>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TParam1>> includeExpression1,
            Expression<Func<TEntity, TParam2>> includeExpression2,
            Expression<Func<TEntity, TParam3>> includeExpression3,
            Expression<Func<TEntity, TKey>> orderExpression,
            bool isOrderAsc = true, bool beTracked = false)
            where TParam1 : class
            where TParam2 : class
            where TParam3 : class
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();

            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());

            query = query.
                Include(includeExpression1).
                Include(includeExpression2).
                Include(includeExpression3).
                Where(expression);

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }

            if (isOrderAsc)
                return query.OrderBy(orderExpression).ToList();
            else
                return query.OrderByDescending(orderExpression).ToList();
        }

        public IList<TEntity> FindList<TParam1, TParam2, TParam3, TParam4, TKey>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TParam1>> includeExpression1,
            Expression<Func<TEntity, TParam2>> includeExpression2,
            Expression<Func<TEntity, TParam3>> includeExpression3,
            Expression<Func<TEntity, TParam4>> includeExpression4,
            Expression<Func<TEntity, TKey>> orderExpression,
            bool isOrderAsc = true, bool beTracked = false)
            where TParam1 : class
            where TParam2 : class
            where TParam3 : class
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();

            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());

            query = query.
                Include(includeExpression1).
                Include(includeExpression2).
                Include(includeExpression3).
                Include(includeExpression4).
                Where(expression);

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }

            if (isOrderAsc)
                return query.OrderBy(orderExpression).ToList();
            else
                return query.OrderByDescending(orderExpression).ToList();
        }


        public IList<TEntity> FindList<TParam1, TParam2, TParam3, TKey>(Pagination pagination, Expression<Func<TEntity, bool>> expression,
           Expression<Func<TEntity, TParam1>> includeExpression1,
           Expression<Func<TEntity, TParam2>> includeExpression2,
           Expression<Func<TEntity, TParam3>> includeExpression3,
           Expression<Func<TEntity, TKey>> orderExpression,
           bool isOrderAsc = true, bool beTracked = false)
           where TParam1 : class
           where TParam2 : class
           where TParam3 : class
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());

            query = query.
                Include(includeExpression1).
                Include(includeExpression2).
                Include(includeExpression3).
                Where(expression);

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }


            if (isOrderAsc)
                query = query.OrderBy(orderExpression);
            else
                query = query.OrderByDescending(orderExpression);

            var totalCount = query.LongCount();
            pagination.TotalCount = totalCount;
            var skipCount = pagination.SkipCount;

            return query.Skip(skipCount).Take(pagination.PageSize).ToList();
        }

        public IList<TEntity> FindList<TParam1, TParam2, TParam3, TParam4, TKey>(Pagination pagination, Expression<Func<TEntity, bool>> expression,
          Expression<Func<TEntity, TParam1>> includeExpression1,
          Expression<Func<TEntity, TParam2>> includeExpression2,
          Expression<Func<TEntity, TParam3>> includeExpression3,
          Expression<Func<TEntity, TParam4>> includeExpression4,
          Expression<Func<TEntity, TKey>> orderExpression,

          bool isOrderAsc = true, bool beTracked = false)
          where TParam1 : class
          where TParam2 : class
          where TParam3 : class
          where TParam4 : class
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());

            query = query.
                Include(includeExpression1).
                Include(includeExpression2).
                Include(includeExpression3).
                Include(includeExpression4).
                Where(expression);

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }


            if (isOrderAsc)
                query = query.OrderBy(orderExpression);
            else
                query = query.OrderByDescending(orderExpression);

            var totalCount = query.LongCount();
            pagination.TotalCount = totalCount;
            var skipCount = pagination.SkipCount;

            return query.Skip(skipCount).Take(pagination.PageSize).ToList();
        }

        public IList<TEntity> FindList<TParam1, TParam2, TParam3, TParam4, TParam5, TKey>(Pagination pagination, Expression<Func<TEntity, bool>> expression,
        Expression<Func<TEntity, TParam1>> includeExpression1,
        Expression<Func<TEntity, TParam2>> includeExpression2,
        Expression<Func<TEntity, TParam3>> includeExpression3,
        Expression<Func<TEntity, TParam4>> includeExpression4,
        Expression<Func<TEntity, TParam5>> includeExpression5,
        Expression<Func<TEntity, TKey>> orderExpression,

        bool isOrderAsc = true, bool beTracked = false)
        where TParam1 : class
        where TParam2 : class
        where TParam3 : class
        where TParam4 : class
        where TParam5 : class
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());

            query = query.
                Include(includeExpression1).
                Include(includeExpression2).
                Include(includeExpression3).
                Include(includeExpression4).
                Include(includeExpression5).
                Where(expression);

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }


            if (isOrderAsc)
                query = query.OrderBy(orderExpression);
            else
                query = query.OrderByDescending(orderExpression);

            var totalCount = query.LongCount();
            pagination.TotalCount = totalCount;
            var skipCount = pagination.SkipCount;

            return query.Skip(skipCount).Take(pagination.PageSize).ToList();
        }

        public IList<TEntity> FindList<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TKey>(Pagination pagination, Expression<Func<TEntity, bool>> expression,
       Expression<Func<TEntity, TParam1>> includeExpression1,
       Expression<Func<TEntity, TParam2>> includeExpression2,
       Expression<Func<TEntity, TParam3>> includeExpression3,
       Expression<Func<TEntity, TParam4>> includeExpression4,
       Expression<Func<TEntity, TParam5>> includeExpression5,
       Expression<Func<TEntity, TParam6>> includeExpression6,
       Expression<Func<TEntity, TKey>> orderExpression,

       bool isOrderAsc = true, bool beTracked = false)
       where TParam1 : class
       where TParam2 : class
       where TParam3 : class
       where TParam4 : class
       where TParam5 : class
            where TParam6 : class
        {
            List<Expression<Func<TEntity, bool>>> expressionList = GetDataAuthorizeExpression();
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());

            query = query.
                Include(includeExpression1).
                Include(includeExpression2).
                Include(includeExpression3).
                Include(includeExpression4).
                Include(includeExpression5).
                 Include(includeExpression6).
                Where(expression);

            foreach (var where in expressionList)
            {
                query = query.Where(where);
            }


            if (isOrderAsc)
                query = query.OrderBy(orderExpression);
            else
                query = query.OrderByDescending(orderExpression);

            var totalCount = query.LongCount();
            pagination.TotalCount = totalCount;
            var skipCount = pagination.SkipCount;

            return query.Skip(skipCount).Take(pagination.PageSize).ToList();
        }





        public TEntity Single(string keyValue, bool beTracked = false)
        {
            ParameterExpression tEntityParam = Expression.Parameter(typeof(TEntity), "t");
            MemberExpression memberExpression = Expression.Property(tEntityParam, _primaryKeyName);
            ConstantExpression constExpr = Expression.Constant(keyValue, typeof(string));
            BinaryExpression binaryExpression = Expression.Equal(memberExpression, constExpr);
            LambdaExpression lambdaExpr = Expression.Lambda(binaryExpression, new List<ParameterExpression>() { tEntityParam });

            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());
            return query.Single((Expression<Func<TEntity, bool>>)lambdaExpr);

        }

        public TEntity SingleOrDefault(string keyValue, bool beTracked = false)
        {
            ParameterExpression tEntityParam = Expression.Parameter(typeof(TEntity), "t");
            MemberExpression memberExpression = Expression.Property(tEntityParam, _primaryKeyName);
            ConstantExpression constExpr = Expression.Constant(keyValue, typeof(string));
            BinaryExpression binaryExpression = Expression.Equal(memberExpression, constExpr);
            LambdaExpression lambdaExpr = Expression.Lambda(binaryExpression, new List<ParameterExpression>() { tEntityParam });

            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());
            return query.SingleOrDefault((Expression<Func<TEntity, bool>>)lambdaExpr);

        }

        public TEntity First(Expression<Func<TEntity, bool>> expression, bool beTracked = false)
        {
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());
            return query.First(expression);
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool beTracked = false)
        {
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());
            return query.FirstOrDefault(expression);
        }

        public bool IsExists(Expression<Func<TEntity, bool>> expression, bool beTracked = false)
        {
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());
            return query.Where(expression).AsQueryable().Count() > 0;
        }

        public int Count(Expression<Func<TEntity, bool>> expression, bool beTracked = false)
        {
            var query = (beTracked ? _dbSet : _dbSet.AsNoTracking());
            return query.Where(expression).AsQueryable().Count();
        }


        public void Commit()
        {
            _baseDbContext.SaveChanges();
        }

        private List<Expression<Func<TEntity, bool>>> GetDataAuthorizeExpression()
        {
            List<string> whereSqls = null;
            whereSqls = (List<string>)WebHelper.GetHttpItems("DataAurhorizeWhereSqls");

            List<Expression<Func<TEntity, bool>>> expressionList = new List<Expression<Func<TEntity, bool>>>();

            if (whereSqls != null && whereSqls.Count > 0)
            {
                foreach (var wheresql in whereSqls)
                {
                    Expression<Func<TEntity, bool>> where = DynamicExpressionParser.ParseLambda<TEntity, bool>(new ParsingConfig(), false, wheresql);

                    expressionList.Add(where);
                }
            }
            return expressionList;

        }
    }

}
