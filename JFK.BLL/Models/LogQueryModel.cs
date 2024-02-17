using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Models
{
    public class LogQueryModel
    {
        public int CategoryId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string OperateUserId { get; set; }

        public string OperateAccount { get; set; }
        public string OperateType { get; set; }

        public string Module { get; set; }
        public string keyword { get; set; }



    }

    public class StaticsPurchaseQueryModel
    {
        public string CategoryId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
       
    }

    public class StaticsPurchaseReturnQueryModel
    {
        public string CategoryId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }

    public class StaticsSalesQueryModel
    {
        public string CategoryId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }

    public class StaticsSalesReturnQueryModel
    {
        public string CategoryId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }
}
