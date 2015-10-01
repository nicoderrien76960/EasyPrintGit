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
    public partial class UserControlVariable : UserControl
    {
        public UserControlVariable()
        {
            InitializeComponent();
            textBox1.Text = ConfigurationManager.AppSettings["ChaineDeConnexionBase"];
            textBox2.Text = ConfigurationManager.AppSettings["NbCopieGC"];
            textBox3.Text = ConfigurationManager.AppSettings["PiloteImpressionGC"];
        }

        internal CurseurTemplate CurseurTemplate
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings["ChaineDeConnexionBase"] = textBox1.Text;
            ConfigurationManager.AppSettings["NbCopieGC"] = textBox2.Text;
            ConfigurationManager.AppSettings["PiloteImpressionGC"] = textBox3.Text;
            MessageBox.Show("Modification effectuée");
        }

    }
}
