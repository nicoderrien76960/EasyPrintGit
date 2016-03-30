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

namespace EssaiJobImp
{
    public partial class UserControlAR : UserControl
    {
        CurseurTemplate curseur = new CurseurTemplate();
        public UserControlAR()
        {
            InitializeComponent();
            Dictionary<String, String> dicoAR = curseur.chercher("AR");
            tBD1.Text = dicoAR["Dimension1"];
            tBD2.Text = dicoAR["Dimension2"];
            tBD3.Text = dicoAR["Dimension3"];
            tBD4.Text = dicoAR["Dimension4"];
            tBD5.Text = dicoAR["Dimension5"];
            tBD6.Text = dicoAR["Dimension6"];
            tBD7.Text = dicoAR["Dimension7"];
            tBD8.Text = dicoAR["Dimension8"];
            textBox1.Text = ConfigurationManager.AppSettings["CheminLogoABCR"];
            textBox2.Text = ConfigurationManager.AppSettings["CheminPatternHautDroiteBp"];
            textBox3.Text = ConfigurationManager.AppSettings["CheminPatternTableau"];
            textBox4.Text = ConfigurationManager.AppSettings["CheminFilligraneAr"];
            textBox5.Text = ConfigurationManager.AppSettings["CheminPatternTotBl"];
        }
        private void btnValider_Click(object sender, EventArgs e)
        {
            if ((int.Parse(tBD1.Text) + int.Parse(tBD2.Text) + int.Parse(tBD3.Text) + int.Parse(tBD4.Text) + int.Parse(tBD5.Text) + int.Parse(tBD6.Text) + int.Parse(tBD7.Text) + int.Parse(tBD8.Text)) <= 110)
            {
                curseur.modifier("AR", "Dimension1", tBD1.Text);
                curseur.modifier("AR", "Dimension2", tBD2.Text);
                curseur.modifier("AR", "Dimension3", tBD3.Text);
                curseur.modifier("AR", "Dimension4", tBD4.Text);
                curseur.modifier("AR", "Dimension5", tBD5.Text);
                curseur.modifier("AR", "Dimension6", tBD6.Text);
                curseur.modifier("AR", "Dimension7", tBD7.Text);
                curseur.modifier("AR", "Dimension8", tBD8.Text);
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
                    ConfigurationManager.AppSettings["CheminPatternHautDroiteBp"] = textBox2.Text;
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
                    ConfigurationManager.AppSettings["CheminFilligraneBp"] = textBox4.Text;
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
                    ConfigurationManager.AppSettings["CheminPatternTotBl"] = textBox5.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problème à la lecture du fichier (" + ex.Message + ")", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

      
    }
}
