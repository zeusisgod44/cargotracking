using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cargotracking.Kayit
{
    public partial class kargokayitpp : Form
    {
        private List<string> ulkeler = new List<string>
        {
            "Almanya", "Fransa", "Hollanda", "İngiltere", "İtalya", "İspanya",
            "Polonya", "Romanya", "Bulgaristan", "Yunanistan", "Rusya", "Çin",
            "Hindistan", "Japonya", "Güney Kore", "Birleşik Arap Emirlikleri",
            "Suudi Arabistan", "Katar", "İsrail", "Mısır", "Fas", "Tunus",
            "Güney Afrika", "Kenya", "ABD", "Kanada", "Meksika", "Brezilya",
            "Arjantin", "Avustralya", "Yeni Zelanda", "Azerbaycan", "Gürcistan", "Ukrayna"
        };

        private Dictionary<string, int> ulkeUzaklikFaktorleri = new Dictionary<string, int>
        {
            {"Almanya", 1}, {"Fransa", 1}, {"Hollanda", 1}, {"İngiltere", 1}, {"İtalya", 1},
            {"İspanya", 1}, {"Polonya", 1}, {"Romanya", 1}, {"Bulgaristan", 1}, {"Yunanistan", 1},
            {"Ukrayna", 1}, {"Gürcistan", 1}, {"Azerbaycan", 1},
            {"Rusya", 2}, {"İsrail", 2}, {"Mısır", 2}, {"Fas", 2}, {"Tunus", 2},
            {"Birleşik Arap Emirlikleri", 2}, {"Suudi Arabistan", 2}, {"Katar", 2},
            {"Çin", 3}, {"Hindistan", 3}, {"Japonya", 3}, {"Güney Kore", 3},
            {"Güney Afrika", 4}, {"Kenya", 4}, {"ABD", 4}, {"Kanada", 4}, {"Meksika", 4},
            {"Brezilya", 4}, {"Arjantin", 4},
            {"Avustralya", 5}, {"Yeni Zelanda", 5}
        };

        private Dictionary<string, double> paraBirimleri = new Dictionary<string, double>
        {
            { "USD", 1.0 },         // Ana baz
            { "TRY", 32.00 },       // 1 USD = 32 TL
            { "EUR", 0.92 },        // 1 USD = 0.92 EUR
            { "GBP", 0.80 }         // 1 USD = 0.80 GBP
        };

        public kargokayitpp()
        {
            InitializeComponent();
            FormuHazirla();
        }

        private void FormuHazirla()
        {
            tasitcmbox.Items.Clear();
            tasitcmbox.Items.AddRange(new string[] { "Gemi", "Uçak" });

            birimcmbox2.Items.Clear();
            foreach (var birim in paraBirimleri.Keys)
                birimcmbox2.Items.Add(birim);

            neredentbox.Enabled = false;
            variscmbox.Enabled = false;
            yuktipicmbox.Enabled = false;
            agirliktbox.Enabled = false;
            birimcmbox2.Enabled = false;

            tasitcmbox.SelectedIndexChanged += Tasitcmbox_SelectedIndexChanged;
            neredentbox.SelectedIndexChanged += Rota_Degisti;
            variscmbox.SelectedIndexChanged += Rota_Degisti;
            birimcmbox2.SelectedIndexChanged += BirimDegisti;
            agirliktbox.TextChanged += AgirlikDegisti;
            firmacmbox.SelectedIndexChanged += Firmacmbox_SelectedIndexChanged;
        }

        private void Tasitcmbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            yuktipicmbox.Items.Clear();
            yuktipicmbox.Enabled = true;

            if (tasitcmbox.SelectedItem.ToString() == "Gemi")
            {
                yuktipicmbox.Items.AddRange(new string[]
                {
                    "Konteyner (20ft)", "Konteyner (40ft)", "Dökme Yük",
                    "Tehlikeli Madde", "Soğuk Zincir"
                });
            }
            else if (tasitcmbox.SelectedItem.ToString() == "Uçak")
            {
                yuktipicmbox.Items.AddRange(new string[]
                {
                    "Genel Kargo", "Hassas Elektronik", "Tehlikeli Mal",
                    "Canlı Hayvan", "İlaç/Medikal Ürün"
                });
            }

            neredentbox.Items.Clear();
            variscmbox.Items.Clear();
            neredentbox.Items.AddRange(ulkeler.ToArray());
            variscmbox.Items.AddRange(ulkeler.ToArray());
            neredentbox.Enabled = true;
            variscmbox.Enabled = true;
            birimcmbox2.Enabled = true;
        }

        private void Rota_Degisti(object sender, EventArgs e)
        {
            if (neredentbox.SelectedItem == null || variscmbox.SelectedItem == null)
                return;

            if (neredentbox.SelectedItem.ToString() == variscmbox.SelectedItem.ToString())
            {
                MessageBox.Show("Başlangıç ve varış aynı ülke olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                suretbox.Clear();
                tutartbox.Clear();
                return;
            }

            int f1 = ulkeUzaklikFaktorleri[neredentbox.SelectedItem.ToString()];
            int f2 = ulkeUzaklikFaktorleri[variscmbox.SelectedItem.ToString()];
            int saat = 10 + Math.Abs(f1 - f2) * 12;

            suretbox.Text = saat + " saat";

            agirliktbox.Enabled = true;
        }

        private void AgirlikDegisti(object sender, EventArgs e)
        {
            HesaplaTutar();
        }

        private void BirimDegisti(object sender, EventArgs e)
        {
            HesaplaTutar();
        }

        private void HesaplaTutar()
        {
            // Gerekli alanlar boşsa işlem yapma
            if (string.IsNullOrWhiteSpace(suretbox.Text) ||
                birimcmbox2.SelectedItem == null ||
                string.IsNullOrWhiteSpace(agirliktbox.Text))
            {
                tutartbox.Clear();
                return;
            }

            // Ağırlığı sayıya çevir
            if (!double.TryParse(agirliktbox.Text, out double agirlik))
            {
                tutartbox.Text = "Geçersiz ağırlık";
                return;
            }

            // Süreyi saat olarak al (örneğin: "18 saat" -> 18)
            string saatText = suretbox.Text.Replace(" saat", "").Trim();
            if (!int.TryParse(saatText, out int saat))
            {
                tutartbox.Text = "Geçersiz süre";
                return;
            }

            // USD bazında hesaplama (sabit ücret + süreye ve ağırlığa bağlı değişken ücret)
            double sabitUcretUSD = 500;
            double saatlikUcretUSD = 80;
            double agirlikUcretiUSD = agirlik * 10;

            double toplamTutarUSD = sabitUcretUSD + (saat * saatlikUcretUSD) + agirlikUcretiUSD;

            // Seçilen para biriminin kurunu al
            string secilenBirim = birimcmbox2.SelectedItem.ToString();
            double kur = paraBirimleri[secilenBirim];

            // Nihai tutarı hesapla: USD * Kur
            double toplamYerelTutar = toplamTutarUSD * kur;

            // Formatlayıp TextBox'a yaz
            tutartbox.Text = $"{toplamYerelTutar:N2} {secilenBirim}";
        }

        private void Firmacmbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secilenFirma = firmacmbox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(secilenFirma)) return;

            string connectionString = "Server=DESKTOP-OLJIH3S\\SQLEXPRESS;Database=cargodb;Integrated Security=True;";
            string query = "SELECT Bolge FROM Firmalar WHERE FirmaAdi = @firmaadi";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firmaadi", secilenFirma);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        bolgetbox.Text = reader["Bolge"].ToString();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bölge bilgisi alınırken hata: " + ex.Message);
            }
        }

        private void FirmalariYukle()
        {
            string connectionString = "Server=DESKTOP-OLJIH3S\\SQLEXPRESS;Database=cargodb;Integrated Security=True;";
            string query = "SELECT FirmaAdi FROM Firmalar ORDER BY FirmaAdi";

            firmacmbox.Items.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            firmacmbox.Items.Add(reader["FirmaAdi"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firmalar yüklenirken hata: " + ex.Message);
            }
        }

        private void kargokayitpp_Load(object sender, EventArgs e)
        {
            FirmalariYukle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DESKTOP-OLJIH3S\\SQLEXPRESS;Database=cargodb;Integrated Security=True;";

            string query = @"INSERT INTO KargoKayitlar 
        (FirmaAdi, Bolge, TasitTipi, YukTipi, Nereden, Nereye, TahminiSure, Agirlik, ParaBirimi, Tutar)
        VALUES 
        (@FirmaAdi, @Bolge, @TasitTipi, @YukTipi, @Nereden, @Nereye, @TahminiSure, @Agirlik, @ParaBirimi, @Tutar)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirmaAdi", firmacmbox.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Bolge", bolgetbox.Text);
                    cmd.Parameters.AddWithValue("@TasitTipi", tasitcmbox.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@YukTipi", yuktipicmbox.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Nereden", neredentbox.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Nereye", variscmbox.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@TahminiSure", suretbox.Text);
                    cmd.Parameters.AddWithValue("@Agirlik", Convert.ToDecimal(agirliktbox.Text));
                    cmd.Parameters.AddWithValue("@ParaBirimi", birimcmbox2.SelectedItem?.ToString() ?? "");

                    // "300.50 TL" gibi string'den sadece sayıyı çekelim:
                    string tutarRaw = tutartbox.Text.Split(' ')[0].Replace(".", ","); // nokta-virgül düzeltmesi
                    cmd.Parameters.AddWithValue("@Tutar", Convert.ToDecimal(tutarRaw));

                    conn.Open();
                    int sonuc = cmd.ExecuteNonQuery();

                    if (sonuc > 0)
                        MessageBox.Show("Kayıt başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Kayıt eklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında hata: " + ex.Message);
            }
        }
    }
}