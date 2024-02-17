using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Models
{
    public class ProductDetail
    {
        public string DetailId { get; set; }

        public string WarehouseId { get; set; }

        public string ProductId { get; set; }

        public string BatchNo { get; set; }

        public string ProductDate { get; set; }

        public int Count { get; set; }

        public int Number { get; set; }
    }

    public class MaterialDetails
    {
        public string DetailId { get; set; }

        public string WarehouseId { get; set; }

        public string MaterialId { get; set; }

        public string BatchNo { get; set; }

        public string ProductDate { get; set; }

        public int Count { get; set; }

        public int Number { get; set; }
    }
}
