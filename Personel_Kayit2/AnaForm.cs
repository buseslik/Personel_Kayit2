using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Personel_Kayit2
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }
        // server explorer database properties kullanabilirsin
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FOH8AGD;Initial Catalog=Personel;Integrated Security=True");

        void Temizle()
        {
            txtad.Text = "";
            txtsoyad.Text = "";
            combosehir.Text = "";
            maskmaas.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtmeslek.Text = "";
            txtad.Focus();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
          
            this.tbl_PersonelTableAdapter.Fill(this.personelDataSet1.Tbl_Personel);

        }

        private void listele_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelDataSet1.Tbl_Personel);
        }

        private void kaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            //komut nesnesi oluşturuyoruz
            //Insert into TableName (columnName) values (Parameters)

            SqlCommand komut = new SqlCommand("insert into Tbl_Personel(PerAd, PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum) values(@p1,@p2,@p3,@p4,@p5,@p6) ", baglanti);
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", combosehir.Text);
            komut.Parameters.AddWithValue("@p4", maskmaas.Text);
            komut.Parameters.AddWithValue("@p5", txtmeslek.Text);
            komut.Parameters.AddWithValue("@p6", label9.Text);

            //executenonquery tablo işlemlerinde ekleme çıkartma yapılacağı durumlarda kullanılır
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Eklendi");
            

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) 
            {
                label9.Text = "True";
            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if ( radioButton2.Checked == true)
            {
                label9.Text = "False";
            }
            
        }

        private void temizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        //grid kısmında herhangi bir hücreye iki defa tıklandığı zaman
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            combosehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            maskmaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtmeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();


        }

        private void label9_TextChanged(object sender, EventArgs e)
        {
            if (label9.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if (label9.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        private void sil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand(" Delete from Tbl_Personel Where Perid= @k1",baglanti);
            komutsil.Parameters.AddWithValue("@k1", txtid.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi");
        }

        private void guncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_Personel Set PerAd=@a1,PerSoyad=@a2,PerSehir=@a3,PerMaas=@a4,@PerDurum=@a5,@PerMeslek=@a6 where Perid=@a7", baglanti);


            komutguncelle.Parameters.AddWithValue("@a1", txtad.Text);
            komutguncelle.Parameters.AddWithValue("@a2", txtsoyad.Text);
            komutguncelle.Parameters.AddWithValue("@a3", combosehir.Text);
            komutguncelle.Parameters.AddWithValue("@a4", maskmaas.Text);
            komutguncelle.Parameters.AddWithValue("@a5", label9.Text);
            komutguncelle.Parameters.AddWithValue("@a6", txtmeslek.Text);
            komutguncelle.Parameters.AddWithValue("@a7", txtid.Text);

            komutguncelle.ExecuteNonQuery();


            baglanti.Close();
            MessageBox.Show("Personel Bilgi Güncellendi");

        }

        private void txtad_TextChanged(object sender, EventArgs e)
        {

        }

        private void istatistik_Click(object sender, EventArgs e)
        {
            Frmİstatistik fr = new Frmİstatistik();
            fr.Show();
        }

        private void grafikler_Click(object sender, EventArgs e)
        {
           FrmGrafikler fr = new FrmGrafikler();
            fr.Show();
        }
    }
}
