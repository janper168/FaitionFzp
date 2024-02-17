using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public static class CommonDefine
    {
        public static string XMlFilePath = @"E:\CodeGenerator_JFK\";
        public static string OutputFilePath = @"E:\CodeGenerator_JFK\OutPut\";

        static CommonDefine()
        {
            if (!Directory.Exists(XMlFilePath))
            {
                Directory.CreateDirectory(XMlFilePath);
            }
            if (!Directory.Exists(OutputFilePath))
            {
                Directory.CreateDirectory(OutputFilePath);
            }
        }

    }
}
