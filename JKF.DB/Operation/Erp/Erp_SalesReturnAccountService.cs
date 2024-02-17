using JKF.DB.DbContexts;
using JKF.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class Erp_SalesReturnAccountService : BaseService<Erp_SalesReturnAccount>
    {
        public Erp_SalesReturnAccountService(BaseDbContext baseDbContext) : base(baseDbContext) { }
    }
}
