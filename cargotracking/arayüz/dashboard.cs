using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace cargotracking.arayüz
{
    public partial class dashboard : Form
    {
        public string kullaniciAdi { get; set; }
        public string adSoyad { get; set; }

        SqlBaglanti bgl = new SqlBaglanti();

        public dashboard()
        {
            InitializeComponent();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            ucSidebarFix ucSidebarFix1 = new ucSidebarFix();
            ucSidebarFix1.Dock = DockStyle.Left;
            this.Controls.Add(ucSidebarFix1);

            label1.Text = "Hoş geldin, " + adSoyad;
            label10.Text = kullaniciAdi;

            // NOT YÜKLE
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM Notes ORDER BY UpdatedAt DESC", bgl.baglanti());
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                guna2TextBox1.Text = reader["Message"].ToString();
                label9.Text = "Son güncelleyen: " + reader["LastUpdatedBy"].ToString();
            }
            reader.Close();
            bgl.baglanti().Close();

            // PROFİL RESMİ YÜKLE
            SqlCommand komut = new SqlCommand("SELECT profilResmi FROM dbo.Users WHERE Username = @k", bgl.baglanti());
            komut.Parameters.AddWithValue("@k", kullaniciAdi);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read() && dr["profilResmi"] != DBNull.Value)
            {
                byte[] resimBytes = (byte[])dr["profilResmi"];
                using (MemoryStream ms = new MemoryStream(resimBytes))
                {
                    pictureBoxProfil.Image = Image.FromStream(ms);
                }
            }
            dr.Close();
            bgl.baglanti().Close();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Notes", bgl.baglanti());
            cmd.ExecuteNonQuery();

            SqlCommand ekle = new SqlCommand("INSERT INTO Notes (Message, LastUpdatedBy, UpdatedAt) VALUES (@m, @u, @t)", bgl.baglanti());
            ekle.Parameters.AddWithValue("@m", guna2TextBox1.Text);
            ekle.Parameters.AddWithValue("@u", adSoyad);
            ekle.Parameters.AddWithValue("@t", DateTime.Now);
            ekle.ExecuteNonQuery();
            bgl.baglanti().Close();

            label9.Text = "Son güncelleyen: " + adSoyad;
            MessageBox.Show("Not kaydedildi.");
        }

        private void btnResimDegistir_Click(object sender, EventArgs e)
        {
            frmResimGuncellePOPUP popup = new frmResimGuncellePOPUP(kullaniciAdi);
            if (popup.ShowDialog() == DialogResult.OK)
            {
                pictureBoxProfil.Image = popup.guncellenenResim;
            }
        }
    }
}
