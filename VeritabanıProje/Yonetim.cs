using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace VeritabanıProje
{
    public partial class Yonetim : Form
    {
        private readonly string connectionString = "User Id=rob;Password=875421;Data Source=localhost:1521/rob";
        public Yonetim()
        {
            InitializeComponent();
            LoadAdisyonForToday();
            LoadMenu();
        }
        private void LoadMenu()
        {
            listBox1.Items.Clear();

            using (OracleConnection conn = new OracleConnection("User Id=rob;Password=875421;Data Source=localhost:1521/rob"))
            {
                conn.Open();

                string query = "SELECT urunid, urunisim, urunfiyat FROM urunler";

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string urun = $"{reader.GetInt32(0)} - {reader.GetString(1)} - {reader.GetDecimal(2):C}";
                            listBox1.Items.Add(urun);
                        }
                    }
                }
            }
        }


        private void Yonetim_Load(object sender, EventArgs e)
        {
            // Tarih seçiciyi bugünün tarihi ile başlat
            dtpTarih.Value = DateTime.Now.Date;

            // Bugünün tarihine göre adisyonları yükle
            LoadAdisyonForToday();
            LoadMenu();
        }

        private void LoadAdisyonForToday()
        {
            DateTime selectedDate = dtpTarih.Value.Date;

            using (OracleConnection conn = new OracleConnection("User Id=rob;Password=875421;Data Source=localhost:1521/rob"))
            {
                conn.Open();

                string query = @"
                SELECT adisyonno, acilis, toplam, durum
                FROM adisyonlistesi
                WHERE TRUNC(acilis) = :selectedDate";

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":selectedDate", OracleDbType.Date).Value = selectedDate;

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dgvAdisyonListesi.DataSource = table;
                    }
                }
            }

            // Verileri yükledikten sonra toplamları hesaplayalım
            CalculateTotals();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Tarih değiştiğinde adisyonları yükle
            LoadAdisyonForToday();
        }

        private void CalculateTotals()
        {
            decimal toplamTutar = 0;
            int toplamDurum = 0;

            foreach (DataGridViewRow row in dgvAdisyonListesi.Rows)
            {
                if (row.Cells["toplam"].Value != DBNull.Value)
                    toplamTutar += Convert.ToDecimal(row.Cells["toplam"].Value);

                if (row.Cells["durum"].Value != DBNull.Value)
                    toplamDurum += Convert.ToInt32(row.Cells["durum"].Value);
            }

            // Durumun toplam tutara oranını hesaplama
            decimal durumOrani = toplamTutar > 0 ? (decimal)toplamDurum / toplamTutar * 100 : 0;

            label1.Text = $"Toplam Ciro: {toplamTutar:C}";
            label3.Text = $"Açık Oranı: {durumOrani:F2} %";
            label2.Text = $"Adisyon Açığı: {toplamDurum:C}";
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedItem = listBox1.SelectedItem.ToString();
                string[] parts = selectedItem.Split('-');
                int urunId = int.Parse(parts[0].Trim());

                DialogResult result = MessageBox.Show(
                    "Bu ürünü silmek istediğinize emin misiniz?",
                    "Ürün Sil",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    DeleteProduct(urunId);
                }
            }

        }
        private void DeleteProduct(int urunId)
        {
            using (OracleConnection conn = new OracleConnection("User Id=rob;Password=875421;Data Source=localhost:1521/rob"))
            {
                conn.Open();

                string query = "DELETE FROM urunler WHERE urunid = :urunId";

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":urunId", OracleDbType.Int32).Value = urunId;

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Ürün başarıyla silindi!");
                        LoadMenu(); // Menü güncellenir
                    }
                    else
                    {
                        MessageBox.Show("Ürün silinemedi veya bulunamadı.");
                    }
                }
            }
            LoadMenu();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Girdi kontrolü
            if (string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out int urunID) ||
                !decimal.TryParse(textBox2.Text, out decimal urunFiyat))
            {
                MessageBox.Show("Lütfen geçerli bir Ürün ID ve Fiyat girin.");
                return;
            }

            string urunIsim = textBox1.Text.Trim();

            // Veritabanına ürün ekleme
            using (OracleConnection conn = new OracleConnection("User Id=rob;Password=875421;Data Source=localhost:1521/rob"))
            {
                conn.Open();

                string query = "INSERT INTO urunler (urunid, urunisim, urunfiyat) VALUES (:urunid, :urunisim, :urunfiyat)";

                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":urunid", OracleDbType.Int32).Value = urunID;
                    cmd.Parameters.Add(":urunisim", OracleDbType.Varchar2).Value = urunIsim;
                    cmd.Parameters.Add(":urunfiyat", OracleDbType.Decimal).Value = urunFiyat;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Ürün başarıyla eklendi!");

                        // Menüyü güncelle
                        LoadMenu();

                        // TextBox'ları temizle
                        textBox3.Clear();
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    catch (OracleException ex)
                    {
                        MessageBox.Show($"Veritabanı hatası: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Bilinmeyen bir hata oluştu: {ex.Message}");
                    }
                }
            }
            LoadMenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Veritabanını sıfırlamak istediğinizden emin misiniz? Bu işlem geri alınamaz!",
                "Veritabanı Sıfırlama",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (OracleConnection conn = new OracleConnection("User Id=rob;Password=875421;Data Source=localhost:1521/rob"))
                    {
                        conn.Open();

                        // Adisyon tablosunu güncelle
                        string updateAdisyonQuery = @"
                    UPDATE adisyon
                    SET adisyonacilis = NULL, adisyontoplam = NULL, adisyonindirim = 0, odeme = 0, adisyondurum = 0";

                        using (OracleCommand cmdUpdate = new OracleCommand(updateAdisyonQuery, conn))
                        {
                            cmdUpdate.ExecuteNonQuery();
                        }

                        // Masa_urunleri tablosunu temizle
                        string truncateMasaUrunleriQuery = "TRUNCATE TABLE masa_urunleri";

                        using (OracleCommand cmdTruncateMasaUrunleri = new OracleCommand(truncateMasaUrunleriQuery, conn))
                        {
                            cmdTruncateMasaUrunleri.ExecuteNonQuery();
                        }

                        // Adisyonlistesi tablosunu temizle
                        string truncateAdisyonListesiQuery = "TRUNCATE TABLE adisyonlistesi";

                        using (OracleCommand cmdTruncateAdisyonListesi = new OracleCommand(truncateAdisyonListesiQuery, conn))
                        {
                            cmdTruncateAdisyonListesi.ExecuteNonQuery();
                        }

                        MessageBox.Show("Veritabanı başarıyla sıfırlandı.");
                    }
                }
                catch (OracleException ex)
                {
                    MessageBox.Show($"Veritabanı hatası: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bilinmeyen bir hata oluştu: {ex.Message}");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OracleConnection conn = new OracleConnection("User Id=rob;Password=875421;Data Source=localhost:1521/rob"))
            {
                try
                {
                    conn.Open();

                    // Maksimum masa numarasını al
                    string getMaxMasaNoQuery = "SELECT NVL(MAX(masano), 0) FROM adisyon";

                    int maxMasaNo;
                    using (OracleCommand cmdGetMax = new OracleCommand(getMaxMasaNoQuery, conn))
                    {
                        maxMasaNo = Convert.ToInt32(cmdGetMax.ExecuteScalar());
                    }

                    // Yeni masa numarası belirle
                    int newMasaNo = maxMasaNo + 1;

                    // Yeni masayı ekle
                    string insertMasaQuery = @"
                INSERT INTO adisyon (masano, adisyonacilis, adisyontoplam, adisyonindirim, adisyondurum, odeme)
                VALUES (:masano, NULL, NULL, 0, 0, 0)";

                    using (OracleCommand cmdInsert = new OracleCommand(insertMasaQuery, conn))
                    {
                        cmdInsert.Parameters.Add(":masano", OracleDbType.Int32).Value = newMasaNo;
                        cmdInsert.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Masa {newMasaNo} başarıyla eklendi!");
                }
                catch (OracleException ex)
                {
                    MessageBox.Show($"Veritabanı hatası: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (OracleConnection conn = new OracleConnection("User Id=rob;Password=875421;Data Source=localhost:1521/rob"))
            {
                try
                {
                    conn.Open();

                    // Maksimum masa numarasını al
                    string getMaxMasaNoQuery = "SELECT NVL(MAX(masano), 0) FROM adisyon";

                    int maxMasaNo;
                    using (OracleCommand cmdGetMax = new OracleCommand(getMaxMasaNoQuery, conn))
                    {
                        maxMasaNo = Convert.ToInt32(cmdGetMax.ExecuteScalar());
                    }

                    if (maxMasaNo > 0)
                    {
                        // Maksimum masayı sil
                        string deleteMasaQuery = "DELETE FROM adisyon WHERE masano = :masano";

                        using (OracleCommand cmdDelete = new OracleCommand(deleteMasaQuery, conn))
                        {
                            cmdDelete.Parameters.Add(":masano", OracleDbType.Int32).Value = maxMasaNo;
                            int rowsAffected = cmdDelete.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"Masa {maxMasaNo} başarıyla silindi!");
                            }
                            else
                            {
                                MessageBox.Show("Masa silinemedi.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Silinecek bir masa bulunamadı.");
                    }
                }
                catch (OracleException ex)
                {
                    MessageBox.Show($"Veritabanı hatası: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Personel personel = new Personel();
            personel.Show();
            this.Hide();
        }
        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz ürünü seçin.");
                return;
            }

            // ListBox'tan seçilen ürün bilgilerini al
            string selectedUrun = listBox1.SelectedItem.ToString();
            int urunID = Convert.ToInt32(selectedUrun.Split('-')[0].Trim());

            // TextBox'lardaki değerleri kontrol et
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
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
                UPDATE urunler
                SET urunisim = :urunisim,
                    urunfiyat = :urunfiyat
                WHERE urunid = :urunid";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        // Parametreleri ekle
                        cmd.Parameters.Add(":urunisim", OracleDbType.Varchar2).Value = textBox1.Text.Trim();
                        cmd.Parameters.Add(":urunfiyat", OracleDbType.Decimal).Value = Convert.ToDecimal(textBox2.Text.Trim());
                        cmd.Parameters.Add(":urunid", OracleDbType.Int32).Value = urunID;

                        // Sorguyu çalıştır
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Ürün başarıyla güncellendi!");
                            LoadMenu();
                            ClearTextBoxes();
                        }
                        else
                        {
                            MessageBox.Show("Ürün güncellenemedi! Kayıt bulunamadı.");
                        }
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

        
    }
}
