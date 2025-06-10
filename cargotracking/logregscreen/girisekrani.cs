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
using cargotracking.arayüz;

namespace cargotracking
{
    public partial class girisekrani : Form
    {
        public girisekrani()
        {
            InitializeComponent();
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        SqlBaglanti bgl = new SqlBaglanti();
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username=@u AND Password=@p", bgl.baglanti());
            cmd.Parameters.AddWithValue("@u", txtKullaniciAdi.Text);
            cmd.Parameters.AddWithValue("@p", txtSifre.Text);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                dashboard frm = new dashboard();
                frm.kullaniciAdi = dr["Username"].ToString();
                frm.adSoyad = dr["FullName"].ToString();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı veya şifre!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            kayitekrani frm = new kayitekrani();
            frm.Show();
            this.Hide();
        }

        private void girisekrani_Load(object sender, EventArgs e)
        {

        }
    }
}
