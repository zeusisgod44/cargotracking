using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace cargotracking.arayüz
{
    public partial class frmResimGuncellePOPUP : Form
    {
        private string kullaniciAdi;
        public Image guncellenenResim { get; private set; }

        SqlBaglanti bgl = new SqlBaglanti();

        public frmResimGuncellePOPUP(string kullaniciAdi)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciAdi;
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                guna2CirclePictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (guna2CirclePictureBox1.Image != null)
            {
                try
                {
                    // Resmi byte dizisine çevir
                    using (MemoryStream ms = new MemoryStream())
                    {
                        guna2CirclePictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] resimBytes = ms.ToArray();

                        // SQL'e güncelle
                        SqlCommand cmd = new SqlCommand("UPDATE dbo.Users SET profilResmi = @r WHERE Username = @k", bgl.baglanti());
                        cmd.Parameters.AddWithValue("@r", resimBytes);
                        cmd.Parameters.AddWithValue("@k", kullaniciAdi);
                        cmd.ExecuteNonQuery();
                        bgl.baglanti().Close();
                    }

                    // Güncellenmiş resmi dashboard tarafında kullanılmak üzere kaydet
                    guncellenenResim = guna2CirclePictureBox1.Image;

                    // Başarı mesajı göster
                    MessageBox.Show("Profil fotoğrafı başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Formu kapat, dashboard'a sonucu bildir
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir resim seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmResimGuncellePOPUP_Load(object sender, EventArgs e)
        {

        }
    }
}
