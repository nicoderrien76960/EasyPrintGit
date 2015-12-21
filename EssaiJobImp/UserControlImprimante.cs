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
    public partial class UserControlImprimante : UserControl
    {
        GestionListeDoc list = new GestionListeDoc();
        public Dictionary<String, List<String>> dicoProfil = new Dictionary<string, List<string>>();
        UserControlImprimanteProfil UCIP = new UserControlImprimanteProfil();
        public UserControlImprimante()
        {
            InitializeComponent();
        }

        public UserControlImprimanteProfil UserControlImprimanteProfil
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        internal GestionListeDoc GestionListeDoc
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public Dictionary<String, List<String>> remplirLB()
        {
            lBProfil.Items.Clear();
            list.chargementXML();
            var listeProfil = list.getDonneeProfil();
            foreach (var v in listeProfil)
            {
                lBProfil.Items.Add(v.Value[0].TrimStart().ToString());
            }

            return listeProfil;
        }
        private void btnAjoutProfil_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            label4.Visible = true;
            btnValiderAjoutPro.Visible = true;
            tbAjoutProfil.Visible = true;
            tbAjoutImprimante.Visible = true;
            this.Update();
        }
        private void btnSupProfil_Click(object sender, EventArgs e)
        {
            string document = dicoProfil.Keys.ElementAt(lBProfil.SelectedIndex).ToString();
            bool result = list.Supprimer(document, UCIP.cBProfil.SelectedItem.ToString());
            
            if (result == true)
            {
                MessageBox.Show("Suppression effectué");
            }
            else { MessageBox.Show("Erreur lors de la supression"); }
            this.Update();
        }
        private void btnValiderAjoutPro_Click(object sender, EventArgs e)
        {
            string document = dicoProfil.Keys.ElementAt(lBProfil.SelectedIndex).ToString();
            bool result = list.AjouteUser(document, tbAjoutProfil.Text.ToString(), tbAjoutImprimante.Text.ToString());
            if (result == true)
            {
                MessageBox.Show("Ajout effectué");
            }
            else { MessageBox.Show("Erreur lors de l'ajout"); }
            label3.Visible = false;
            label4.Visible = false;
            btnValiderAjoutPro.Visible = false;
            tbAjoutProfil.Visible = false;
            tbAjoutImprimante.Visible = false;
            this.Update();
        }
        private void lBProfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            SplitContainer S1 = (this.ParentForm as Form_reglage).Controls["splitContainer1"] as SplitContainer;
            S1.Panel2.Controls.Clear();
            S1.Panel2.Controls.Add(UCIP);
            UCIP.getParentObject(lBProfil);
            UCIP.dicoProfil = dicoProfil;
            UCIP.dicoProfilImp = UCIP.remplirCB(lBProfil.SelectedItem.ToString().TrimEnd());
            UCIP.cBProfil.SelectedIndex = 0;
        }
    }
}
