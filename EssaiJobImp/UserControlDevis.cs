using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;

namespace Ireport_Rubis
{
    public partial class UserControlDevis : UserControl
    {
        CurseurTemplate curseur = new CurseurTemplate();
        public UserControlDevis()
        {
            InitializeComponent();
            Dictionary<String, String> dicodevis = curseur.chercher("Devis");
            tBD1.Text = dicodevis["Dimension1"];
            tBD2.Text = dicodevis["Dimension2"];
            tBD3.Text = dicodevis["Dimension3"];
            tBD4.Text = dicodevis["Dimension4"];
            tBD5.Text = dicodevis["Dimension5"];
            tBD6.Text = dicodevis["Dimension6"];
            tBD7.Text = dicodevis["Dimension7"];
            tBD8.Text = dicodevis["Dimension8"];
            tBD9.Text = dicodevis["Dimension9"];
            textBox1.Text = ConfigurationManager.AppSettings["CheminLogoABCR_DEVIS"];
            textBox2.Text = ConfigurationManager.AppSettings["CheminPatternHautDroiteDevis"];
            textBox3.Text = ConfigurationManager.AppSettings["CheminPatternTableau"];
            textBox4.Text = ConfigurationManager.AppSettings["CheminFilligraneDevis"];
            textBox5.Text = ConfigurationManager.AppSettings["CheminPatternTot"];

            /*-------------------------------------------------AJOUT V3------------------------------*/
            
            LargeurLogoDevis.Text = ConfigurationManager.AppSettings["LargeurLogoABCR_DEVIS"];
            HauteurLogoDevis.Text = ConfigurationManager.AppSettings["HauteurLogoABCR_DEVIS"];

            RectangleReferenceDevisX.Text = ConfigurationManager.AppSettings["rectangleReferenceDevisX"];
            RectangleReferenceDevisY.Text = ConfigurationManager.AppSettings["rectangleReferenceDevisY"];

            /*-------------------------------------------Fin AJOUT V3 ---------------------------------*/


        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            if ((int.Parse(tBD1.Text) + int.Parse(tBD2.Text) + int.Parse(tBD3.Text) + int.Parse(tBD4.Text) + int.Parse(tBD5.Text) + int.Parse(tBD6.Text) + int.Parse(tBD7.Text) + int.Parse(tBD8.Text) + int.Parse(tBD9.Text)) <= 110)
            {
                curseur.modifier("Devis", "Dimension1", tBD1.Text);
                curseur.modifier("Devis", "Dimension2", tBD2.Text);
                curseur.modifier("Devis", "Dimension3", tBD3.Text);
                curseur.modifier("Devis", "Dimension4", tBD4.Text);
                curseur.modifier("Devis", "Dimension5", tBD5.Text);
                curseur.modifier("Devis", "Dimension6", tBD6.Text);
                curseur.modifier("Devis", "Dimension7", tBD7.Text);
                curseur.modifier("Devis", "Dimension8", tBD8.Text);
                curseur.modifier("Devis", "Dimension9", tBD9.Text);

            /*-------------------------------Ajout V3-----------------------------------*/


                /*------------------modif fichier app.config---------------------*/

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                foreach (XmlElement element in xmlDoc.DocumentElement)
                {
                    if (element.Name.Equals("appSettings"))
                    {
                        foreach (XmlNode node in element.ChildNodes)
                        {
                            try
                            {
                                if (node.Attributes[0].Value.Equals("LargeurLogoABCR_DEVIS"))
                                {
                                    node.Attributes[1].Value = LargeurLogoDevis.Text;
                                }


                                if (node.Attributes[0].Value.Equals("HauteurLogoABCR_DEVIS"))
                                {
                                    node.Attributes[1].Value = HauteurLogoDevis.Text;
                                }
                            
                            
                            
                            
                            
                            
                            
                            
                            }
                            catch { 
                            
                            }
                        }
                    }
                }
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                ConfigurationManager.RefreshSection("appSettings");
                /*------------------------------------------------------------------Fin Ajout V3*/

                /*--------------------------------------------*/










                MessageBox.Show("Modification effectuée");
            }
            else { MessageBox.Show("Les valeurs entrée dépasse la somme total de 110ppp"); }
        }

        private void btnCheminLogo_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath; 
            openFileDialog1.FileName = "Logo";
            openFileDialog1.Filter = "Fichiers image (*.jpg)|*.jpg|Tous les fichiers (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            string nomFichier = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nomFichier = openFileDialog1.FileName;

                try
                {
                    textBox1.Text = nomFichier;
                    ConfigurationManager.AppSettings["CheminLogoABCR"] = textBox1.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problème à la lecture du fichier (" + ex.Message + ")", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath; 
            openFileDialog1.FileName = "Logo";
            openFileDialog1.Filter = "Fichiers image (*.jpg)|*.jpg|Tous les fichiers (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            string nomFichier = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nomFichier = openFileDialog1.FileName;

                try
                {
                    textBox2.Text = nomFichier;
                    ConfigurationManager.AppSettings["CheminPatternHautDroiteDevis"] = textBox2.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problème à la lecture du fichier (" + ex.Message + ")", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath; 
            openFileDialog1.FileName = "Logo";
            openFileDialog1.Filter = "Fichiers image (*.jpg)|*.jpg|Tous les fichiers (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            string nomFichier = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nomFichier = openFileDialog1.FileName;

                try
                {
                    textBox3.Text = nomFichier;
                    ConfigurationManager.AppSettings["CheminPatternTableau"] = textBox3.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problème à la lecture du fichier (" + ex.Message + ")", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath; 
            openFileDialog1.FileName = "Logo";
            openFileDialog1.Filter = "Fichiers image (*.jpg)|*.jpg|Tous les fichiers (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            string nomFichier = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nomFichier = openFileDialog1.FileName;

                try
                {
                    textBox4.Text = nomFichier;
                    ConfigurationManager.AppSettings["CheminFilligraneDevis"] = textBox4.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problème à la lecture du fichier (" + ex.Message + ")", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.ExecutablePath;
            openFileDialog1.FileName = "Logo";
            openFileDialog1.Filter = "Fichiers image (*.jpg)|*.jpg|Tous les fichiers (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            string nomFichier = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nomFichier = openFileDialog1.FileName;

                try
                {
                    textBox5.Text = nomFichier;
                    ConfigurationManager.AppSettings["CheminPatternTot"] = textBox5.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problème à la lecture du fichier (" + ex.Message + ")", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void UserControlDevis_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void LargeurLogoDevis_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
