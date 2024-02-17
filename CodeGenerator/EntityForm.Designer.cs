
namespace CodeGenerator
{
    partial class EntityForm
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
            this.dgvEntityConfig = new System.Windows.Forms.DataGridView();
            this.ckbProperty = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ckbPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ckbNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ForeignKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CustomerType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Annotation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbXmlFiles = new System.Windows.Forms.ListBox();
            this.btnNewEntity = new System.Windows.Forms.Button();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.btnSaveXmlFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.BtnReverse = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnCreateCode = new System.Windows.Forms.Button();
            this.btnDeleteRows = new System.Windows.Forms.Button();
            this.btnDeleteXmlFile = new System.Windows.Forms.Button();
            this.btnInsertRows = new System.Windows.Forms.Button();
            this.btnPutFiles = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntityConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEntityConfig
            // 
            this.dgvEntityConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEntityConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntityConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ckbProperty,
            this.PropertyName,
            this.ckbPrimaryKey,
            this.ckbNull,
            this.ForeignKey,
            this.DefaultType,
            this.CustomerType,
            this.Length,
            this.Annotation});
            this.dgvEntityConfig.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvEntityConfig.Location = new System.Drawing.Point(249, 94);
            this.dgvEntityConfig.Name = "dgvEntityConfig";
            this.dgvEntityConfig.RowTemplate.Height = 25;
            this.dgvEntityConfig.Size = new System.Drawing.Size(539, 327);
            this.dgvEntityConfig.TabIndex = 0;
            this.dgvEntityConfig.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEntityConfig_CellValueChanged);
            this.dgvEntityConfig.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvEntityConfig_CurrentCellDirtyStateChanged);
            // 
            // ckbProperty
            // 
            this.ckbProperty.FalseValue = "0";
            this.ckbProperty.Frozen = true;
            this.ckbProperty.HeaderText = "选择";
            this.ckbProperty.Name = "ckbProperty";
            this.ckbProperty.TrueValue = "1";
            this.ckbProperty.Width = 50;
            // 
            // PropertyName
            // 
            this.PropertyName.DataPropertyName = "PropertyName";
            this.PropertyName.HeaderText = "属性名";
            this.PropertyName.Name = "PropertyName";
            this.PropertyName.Width = 200;
            // 
            // ckbPrimaryKey
            // 
            this.ckbPrimaryKey.DataPropertyName = "IsPrimaryKey";
            this.ckbPrimaryKey.FalseValue = "0";
            this.ckbPrimaryKey.HeaderText = "主键";
            this.ckbPrimaryKey.Name = "ckbPrimaryKey";
            this.ckbPrimaryKey.TrueValue = "1";
            this.ckbPrimaryKey.Width = 50;
            // 
            // ckbNull
            // 
            this.ckbNull.DataPropertyName = "IsNull";
            this.ckbNull.FalseValue = "0";
            this.ckbNull.HeaderText = "可空";
            this.ckbNull.Name = "ckbNull";
            this.ckbNull.TrueValue = "1";
            this.ckbNull.Width = 50;
            // 
            // ForeignKey
            // 
            this.ForeignKey.HeaderText = "外键名称";
            this.ForeignKey.Name = "ForeignKey";
            this.ForeignKey.Width = 200;
            // 
            // DefaultType
            // 
            this.DefaultType.DataPropertyName = "DefaultType";
            this.DefaultType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DefaultType.HeaderText = "默认类型";
            this.DefaultType.Items.AddRange(new object[] {
            "",
            "byte",
            "short",
            "int",
            "long",
            "float",
            "double",
            "decimal",
            "string",
            "DateTime",
            "byte[]"});
            this.DefaultType.MaxDropDownItems = 10;
            this.DefaultType.Name = "DefaultType";
            // 
            // CustomerType
            // 
            this.CustomerType.DataPropertyName = "CustomerType";
            this.CustomerType.HeaderText = "自定义类型";
            this.CustomerType.Name = "CustomerType";
            this.CustomerType.Width = 200;
            // 
            // Length
            // 
            this.Length.DataPropertyName = "Length";
            this.Length.HeaderText = "长度";
            this.Length.Name = "Length";
            // 
            // Annotation
            // 
            this.Annotation.HeaderText = "注释";
            this.Annotation.Name = "Annotation";
            this.Annotation.Width = 300;
            // 
            // lbXmlFiles
            // 
            this.lbXmlFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbXmlFiles.FormattingEnabled = true;
            this.lbXmlFiles.ItemHeight = 17;
            this.lbXmlFiles.Location = new System.Drawing.Point(13, 94);
            this.lbXmlFiles.Name = "lbXmlFiles";
            this.lbXmlFiles.Size = new System.Drawing.Size(211, 327);
            this.lbXmlFiles.TabIndex = 1;
            this.lbXmlFiles.DoubleClick += new System.EventHandler(this.lbXmlFiles_DoubleClick);
            // 
            // btnNewEntity
            // 
            this.btnNewEntity.Location = new System.Drawing.Point(13, 13);
            this.btnNewEntity.Name = "btnNewEntity";
            this.btnNewEntity.Size = new System.Drawing.Size(75, 23);
            this.btnNewEntity.TabIndex = 2;
            this.btnNewEntity.Text = "新建";
            this.btnNewEntity.UseVisualStyleBackColor = true;
            this.btnNewEntity.Click += new System.EventHandler(this.btnNewEntity_Click);
            // 
            // txtEntityName
            // 
            this.txtEntityName.Location = new System.Drawing.Point(506, 16);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(294, 23);
            this.txtEntityName.TabIndex = 3;
            this.txtEntityName.TextChanged += new System.EventHandler(this.txtEntityName_TextChanged);
            // 
            // btnSaveXmlFile
            // 
            this.btnSaveXmlFile.Location = new System.Drawing.Point(94, 12);
            this.btnSaveXmlFile.Name = "btnSaveXmlFile";
            this.btnSaveXmlFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveXmlFile.TabIndex = 4;
            this.btnSaveXmlFile.Text = "保存定义";
            this.btnSaveXmlFile.UseVisualStyleBackColor = true;
            this.btnSaveXmlFile.Click += new System.EventHandler(this.btnSaveXmlFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "实体名称";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(435, 51);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // BtnReverse
            // 
            this.BtnReverse.Location = new System.Drawing.Point(516, 51);
            this.BtnReverse.Name = "BtnReverse";
            this.BtnReverse.Size = new System.Drawing.Size(75, 23);
            this.BtnReverse.TabIndex = 6;
            this.BtnReverse.Text = "反选";
            this.BtnReverse.UseVisualStyleBackColor = true;
            this.BtnReverse.Click += new System.EventHandler(this.BtnReverse_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(13, 51);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "预览代码";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnCreateCode
            // 
            this.btnCreateCode.Location = new System.Drawing.Point(94, 51);
            this.btnCreateCode.Name = "btnCreateCode";
            this.btnCreateCode.Size = new System.Drawing.Size(75, 23);
            this.btnCreateCode.TabIndex = 7;
            this.btnCreateCode.Text = "生成代码";
            this.btnCreateCode.UseVisualStyleBackColor = true;
            this.btnCreateCode.Click += new System.EventHandler(this.btnCreateCode_Click);
            // 
            // btnDeleteRows
            // 
            this.btnDeleteRows.Location = new System.Drawing.Point(597, 51);
            this.btnDeleteRows.Name = "btnDeleteRows";
            this.btnDeleteRows.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRows.TabIndex = 6;
            this.btnDeleteRows.Text = "删除行";
            this.btnDeleteRows.UseVisualStyleBackColor = true;
            this.btnDeleteRows.Click += new System.EventHandler(this.btnDeleteRows_Click);
            // 
            // btnDeleteXmlFile
            // 
            this.btnDeleteXmlFile.Location = new System.Drawing.Point(175, 12);
            this.btnDeleteXmlFile.Name = "btnDeleteXmlFile";
            this.btnDeleteXmlFile.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteXmlFile.TabIndex = 4;
            this.btnDeleteXmlFile.Text = "删除定义";
            this.btnDeleteXmlFile.UseVisualStyleBackColor = true;
            this.btnDeleteXmlFile.Click += new System.EventHandler(this.btnDeleteXmlFile_Click);
            // 
            // btnInsertRows
            // 
            this.btnInsertRows.Location = new System.Drawing.Point(678, 51);
            this.btnInsertRows.Name = "btnInsertRows";
            this.btnInsertRows.Size = new System.Drawing.Size(75, 23);
            this.btnInsertRows.TabIndex = 6;
            this.btnInsertRows.Text = "插入行";
            this.btnInsertRows.UseVisualStyleBackColor = true;
            this.btnInsertRows.Click += new System.EventHandler(this.btnInsertRows_Click);
            // 
            // btnPutFiles
            // 
            this.btnPutFiles.Location = new System.Drawing.Point(175, 51);
            this.btnPutFiles.Name = "btnPutFiles";
            this.btnPutFiles.Size = new System.Drawing.Size(75, 23);
            this.btnPutFiles.TabIndex = 8;
            this.btnPutFiles.Text = "放置代码";
            this.btnPutFiles.UseVisualStyleBackColor = true;
            this.btnPutFiles.Click += new System.EventHandler(this.btnPutFiles_Click);
            // 
            // EntityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPutFiles);
            this.Controls.Add(this.btnCreateCode);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnInsertRows);
            this.Controls.Add(this.btnDeleteRows);
            this.Controls.Add(this.BtnReverse);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeleteXmlFile);
            this.Controls.Add(this.btnSaveXmlFile);
            this.Controls.Add(this.txtEntityName);
            this.Controls.Add(this.btnNewEntity);
            this.Controls.Add(this.lbXmlFiles);
            this.Controls.Add(this.dgvEntityConfig);
            this.Name = "EntityForm";
            this.Text = "实体配置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntityConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEntityConfig;
        private System.Windows.Forms.ListBox lbXmlFiles;
        private System.Windows.Forms.Button btnNewEntity;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSaveXmlFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button BtnReverse;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnCreateCode;
        private System.Windows.Forms.Button btnDeleteRows;
        private System.Windows.Forms.Button btnDeleteXmlFile;
        private System.Windows.Forms.Button btnInsertRows;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ckbProperty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ckbPrimaryKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ckbNull;
        private System.Windows.Forms.DataGridViewTextBoxColumn ForeignKey;
        private System.Windows.Forms.DataGridViewComboBoxColumn DefaultType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn Annotation;
        private System.Windows.Forms.Button btnPutFiles;
    }
}