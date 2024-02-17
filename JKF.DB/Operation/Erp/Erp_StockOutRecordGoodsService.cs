using JKF.DB.DbContexts;
using JKF.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.DB.Operation
{
    public class Erp_StockOutRecordGoodsService : BaseService<Erp_StockOutRecordGoods>
    {
        public Erp_StockOutRecordGoodsService(BaseDbContext baseDbContext) : base(baseDbContext) { }
    }
}
