using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JKF.BLL.Models
{
    public class ProductSock
    {
        public string ProductId { get; set; }
        public string BatchNo { get; set; }
        public string ProductDate { get; set; }
        public string Count { get; set; }

        public string Number { get; set; }
        public string Warehouse { get; set; }

        public string WarehouseId { get; set; }
    }

    public class MaterialSock
    {
        public string MaterialId { get; set; }
        public string BatchNo { get; set; }
        public string ProductDate { get; set; }
        public string Count { get; set; }

        public string Number { get; set; }
        public string Warehouse { get; set; }

        public string WarehouseId { get; set; }
    }

}
