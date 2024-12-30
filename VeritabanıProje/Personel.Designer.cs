namespace VeritabanıProje
{
    partial class Personel
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtPersonelID = new System.Windows.Forms.TextBox();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.txtPersonelRol = new System.Windows.Forms.TextBox();
            this.txtPersonelSoyisim = new System.Windows.Forms.TextBox();
            this.txtPersonelIsim = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(458, 394);
            this.listBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ekle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(713, 209);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Sil";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(713, 369);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Güncelle";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtPersonelID
            // 
            this.txtPersonelID.Location = new System.Drawing.Point(509, 51);
            this.txtPersonelID.Name = "txtPersonelID";
            this.txtPersonelID.Size = new System.Drawing.Size(100, 20);
            this.txtPersonelID.TabIndex = 4;
            this.txtPersonelID.Text = "id";
            // 
            // txtSifre
            // 
            this.txtSifre.Location = new System.Drawing.Point(509, 271);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(100, 20);
            this.txtSifre.TabIndex = 5;
            this.txtSifre.Text = "Şifre";
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.Location = new System.Drawing.Point(509, 223);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(100, 20);
            this.txtKullaniciAdi.TabIndex = 6;
            this.txtKullaniciAdi.Text = "Kullanici Adi";
            // 
            // txtPersonelRol
            // 
            this.txtPersonelRol.Location = new System.Drawing.Point(509, 179);
            this.txtPersonelRol.Name = "txtPersonelRol";
            this.txtPersonelRol.Size = new System.Drawing.Size(100, 20);
            this.txtPersonelRol.TabIndex = 7;
            this.txtPersonelRol.Text = "Rol";
            // 
            // txtPersonelSoyisim
            // 
            this.txtPersonelSoyisim.Location = new System.Drawing.Point(509, 134);
            this.txtPersonelSoyisim.Name = "txtPersonelSoyisim";
            this.txtPersonelSoyisim.Size = new System.Drawing.Size(100, 20);
            this.txtPersonelSoyisim.TabIndex = 8;
            this.txtPersonelSoyisim.Text = "Soyisim";
            // 
            // txtPersonelIsim
            // 
            this.txtPersonelIsim.Location = new System.Drawing.Point(509, 91);
            this.txtPersonelIsim.Name = "txtPersonelIsim";
            this.txtPersonelIsim.Size = new System.Drawing.Size(100, 20);
            this.txtPersonelIsim.TabIndex = 9;
            this.txtPersonelIsim.Text = "İsim";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(617, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(171, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "Çıkış";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Personel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txtPersonelIsim);
            this.Controls.Add(this.txtPersonelSoyisim);
            this.Controls.Add(this.txtPersonelRol);
            this.Controls.Add(this.txtKullaniciAdi);
            this.Controls.Add(this.txtSifre);
            this.Controls.Add(this.txtPersonelID);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Name = "Personel";
            this.Text = "Personel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtPersonelID;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.TextBox txtPersonelRol;
        private System.Windows.Forms.TextBox txtPersonelSoyisim;
        private System.Windows.Forms.TextBox txtPersonelIsim;
        private System.Windows.Forms.Button button4;
    }
}