# Balistik_simulasyon_bilgi
Balistik Simulasyon ve bilgilendirme uygulaması
Projenin amacı
·	Farklı mühimmatlar için 2B balistik uçuşu (trajektori) simüle etmek.
·	Vakum ve hava dirençli senaryoları kıyaslamak, sonuçları grafik, CSV ve HTML rapor olarak dışa aktarmak.
Uygulama mimarisi
·	Form1
·	Mühimmat kataloğu: Dictionary<string,string> içinde isim + teknik özellik metni tutulur.
·	Arama/filtre: Anlık metin araması ile ComboBox içeriği filtrelenir.
·	Detay paneli: Teknik metinleri okunaklı (Consolas) yazı tipiyle gösterir; sağ tık menüsüyle “Tümünü Kopyala” ve “.txt olarak Kaydet”.
·	Simülasyon başlat: Seçili mühimmata ait varsayılan parametreler (v0, kütle, varsa BC) SimulationForm’a aktarılır.
·	SimulationForm
·	Giriş parametreleri: v0, atış açısı, mermi kütlesi, yerçekimi, başlangıç yüksekliği, rüzgar, zaman adımı.
·	Hava direnci modelleri:
·	Cd–A–ρ (klasik aerodinamik sürükleme)
·	Balistik katsayı (BC) sabit (kg/m²)
·	Balistik katsayı (G1/G7) hız-tabanlı tablo modeli
·	Nümerik çözüm: İleri Euler entegrasyonu, yere temas anında lineer interpolasyonla iniş noktasını düzeltme.
·	Çıktılar: Uçuş süresi, menzil, maksimum yükseklik, başlangıç/etki enerjisi.
·	Dışa aktarım: CSV (zaman, x, y, vx, vy), HTML (modern responsive), Yazdır/PDF (PrintDialog).
·	Grafik: Vakum ve hava dirençli eğriler aynı grafikte; eksenler otomatik ölçeklenir, zemin çizgisi eklenir.
·	Ön ayar: Parametreleri XML’e kaydet/yükle (AppData\Roaming\BalistikCalisma\preset.xml).
·	Ağır mühimmat desteği: numMass.Maximum 100 kg’a yükseltilmiştir.
·	TrajectoryChartForm
·	Başlık + iki seri (Vakum / Hava Dirençli) ve zemin çizgisi; yüksek çözünürlüklü PNG olarak dışa kaydetmede de kullanılır.
Temel kavramlar
·	Başlangıç hızı (v0, m/s): Namlu çıkış hızı.
·	Atış açısı (°): Yatay eksene göre açı; 0° yatay, 90° dikey.
·	Mermi kütlesi (m, kg): Simülasyon kütlesi; ağır top/havan için kg mertebesinde olabilir.
·	Yerçekimi (g, m/s²): Dünya için ~9.81; farklı gezegen/koşullar için değiştirilebilir.
·	Başlangıç yüksekliği (h0, m): Atış yapılan irtifa; zemin 0 kabul edilir.
·	Rüzgar (m/s): Yatay bileşen; bağıl hız vrel = v − vrüzgar ile sürükleme hesaplanır.
·	Zaman adımı (dt, s): Entegrasyon çözünürlüğü; çok büyük dt hatayı, çok küçük dt süreyi artırır (0.005–0.02 tipiktir).
·	Hava yoğunluğu (ρ, kg/m³) ve irtifaya bağlı model:
·	Sabit: ρ = ρ0
·	Üstel azalma: ρ(h) = ρ0·exp(−h/H) (H ölçek yüksekliği)
·	Sürükleme (Drag) modelleri:
·	Cd–A–ρ modeli: Fd = 0.5·ρ·Cd·A·|v|·v; Cd boyutsuz, A kesit alanı (m²).
·	BC sabit (kg/m²): Geometriden ayrıştırılmış sürükleme temsili; k adımı ~ 0.5·ρ/BC.
·	BC (G1/G7): Referans mermi tablolarına göre dv/dt = (i·v^m)/BC; hız fps’e çevrilir; yoğunluk ölçeklenir (ρ/1.225).
·	Enerji (J):
·	Başlangıç: ½·m·v0²
·	Etki anı: ½·m·vimpact² (hava direncinde daha düşüktür).
Kullanım akışı
1.	Form1’de mühimmat seçilir; teknik özellikler metni görüntülenir (arama ile filtrelenebilir).
2.	“Simülasyon” tıklanır; seçime göre v0/m (ve varsa BC) otomatik atanmış olarak SimulationForm açılır.
3.	İstenirse rüzgar, ρ modeli, dt ve BC/Cd–A seçilir.
4.	Hesapla: Metin özet alınır; grafik penceresi ile eğriler görüntülenebilir.
5.	Dışa aktar: CSV (örnekler), HTML rapor (parametreler + özet + grafik), Yazdır/PDF.
Projede hazır gelen veriler
·	Tüfek/tabanca (5.56×45, 7.62×51, 9×19 vb.)
·	Ağır kalibre/top/havan (12.7×99, 20×110 HEI, 105 mm, 120 mm, 81 mm, 60 mm, 155 mm HEER vb.)
·	Form1, seçilen mühimmat için uygun varsayılan v0 ve kütleyi kg cinsinden SimulationForm’a gönderir; G1 BC bilinenlerde otomatik BC modu etkinleşir.
Sınırlamalar
·	2B model; yan/ düşey rüzgar sadece yatay bileşen olarak ele alınır.
·	Euler entegrasyonu basit ve hızlıdır; çok uzun menziller ve yüksek Mach aralığında RK4 gibi yöntemler daha doğru sonuç verebilir.
·	BC değerleri yoksa sürükleme çözümü Cd–A–ρ veya vakumla sınırlıdır.
Neler yapılabilir (geliştirme önerileri)
·	Sayısal çözüm: Yarı-implicit Euler, RK4, adaptif dt.
·	Aerodinamik: Cd(Mach) eğrileri, G8 vb. ek referanslar, ses altı/üstü arası geçişleri pürüzsüzleştirme.
·	Harici rüzgar/yoğunluk profili (h fonksiyonu), sıcaklık ve basınçla standart atmosfer.
·	Balistik “zeroing”: Hedef mesafesine göre gerekli atış açısını otomatik bulan buton.
·	DOPE tablosu: Mesafe adımlarına göre düşüş, sürüklenme, hız, enerji.
·	Veri yönetimi: Mühimmatları JSON/XML’den yükleme, kullanıcı ekleme/düzenleme ekranı.
·	Görsel iyileştirme: Grafikte imleç/ölçüm araçları, birden fazla senaryo kıyası.
·	Raporlama: PDF’e doğrudan export; HTML’de çoklu senaryo karşılaştırma tabloları.
İpucu ve bakım
·	Mühimmat adı, Form1’deki switch-case anahtarlarıyla birebir eşleşmelidir (varsayılan atamalar için).
·	Derleme sorunlarında Build > Clean Solution ardından Build > Rebuild Solution uygulayın.
·	Ağır mühimmatlar için numMass.Maximum artırılmıştır; daha yüksek kütle gerekli ise SimulationForm’da bu sınırı büyütmek yeterlidir.

