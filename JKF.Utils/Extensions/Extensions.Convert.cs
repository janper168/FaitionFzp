using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JKF.Utils
{

    public static partial class Extensions
    {
       
        public static string ToString(this List<string> list , string comma = ",")
        {
            if (list.Count <= 0) return "";
            return list.Aggregate((b, c) => b + comma + c);
        }

        public static List<string> ToList(this string val, string comma = ",")
        {
            string[] strs = val.Split(comma,StringSplitOptions.RemoveEmptyEntries);
            return strs.ToList();
        }

        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
    }
}
