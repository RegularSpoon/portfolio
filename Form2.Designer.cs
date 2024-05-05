namespace SimpleLedgerApp
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.companyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1 = new SimpleLedgerApp._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1();
            this.btnDelCompany = new System.Windows.Forms.Button();
            this.btnCancelCompany = new System.Windows.Forms.Button();
            this.btnAddCompany = new System.Windows.Forms.Button();
            this.companyTableAdapter = new SimpleLedgerApp._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1TableAdapters.CompanyTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "会社名";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(140, 93);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(170, 34);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.companyBindingSource;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(134, 182);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(170, 35);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.ValueMember = "CompanyId";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // companyBindingSource
            // 
            this.companyBindingSource.DataMember = "Company";
            this.companyBindingSource.DataSource = this._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1;
            // 
            // _desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1
            // 
            this._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1.DataSetName = "_desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1";
            this._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnDelCompany
            // 
            this.btnDelCompany.Enabled = false;
            this.btnDelCompany.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDelCompany.Location = new System.Drawing.Point(319, 184);
            this.btnDelCompany.Name = "btnDelCompany";
            this.btnDelCompany.Size = new System.Drawing.Size(75, 33);
            this.btnDelCompany.TabIndex = 10;
            this.btnDelCompany.Text = "削除";
            this.btnDelCompany.UseVisualStyleBackColor = true;
            this.btnDelCompany.Click += new System.EventHandler(this.btnDelCompany_Click_1);
            // 
            // btnCancelCompany
            // 
            this.btnCancelCompany.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCancelCompany.Location = new System.Drawing.Point(268, 261);
            this.btnCancelCompany.Name = "btnCancelCompany";
            this.btnCancelCompany.Size = new System.Drawing.Size(126, 37);
            this.btnCancelCompany.TabIndex = 11;
            this.btnCancelCompany.Text = "閉じる";
            this.btnCancelCompany.UseVisualStyleBackColor = true;
            this.btnCancelCompany.Click += new System.EventHandler(this.btnCancelCompany_Click_1);
            // 
            // btnAddCompany
            // 
            this.btnAddCompany.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAddCompany.Location = new System.Drawing.Point(319, 93);
            this.btnAddCompany.Name = "btnAddCompany";
            this.btnAddCompany.Size = new System.Drawing.Size(75, 43);
            this.btnAddCompany.TabIndex = 12;
            this.btnAddCompany.Text = "登録";
            this.btnAddCompany.UseVisualStyleBackColor = true;
            this.btnAddCompany.Click += new System.EventHandler(this.btnAddCompany_Click_1);
            // 
            // companyTableAdapter
            // 
            this.companyTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(316, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 40);
            this.button1.TabIndex = 13;
            this.button1.Text = "更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 305);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddCompany);
            this.Controls.Add(this.btnCancelCompany);
            this.Controls.Add(this.btnDelCompany);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "会社情報";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnDelCompany;
        private System.Windows.Forms.Button btnCancelCompany;
        private System.Windows.Forms.Button btnAddCompany;
        private _desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1 _desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1;
        private System.Windows.Forms.BindingSource companyBindingSource;
        private _desktop_qeo9s1k_localdb37594229b_ClassicApps_AccountInfo_dboDataSet1TableAdapters.CompanyTableAdapter companyTableAdapter;
        private System.Windows.Forms.Button button1;
    }
}