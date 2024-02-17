using System;
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
    public partial class EntityForm : Form
    {
        public EntityForm()
        {
            InitializeComponent();
            
            dgvEntityConfig.AutoGenerateColumns = false;
            dgvEntityConfig.Enabled = false;
            dgvEntityConfig.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEntityConfig.MultiSelect = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadXMlFiles();
        }

        private void btnNewEntity_Click(object sender, EventArgs e)
        {
            dgvEntityConfig.Enabled = true;

            txtEntityName.Text = "Undefined";

            dgvEntityConfig.Rows.Clear();

        }

        private void btnSaveXmlFile_Click(object sender, EventArgs e)
        {
            if (txtEntityName.Text == "Undefined")
            {
                MessageBox.Show("请输入实体的名字！");
                txtEntityName.BackColor = Color.Pink;
                txtEntityName.Focus();
                return;
            }

            List<EntityDefine> list = new List<EntityDefine>();
            int rowCount = dgvEntityConfig.RowCount;
            for (var i = 0; i < rowCount - 1; i++)
            {

                EntityDefine define = new EntityDefine();

                define.PropertyName 
                    = dgvEntityConfig.Rows[i].Cells["PropertyName"].FormattedValue.ToString();
                define.IsPrimaryKey 
                    = bool.Parse(dgvEntityConfig.Rows[i].Cells["ckbPrimaryKey"].FormattedValue.ToString());
                define.IsNull 
                    = bool.Parse(dgvEntityConfig.Rows[i].Cells["ckbNull"].FormattedValue.ToString());
                define.ForeignKey 
                   = dgvEntityConfig.Rows[i].Cells["ForeignKey"].FormattedValue.ToString();
                define.DefaultType
                   = dgvEntityConfig.Rows[i].Cells["DefaultType"].FormattedValue.ToString();
                define.CustomerType 
                    = dgvEntityConfig.Rows[i].Cells["CustomerType"].FormattedValue.ToString();
                define.Length 
                    = dgvEntityConfig.Rows[i].Cells["Length"].FormattedValue.ToString();
                define.Annotation 
                    = dgvEntityConfig.Rows[i].Cells["Annotation"].FormattedValue.ToString();

                if (string.IsNullOrEmpty(define.PropertyName))
                {
                    MessageBox.Show($"第{i + 1}行：请输入属性名！");
                    dgvEntityConfig.Rows[i].Cells["PropertyName"].Style.BackColor = Color.Pink;

                    return;
                }

                if (string.IsNullOrEmpty(define.DefaultType) && string.IsNullOrEmpty(define.CustomerType))
                {
                    MessageBox.Show($"第{i + 1}行：请输入默认类型或自定义类型！");
                    dgvEntityConfig.Rows[i].Cells["DefaultType"].Style.BackColor = Color.Pink;
                    dgvEntityConfig.Rows[i].Cells["CustomerType"].Style.BackColor = Color.Pink;
                    return;
                }

                if (!string.IsNullOrEmpty(define.DefaultType) && !string.IsNullOrEmpty(define.CustomerType))
                {
                    MessageBox.Show($"第{i + 1}行：默认类型和自定义类型只能二选一！");
                    //dgvEntityConfig.Rows[i].Cells["DefaultType"].Style.BackColor = Color.Pink;
                    //dgvEntityConfig.Rows[i].Cells["CustomerType"].Style.BackColor = Color.Pink;
                    return;
                }

                list.Add(define);
                
            }
            SaveToXml(list, txtEntityName.Text.Trim());
            MessageBox.Show("已保存");
            LoadXMlFiles();


        }
        private void SaveToXml(List<EntityDefine> list, string entityName)
        {
            var xmlString = Xml.XmlSerialize(list);

            if (string.IsNullOrEmpty(xmlString))
                throw new Exception("序列化失败！");

            var savePath = CommonDefine.XMlFilePath + entityName + ".xml";

            File.WriteAllText(savePath, xmlString);        
        }

        private void dgvEntityConfig_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex == 1) 
            {
                if (dgvEntityConfig.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString() != "")
                {
                    dgvEntityConfig.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
            }

            if (e.ColumnIndex == 4)
            {
                if (dgvEntityConfig.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString() != "")
                {
                    dgvEntityConfig.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                    dgvEntityConfig.Rows[e.RowIndex].Cells[e.ColumnIndex  + 1].Style.BackColor = Color.White;
                }
            }
            if (e.ColumnIndex == 5)
            {
                if (dgvEntityConfig.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString() != "")
                {
                    dgvEntityConfig.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                    dgvEntityConfig.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Style.BackColor = Color.White;
                }
            }
        }

        private void dgvEntityConfig_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvEntityConfig.IsCurrentCellDirty)
            {
                dgvEntityConfig.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void LoadXMlFiles()
        {
            lbXmlFiles.Items.Clear();

            DirectoryInfo dInfo = new DirectoryInfo(CommonDefine.XMlFilePath);
            FileInfo[] fInfos = dInfo.GetFiles("*.xml",SearchOption.TopDirectoryOnly);

            foreach (var f in fInfos)
            {
                var fileName = Path.GetFileName(f.FullName);
                lbXmlFiles.Items.Add(fileName);
            }
            
            
        }

        private void EntityForm_DoubleClick(object sender, EventArgs e)
        {
           


        }

        private void lbXmlFiles_DoubleClick(object sender, EventArgs e)
        {
            int selectedIndex = lbXmlFiles.SelectedIndex;
            var fileName = lbXmlFiles.Items[selectedIndex] as string;

            var path = CommonDefine.XMlFilePath + fileName;

            txtEntityName.Text = fileName.Substring(0, fileName.LastIndexOf("."));
            
            LoadXmlFile(path);

        }

        private void LoadXmlFile(string path)
        {
            dgvEntityConfig.Rows.Clear();


            string xmlContent = File.ReadAllText(path);

            List<EntityDefine> list = Xml.DeSerializer<List<EntityDefine>>(xmlContent);


            int count = 0;
            foreach (var entity in list)
            {
                DataGridViewRow dr = new DataGridViewRow();

                foreach (DataGridViewColumn c in this.dgvEntityConfig.Columns)
                {
                    // 给行添加单元格
                    dr.Cells.Add((DataGridViewCell)c.CellTemplate.Clone());
                }
                dr.Cells[1].Value = false;
                dr.Cells[1].Value = entity.PropertyName;
                dr.Cells[2].Value = entity.IsPrimaryKey;
                dr.Cells[3].Value = entity.IsNull;
                dr.Cells[4].Value = entity.ForeignKey;
                dr.Cells[5].Value = entity.DefaultType;
                dr.Cells[6].Value = entity.CustomerType;
                dr.Cells[7].Value = entity.Length;
                dr.Cells[8].Value = entity.Annotation;

                dgvEntityConfig.Rows.Add(dr);
                count++;

            }
            dgvEntityConfig.Enabled = true;
            
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < dgvEntityConfig.RowCount; i++)
            {
                DataGridViewCell cell = dgvEntityConfig.Rows[i].Cells[0];
                cell.Value = true;
            }
        }

        private void BtnReverse_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < dgvEntityConfig.RowCount; i++)
            {
                DataGridViewCell cell = dgvEntityConfig.Rows[i].Cells[0];
                cell.Value = !(bool.Parse(cell.FormattedValue.ToString()));
            }
        }

        private void btnDeleteRows_Click(object sender, EventArgs e)
        {
            if (GetFirstIndexToDeleted() == -1)
            {
                MessageBox.Show("请选中要删除的行");
                return;
            }
            var index = -1;
            while ((index = GetFirstIndexToDeleted()) != -1)
            {
                dgvEntityConfig.Rows.RemoveAt(index);
            }
        }

        private int GetFirstIndexToDeleted()
        {
            for (var i = 0; i < dgvEntityConfig.RowCount - 1; i++)
            {
                DataGridViewCell cell = dgvEntityConfig.Rows[i].Cells[0];
                //if (cell.FormattedValue != null)
                //{
                    if ((bool)cell.FormattedValue)
                    {
                        return i;
                    }
                //}
               
            }
            return -1;
        }

        private void btnDeleteXmlFile_Click(object sender, EventArgs e)
        {
            var index = lbXmlFiles.SelectedIndex;
            if (index != -1)
            {
                var fileName = lbXmlFiles.Items[index] as string;
                var path = CommonDefine.XMlFilePath + fileName;
                File.Delete(path);
                LoadXMlFiles();
            }
        }

        private void txtEntityName_TextChanged(object sender, EventArgs e)
        {
            if (!(txtEntityName.Text == "" || txtEntityName.Text == "Undefined"))
            {
                txtEntityName.BackColor = Color.White;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var index = lbXmlFiles.SelectedIndex;
            if (index != -1)
            {
                var fileName = lbXmlFiles.Items[index] as string;
                var path = CommonDefine.XMlFilePath + fileName;
                
                EntityCodePreview form = new EntityCodePreview();
                form.xmlFilePath = path;
                form.EntityName = fileName.Split(".")[0].Trim();
                form.ShowDialog();
            }

            
        }

        private void btnCreateCode_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtEntityName.Text))
            {
                MessageBox.Show("实体名称不能为空！");
                return;
            }
                

            var index = lbXmlFiles.SelectedIndex;
            if (index != -1)
            {
                var fileName = lbXmlFiles.Items[index] as string;
                var path = CommonDefine.XMlFilePath + fileName; 
                
                var xmlContent = File.ReadAllText(path);
                List<EntityDefine> list = Xml.DeSerializer<List<EntityDefine>>(xmlContent);
                string entityCode = new CodeGenerator().CreateEntityCode(txtEntityName.Text.Trim(), list);
                string dbSetCode = new CodeGenerator().CreateDbSetCode(txtEntityName.Text.Trim());
                string serviceCode = new CodeGenerator().CreateServiceCode(txtEntityName.Text.Trim());
                string bllCode = new CodeGenerator().CreateBLLCode(txtEntityName.Text.Trim());

                var entityCodeSaveFile = CommonDefine.OutputFilePath + txtEntityName.Text.Trim() + ".cs";
                File.WriteAllText(entityCodeSaveFile, entityCode, Encoding.UTF8);

                var dbSetCodeSaveFile = CommonDefine.OutputFilePath + txtEntityName.Text.Trim() + "DbSet.cs";
                File.WriteAllText(dbSetCodeSaveFile, dbSetCode, Encoding.UTF8);

                var serivceCodeSaveFile = CommonDefine.OutputFilePath + txtEntityName.Text.Trim() + "Service.cs";
                File.WriteAllText(serivceCodeSaveFile, serviceCode, Encoding.UTF8);

                var bllCodeSaveFile = CommonDefine.OutputFilePath + txtEntityName.Text.Trim() + "BLL.cs";
                File.WriteAllText(bllCodeSaveFile, bllCode, Encoding.UTF8);

                MessageBox.Show("生成成功！");
            }
        }


        private void btnInsertRows_Click(object sender, EventArgs e)
        {
            //var selectedRow = 
            var selectRows = dgvEntityConfig.SelectedRows;
            if (selectRows == null || selectRows.Count <= 0)
            {
                MessageBox.Show("请选择要在之前添加空行的行");
                return;
            }
            var selectRowIndex = selectRows[0].Index;

            DataGridViewRow insertDr = new DataGridViewRow();

            foreach (DataGridViewColumn c in this.dgvEntityConfig.Columns)
            {
                // 给行添加单元格
                insertDr.Cells.Add((DataGridViewCell)c.CellTemplate.Clone());
            }
            insertDr.Cells[1].Value = false;
            insertDr.Cells[1].Value = "";
            insertDr.Cells[2].Value = false;
            insertDr.Cells[3].Value = false;
            insertDr.Cells[4].Value = "";
            insertDr.Cells[5].Value = "";
            insertDr.Cells[6].Value = "";
            insertDr.Cells[7].Value = "";
            insertDr.Cells[8].Value = "";

            dgvEntityConfig.Rows.Insert(selectRowIndex, insertDr);
        }

        private void btnPutFiles_Click(object sender, EventArgs e)
        {
            var index = lbXmlFiles.SelectedIndex;
            if (index != -1)
            { 

                var srcPath = CommonDefine.OutputFilePath;

                var entityCodeFile = txtEntityName.Text.Trim() + ".cs";
              
                var dbSetCodeFile = txtEntityName.Text.Trim() + "DbSet.cs";
               
                var serivceCodeFile = txtEntityName.Text.Trim() + "Service.cs";
                
                var bllCodeFile = txtEntityName.Text.Trim() + "BLL.cs";

                PutFilesForm dialog = new PutFilesForm(srcPath, entityCodeFile, dbSetCodeFile, serivceCodeFile, bllCodeFile);
                dialog.ShowDialog();
                
            }
        }
    }
}
