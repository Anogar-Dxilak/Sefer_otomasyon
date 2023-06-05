using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Sefer_otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string mysqlcon = "datasource=localhost;port=3306;user=root;database=yolcu-bilet;password=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlcon);

            try
            {
                mySqlConnection.Open();
                MessageBox.Show("Successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        MySqlConnection db = new MySqlConnection(@"datasource=localhost;port=3306;
        user=root;database=yolcu-bilet;password=");

        void seferlistesi()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From sefer_bilgiler", db);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void yolculistesi()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From yolculuk_bilgileri", db);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        void değiştirilmişyolculistesi()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From yolculuk_bilgileri where seferno = '" + Txtsefernumarası.Text + "'", db);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void SeferNoDegisti()
        {
            
            Btn1.Enabled = true;
            Btn1.BackColor = DefaultBackColor;
            Btn2.Enabled = true;
            Btn2.BackColor = DefaultBackColor;
            

            
            veritabanı_dolu_koltuklar();
        }


        private void veritabanı_dolu_koltuklar()
        {
            db.Open();
            MySqlCommand komut = new MySqlCommand("SELECT koltuk FROM yolculuk_bilgileri WHERE seferno = @seferno", db);
            komut.Parameters.AddWithValue("@seferno", Txtsefernumarası.Text);
            MySqlDataReader read = komut.ExecuteReader();
            
            while (read.Read())
            {
                int koltukno = read.GetInt32("koltuk");
                Button button = Controls.Find("Btn" + koltukno, true).FirstOrDefault() as Button;

                if (button != null)
                {
                    button.Enabled = false;
                    button.BackColor = Color.Red;
                }
            }

            db.Close();
        }

        private void BtnBiletal_Click(object sender, EventArgs e)
        {
            db.Open();
            MySqlCommand komut = new MySqlCommand("insert into kişi_bilgileri (ad,soyad,telefon,tc,cinsiyet,mail)" +
                "values (@p1,@p2,@p3,@p4,@p5,@p6)", db);
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", Msktxtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", Msktxttel.Text);
            komut.Parameters.AddWithValue("@p4", Msktxttc.Text);
            komut.Parameters.AddWithValue("@p5", Cboxcinsiyet.Text);
            komut.Parameters.AddWithValue("@p6", Msktxtmail.Text);
            komut.ExecuteNonQuery();
            db.Close();
            MessageBox.Show("Yolcu Bilgisi Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Btnsfr_Click(object sender, EventArgs e)
        {
            db.Open();
            MySqlCommand komut = new MySqlCommand("insert into şöför (sfrno,AdSoyad,Telefon) values (@p1,@p2,@p3)", db);
            komut.Parameters.AddWithValue("@p1", Txtsoforno.Text);
            komut.Parameters.AddWithValue("@p2", Txtsfradsoyad.Text);
            komut.Parameters.AddWithValue("@p3", Msktxtsfrtel.Text);
            komut.ExecuteNonQuery();
            db.Close();
            MessageBox.Show("Şöför Bilgisi Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.Open();
            MySqlCommand komut = new MySqlCommand("insert into sefer_bilgiler (kalkis,varis,tarih,saat,şöför,fiyat) values (@p1,@p2,@p3,@p4,@p5,@p6)", db);
            komut.Parameters.AddWithValue("@p1", Txtkalkis.Text);
            komut.Parameters.AddWithValue("@p2", Txtvaris.Text);
            komut.Parameters.AddWithValue("@p3", Msktxttarih.Text);
            komut.Parameters.AddWithValue("@p4", Msktxtsaat.Text);
            komut.Parameters.AddWithValue("@p5", Msktxtsofor.Text);
            komut.Parameters.AddWithValue("@p6", Txtfiyat.Text);
            komut.ExecuteNonQuery();
            db.Close();
            MessageBox.Show("Sefer Bilgisi Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            seferlistesi();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            seferlistesi();
            yolculistesi();
            veritabanı_dolu_koltuklar();
            
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtsefernumarası.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
           
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "1";
            
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "2";
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "3";
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "4";
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "5";
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "6";
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "7";
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "8";
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "9";
        }

        private void Btn10_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "10";
        }

        private void Btn11_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "11";
        }

        private void Btn12_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "12";
        }

        private void Btn13_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "13";
        }

        private void Btn14_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "14";
        }

        private void Btn15_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "15";
        }

        private void Btn16_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "16";
        }

        private void Btn17_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "17";
        }

        private void Btn18_Click(object sender, EventArgs e)
        {
            Txtkoltukno.Text = "18";
        }

        private void Btnrezervasyon_Click(object sender, EventArgs e)
        {
            db.Open();
            MySqlCommand komut = new MySqlCommand("insert into yolculuk_bilgileri (seferno,yolcutc,koltuk) values (@p1,@p2,@p3)", db);
            komut.Parameters.AddWithValue("@p1", Txtsefernumarası.Text);
            komut.Parameters.AddWithValue("@p2", Msktxtyolcutc.Text);
            komut.Parameters.AddWithValue("@p3", Txtkoltukno.Text);
            komut.ExecuteNonQuery();
            db.Close();
            MessageBox.Show("Rezarvasyon Bilgisi Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            yolculistesi();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Güncelle_Click(object sender, EventArgs e)
        {
            değiştirilmişyolculistesi();
            SeferNoDegisti();
       
        }
    }
}
