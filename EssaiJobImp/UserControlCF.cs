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
    public partial class UserControlCF : UserControl
    {
        CurseurTemplate curseur = new CurseurTemplate();
        public UserControlCF()
        {
            InitializeComponent();
            Dictionary<String, String> dicoCF = curseur.chercher("CF");
            tBD1.Text = dicoCF["Dimension1"];
            tBD2.Text = dicoCF["Dimension2"];
            tBD3.Text = dicoCF["Dimension3"];
            tBD4.Text = dicoCF["Dimension4"];
            tBD5.Text = dicoCF["Dimension5"];
            tBD6.Text = dicoCF["Dimension6"];
            tBD7.Text = dicoCF["Dimension7"];
            tBD8.Text = dicoCF["Dimension8"];
        }
        private void btnValider_Click(object sender, EventArgs e)
        {
            if ((int.Parse(tBD1.Text) + int.Parse(tBD2.Text) + int.Parse(tBD3.Text) + int.Parse(tBD4.Text) + int.Parse(tBD5.Text) + int.Parse(tBD6.Text) + int.Parse(tBD7.Text) + int.Parse(tBD8.Text)) <= 110)
            {
                curseur.modifier("CF", "Dimension1", tBD1.Text);
                curseur.modifier("CF", "Dimension2", tBD2.Text);
                curseur.modifier("CF", "Dimension3", tBD3.Text);
                curseur.modifier("CF", "Dimension4", tBD4.Text);
                curseur.modifier("CF", "Dimension5", tBD5.Text);
                curseur.modifier("CF", "Dimension6", tBD6.Text);
                curseur.modifier("CF", "Dimension7", tBD7.Text);
                curseur.modifier("CF", "Dimension8", tBD8.Text);
                MessageBox.Show("Modification effectuée");
            }
            else { MessageBox.Show("Les valeurs entrée dépasse la somme total de 110ppp"); }
        }

    }
}
