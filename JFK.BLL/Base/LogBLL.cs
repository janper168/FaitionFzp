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

namespace JKF.BLL.Base
{
    public class LogBLL
    {
        private BaseDbContext _dbContext = null;
        private LogService _logServie = null;
        private BaseBLL _baseBLL = new BaseBLL();

        public LogBLL()
        {
            _dbContext = new BaseDbContext();
            _logServie = new LogService(_dbContext);
        }

        public void AddLog(Log log,string ip)
        {
            log.Create();
            log.OperateTime = DateTime.Now;
            log.IPAddress = ip;
            log.Host = "";
            log.Browser = "";
            _logServie.Add(log);
        }

        public void RemoveLog(int categoryId, string keepTime)
        {

            DateTime operateTime = DateTime.Now;
            if (keepTime == "7")//保留近一周
            {
                operateTime = DateTime.Now.AddDays(-7);
            }
            else if (keepTime == "1")//保留近一个月
            {
                operateTime = DateTime.Now.AddMonths(-1);
            }
            else if (keepTime == "3")//保留近三个月
            {
                operateTime = DateTime.Now.AddMonths(-3);
            }
            Expression<Func<Log,bool>> expression = a => true;
            expression = expression.And(t => t.OperateTime <= operateTime);
            expression = expression.And(t => t.CategoryId == categoryId);
            _logServie.RemoveRange(_logServie.FindList(expression, a => a.LogId));

        }

        public Log GetLog(string keyValue)
        {
            return _logServie.SingleOrDefault(keyValue);
        }

        public IList<Log> GetLogList(Pagination pagination, Sort sort, LogQueryModel queryModel, string userId)
        {

            Expression<Func<Log, bool>> expression = t => true;

            // 日志分类
            int categoryId = queryModel.CategoryId;
            expression = expression.And(t => t.CategoryId == categoryId);


            // 操作时间
            if (queryModel.StartTime != null && queryModel.EndTime != null)
            {
                DateTime startTime = queryModel.StartTime.Value;
                DateTime endTime = queryModel.EndTime.Value.AddDays(1);
                expression = expression.And(t => t.OperateTime >= startTime && t.OperateTime <= endTime);
            }
            // 操作用户Id
            //if (!queryModel.OperateUserId.IsEmpty())
            //{
            //    string OperateUserId = queryModel.OperateUserId;
            //    expression = expression.And(t => t.OperateUserId == OperateUserId);
            //}
            // 操作用户账户
            if (!queryModel.OperateAccount.IsEmpty())
            {
                string OperateAccount = queryModel.OperateAccount;
                expression = expression.And(t => t.OperateAccount.Contains(OperateAccount));
            }
            // 操作类型
            if (!queryModel.OperateType.IsEmpty())
            {
                string operateType = queryModel.OperateType;
                expression = expression.And(t => t.OperateType == operateType);
            }
            // 功能模块
            if (!queryModel.Module.IsEmpty())
            {
                string module = queryModel.Module;
                expression = expression.And(t => t.Module.Contains(module));
            }
            // 关键字
            if (!queryModel.keyword.IsEmpty())
            {
                string keyword = queryModel.keyword;
                expression = expression.And(t => t.Module.Contains(keyword) || t.OperateType.Contains(keyword) || t.IPAddress.Contains(keyword)||t.OperateAccount.Contains(keyword));
            }
            // 如果只查自己的日志
            if (queryModel.OperateUserId == "1")
            {
                var myUserId = new OperatorProvider().GetCurrent().UserId;

                expression = expression.And(t => t.OperateUserId == myUserId);
            }

            return _baseBLL.GetObjects(_logServie, pagination, sort, expression);

        }
    }
}
