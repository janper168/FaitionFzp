using JKF.DB.DbContexts;
using JKF.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class Erp_StockInRecordService : BaseService<Erp_StockInRecord>
    {
        public Erp_StockInRecordService(BaseDbContext baseDbContext) : base(baseDbContext) { }
    }
}
