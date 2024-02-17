using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Base
{
    internal class BaseBLL
    {
        /// <summary>
        /// 根据排序具体的排序字段类型进行封装查询分页列表的方法（不带include参数）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSubService"></typeparam>
        /// <param name="subSerivce"></param>
        /// <param name="pagination"></param>
        /// <param name="sort"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public IList<T> GetObjects<T, TSubService>(TSubService subSerivce, Pagination pagination, Sort sort, Expression<Func<T, bool>> whereExpression) 
            where T: BaseEntity 
            where TSubService : BaseService<T>
        {
            ParameterExpression tEntityParam = Expression.Parameter(typeof(T), "a");
            MemberExpression memberExpression = Expression.Property(tEntityParam, sort.sortField);
            LambdaExpression lambdaExpr = Expression.Lambda(memberExpression, new List<ParameterExpression>() { tEntityParam });

            Type keyType = typeof(T).GetProperty(sort.sortField).PropertyType;

            if (keyType == typeof(int))
            {
                var objects = subSerivce.FindList(pagination, whereExpression,
                (Expression<Func<T, int>>)lambdaExpr, sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(int?))
            {
                var objects = subSerivce.FindList(pagination, whereExpression,
                (Expression<Func<T, int?>>)lambdaExpr, sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(string))
            {
                var objects = subSerivce.FindList(pagination, whereExpression,
                (Expression<Func<T, string>>)lambdaExpr, sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(DateTime?)) 
            {
                var objects = subSerivce.FindList(pagination, whereExpression,
                   (Expression<Func<T, DateTime?>>)lambdaExpr, sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(DateTime))
            {
                var objects = subSerivce.FindList(pagination, whereExpression,
                   (Expression<Func<T, DateTime>>)lambdaExpr, sort.sortOrder == "asc");
                return objects;
            }

            return null;

        }

        /// <summary>
        /// 根据排序具体的排序字段类型进行封装查询分页列表的方法（带include参数）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSubService"></typeparam>
        /// <typeparam name="includeT1"></typeparam>
        /// <typeparam name="includeT2"></typeparam>
        /// <param name="subSerivce"></param>
        /// <param name="pagination"></param>
        /// <param name="sort"></param>
        /// <param name="whereExpression"></param>
        /// <param name="includeExpression1"></param>
        /// <param name="includeExpression2"></param>
        /// <returns></returns>
        public IList<T> GetObjects<T, TSubService,includeT1,includeT2>(TSubService subSerivce, 
            Pagination pagination,
            Sort sort, 
            Expression<Func<T, bool>> whereExpression ,
            Expression<Func<T, includeT1>> includeExpression1,
            Expression<Func<T, includeT2>> includeExpression2)
           where T : BaseEntity
           where TSubService : BaseService<T>
           where includeT1:class
           where includeT2:class
        {
            ParameterExpression tEntityParam = Expression.Parameter(typeof(T), "a");
            MemberExpression memberExpression = Expression.Property(tEntityParam, sort.sortField);
            LambdaExpression lambdaExpr = Expression.Lambda(memberExpression, new List<ParameterExpression>() { tEntityParam });

            Type keyType = typeof(T).GetProperty(sort.sortField).PropertyType;

            if (keyType == typeof(int))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 (Expression<Func<T, int>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(int?))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 (Expression<Func<T, int?>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(string))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 (Expression<Func<T, string>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(DateTime?))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 (Expression<Func<T, DateTime?>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(DateTime))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 (Expression<Func<T, DateTime>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }

            return null;

        }

        /// <summary>
        /// 根据排序具体的排序字段类型进行封装查询分页列表的方法（带include参数）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSubService"></typeparam>
        /// <typeparam name="includeT1"></typeparam>
        /// <typeparam name="includeT2"></typeparam>
        /// <typeparam name="includeT3"></typeparam>
        /// <param name="subSerivce"></param>
        /// <param name="pagination"></param>
        /// <param name="sort"></param>
        /// <param name="whereExpression"></param>
        /// <param name="includeExpression1"></param>
        /// <param name="includeExpression2"></param>
        /// <param name="includeExpression3"></param>
        /// <returns></returns>
        public IList<T> GetObjects<T, TSubService, includeT1, includeT2, includeT3>(TSubService subSerivce,
            Pagination pagination,
            Sort sort,
            Expression<Func<T, bool>> whereExpression,
            Expression<Func<T, includeT1>> includeExpression1,
            Expression<Func<T, includeT2>> includeExpression2,
            Expression<Func<T, includeT3>> includeExpression3)
           where T : BaseEntity
           where TSubService : BaseService<T>
           where includeT1 : class
           where includeT2 : class
           where includeT3 : class
        {
            ParameterExpression tEntityParam = Expression.Parameter(typeof(T), "a");
            MemberExpression memberExpression = Expression.Property(tEntityParam, sort.sortField);
            LambdaExpression lambdaExpr = Expression.Lambda(memberExpression, new List<ParameterExpression>() { tEntityParam });

            Type keyType = typeof(T).GetProperty(sort.sortField).PropertyType;

            if (keyType == typeof(int))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 (Expression<Func<T, int>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(int?))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 (Expression<Func<T, int?>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(string))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 (Expression<Func<T, string>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(DateTime?))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 (Expression<Func<T, DateTime?>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(DateTime))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 (Expression<Func<T, DateTime>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }

            return null;

        }

        /// <summary>
        /// 根据排序具体的排序字段类型进行封装查询分页列表的方法（带include参数）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSubService"></typeparam>
        /// <typeparam name="includeT1"></typeparam>
        /// <typeparam name="includeT2"></typeparam>
        /// <typeparam name="includeT3"></typeparam>
        /// <typeparam name="includeT4"></typeparam>
        /// <param name="subSerivce"></param>
        /// <param name="pagination"></param>
        /// <param name="sort"></param>
        /// <param name="whereExpression"></param>
        /// <param name="includeExpression1"></param>
        /// <param name="includeExpression2"></param>
        /// <param name="includeExpression3"></param>
        /// <param name="includeExpression4"></param>
        /// <returns></returns>
        public IList<T> GetObjects<T, TSubService, includeT1, includeT2, includeT3, includeT4>(TSubService subSerivce,
           Pagination pagination,
           Sort sort,
           Expression<Func<T, bool>> whereExpression,
           Expression<Func<T, includeT1>> includeExpression1,
           Expression<Func<T, includeT2>> includeExpression2,
           Expression<Func<T, includeT3>> includeExpression3,
           Expression<Func<T, includeT4>> includeExpression4)
          where T : BaseEntity
          where TSubService : BaseService<T>
          where includeT1 : class
          where includeT2 : class
          where includeT3 : class
          where includeT4 : class
        {
            ParameterExpression tEntityParam = Expression.Parameter(typeof(T), "a");
            MemberExpression memberExpression = Expression.Property(tEntityParam, sort.sortField);
            LambdaExpression lambdaExpr = Expression.Lambda(memberExpression, new List<ParameterExpression>() { tEntityParam });

            Type keyType = typeof(T).GetProperty(sort.sortField).PropertyType;

            if (keyType == typeof(int))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 (Expression<Func<T, int>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(int?))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 (Expression<Func<T, int?>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(string))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 (Expression<Func<T, string>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(DateTime?))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 (Expression<Func<T, DateTime?>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(DateTime))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 (Expression<Func<T, DateTime>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }

            return null;

        }

        public IList<T> GetObjects<T, TSubService, includeT1, includeT2, includeT3, includeT4,includeT5>(TSubService subSerivce,
          Pagination pagination,
          Sort sort,
          Expression<Func<T, bool>> whereExpression,
          Expression<Func<T, includeT1>> includeExpression1,
          Expression<Func<T, includeT2>> includeExpression2,
          Expression<Func<T, includeT3>> includeExpression3,
          Expression<Func<T, includeT4>> includeExpression4,
          Expression<Func<T, includeT5>> includeExpression5)
         where T : BaseEntity
         where TSubService : BaseService<T>
         where includeT1 : class
         where includeT2 : class
         where includeT3 : class
         where includeT4 : class
         where includeT5 : class
        {
            ParameterExpression tEntityParam = Expression.Parameter(typeof(T), "a");
            MemberExpression memberExpression = Expression.Property(tEntityParam, sort.sortField);
            LambdaExpression lambdaExpr = Expression.Lambda(memberExpression, new List<ParameterExpression>() { tEntityParam });

            Type keyType = typeof(T).GetProperty(sort.sortField).PropertyType;

            if (keyType == typeof(int))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 includeExpression5: includeExpression5,
                 (Expression<Func<T, int>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(int?))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 includeExpression5: includeExpression5,
                 (Expression<Func<T, int?>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(string))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 includeExpression5: includeExpression5,
                 (Expression<Func<T, string>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(DateTime?))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 includeExpression5: includeExpression5,
                 (Expression<Func<T, DateTime?>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(DateTime))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 includeExpression5: includeExpression5,
                 (Expression<Func<T, DateTime>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }

            return null;

        }

        public IList<T> GetObjects<T, TSubService, includeT1, includeT2, includeT3, includeT4, includeT5, includeT6>(TSubService subSerivce,
          Pagination pagination,
          Sort sort,
          Expression<Func<T, bool>> whereExpression,
          Expression<Func<T, includeT1>> includeExpression1,
          Expression<Func<T, includeT2>> includeExpression2,
          Expression<Func<T, includeT3>> includeExpression3,
          Expression<Func<T, includeT4>> includeExpression4,
          Expression<Func<T, includeT5>> includeExpression5,
          Expression<Func<T, includeT6>> includeExpression6)
         where T : BaseEntity
         where TSubService : BaseService<T>
         where includeT1 : class
         where includeT2 : class
         where includeT3 : class
         where includeT4 : class
         where includeT5 : class
         where includeT6 : class
        {
            ParameterExpression tEntityParam = Expression.Parameter(typeof(T), "a");
            MemberExpression memberExpression = Expression.Property(tEntityParam, sort.sortField);
            LambdaExpression lambdaExpr = Expression.Lambda(memberExpression, new List<ParameterExpression>() { tEntityParam });

            Type keyType = typeof(T).GetProperty(sort.sortField).PropertyType;

            if (keyType == typeof(int))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 includeExpression5: includeExpression5,
                 includeExpression6: includeExpression6,
                 (Expression<Func<T, int>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(int?))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 includeExpression5: includeExpression5,
                 includeExpression6: includeExpression6,
                 (Expression<Func<T, int?>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(string))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 includeExpression5: includeExpression5,
                  includeExpression6: includeExpression6,
                 (Expression<Func<T, string>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;

            }
            else if (keyType == typeof(DateTime?))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 includeExpression5: includeExpression5,
                  includeExpression6: includeExpression6,
                 (Expression<Func<T, DateTime?>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }
            else if (keyType == typeof(DateTime))
            {
                var objects = subSerivce.FindList(pagination,
                 whereExpression,
                 includeExpression1: includeExpression1,
                 includeExpression2: includeExpression2,
                 includeExpression3: includeExpression3,
                 includeExpression4: includeExpression4,
                 includeExpression5: includeExpression5,
                 includeExpression6: includeExpression6,
                 (Expression<Func<T, DateTime>>)lambdaExpr,
                 sort.sortOrder == "asc");
                return objects;
            }

            return null;

        }

        /// <summary>
        /// 更新子表列表记录（先删除已有的副实体类列表，再插入已有的新副实体列表（新的列表已带关键字的值））
        /// </summary>
        /// <typeparam name="TEntity">主实体名</typeparam>
        /// <typeparam name="TSubEntity">副实体名</typeparam>
        /// <typeparam name="TSubService">副实体service类名</typeparam>
        /// <param name="entity">主实体</param>
        /// <param name="entityMainKeyName">主实体键值</param>
        /// <param name="entityName">副实体的主实体导航属性名</param>
        /// <param name="subEntityKeyName">副实体键名</param>
        /// <param name="subList">主实体的子列表属性</param>
        /// <param name="subSerivce">副实体service类</param>
        public void UpdateSubEntities<TEntity, TSubEntity, TSubService>(
            TEntity entity,
            string entityMainKeyName,
            string entityName,
            string subEntityKeyName,
            ICollection<TSubEntity> subList,
            TSubService subSerivce)

            where TEntity : BaseEntity
            where TSubEntity : BaseEntity
            where TSubService : BaseService<TSubEntity>

        {

            ParameterExpression tEntityParam = Expression.Parameter(typeof(TSubEntity), "a");
            MemberExpression memberExpression = Expression.Property(tEntityParam, entityName);
            ConstantExpression constExpr = Expression.Constant(entity, typeof(TEntity));
            BinaryExpression binaryExpression = Expression.Equal(memberExpression, constExpr);
            LambdaExpression lambdaExpr = Expression.Lambda(binaryExpression, new List<ParameterExpression>() { tEntityParam });

            ParameterExpression tEntityParam2 = Expression.Parameter(typeof(TSubEntity), "a");
            MemberExpression memberExpression2 = Expression.Property(tEntityParam2, subEntityKeyName);
            LambdaExpression lambdaExpr2 = Expression.Lambda(memberExpression2, new List<ParameterExpression>() { tEntityParam2 });

            //查出关联的副实体列表
            var existsSubObjs = subSerivce.FindList((Expression<Func<TSubEntity, bool>>)lambdaExpr, (Expression<Func<TSubEntity, string>>)lambdaExpr2);
            
            List<TSubEntity> removeList = new List<TSubEntity>(existsSubObjs);
            List<TSubEntity> addList = new List<TSubEntity>();

            //如果子列表不为空
            if (subList != null && subList.Count() > 0)
            {
                foreach (var sub in subList)
                {
                    addList.Add(sub);
                }
                //先删除旧的，再插入新的
                if (removeList.Count > 0)
                {
                    subSerivce.RemoveRange(removeList, false);
                }
                if (addList.Count > 0)
                {
                    subSerivce.AddRange(addList, false);
                }
            }
            else//如果新的副实体列表为空，就只删除旧的
            {
                if (removeList.Count > 0)
                {
                    subSerivce.RemoveRange(removeList, false);
                }
            }
            
        }

       


    }
}
