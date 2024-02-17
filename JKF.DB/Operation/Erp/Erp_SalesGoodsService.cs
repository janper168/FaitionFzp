using JKF.DB.DbContexts;
using JKF.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class Erp_SalesGoodsService : BaseService<Erp_SalesGoods>
    {
        public Erp_SalesGoodsService(BaseDbContext baseDbContext) : base(baseDbContext) { }
    }
}
