using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;// ucKargoKart için namespace
using cargotracking.kargotakip;


namespace cargotracking.kargotakip
{
    public partial class frmKargoTakip : Form

    {
        SqlBaglanti bgl = new SqlBaglanti();

        public frmKargoTakip()
        {
            InitializeComponent();

            // Panel konfigürasyonu (isterseniz designer’da da yapabilirsiniz)
            flowKara.AutoScroll = true;
            flowKara.WrapContents = false;

            flowHava.AutoScroll = true;
            flowHava.WrapContents = false;

            flowGemi.AutoScroll = true;
            flowGemi.WrapContents = false;
        }

        private void kargotakip_Load(object sender, EventArgs e)
        {
            LoadKargoKartlari();

            ucSidebarFix ucSidebarFix1 = new ucSidebarFix();
            ucSidebarFix1.Dock = DockStyle.Left;
            this.Controls.Add(ucSidebarFix1);


        }

        private void LoadKargoKartlari()
        {
            // Şimdilik test verisi kullanıyoruz
            var testVeri = new[]
            {
        new { Tip = "Kara", Id = "K1001", Durum = "Yolda", Tarih = "01.07.2025" },
        new { Tip = "Kara", Id = "K1002", Durum = "Teslim Edildi", Tarih = "28.06.2025" },
                new { Tip = "Kara", Id = "K1002", Durum = "Teslim Edildi", Tarih = "28.06.2025" },
        new { Tip = "Hava", Id = "H2001", Durum = "Yolda", Tarih = "02.07.2025" },
        new { Tip = "Hava", Id = "H2002", Durum = "Beklemede", Tarih = "03.07.2025" },
        new { Tip = "Gemi", Id = "G3001", Durum = "Yolda", Tarih = "05.07.2025" },
        new { Tip = "Gemi", Id = "G3002", Durum = "Teslim Edildi", Tarih = "30.06.2025" },
    };

            foreach (var kayit in testVeri)
            {
                var kart = new ucKargoKart
                {
                    KargoNo = kayit.Id,
                    Durum = kayit.Durum,
                    Tarih = kayit.Tarih
                };

                kart.PerformLayout();

                switch (kayit.Tip)
                {
                    case "Kara":
                        flowKara.Controls.Add(kart);
                        break;
                    case "Hava":
                        flowHava.Controls.Add(kart);
                        break;
                    case "Gemi":
                        flowGemi.Controls.Add(kart);
                        break;
                }
            }
        }

    }
}
