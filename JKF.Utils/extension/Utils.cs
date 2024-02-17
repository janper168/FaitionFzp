using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.extension
{
    public static class DBUtils
    {
        public static List<PropertyInfo> GetPropertyInfoList()
        {
            return new List<PropertyInfo>();
        }

        public static List<PropertyInfo> PutProperty<TEntity, TParam>(this List<PropertyInfo> propInfoList, Expression<Func<TEntity, TParam>> expression)
        {
            string propName = expression.Body.ToString().Split(".")[1].Trim();

            propInfoList.Add(typeof(TEntity).GetProperty(propName));
            return propInfoList;

        }

        /// <summary>
        /// 获取实体的key值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="modelType"></param>
        /// <param name="mainKeyAttribute"></param>
        /// <returns></returns>
        public static T GetMainKeyValue<T>(object entity, Type? modelType)
        {
            var props = modelType.GetProperties().ToList();

            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute(typeof(KeyAttribute));
                if (attr != null)
                {
                    return (T)prop.GetValue(entity);
                }
            }
            return  default(T);
        }
    }
}
