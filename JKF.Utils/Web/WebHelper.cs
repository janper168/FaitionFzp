using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace JKF.Utils
{

    public class WebHelper
    {

        #region 添加/获取上下文信息
        /// <summary>
        /// 添加链接上下文信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="data">数据</param>
        public static void AddHttpItems(string name, object data)
        {          
            new HttpContextAccessor().HttpContext.Items.Add(name, data);
        }
        /// <summary>
        /// 更新链接上下文信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="data">数据</param>
        public static void UpdateHttpItem(string name, object data)
        {
            new HttpContextAccessor().HttpContext.Items[name] = data;
        }
        /// <summary>
        /// 获取链接上下文信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static object GetHttpItems(string name)
        {
            HttpContextAccessor accessor = new HttpContextAccessor();

            if (accessor.HttpContext == null)
            {
                return null;
            }
            return accessor.HttpContext.Items[name];
        }
        #endregion
    }
}
