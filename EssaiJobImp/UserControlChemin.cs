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
    public partial class UserControlChemin : UserControl
    {
        public UserControlChemin()
        {
            InitializeComponent();
            textBox1.Text = ConfigurationManager.AppSettings["CheminGED"];
            textBox4.Text = ConfigurationManager.AppSettings["CheminRessources"];
            textBox3.Text = ConfigurationManager.AppSettings["CheminDocFinaux"];
            textBox2.Text = ConfigurationManager.AppSettings["ImpDef"];
            textBox5.Text = ConfigurationManager.AppSettings["ImpDefBL"];
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings["CheminGED"] = textBox1.Text;
            ConfigurationManager.AppSettings["CheminRessources"] = textBox4.Text;
            ConfigurationManager.AppSettings["CheminDocFinaux"] = textBox3.Text ;
            ConfigurationManager.AppSettings["ImpDef"] = textBox2.Text;
            ConfigurationManager.AppSettings["ImpDefBL"] = textBox5.Text;
            MessageBox.Show("Modification effectuée");
        }
    }
}
