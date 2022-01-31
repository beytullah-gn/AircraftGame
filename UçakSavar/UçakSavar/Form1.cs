using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UçakSavar.Properties;

namespace UçakSavar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PictureBox ucak = new PictureBox();
        PictureBox ucaksavar = new PictureBox();
        
        ArrayList mermilistesi = new ArrayList();
        ArrayList düşmanlistesi = new ArrayList();
        

        private void Form1_Load(object sender, EventArgs e)
        {
            ucaksavar.Image = Resources.ucaksavar;
            this.Controls.Add(ucaksavar);
            ucaksavar.Location = new Point(550, 630);
            timer1.Enabled = true;
            timer3.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UçakOluştur();
            foreach (PictureBox item in düşmanlistesi)
            {
                this.Controls.Add(item);
            }
            uçakhareket();
            
        }

        private void uçakhareket()
        {
            foreach (PictureBox item in düşmanlistesi)
            {
                Point konum = item.Location;
                konum.Y += 15;
                item.Location = konum;


            }
        }

        private void UçakOluştur()
        {
            Random rnd = new Random();
            PictureBox uçak = new PictureBox();
            uçak.Image = Resources.ucak;
            int konum = rnd.Next(0, 1200);
            int konum2 = rnd.Next(0, 15);
            uçak.Location = new Point(konum, konum2);
            düşmanlistesi.Add(uçak);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Point konum = ucaksavar.Location;
            if(e.KeyCode  ==Keys.A && (ucaksavar.Location.X>0 && ucaksavar.Location.X <1200) )
            {
                konum.X -= 8;
                ucaksavar.Location = konum;
            }
            if (e.KeyCode == Keys.D && (ucaksavar.Location.X > 0 && ucaksavar.Location.X < 1200))
            {
                konum.X += 8;
                ucaksavar.Location = konum;
            }
            if (e.KeyCode == Keys.Space )
            {
                BombaÜret();
                timer2.Enabled = true;
                

            }
        }

        private void BombaÜret()
        {
            PictureBox mermi = new PictureBox();
            mermi.Image = Resources.mermi1;
            mermi.Location = ucaksavar.Location;
            mermilistesi.Add(mermi);

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
            foreach (PictureBox item in mermilistesi)
            {
                this.Controls.Add(item);
            }

            BombaHareket();
            

        }

        private void BombaHareket()
        {
            foreach (PictureBox item in mermilistesi)
            {
                Point konum = item.Location;
                konum.Y -= 5;
                item.Location = konum;
            }
        }

        int skor = 0;
        PictureBox kaldirilanucaklar = new PictureBox();
        PictureBox kaldirilanmermiler = new PictureBox();
        private void timer3_Tick(object sender, EventArgs e)
        {
            foreach (PictureBox item1 in mermilistesi)
            {
                foreach (PictureBox item2 in düşmanlistesi)
                {
                    if(item1.Bounds.IntersectsWith(item2.Bounds))
                    {
                        this.Controls.Remove(item1);
                        this.Controls.Remove(item2);
                        kaldirilanmermiler = item1;
                        kaldirilanucaklar = item2;
                        skor += 1;
                    }
                }
            }
            mermilistesi.Remove(kaldirilanmermiler);
            düşmanlistesi.Remove(kaldirilanucaklar);
            Sayı.Text = skor.ToString();
        }
    }
}
