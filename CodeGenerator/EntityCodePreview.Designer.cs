
namespace CodeGenerator
{
    partial class EntityCodePreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbEntityCtrl = new System.Windows.Forms.TabControl();
            this.tpEntityCode = new System.Windows.Forms.TabPage();
            this.rtbEntityCode = new System.Windows.Forms.RichTextBox();
            this.tbDbsetCtrl = new System.Windows.Forms.TabPage();
            this.rtbDbSet = new System.Windows.Forms.RichTextBox();
            this.tbService = new System.Windows.Forms.TabPage();
            this.rtbService = new System.Windows.Forms.RichTextBox();
            this.tbBLL = new System.Windows.Forms.TabPage();
            this.rtbBLL = new System.Windows.Forms.RichTextBox();
            this.tbEntityCtrl.SuspendLayout();
            this.tpEntityCode.SuspendLayout();
            this.tbDbsetCtrl.SuspendLayout();
            this.tbService.SuspendLayout();
            this.tbBLL.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbEntityCtrl
            // 
            this.tbEntityCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEntityCtrl.Controls.Add(this.tpEntityCode);
            this.tbEntityCtrl.Controls.Add(this.tbDbsetCtrl);
            this.tbEntityCtrl.Controls.Add(this.tbService);
            this.tbEntityCtrl.Controls.Add(this.tbBLL);
            this.tbEntityCtrl.Location = new System.Drawing.Point(27, 25);
            this.tbEntityCtrl.Name = "tbEntityCtrl";
            this.tbEntityCtrl.SelectedIndex = 0;
            this.tbEntityCtrl.Size = new System.Drawing.Size(743, 413);
            this.tbEntityCtrl.TabIndex = 0;
            // 
            // tpEntityCode
            // 
            this.tpEntityCode.Controls.Add(this.rtbEntityCode);
            this.tpEntityCode.Location = new System.Drawing.Point(4, 26);
            this.tpEntityCode.Name = "tpEntityCode";
            this.tpEntityCode.Padding = new System.Windows.Forms.Padding(3);
            this.tpEntityCode.Size = new System.Drawing.Size(735, 383);
            this.tpEntityCode.TabIndex = 0;
            this.tpEntityCode.Text = "实体代码";
            this.tpEntityCode.UseVisualStyleBackColor = true;
            // 
            // rtbEntityCode
            // 
            this.rtbEntityCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbEntityCode.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbEntityCode.Location = new System.Drawing.Point(17, 16);
            this.rtbEntityCode.Name = "rtbEntityCode";
            this.rtbEntityCode.Size = new System.Drawing.Size(700, 350);
            this.rtbEntityCode.TabIndex = 0;
            this.rtbEntityCode.Text = "";
            // 
            // tbDbsetCtrl
            // 
            this.tbDbsetCtrl.Controls.Add(this.rtbDbSet);
            this.tbDbsetCtrl.Location = new System.Drawing.Point(4, 26);
            this.tbDbsetCtrl.Name = "tbDbsetCtrl";
            this.tbDbsetCtrl.Padding = new System.Windows.Forms.Padding(3);
            this.tbDbsetCtrl.Size = new System.Drawing.Size(735, 383);
            this.tbDbsetCtrl.TabIndex = 1;
            this.tbDbsetCtrl.Text = "DBSet文件";
            this.tbDbsetCtrl.UseVisualStyleBackColor = true;
            // 
            // rtbDbSet
            // 
            this.rtbDbSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDbSet.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbDbSet.Location = new System.Drawing.Point(20, 13);
            this.rtbDbSet.Name = "rtbDbSet";
            this.rtbDbSet.Size = new System.Drawing.Size(694, 353);
            this.rtbDbSet.TabIndex = 0;
            this.rtbDbSet.Text = "";
            // 
            // tbService
            // 
            this.tbService.Controls.Add(this.rtbService);
            this.tbService.Location = new System.Drawing.Point(4, 26);
            this.tbService.Name = "tbService";
            this.tbService.Padding = new System.Windows.Forms.Padding(3);
            this.tbService.Size = new System.Drawing.Size(735, 383);
            this.tbService.TabIndex = 2;
            this.tbService.Text = "Service文件";
            this.tbService.UseVisualStyleBackColor = true;
            // 
            // rtbService
            // 
            this.rtbService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbService.Location = new System.Drawing.Point(19, 17);
            this.rtbService.Name = "rtbService";
            this.rtbService.Size = new System.Drawing.Size(700, 349);
            this.rtbService.TabIndex = 0;
            this.rtbService.Text = "";
            // 
            // tbBLL
            // 
            this.tbBLL.Controls.Add(this.rtbBLL);
            this.tbBLL.Location = new System.Drawing.Point(4, 26);
            this.tbBLL.Name = "tbBLL";
            this.tbBLL.Padding = new System.Windows.Forms.Padding(3);
            this.tbBLL.Size = new System.Drawing.Size(735, 383);
            this.tbBLL.TabIndex = 3;
            this.tbBLL.Text = "BLL文件";
            this.tbBLL.UseVisualStyleBackColor = true;
            // 
            // rtbBLL
            // 
            this.rtbBLL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbBLL.Location = new System.Drawing.Point(17, 17);
            this.rtbBLL.Name = "rtbBLL";
            this.rtbBLL.Size = new System.Drawing.Size(700, 349);
            this.rtbBLL.TabIndex = 1;
            this.rtbBLL.Text = "";
            // 
            // EntityCodePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbEntityCtrl);
            this.Name = "EntityCodePreview";
            this.Text = "实体代码预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EntityCodePreview_Load);
            this.tbEntityCtrl.ResumeLayout(false);
            this.tpEntityCode.ResumeLayout(false);
            this.tbDbsetCtrl.ResumeLayout(false);
            this.tbService.ResumeLayout(false);
            this.tbBLL.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbEntityCtrl;
        private System.Windows.Forms.TabPage tpEntityCode;
        private System.Windows.Forms.RichTextBox rtbEntityCode;
        private System.Windows.Forms.TabPage tbDbsetCtrl;
        private System.Windows.Forms.RichTextBox rtbDbSet;
        private System.Windows.Forms.TabPage tbService;
        private System.Windows.Forms.RichTextBox rtbService;
        private System.Windows.Forms.TabPage tbBLL;
        private System.Windows.Forms.RichTextBox rtbBLL;
    }
}