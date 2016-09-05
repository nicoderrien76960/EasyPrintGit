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

namespace Ireport_Rubis
{
    public partial class UserControlMail : UserControl
    {
        public UserControlMail()
        {
            InitializeComponent();
            string tempo = System.Configuration.ConfigurationManager.AppSettings["ParamMail"];
            string[] paramServeur = tempo.Split('%');
            textBox1.Text = ConfigurationManager.AppSettings["CorpMail"];
            textBox4.Text = paramServeur[2];
            textBox3.Text = paramServeur[0];
            textBox2.Text = paramServeur[3];
            textBox5.Text = paramServeur[1];       
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

        private void button1_Click(object sender, EventArgs e)
        {
            string tempo = System.Configuration.ConfigurationManager.AppSettings["ParamMail"];
            string[] paramServeur = tempo.Split('%');
            ConfigurationManager.AppSettings["CorpMail"] = textBox1.Text;
            paramServeur[2] = textBox4.Text;
            paramServeur[0] = textBox3.Text;
            paramServeur[3] = textBox2.Text;
            paramServeur[1] = textBox5.Text;
            ConfigurationManager.AppSettings["ParamMail"] = paramServeur[0] + "%" + paramServeur[1] + "%" + paramServeur[2] + "%" + paramServeur[3];
            MessageBox.Show("Modification effectuée");
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
