using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace VeritabanıProje
{
    partial class Giris
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kullaniciAdi = new System.Windows.Forms.TextBox();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.giris_Btn = new System.Windows.Forms.Button();
            this.kullaniciAd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hataMesaj = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // kullaniciAdi
            // 
            this.kullaniciAdi.Location = new System.Drawing.Point(371, 171);
            this.kullaniciAdi.Name = "kullaniciAdi";
            this.kullaniciAdi.Size = new System.Drawing.Size(362, 20);
            this.kullaniciAdi.TabIndex = 0;
            // 
            // txtSifre
            // 
            this.txtSifre.Location = new System.Drawing.Point(371, 218);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(362, 20);
            this.txtSifre.TabIndex = 1;
            // 
            // giris_Btn
            // 
            this.giris_Btn.Location = new System.Drawing.Point(505, 265);
            this.giris_Btn.Name = "giris_Btn";
            this.giris_Btn.Size = new System.Drawing.Size(63, 20);
            this.giris_Btn.TabIndex = 2;
            this.giris_Btn.Text = "Giriş Yap";
            this.giris_Btn.UseVisualStyleBackColor = true;
            this.giris_Btn.Click += new System.EventHandler(this.giris_Btn_Click_1);
            // 
            // kullaniciAd
            // 
            this.kullaniciAd.AutoSize = true;
            this.kullaniciAd.Location = new System.Drawing.Point(505, 155);
            this.kullaniciAd.Name = "kullaniciAd";
            this.kullaniciAd.Size = new System.Drawing.Size(64, 13);
            this.kullaniciAd.TabIndex = 3;
            this.kullaniciAd.Text = "Kullanıcı Adı";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(522, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Şifre";
            // 
            // hataMesaj
            // 
            this.hataMesaj.AutoSize = true;
            this.hataMesaj.Location = new System.Drawing.Point(522, 338);
            this.hataMesaj.Name = "hataMesaj";
            this.hataMesaj.Size = new System.Drawing.Size(31, 13);
            this.hataMesaj.TabIndex = 5;
            this.hataMesaj.Text = "2024";
            // 
            // Giris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 624);
            this.Controls.Add(this.hataMesaj);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.kullaniciAd);
            this.Controls.Add(this.giris_Btn);
            this.Controls.Add(this.txtSifre);
            this.Controls.Add(this.kullaniciAdi);
            this.Name = "Giris";
            this.Text = "Giris";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox kullaniciAdi;
        private TextBox txtSifre;
        private Button giris_Btn;



        private Label kullaniciAd;
        private Label label1;
        private Label hataMesaj;
    }
}