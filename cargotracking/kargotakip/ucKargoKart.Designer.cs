namespace cargotracking.kargotakip
{
    partial class ucKargoKart
    {
        /// <summary> 
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        /// <summary> 
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.lblKargoNo = new System.Windows.Forms.Label();
            this.lblDurum = new System.Windows.Forms.Label();
            this.lblTarih = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this;
            // 
            // lblKargoNo
            // 
            this.lblKargoNo.AutoSize = true;
            this.lblKargoNo.Location = new System.Drawing.Point(38, 20);
            this.lblKargoNo.Name = "lblKargoNo";
            this.lblKargoNo.Size = new System.Drawing.Size(59, 13);
            this.lblKargoNo.TabIndex = 0;
            this.lblKargoNo.Text = "lblKargoNo";
            // 
            // lblDurum
            // 
            this.lblDurum.AutoSize = true;
            this.lblDurum.Location = new System.Drawing.Point(38, 51);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(48, 13);
            this.lblDurum.TabIndex = 1;
            this.lblDurum.Text = "lblDurum";
            // 
            // lblTarih
            // 
            this.lblTarih.AutoSize = true;
            this.lblTarih.Location = new System.Drawing.Point(38, 86);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(41, 13);
            this.lblTarih.TabIndex = 2;
            this.lblTarih.Text = "lblTarih";
            // 
            // ucKargoKart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTarih);
            this.Controls.Add(this.lblDurum);
            this.Controls.Add(this.lblKargoNo);
            this.Name = "ucKargoKart";
            this.Size = new System.Drawing.Size(230, 115);
            this.Load += new System.EventHandler(this.ucKargoKart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.Label lblDurum;
        private System.Windows.Forms.Label lblKargoNo;
    }
}
