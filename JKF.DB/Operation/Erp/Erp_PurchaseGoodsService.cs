using JKF.DB.DbContexts;
using JKF.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class Erp_PurchaseGoodsService : BaseService<Erp_PurchaseGoods>
    {
        public Erp_PurchaseGoodsService(BaseDbContext baseDbContext) : base(baseDbContext) { }
    }
}
