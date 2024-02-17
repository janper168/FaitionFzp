using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Models
{
    public class DataCondition
    {
        public string ConditionId { get; set; }
        public string PropertyName { get; set; }
        public string OperatorValue { get; set; }
        public string Operator { get; set; }
        public string OperatorValueType { get; set; }
        public string Sort { get; set; }
        public string ConditionText { get; set; }

    }
}
