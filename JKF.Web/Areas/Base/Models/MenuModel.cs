using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JKF.Web.Areas.Base.Models
{
    public class MenuModel
    {
        public string menuId {get;set;}

        public string menuName { get; set; }

        public string icon { get; set; }

        public string url { get; set; }

        public List<MenuModel> children { get; set; } 
    }
}
