using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Serialization;
using System.Drawing.Printing;

namespace BalistikCalisma
{
    public partial class SimulationForm : Form
    {
        private readonly string _selectedAmmo;
        private readonly List<PointF> _trajectoryVacuum = new List<PointF>();
        private readonly List<PointF> _trajectoryDrag = new List<PointF>();

        public SimulationForm(string selectedAmmo)
        {
            InitializeComponent();

            _selectedAmmo = selectedAmmo;
            lblSelectedAmmo.Text = string.IsNullOrWhiteSpace(_selectedAmmo)
                ? "Seçilen mühimmat: (yok)"
                : "Seçilen mühimmat: " + _selectedAmmo;

            // Başlangıç parametreleri
            numV0.DecimalPlaces = 2; numV0.Minimum = 0m; numV0.Maximum = 2000m; numV0.Increment = 0.10m; numV0.Value = 820m;
            numAngle.DecimalPlaces = 2; numAngle.Minimum = 0m; numAngle.Maximum = 90m; numAngle.Increment = 0.10m; numAngle.Value = 45m;
            numMass.DecimalPlaces = 4; numMass.Minimum = 0.0010m; numMass.Maximum = 100.0000m; numMass.Increment = 0.0010m; numMass.Value = 0.0090m;
            numGravity.DecimalPlaces = 3; numGravity.Minimum = 0.100m; numGravity.Maximum = 50.000m; numGravity.Increment = 0.010m; numGravity.Value = 9.810m;

            // Cd-A-ρ
            numCd.DecimalPlaces = 3; numCd.Minimum = 0.005m; numCd.Maximum = 2.000m; numCd.Increment = 0.005m; numCd.Value = 0.295m;
            numArea.DecimalPlaces = 6; numArea.Minimum = 0.000001m; numArea.Maximum = 0.010000m; numArea.Increment = 0.000001m; numArea.Value = 0.000063m;
            numRho.DecimalPlaces = 3; numRho.Minimum = 0.100m; numRho.Maximum = 2.000m; numRho.Increment = 0.005m; numRho.Value = 1.225m;
            numScaleH.Minimum = 1000m; numScaleH.Maximum = 20000m; numScaleH.Increment = 100m; numScaleH.Value = 8500m; chkRhoAlt.Checked = false; numScaleH.Enabled = chkRhoAlt.Checked;

            // BC sabit (kg/m²)
            chkUseBC.Checked = false;
            numBC.DecimalPlaces = 2; numBC.Minimum = 1m; numBC.Maximum = 1000m; numBC.Increment = 1m; numBC.Value = 50m;

            // BC modu ve G-model BC
            cboBCType.SelectedIndex = 0; // Sabit
            numBCG.DecimalPlaces = 3; numBCG.Minimum = 0.050m; numBCG.Maximum = 2.000m; numBCG.Increment = 0.005m; numBCG.Value = 0.500m;

            // Rüzgar ve h0
            numWind.DecimalPlaces = 2; numWind.Minimum = -100m; numWind.Maximum = 100m; numWind.Increment = 0.10m; numWind.Value = 0m;
            numH0.DecimalPlaces = 2; numH0.Minimum = 0m; numH0.Maximum = 10000m; numH0.Value = 0m;

            // Zaman adımı
            numDt.DecimalPlaces = 3; numDt.Minimum = 0.001m; numDt.Maximum = 10.000m; numDt.Increment = 0.001m; numDt.Value = 0.100m;
            chkUseDt.Checked = false; numDt.Enabled = chkUseDt.Checked;

            chkDrag.Checked = false;
            ToggleDragInputs(chkDrag.Checked);
            UpdateBCModeUI();
        }

        // Dışarıdan okunabilir
        public decimal BaslangicHizi => numV0.Value;
        public decimal AtisAcisi => numAngle.Value;
        public decimal MermiKutlesi => numMass.Value;
        public decimal Yercekimi => numGravity.Value;
        public decimal? ZamanAdimi => chkUseDt.Checked ? (decimal?)numDt.Value : null;

        public IReadOnlyList<PointF> TrajectoryVacuum => _trajectoryVacuum;
        public IReadOnlyList<PointF> TrajectoryDrag => _trajectoryDrag;

        // Seçilen mühimmat için varsayılan atama (Form1 kullanıyor)
        public void ApplyAmmoDefaults(double? v0 = null, double? massKg = null, double? bcG1 = null)
        {
            if (v0.HasValue) SetNumeric(numV0, (decimal)v0.Value);
            if (massKg.HasValue) SetNumeric(numMass, (decimal)massKg.Value);

            if (bcG1.HasValue)
            {
                chkDrag.Checked = true;
                chkUseBC.Checked = true;
                cboBCType.SelectedItem = "G1";
                SetNumeric(numBCG, (decimal)bcG1.Value);
            }
        }

        private static void SetNumeric(NumericUpDown ctrl, decimal value)
        {
            if (value < ctrl.Minimum) value = ctrl.Minimum;
            else if (value > ctrl.Maximum) value = ctrl.Maximum;
            ctrl.Value = value;
        }

        private void chkUseDt_CheckedChanged(object sender, EventArgs e) => numDt.Enabled = chkUseDt.Checked;

        private void chkDrag_CheckedChanged(object sender, EventArgs e)
        {
            ToggleDragInputs(chkDrag.Checked);
            UpdateBCModeUI();
        }

        private void chkRhoAlt_CheckedChanged(object sender, EventArgs e)
        {
            numScaleH.Enabled = chkRhoAlt.Checked && chkDrag.Checked && cboBCType.SelectedItem as string == "Sabit";
        }

        private void chkUseBC_CheckedChanged(object sender, EventArgs e) => UpdateBCModeUI();

        private void cboBCType_SelectedIndexChanged(object sender, EventArgs e) => UpdateBCModeUI();

        private void ToggleDragInputs(bool enabled)
        {
            lblCd.Enabled = enabled && (cboBCType.SelectedItem as string == "Sabit");
            numCd.Enabled = enabled && (cboBCType.SelectedItem as string == "Sabit");
            lblArea.Enabled = enabled && (cboBCType.SelectedItem as string == "Sabit");
            numArea.Enabled = enabled && (cboBCType.SelectedItem as string == "Sabit");

            lblRho.Enabled = enabled;
            numRho.Enabled = enabled;

            chkRhoAlt.Enabled = enabled && (cboBCType.SelectedItem as string == "Sabit");
            numScaleH.Enabled = enabled && (cboBCType.SelectedItem as string == "Sabit") && chkRhoAlt.Checked;

            chkUseBC.Enabled = enabled;
            numBC.Enabled = enabled && chkUseBC.Checked && (cboBCType.SelectedItem as string == "Sabit");

            lblBCType.Enabled = enabled && chkUseBC.Checked;
            cboBCType.Enabled = enabled && chkUseBC.Checked;

            lblBCG.Enabled = enabled && chkUseBC.Checked && (cboBCType.SelectedItem as string != "Sabit");
            numBCG.Enabled = enabled && chkUseBC.Checked && (cboBCType.SelectedItem as string != "Sabit");

            lblWind.Enabled = enabled;
            numWind.Enabled = enabled;
        }

        private void UpdateBCModeUI()
        {
            var bcMode = cboBCType.SelectedItem as string ?? "Sabit";
            bool drag = chkDrag.Checked;

            numBC.Enabled = drag && chkUseBC.Checked && bcMode == "Sabit";
            lblBCG.Enabled = numBCG.Enabled = drag && chkUseBC.Checked && bcMode != "Sabit";

            lblCd.Enabled = numCd.Enabled = drag && bcMode == "Sabit";
            lblArea.Enabled = numArea.Enabled = drag && bcMode == "Sabit";
            chkRhoAlt.Enabled = drag && bcMode == "Sabit";
            numScaleH.Enabled = drag && bcMode == "Sabit" && chkRhoAlt.Checked;

            lblRho.Enabled = numRho.Enabled = drag;
            lblBCType.Enabled = cboBCType.Enabled = drag && chkUseBC.Checked;
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var dt = chkUseDt.Checked ? (double)numDt.Value : 0.01;

            var vacuum = RunSimulation(withDrag: false, dt, out var vacPoints);

            SimulationResult drag = null;
            List<PointF> dragPoints = null;
            if (chkDrag.Checked)
            {
                drag = RunSimulation(withDrag: true, dt, out dragPoints);
            }

            _trajectoryVacuum.Clear(); _trajectoryVacuum.AddRange(vacPoints);
            _trajectoryDrag.Clear(); if (dragPoints != null) _trajectoryDrag.AddRange(dragPoints);

            var sb = new StringBuilder();
            sb.AppendLine("Vakum (Hava Direnci Yok)");
            sb.AppendLine("-------------------------");
            sb.AppendLine($"Uçuş Süresi (s): {vacuum.TotalTime:F3}");
            sb.AppendLine($"Menzil (m): {vacuum.RangeX:F3}");
            sb.AppendLine($"Maks. Yükseklik (m): {vacuum.MaxY:F3}");
            sb.AppendLine($"Başlangıç Enerjisi (J): {vacuum.InitialKE:F2}");
            sb.AppendLine($"Etki Anı Enerjisi (J): {vacuum.ImpactKE:F2}");

            if (drag != null)
            {
                var mode = (cboBCType.SelectedItem as string == "Sabit")
                    ? (chkUseBC.Checked ? "BC (sabit)" : "Cd,A,ρ")
                    : ("BC (" + (cboBCType.SelectedItem as string) + ")");
                sb.AppendLine();
                sb.AppendLine("Hava Dirençli - " + mode);
                sb.AppendLine("--------------------------------");
                sb.AppendLine($"Uçuş Süresi (s): {drag.TotalTime:F3}");
                sb.AppendLine($"Menzil (m): {drag.RangeX:F3}");
                sb.AppendLine($"Maks. Yükseklik (m): {drag.MaxY:F3}");
                sb.AppendLine($"Başlangıç Enerjisi (J): {drag.InitialKE:F2}");
                sb.AppendLine($"Etki Anı Enerjisi (J): {drag.ImpactKE:F2}");
            }

            txtResults.Text = sb.ToString();
        }

        private void btnGrafik_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            var dt = chkUseDt.Checked ? (double)numDt.Value : 0.01;

            var _ = RunSimulation(withDrag: false, dt, out var vacPoints);
            List<PointF> dragPoints = null;
            if (chkDrag.Checked) _ = RunSimulation(withDrag: true, dt, out dragPoints);

            string title = string.IsNullOrWhiteSpace(_selectedAmmo) ? "Trajektori" : _selectedAmmo + " - Trajektori";
            using (var chartForm = new TrajectoryChartForm(vacPoints, dragPoints, title))
            {
                chartForm.ShowDialog(this);
            }
        }

        private void btnCsv_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var dt = chkUseDt.Checked ? (double)numDt.Value : 0.01;
            var vacSamples = RunSimulationDetailed(withDrag: false, dt);
            List<TrajSample> dragSamples = null;
            if (chkDrag.Checked) dragSamples = RunSimulationDetailed(withDrag: true, dt);

            using (var sfd = new SaveFileDialog
            {
                Title = "CSV Dışa Aktar",
                Filter = "CSV Dosyası (*.csv)|*.csv",
                FileName = "trajektori.csv",
                AddExtension = true,
                DefaultExt = "csv"
            })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                var sep = ",";
                using (var sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                {
                    sw.WriteLine("scenario,t(s),x(m),y(m),vx(m/s),vy(m/s)");
                    foreach (var s in vacSamples)
                        sw.WriteLine(string.Join(sep, "vacuum",
                            s.T.ToString("F6", CultureInfo.InvariantCulture),
                            s.X.ToString("F6", CultureInfo.InvariantCulture),
                            s.Y.ToString("F6", CultureInfo.InvariantCulture),
                            s.Vx.ToString("F6", CultureInfo.InvariantCulture),
                            s.Vy.ToString("F6", CultureInfo.InvariantCulture)));

                    if (dragSamples != null)
                    {
                        foreach (var s in dragSamples)
                            sw.WriteLine(string.Join(sep, "drag",
                                s.T.ToString("F6", CultureInfo.InvariantCulture),
                                s.X.ToString("F6", CultureInfo.InvariantCulture),
                                s.Y.ToString("F6", CultureInfo.InvariantCulture),
                                s.Vx.ToString("F6", CultureInfo.InvariantCulture),
                                s.Vy.ToString("F6", CultureInfo.InvariantCulture)));
                    }
                }

                MessageBox.Show("CSV dışa aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHtml_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var dt = chkUseDt.Checked ? (double)numDt.Value : 0.01;
            var vacRes = RunSimulation(withDrag: false, dt, out var vacPoints);
            SimulationResult dragRes = null; List<PointF> dragPoints = null;
            if (chkDrag.Checked) dragRes = RunSimulation(withDrag: true, dt, out dragPoints);

            using (var sfd = new SaveFileDialog
            {
                Title = "HTML Rapor Kaydet",
                Filter = "HTML Dosyası (*.html)|*.html",
                FileName = "rapor.html",
                AddExtension = true,
                DefaultExt = "html"
            })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                string imgPath = Path.Combine(Path.GetDirectoryName(sfd.FileName) ?? "", Path.GetFileNameWithoutExtension(sfd.FileName) + ".png");
                SaveChartImage(vacPoints, dragPoints, imgPath);

                var now = DateTime.Now;

                var sb = new StringBuilder();
                sb.AppendLine("<!doctype html><html lang=\"tr\"><head><meta charset=\"utf-8\">");
                sb.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"/>");
                sb.AppendLine("<title>Balistik Rapor</title>");
                sb.AppendLine("<style>");
                sb.AppendLine(":root{--bg:#0b132b;--pri:#1c7ed6;--pri2:#228be6;--mut:#adb5bd;--card:#ffffff;--tbl:#f8f9fa}");
                sb.AppendLine("body{margin:0;font-family:'Segoe UI',Arial,sans-serif;background:#f5f7fb;color:#222}");
                sb.AppendLine(".appbar{background:var(--bg);color:#fff;padding:16px 24px;font-size:20px;font-weight:700}");
                sb.AppendLine(".container{max-width:1100px;margin:24px auto;padding:0 16px}");
                sb.AppendLine(".meta{color:#666;font-size:12px;margin-top:4px}");
                sb.AppendLine(".section{margin-top:20px}");
                sb.AppendLine(".grid{display:grid;grid-template-columns:repeat(auto-fit,minmax(240px,1fr));gap:12px}");
                sb.AppendLine(".card{background:var(--card);border:1px solid #e9ecef;border-radius:8px;padding:14px;box-shadow:0 1px 3px rgba(0,0,0,.04)}");
                sb.AppendLine(".card h3{margin:0 0 8px;font-size:14px;color:#495057}");
                sb.AppendLine(".card .val{font-size:20px;font-weight:700;color:var(--bg)}");
                sb.AppendLine("table{width:100%;border-collapse:collapse;background:var(--card);border:1px solid #e9ecef;border-radius:8px;overflow:hidden}");
                sb.AppendLine("th,td{padding:8px 10px;border-bottom:1px solid #eee;font-size:13px}");
                sb.AppendLine("th{background:var(--tbl);text-align:left;color:#495057}");
                sb.AppendLine("tr:last-child td{border-bottom:none}");
                sb.AppendLine(".imgwrap{background:#fff;border:1px solid #e9ecef;border-radius:8px;padding:10px}");
                sb.AppendLine(".subtitle{color:#364fc7;font-weight:700;margin:0 0 8px;font-size:16px}");
                sb.AppendLine("</style></head><body>");
                sb.AppendLine($"<div class=\"appbar\">Balistik Rapor</div>");
                sb.AppendLine("<div class=\"container\">");

                // Başlık + meta
                sb.AppendLine("<div class=\"section\">");
                sb.AppendLine($"<div class=\"subtitle\">Seçilen Mühimmat</div>");
                sb.AppendLine("<div class=\"card\"><div style=\"display:flex;justify-content:space-between;align-items:center\">");
                sb.AppendLine($"<div style=\"font-weight:700\">{System.Net.WebUtility.HtmlEncode(_selectedAmmo ?? "(yok)")}</div>");
                sb.AppendLine($"<div class=\"meta\">Oluşturulma: {now:dd.MM.yyyy HH:mm}</div>");
                sb.AppendLine("</div></div>");
                sb.AppendLine("</div>");

                // Parametreler
                sb.AppendLine("<div class=\"section\">");
                sb.AppendLine("<div class=\"subtitle\">Giriş Parametreleri</div>");
                sb.AppendLine("<table><thead><tr><th>Parametre</th><th>Değer</th></tr></thead><tbody>");
                sb.AppendLine($"<tr><td>v0 (m/s)</td><td>{numV0.Value}</td></tr>");
                sb.AppendLine($"<tr><td>Açı (°)</td><td>{numAngle.Value}</td></tr>");
                sb.AppendLine($"<tr><td>m (kg)</td><td>{numMass.Value}</td></tr>");
                sb.AppendLine($"<tr><td>g (m/s²)</td><td>{numGravity.Value}</td></tr>");
                sb.AppendLine($"<tr><td>h0 (m)</td><td>{numH0.Value}</td></tr>");
                sb.AppendLine($"<tr><td>Rüzgar (m/s)</td><td>{numWind.Value}</td></tr>");

                if (chkDrag.Checked)
                {
                    var bcMode = cboBCType.SelectedItem as string ?? "Sabit";
                    if (chkUseBC.Checked)
                    {
                        if (bcMode == "Sabit")
                            sb.AppendLine($"<tr><td>BC (kg/m²)</td><td>{numBC.Value}</td></tr>");
                        else
                            sb.AppendLine($"<tr><td>BC ({bcMode})</td><td>{numBCG.Value}</td></tr>");
                    }
                    else
                    {
                        sb.AppendLine($"<tr><td>Cd</td><td>{numCd.Value}</td></tr>");
                        sb.AppendLine($"<tr><td>A (m²)</td><td>{numArea.Value}</td></tr>");
                    }
                    sb.AppendLine($"<tr><td>ρ0 (kg/m³)</td><td>{numRho.Value}</td></tr>");
                    var rhoDesc = (bcMode == "Sabit" && chkRhoAlt.Checked) ? ("exp(-h/H), H=" + numScaleH.Value + " m") : "ölçek: ρ/ρ0";
                    sb.AppendLine($"<tr><td>ρ(h)</td><td>{rhoDesc}</td></tr>");
                }
                sb.AppendLine("</tbody></table>");
                sb.AppendLine("</div>");

                // Özet metrikler
                sb.AppendLine("<div class=\"section\">");
                sb.AppendLine("<div class=\"subtitle\">Özet Sonuçlar</div>");
                sb.AppendLine("<div class=\"grid\">");
                sb.AppendLine($"<div class=\"card\"><h3>Vakum Uçuş Süresi</h3><div class=\"val\">{vacRes.TotalTime:F3} s</div></div>");
                sb.AppendLine($"<div class=\"card\"><h3>Vakum Menzil</h3><div class=\"val\">{vacRes.RangeX:F3} m</div></div>");
                sb.AppendLine($"<div class=\"card\"><h3>Vakum Maks. Yükseklik</h3><div class=\"val\">{vacRes.MaxY:F3} m</div></div>");
                sb.AppendLine($"<div class=\"card\"><h3>Vakum Etki Enerjisi</h3><div class=\"val\">{vacRes.ImpactKE:F2} J</div></div>");
                if (dragRes != null)
                {
                    var mode = (cboBCType.SelectedItem as string == "Sabit")
                        ? (chkUseBC.Checked ? "BC (sabit)" : "Cd,A,ρ")
                        : ("BC " + (cboBCType.SelectedItem as string));
                    sb.AppendLine($"<div class=\"card\"><h3>Dirençli Uçuş Süresi</h3><div class=\"val\">{dragRes.TotalTime:F3} s</div></div>");
                    sb.AppendLine($"<div class=\"card\"><h3>Dirençli Menzil</h3><div class=\"val\">{dragRes.RangeX:F3} m</div></div>");
                    sb.AppendLine($"<div class=\"card\"><h3>Dirençli Maks. Yükseklik</h3><div class=\"val\">{dragRes.MaxY:F3} m</div></div>");
                    sb.AppendLine($"<div class=\"card\"><h3>Dirençli Etki Enerjisi</h3><div class=\"val\">{dragRes.ImpactKE:F2} J</div></div>");
                    sb.AppendLine($"<div class=\"card\"><h3>Model</h3><div class=\"val\">{mode}</div></div>");
                }
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");

                // Grafik
                sb.AppendLine("<div class=\"section\">");
                sb.AppendLine("<div class=\"subtitle\">Trajektori Grafiği</div>");
                sb.AppendLine("<div class=\"imgwrap\">");
                sb.AppendLine($"<img src=\"{System.Net.WebUtility.HtmlEncode(Path.GetFileName(imgPath))}\" style=\"max-width:100%;height:auto;display:block;margin:auto\"/>");
                sb.AppendLine("</div></div>");

                sb.AppendLine("</div></body></html>");

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("HTML rapor oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSavePreset_Click(object sender, EventArgs e)
        {
            try
            {
                var preset = CollectPreset();
                var path = GetPresetPath();
                Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");
                using (var fs = File.Create(path))
                {
                    var xs = new XmlSerializer(typeof(SimPreset));
                    xs.Serialize(fs, preset);
                }
                MessageBox.Show("Ön ayar kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ön ayar kaydedilemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadPreset_Click(object sender, EventArgs e)
        {
            try
            {
                var path = GetPresetPath();
                if (!File.Exists(path))
                {
                    MessageBox.Show("Kayıtlı ön ayar bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                SimPreset p;
                using (var fs = File.OpenRead(path))
                {
                    var xs = new XmlSerializer(typeof(SimPreset));
                    p = (SimPreset)xs.Deserialize(fs);
                }
                ApplyPreset(p);
                MessageBox.Show("Ön ayar yüklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ön ayar yüklenemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // PRESet: eksik olan üyeler (EKLENDİ)
        [Serializable]
        public class SimPreset
        {
            public string SelectedAmmo { get; set; }
            public double V0 { get; set; }
            public double AngleDeg { get; set; }
            public double MassKg { get; set; }
            public double Gravity { get; set; }
            public double H0 { get; set; }
            public double WindX { get; set; }
            public bool UseDt { get; set; }
            public double Dt { get; set; }

            public bool DragEnabled { get; set; }
            public bool UseBC { get; set; }
            public string BCType { get; set; } // "Sabit", "G1", "G7"
            public double BC_metric { get; set; } // kg/m²
            public double BC_g { get; set; }      // G1/G7 BC
            public double Cd { get; set; }
            public double Area { get; set; }
            public double Rho0 { get; set; }
            public bool RhoAlt { get; set; }
            public double ScaleH { get; set; }
        }

        private SimPreset CollectPreset()
        {
            return new SimPreset
            {
                SelectedAmmo = _selectedAmmo,
                V0 = (double)numV0.Value,
                AngleDeg = (double)numAngle.Value,
                MassKg = (double)numMass.Value,
                Gravity = (double)numGravity.Value,
                H0 = (double)numH0.Value,
                WindX = (double)numWind.Value,
                UseDt = chkUseDt.Checked,
                Dt = (double)numDt.Value,
                DragEnabled = chkDrag.Checked,
                UseBC = chkUseBC.Checked,
                BCType = cboBCType.SelectedItem as string ?? "Sabit",
                BC_metric = (double)numBC.Value,
                BC_g = (double)numBCG.Value,
                Cd = (double)numCd.Value,
                Area = (double)numArea.Value,
                Rho0 = (double)numRho.Value,
                RhoAlt = chkRhoAlt.Checked,
                ScaleH = (double)numScaleH.Value
            };
        }

        private void ApplyPreset(SimPreset p)
        {
            if (p == null) return;

            SetNumeric(numV0, (decimal)p.V0);
            SetNumeric(numAngle, (decimal)p.AngleDeg);
            SetNumeric(numMass, (decimal)p.MassKg);
            SetNumeric(numGravity, (decimal)p.Gravity);
            SetNumeric(numH0, (decimal)p.H0);
            SetNumeric(numWind, (decimal)p.WindX);

            chkUseDt.Checked = p.UseDt;
            if (p.UseDt) SetNumeric(numDt, (decimal)p.Dt);

            chkDrag.Checked = p.DragEnabled;
            chkUseBC.Checked = p.UseBC;
            cboBCType.SelectedItem = p.BCType ?? "Sabit";

            SetNumeric(numBC, (decimal)p.BC_metric);
            SetNumeric(numBCG, (decimal)p.BC_g);
            SetNumeric(numCd, (decimal)p.Cd);
            SetNumeric(numArea, (decimal)p.Area);
            SetNumeric(numRho, (decimal)p.Rho0);
            chkRhoAlt.Checked = p.RhoAlt;
            if (p.RhoAlt) SetNumeric(numScaleH, (decimal)p.ScaleH);

            UpdateBCModeUI();
            ToggleDragInputs(chkDrag.Checked);
        }

        private static string GetPresetPath()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BalistikCalisma");
            return Path.Combine(dir, "preset.xml");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            var dt = chkUseDt.Checked ? (double)numDt.Value : 0.01;
            var vacRes = RunSimulation(withDrag: false, dt, out var vacPoints);
            SimulationResult dragRes = null; List<PointF> dragPoints = null;
            if (chkDrag.Checked) dragRes = RunSimulation(withDrag: true, dt, out dragPoints);

            string tmpImg = Path.Combine(Path.GetTempPath(), "trajektori_print.png");
            SaveChartImage(vacPoints, dragPoints, tmpImg);

            var doc = new PrintDocument { DocumentName = "Balistik Raporu" };
            doc.DefaultPageSettings.Landscape = true;
            doc.PrintPage += (s, ev) =>
            {
                var g = ev.Graphics;
                var margin = ev.MarginBounds;
                float y = margin.Top;

                using (var titleFont = new Font("Segoe UI", 14, FontStyle.Bold))
                using (var normal = new Font("Segoe UI", 9))
                using (var bold = new Font("Segoe UI", 9, FontStyle.Bold))
                {
                    g.DrawString("Balistik Rapor", titleFont, Brushes.Black, margin.Left, y);
                    y += titleFont.GetHeight(g) + 6;
                    g.DrawString("Mühimmat: " + (_selectedAmmo ?? "(yok)"), bold, Brushes.Black, margin.Left, y);
                    y += normal.GetHeight(g) + 4;

                    string parms =
                        $"v0: {numV0.Value} m/s   Açı: {numAngle.Value}°   m: {numMass.Value} kg   g: {numGravity.Value} m/s²\r\n" +
                        $"h0: {numH0.Value} m   Rüzgar: {numWind.Value} m/s   dt: {(chkUseDt.Checked ? numDt.Value + " s" : "(otomatik)")}";
                    g.DrawString(parms, normal, Brushes.Black, margin.Left, y);
                    y += normal.GetHeight(g) * 2;

                    string sumVac = $"[Vakum]  T: {vacRes.TotalTime:F3} s  R: {vacRes.RangeX:F3} m  Hmax: {vacRes.MaxY:F3} m  Eimpact: {vacRes.ImpactKE:F2} J";
                    g.DrawString(sumVac, normal, Brushes.Black, margin.Left, y);
                    y += normal.GetHeight(g) + 2;

                    if (dragRes != null)
                    {
                        var mode = (cboBCType.SelectedItem as string == "Sabit")
                            ? (chkUseBC.Checked ? "BC(sabit)" : "Cd,A,ρ")
                            : ("BC " + (cboBCType.SelectedItem as string));
                        string sumDrag = $"[Dirençli-{mode}]  T: {dragRes.TotalTime:F3} s  R: {dragRes.RangeX:F3} m  Hmax: {dragRes.MaxY:F3} m  Eimpact: {dragRes.ImpactKE:F2} J";
                        g.DrawString(sumDrag, normal, Brushes.Black, margin.Left, y);
                        y += normal.GetHeight(g) + 6;
                    }

                    if (File.Exists(tmpImg))
                    {
                        using (var img = Image.FromFile(tmpImg))
                        {
                            var maxW = margin.Width;
                            var maxH = margin.Height - (int)(y - margin.Top);
                            var scale = Math.Min((float)maxW / img.Width, (float)maxH / img.Height);
                            var w = img.Width * scale;
                            var h = img.Height * scale;
                            var rect = new RectangleF(margin.Left, y, w, h);
                            g.DrawImage(img, rect);
                        }
                    }
                }

                ev.HasMorePages = false;
            };

            using (var dlg = new PrintDialog { Document = doc, UseEXDialog = true })
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try { doc.Print(); }
                    catch (Exception ex) { MessageBox.Show("Yazdırma başarısız: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private bool ValidateInputs()
        {
            if (numV0.Value < 0m) { MessageBox.Show("Başlangıç hızı negatif olamaz.", "Geçersiz değer", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (numMass.Value <= 0m) { MessageBox.Show("Mermi kütlesi sıfırdan büyük olmalıdır.", "Geçersiz değer", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (numGravity.Value <= 0m) { MessageBox.Show("Yerçekimi sıfırdan büyük olmalıdır.", "Geçersiz değer", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (chkUseDt.Checked && numDt.Value <= 0m) { MessageBox.Show("Zaman adımı sıfırdan büyük olmalıdır.", "Geçersiz değer", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (numH0.Value < 0m) { MessageBox.Show("Başlangıç yüksekliği negatif olamaz.", "Geçersiz değer", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }

            if (chkDrag.Checked)
            {
                var bcMode = cboBCType.SelectedItem as string ?? "Sabit";
                if (chkUseBC.Checked)
                {
                    if (bcMode == "Sabit")
                    {
                        if (numBC.Value <= 0m) { MessageBox.Show("BC (kg/m²) sıfırdan büyük olmalıdır.", "Geçersiz değer", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
                    }
                    else
                    {
                        if (numBCG.Value <= 0m) { MessageBox.Show("BC (G-model) sıfırdan büyük olmalıdır.", "Geçersiz değer", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
                    }
                }
                else
                {
                    if (numCd.Value <= 0m || numArea.Value <= 0m || numRho.Value <= 0m)
                    { MessageBox.Show("Cd, A ve ρ değerleri sıfırdan büyük olmalıdır.", "Geçersiz değer", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
                }
            }
            return true;
        }

        private SimulationResult RunSimulation(bool withDrag, double dt, out List<PointF> trajectory)
        {
            double v0 = (double)numV0.Value;
            double angleDeg = (double)numAngle.Value;
            double m = (double)numMass.Value;
            double g = (double)numGravity.Value;
            double h0 = (double)numH0.Value;
            double windX = (double)numWind.Value;

            double cd = (double)numCd.Value;
            double area = (double)numArea.Value;
            double rho0 = (double)numRho.Value;
            bool rhoAlt = withDrag && chkRhoAlt.Checked && (cboBCType.SelectedItem as string == "Sabit");
            double H = (double)numScaleH.Value;

            bool useBC = withDrag && chkUseBC.Checked;
            string bcType = cboBCType.SelectedItem as string ?? "Sabit";
            double BC_metric = (double)numBC.Value;
            double BC_g = (double)numBCG.Value;

            double kConst = 0.5 * cd * rho0 * area / m;

            double rad = angleDeg * Math.PI / 180.0;
            double vx = v0 * Math.Cos(rad);
            double vy = v0 * Math.Sin(rad);

            double x = 0.0;
            double y = h0;
            double maxY = y;
            double totalTime = 0.0;

            trajectory = new List<PointF>(512);
            trajectory.Add(new PointF((float)x, (float)y));

            double lastVy = vy;

            while (true)
            {
                if (withDrag)
                {
                    double vrelx = vx - windX;
                    double vrely = vy;
                    double speedRel = Math.Sqrt(vrelx * vrelx + vrely * vrely);

                    double rhoStep = rho0;
                    if (rhoAlt) rhoStep = rho0 * Math.Exp(-Math.Max(0.0, y) / H);
                    double densityScale = rhoStep / 1.225;

                    double ax = 0.0, ay = -g;

                    if (speedRel > 0)
                    {
                        if (chkUseBC.Checked && bcType != "Sabit")
                        {
                            double v_fps = speedRel * 3.28083989501312;
                            double dvdt_fps = BallisticDrag.EvaluateDvDtFps(bcType == "G1" ? BallisticDrag.StandardModel.G1 : BallisticDrag.StandardModel.G7, v_fps, BC_g);
                            dvdt_fps *= densityScale;
                            double aMag = dvdt_fps * 0.3048;
                            ax += -aMag * (vrelx / speedRel);
                            ay += -aMag * (vrely / speedRel);
                        }
                        else
                        {
                            double kStep = (chkUseBC.Checked && bcType == "Sabit")
                                ? (0.5 * rhoStep / BC_metric)
                                : (rhoAlt ? (0.5 * cd * rhoStep * area / m) : kConst);
                            ax += -kStep * speedRel * vrelx;
                            ay += -kStep * speedRel * vrely;
                        }
                    }

                    double nextX = x + vx * dt;
                    double nextY = y + vy * dt;
                    double nextVx = vx + ax * dt;
                    double nextVy = vy + ay * dt;

                    if (nextY < 0.0)
                    {
                        double alpha = y <= 0.0 && nextY < 0.0 ? 0.0 : (y / (y - nextY));
                        if (double.IsNaN(alpha) || alpha < 0.0) alpha = 0.0;
                        if (alpha > 1.0) alpha = 1.0;

                        x = x + alpha * (nextX - x);
                        y = 0.0;
                        vx = vx + alpha * (nextVx - vx);
                        vy = vy + alpha * (nextVy - vy);
                        totalTime += alpha * dt;

                        trajectory.Add(new PointF((float)x, (float)y));
                        lastVy = vy;
                        break;
                    }

                    x = nextX; y = nextY; vx = nextVx; vy = nextVy;
                }
                else
                {
                    double nextX = x + vx * dt;
                    double nextY = y + vy * dt - 0.5 * g * dt * dt;
                    double nextVy = vy - g * dt;
                    double nextVx = vx;

                    if (nextY < 0.0)
                    {
                        double alpha = y <= 0.0 && nextY < 0.0 ? 0.0 : (y / (y - nextY));
                        if (double.IsNaN(alpha) || alpha < 0.0) alpha = 0.0;
                        if (alpha > 1.0) alpha = 1.0;

                        x = x + alpha * (nextX - x);
                        y = 0.0;
                        vy = vy + alpha * (nextVy - vy);
                        vx = vx + alpha * (nextVx - vx);
                        totalTime += alpha * dt;

                        trajectory.Add(new PointF((float)x, (float)y));
                        lastVy = vy;
                        break;
                    }

                    x = nextX; y = nextY; vy = nextVy; vx = nextVx;
                }

                if (y > maxY) maxY = y;

                totalTime += dt;
                lastVy = vy;

                trajectory.Add(new PointF((float)x, (float)y));
            }

            double rangeX = x;
            double initialKE = 0.5 * m * v0 * v0;
            double impactSpeed = Math.Sqrt(vx * vx + lastVy * lastVy);
            double impactKE = 0.5 * m * impactSpeed * impactSpeed;

            return new SimulationResult
            {
                TotalTime = totalTime,
                MaxY = maxY,
                RangeX = rangeX,
                InitialKE = initialKE,
                ImpactKE = impactKE
            };
        }

        private List<TrajSample> RunSimulationDetailed(bool withDrag, double dt)
        {
            double v0 = (double)numV0.Value;
            double angleDeg = (double)numAngle.Value;
            double m = (double)numMass.Value;
            double g = (double)numGravity.Value;
            double h0 = (double)numH0.Value;
            double windX = (double)numWind.Value;

            double cd = (double)numCd.Value;
            double area = (double)numArea.Value;
            double rho0 = (double)numRho.Value;
            bool rhoAlt = withDrag && chkRhoAlt.Checked && (cboBCType.SelectedItem as string == "Sabit");
            double H = (double)numScaleH.Value;

            bool useBC = withDrag && chkUseBC.Checked;
            string bcType = cboBCType.SelectedItem as string ?? "Sabit";
            double BC_metric = (double)numBC.Value;
            double BC_g = (double)numBCG.Value;

            double rad = angleDeg * Math.PI / 180.0;
            double vx = v0 * Math.Cos(rad);
            double vy = v0 * Math.Sin(rad);

            double x = 0.0, y = h0, t = 0.0;

            var samples = new List<TrajSample>(1024);
            samples.Add(new TrajSample { T = t, X = x, Y = y, Vx = vx, Vy = vy });

            while (true)
            {
                double vrelx = vx - windX;
                double vrely = vy;
                double speedRel = Math.Sqrt(vrelx * vrelx + vrely * vrely);

                double rhoStep = rho0;
                if (rhoAlt) rhoStep = rho0 * Math.Exp(-Math.Max(0.0, y) / H);
                double densityScale = rhoStep / 1.225;

                double ax = 0.0, ay = -g;

                if (withDrag && speedRel > 0)
                {
                    if (chkUseBC.Checked && bcType != "Sabit")
                    {
                        double v_fps = speedRel * 3.28083989501312;
                        double dvdt_fps = BallisticDrag.EvaluateDvDtFps(bcType == "G1" ? BallisticDrag.StandardModel.G1 : BallisticDrag.StandardModel.G7, v_fps, BC_g);
                        dvdt_fps *= densityScale;
                        double aMag = dvdt_fps * 0.3048;
                        ax += -aMag * (vrelx / speedRel);
                        ay += -aMag * (vrely / speedRel);
                    }
                    else
                    {
                        double kStep = (chkUseBC.Checked && bcType == "Sabit")
                            ? (0.5 * rhoStep / BC_metric)
                            : (0.5 * cd * rhoStep * area / m);
                        ax += -kStep * speedRel * vrelx;
                        ay += -kStep * speedRel * vrely;
                    }
                }

                double nextX = x + vx * dt;
                double nextY = y + vy * dt;
                double nextVx = vx + ax * dt;
                double nextVy = vy + ay * dt;

                if (nextY < 0.0)
                {
                    double alpha = y <= 0.0 && nextY < 0.0 ? 0.0 : (y / (y - nextY));
                    if (double.IsNaN(alpha) || alpha < 0.0) alpha = 0.0;
                    if (alpha > 1.0) alpha = 1.0;

                    x = x + alpha * (nextX - x);
                    y = 0.0;
                    vx = vx + alpha * (nextVx - vx);
                    vy = vy + alpha * (nextVy - vy);
                    t += alpha * dt;

                    samples.Add(new TrajSample { T = t, X = x, Y = y, Vx = vx, Vy = vy });
                    break;
                }

                x = nextX; y = nextY; vx = nextVx; vy = nextVy; t += dt;
                samples.Add(new TrajSample { T = t, X = x, Y = y, Vx = vx, Vy = vy });
            }

            return samples;
        }

        private void SaveChartImage(IEnumerable<PointF> vacuumPoints, IEnumerable<PointF> dragPoints, string filePath)
        {
            var chart = new Chart { Width = 1000, Height = 600 };
            var area = new ChartArea("A");
            area.AxisX.Title = "X (m)";
            area.AxisY.Title = "Y (m)";
            area.AxisX.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisX.LabelStyle.Format = "0.##";
            area.AxisY.LabelStyle.Format = "0.##";
            chart.ChartAreas.Add(area);

            float maxX = 0f, maxY = 0f;

            var sVac = new Series("Vakum")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.RoyalBlue,
                XValueType = ChartValueType.Single,
                YValueType = ChartValueType.Single
            };
            chart.Series.Add(sVac);
            foreach (var p in vacuumPoints)
            {
                if (p.Y < 0) continue;
                sVac.Points.AddXY(p.X, p.Y);
                if (p.X > maxX) maxX = p.X;
                if (p.Y > maxY) maxY = p.Y;
            }

            if (dragPoints != null)
            {
                var sDrag = new Series("Hava Dirençli")
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    Color = Color.OrangeRed,
                    XValueType = ChartValueType.Single,
                    YValueType = ChartValueType.Single
                };
                chart.Series.Add(sDrag);
                foreach (var p in dragPoints)
                {
                    if (p.Y < 0) continue;
                    sDrag.Points.AddXY(p.X, p.Y);
                    if (p.X > maxX) maxX = p.X;
                    if (p.Y > maxY) maxY = p.Y;
                }
            }

            var ground = new Series("Zemin")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.DarkGray,
                BorderDashStyle = ChartDashStyle.Dash,
                IsVisibleInLegend = false
            };
            ground.Points.AddXY(0, 0);
            ground.Points.AddXY(maxX <= 0 ? 1 : maxX, 0);
            chart.Series.Add(ground);

            area.AxisX.Minimum = 0; area.AxisY.Minimum = 0;
            area.AxisX.Maximum = maxX <= 0 ? 1 : maxX * 1.05f;
            area.AxisY.Maximum = maxY <= 0 ? 1 : maxY * 1.10f;

            chart.SaveImage(filePath, ChartImageFormat.Png);
        }

        // İç veri tipleri
        private sealed class TrajSample
        {
            public double T { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Vx { get; set; }
            public double Vy { get; set; }
        }

        private sealed class SimulationResult
        {
            public double TotalTime { get; set; }
            public double MaxY { get; set; }
            public double RangeX { get; set; }
            public double InitialKE { get; set; }
            public double ImpactKE { get; set; }
        }
    }

    internal static class BallisticDrag
    {
        internal enum StandardModel { G1, G7 }

        private struct Regime
        {
            public double VminFps; public double A; public double M;
            public Regime(double vminFps, double a, double m) { VminFps = vminFps; A = a; M = m; }
        }

        private static readonly Regime[] G1 = {
            new Regime(4230, 1.477404177730177e-04, 1.9565),
            new Regime(3680, 1.920339268755614e-04, 1.9250),
            new Regime(3450, 2.894751026819746e-04, 1.8750),
            new Regime(3295, 4.349905111115636e-04, 1.8250),
            new Regime(3130, 6.520421871892662e-04, 1.7750),
            new Regime(2960, 9.748073694078696e-04, 1.7250),
            new Regime(2830, 1.453721560187286e-03, 1.6750),
            new Regime(2680, 2.162887202930376e-03, 1.6250),
            new Regime(2460, 3.209559783129881e-03, 1.5750),
            new Regime(2225, 4.933560977023097e-03, 1.5250),
            new Regime(2015, 7.431744454315722e-03, 1.4750),
            new Regime(1890, 1.142952698998704e-02, 1.4250),
            new Regime(1810, 1.838526531783206e-02, 1.3750),
            new Regime(1730, 3.044120283295612e-02, 1.3250),
            new Regime(1595, 5.090543995008380e-02, 1.2750),
            new Regime(1520, 8.696626663676558e-02, 1.2250),
            new Regime(1420, 1.493210044778970e-01, 1.1750),
            new Regime(1360, 2.497001759259610e-01, 1.1250),
            new Regime(1315, 4.077362526956810e-01, 1.0750),
            new Regime(1280, 6.641473468366750e-01, 1.0250),
            new Regime(1220, 1.102732791246940e+00, 0.9750),
            new Regime(1185, 1.695903023680590e+00, 0.9250),
            new Regime(1150, 2.639552708310750e+00, 0.8750),
            new Regime(1100, 3.962532505243390e+00, 0.8250),
            new Regime(1060, 5.596147174018450e+00, 0.7750),
            new Regime(1025, 7.460693945362000e+00, 0.7250),
            new Regime(980,  9.60795e+00, 0.6750),
            new Regime(945,  1.19078e+01, 0.6250),
            new Regime(905,  1.43450e+01, 0.5750),
            new Regime(860,  1.68880e+01, 0.5250),
            new Regime(810,  1.95530e+01, 0.4750),
            new Regime(780,  2.21660e+01, 0.4250),
            new Regime(750,  2.50280e+01, 0.3750),
            new Regime(0,    0.0,        0.0)
        };

        private static readonly Regime[] G7 = {
            new Regime(4200, 1.29081656775919e-04, 1.9565),
            new Regime(3000, 1.697998136087e-04,   1.9250),
            new Regime(1470, 2.583646067850e-04,   1.8750),
            new Regime(1260, 3.972599164420e-04,   1.8250),
            new Regime(1110, 5.945501143000e-04,   1.7750),
            new Regime(1000, 8.739952969000e-04,   1.7250),
            new Regime(900,  1.286515211000e-03,   1.6750),
            new Regime(800,  1.874962580000e-03,   1.6250),
            new Regime(700,  2.599080914000e-03,   1.5750),
            new Regime(600,  3.495180442000e-03,   1.5250),
            new Regime(500,  4.571507588000e-03,   1.4750),
            new Regime(400,  5.859010889000e-03,   1.4250),
            new Regime(300,  7.473508145000e-03,   1.3750),
            new Regime(150,  9.265983283000e-03,   1.3250),
            new Regime(0,    1.152366455000e-02,   1.2750)
        };

        internal static double EvaluateDvDtFps(StandardModel model, double vFps, double bcG)
        {
            if (vFps <= 0 || bcG <= 0) return 0.0;
            var table = model == StandardModel.G1 ? G1 : G7;
            foreach (var r in table)
            {
                if (vFps >= r.VminFps)
                {
                    if (r.A <= 0) break;
                    return (r.A * Math.Pow(vFps, r.M)) / bcG;
                }
            }
            return 0.0;
        }
    }
}
