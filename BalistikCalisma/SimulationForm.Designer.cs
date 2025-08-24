namespace BalistikCalisma
{
    partial class SimulationForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSelectedAmmo = new System.Windows.Forms.Label();
            this.tableInputs = new System.Windows.Forms.TableLayoutPanel();
            this.lblV0 = new System.Windows.Forms.Label();
            this.lblAngle = new System.Windows.Forms.Label();
            this.lblMass = new System.Windows.Forms.Label();
            this.lblGravity = new System.Windows.Forms.Label();
            this.numV0 = new System.Windows.Forms.NumericUpDown();
            this.numAngle = new System.Windows.Forms.NumericUpDown();
            this.numMass = new System.Windows.Forms.NumericUpDown();
            this.numGravity = new System.Windows.Forms.NumericUpDown();
            this.lblCd = new System.Windows.Forms.Label();
            this.numCd = new System.Windows.Forms.NumericUpDown();
            this.lblArea = new System.Windows.Forms.Label();
            this.numArea = new System.Windows.Forms.NumericUpDown();
            this.lblRho = new System.Windows.Forms.Label();
            this.numRho = new System.Windows.Forms.NumericUpDown();
            this.chkRhoAlt = new System.Windows.Forms.CheckBox();
            this.numScaleH = new System.Windows.Forms.NumericUpDown();
            this.chkUseBC = new System.Windows.Forms.CheckBox();
            this.numBC = new System.Windows.Forms.NumericUpDown();
            this.lblWind = new System.Windows.Forms.Label();
            this.numWind = new System.Windows.Forms.NumericUpDown();
            this.lblH0 = new System.Windows.Forms.Label();
            this.numH0 = new System.Windows.Forms.NumericUpDown();
            this.lblBCType = new System.Windows.Forms.Label();
            this.cboBCType = new System.Windows.Forms.ComboBox();
            this.lblBCG = new System.Windows.Forms.Label();
            this.numBCG = new System.Windows.Forms.NumericUpDown();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.chkDrag = new System.Windows.Forms.CheckBox();
            this.chkUseDt = new System.Windows.Forms.CheckBox();
            this.numDt = new System.Windows.Forms.NumericUpDown();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnHtml = new System.Windows.Forms.Button();
            this.btnCsv = new System.Windows.Forms.Button();
            this.btnGrafik = new System.Windows.Forms.Button();
            this.btnBaslat = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSavePreset = new System.Windows.Forms.Button();
            this.btnLoadPreset = new System.Windows.Forms.Button();
            this.tableInputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numV0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGravity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScaleH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numH0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBCG)).BeginInit();
            this.grpResults.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDt)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(991, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Simülasyon";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSelectedAmmo
            // 
            this.lblSelectedAmmo.AutoEllipsis = true;
            this.lblSelectedAmmo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSelectedAmmo.Location = new System.Drawing.Point(0, 48);
            this.lblSelectedAmmo.Name = "lblSelectedAmmo";
            this.lblSelectedAmmo.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.lblSelectedAmmo.Size = new System.Drawing.Size(991, 28);
            this.lblSelectedAmmo.TabIndex = 1;
            this.lblSelectedAmmo.Text = "Seçilen mühimmat: (yok)";
            // 
            // tableInputs
            // 
            this.tableInputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableInputs.ColumnCount = 2;
            this.tableInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableInputs.Controls.Add(this.lblV0, 0, 0);
            this.tableInputs.Controls.Add(this.lblAngle, 0, 1);
            this.tableInputs.Controls.Add(this.lblMass, 0, 2);
            this.tableInputs.Controls.Add(this.lblGravity, 0, 3);
            this.tableInputs.Controls.Add(this.numV0, 1, 0);
            this.tableInputs.Controls.Add(this.numAngle, 1, 1);
            this.tableInputs.Controls.Add(this.numMass, 1, 2);
            this.tableInputs.Controls.Add(this.numGravity, 1, 3);
            this.tableInputs.Controls.Add(this.lblCd, 0, 4);
            this.tableInputs.Controls.Add(this.numCd, 1, 4);
            this.tableInputs.Controls.Add(this.lblArea, 0, 5);
            this.tableInputs.Controls.Add(this.numArea, 1, 5);
            this.tableInputs.Controls.Add(this.lblRho, 0, 6);
            this.tableInputs.Controls.Add(this.numRho, 1, 6);
            this.tableInputs.Controls.Add(this.chkRhoAlt, 0, 7);
            this.tableInputs.Controls.Add(this.numScaleH, 1, 7);
            this.tableInputs.Controls.Add(this.chkUseBC, 0, 8);
            this.tableInputs.Controls.Add(this.numBC, 1, 8);
            this.tableInputs.Controls.Add(this.lblWind, 0, 9);
            this.tableInputs.Controls.Add(this.numWind, 1, 9);
            this.tableInputs.Controls.Add(this.lblH0, 0, 10);
            this.tableInputs.Controls.Add(this.numH0, 1, 10);
            this.tableInputs.Controls.Add(this.lblBCType, 0, 11);
            this.tableInputs.Controls.Add(this.cboBCType, 1, 11);
            this.tableInputs.Controls.Add(this.lblBCG, 0, 12);
            this.tableInputs.Controls.Add(this.numBCG, 1, 12);
            this.tableInputs.Location = new System.Drawing.Point(12, 88);
            this.tableInputs.Name = "tableInputs";
            this.tableInputs.RowCount = 13;
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692308F));
            this.tableInputs.Size = new System.Drawing.Size(967, 390);
            this.tableInputs.TabIndex = 2;
            // 
            // lblV0
            // 
            this.lblV0.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblV0.AutoSize = true;
            this.lblV0.Location = new System.Drawing.Point(3, 7);
            this.lblV0.Name = "lblV0";
            this.lblV0.Size = new System.Drawing.Size(125, 16);
            this.lblV0.TabIndex = 0;
            this.lblV0.Text = "Başlangıç Hızı (m/s)";
            // 
            // lblAngle
            // 
            this.lblAngle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(3, 37);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(115, 16);
            this.lblAngle.TabIndex = 2;
            this.lblAngle.Text = "Atış Açısı (derece)";
            // 
            // lblMass
            // 
            this.lblMass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMass.AutoSize = true;
            this.lblMass.Location = new System.Drawing.Point(3, 67);
            this.lblMass.Name = "lblMass";
            this.lblMass.Size = new System.Drawing.Size(112, 16);
            this.lblMass.TabIndex = 4;
            this.lblMass.Text = "Mermi Kütlesi (kg)";
            // 
            // lblGravity
            // 
            this.lblGravity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGravity.AutoSize = true;
            this.lblGravity.Location = new System.Drawing.Point(3, 97);
            this.lblGravity.Name = "lblGravity";
            this.lblGravity.Size = new System.Drawing.Size(104, 16);
            this.lblGravity.TabIndex = 6;
            this.lblGravity.Text = "Yerçekimi (m/s²)";
            // 
            // numV0
            // 
            this.numV0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numV0.Location = new System.Drawing.Point(438, 4);
            this.numV0.Name = "numV0";
            this.numV0.Size = new System.Drawing.Size(526, 22);
            this.numV0.TabIndex = 1;
            this.numV0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numAngle
            // 
            this.numAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numAngle.Location = new System.Drawing.Point(438, 34);
            this.numAngle.Name = "numAngle";
            this.numAngle.Size = new System.Drawing.Size(526, 22);
            this.numAngle.TabIndex = 3;
            this.numAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numMass
            // 
            this.numMass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numMass.Location = new System.Drawing.Point(438, 64);
            this.numMass.Name = "numMass";
            this.numMass.Size = new System.Drawing.Size(526, 22);
            this.numMass.TabIndex = 5;
            this.numMass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numGravity
            // 
            this.numGravity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numGravity.Location = new System.Drawing.Point(438, 94);
            this.numGravity.Name = "numGravity";
            this.numGravity.Size = new System.Drawing.Size(526, 22);
            this.numGravity.TabIndex = 7;
            this.numGravity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCd
            // 
            this.lblCd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCd.AutoSize = true;
            this.lblCd.Location = new System.Drawing.Point(3, 127);
            this.lblCd.Name = "lblCd";
            this.lblCd.Size = new System.Drawing.Size(156, 16);
            this.lblCd.TabIndex = 8;
            this.lblCd.Text = "Sürükleme Katsayısı (Cd)";
            // 
            // numCd
            // 
            this.numCd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numCd.DecimalPlaces = 3;
            this.numCd.Location = new System.Drawing.Point(438, 124);
            this.numCd.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numCd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numCd.Name = "numCd";
            this.numCd.Size = new System.Drawing.Size(526, 22);
            this.numCd.TabIndex = 9;
            this.numCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCd.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // lblArea
            // 
            this.lblArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(3, 157);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(144, 16);
            this.lblArea.TabIndex = 10;
            this.lblArea.Text = "Kesit Alanı (m²) (A = πr²)";
            // 
            // numArea
            // 
            this.numArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numArea.DecimalPlaces = 6;
            this.numArea.Increment = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.numArea.Location = new System.Drawing.Point(438, 154);
            this.numArea.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numArea.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.numArea.Name = "numArea";
            this.numArea.Size = new System.Drawing.Size(526, 22);
            this.numArea.TabIndex = 11;
            this.numArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numArea.Value = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            // 
            // lblRho
            // 
            this.lblRho.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRho.AutoSize = true;
            this.lblRho.Location = new System.Drawing.Point(3, 187);
            this.lblRho.Name = "lblRho";
            this.lblRho.Size = new System.Drawing.Size(163, 16);
            this.lblRho.TabIndex = 12;
            this.lblRho.Text = "Hava Yoğunluğu ρ (kg/m³)";
            // 
            // numRho
            // 
            this.numRho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numRho.DecimalPlaces = 3;
            this.numRho.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numRho.Location = new System.Drawing.Point(438, 184);
            this.numRho.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numRho.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numRho.Name = "numRho";
            this.numRho.Size = new System.Drawing.Size(526, 22);
            this.numRho.TabIndex = 13;
            this.numRho.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRho.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // chkRhoAlt
            // 
            this.chkRhoAlt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkRhoAlt.AutoSize = true;
            this.chkRhoAlt.Location = new System.Drawing.Point(3, 215);
            this.chkRhoAlt.Name = "chkRhoAlt";
            this.chkRhoAlt.Size = new System.Drawing.Size(166, 20);
            this.chkRhoAlt.TabIndex = 14;
            this.chkRhoAlt.Text = "ρ irtifaya bağlı (H m)  H:";
            this.chkRhoAlt.UseVisualStyleBackColor = true;
            this.chkRhoAlt.CheckedChanged += new System.EventHandler(this.chkRhoAlt_CheckedChanged);
            // 
            // numScaleH
            // 
            this.numScaleH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numScaleH.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numScaleH.Location = new System.Drawing.Point(438, 214);
            this.numScaleH.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numScaleH.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numScaleH.Name = "numScaleH";
            this.numScaleH.Size = new System.Drawing.Size(526, 22);
            this.numScaleH.TabIndex = 15;
            this.numScaleH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numScaleH.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // chkUseBC
            // 
            this.chkUseBC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkUseBC.AutoSize = true;
            this.chkUseBC.Location = new System.Drawing.Point(3, 245);
            this.chkUseBC.Name = "chkUseBC";
            this.chkUseBC.Size = new System.Drawing.Size(130, 20);
            this.chkUseBC.TabIndex = 16;
            this.chkUseBC.Text = "BC (kg/m²) kullan";
            this.chkUseBC.UseVisualStyleBackColor = true;
            this.chkUseBC.CheckedChanged += new System.EventHandler(this.chkUseBC_CheckedChanged);
            // 
            // numBC
            // 
            this.numBC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numBC.DecimalPlaces = 2;
            this.numBC.Location = new System.Drawing.Point(438, 244);
            this.numBC.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBC.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBC.Name = "numBC";
            this.numBC.Size = new System.Drawing.Size(526, 22);
            this.numBC.TabIndex = 17;
            this.numBC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numBC.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblWind
            // 
            this.lblWind.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblWind.AutoSize = true;
            this.lblWind.Location = new System.Drawing.Point(3, 277);
            this.lblWind.Name = "lblWind";
            this.lblWind.Size = new System.Drawing.Size(189, 16);
            this.lblWind.TabIndex = 18;
            this.lblWind.Text = "Rüzgar Hızı (m/s) (+X yönünde)";
            // 
            // numWind
            // 
            this.numWind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numWind.DecimalPlaces = 2;
            this.numWind.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numWind.Location = new System.Drawing.Point(438, 274);
            this.numWind.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147352576});
            this.numWind.Name = "numWind";
            this.numWind.Size = new System.Drawing.Size(526, 22);
            this.numWind.TabIndex = 19;
            this.numWind.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblH0
            // 
            this.lblH0.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblH0.AutoSize = true;
            this.lblH0.Location = new System.Drawing.Point(3, 307);
            this.lblH0.Name = "lblH0";
            this.lblH0.Size = new System.Drawing.Size(171, 16);
            this.lblH0.TabIndex = 20;
            this.lblH0.Text = "Başlangıç Yüksekliği h0 (m)";
            // 
            // numH0
            // 
            this.numH0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numH0.DecimalPlaces = 2;
            this.numH0.Location = new System.Drawing.Point(438, 304);
            this.numH0.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numH0.Name = "numH0";
            this.numH0.Size = new System.Drawing.Size(526, 22);
            this.numH0.TabIndex = 21;
            this.numH0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblBCType
            // 
            this.lblBCType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBCType.AutoSize = true;
            this.lblBCType.Location = new System.Drawing.Point(3, 337);
            this.lblBCType.Name = "lblBCType";
            this.lblBCType.Size = new System.Drawing.Size(62, 16);
            this.lblBCType.TabIndex = 22;
            this.lblBCType.Text = "BC Modu";
            // 
            // cboBCType
            // 
            this.cboBCType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBCType.FormattingEnabled = true;
            this.cboBCType.Items.AddRange(new object[] {
            "Sabit",
            "G1",
            "G7"});
            this.cboBCType.Location = new System.Drawing.Point(438, 333);
            this.cboBCType.Name = "cboBCType";
            this.cboBCType.Size = new System.Drawing.Size(526, 24);
            this.cboBCType.TabIndex = 23;
            this.cboBCType.SelectedIndexChanged += new System.EventHandler(this.cboBCType_SelectedIndexChanged);
            // 
            // lblBCG
            // 
            this.lblBCG.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBCG.AutoSize = true;
            this.lblBCG.Location = new System.Drawing.Point(3, 367);
            this.lblBCG.Name = "lblBCG";
            this.lblBCG.Size = new System.Drawing.Size(88, 16);
            this.lblBCG.TabIndex = 24;
            this.lblBCG.Text = "BC (G-model)";
            // 
            // numBCG
            // 
            this.numBCG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numBCG.DecimalPlaces = 3;
            this.numBCG.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numBCG.Location = new System.Drawing.Point(438, 364);
            this.numBCG.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numBCG.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numBCG.Name = "numBCG";
            this.numBCG.Size = new System.Drawing.Size(526, 22);
            this.numBCG.TabIndex = 25;
            this.numBCG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numBCG.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // grpResults
            // 
            this.grpResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpResults.Controls.Add(this.txtResults);
            this.grpResults.Location = new System.Drawing.Point(12, 484);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(967, 126);
            this.grpResults.TabIndex = 4;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Sonuçlar";
            // 
            // txtResults
            // 
            this.txtResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults.Location = new System.Drawing.Point(3, 18);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(961, 105);
            this.txtResults.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.chkDrag);
            this.bottomPanel.Controls.Add(this.chkUseDt);
            this.bottomPanel.Controls.Add(this.numDt);
            this.bottomPanel.Controls.Add(this.btnIptal);
            this.bottomPanel.Controls.Add(this.btnHtml);
            this.bottomPanel.Controls.Add(this.btnCsv);
            this.bottomPanel.Controls.Add(this.btnGrafik);
            this.bottomPanel.Controls.Add(this.btnBaslat);
            this.bottomPanel.Controls.Add(this.btnPrint);
            this.bottomPanel.Controls.Add(this.btnSavePreset);
            this.bottomPanel.Controls.Add(this.btnLoadPreset);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 616);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.bottomPanel.Size = new System.Drawing.Size(991, 60);
            this.bottomPanel.TabIndex = 3;
            // 
            // chkDrag
            // 
            this.chkDrag.AutoSize = true;
            this.chkDrag.Location = new System.Drawing.Point(12, 31);
            this.chkDrag.Name = "chkDrag";
            this.chkDrag.Size = new System.Drawing.Size(226, 20);
            this.chkDrag.TabIndex = 0;
            this.chkDrag.Text = "Hava direncini kullan (Cd,A,ρ/BC)";
            this.chkDrag.UseVisualStyleBackColor = true;
            this.chkDrag.CheckedChanged += new System.EventHandler(this.chkDrag_CheckedChanged);
            // 
            // chkUseDt
            // 
            this.chkUseDt.AutoSize = true;
            this.chkUseDt.Location = new System.Drawing.Point(12, 5);
            this.chkUseDt.Name = "chkUseDt";
            this.chkUseDt.Size = new System.Drawing.Size(164, 20);
            this.chkUseDt.TabIndex = 1;
            this.chkUseDt.Text = "Zaman Adımı (s) kullan";
            this.chkUseDt.UseVisualStyleBackColor = true;
            this.chkUseDt.CheckedChanged += new System.EventHandler(this.chkUseDt_CheckedChanged);
            // 
            // numDt
            // 
            this.numDt.Location = new System.Drawing.Point(193, 4);
            this.numDt.Name = "numDt";
            this.numDt.Size = new System.Drawing.Size(80, 22);
            this.numDt.TabIndex = 2;
            this.numDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnIptal
            // 
            this.btnIptal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIptal.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIptal.Location = new System.Drawing.Point(503, 9);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(60, 25);
            this.btnIptal.TabIndex = 5;
            this.btnIptal.Text = "İptal";
            this.btnIptal.UseVisualStyleBackColor = true;
            // 
            // btnHtml
            // 
            this.btnHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHtml.Location = new System.Drawing.Point(645, 9);
            this.btnHtml.Name = "btnHtml";
            this.btnHtml.Size = new System.Drawing.Size(70, 25);
            this.btnHtml.TabIndex = 6;
            this.btnHtml.Text = "HTML Rapor";
            this.btnHtml.UseVisualStyleBackColor = true;
            this.btnHtml.Click += new System.EventHandler(this.btnHtml_Click);
            // 
            // btnCsv
            // 
            this.btnCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCsv.Location = new System.Drawing.Point(797, 9);
            this.btnCsv.Name = "btnCsv";
            this.btnCsv.Size = new System.Drawing.Size(54, 25);
            this.btnCsv.TabIndex = 7;
            this.btnCsv.Text = "CSV";
            this.btnCsv.UseVisualStyleBackColor = true;
            this.btnCsv.Click += new System.EventHandler(this.btnCsv_Click);
            // 
            // btnGrafik
            // 
            this.btnGrafik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrafik.Location = new System.Drawing.Point(857, 9);
            this.btnGrafik.Name = "btnGrafik";
            this.btnGrafik.Size = new System.Drawing.Size(82, 25);
            this.btnGrafik.TabIndex = 8;
            this.btnGrafik.Text = "Grafik";
            this.btnGrafik.UseVisualStyleBackColor = true;
            this.btnGrafik.Click += new System.EventHandler(this.btnGrafik_Click);
            // 
            // btnBaslat
            // 
            this.btnBaslat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBaslat.Location = new System.Drawing.Point(295, 9);
            this.btnBaslat.Name = "btnBaslat";
            this.btnBaslat.Size = new System.Drawing.Size(81, 25);
            this.btnBaslat.TabIndex = 4;
            this.btnBaslat.Text = "Hesapla";
            this.btnBaslat.UseVisualStyleBackColor = true;
            this.btnBaslat.Click += new System.EventHandler(this.btnBaslat_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(721, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(70, 25);
            this.btnPrint.TabIndex = 11;
            this.btnPrint.Text = "Yazdır/PDF";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSavePreset.Location = new System.Drawing.Point(569, 9);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(70, 25);
            this.btnSavePreset.TabIndex = 9;
            this.btnSavePreset.Text = "Ön Ayar Kaydet";
            this.btnSavePreset.UseVisualStyleBackColor = true;
            this.btnSavePreset.Click += new System.EventHandler(this.btnSavePreset_Click);
            // 
            // btnLoadPreset
            // 
            this.btnLoadPreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadPreset.Location = new System.Drawing.Point(392, 9);
            this.btnLoadPreset.Name = "btnLoadPreset";
            this.btnLoadPreset.Size = new System.Drawing.Size(95, 25);
            this.btnLoadPreset.TabIndex = 10;
            this.btnLoadPreset.Text = "Ön Ayar Yükle";
            this.btnLoadPreset.UseVisualStyleBackColor = true;
            this.btnLoadPreset.Click += new System.EventHandler(this.btnLoadPreset_Click);
            // 
            // SimulationForm
            // 
            this.AcceptButton = this.btnBaslat;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnIptal;
            this.ClientSize = new System.Drawing.Size(991, 676);
            this.Controls.Add(this.grpResults);
            this.Controls.Add(this.tableInputs);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.lblSelectedAmmo);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(640, 600);
            this.Name = "SimulationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Simülasyon";
            this.tableInputs.ResumeLayout(false);
            this.tableInputs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numV0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGravity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScaleH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numH0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBCG)).EndInit();
            this.grpResults.ResumeLayout(false);
            this.grpResults.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSelectedAmmo;
        private System.Windows.Forms.TableLayoutPanel tableInputs;
        private System.Windows.Forms.Label lblV0;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.Label lblMass;
        private System.Windows.Forms.Label lblGravity;
        private System.Windows.Forms.NumericUpDown numV0;
        private System.Windows.Forms.NumericUpDown numAngle;
        private System.Windows.Forms.NumericUpDown numMass;
        private System.Windows.Forms.NumericUpDown numGravity;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.CheckBox chkUseDt;
        private System.Windows.Forms.NumericUpDown numDt;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Label lblCd;
        private System.Windows.Forms.NumericUpDown numCd;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.NumericUpDown numArea;
        private System.Windows.Forms.Label lblRho;
        private System.Windows.Forms.NumericUpDown numRho;
        private System.Windows.Forms.CheckBox chkDrag;
        private System.Windows.Forms.CheckBox chkRhoAlt;
        private System.Windows.Forms.NumericUpDown numScaleH;
        private System.Windows.Forms.CheckBox chkUseBC;
        private System.Windows.Forms.NumericUpDown numBC;
        private System.Windows.Forms.Label lblH0;
        private System.Windows.Forms.NumericUpDown numH0;
        private System.Windows.Forms.Label lblWind;
        private System.Windows.Forms.NumericUpDown numWind;
        private System.Windows.Forms.Label lblBCType;
        private System.Windows.Forms.ComboBox cboBCType;
        private System.Windows.Forms.Label lblBCG;
        private System.Windows.Forms.NumericUpDown numBCG;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnHtml;
        private System.Windows.Forms.Button btnCsv;
        private System.Windows.Forms.Button btnGrafik;
        private System.Windows.Forms.Button btnBaslat;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSavePreset;
        private System.Windows.Forms.Button btnLoadPreset;
    }
}