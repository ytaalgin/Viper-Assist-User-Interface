using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modern_Sliding_Sidebar___C_Sharp_Winform
{
    public partial class telefonekle : Form
    {
        private List<Kisi> kisiler = new List<Kisi>();
        public telefonekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ad = textBox1.Text;
            string telefon = textBox2.Text;

            Kisi yeniKisi = new Kisi { Ad = ad, Telefon = telefon };
            kisiler.Add(yeniKisi);

            // Diğer formu kontrol et ve veriyi eklemek için metodunu çağır
            Form1 Form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (Form1 != null)
            {
                Form1.VeriEkle(yeniKisi);
            }
        }
    }
}
