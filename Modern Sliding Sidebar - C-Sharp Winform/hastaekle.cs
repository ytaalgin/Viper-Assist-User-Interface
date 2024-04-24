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
    public partial class hastaekle : Form
    {
        private List<Kisi> kisiler = new List<Kisi>();
        public hastaekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string cihaz = textBox2.Text;
            string ad = textBox3.Text;

            Kisi yeniKisi = new Kisi { Id = id, Cihaz = cihaz, Isim = ad };
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
