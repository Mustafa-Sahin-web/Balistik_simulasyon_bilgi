using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BalistikCalisma
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, string> _ammoData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly List<string> _ammoNames = new List<string>(); // filtreleme için kaynak
        private ContextMenuStrip _detailsMenu;

        public Form1()
        {
            InitializeComponent();
            InitializeAmmoData();
            BuildAmmoNameCache();
            LoadAmmoToCombo();
            txtAmmoDetails.Text = string.Empty;

            ApplyUiStyling();
            SetupDetailsContextMenu();
        }

        private void BuildAmmoNameCache()
        {
            _ammoNames.Clear();
            _ammoNames.AddRange(_ammoData.Keys.OrderBy(k => k, StringComparer.CurrentCultureIgnoreCase));
        }

        private void LoadAmmoToCombo(string filter = null)
        {
            var selectedPrev = cboAmmo.SelectedItem as string;

            IEnumerable<string> items = _ammoNames;
            if (!string.IsNullOrWhiteSpace(filter))
                items = items.Where(n => n.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0);

            cboAmmo.BeginUpdate();
            try
            {
                cboAmmo.Items.Clear();
                foreach (var name in items)
                    cboAmmo.Items.Add(name);

                if (cboAmmo.Items.Count > 0)
                {
                    int idx = selectedPrev != null ? cboAmmo.Items.IndexOf(selectedPrev) : -1;
                    cboAmmo.SelectedIndex = idx >= 0 ? idx : 0;
                }
                else
                {
                    txtAmmoDetails.Text = string.Empty;
                }
            }
            finally
            {
                cboAmmo.EndUpdate();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadAmmoToCombo(txtSearch.Text);
        }

        private void ApplyUiStyling()
        {
            try { this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point); } catch { }
            try { lblTitle.ForeColor = Color.FromArgb(25, 118, 210); } catch { }
            try
            {
                txtAmmoDetails.Font = new Font("Consolas", 9.5f, FontStyle.Regular, GraphicsUnit.Point);
                txtAmmoDetails.BackColor = Color.FromArgb(250, 250, 250);
                txtAmmoDetails.BorderStyle = BorderStyle.FixedSingle;
            }
            catch { }

            var tip = new ToolTip { IsBalloon = false, InitialDelay = 300, ReshowDelay = 200, AutoPopDelay = 8000 };
            tip.SetToolTip(cboAmmo, "Mühimmat seçiniz. Arama kutusu ile filtreleyebilirsiniz.");
            tip.SetToolTip(btnSimulasyon, "Seçili mühimmatla simülasyonu başlat.");
            tip.SetToolTip(txtSearch, "İsme göre filtrele (anlık).");
        }

        private void SetupDetailsContextMenu()
        {
            _detailsMenu = new ContextMenuStrip();
            var copyItem = new ToolStripMenuItem("Tümünü Kopyala", null, (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtAmmoDetails.Text))
                    Clipboard.SetText(txtAmmoDetails.Text);
            });
            var saveItem = new ToolStripMenuItem("Metni Kaydet (.txt)", null, (s, e) =>
            {
                using (var sfd = new SaveFileDialog
                {
                    Title = "Teknik Özellikleri Kaydet",
                    Filter = "Metin Dosyası (*.txt)|*.txt",
                    FileName = "muhimmat_bilgisi.txt",
                    AddExtension = true,
                    DefaultExt = "txt"
                })
                {
                    if (sfd.ShowDialog(this) != DialogResult.OK) return;
                    File.WriteAllText(sfd.FileName, txtAmmoDetails.Text ?? string.Empty);
                }
            });

            _detailsMenu.Items.Add(copyItem);
            _detailsMenu.Items.Add(new ToolStripSeparator());
            _detailsMenu.Items.Add(saveItem);

            txtAmmoDetails.ContextMenuStrip = _detailsMenu;
        }

        private void InitializeAmmoData()
        {
            // Mevcut örnekler
            var key = "9,65 MM (.38 CAL ) NORMAL TABANCA FİŞİĞİ";
            var details =
@"9,65 MM (.38 CAL ) NORMAL TABANCA FİŞİĞİ
TEKNİK ÖZELLİKLER

Fişek Boyu : 30,6 + 1 mm
Fişek Ağırlığı : ~15,7 g
Hız (10 M) :  180 ± 10 m/s
Ortalama Basınç : max. 1500 Kg / cm2
Dağılım   :  max. 9 cm (30 m’de)
Mermi İrtibat Kuvveti :   min. 15 Kgf
Kovan Model Numarası  :  9,65 mm NORMAL KOVAN
Kovan Boyu : 19,23 + 0,25 mm
Mermi Malzemesi :   FMJ, PİRİNÇ ve KURŞUN ANTİMON ALAŞIMI
Mermi Ağırlığı   :  11,5 ± 0,1 g
Kovan Malzemesi   : PİRİNÇ (CuZn30)
Kapsül     : 9 mm KAPSÜL, BOXER
Barut      : KÜRESEL BARUT
Kullanıldığı Silah :  9,65 mm COLT ve 9,65 mm SMITH WESSON TOPLU TABANCALAR";
            _ammoData[key] = details;

            var key556 = "5,56 mmx45 (SS109/M855) NORMAL FİŞEK";
            var details556 =
@"5,56 mmx45 (SS109/M855) NORMAL FİŞEK
TEKNİK ÖZELLİKLER

Şartname  STANAG 4172, AEP-97 EDITION A (MULTI CALIBRE MOPI)
Fişek Boyu  57,4 mm
Fişek Ağırlığı  ~12,5 g
Hız (23,7 M)  914,4 ± 12,2 m/s
Mermi İrtibat Kuvveti  min. 20,4 kgf
Kovan Model Numarası  5,56 mmx45 KOVAN
Kovan Malzemesi  PİRİNÇ (CuZn28 veya CuZn30)
Mermi Malzemesi  TOMBAK, ÇELİK ÇEKİRDEK VE KURŞUN ÇEKİRDEK ( KURŞUN-ANTIMON ALAŞIMI)
Kapsül  5,56 mm KAPSÜL, BOXER
Barut  KÜRESEL BARUT
Kullanıldığı Silah  M 16A2, HK 33 E, MINIMI vs.
Ambalaj  30 FİŞEK BİR MUKAVVA KUTUDA,15 MUKAVVA KUTU BİR PVC POŞETTE, 5 PVC POŞET 1 TAHTA SANDIKTA VE 30 TAHTA SANDIK 1 PALETTE (TOPLAM 67500 ADET FİŞEK)
Nato Stok Numarası  1305 27 017 9197 (Mukavva Kutulu)
Dağılım(100 M)  Sx ve Sy max. 2,2 cm
Mayon Tipi  M27 MAYON
Action Time  max. 3 ms
Mermi Ağırlığı  4 g
Zırh Delme  Mermilerin en az %90’ı 570 metre mesafedeki, 3,5mm kalınlığındaki çelik plakayı (SAE 1010 veya 1020) tamamen delmektedir.
Ortalama Namlu Basıncı  min. 1030 bar (Port-3s)
Ortalama Kovan Ağız Basıncı  max. 4450 bar (P+3s)
Balistik Katsayı  0,35 (G1)
Nato Tasarım Numarası  AC/225-141A";
            _ammoData[key556] = details556;

            var key762 = "7,62 mmx51 (M80) NORMAL FİŞEK";
            var details762 =
@"7,62 mmx51 (M80) NORMAL FİŞEK
TEKNİK ÖZELLİKLER

Şartname  STANAG 2310, AEP-97 EDITION A (MULTI CALIBRE MOPI)
Fişek Boyu  71,12 mm
Fişek Ağırlığı  ~ 24,5 g
Hız (23,7 M)  838 ± 9,1 m/s
Mermi İrtibat Kuvveti  min. 27 kgf (265 N)
Kovan Model Numarası  7,62 mmx51 KOVAN
Kovan Malzemesi  PİRİNÇ (CuZn28 veya CuZn30)
Mermi Malzemesi  TOMBAK, KURŞUN ÇEKİRDEK (KURŞUN-ANTIMON ALAŞIMI)
Kapsül  7,62 mm KAPSÜL, BOXER
Barut  KÜRESEL BARUT
Kullanıldığı Silah  G3, FAL, MG3, L7A2, M60
Ambalaj  1) 20 FİŞEK 1 KARTON KUTUDA, 10 KARTON KUTU 1 PVC POŞETTE 5 PVC POŞET 1 TAHTA SANDIKTA, 30 TAHTA SANDIK PALETTE (TOPLAM 30000 FİŞEK)
Ambalaj  1) 20 FİŞEK 1 KARTON KUTUDA, 10 KARTON KUTU 1 PVC POŞETTE 5 PVC POŞET 1 TAHTA SANDIKTA, 30 TAHTA SANDIK PALETTE (TOPLAM 30000 FİŞEEK)
Nato Stok Numarası  1305 27 043 1741 (MUKAVVA KUTULU)…1305 27 053 9288 (M13 MAYONLU)
Dağılım(100 M)  ort dağ. yarıçapı max. 3,5 cm
Mayon Tipi  M13 MAYON
Action Time  max. 4 milisaniye
Mermi Ağırlığı  9,65 – 0,20 g
Ortalama Namlu Basıncı  min. 561 kg/cm²
Ortalama Kovan Ağız Basıncı  max. 3876 kg/cm²
Balistik Katsayı  0,54 (G1)
Nato Tasarım Numarası  AC/116-43A";
            _ammoData[key762] = details762;

            var key9x19 = "9 mmx19 PARABELLUM TABANCA FİŞEĞİ";
            var details9x19 =
@"9 mmx19 PARABELLUM TABANCA FİŞEĞİ
TEKNİK ÖZELLİKLER

Şartname  STANAG 4090, AEP-97 EDITION A (MULTI CALIBRE MOPI)
Fişek Boyu  29,69 – 0,3 mm
Fişek Ağırlığı  ~12,15 g
Hız  370± 10 m/s (16 m’de )
Hız Standart Sapması  max. 9 m/s
Ortalama Kovan Ağız Basıncı  Max. 2850 bar
Dağılım  Ort. dağılım yarıçapı max. 7,6 cm ( 46m’de)
Mermi İrtibat Kuvveti  min. 20,4 kgf
Kovan Model Numarası  9 mmx19 PARABELLUM KOVAN
Mermi Tipi  FMJ, MERMİ GÖMLEK YÜKSÜĞÜ PİRİNÇ (CuZn36), MERMİ ÇEKİRDEĞİ KURŞUN-ANTİMON ALAŞIMI
Mermi Ağırlığı  8 ± 0,075 g
Kovan Malzemesi  PİRİNÇ (CuZn30)
Kapsül  9 mm KAPSÜL, BOXER
Barut  KÜRESEL BARUT
Kullanıldığı Silah  9mm BELÇİKA BROWNING,9mm P1 ALMAN WALTHER, 9mm PM 12S İTALYAN BERETTA(HAFİF MAKİNALI), 9mm 92F İTALYAN BERETTA ,9 mm CZ 75, 9 mm RUGER, 9 mm ASTRA, 9mm MP-5 MAKİNALI TÜFEK
Ambalaj  1) 50 ADET FİŞEK 1 PLASTİK SEPARATÖRDE VE MUKAVVA KUTUDA, 12 MUKAVVA KUTU 1 PVC POŞETTE, 5 PVC POŞET 1 TAHTA SANDIKTA, 30 TAHTA SANDIK 1 PALETTE (TOPLAM 90000 FİŞEK)
Ambalaj  1) 50 ADET FİŞEK 1 PLASTİK SEPARATÖRDE VE MUKAVVA KUTUDA, 12 MUKAVVA KUTU 1 PVC POŞETTE, 5 PVC POŞET 1 TAHTA SANDIKTA, 30 TAHTA SANDIK 1 PALETTE (TOPLAM 90000 FİŞEK)
Stok No  1305 27 007 8111 (TAHTA SANDIKLI)";
            _ammoData[key9x19] = details9x19;

            // YENİ EKLENENLER
            var key50 = "12,7 mmx99 (.50 cal) (M17) FİŞEK";
            var details50 =
@"12,7 mmx99 (.50 cal) (M17) FİŞEK
TEKNİK ÖZELLİKLER

Şartname  MIL-C-1318 B (3 Nisan 1985 ), KKKTEKŞ-F-643-B
Fişek Boyu  138,43 mm
Fişek Ağırlığı  ~ 114 g
Hız (23,7 M)  872 ± 12 m/s
Ortalama Basınç  max. 3797 kg/cm²
Dağılım(232,4M)  Ort. dağılım yarıçapı max. 21,54 cm
Mermi İrtibat Kuvveti  min. 90 kgf
İz Verme  İzli mermilerin en az %90’ı en az 2,5 sn. süreyle iz vermektedir.
Kovan Model Numarası  12,7 mmx99 KOVAN
Kovan Malzemesi  PİRİNÇ (CuZn30 )
Mermi Malzemesi  TOMBAK, KURŞUN, İZ MADDESİ
Kapsül  12,7 mm KAPSÜL, BOXER
Mayon Tipi  M9
Barut  SİLİNDİRİK BARUT
Kullanıldığı Silah  M2 , M3
Ambalaj  120 ADET FİŞEK BİR MKE S1 METAL KUTU VEYA M2A1 METAL KUTUDA (DÖKÜM) / 2 MKE S1 METAL KUTU BİR TEL SARILI SANDIKTA VE 45 ADET TEL SARILI SANDIK BİR PALETTE (TOPLAM 10800 ADET DÖKÜM FİŞEK)
Ambalaj (Alternatif)  100 ADET M9 MAYONLU FİŞEK BİR MKE S1 METAL KUTU VEYA M2A1 METAL KUTUDA, 2 MKE S1 METAL KUTU VEYA M2A1 KUTU BİR TEL SARILI SANDIKTA VE 45 ADET TEL SARILI SANDIK BİR PALETTE (TOPLAM 9000 ADET M9 MAYONLU FİŞEK)
Mermi Ağırlığı  41,7 g
Nato Stok Numarası  1305 27 000 2045 (DÖKÜM)";
            _ammoData[key50] = details50;

            var key20 = "20 MM x 110 HEI (MKE MOD 1102) HARP BAŞLIKLI FİŞEK";
            var details20 =
@"20 MM x 110 HEI (MKE MOD 1102) HARP BAŞLIKLI FİŞEK
TEKNİK ÖZELLİKLER

Şartname  DZ.K.K. MÜHT.Ş. 01/03
Fişek Boyu  max. 178 mm
Fişek Ağırlığı  ~ 220 g
Hız (23,7 M)  945 ± 15,24 m/s
Hız Standart Sapması  max. 12,19 m /s
Ortalama Basınç  max. 4254 Kg/cm2
Dağılım(232,4M)  max. 16,15 cm
Mermi İrtibat Kuvveti  min. 250 Kgf
Mermi Fonksiyonu  NAMLUDAN 182,88±22,86 MESAFEDE 0,04 inch KALINLIKTAKİ ALÜMİNYUM PLAKADA YÜKSEK PATLAMA İLE FONKSİYON YAPAR.
Kovan Model Numarası  MKE MOD 1001 KOVAN
Kovan Boyu  110,31 – 0,51 mm
Kovan Malzemesi  PİRİNÇ (CuZn30)
Mermi Gövdesi Malzemesi  ÇELİK (Ç 1040)
Mermi Tapası Tipi ve Malzemesi  M 505 A3 TAPA
Kapsül  12,7 mm (.50 cal ) KAPSÜL
Barut Tipi  20 mm KÜRESEL BARUT
Kullanıldığı Silah  20 mm MK 4 TÜFEK
Ambalaj  KARTON TÜPLERDE 180 ADET FİŞEK T 46 METAL KUTUDA, 20 ADET T 46 METAL KUTU 1 PALETTE ( TOPLAM 3600 FİŞEK)
Stok No  1305 27 000 4279";
            _ammoData[key20] = details20;

            var key105TP = "105 mm TP MKE MOD 234 DERS ATIŞ TOP MÜHİMMATI";
            var details105TP =
@"105 mm TP MKE MOD 234 DERS ATIŞ TOP MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  24155 g
Patlayıcı Tipi  Sorel Cement
Tapa  M73 kör
Barut  M1
Tam Atım Boy  1028 mm
İlk Hız  683 m/s
Azami Menzil  11000 m
Kullanıldığı Silah  M48A5-M48T5 Leopar 1 tankı";
            _ammoData[key105TP] = details105TP;

            var key105HE = "105 mm HE MKE MOD233 TOP MÜHİMMATI";
            var details105HE =
@"105 mm HE MKE MOD233 TOP MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  24155 g
Patlayıcı Tipi  TNT
Tapa  M557
Barut  M1,M6
Tam Atım Boy  1028 mm
İlk Hız  683 m/s
Azami Menzil  11000 m
Kullanıldığı Silah  M48A5-M48T5 Leopar 1 tankı";
            _ammoData[key105HE] = details105HE;

            var key120HE = "120 mm HE MKE MOD 209 HAVAN MÜHİMMATI";
            var details120HE =
@"120 mm HE MKE MOD 209 HAVAN MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  23900 g
Patlayıcı Tipi  TNT
Tapa  M557
Tam Atım Boy  827 mm
İlk Hız  365 m/s
Azami Menzil  8180 m
Kullanıldığı Silah  TOSAM-HY-12";
            _ammoData[key120HE] = details120HE;

            var key120TP = "120 mm MKE MOD 228 HAVAN DERS ATIŞ MÜHİMMATI";
            var details120TP =
@"120 mm MKE MOD 228 HAVAN DERS ATIŞ MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  17000 g
İmla Maddesi  Sorel çimento
Tapa  M51A5
Tam Atım Boy  827 mm
İlk Hız  365 m/s
Azami Menzil  8132 m
Kullanıldığı Silah  TOSAM-HY-12";
            _ammoData[key120TP] = details120TP;

            var key120SIS = "120 mm MKE MOD226 SİS MÜHİMMATI";
            var details120SIS =
@"120 mm MKE MOD226 SİS MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  18000 g
Tam Atım Boy  827 mm
Tapa  M51A5
İmla Maddesi  beyaz fosfor
İlk Hız  365 m/s
Azami Menzil  8132 m";
            _ammoData[key120SIS] = details120SIS;

            var key81HE = "81 mm HE MKE MOD214 HAVAN MÜHİMMATI";
            var details81HE =
@"81 mm HE MKE MOD214 HAVAN MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  4820 g
Patlayıcı Tipi  TNT
Tapa  AZDM 111 A2
Tam Atım Boy  500.52 mm
İlk Hız  331 m/s
Azami Menzil  6500 m
Barut  M8
Sevk Fişeği  MKE MOD30
İmla Maddesi  TNT
Kapsül  M34
Kullanıldığı Silah  Tosam UT-1 Havan";
            _ammoData[key81HE] = details81HE;

            var key81TP = "81 mm MKE MOD 238 HAVAN HEDEF DERS ATŞ. MÜHİMMATI";
            var details81TP =
@"81 mm MKE MOD 238 HAVAN HEDEF DERS ATŞ. MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  4820 g
Tapa  AZDM 111 A2
Tam Atım Boy  500.52 mm
İlk Hız  331 m/s
Azami Menzil  6500 m
Barut  M8
İmla Maddesi  Sorel Çimento
Sevk Fişeği  MKE MOD30
Kapsül  M34
Kullanıldığı Silah  TOSAM UT-1 Havan";
            _ammoData[key81TP] = details81TP;

            var key81Light = "81 mm M301 A2 AYDINLATMA HAVAN MÜHİMMATI";
            var details81Light =
@"81 mm M301 A2 AYDINLATMA HAVAN MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  4863 g
Tapa  DM93
Tam Atım Boy  571 mm
İlk Hız  173 m/s
Barut  M8
Sevk Fişeği  MKE MOD30
Kapsül  M34
Kullanıldığı Silah  M1 ve M29 havan
İmla Maddesi  Tenvir aydınlatma malzemesi";
            _ammoData[key81Light] = details81Light;

            var key81M43 = "81 mm HE M43 A1 B1 HAVAN TAHRİP MÜHİMMATI";
            var details81M43 =
@"81 mm HE M43 A1 B1 HAVAN TAHRİP MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  3253 g
İmla Maddesi  TNT
Tapa  AZDM111 A2
Tam Atım Boy  338.33 mm
İlk Hız  213.6 m/s
Azami Menzil  3017 m
Barut  M1A1
Kullanıldığı Silah  M1 ve M29 havan";
            _ammoData[key81M43] = details81M43;

            var key81MOD273 = "81 mm MKE MOD273 DERS ATIŞ MERMİSİ";
            var details81MOD273 =
@"81 mm MKE MOD273 DERS ATIŞ MERMİSİ
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  3253 g
Tam Atım Boy  338.3 mm
Tapa  AZDM111 A2 VEYA AZDM111 A5
İmla Maddesi  Sorel çimento
Menzil  max. 3017 m
Kullanıldığı Silah  M1 ve M29 havanları";
            _ammoData[key81MOD273] = details81MOD273;

            var key60TP = "60 mm MKE MOD257 HAVAN DERS ATIŞ MÜHİMMATI";
            var details60TP =
@"60 mm MKE MOD257 HAVAN DERS ATIŞ MÜHİMMATI
TEKNİK ÖZELLİKLER

Ağırlık  1421 g
Boy  242.73 mm
Tapa  AZDM 111 A2
İmla Maddesi  sorel çimento
Sevk Fişeği  M5 A1
İlk Hız  158 m/s
Maksimum Menzil  1814 m
Kapsül  M32
Kullanıldığı Silah  60 mm komando havan";
            _ammoData[key60TP] = details60TP;

            var key60Train = "60 mm MKE MOD256 EĞİTİM HAVAN MÜHİMMATI";
            var details60Train =
@"60 mm MKE MOD256 EĞİTİM HAVAN MÜHİMMATI
TEKNİK ÖZELLİKLER

Ağırlık  1365 g
Boy  242.8 mm
Tapa  Kör
İmla Maddesi  kum+ağaç talaşı
Sevk Fişeği  M5A1(boş)
Kapsül  M32(boş)
Kullanıldığı Silah  60 mm Komando havanı";
            _ammoData[key60Train] = details60Train;

            var key60M49 = "60 mm M49A2 TAHRİP MERMİSİ";
            var details60M49 =
@"60 mm M49A2 TAHRİP MERMİSİ
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  1421 g
Tam Atım Boy  242.8 mm
Tapa  AZDM111 A2
İmla Maddesi  TNT
Menzil  1814 m max.
Kullanıldığı Silah  60 mm komando havanı";
            _ammoData[key60M49] = details60M49;

            var key155HEER = "155 mm MKE MOD 274 HEER";
            var details155HEER =
@"155 mm MKE MOD 274 HEER
Dipten Yanmalı (Base Bleed) Uzun Menzilli Tahrip Mühimmatı
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  45.350 g (orta kare)
Tam Atım Boy  950 mm
Tapa  M582A1 Zaman, Çarpma ve Yaklaşımlı Tapalar
İmla Maddesi  TNT
Barut Hakkı  DM92 Modüler Barut Sistemi
İlk Hız  940 m/s (6 modül ile)
Menzil  39.000 m (Deniz Seviyesi)
Kullanıldığı Silah  Fırtına ve Panter Obüsleri (52 Kalibre)";
            _ammoData[key155HEER] = details155HEER;

            var key35ULD = "35 mm ULD 034 UÇ. SV. T. HD. DRS. ATŞ. MÜHİMMATI";
            var details35ULD =
@"35 mm ULD 034 UÇ. SV. T. HD. DRS. ATŞ. MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  1,570 kg
Tam Atım Boy  387 mm
Barut  NC.01.T.35.OE.M.SMS
Kapsül  WK ZSD 263
İlk Hız  1175 m/s
Maksimum Menzil  11000 m
Etkili Menzil  6000 m
Kullanıldığı Silah  KDP (353 MK) TOPU
Tapa  Kör tapa";
            _ammoData[key35ULD] = details35ULD;

            var key35MSD = "35 mm MSD 020 UÇ. SV. TAHRİP YANGIN MÜHİMMATI";
            var details35MSD =
@"35 mm MSD 020 UÇ. SV. TAHRİP YANGIN MÜHİMMATI
TEKNİK ÖZELLİKLER

Tam Atım Ağırlık  1,570 kg
Tam Atım Boy  387 mm
Tapa  KZD 242
İmla Maddesi  Hexal
Kapsül  WK ZSD 263
Barut  NC.01.T.35.OE.M.SMS
İlk Hız  1175 m/s
Maksimum Menzil  11000 m
Etkili Menzil  6000 m
Kullanıldığı Silah  KDP (353 MK) Topu";
            _ammoData[key35MSD] = details35MSD;

            var key40HE = "40 mm BOMBA ATAR MÜHİMMATI (MKE MOD60 HE)";
            var details40HE =
@"40 mm BOMBA ATAR MÜHİMMATI (MKE MOD60 HE)
TEKNİK ÖZELLİKLER

Ağırlık  230 g
Kullanıldığı Silah  M79,M203 , T40
İmla Maddesi  COMP-B (32 g) TNT
İlk Hız  76 m/s
Maksimum Menzil  400 m
Tapa  M551";
            _ammoData[key40HE] = details40HE;

            var key25M791 = "25 mm M791 APDS-T MERMİSİ";
            var details25M791 =
@"25 mm M791 APDS-T MERMİSİ
ZIRH DELİCİ SABOTLU İZLİKLİ APDS-T
TEKNİK ÖZELLİKLER

Ağırlık  455 g
Boy  223 mm
Namlu Çıkış Hızı  1345 m/s
İzlik Zamanı  2.2 s
Kullanıldığı Silah  M242,KBA,M811,GAV-12";
            _ammoData[key25M791] = details25M791;

            var key25M793 = "25 mm M793 TP-T MERMİSİ";
            var details25M793 =
@"25 mm M793 TP-T MERMİSİ
TP-T İzlikli Ders Atış
TEKNİK ÖZELLİKLER

Ağırlık  492 g
Boy  223 mm
Namlu Çıkış Hızı  1100 m/s
İzlik Zamanı  6 s";
            _ammoData[key25M793] = details25M793;

            var key25M792 = "25 mm M792 HEI-T MERMİSİ";
            var details25M792 =
@"25 mm M792 HEI-T MERMİSİ
Yüksek Tahrip Güçlü Yangın İzlikli ve İntiharlı
TEKNİK ÖZELLİKLER

Ağırlık  493 g
Boy  223 mm
Namlu Çıkış Hızı  1100 m/s
İzlik Zamanı  6 s";
            _ammoData[key25M792] = details25M792;
        }

        private void cboAmmo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cboAmmo.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(selected))
            {
                txtAmmoDetails.Text = string.Empty;
                return;
            }

            if (_ammoData.TryGetValue(selected, out var detailsText))
            {
                txtAmmoDetails.Text = detailsText;
            }
            else
            {
                txtAmmoDetails.Text = "Seçilen mühimmat için bilgi bulunamadı.";
            }
        }

        private void btnSimulasyon_Click(object sender, EventArgs e)
        {
            var selected = cboAmmo.SelectedItem as string;
            using (var simForm = new SimulationForm(selected))
            {
                // Seçime göre varsayılanları uygula
                switch (selected)
                {
                    case "5,56 mmx45 (SS109/M855) NORMAL FİŞEK":
                        simForm.ApplyAmmoDefaults(v0: 914.4, massKg: 0.0040, bcG1: 0.35);
                        break;
                    case "7,62 mmx51 (M80) NORMAL FİŞEK":
                        simForm.ApplyAmmoDefaults(v0: 838.0, massKg: 0.00965, bcG1: 0.54);
                        break;
                    case "9 mmx19 PARABELLUM TABANCA FİŞEĞİ":
                        simForm.ApplyAmmoDefaults(v0: 370.0, massKg: 0.0080);
                        break;
                    case "9,65 MM (.38 CAL ) NORMAL TABANCA FİŞİĞİ":
                        simForm.ApplyAmmoDefaults(v0: 180.0, massKg: 0.0115);
                        break;

                    // Yeni eklenenler
                    case "12,7 mmx99 (.50 cal) (M17) FİŞEK":
                        simForm.ApplyAmmoDefaults(v0: 872.0, massKg: 0.0417);
                        break;
                    case "20 MM x 110 HEI (MKE MOD 1102) HARP BAŞLIKLI FİŞEK":
                        simForm.ApplyAmmoDefaults(v0: 945.0, massKg: 0.220);
                        break;
                    case "105 mm TP MKE MOD 234 DERS ATIŞ TOP MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 683.0, massKg: 24.155);
                        break;
                    case "105 mm HE MKE MOD233 TOP MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 683.0, massKg: 24.155);
                        break;
                    case "120 mm HE MKE MOD 209 HAVAN MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 365.0, massKg: 23.900);
                        break;
                    case "120 mm MKE MOD 228 HAVAN DERS ATIŞ MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 365.0, massKg: 17.000);
                        break;
                    case "120 mm MKE MOD226 SİS MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 365.0, massKg: 18.000);
                        break;
                    case "81 mm HE MKE MOD214 HAVAN MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 331.0, massKg: 4.820);
                        break;
                    case "81 mm MKE MOD 238 HAVAN HEDEF DERS ATŞ. MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 331.0, massKg: 4.820);
                        break;
                    case "81 mm M301 A2 AYDINLATMA HAVAN MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 173.0, massKg: 4.863);
                        break;
                    case "81 mm HE M43 A1 B1 HAVAN TAHRİP MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 213.6, massKg: 3.253);
                        break;
                    case "81 mm MKE MOD273 DERS ATIŞ MERMİSİ":
                        simForm.ApplyAmmoDefaults(massKg: 3.253);
                        break;
                    case "60 mm MKE MOD257 HAVAN DERS ATIŞ MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 158.0, massKg: 1.421);
                        break;
                    case "60 mm MKE MOD256 EĞİTİM HAVAN MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(massKg: 1.365);
                        break;
                    case "60 mm M49A2 TAHRİP MERMİSİ":
                        simForm.ApplyAmmoDefaults(massKg: 1.421);
                        break;
                    case "155 mm MKE MOD 274 HEER":
                        simForm.ApplyAmmoDefaults(v0: 940.0, massKg: 45.350);
                        break;
                    case "35 mm ULD 034 UÇ. SV. T. HD. DRS. ATŞ. MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 1175.0, massKg: 1.570);
                        break;
                    case "35 mm MSD 020 UÇ. SV. TAHRİP YANGIN MÜHİMMATI":
                        simForm.ApplyAmmoDefaults(v0: 1175.0, massKg: 1.570);
                        break;
                    case "40 mm BOMBA ATAR MÜHİMMATI (MKE MOD60 HE)":
                        simForm.ApplyAmmoDefaults(v0: 76.0, massKg: 0.230);
                        break;
                    case "25 mm M791 APDS-T MERMİSİ":
                        simForm.ApplyAmmoDefaults(v0: 1345.0, massKg: 0.455);
                        break;
                    case "25 mm M793 TP-T MERMİSİ":
                        simForm.ApplyAmmoDefaults(v0: 1100.0, massKg: 0.492);
                        break;
                    case "25 mm M792 HEI-T MERMİSİ":
                        simForm.ApplyAmmoDefaults(v0: 1100.0, massKg: 0.493);
                        break;
                }

                if (simForm.ShowDialog(this) == DialogResult.OK)
                {
                    var v0Val = simForm.BaslangicHizi;
                    var angle = simForm.AtisAcisi;
                    var mass = simForm.MermiKutlesi;
                    var g = simForm.Yercekimi;
                    var dt = simForm.ZamanAdimi;

                    var msg =
                        "Başlangıç Hızı: " + v0Val + " m/s\r\n" +
                        "Atış Açısı: " + angle + "°\r\n" +
                        "Mermi Kütlesi: " + mass + " kg\r\n" +
                        "Yerçekimi: " + g + " m/s²\r\n" +
                        "Zaman Adımı: " + (dt.HasValue ? (dt.Value + " s") : "(kullanılmıyor)");

                    MessageBox.Show(this, msg, "Simülasyon Parametreleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void lblTitle_Click(object sender, EventArgs e) { }
    }
}
