using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
