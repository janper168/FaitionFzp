using JKF.DB.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class BaseTransaction
    {

        private BaseDbContext _dbContext = null;

        public BaseTransaction(BaseDbContext dbContext)
        {
            if (dbContext == null) throw new Exception("传入的dbcontext不能为空！");
            _dbContext = dbContext;
        }

        public void Execute(Action<BaseDbContext, IDbTransaction> action)
        {

            var ts = _dbContext.Database.BeginTransaction();

            try
            {
                action(_dbContext, ts.GetDbTransaction());

                _dbContext.SaveChanges();

                ts.Commit();
            }
            catch (Exception ex)
            {
                ts.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                ts.Dispose();
                ts = null;
            }
        
        }
    }
}
