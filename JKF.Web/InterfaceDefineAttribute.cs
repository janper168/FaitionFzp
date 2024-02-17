using JKF.BLL.Base;
using JKF.BLL.Models;
using JKF.DB.Models;
using JKF.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Web
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InterfaceDefineAttribute : ActionFilterAttribute
    {

        private string _url;
        private string _returnTypeName;

        public string Url { get { return _url; } set { _url = value; } }
        public string ReturnTypeName { get { return _returnTypeName; } set { _returnTypeName = value; } }

        public InterfaceDefineAttribute(string url, string returnTypeName)
        {
            _url = url;
            _returnTypeName = returnTypeName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            DataAuthorizeBLL _dataAuthorizeBLL = new DataAuthorizeBLL();
            UserBLL _userBLL = new UserBLL();

            JKF.Utils.OperatorModel userInfo = new JKF.Utils.OperatorProvider().GetCurrent();

            List<string> whereSqls = new List<string>();
            
            List<string> roleList = userInfo.RoleIds.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            List<DataAuthorize> authorizeList = new List<DataAuthorize>();
            foreach (var roleId in roleList)
            {
                var dataAuthorizes = _dataAuthorizeBLL.GetDataAuthorizes(a => a.ObjectId == roleId && a.ObjectType == 1 && a.EnabledMark == 1).ToList();
                foreach (var da in dataAuthorizes)
                {
                    if (da.Interface.Address == _url)
                    {
                        authorizeList.Add(da);
                    }
                }               
            }

            var dataAuthorizes2 = _dataAuthorizeBLL.GetDataAuthorizes(a => a.ObjectId == userInfo.UserId && a.ObjectType == 2 && a.EnabledMark == 1).ToList();
            foreach (var da in dataAuthorizes2)
            {
                if (da.Interface.Address == _url)
                {
                    authorizeList.Add(da);
                }
            }

            var list = _userBLL.GetUsers2(a => true);

            Dictionary<string, string> userRoleIds = new Dictionary<string, string>();
            var list11 = list.Select(a => new { a.UserId, RoleIdList = (a.RoleUserList == null || a.RoleUserList.Count <=0)? new List<string>(): a.RoleUserList.Select(b => b.RoleId).OrderBy(c => c).ToList() });

            foreach (var l in list11)
            {
                var roleIds = (l.RoleIdList == null || l.RoleIdList.Count <= 0) ? "" : l.RoleIdList.Aggregate((b, c) => b + "," + c);
                userRoleIds.Add(l.UserId, roleIds);
            }

            Dictionary<string, string> userPostIds = new Dictionary<string, string>();
            var list12 = list.Select(a => new { a.UserId, PostIdList = (a.PostUserList == null || a.PostUserList.Count <= 0) ? new List<string>() : a.PostUserList.Select(b => b.PostId).OrderBy(c => c).ToList() });

            foreach (var l in list12)
            {
                var postIds = (l.PostIdList == null || l.PostIdList.Count <= 0) ? "" : l.PostIdList.Aggregate((b, c) => b + "," + c);
                userPostIds.Add(l.UserId, postIds);

            }

            foreach (var da in authorizeList)
            {
                List<DataCondition> dataConditions = JsonConvert.DeserializeObject<List<DataCondition>>(da.ConditionsJson);

                string whereSql = da.Formula;

                List<string> conditionNos = new List<string>();
                MatchCollection matches = Regex.Matches(whereSql, "{\\d+}");
                foreach (Match match in matches)
                {
                    conditionNos.Add(match.Value);
                }

                foreach (var con in conditionNos)
                {
                    whereSql = whereSql.Replace(con, dataConditions.Single(a => con.Contains(a.Sort) ).ConditionText);
                }
                whereSql = whereSql.Replace("and", "&&").Replace("or", "||");

                whereSql = "a => " + whereSql;


                //登录者id
                whereSql = whereSql.Replace("{登录者ID}", "\"" + userInfo.UserId + "\"");
                whereSql = whereSql.Replace("{登录者账号}", "\"" + userInfo.UserAccount + "\"");
                
                Match match2 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}=={登录者角色}");
                if (match2.Success == true)
                {
                    var propertyName = match2.Groups[1].Value;

                    var where2 = "";
                    if (propertyName == "a.RoleUserList")
                    {
                        var userList = new List<string>();
                        bool isAdd = true;
                        var userInfoRoles = userInfo.RoleIds.ToList(",");
                        if (userInfoRoles == null || userInfoRoles.Count <= 0) isAdd = false;
                        foreach (var key in userRoleIds.Keys)
                        {
                            var roles = userRoleIds[key];

                            var rolesList = roles.ToList(",");
                            if (rolesList == null || rolesList.Count <= 0) isAdd = false;
                            bool isExist = false;
                            if (userInfoRoles.Count != rolesList.Count)
                            {
                                isAdd = false;
                                goto GoHere;
                            }
                            foreach (var i in userInfoRoles)
                            {
                                isExist = false;
                                foreach (var j in rolesList)
                                {
                                    if (j == i)
                                    {
                                        isExist = true;
                                        break;
                                    }
                                }
                                if (isExist == false) 
                                {
                                    isAdd = false;
                                    break;
                                }                               
                            }
                        GoHere:
                            if (isAdd == true) {
                                userList.Add(key);
                            }
                            isAdd = true;
                        }
                       
                        where2 = "\"" + userList.ToString(",") + "\".Contains(a.UserId)";
                    }
                    else if (propertyName == "a.RoleId"){ //即是角色里面有存在在这些角色里面的记录
                        where2 = "\"" + userInfo.RoleIds + "\".Contains(a.RoleId)";
                    }
                    
                    whereSql = whereSql.Replace("字段{" + propertyName + "}=={登录者角色}", where2);
                }

                Match match21 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含{登录者角色}");
                if (match21.Success == true)
                {
                    var propertyName = match21.Groups[1].Value;
                    var where2 = "";
                    if (propertyName == "a.RoleUserList")
                    {
                        var userList = new List<string>();
                        bool isAdd = true;
                        var userInfoRoles = userInfo.RoleIds.ToList(",");
                        if (userInfoRoles == null || userInfoRoles.Count <= 0) isAdd = false;
                        foreach (var key in userRoleIds.Keys)
                        {
                            var roles = userRoleIds[key];
 
                            var rolesList = roles.ToList(",");
                            if (rolesList == null || rolesList.Count <= 0) isAdd = false;
                            bool isExist = false;
                            if (rolesList.Count < userInfoRoles.Count) isAdd = false;
                            foreach (var i in userInfoRoles) //登录者的角色包含于这个用户的所有角色之内
                            {
                                isExist = false;
                                foreach (var j in rolesList)
                                {
                                    isExist = false;
                                    if (j == i)
                                    {
                                        isExist = true;
                                        break;
                                    }
                                    
                                }
                                if (isExist == false)
                                {
                                    isAdd = false;
                                    break;
                                }
                            }
                            if (isAdd == true)
                            {
                                userList.Add(key);
                            }
                            isAdd = true;
                        }
                        where2 = "\"" + userList.ToString(",") + "\".Contains(a.UserId)";
                    }
                    else if (propertyName == "a.RoleId")//即是角色里面有存在在这些角色里面的记录
                    {
                        where2 = "(";
                        foreach (var i in userInfo.RoleIds)
                        {
                            where2 += "a.RoleId.Contains(\"" + i + "\") || ";
                        }
                        where2 = where2.Remove(where2.Length - 3, 3);
                        where2 += ")";
                        
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含{登录者角色}", where2);
                }

                Match match31 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含于{登录者角色}");
                if (match31.Success == true)
                {
                    var propertyName = match31.Groups[1].Value;
                    var where2 = "";
                    if (propertyName == "a.RoleUserList")
                    {
                        var userList = new List<string>();
                        bool isAdd = true;
                        var userInfoRoles = userInfo.RoleIds.ToList(",");
                        if (userInfoRoles == null || userInfoRoles.Count <= 0) isAdd = false;
                        foreach (var key in userRoleIds.Keys)
                        {
                            var roles = userRoleIds[key];

                            var rolesList = roles.ToList(",");
                            if (rolesList == null || rolesList.Count <= 0) isAdd = false;
                            if (rolesList.Count > userInfoRoles.Count) isAdd = false;
                            
                            int count = 0;
                            isAdd = false;
                            foreach (var i in rolesList)
                            {  
                                foreach (var j in userInfoRoles)
                                {
                                    if (j == i)
                                    {
                                        count++;
                                        break;
                                    }
                                }
                                if (count == rolesList.Count)
                                {
                                    isAdd = true;
                                    break;
                                }
                            }
                            if (isAdd == true)
                            {
                                userList.Add(key);
                            }
                            isAdd = false;
                        }
                        where2 = "\"" + userList.ToString(",") + "\".Contains(a.UserId)";
                    }
                    else if (propertyName == "a.RoleId")//即是角色里面有存在在这些角色里面的记录
                    {
                        where2 = "\"" + userInfo.RoleIds + "\".Contains(a.RoleId)";
                    }
                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含于{登录者角色}", where2);
                }


                Match match4 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}=={登录者岗位}");
                if (match4.Success == true)
                {
                    var propertyName = match4.Groups[1].Value;
                    var where2 = "";
                    if (propertyName == "a.PostUserList")
                    {
                        var userList = new List<string>();
                        bool isAdd = true;
                        var userInfoPosts = userInfo.PostIds.ToList(",");
                        if (userInfoPosts == null || userInfoPosts.Count <= 0) isAdd = false;
                        foreach (var key in userPostIds.Keys)
                        {
                            var posts = userPostIds[key];

                            var postsList = posts.ToList(",");
                            if (postsList == null || postsList.Count <= 0) isAdd = false;
                            bool isExist = false;
                            if (userInfoPosts.Count != postsList.Count) 
                            {
                                isAdd = false;
                                goto GoHere;
                            }
                            foreach (var i in userInfoPosts)
                            {
                                isExist = false;
                                foreach (var j in postsList)
                                {
                                    if (j == i)
                                    {
                                        isExist = true;
                                        break;
                                    }
                                }
                                if (isExist == false)
                                {
                                    isAdd = false;
                                    break;
                                }
                            }
                        GoHere:
                            if (isAdd == true)
                            {
                                userList.Add(key);
                            }
                            isAdd = true;
                        }

                        where2 = "\"" + userList.ToString(",") + "\".Contains(a.UserId)";
                    }
                    else if (propertyName == "a.PostId")//即是角色里面有存在在这些角色里面的记录
                    {
                        where2 = "\"" + userInfo.PostIds + "\".Contains(a.PostId)";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}=={登录者岗位}", where2);
                }

                Match match42 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含{登录者岗位}");
                if (match42.Success == true)
                {
                    var propertyName = match42.Groups[1].Value;
                    var where2 = "";
                    if (propertyName == "a.PostUserList")
                    {
                        var userList = new List<string>();
                        bool isAdd = true;
                        var userInfoPosts = userInfo.PostIds.ToList(",");
                        if (userInfoPosts == null || userInfoPosts.Count <= 0) isAdd = false;
                        foreach (var key in userPostIds.Keys)
                        {
                            var posts = userPostIds[key];

                            var postsList = posts.ToList(",");
                            if (postsList == null || postsList.Count <= 0) isAdd = false;
                            bool isExist = false;
                            if (postsList.Count < userInfoPosts.Count) isAdd = false;
                            foreach (var i in userInfoPosts) //登录者的角色包含于这个用户的所有角色之内
                            {
                                isExist = false;
                                foreach (var j in postsList)
                                {
                                    isExist = false;
                                    if (j == i)
                                    {
                                        isExist = true;
                                        break;
                                    }

                                }
                                if (isExist == false)
                                {
                                    isAdd = false;
                                    break;
                                }
                            }
                            if (isAdd == true)
                            {
                                userList.Add(key);
                            }
                            isAdd = true;
                        }
                        where2 = "\"" + userList.ToString(",") + "\".Contains(a.UserId)";
                    }
                    else if(propertyName == "a.PostId")
                    {
                        where2 = "(";
                        foreach (var i in userInfo.PostIds)
                        {
                            where2 += "a.PostId.Contains(\"" + i + "\") || ";
                        }
                        where2 = where2.Remove(where2.Length - 3, 3);
                        where2 += ")";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含{登录者岗位}", where2);
                }

                Match match43 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含于{登录者岗位}");
                if (match43.Success == true)
                {
                    var propertyName = match43.Groups[1].Value;
                    var where2 = "";
                    if (propertyName == "a.PostUserList")
                    {
                        var userList = new List<string>();
                        bool isAdd = true;
                        var userInfoPosts = userInfo.PostIds.ToList(",");
                        if (userInfoPosts == null || userInfoPosts.Count <= 0) isAdd = false;
                        foreach (var key in userPostIds.Keys)
                        {
                            var posts = userPostIds[key];

                            var postsList = posts.ToList(",");
                            if (postsList == null || postsList.Count <= 0) isAdd = false;
                            if (postsList.Count > userInfoPosts.Count) isAdd = false;

                            int count = 0;
                            isAdd = false;
                            foreach (var i in postsList)
                            {
                                foreach (var j in userInfoPosts)
                                {
                                    if (j == i)
                                    {
                                        count++;
                                        break;
                                    }
                                }
                                if (count == postsList.Count)
                                {
                                    isAdd = true;
                                    break;
                                }
                            }
                            if (isAdd == true)
                            {
                                userList.Add(key);
                            }
                            isAdd = false;
                        }
                        where2 = "\"" + userList.ToString(",") + "\".Contains(a.UserId)";
                    }
                    else if (propertyName == "a.PostId")
                    {
                        where2 = "\"" + userInfo.PostIds + "\".Contains(a.PostId)";
                    }
                   

                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含于{登录者岗位}", where2);
                }

                Match match5 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}=={登录者岗位及下级}");
                if (match5.Success == true)
                {
                    var propertyName = match5.Groups[1].Value;
                    var where2 = "";
                    if (propertyName == "a.PostUserList")
                    {
                        var userList = new List<string>();
                        bool isAdd = true;
                        var userInfoPosts = userInfo.PostIdsAndDownPostIds.ToList(",");
                        if (userInfoPosts == null || userInfoPosts.Count <= 0) isAdd = false;
                        foreach (var key in userPostIds.Keys)
                        {
                            var posts = userPostIds[key];

                            var postsList = posts.ToList(",");
                            if (postsList == null || postsList.Count <= 0) isAdd = false;
                            bool isExist = false;
                            if (userInfoPosts.Count != postsList.Count)
                            {
                                isAdd = false;
                                goto GoHere;
                            }
                            foreach (var i in userInfoPosts)
                            {
                                isExist = false;
                                foreach (var j in postsList)
                                {
                                    if (j == i)
                                    {
                                        isExist = true;
                                        break;
                                    }
                                }
                                if (isExist == false)
                                {
                                    isAdd = false;
                                    break;
                                }
                            }
                            GoHere:
                            if (isAdd == true)
                            {
                                userList.Add(key);
                            }
                            isAdd = true;
                        }

                        where2 = "\"" + userList.ToString(",") + "\".Contains(a.UserId)";
                    }
                    else if (propertyName == "a.PostId")
                    {
                        where2 = "\"" + userInfo.PostIdsAndDownPostIds + "\".Contains(a.PostId)";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}=={登录者岗位及下级}", where2);
                }

                Match match52 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含{登录者岗位及下级}");
                if (match52.Success == true)
                {
                    var propertyName = match52.Groups[1].Value;
                    var where2 = "";
                    if (propertyName == "a.PostUserList")
                    {
                        var userList = new List<string>();
                        bool isAdd = true;
                        var userInfoPosts = userInfo.PostIdsAndDownPostIds.ToList(",");
                        if (userInfoPosts == null || userInfoPosts.Count <= 0) isAdd = false;
                        foreach (var key in userPostIds.Keys)
                        {
                            var posts = userPostIds[key];

                            var postsList = posts.ToList(",");
                            if (postsList == null || postsList.Count <= 0) isAdd = false;
                            bool isExist = false;
                            if (postsList.Count < userInfoPosts.Count) isAdd = false;
                            foreach (var i in userInfoPosts) //登录者的角色包含于这个用户的所有角色之内
                            {
                                isExist = false;
                                foreach (var j in postsList)
                                {
                                    isExist = false;
                                    if (j == i)
                                    {
                                        isExist = true;
                                        break;
                                    }

                                }
                                if (isExist == false)
                                {
                                    isAdd = false;
                                    break;
                                }
                            }
                            if (isAdd == true)
                            {
                                userList.Add(key);
                            }
                            isAdd = true;
                        }
                        where2 = "\"" + userList.ToString(",") + "\".Contains(a.UserId)";
                    }
                    else
                    {
                        where2 = "(";
                        foreach (var i in userInfo.PostIdsAndDownPostIds)
                        {
                            where2 += "a.PostId.Contains(\"" + i + "\") || ";
                        }
                        where2 = where2.Remove(where2.Length - 3, 3);
                        where2 += ")";
                    }
                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含{登录者岗位及下级}", where2);
                }

                Match match53 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含于{登录者岗位及下级}");
                if (match53.Success == true)
                {
                    var propertyName = match53.Groups[1].Value;
                    var where2 = "";
                    if (propertyName == "a.PostUserList")
                    {
                        var userList = new List<string>();
                        bool isAdd = true;
                        var userInfoPosts = userInfo.PostIdsAndDownPostIds.ToList(",");
                        if (userInfoPosts == null || userInfoPosts.Count <= 0) isAdd = false;
                        foreach (var key in userPostIds.Keys)
                        {
                            var posts = userPostIds[key];

                            var postsList = posts.ToList(",");
                            if (postsList == null || postsList.Count <= 0) isAdd = false;
                            if (postsList.Count > userInfoPosts.Count) isAdd = false;

                            int count = 0;
                            isAdd = false;
                            foreach (var i in postsList)
                            {
                                foreach (var j in userInfoPosts)
                                {
                                    if (j == i)
                                    {
                                        count++;
                                        break;
                                    }
                                }
                                if (count == postsList.Count)
                                {
                                    isAdd = true;
                                    break;
                                }
                            }
                            if (isAdd == true)
                            {
                                userList.Add(key);
                            }
                            isAdd = false;
                        }
                        where2 = "\"" + userList.ToString(",") + "\".Contains(a.UserId)";
                    }
                    else if (propertyName == "a.PostId")
                    {
                        where2 = "\"" + userInfo.PostIdsAndDownPostIds + "\".Contains(a.PostId)";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含于{登录者岗位及下级}", where2);
                }


                Match match6 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}=={登录者公司}");
                if (match6.Success == true)
                {
                    var propertyName = match6.Groups[1].Value;

                    string where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = propertyName + "==\"" + userInfo.CompanyId + "\"";
                    }
                    else
                    {
                        where2 = propertyName + ".CompanyId==\"" + userInfo.CompanyId + "\"";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}=={登录者公司}", where2);
                }

                Match match62 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含{登录者公司}");
                if (match62.Success == true)
                {
                    var propertyName = match62.Groups[1].Value;

                    string where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = propertyName + ".Contains(\"" + userInfo.CompanyId + "\")";
                    }
                    else
                    {
                        where2 = propertyName + ".CompanyId.Contains(\"" + userInfo.CompanyId + "\")";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含{登录者公司}", where2);
                }

                Match match63 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含于{登录者公司}");
                if (match63.Success == true)
                {
                    var propertyName = match63.Groups[1].Value;

                    string where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = "\"" + userInfo.CompanyId + "\".Contains(" + propertyName + ")";
                    }
                    else
                    {
                        where2 = "\"" + userInfo.CompanyId + "\".Contains(" + propertyName + ".CompanyId)";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含于{登录者公司}", where2);
                }

                Match match7 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}=={登录者部门}");
                if (match7.Success == true)
                {
                    var propertyName = match7.Groups[1].Value;

                    string where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = propertyName + "==\"" + userInfo.DepartmentId + "\"";
                    }
                    else
                    {
                        where2 = propertyName + ".DepartmentId==\"" + userInfo.DepartmentId + "\"";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}=={登录者部门}", where2);
                }

                Match match72 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含{登录者部门}");
                if (match72.Success == true)
                {
                    var propertyName = match72.Groups[1].Value;

                    string where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = propertyName + ".Contains(\"" + userInfo.DepartmentId + "\")";
                    }
                    else
                    {
                        where2 = propertyName + ".DepartmentId.Contains(\"" + userInfo.DepartmentId + "\")";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含{登录者部门}", where2);
                }

                Match match73 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含于{登录者部门}");
                if (match73.Success == true)
                {
                    var propertyName = match73.Groups[1].Value;

                    string where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = "\"" + userInfo.DepartmentId + "\".Contains(" + propertyName + ")";
                    }
                    else
                    {
                        where2 = "\"" + userInfo.DepartmentId + "\".Contains(" + propertyName + ".DepartmentId)";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含于{登录者部门}", where2);
                }

                Match match8 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}=={登录者公司及下级}");
                if (match8.Success == true)
                {
                    var propertyName = match8.Groups[1].Value;
                    var where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = "\"" + userInfo.CompanyIdAndDownCompanyIds + "\".Contains("+ propertyName + ")";
                    }
                    else
                    {
                        where2 = "\"" + userInfo.CompanyIdAndDownCompanyIds + "\".Contains("+ propertyName + ".CompanyId)";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}=={登录者公司及下级}", where2);
                }

                Match match82 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含{登录者公司及下级}");
                if (match82.Success == true)
                {
                    var companyList = userInfo.CompanyIdAndDownCompanyIds.ToList(",");

                    var propertyName = match82.Groups[1].Value;
                    var where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = "(";
                        foreach (var i in companyList)
                        {
                            where2 += propertyName+".Contains(\"" + i + "\") || ";
                        }
                        where2 = where2.Remove(where2.Length - 3, 3);
                        where2 += ")";
                    }
                    else 
                    {
                        where2 = "(";
                        foreach (var i in companyList)
                        {
                            where2 += propertyName+".CompanyId.Contains(\"" + i + "\") || ";
                        }
                        where2 = where2.Remove(where2.Length - 3, 3);
                        where2 += ")";

                    }
                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含{登录者公司及下级}", where2);
                }

                Match match83 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含于{登录者公司及下级}");
                if (match83.Success == true)
                {
                    var propertyName = match83.Groups[1].Value;
                    var where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = "\"" + userInfo.CompanyIdAndDownCompanyIds + "\".Contains("+propertyName+")";
                    }
                    else
                    {
                        where2 = "\"" + userInfo.CompanyIdAndDownCompanyIds + "\".Contains("+propertyName+".CompanyId)";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含于{登录者公司及下级}", where2);
                }

                Match match9 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}=={登录者部门及下级}");
                if (match9.Success == true)
                {
                    var propertyName = match9.Groups[1].Value;
                    var where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = "\"" + userInfo.DepartmentIdAndDownDepartmentIds + "\".Contains(" + propertyName + ")";
                    }
                    else
                    {
                        where2 = "\"" + userInfo.DepartmentIdAndDownDepartmentIds + "\".Contains(" + propertyName + ".DepartmentId)";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}=={登录者部门及下级}", where2);
                }

                Match match92 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含{登录者部门及下级}");
                if (match92.Success == true)
                {
                    var departmentList = userInfo.DepartmentIdAndDownDepartmentIds.ToList(",");

                    var propertyName = match92.Groups[1].Value;
                    var where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = "(";
                        foreach (var i in departmentList)
                        {
                            where2 +=  propertyName + ".Contains(\"" + i + "\") || ";
                        }
                        where2 = where2.Remove(where2.Length - 3, 3);
                        where2 += ")";
                    }
                    else
                    {
                        where2 = "(";
                        foreach (var i in departmentList)
                        {
                            where2 += propertyName + ".DepartmentId.Contains(\"" + i + "\") || ";
                        }
                        where2 = where2.Remove(where2.Length - 3, 3);
                        where2 += ")";

                    }
                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含{登录者部门及下级}", where2);
                }

                Match match93 = Regex.Match(whereSql, @"字段{(a.([\w\W]+))}包含于{登录者部门及下级}");
                if (match93.Success == true)
                {
                    var propertyName = match93.Groups[1].Value;
                    var where2 = "";
                    if (propertyName.EndsWith("Id"))
                    {
                        where2 = "\"" + userInfo.DepartmentIdAndDownDepartmentIds + "\".Contains(" + propertyName + ")";
                    }
                    else
                    {
                        where2 = "\"" + userInfo.DepartmentIdAndDownDepartmentIds + "\".Contains(" + propertyName + ".DepartmentId)";
                    }

                    whereSql = whereSql.Replace("字段{" + propertyName + "}包含于{登录者部门及下级}", where2);
                }


                whereSqls.Add(whereSql);

 
            }

            
            WebHelper.AddHttpItems("DataAurhorizeWhereSqls", whereSqls);

            return;

        }
       
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

    }
}