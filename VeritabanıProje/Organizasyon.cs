using Oracle.ManagedDataAccess.Client;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace VeritabanıProje
{
    public partial class Organizasyon : Form
    {
        private readonly string connectionString = "User Id=rob;Password=875421;Data Source=localhost:1521/rob";

        private string personelIsim;

        public Organizasyon(string personelIsim)
        {
            InitializeComponent();
            this.personelIsim = personelIsim;

            lblGarson.Text = $"Garson: {personelIsim}";
            LoadTables(); // Masaları yükle
        }

        private void LoadTables()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Padding = new Padding(10);
            flowLayoutPanel1.Margin = new Padding(5);

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT MASANO, NVL(ADISYONDURUM, 0) AS ADISYONDURUM FROM adisyon";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int masaNo = reader.GetInt32(0);
                                int adisyonDurum = reader.GetInt32(1);

                                Button btnMasa = new Button
                                {
                                    Text = $"Masa {masaNo}",
                                    Width = 300,
                                    Height = 300,
                                    BackColor = adisyonDurum == 1 ? Color.Green : Color.Red,
                                    Tag = masaNo,
                                    Margin = new Padding(10)
                                };

                                btnMasa.Click += MasaButton_Click;

                                flowLayoutPanel1.Controls.Add(btnMasa);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }

        private void MasaButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int masaNo = (int)clickedButton.Tag;

            // Adisyon formunu aç
            Adisyon adisyonForm = new Adisyon(masaNo);
            adisyonForm.Show();

            // Mevcut organizasyon formunu tamamen kapat
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Hide();
        }
    }
}
