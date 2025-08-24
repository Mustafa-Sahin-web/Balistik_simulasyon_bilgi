using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BalistikCalisma
{
    public partial class TrajectoryChartForm : Form
    {
        private ContextMenuStrip _ctxMenu;

        // Tek seriyi destekleyen önceki ctor korunuyor
        public TrajectoryChartForm(IEnumerable<PointF> points, string title = null)
            : this(points, null, title)
        {
        }

        // İki seri (Vakum ve Hava Dirençli) için genişletilmiş ctor
        public TrajectoryChartForm(IEnumerable<PointF> vacuumPoints, IEnumerable<PointF> dragPoints, string title = null)
        {
            InitializeComponent();
            InitializeChart(vacuumPoints ?? Enumerable.Empty<PointF>(), dragPoints ?? Enumerable.Empty<PointF>(), title);
            InitializeContextMenu();
            chart.MouseDoubleClick += (s, e) => ResetZoom();
        }

        private void InitializeChart(IEnumerable<PointF> vacuumPoints, IEnumerable<PointF> dragPoints, string title)
        {
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            chart.Legends.Clear();
            chart.Titles.Clear();

            if (!string.IsNullOrWhiteSpace(title))
            {
                chart.Titles.Add(new Title(title, Docking.Top,
                    new Font("Segoe UI", 12f, FontStyle.Bold), Color.Black));
            }

            chart.Series.Clear();
            chart.ChartAreas.Clear();

            var area = new ChartArea("TrajectoryArea");
            area.AxisX.Title = "X (m)";
            area.AxisY.Title = "Y (m)";
            area.AxisX.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;
            area.AxisX.LabelStyle.Format = "0.##";
            area.AxisY.LabelStyle.Format = "0.##";

            // Zoom/Pan
            area.CursorX.IsUserEnabled = true;
            area.CursorX.IsUserSelectionEnabled = true;
            area.CursorY.IsUserEnabled = true;
            area.CursorY.IsUserSelectionEnabled = true;
            area.AxisX.ScaleView.Zoomable = true;
            area.AxisY.ScaleView.Zoomable = true;

            chart.ChartAreas.Add(area);
            chart.Legends.Add(new Legend { Docking = Docking.Bottom });

            float maxX = 0f;
            float maxY = 0f;

            // Vakum serisi
            var sVac = new Series("Vakum")
            {
                ChartType = SeriesChartType.Line,
                XValueType = ChartValueType.Single,
                YValueType = ChartValueType.Single,
                BorderWidth = 2,
                Color = Color.RoyalBlue,
                ToolTip = "Vakum: X=#VALX{0.##}, Y=#VALY{0.##}"
            };
            sVac.MarkerStyle = MarkerStyle.Circle;
            sVac.MarkerSize = 3;
            chart.Series.Add(sVac);

            foreach (var p in vacuumPoints)
            {
                if (p.Y < 0) continue;
                sVac.Points.AddXY(p.X, p.Y);
                if (p.X > maxX) maxX = p.X;
                if (p.Y > maxY) maxY = p.Y;
            }

            // Hava dirençli seri (varsa)
            Series sDrag = null;
            bool hasDrag = dragPoints != null && dragPoints.Any();
            if (hasDrag)
            {
                sDrag = new Series("Hava Dirençli")
                {
                    ChartType = SeriesChartType.Line,
                    XValueType = ChartValueType.Single,
                    YValueType = ChartValueType.Single,
                    BorderWidth = 2,
                    Color = Color.OrangeRed,
                    ToolTip = "Dirençli: X=#VALX{0.##}, Y=#VALY{0.##}"
                };
                sDrag.MarkerStyle = MarkerStyle.Circle;
                sDrag.MarkerSize = 3;
                chart.Series.Add(sDrag);

                foreach (var p in dragPoints)
                {
                    if (p.Y < 0) continue;
                    sDrag.Points.AddXY(p.X, p.Y);
                    if (p.X > maxX) maxX = p.X;
                    if (p.Y > maxY) maxY = p.Y;
                }
            }

            // Zemin çizgisi
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

            // Eksen sınırları
            area.AxisX.Minimum = 0;
            area.AxisY.Minimum = 0;
            area.AxisX.Maximum = maxX <= 0 ? 1 : maxX * 1.05f;
            area.AxisY.Maximum = maxY <= 0 ? 1 : maxY * 1.10f;
        }

        private void InitializeContextMenu()
        {
            _ctxMenu = new ContextMenuStrip();
            var saveItem = new ToolStripMenuItem("PNG olarak kaydet", null, (s, e) => SaveChartAsImage());
            var resetItem = new ToolStripMenuItem("Pan/Zoom sıfırla", null, (s, e) => ResetZoom());

            _ctxMenu.Items.Add(saveItem);
            _ctxMenu.Items.Add(new ToolStripSeparator());
            _ctxMenu.Items.Add(resetItem);

            chart.ContextMenuStrip = _ctxMenu;
        }

        private void ResetZoom()
        {
            foreach (ChartArea ca in chart.ChartAreas)
            {
                ca.AxisX.ScaleView.ZoomReset(0);
                ca.AxisY.ScaleView.ZoomReset(0);
                ca.CursorX.SetCursorPosition(double.NaN);
                ca.CursorY.SetCursorPosition(double.NaN);
            }
        }

        private void SaveChartAsImage()
        {
            using (var sfd = new SaveFileDialog
            {
                Title = "Grafiği Kaydet",
                Filter = "PNG Dosyası (*.png)|*.png|JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp",
                DefaultExt = "png",
                AddExtension = true,
                FileName = "trajektori.png"
            })
            {
                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                var ext = Path.GetExtension(sfd.FileName)?.ToLowerInvariant();
                var fmt = ChartImageFormat.Png;
                if (ext == ".jpg" || ext == ".jpeg") fmt = ChartImageFormat.Jpeg;
                else if (ext == ".bmp") fmt = ChartImageFormat.Bmp;

                chart.SaveImage(sfd.FileName, fmt);
            }
        }
    }
}
