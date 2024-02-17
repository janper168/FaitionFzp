using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Models
{
    public class Erp_StockInItem
    {
        public string StockInGoodsId { get; set; }       
        public string GoodsId { get; set; }
        public int StockInQuantity { get; set; }
        public string BatchNo { get; set; }
        public string ProductionDate { get; set; }
    }

    public class Erp_StockOutItem
    {
        public string StockOutGoodsId { get; set; }
        public string GoodsId { get; set; }
        public int StockOutQuantity { get; set; }
        public string BatchNo { get; set; }

    }
}
