using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKF.BLL.Models
{
    public class WFNodesAndLinesInfo
    {
        public List<WFNode> nodes { get; set; }

        public List<WFLine> lines { get; set; }
    }

    public class WFNode
    {
        public string nodeId {get;set;}

        public int nodeType { get; set; }

        public string dataType { get; set; }

        public string posX { get; set; }

        public string posY { get; set; }
    }

    public class WFLine
    { 
        public string lineId { get; set; }

        public string type { get; set; }

        public double M { get; set; }

        public string lineText { get; set; }

        public List<double> sxy { get; set; }

        public List<double> exy { get; set; }

        public string outPointId { get; set; }

        public string inPointId { get; set; }

        public string beginNodeId { get; set; }

        public string endNodeId { get; set; }
    
    }
}
