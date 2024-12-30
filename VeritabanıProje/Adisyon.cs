using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeritabanıProje
{
    public partial class Adisyon : Form
    {
        private readonly string connectionString = "User Id=rob;Password=875421;Data Source=localhost:1521/rob";
        
        private readonly int selectedMasa;
        
        public Adisyon(int masaNo)
        {
            InitializeComponent();
            selectedMasa = masaNo;

            try
            {
                LoadMenu();
                LoadAdisyon(selectedMasa);
                LoadAdisyonDetails(selectedMasa);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization error: {ex.Message}");
            }
        }

        private void LoadMenu()
        {
            listBoxMenu.Items.Clear();
            const string query = "SELECT urunid, urunisim, urunfiyat FROM urunler";

            ExecuteQuery(query, cmd =>
            {
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            int urunId = Convert.ToInt32(reader.GetDecimal(0));
                            string urunIsim = reader.GetString(1);
                            int urunFiyat = Convert.ToInt32(reader.GetDecimal(2));

                            string urun = $"{urunId} - {urunIsim} - {urunFiyat} TL";
                            listBoxMenu.Items.Add(urun);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing menu item: {ex.Message}");
                        }
                    }
                }
            });
        }

        private void LoadAdisyon(int masaNo)
        {
            listBoxAdisyon.Items.Clear();

            const string query = @"SELECT u.urunisim, u.urunfiyat, au.urunadet
                                  FROM masa_urunleri au
                                  JOIN urunler u ON au.urunid = u.urunid
                                  WHERE au.masano = :cmasa";

            try
            {
                ExecuteQuery(query, cmd =>
                {
                    cmd.Parameters.Add(":cmasa", OracleDbType.Int32).Value = masaNo;
                    Console.WriteLine($"Executing query for masano: {masaNo}");

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine($"No items found for masano: {masaNo}");
                            MessageBox.Show("No products found for the selected masa.");
                            return;
                        }

                        while (reader.Read())
                        {
                            try
                            {
                                string urunIsim = reader.GetString(0);
                                int urunFiyat = Convert.ToInt32(reader.GetDecimal(1));
                                int urunAdet = Convert.ToInt32(reader.GetDecimal(2));

                                string display = $"{urunIsim} - {urunFiyat} TL - {urunAdet} Adet";
                                listBoxAdisyon.Items.Add(display);

                                Console.WriteLine($"Loaded Adisyon Item: {display}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error processing adisyon item: {ex.Message}");
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading adisyon: {ex.Message}");
                Console.WriteLine($"Error loading adisyon: {ex.Message}");
            }
        }

        private void ExecuteQuery(string query, Action<OracleCommand> action)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Database connection opened.");

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        using (var transaction = conn.BeginTransaction())
                        {
                            cmd.Transaction = transaction;
                            action(cmd);
                            transaction.Commit();
                            Console.WriteLine("Query executed and committed successfully.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                MessageBox.Show($"Database error: {ex.Message}");
            }
        }

        private void LoadAdisyonDetails(int masaNo)
        {
            const string query = "SELECT NVL(adisyontoplam, 0), NVL(odeme, 0), NVL(adisyonindirim, 0) FROM adisyon WHERE masano = :masano";

            ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.Add(":masano", OracleDbType.Int32).Value = masaNo;

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        try
                        {
                            int toplamTutar = Convert.ToInt32(reader.GetDecimal(0));
                            int odenenTutar = Convert.ToInt32(reader.GetDecimal(1));
                            int indirimTutar = Convert.ToInt32(reader.GetDecimal(2));

                            int kalanTutar = toplamTutar - (odenenTutar + indirimTutar);

                            adisyonToplam.Text = $"Toplam Tutar: {toplamTutar} TL";
                            alinmisOdeme.Text = $"Ödenen Tutar: {odenenTutar} TL";
                            adisyonİndirim.Text = $"İndirim: {indirimTutar} TL";
                            kalan.Text = $"Kalan Tutar: {kalanTutar} TL";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing adisyon details: {ex.Message}");
                        }
                    }
                }
            });
        }

        private async Task AddProductToMasaAsync(int masaNo, int urunId, int urunAdet)
        {
            const string query = @"MERGE INTO masa_urunleri au
                                   USING DUAL
                                   ON (au.masano = :masano AND au.urunid = :urunid)
                                   WHEN MATCHED THEN
                                       UPDATE SET au.urunadet = au.urunadet + :urunadet
                                   WHEN NOT MATCHED THEN
                                       INSERT (masano, urunid, urunadet)
                                       VALUES (:masano, :urunid, :urunadet)";

            try
            {
                await Task.Run(() =>
                {
                    ExecuteQuery(query, cmd =>
                    {
                        cmd.Parameters.Add(":masano", OracleDbType.Int32).Value = masaNo;
                        cmd.Parameters.Add(":urunid", OracleDbType.Int32).Value = urunId;
                        cmd.Parameters.Add(":urunadet", OracleDbType.Int32).Value = urunAdet;
                        cmd.ExecuteNonQuery();
                    });
                });
                MessageBox.Show("Product successfully added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}");
            }
        }

        private async void listBoxMenu_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxMenu.SelectedItem != null)
            {
                string selectedItem = listBoxMenu.SelectedItem.ToString();
                string[] parts = selectedItem.Split('-');
                int urunId = int.Parse(parts[0].Trim());

                string miktarInput = ShowInputDialog("Lütfen ürün miktarını girin:");
                if (int.TryParse(miktarInput, out int urunAdet) && urunAdet > 0)
                {
                    try
                    {
                        await AddProductToMasaAsync(selectedMasa, urunId, urunAdet);
                        LoadAdisyon(selectedMasa);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding product: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Geçersiz miktar girişi!");
                }
            }
            LoadAdisyonDetails(selectedMasa);
        }

        private async Task UrunAdetGuncelleAsync(int masaNo, string urunIsim, int yeniAdet)
        {
            const string query = @"
        UPDATE masa_urunleri
        SET urunadet = :yeniAdet
        WHERE masano = :masano AND urunid = (
            SELECT urunid FROM urunler WHERE urunisim = :urunIsim
        )";

            await Task.Run(() =>
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":yeniAdet", OracleDbType.Int32).Value = yeniAdet;
                        cmd.Parameters.Add(":masano", OracleDbType.Int32).Value = masaNo;
                        cmd.Parameters.Add(":urunIsim", OracleDbType.Varchar2).Value = urunIsim;

                        cmd.ExecuteNonQuery();
                    }
                }
            });
        }



        private async void listBoxAdisyon_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxAdisyon.SelectedItem != null)
            {
                string selectedItem = listBoxAdisyon.SelectedItem.ToString();
                string[] parts = selectedItem.Split('-');
                string urunIsim = parts[0].Trim(); // Ürün ismi
                int urunFiyat = int.Parse(parts[1].Replace("TL", string.Empty).Trim());
                int mevcutAdet = int.Parse(parts[2].Replace("Adet", string.Empty).Trim());

                Form prompt = new Form
                {
                    Width = 300,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "Ürün İşlemleri",
                    StartPosition = FormStartPosition.CenterScreen
                };

                // Adet Güncelleme TextBox
                TextBox txtYeniAdet = new TextBox
                {
                    Left = 50,
                    Top = 20,
                    Width = 200,
                    Text = mevcutAdet.ToString() // Varsayılan olarak mevcut adet
                };

                Button btnAdetGuncelle = new Button
                {
                    Text = "Adet Güncelle",
                    Left = 50,
                    Width = 100,
                    Top = 70,
                    DialogResult = DialogResult.OK
                };

                Button btnIptal = new Button
                {
                    Text = "İptal Et",
                    Left = 160,
                    Width = 100,
                    Top = 70,
                    DialogResult = DialogResult.Cancel
                };

                prompt.Controls.Add(txtYeniAdet);
                prompt.Controls.Add(btnAdetGuncelle);
                prompt.Controls.Add(btnIptal);

                var result = prompt.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Adet Güncelleme
                    if (int.TryParse(txtYeniAdet.Text, out int yeniAdet) && yeniAdet > 0)
                    {
                        await UrunAdetGuncelleAsync(selectedMasa, urunIsim, yeniAdet);
                        MessageBox.Show("Ürün adedi başarıyla güncellendi!");
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz adet girdiniz!");
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    // Ürün Silme
                    await UrunSilAsync(selectedMasa, urunIsim);
                    MessageBox.Show("Ürün iptal edildi!");
                }
            }

            LoadAdisyonDetails(selectedMasa);
            LoadAdisyon(selectedMasa);
        }



        private async Task UrunSilAsync(int masaNo, string urunIsim)
        {
            const string query = @"DELETE FROM masa_urunleri 
                                   WHERE masano = :masano 
                                   AND urunid = (SELECT urunid FROM urunler WHERE urunisim = :urunIsim)";

            await Task.Run(() =>
            {
                ExecuteQuery(query, cmd =>
                {
                    cmd.Parameters.Add(":masano", OracleDbType.Int32).Value = masaNo;
                    cmd.Parameters.Add(":urunIsim", OracleDbType.Varchar2).Value = urunIsim;
                    cmd.ExecuteNonQuery();
                });
            });

            MessageBox.Show("Ürün iptal edildi.");
            LoadAdisyon(masaNo);
            LoadAdisyonDetails(masaNo);
        }


        public static string ShowInputDialog(string text)
        {
            Form prompt = new Form
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Miktar Girin",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label { Left = 10, Top = 20, Text = text, AutoSize = true };
            TextBox inputBox = new TextBox { Left = 10, Top = 50, Width = 250 };
            Button confirmation = new Button { Text = "Tamam", Left = 200, Width = 60, Top = 80, DialogResult = DialogResult.OK };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : null;
        }

        private void indirim_Click(object sender, EventArgs e)
        {
            string discountInput = ShowInputDialog("Lütfen indirim tutarını girin:");

            if (int.TryParse(discountInput, out int discountAmount) && discountAmount > 0)
            {
                AddDiscountToAdisyon(selectedMasa, discountAmount);
            }
            else
            {
                MessageBox.Show("Geçersiz indirim miktarı!");
            }
            LoadAdisyonDetails(selectedMasa);

        }

        private void AddDiscountToAdisyon(int masaNo, int discountAmount)
        {
            const string query = "UPDATE adisyon SET adisyonindirim = adisyonindirim + :discountAmount WHERE masano = :masano";

            try
            {
                ExecuteQuery(query, cmd =>
                {
                    cmd.Parameters.Add(":discountAmount", OracleDbType.Int32).Value = discountAmount;
                    cmd.Parameters.Add(":masano", OracleDbType.Int32).Value = masaNo;
                    cmd.ExecuteNonQuery();
                });

                MessageBox.Show("İndirim başarıyla eklendi.");
                LoadAdisyonDetails(masaNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İndirim eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        private void odeme_Click(object sender, EventArgs e)
        {
            string paymentInput = ShowInputDialog("Lütfen ödeme tutarını girin:");

            if (int.TryParse(paymentInput, out int paymentAmount) && paymentAmount > 0)
            {
                AddPaymentToAdisyon(selectedMasa, paymentAmount);
            }
            else
            {
                MessageBox.Show("Geçersiz ödeme miktarı!");
            }
        }
        private void AddPaymentToAdisyon(int masaNo, int paymentAmount)
        {
            const string query = "UPDATE adisyon SET odeme = odeme + :paymentAmount WHERE masano = :masano";

            try
            {
                ExecuteQuery(query, cmd =>
                {
                    cmd.Parameters.Add(":paymentAmount", OracleDbType.Int32).Value = paymentAmount;
                    cmd.Parameters.Add(":masano", OracleDbType.Int32).Value = masaNo;
                    cmd.ExecuteNonQuery();
                });

                MessageBox.Show("Ödeme başarıyla eklendi.");
                LoadAdisyonDetails(masaNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ödeme eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        private void iptal_Click(object sender, EventArgs e)
        {
            string refInput = ShowInputDialog("Lütfen iptal tutarını girin:");

            if (int.TryParse(refInput, out int refund) && refund > 0)
            {
                CancelPaymentFromAdisyon(selectedMasa, refund);
            }
            else
            {
                MessageBox.Show("Geçersiz rakam");
            }
        }
        private void CancelPaymentFromAdisyon(int masaNo, int paymentAmount)
        {
            const string query = "UPDATE adisyon SET odeme = odeme - :paymentAmount WHERE masano = :masano AND odeme >= :paymentAmount";

            try
            {
                ExecuteQuery(query, cmd =>
                {
                    cmd.Parameters.Add(":paymentAmount", OracleDbType.Int32).Value = paymentAmount;
                    cmd.Parameters.Add(":masano", OracleDbType.Int32).Value = masaNo;
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Ödeme iptali başarıyla gerçekleştirildi.");
                    }
                    else
                    {
                        MessageBox.Show("İptal edilecek ödeme mevcut değil veya ödeme miktarı yetersiz.");
                    }
                });

                LoadAdisyonDetails(masaNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ödeme iptali sırasında bir hata oluştu: {ex.Message}");
            }
        }
        private void kapat_Click(object sender, EventArgs e)
        {
            try
            {
                const string selectQuery = @"
            SELECT adisyonacilis, adisyontoplam, adisyonindirim, odeme
            FROM adisyon
            WHERE masano = :masano";

                ExecuteQuery(selectQuery, cmd =>
                {
                    cmd.Parameters.Add(":masano", OracleDbType.Int32).Value = selectedMasa;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Benzersiz adisyon numarası oluştur
                            string adisyonNo = $"{DateTime.Now:yyyyMMdd_HHmm}_{DateTime.Now.Ticks}_{selectedMasa}";

                            // Veritabanı verilerini al
                            DateTime acilis = !reader.IsDBNull(0) ? reader.GetDateTime(0) : DateTime.MinValue;
                            decimal toplam = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1);
                            decimal indirim = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2);
                            decimal odeme = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);

                            // Kalan tutarı hesapla
                            decimal kalan = toplam - (indirim + odeme);

                            // Adisyonlistesi tablosuna ekleme
                            AddToAdisyonListesi(adisyonNo, acilis, toplam, kalan);

                            // Adisyon tablosundaki satırı temizle
                            ClearAdisyonForMasa(selectedMasa);

                            // Masa_urunleri tablosundaki ilgili kayıtları sil
                            ClearMasaUrunleriForMasa(selectedMasa);
                        }
                        else
                        {
                            MessageBox.Show("Seçili masa için aktif bir adisyon bulunamadı!");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Adisyon kapatma sırasında bir hata oluştu: {ex.Message}");
            }
            // Organizasyon formunu yeniden başlat
            Organizasyon yeniOrganizasyon = new Organizasyon(Global.PersonelIsim);
            yeniOrganizasyon.Show();

            // Mevcut Adisyon formunu tamamen kapat
            this.Close();

        }



        private void AddToAdisyonListesi(string adisyonNo, DateTime acilis, decimal toplam, decimal kalan)
        {
            const string insertQuery = @"
        INSERT INTO adisyonlistesi (adisyonno, acilis, toplam, durum)
        VALUES (:adisyonno, :acilis, :toplama, :duruma)";

            ExecuteQuery(insertQuery, cmd =>
            {
                cmd.Parameters.Add(":adisyonno", OracleDbType.Varchar2).Value = adisyonNo;
                cmd.Parameters.Add(":acilis", OracleDbType.Date).Value = acilis;
                cmd.Parameters.Add(":toplama", OracleDbType.Decimal).Value = toplam;
                cmd.Parameters.Add(":duruma", OracleDbType.Decimal).Value = kalan;
                cmd.ExecuteNonQuery();
            });
        }


        private void ClearMasaUrunleriForMasa(int masaNo)
        {
            const string deleteQuery = "DELETE FROM masa_urunleri WHERE masano = :masano";

            ExecuteQuery(deleteQuery, cmd =>
            {
                cmd.Parameters.Add(":masano", OracleDbType.Int32).Value = masaNo;
                cmd.ExecuteNonQuery();
            });
        }



        private void ClearAdisyonForMasa(int masaNo)
        {
            const string updateQuery = @"
        UPDATE adisyon
        SET adisyonacilis = NULL, adisyontoplam = NULL, adisyonindirim = 0, odeme = 0, adisyondurum = 0
        WHERE masano = :masano";

            ExecuteQuery(updateQuery, cmd =>
            {
                cmd.Parameters.Add(":masano", OracleDbType.Int32).Value = masaNo;
                cmd.ExecuteNonQuery();
            });
        }

        private void geriBtn_Click(object sender, EventArgs e)
        {
          
            // Organizasyon formunu yeniden başlat
            Organizasyon yeniOrganizasyon = new Organizasyon(Global.PersonelIsim);
            yeniOrganizasyon.Show();

            // Mevcut Adisyon formunu tamamen kapat
            this.Close();
        }
    }
}
