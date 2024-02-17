using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Models
{
    public class Rules
    {
        public List<DataCondition> Conditions { get; set; }

        public string Formula { get; set; }

        public int EnabledMark { get; set; }
    }

    public class FormContentItem
    {
        public string Id { get; set; }
        public string value { get; set; }    
    }

    //public class FormContent 
    //{ 
    //    public List<FormContentItem> FormContentItems { get; set; }
    //}
}
