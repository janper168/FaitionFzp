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
    public partial class PutFilesForm : Form
    {
        private string _srcPath;
        private string _entityCodeFile;
        private string _dbSetFile;
        private string _serviceFile;
        private string _bllFile;
        private string projectPath;

        public PutFilesForm(
            string srcPath,
            string entityCodeFile,
            string dbSetFile,
            string serviceFile,
            string bllFile
            )
        {
            InitializeComponent();
            _srcPath         = srcPath;
            _entityCodeFile  = entityCodeFile;
            _dbSetFile       = dbSetFile;
            _serviceFile     = serviceFile;
            _bllFile         = bllFile;

            //MessageBox.Show(Application.StartupPath);

            projectPath = Application.StartupPath.Substring(0, Application.StartupPath.IndexOf("CodeGenerator"));

        }

        private void PutFilesForm_Load(object sender, EventArgs e)
        {
            txtModelFilePath.Text = projectPath + @"JKF.DB\Models\";
            txtDbSetFilePath.Text = projectPath +  @"JKF.DB\DbContexts\DbSets\";
            txtServiceFilePath.Text = projectPath + @"JKF.DB\Operation\";
            txtBllFilePath.Text = projectPath + @"JFK.BLL\";

            txtModelFile.Text = _entityCodeFile;
            txtDbSetFile.Text = _dbSetFile;
            txtServiceFile.Text = _serviceFile;
            txtBllFile.Text = _bllFile;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPostfixPath.Text))
            {
                txtModelFilePath.Text = txtModelFilePath.Text + txtPostfixPath.Text;
                txtDbSetFilePath.Text = txtDbSetFilePath.Text + txtPostfixPath.Text;
                txtServiceFilePath.Text = txtServiceFilePath.Text + txtPostfixPath.Text;
                txtBllFilePath.Text = txtBllFilePath.Text + txtPostfixPath.Text;


                if (!Directory.Exists(txtModelFilePath.Text))
                {
                    Directory.CreateDirectory(txtModelFilePath.Text);
                }

                if (!Directory.Exists(txtDbSetFilePath.Text))
                {
                    Directory.CreateDirectory(txtDbSetFilePath.Text);
                }

                if (!Directory.Exists(txtServiceFilePath.Text))
                {
                    Directory.CreateDirectory(txtServiceFilePath.Text);
                }

                if (!Directory.Exists(txtBllFilePath.Text))
                {
                    Directory.CreateDirectory(txtBllFilePath.Text);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPostfixPath.Text))
            {
                
                txtModelFilePath.Text = txtModelFilePath.Text.Replace(txtPostfixPath.Text, "");
                txtDbSetFilePath.Text = txtDbSetFilePath.Text.Replace(txtPostfixPath.Text, "");
                txtServiceFilePath.Text = txtServiceFilePath.Text.Replace(txtPostfixPath.Text, "");
                txtBllFilePath.Text = txtBllFilePath.Text.Replace(txtPostfixPath.Text, "");



                txtPostfixPath.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            File.Copy(_srcPath + "\\"+ _entityCodeFile, txtModelFilePath.Text + "\\" + _entityCodeFile,true);
            label2.Text = "拷贝成功！";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            File.Copy(_srcPath + "\\" + _dbSetFile, txtDbSetFilePath.Text + "\\" + _dbSetFile, true);
            label2.Text = "拷贝成功！";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            File.Copy(_srcPath + "\\" + _serviceFile, txtServiceFilePath.Text + "\\" + _serviceFile, true);
            label2.Text = "拷贝成功！";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            File.Copy(_srcPath + "\\" + _bllFile, txtBllFilePath.Text + "\\" + _bllFile, true);
            label2.Text = "拷贝成功！";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            File.Copy(_srcPath + "\\" + _entityCodeFile, txtModelFilePath.Text + "\\" + _entityCodeFile, true);
            File.Copy(_srcPath + "\\" + _dbSetFile, txtDbSetFilePath.Text + "\\" + _dbSetFile, true);
            File.Copy(_srcPath + "\\" + _serviceFile, txtServiceFilePath.Text + "\\" + _serviceFile, true);
            File.Copy(_srcPath + "\\" + _bllFile, txtBllFilePath.Text + "\\" + _bllFile, true);
            label2.Text = "一键拷贝成功！";
        }
    }
}
