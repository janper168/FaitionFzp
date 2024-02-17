using JKF.DB.DbContexts;
using JKF.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class Erp_StockInOrderService : BaseService<Erp_StockInOrder>
    {
        public Erp_StockInOrderService(BaseDbContext baseDbContext) : base(baseDbContext) { }
    }
}
