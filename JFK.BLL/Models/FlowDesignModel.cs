using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Models
{
    public class FlowDesign
    {
        public List<node> nodeList { get; set; }
        public List<line> lineList { get; set; }
    }


    public class node
    { 
        public string id { get; set; }
        public float offsetX { get; set; }
        public float offsetY { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string type { get; set; }

    }
    public class line
    {
        public string id { get; set; }
        public string startNodeId { get; set; }
        public string endNodeId { get; set; }
        public string startPosType { get; set; }
        public string endPosType { get; set; }

    }
}
