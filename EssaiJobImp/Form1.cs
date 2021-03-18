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
using System.Configuration;
using System.Threading;

namespace Ireport_Rubis
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
      
        /*ND DEBUT 19 10 2015*/
        object sender ; 
        EventArgs e  ;
        
        
        

        /*FIN ND 19 10 2015*/
        /// <summary>
        /// Démarrage du programme ici
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            chargementXML();
            if ("true"== ConfigurationManager.AppSettings["AutoLoad"])
            {
                Timer1_Click(sender, e);
                dossierSpool.Text = "Dossier Spool : "+ConfigurationManager.AppSettings["DossierSpoolIRAM0105"];
                HELP.Text += "Aide au Pramètrage :";
                HELP.Text += "\r\n\r\nLes imprimantes IRAM01, IRAM0101, IRAM0102, IRAM0103, IRAM0104 et IRAM0105, doivent avoir le dossier Spool ci-dessus avec les droits en écriture.";
                HELP.Text += "\r\n\r\nIl faut renseigner le chemin des imprimante LPD dans la base de registre sous :";
                HELP.Text += "\r\n[HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Print\\Printers\\IRAM0xxx]";
                HELP.Text += "\r\n\r\nConfig Imprimante windows : LPT1 -> Généric/Text only -> nom de l'imprimante IRAM0xxxx ";
                HELP.Text += "\r\n\r\n! Ne pas oublier d'installer le service d'impression LPD dans ajout/suppression de programme-> activer des fonctionnalités !";


                HELP.Text += "\r\n\r\nNOTE DE VERSION : ";
                HELP.Text += "\r\n\r\nR180321 : Ajout Prestation Consigne/Déconsigne (remise en état palette) sur AR et BL (Ligne_type=PRE)";
                HELP.Text += "\r\n\r\nR230320 : Ajout du stock théorique dans la colonne localisation ";
                HELP.Text += "\r\n\r\nR190618 : Correction Bug affichage Gratuit dans Devis --> désignation était dans Unité ";
                

                 
            }
        }
        List<string> listeImp = new List<string>();                 //Liste d'imprimante

        /// <summary>
        /// Récupère une liste d'imprimante grace au fichier de configuration correspondant
        /// </summary>
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
        /// <summary>
        /// Boucle infini qui permet la lecture des spools Windows toute les 250ms (paramètre modifiable)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        public void s_myTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan diffTemps = DateTime.Now - dureeF;
            s_myCounter++;

            LABELimprimante1.Text = ("Imprimante LPD1: " + listeImp[0] + " - Cycles : " + s_myCounter) + Environment.NewLine;

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
            LABELimprimante2.Text = ("Imprimante LPD2: " + listeImp[1] + " - Cycles : " + s_myCounter2) + Environment.NewLine;

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

            LABELimprimante3.Text = ("Imprimante LPD3: " + listeImp[2] + " - Cycles : " + s_myCounter3) + Environment.NewLine;

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
            LABELimprimante4.Text = ("Imprimante LPD4: " + listeImp[3] + " - Cycles : " + s_myCounter4) + Environment.NewLine;
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
            LABELimprimante5.Text = ("Imprimante LPD5: " + listeImp[4] + " - Cycles : " + s_myCounter5) + Environment.NewLine;
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
            LABELimprimante6.Text = ("Imprimante LPD6: " + listeImp[5] + " - Cycles : " + s_myCounter6) + Environment.NewLine;
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
           // textBox1.Clear();
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

        private void btnReglage_Click(object sender, EventArgs e)
        {
            Form_reglage frmreglage = new Form_reglage();
            frmreglage.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            s_myTimer.Stop();
            s_myTimer2.Stop();
            s_myTimer3.Stop();
            s_myTimer4.Stop();
            s_myTimer5.Stop();
            s_myTimer6.Stop();
            btnReprise.Visible = true;
        }

        private void btnReprise_Click(object sender, EventArgs e)
        {
            s_myTimer.Start();
            s_myTimer2.Start();
            s_myTimer3.Start();
            s_myTimer4.Start();
            s_myTimer5.Start();
            s_myTimer6.Start();
            btnReprise.Visible = false;
        }
      /*  private void btnStop_Click(object sender, EventArgs e)
        {
          
        }*/

     

        internal Imprimante Imprimante
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Form_reglage Form_reglage
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

       

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show("Mise à Jour le 23/03/20\nNote de version : \nAjout du stock théorique dans la colonne localisation \nDev : ND ");
            MessageBox.Show("Mise à Jour le 18/03/21\nNote de version : \nAjout Prestation Consigne/Déconsigne (remise en état palette) sur AR et BL (Ligne_type=PRE) \nDev : ND ");

        }
    }
}
