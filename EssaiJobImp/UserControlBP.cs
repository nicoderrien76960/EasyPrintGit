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
    public partial class UserControlBP : UserControl
    {
        CurseurTemplate curseur = new CurseurTemplate();
        public UserControlBP()
        {
            InitializeComponent();
            Dictionary<String, String> dicoBP = curseur.chercher("BP");
            tBD1.Text = dicoBP["Dimension1"];
            tBD2.Text = dicoBP["Dimension2"];
            tBD3.Text = dicoBP["Dimension3"];
            tBD4.Text = dicoBP["Dimension4"];
            tBD5.Text = dicoBP["Dimension5"];
        }
        private void btnValider_Click(object sender, EventArgs e)
        {
            if ((int.Parse(tBD1.Text) + int.Parse(tBD2.Text) + int.Parse(tBD3.Text) + int.Parse(tBD4.Text) + int.Parse(tBD5.Text)) <= 110)
            {
                curseur.modifier("BP", "Dimension1", tBD1.Text);
                curseur.modifier("BP", "Dimension2", tBD2.Text);
                curseur.modifier("BP", "Dimension3", tBD3.Text);
                curseur.modifier("BP", "Dimension4", tBD4.Text);
                curseur.modifier("BP", "Dimension5", tBD5.Text);
                MessageBox.Show("Modification effectuée");
            }
            else { MessageBox.Show("Les valeurs entrée dépasse la somme total de 110ppp"); }
        }
        private void UserControlBP_Load(object sender, EventArgs e)
        {

        }
    }
}
