namespace Modern_Sliding_Sidebar___C_Sharp_Winform
{
    partial class ara
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ara));
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.dataGridViewHospitals = new System.Windows.Forms.DataGridView();
            this.btnFindHospitals = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHospitals)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(28, 56);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(433, 22);
            this.txtAddress.TabIndex = 2;
            // 
            // dataGridViewHospitals
            // 
            this.dataGridViewHospitals.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridViewHospitals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHospitals.Location = new System.Drawing.Point(28, 103);
            this.dataGridViewHospitals.Name = "dataGridViewHospitals";
            this.dataGridViewHospitals.RowHeadersWidth = 51;
            this.dataGridViewHospitals.RowTemplate.Height = 24;
            this.dataGridViewHospitals.Size = new System.Drawing.Size(955, 501);
            this.dataGridViewHospitals.TabIndex = 3;
            // 
            // btnFindHospitals
            // 
            this.btnFindHospitals.BackColor = System.Drawing.Color.Teal;
            this.btnFindHospitals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindHospitals.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnFindHospitals.ForeColor = System.Drawing.Color.White;
            this.btnFindHospitals.Location = new System.Drawing.Point(477, 32);
            this.btnFindHospitals.Name = "btnFindHospitals";
            this.btnFindHospitals.Size = new System.Drawing.Size(155, 46);
            this.btnFindHospitals.TabIndex = 4;
            this.btnFindHospitals.Text = "Hastane Bul";
            this.btnFindHospitals.UseVisualStyleBackColor = false;
            this.btnFindHospitals.Click += new System.EventHandler(this.btnFindHospitals_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Teal;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(645, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 46);
            this.button1.TabIndex = 5;
            this.button1.Text = "Eczane Bul";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(24, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Adres bilgisi giriniz (Şehir, Cadde, Sokak vs.)";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.IndianRed;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(812, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(171, 46);
            this.button2.TabIndex = 7;
            this.button2.Text = "Nöbetçi Eczane";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1012, 633);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnFindHospitals);
            this.Controls.Add(this.dataGridViewHospitals);
            this.Controls.Add(this.txtAddress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ara";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hastane Ara";
            this.Load += new System.EventHandler(this.ara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHospitals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.DataGridView dataGridViewHospitals;
        private System.Windows.Forms.Button btnFindHospitals;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}