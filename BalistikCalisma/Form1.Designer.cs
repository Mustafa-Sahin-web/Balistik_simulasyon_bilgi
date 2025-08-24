namespace BalistikCalisma
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpAmmoDetails = new System.Windows.Forms.GroupBox();
            this.txtAmmoDetails = new System.Windows.Forms.TextBox();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.lblSelectAmmo = new System.Windows.Forms.Label();
            this.cboAmmo = new System.Windows.Forms.ComboBox();
            this.btnSimulasyon = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.grpAmmoDetails.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(800, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Balistik Bilgi ve Simülasyon";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // grpAmmoDetails
            // 
            this.grpAmmoDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAmmoDetails.Controls.Add(this.txtAmmoDetails);
            this.grpAmmoDetails.Location = new System.Drawing.Point(12, 70);
            this.grpAmmoDetails.Name = "grpAmmoDetails";
            this.grpAmmoDetails.Size = new System.Drawing.Size(776, 308);
            this.grpAmmoDetails.TabIndex = 1;
            this.grpAmmoDetails.TabStop = false;
            this.grpAmmoDetails.Text = "Mühimmat Bilgileri";
            // 
            // txtAmmoDetails
            // 
            this.txtAmmoDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAmmoDetails.Location = new System.Drawing.Point(3, 18);
            this.txtAmmoDetails.Multiline = true;
            this.txtAmmoDetails.Name = "txtAmmoDetails";
            this.txtAmmoDetails.ReadOnly = true;
            this.txtAmmoDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAmmoDetails.Size = new System.Drawing.Size(770, 287);
            this.txtAmmoDetails.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.lblSelectAmmo);
            this.bottomPanel.Controls.Add(this.cboAmmo);
            this.bottomPanel.Controls.Add(this.btnSimulasyon);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 384);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.bottomPanel.Size = new System.Drawing.Size(800, 66);
            this.bottomPanel.TabIndex = 2;
            // 
            // lblSelectAmmo
            // 
            this.lblSelectAmmo.AutoSize = true;
            this.lblSelectAmmo.Location = new System.Drawing.Point(12, 24);
            this.lblSelectAmmo.Name = "lblSelectAmmo";
            this.lblSelectAmmo.Size = new System.Drawing.Size(115, 16);
            this.lblSelectAmmo.TabIndex = 0;
            this.lblSelectAmmo.Text = "Mühimmat seçiniz:";
            // 
            // cboAmmo
            // 
            this.cboAmmo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAmmo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAmmo.FormattingEnabled = true;
            this.cboAmmo.Location = new System.Drawing.Point(136, 21);
            this.cboAmmo.Name = "cboAmmo";
            this.cboAmmo.Size = new System.Drawing.Size(523, 24);
            this.cboAmmo.TabIndex = 1;
            this.cboAmmo.SelectedIndexChanged += new System.EventHandler(this.cboAmmo_SelectedIndexChanged);
            // 
            // btnSimulasyon
            // 
            this.btnSimulasyon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSimulasyon.Location = new System.Drawing.Point(676, 19);
            this.btnSimulasyon.Name = "btnSimulasyon";
            this.btnSimulasyon.Size = new System.Drawing.Size(112, 25);
            this.btnSimulasyon.TabIndex = 2;
            this.btnSimulasyon.Text = "Simülasyon";
            this.btnSimulasyon.UseVisualStyleBackColor = true;
            this.btnSimulasyon.Click += new System.EventHandler(this.btnSimulasyon_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 12);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(31, 16);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Ara:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(49, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(140, 22);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.grpAmmoDetails);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Balistik Bilgi ve Simülasyon";
            this.grpAmmoDetails.ResumeLayout(false);
            this.grpAmmoDetails.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpAmmoDetails;
        private System.Windows.Forms.TextBox txtAmmoDetails;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Label lblSelectAmmo;
        private System.Windows.Forms.ComboBox cboAmmo;
        private System.Windows.Forms.Button btnSimulasyon;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
    }
}

