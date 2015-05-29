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
    public partial class UserControlBL : UserControl
    {
        CurseurTemplate curseur = new CurseurTemplate();
        public UserControlBL()
        {
            InitializeComponent();
            Dictionary<String, String> dicoBL= curseur.chercher("BL");
            tBD1.Text = dicoBL["Dimension1"];
            tBD2.Text = dicoBL["Dimension2"];
            tBD3.Text = dicoBL["Dimension3"];
            tBD4.Text = dicoBL["Dimension4"];
            tBD5.Text = dicoBL["Dimension5"];
            tBD6.Text = dicoBL["Dimension6"];
            tBD7.Text = dicoBL["Dimension7"];
            tBD8.Text = dicoBL["Dimension8"];
        }
        private void btnValider_Click(object sender, EventArgs e)
        {
            if ((int.Parse(tBD1.Text) + int.Parse(tBD2.Text) + int.Parse(tBD3.Text) + int.Parse(tBD4.Text) + int.Parse(tBD5.Text) + int.Parse(tBD6.Text) + int.Parse(tBD7.Text) + int.Parse(tBD8.Text)) <= 110)
            {
                curseur.modifier("BL", "Dimension1", tBD1.Text);
                curseur.modifier("BL", "Dimension2", tBD2.Text);
                curseur.modifier("BL", "Dimension3", tBD3.Text);
                curseur.modifier("BL", "Dimension4", tBD4.Text);
                curseur.modifier("BL", "Dimension5", tBD5.Text);
                curseur.modifier("BL", "Dimension6", tBD6.Text);
                curseur.modifier("BL", "Dimension7", tBD7.Text);
                curseur.modifier("BL", "Dimension8", tBD8.Text);
                MessageBox.Show("Modification effectuée");
            }
            else { MessageBox.Show("Les valeurs entrée dépasse la somme total de 110ppp"); }
        }
    }
}
