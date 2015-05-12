using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EssaiJobImp
{
    public partial class Form_reglage : Form
    {
        GestionListeDoc list = new GestionListeDoc();
        Dictionary<String,List<String>> listeProfil = new Dictionary<string,List<string>>();
        public Form_reglage()
        {
            InitializeComponent();
            remplirLB();
        }
        public Dictionary<String,List<String>> remplirLB()
        {
            list.chargementXML();
            var listeProfil = list.getDonneeProfil();
            foreach (var v in listeProfil)
            {
                lBProfil.Items.Add(v.Value[0].TrimStart().ToString());
            }
            return listeProfil;
        }
        public void remplirCB(string value)
        {
            foreach (KeyValuePair<String,List<string>> s in listeProfil)
            {
                if (s.Value.ToString()== value)
                {
                    MessageBox.Show("Woupi");
                }
            }

        }
        private void lBProfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            remplirCB(lBProfil.SelectedItem.ToString().TrimEnd());
        }
    }
}
