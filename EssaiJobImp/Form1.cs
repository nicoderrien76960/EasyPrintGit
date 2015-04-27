using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Printing;
using System.IO;
using System.Xml;
using System.Threading;

namespace EssaiJobImp
{
    public partial class Form1 : Form
    {
        static System.Windows.Forms.Timer s_myTimer = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer s_myTimer2 = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer s_myTimer3 = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer s_myTimer4 = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer s_myTimer5 = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer s_myTimer6 = new System.Windows.Forms.Timer();
        static int s_myCounter = 0;
        static int s_myCounter2 = 0;
        static int s_myCounter3 = 0;
        static int s_myCounter4 = 0;
        static int s_myCounter5 = 0;
        static int s_myCounter6 = 0;
        int timer = 250; DateTime dureeF;
        Imprimante imprimante = new Imprimante();
        public Form1()
        {
            InitializeComponent();
            chargementXML();
        }
        List<string> listeImp = new List<string>();
        public void chargementXML()
        {
            string imp;
            XmlDocument unxml = new XmlDocument();
            try
            {
                unxml.Load("ImprimanteConf.config");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            XmlNode node = unxml.SelectSingleNode("//configuration/Imprimante");
            foreach (XmlNode unNode in node)
            {
                imp = (string)unNode.FirstChild.InnerXml;
                listeImp.Add(imp);
            }
        }
        public void s_myTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan diffTemps = DateTime.Now - dureeF;
            s_myCounter++;
            textBox1.Clear();
            textBox1.Text += ("Timer vaut " + s_myCounter + "   Imprimante: " + listeImp[0]) + Environment.NewLine;
            tBdureeTimer.Clear();
            tBdureeTimer.Text += ("Le Timer fonctionne depuis :" + diffTemps.ToString());
            Imprimante imp1 = (Imprimante)imprimante.Clone();
            imprimante.lectureSpooler(listeImp[0]);
            if (s_myCounter == 10000)
            {
                s_myCounter = 0;
            }
            int tempo = imprimante.getNbDoc();
            if (tempo == 1)
            {
                tempo = int.Parse(tbNbfichier.Text) + tempo;
                tbNbfichier.Clear();
                tbNbfichier.Text = tempo.ToString();
            }
            imprimante.setNbDoc(0);
        }

        public void s_myTimer_Tick2(object sender, EventArgs e)
        {
            s_myCounter2++;
            textBox2.Clear();
            textBox2.Text += ("Timer vaut " + s_myCounter2 + "   Imprimante: " + listeImp[1]) + Environment.NewLine;
            Imprimante imp2 = (Imprimante)imprimante.Clone();
            imprimante.lectureSpooler(listeImp[1]);
            if (s_myCounter2 == 10000)
            {
                s_myCounter2 = 0;
            }
            int tempo = imprimante.getNbDoc();
            if (tempo == 1)
            {
                tempo = int.Parse(tbNbfichier.Text) + tempo;
                tbNbfichier.Clear();
                tbNbfichier.Text = tempo.ToString();
            }
            imprimante.setNbDoc(0);
        }
        public void s_myTimer_Tick3(object sender, EventArgs e)
        {
            s_myCounter3++;
            textBox3.Clear();
            textBox3.Text += ("Timer vaut " + s_myCounter3 + "   Imprimante: " + listeImp[2]) + Environment.NewLine;
            Imprimante imp3 = (Imprimante)imprimante.Clone();
            imprimante.lectureSpooler(listeImp[2]);
            if (s_myCounter3 == 10000)
            {
                s_myCounter3 = 0;
            }
            int tempo = imprimante.getNbDoc();
            if (tempo == 1)
            {
                tempo = int.Parse(tbNbfichier.Text) + tempo;
                tbNbfichier.Clear();
                tbNbfichier.Text = tempo.ToString();
            }
            imprimante.setNbDoc(0);
        }
        public void s_myTimer_Tick4(object sender, EventArgs e)
        {
            s_myCounter4++;
            textBox4.Clear();
            textBox4.Text += ("Timer vaut " + s_myCounter4 + "    Imprimante: " + listeImp[3]) + Environment.NewLine;
            Imprimante imp4 = (Imprimante)imprimante.Clone();
            imprimante.lectureSpooler(listeImp[3]);
            if (s_myCounter4 == 10000)
            {
                s_myCounter4 = 0;
            }
            int tempo = imprimante.getNbDoc();
            if (tempo == 1)
            {
                tempo = int.Parse(tbNbfichier.Text) + tempo;
                tbNbfichier.Clear();
                tbNbfichier.Text = tempo.ToString();
            }
            imprimante.setNbDoc(0);
        }
        public void s_myTimer_Tick5(object sender, EventArgs e)
        {
            s_myCounter5++;
            textBox5.Clear();
            textBox5.Text += ("Timer vaut " + s_myCounter5 + "    Imprimante: " + listeImp[4]) + Environment.NewLine;
            imprimante.lectureSpooler(listeImp[4]);
            if (s_myCounter5 == 10000)
            {
                s_myCounter5 = 0;
            }
            int tempo = imprimante.getNbDoc();
            if (tempo == 1)
            {
                tempo = int.Parse(tbNbfichier.Text) + tempo;
                tbNbfichier.Clear();
                tbNbfichier.Text = tempo.ToString();
            }
            imprimante.setNbDoc(0);
        }
        public void s_myTimer_Tick6(object sender, EventArgs e)
        {
            s_myCounter6++;
            textBox6.Clear();
            textBox6.Text += ("Timer vaut " + s_myCounter6 + "    Imprimante: " + listeImp[5]) + Environment.NewLine;
            imprimante.lectureSpooler(listeImp[5]);
            if (s_myCounter6 == 10000)
            {
                s_myCounter6 = 0;
            }
            int tempo = imprimante.getNbDoc();
            if (tempo == 1)
            {
                tempo = int.Parse(tbNbfichier.Text) + tempo;
                tbNbfichier.Clear();
                tbNbfichier.Text = tempo.ToString();
            }
            imprimante.setNbDoc(0);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void boutonLectureJobs_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
        private void boutonLectureSpooler_Click(object sender, EventArgs e)
        {
            //lectureSpooler(nomIMP);
        }
        private void boutonSupJob_Click(object sender, EventArgs e)
        {
        }
        private void Timer1_Click(object sender, EventArgs e)
        {
            dureeF = DateTime.Now;
            s_myTimer.Tick += new EventHandler(s_myTimer_Tick);
            s_myTimer.Interval = timer;
            s_myTimer.Start();
            s_myTimer2.Interval = timer;
            timer2(sender, e);
            s_myTimer2.Start();
            s_myTimer3.Interval = timer;
            timer3(sender, e);
            s_myTimer3.Start();
            s_myTimer4.Interval = timer;
            timer4(sender, e);
            s_myTimer4.Start();
            s_myTimer5.Interval = timer;
            timer5(sender, e);
            s_myTimer5.Start();
            s_myTimer6.Interval = timer;
            timer6(sender, e);
            s_myTimer6.Start();
            //MessageBox.Show("Timer lancé.");
        }
        private void timer2(object sender, EventArgs e)
        {
            s_myTimer2.Tick += new EventHandler(s_myTimer_Tick2);
        }
        private void timer3(object sender, EventArgs e)
        {
            s_myTimer3.Tick += new EventHandler(s_myTimer_Tick3);
        }
        private void timer4(object sender, EventArgs e)
        {
            s_myTimer4.Tick += new EventHandler(s_myTimer_Tick4);
        }
        private void timer5(object sender, EventArgs e)
        {
            s_myTimer5.Tick += new EventHandler(s_myTimer_Tick5);
        }
        private void timer6(object sender, EventArgs e)
        {
            s_myTimer6.Tick += new EventHandler(s_myTimer_Tick6);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult réponse = MessageBox.Show("Voulez-vous vraiment quitter l'application ?", "Fermeture", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (réponse == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void tbNbfichier_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
