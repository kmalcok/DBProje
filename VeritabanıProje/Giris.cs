using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace VeritabanıProje
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void giris_Btn_Click_1(object sender, EventArgs e)
        {
            string kad = kullaniciAdi.Text.Trim();
            string pssw = txtSifre.Text.Trim();

            hataMesaj.Text = string.Empty;
            hataMesaj.Visible = false;

            if (string.IsNullOrEmpty(kad) || string.IsNullOrEmpty(pssw))
            {
                hataMesaj.Text = "Kullanıcı adı ve şifre boş olamaz.";
                hataMesaj.Visible = true;
                return;
            }

            string connectionString = "User Id=rob;Password=875421;Data Source=localhost:1521/rob";

            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("GirisKontrol", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("KAD", OracleDbType.Varchar2, 30).Value = kad;
                        cmd.Parameters.Add("PSSW", OracleDbType.Varchar2, 30).Value = pssw;

                        OracleParameter rolParam = new OracleParameter("ROL", OracleDbType.Varchar2, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(rolParam);

                        OracleParameter isimParam = new OracleParameter("PERSONEL_ISIM", OracleDbType.Varchar2, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(isimParam);

                        cmd.ExecuteNonQuery();

                        string kullaniciTipi = rolParam.Value == DBNull.Value ? null : rolParam.Value.ToString();
                        string personelIsim = isimParam.Value == DBNull.Value ? null : isimParam.Value.ToString();

                        // Kullanıcı doğrulama sonuçlarını işleme
                        if (!string.IsNullOrEmpty(kullaniciTipi))
                        {
                            if (kullaniciTipi == "Yönetici")
                            {
                                MessageBox.Show("Yönetici paneline yönlendiriliyorsunuz.");
                                Yonetim yonetim = new Yonetim();
                                yonetim.Show();
                                this.Hide();
                            }
                            else if (kullaniciTipi == "Çalışan")
                            {
                                Global.PersonelIsim = personelIsim;

                                MessageBox.Show($"Hoşgeldiniz, {personelIsim}");

                                // Organizasyon formuna geçiş yap ve personel ismini aktar
                                Organizasyon organizasyon = new Organizasyon(personelIsim);
                                organizasyon.Show();

                                this.Hide();
                            }
                        }
                        else
                        {
                            hataMesaj.Text = "Kullanıcı adı veya şifre yanlış!";
                            hataMesaj.Visible = true;
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                hataMesaj.Text = $"Veritabanı hatası: {ex.Message}";
                hataMesaj.Visible = true;
            }
            catch (Exception ex)
            {
                hataMesaj.Text = $"Bilinmeyen bir hata oluştu: {ex.Message}";
                hataMesaj.Visible = true;
            }
        }
    }
}
