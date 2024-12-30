using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeritabanıProje
{
    public partial class Personel : Form
    {
        private string connectionString = "User Id=rob;Password=875421;Data Source=localhost:1521/rob";

        public Personel()
        {
            InitializeComponent();
            LoadPersonelList();
        }

        // Personel Listesini Yükle
        private void LoadPersonelList()
        {
            listBox1.Items.Clear();

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT personelid, personelisim, personelsoyisim, personelrol FROM personel";

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string personel = $"{reader.GetInt32(0)} - {reader.GetString(1)} {reader.GetString(2)} ({reader.GetString(3)})";
                            listBox1.Items.Add(personel);
                        }
                    }
                }
            }
        }
        private void ClearTextBoxes()
        {
            txtPersonelID.Clear();
            txtPersonelIsim.Clear();
            txtPersonelSoyisim.Clear();
            txtPersonelRol.Clear();
            txtKullaniciAdi.Clear();
            txtSifre.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPersonelID.Text) ||
                string.IsNullOrWhiteSpace(txtPersonelIsim.Text) ||
                string.IsNullOrWhiteSpace(txtPersonelSoyisim.Text) ||
                string.IsNullOrWhiteSpace(txtPersonelRol.Text) ||
                string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) ||
                string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    INSERT INTO personel (personelid, personelisim, personelsoyisim, personelrol, kullaniciadi, sifre)
                    VALUES (:personelid, :personelisim, :personelsoyisim, :personelrol, :kullaniciadi, :sifre)";

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":personelid", OracleDbType.Int32).Value = Convert.ToInt32(txtPersonelID.Text.Trim());
                    cmd.Parameters.Add(":personelisim", OracleDbType.Varchar2).Value = txtPersonelIsim.Text.Trim();
                    cmd.Parameters.Add(":personelsoyisim", OracleDbType.Varchar2).Value = txtPersonelSoyisim.Text.Trim();
                    cmd.Parameters.Add(":personelrol", OracleDbType.Varchar2).Value = txtPersonelRol.Text.Trim();
                    cmd.Parameters.Add(":kullaniciadi", OracleDbType.Varchar2).Value = txtKullaniciAdi.Text.Trim();
                    cmd.Parameters.Add(":sifre", OracleDbType.Varchar2).Value = txtSifre.Text.Trim();

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Personel başarıyla eklendi!");
                    LoadPersonelList();
                    ClearTextBoxes();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz personeli seçin.");
                return;
            }

            string selectedPersonel = listBox1.SelectedItem.ToString();
            int personelID = Convert.ToInt32(selectedPersonel.Split('-')[0].Trim());

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string query = "DELETE FROM personel WHERE personelid = :personelid";

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":personelid", OracleDbType.Int32).Value = personelID;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Personel başarıyla silindi!");
                    LoadPersonelList();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz personeli seçin.");
                return;
            }

            string selectedPersonel = listBox1.SelectedItem.ToString();
            int personelID = Convert.ToInt32(selectedPersonel.Split('-')[0].Trim());

            // TextBox'ların boş olmadığını kontrol et
            if (string.IsNullOrWhiteSpace(txtPersonelIsim.Text) ||
                string.IsNullOrWhiteSpace(txtPersonelSoyisim.Text) ||
                string.IsNullOrWhiteSpace(txtPersonelRol.Text) ||
                string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) ||
                string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                UPDATE personel
                SET personelisim = :personelisim,
                    personelsoyisim = :personelsoyisim,
                    personelrol = :personelrol,
                    kullaniciadi = :kullaniciadi,
                    sifre = :sifre
                WHERE personelid = :personelid";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        // Parametreleri ekle
                        cmd.Parameters.Add(":personelisim", OracleDbType.Varchar2).Value = txtPersonelIsim.Text.Trim();
                        cmd.Parameters.Add(":personelsoyisim", OracleDbType.Varchar2).Value = txtPersonelSoyisim.Text.Trim();
                        cmd.Parameters.Add(":personelrol", OracleDbType.Varchar2).Value = txtPersonelRol.Text.Trim();
                        cmd.Parameters.Add(":kullaniciadi", OracleDbType.Varchar2).Value = txtKullaniciAdi.Text.Trim();
                        cmd.Parameters.Add(":sifre", OracleDbType.Varchar2).Value = txtSifre.Text.Trim();
                        cmd.Parameters.Add(":personelid", OracleDbType.Int32).Value = personelID;

                        // Sorguyu çalıştır
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Personel başarıyla güncellendi!");
                        }
                        else
                        {
                            MessageBox.Show("Personel güncellenemedi! Kayıt bulunamadı.");
                        }
                        LoadPersonelList();
                        ClearTextBoxes();
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show($"Veritabanı hatası: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen bir hata oluştu: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Yönetim ekranına geri dön
            Yonetim yonetimEkrani = new Yonetim();
            yonetimEkrani.Show();

            // Bu formu kapat
            this.Close();
        }
    }
}
