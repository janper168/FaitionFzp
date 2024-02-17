using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class EntityDefine
    {
        public string PropertyName { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsNull { get; set; }

        public string ForeignKey { get; set; }

        public string DefaultType { get; set; }

        public string CustomerType { get; set; }

        public string Length { get; set; }

        public string Annotation { get; set; }
    }
}
