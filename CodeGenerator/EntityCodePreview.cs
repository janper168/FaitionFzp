using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator
{
    public partial class EntityCodePreview : Form
    {
        public string xmlFilePath { get; set; }
        public string EntityName { get; set; }

        private string entityCodeString = "";
        private string dbSetCodeString = "";
        private string serviceCodeString = "";
        private string bllCodeString = "";

       
        public EntityCodePreview()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void EntityCodePreview_Load(object sender, EventArgs e)
        {
            if (xmlFilePath != null)
            {
                var xmlContent = File.ReadAllText(xmlFilePath);
                List<EntityDefine> list = Xml.DeSerializer<List<EntityDefine>>(xmlContent);

                entityCodeString = new CodeGenerator().CreateEntityCode(EntityName, list);
                rtbEntityCode.Text = entityCodeString;
                HightLineUtils.HightLineCSharpCdoe(rtbEntityCode);
                dbSetCodeString = new CodeGenerator().CreateDbSetCode(EntityName);
                rtbDbSet.Text = dbSetCodeString;
                HightLineUtils.HightLineCSharpCdoe(rtbDbSet);
                serviceCodeString = new CodeGenerator().CreateServiceCode(EntityName);
                rtbService.Text = serviceCodeString;
                HightLineUtils.HightLineCSharpCdoe(rtbService);
                bllCodeString = new CodeGenerator().CreateBLLCode(EntityName);
                rtbBLL.Text = bllCodeString;
                HightLineUtils.HightLineCSharpCdoe(rtbBLL);


            }

        }

    }
}
