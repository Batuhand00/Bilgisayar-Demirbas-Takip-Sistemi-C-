using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Bitirme_PROJESİ
{
    public partial class frmGenelDonanım : Form
    {
        MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=bilgisayardemisbas;user=root;Convert Zero Datetime=True;");



        public frmGenelDonanım(string kullanici)
        {
            InitializeComponent();
            
        }

        private void tabGenel_Click(object sender, EventArgs e)
        {


            baglanti.Open();

            MySqlCommand komut = new MySqlCommand("SELECT * FROM genel LIMIT 1", baglanti);
            MySqlDataReader okuyucu = komut.ExecuteReader();

            if (okuyucu.Read())
            {
                txtAd.Text = okuyucu["Ad"].ToString();
                txtSoyad.Text = okuyucu["Soyad"].ToString();
                txtSicilNo.Text = okuyucu["SicilNo"].ToString();
                cmbUnvan.Text = okuyucu["Unvan"].ToString();
                cmbBolum.Text = okuyucu["Bolum"].ToString();
                txtEposta.Text = okuyucu["Eposta"].ToString();
                txtOdaNumarası.Text = okuyucu["OdaNumarası"].ToString();
                txtTarih.Text = okuyucu["isbaslamatarih"].ToString();
            }

            okuyucu.Close(); 

            MySqlCommand komut2 = new MySqlCommand("SELECT * FROM resim LIMIT 1", baglanti);
            MySqlDataReader okuyucu2 = komut2.ExecuteReader();

            if (okuyucu2.Read())
            {
                byte[] resim = (byte[])okuyucu2["resim"];
                using (MemoryStream ms = new MemoryStream(resim))
                {
                    pcbFoto.Image = Image.FromStream(ms);
                }
            }

            okuyucu2.Close();
            baglanti.Close();



        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();
                MySqlCommand komut = new MySqlCommand();
                komut.CommandText = "UPDATE genel SET Ad=@pAd, Soyad=@pSoyad, Unvan=@pUnvan, Bolum=@pBolum, " +
                    "Eposta=@pEposta, OdaNumarası=@pOdaNumarası, isbaslamatarih=@pisbaslamatarih WHERE SicilNo=@pSicilNo";
                komut.Parameters.AddWithValue("@pAd", txtAd.Text);
                komut.Parameters.AddWithValue("@pSoyad", txtSoyad.Text);
                komut.Parameters.AddWithValue("@pUnvan", cmbUnvan.Text);
                komut.Parameters.AddWithValue("@pBolum", cmbBolum.Text);
                komut.Parameters.AddWithValue("@pEposta", txtEposta.Text);
                komut.Parameters.AddWithValue("@pOdaNumarası", txtOdaNumarası.Text);
                komut.Parameters.AddWithValue("@pisbaslamatarih", txtTarih.Text);
                komut.Parameters.AddWithValue("@pSicilNo", txtSicilNo.Text);
                komut.Connection = baglanti;
                komut.ExecuteNonQuery();
                MessageBox.Show("Başarılı Şekilde Kaydedildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme işlemi sırasında bir hata oluştu: " + ex.Message);
            }
            baglanti.Close();

        }

        private void tabDonanım_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            MySqlCommand komut = new MySqlCommand("SELECT*FROM donanım LIMIT 2", baglanti);
            MySqlDataReader okuyucu = komut.ExecuteReader();

            int i = 0;
            while (okuyucu.Read() && i < 2)
            {
                if (i == 0)
                {
                    txtMarka1.Text = okuyucu["marka"].ToString();
                    txtModel1.Text =okuyucu["model"].ToString();
                    txtAciklama1.Text = okuyucu["aciklama"].ToString();
                    txtTarih1.Text = okuyucu["verildigitarih"].ToString();
                    
                }
                else if (i == 1)
                {
                    txtMarka2.Text = okuyucu["marka"].ToString();
                   
                    txtModel2.Text = okuyucu["model"].ToString();
                    txtAciklama2.Text = okuyucu["aciklama"].ToString();
                    txtTarih2.Text = okuyucu["verildigitarih"].ToString();
                }
                i++;
            }

            okuyucu.Close();
            baglanti.Close();
        }

        private void grbİlk_Enter(object sender, EventArgs e)
        {

        }

        private void btnKasaBilgi_Click(object sender, EventArgs e)
        {
            frmKasaBilgileri Kasa = new frmKasaBilgileri();
            Kasa.ShowDialog();
        }

        private void btnSil1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Silmek İstediğinize emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cevap == DialogResult.Yes)
                {
                    MySqlCommand komut = new MySqlCommand();
                    komut.CommandText = "DELETE FROM donanım WHERE Marka=@pmarka";
                    komut.Parameters.AddWithValue("@pmarka", txtMarka1.Text);
                    komut.Connection = baglanti;
                    MessageBox.Show("Başarılı şekilde silindi");
                    komut.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme işlemi sırasında bir hata oluştu: " + ex.Message);
            }
            baglanti.Close();
        }

        private void btnSil2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Silmek İstediğinize emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (cevap == DialogResult.Yes)
                {
                    MySqlCommand komut = new MySqlCommand();
                    komut.CommandText = "DELETE FROM donanım WHERE Marka=@pmarka";
                    komut.Parameters.AddWithValue("@pmarka", txtMarka2.Text);
                    komut.Connection = baglanti;
                    MessageBox.Show("Başarılı şekilde silindi");
                    komut.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme işlemi sırasında bir hata oluştu: " + ex.Message);
            }
            baglanti.Close();
        }
    }
}
