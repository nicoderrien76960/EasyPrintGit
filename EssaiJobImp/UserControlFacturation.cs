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
    public partial class UserControlFacturation : UserControl
    {
        CurseurTemplate curseur = new CurseurTemplate();
        public UserControlFacturation()
        {
            InitializeComponent();
            Dictionary<String, String> dicoAR = curseur.chercher("FACTURE");
            tBD1.Text = dicoAR["Dimension1"];
            tBD2.Text = dicoAR["Dimension2"];
            tBD3.Text = dicoAR["Dimension3"];
            tBD4.Text = dicoAR["Dimension4"];
            tBD5.Text = dicoAR["Dimension5"];
            tBD6.Text = dicoAR["Dimension6"];
            tBD7.Text = dicoAR["Dimension7"];
            tBD8.Text = dicoAR["Dimension8"];
            textBox1.Text = ConfigurationManager.AppSettings["CheminPatternFondPageFacturation"];
            textBox2.Text = ConfigurationManager.AppSettings["CheminPatternEnteteTableauFacture"];
        }
        private void btnValider_Click(object sender, EventArgs e)
        {
            if ((int.Parse(tBD1.Text) + int.Parse(tBD2.Text) + int.Parse(tBD3.Text) + int.Parse(tBD4.Text) + int.Parse(tBD5.Text) + int.Parse(tBD6.Text) + int.Parse(tBD7.Text) + int.Parse(tBD8.Text)) <= 110)
            {
                curseur.modifier("FACTURATION", "Dimension1", tBD1.Text);
                curseur.modifier("FACTURATION", "Dimension2", tBD2.Text);
                curseur.modifier("FACTURATION", "Dimension3", tBD3.Text);
                curseur.modifier("FACTURATION", "Dimension4", tBD4.Text);
                curseur.modifier("FACTURATION", "Dimension5", tBD5.Text);
                curseur.modifier("FACTURATION", "Dimension6", tBD6.Text);
                curseur.modifier("FACTURATION", "Dimension7", tBD7.Text);
                curseur.modifier("FACTURATION", "Dimension8", tBD8.Text);
                MessageBox.Show("Modification effectuée");
            }
            else { MessageBox.Show("Les valeurs entrée dépasse la somme total de 110ppp"); }
        }

        private void label12_Click(object sender, EventArgs e)
        {

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
                    ConfigurationManager.AppSettings["CheminPatternFondPageFacturation"] = textBox1.Text;
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
                    ConfigurationManager.AppSettings["CheminPatternEnteteTableauFacture"] = textBox2.Text;
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
