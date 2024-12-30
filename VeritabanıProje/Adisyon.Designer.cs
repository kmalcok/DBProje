namespace VeritabanıProje
{
    partial class Adisyon
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
            this.listBoxAdisyon = new System.Windows.Forms.ListBox();
            this.listBoxMenu = new System.Windows.Forms.ListBox();
            this.adisyonToplam = new System.Windows.Forms.Label();
            this.adisyonİndirim = new System.Windows.Forms.Label();
            this.alinmisOdeme = new System.Windows.Forms.Label();
            this.kalan = new System.Windows.Forms.Label();
            this.indirim = new System.Windows.Forms.Button();
            this.odeme = new System.Windows.Forms.Button();
            this.kapat = new System.Windows.Forms.Button();
            this.iptal = new System.Windows.Forms.Button();
            this.geriBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxAdisyon
            // 
            this.listBoxAdisyon.FormattingEnabled = true;
            this.listBoxAdisyon.Location = new System.Drawing.Point(12, 12);
            this.listBoxAdisyon.Name = "listBoxAdisyon";
            this.listBoxAdisyon.Size = new System.Drawing.Size(591, 524);
            this.listBoxAdisyon.TabIndex = 0;
            this.listBoxAdisyon.DoubleClick += new System.EventHandler(this.listBoxAdisyon_DoubleClick);
            // 
            // listBoxMenu
            // 
            this.listBoxMenu.FormattingEnabled = true;
            this.listBoxMenu.Location = new System.Drawing.Point(609, 12);
            this.listBoxMenu.Name = "listBoxMenu";
            this.listBoxMenu.Size = new System.Drawing.Size(599, 641);
            this.listBoxMenu.TabIndex = 1;
            this.listBoxMenu.DoubleClick += new System.EventHandler(this.listBoxMenu_DoubleClick);
            // 
            // adisyonToplam
            // 
            this.adisyonToplam.AutoSize = true;
            this.adisyonToplam.Location = new System.Drawing.Point(23, 539);
            this.adisyonToplam.Name = "adisyonToplam";
            this.adisyonToplam.Size = new System.Drawing.Size(35, 13);
            this.adisyonToplam.TabIndex = 2;
            this.adisyonToplam.Text = "label1";
            // 
            // adisyonİndirim
            // 
            this.adisyonİndirim.AutoSize = true;
            this.adisyonİndirim.Location = new System.Drawing.Point(265, 539);
            this.adisyonİndirim.Name = "adisyonİndirim";
            this.adisyonİndirim.Size = new System.Drawing.Size(35, 13);
            this.adisyonİndirim.TabIndex = 3;
            this.adisyonİndirim.Text = "label2";
            // 
            // alinmisOdeme
            // 
            this.alinmisOdeme.AutoSize = true;
            this.alinmisOdeme.Location = new System.Drawing.Point(486, 539);
            this.alinmisOdeme.Name = "alinmisOdeme";
            this.alinmisOdeme.Size = new System.Drawing.Size(35, 13);
            this.alinmisOdeme.TabIndex = 4;
            this.alinmisOdeme.Text = "label3";
            // 
            // kalan
            // 
            this.kalan.AutoSize = true;
            this.kalan.Location = new System.Drawing.Point(23, 582);
            this.kalan.Name = "kalan";
            this.kalan.Size = new System.Drawing.Size(35, 13);
            this.kalan.TabIndex = 5;
            this.kalan.Text = "label1";
            // 
            // indirim
            // 
            this.indirim.Location = new System.Drawing.Point(132, 571);
            this.indirim.Name = "indirim";
            this.indirim.Size = new System.Drawing.Size(208, 35);
            this.indirim.TabIndex = 6;
            this.indirim.Text = "İndirim Ekle";
            this.indirim.UseVisualStyleBackColor = true;
            this.indirim.Click += new System.EventHandler(this.indirim_Click);
            // 
            // odeme
            // 
            this.odeme.Location = new System.Drawing.Point(346, 571);
            this.odeme.Name = "odeme";
            this.odeme.Size = new System.Drawing.Size(162, 35);
            this.odeme.TabIndex = 7;
            this.odeme.Text = "Ödeme Ekle";
            this.odeme.UseVisualStyleBackColor = true;
            this.odeme.Click += new System.EventHandler(this.odeme_Click);
            // 
            // kapat
            // 
            this.kapat.Location = new System.Drawing.Point(12, 614);
            this.kapat.Name = "kapat";
            this.kapat.Size = new System.Drawing.Size(591, 39);
            this.kapat.TabIndex = 8;
            this.kapat.Text = "AdisyonuKapat";
            this.kapat.UseVisualStyleBackColor = true;
            this.kapat.Click += new System.EventHandler(this.kapat_Click);
            // 
            // iptal
            // 
            this.iptal.Location = new System.Drawing.Point(514, 572);
            this.iptal.Name = "iptal";
            this.iptal.Size = new System.Drawing.Size(89, 34);
            this.iptal.TabIndex = 9;
            this.iptal.Text = "Ödeme İptali";
            this.iptal.UseVisualStyleBackColor = true;
            this.iptal.Click += new System.EventHandler(this.iptal_Click);
            // 
            // geriBtn
            // 
            this.geriBtn.Location = new System.Drawing.Point(1214, 12);
            this.geriBtn.Name = "geriBtn";
            this.geriBtn.Size = new System.Drawing.Size(38, 84);
            this.geriBtn.TabIndex = 10;
            this.geriBtn.Text = "Geri";
            this.geriBtn.UseVisualStyleBackColor = true;
            this.geriBtn.Click += new System.EventHandler(this.geriBtn_Click);
            // 
            // Adisyon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.geriBtn);
            this.Controls.Add(this.iptal);
            this.Controls.Add(this.kapat);
            this.Controls.Add(this.odeme);
            this.Controls.Add(this.indirim);
            this.Controls.Add(this.kalan);
            this.Controls.Add(this.alinmisOdeme);
            this.Controls.Add(this.adisyonİndirim);
            this.Controls.Add(this.adisyonToplam);
            this.Controls.Add(this.listBoxMenu);
            this.Controls.Add(this.listBoxAdisyon);
            this.Name = "Adisyon";
            this.Text = "Adisyon";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAdisyon;
        private System.Windows.Forms.ListBox listBoxMenu;
        private System.Windows.Forms.Label adisyonToplam;
        private System.Windows.Forms.Label adisyonİndirim;
        private System.Windows.Forms.Label alinmisOdeme;
        private System.Windows.Forms.Label kalan;
        private System.Windows.Forms.Button indirim;
        private System.Windows.Forms.Button odeme;
        private System.Windows.Forms.Button kapat;
        private System.Windows.Forms.Button iptal;
        private System.Windows.Forms.Button geriBtn;
    }
}