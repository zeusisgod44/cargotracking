using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargotracking
{
    public partial class kayitekrani : Form
    {
        public kayitekrani()
        {
            InitializeComponent();
        }

        SqlBaglanti bgl = new SqlBaglanti();

        private void btnKayitOl_Click(object sender, EventArgs e)
        {

        }

        private void btnKayitOl_Click_1(object sender, EventArgs e)
        {
            if (txtSifre.Text != txtSifreTekrar.Text)
            {
                MessageBox.Show("Şifreler uyuşmuyor.");
                return;
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, FullName, Email, Password) VALUES (@u, @f, @e, @p)", bgl.baglanti());
            cmd.Parameters.AddWithValue("@u", txtKullaniciAdi.Text);
            cmd.Parameters.AddWithValue("@f", txtAdSoyad.Text);
            cmd.Parameters.AddWithValue("@e", txtMail.Text);
            cmd.Parameters.AddWithValue("@p", txtSifre.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Kayıt başarılı. Giriş ekranına yönlendiriliyorsunuz.");
            girisekrani giris = new girisekrani();
            giris.Show();
            this.Hide();

        }

        private void kayitekrani_Load(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            girisekrani giris = new girisekrani();
            giris.Show();
            this.Hide(); // Bu formu gizle
        }
    }
}
