using JKF.Commons;
using JKF.DB.DbContexts;
using JKF.DB.Models;
using JKF.DB.Operation;
using JKF.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Base
{
    public class UserBLL
    {
        private BaseDbContext _dbContext = null;
        private DepartmentService _departmentService = null;
        private UserService _userService = null;
        private LogBLL _logBLL = new LogBLL();
        private BaseBLL _baseBLL = new BaseBLL();
        private PostBLL _postBLL = new PostBLL();
        private CompanyBLL _CompanyBLL = new CompanyBLL();
        private DepartmentBLL _DepartmentBLL = new DepartmentBLL();

        public UserBLL()
        {
            _dbContext = new BaseDbContext();
            _departmentService = new DepartmentService(_dbContext);
            _userService = new UserService(_dbContext);
            
        }

        public void AddUser(User entity)
        {
            entity.Create();
            _userService.Add(entity);
        }

        public void UpdateUser(User entity)
        {
            _userService.Update(entity,entity.GetPropInfoList(),entity.GetReferenceInfoList());
        }

        public void RemoveUser(User entity)
        {
            _userService.Remove(entity);
        }

        public void RemoveUsers(List<User> entities)
        {
            _userService.RemoveRange(entities);
        }
        //account获取
        public User GetUser(Expression<Func<User, bool>> whereExpression,bool trace = false)
        {
            //取第一个
            var user = _userService.FindList(whereExpression,        
                a => a.Account, true, trace)[0];
            return user;
        }

        public User GetUser(string keyValue,bool trace)
        {

            //取第一个
            var user = _userService.FindList(a => a.UserId == keyValue,
                //includeExpression1:a=>a.Department,
                //includeExpression2: a => a.Department.Company,
                a => a.Account, true, trace)[0];

            return user;

        }
        public User GetUser(string keyValue)
        {

            //取第一个
            var user = _userService.FindList(a => a.UserId == keyValue, 
                //includeExpression1:a=>a.Department,
                //includeExpression2: a => a.Department.Company,
                a => a.Account)[0];

            return user;

        }

        public IList<User> GetUsers(Pagination pagination, Sort sort ,Expression<Func<User, bool>> whereExpression)
        {
            var users = _baseBLL.GetObjects(_userService, 
                pagination, 
                sort, 
                whereExpression, 
                a => a.Department,
                a => a.Department.Company);
            return users;
        }

        public IList<User> GetUsers( Expression<Func<User, bool>> whereExpression,bool trace =false)
        {

            var posts = _userService.FindList( whereExpression,
                includeExpression1: a => a.Department,
                includeExpression2: a => a.Department.Company,
                includeExpression3: a => a.PostUserList,
                a => a.Account,true, trace);

            return posts;
        }

        public IList<User> GetUsersWithPosts(Expression<Func<User, bool>> whereExpression, bool beTraced = false)
        {

            var posts = _userService.FindList(whereExpression,
                a=>a.PostUserList,
                a => a.Account,true,true );

            return posts;
        }

        public IList<User> GetUsers2(Expression<Func<User, bool>> whereExpression)
        {

            var posts = _userService.FindList(whereExpression,
                includeExpression1: a => a.PostUserList,
                includeExpression2: a => a.RoleUserList,
                a => a.Account);

            return posts;
        }

        public bool IsExists(Expression<Func<User, bool>> whereExpression)
        {
            return _userService.IsExists(whereExpression);
        }

        public Department GetDepartment(string keyValue, bool beTraced = false)
        {

            //取第一个
            var department = _departmentService.FindList(a => a.DepartmentId == keyValue,
                a => a.SortCode, true, beTraced)[0];

            return department;

        }

        public string CheckLogin(string userAccount, string userPassword,string ip)
        {
            var user = _userService.FirstOrDefault(a => a.Account == userAccount, true);

            Log logEntity = new Log();
            logEntity.CategoryId = 1;
            logEntity.OperateTypeId = "1";
            logEntity.OperateType = "登录";
            logEntity.Module = "JKF管理系统";

            string errMsg = "";
            
            if (user == null)
            {
                errMsg = "找不到用户数据！";
                logEntity.OperateUserId = "";
                logEntity.OperateAccount = userAccount;
                logEntity.ExecuteResult = 0;
                logEntity.ExecuteResultJson = "登录失败:" + errMsg;
                
                _logBLL.AddLog(logEntity,ip);
                return errMsg;
            }

            logEntity.OperateAccount = userAccount + "(" + user.RealName + ")";
            logEntity.OperateUserId = user.UserId;

            if (user.Password ==  Md5Helper.Encrypt(userPassword,32))
            {
                //如果已存在，先注销
                OperatorProvider op = new OperatorProvider();
                OperatorModel model = op.GetCurrent();
                if (model != null)
                {
                    op.RemoveCurrent();
                }
                //再登录
                OperatorModel newModel = new OperatorModel();
                newModel.UserAccount = user.Account;
                newModel.UserPassword = user.Password;
                newModel.UserId = user.UserId;
                newModel.RealName = user.RealName;
               

                string roleIds = "";
                if (user.RoleUserList != null && user.RoleUserList.Count > 0) 
                {
                    foreach (var roleUser in user.RoleUserList)
                    {
                        roleIds += roleUser.RoleId + ",";
                    }
                    roleIds = roleIds.Remove(roleIds.Length - 1, 1);
                }
                newModel.RoleIds = roleIds;

                string postIds = "";
                if (user.PostUserList != null && user.PostUserList.Count > 0)
                {
                    foreach (var postUser in user.PostUserList)
                    {
                        postIds += postUser.PostId + ",";
                    }
                    postIds = postIds.Remove(postIds.Length - 1, 1);
                }
                newModel.PostIds = postIds;
                
                newModel.IsSystem = user.Account == "admin";
                if (!newModel.IsSystem)
                {
                    newModel.PostIdsAndDownPostIds = _postBLL.GetDownLevelPostIds(user);
                    newModel.CompanyIdAndDownCompanyIds = _CompanyBLL.GetDownLevelCompanyIds(user);
                    newModel.DepartmentIdAndDownDepartmentIds = _DepartmentBLL.GetDownLevelDepartmentIds(user);
                    newModel.DepartmentId = user.Department.DepartmentId;
                    newModel.CompanyId = user.Department.Company.CompanyId;
                }

                newModel.LoginIPAddress = ip;
                newModel.LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                newModel.LoginToken = DESEncrypt.Encrypt(Guid.NewGuid().ToString());

                op.AddCurrent(newModel);

                logEntity.ExecuteResult = 1;
                logEntity.ExecuteResultJson = "登录成功";
                _logBLL.AddLog(logEntity,ip);

                return "";
            }
            else
            {
                errMsg = "用户密码错误！";
                logEntity.ExecuteResult = 0;
                logEntity.ExecuteResultJson = "登录失败：" + errMsg;
                _logBLL.AddLog(logEntity,ip);
                return errMsg;
            }

            
        }

    }
}
