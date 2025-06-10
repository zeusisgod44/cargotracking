using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargotracking.kargotakip
{
    public partial class ucKargoKart : UserControl
    {
        public string KargoNo { get; set; }
        public string Durum { get; set; }
        public string Tarih { get; set; }

        public ucKargoKart()
        {
            InitializeComponent();
        }

        private void ucKargoKart_Load(object sender, EventArgs e)
        {
            lblKargoNo.Text = KargoNo;
            lblDurum.Text = Durum;
            lblTarih.Text = Tarih;

            // Duruma göre arka plan rengi
            switch (Durum.ToLower())
            {
                case "yolda":
                    this.BackColor = Color.Orange;
                    break;
                case "teslim edildi":
                    this.BackColor = Color.Green;
                    break;
                case "beklemede":
                    this.BackColor = Color.Gray;
                    break;
                default:
                    this.BackColor = Color.LightBlue;
                    break;
            }
        }
    }
}
