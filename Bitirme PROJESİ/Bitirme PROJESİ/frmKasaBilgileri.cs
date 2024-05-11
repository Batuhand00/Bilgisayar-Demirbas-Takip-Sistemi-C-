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

namespace Bitirme_PROJESİ
{
    public partial class frmKasaBilgileri : Form
    {
        MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=bilgisayardemisbas;user=root;Convert Zero Datetime=True;");
        public frmKasaBilgileri()
        {
            InitializeComponent();
        }

        private void frmKasaBilgileri_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("SELECT * FROM kasabilgi LIMIT 1", baglanti);
            MySqlDataReader okuyucu = komut.ExecuteReader();    
            if(okuyucu.Read())
            {
                txtKasa.Text = okuyucu["kasademirbasno"].ToString();
                txtCalisanSicilNo.Text= okuyucu["sicilno"].ToString();
                txtİsletimSistem.Text = okuyucu["isletimsistemi"].ToString();
                txtİslemciModel.Text = okuyucu["islemcimarka"].ToString() ;
                txtRam.Text = okuyucu["ram"].ToString(); ;
                txtSabitDisk.Text = okuyucu["sabitdisk"].ToString();;
                txtEkranKartı.Text = okuyucu["ekrankartı"].ToString(); ;
                txtPCModel.Text = okuyucu["pcmodel"].ToString(); ;
                txtİslemciHizi.Text = okuyucu["islemcihiz"].ToString(); ;
                txtCekirdekSayisi.Text = okuyucu["cekirdeksayisi"].ToString(); ;
                txtMonitor.Text = okuyucu["monitorboyut"].ToString(); ;
                
            }
            okuyucu.Close();
            baglanti.Close();   
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                MySqlCommand komut = new MySqlCommand();
                komut.CommandText = "UPDATE kasabilgi SET Kasa=@pkasademirbasno,İsletimSistem=@pisletimsistemi,İslemciModel=@pİslemciModel," +
                    "Ram=@pram,SabitDisk=@psabitdisk,EkranKartı=@pekrankartı,PCModel=@ppcmodel,İslemcihiz=@pislemcihiz,CekirdekSayisi=@pcekirdeksayisi," +
                    "Monitor=@pmonitorboyut WHERE CalisanSicilNo =@psicilno";
                komut.Parameters.AddWithValue("@pkasademirbasno",txtKasa.Text);
                komut.Parameters.AddWithValue("@pisletimsistemi", txtİsletimSistem.Text);
                komut.Parameters.AddWithValue("@pİslemciModel",txtİslemciModel.Text);
                komut.Parameters.AddWithValue("@pram", txtRam.Text);
                komut.Parameters.AddWithValue("@psabitdisk",txtSabitDisk.Text);
                komut.Parameters.AddWithValue("@pekrankartı",txtEkranKartı.Text);
                komut.Parameters.AddWithValue("@ppcmodel",txtPCModel.Text);
                komut.Parameters.AddWithValue("@pislemcihiz",txtİslemciHizi.Text);
                komut.Parameters.AddWithValue("@pcekirdeksayisi",txtCekirdekSayisi.Text);
                komut.Parameters.AddWithValue("@pmonitorboyut", txtMonitor.Text);
                komut.Parameters.AddWithValue("@psicilno",txtCalisanSicilNo.Text);
                komut.Connection = baglanti;
                komut.ExecuteNonQuery();
                MessageBox.Show("Başarılı Şekilde Kaydedildi.");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme işlemi sırasında bir hata oluştu: " + ex.Message);
            }baglanti.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Silmek İstediğinize emin misiniz?", "Uyarı!",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (cevap == DialogResult.Yes)
                {
                    MySqlCommand komut = new MySqlCommand();
                    komut.CommandText = "DELETE FROM kasabilgi WHERE CalisanSicilNo=@psicilno";
                    komut.Parameters.AddWithValue("@psicilno",txtCalisanSicilNo);
                    komut.Connection= baglanti;
                    MessageBox.Show("Başarılı şekilde silindi");
                    komut.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme işlemi sırasında bir hata oluştu: " + ex.Message);
            } baglanti.Close();
           
            

        }
    }
}
