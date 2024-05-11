using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Bitirme_PROJESİ
{
    public partial class frmAna : Form
    {
        MySqlConnection baglanti = new MySqlConnection("Server = localhost; Database=bilgisayardemisbas; user=root");
        public frmAna()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
            string kullanici;
            string sifre;
            kullanici =txtKullanıcıAdı.Text;
            sifre =txtSifre.Text;
            MySqlCommand komut = new MySqlCommand("SELECT * FROM giris WHERE kullanici = @kullanici AND sifre = @sifre", baglanti);
            komut.Parameters.AddWithValue("@kullanici", kullanici);
            komut.Parameters.AddWithValue("@sifre", sifre);

            MySqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
                {
                MessageBox.Show("Giriş başarılı.");
                    frmGenelDonanım genelDonanimForm = new frmGenelDonanım(kullanici);
                    genelDonanimForm.ShowDialog();
                    
                
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış.");
            }


            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            baglanti.Close();
        }
        }
    }

