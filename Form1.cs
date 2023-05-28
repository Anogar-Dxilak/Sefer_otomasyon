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
    }
}
