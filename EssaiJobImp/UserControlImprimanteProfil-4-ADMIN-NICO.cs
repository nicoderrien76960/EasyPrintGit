using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ireport_Rubis
{
    public partial class UserControlImprimanteProfil : UserControl
    {
        public UserControlImprimanteProfil()
        {
            InitializeComponent();
        }

        internal ProfilImprimante ProfilImprimante
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public UserControlBL UserControlBL
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public UserControlAR UserControlAR
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public UserControlFacturation UserControlFacturation
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public UserControlDevis UserControlDevis
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public UserControlCF UserControlCF
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public UserControlVariable UserControlVariable
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public UserControlChemin UserControlChemin
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public UserControlMail UserControlMail
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public UserControlBP UserControlBP
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
    
        public Dictionary<String, List<String>> dicoProfil = new Dictionary<string, List<string>>();
        public Dictionary<String, List<String>> dicoProfilImp = new Dictionary<string, List<string>>();
        GestionListeDoc list = new GestionListeDoc();
        ListBox L1;
        public Dictionary<String, List<string>> remplirCB(string value)
        {
            cBProfil.Items.Clear();
            Dictionary<String, List<string>> listeProfil = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<String, List<string>> s in dicoProfil)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(s.Value[0].ToString(), value, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    ProfilImprimante profil = new ProfilImprimante();
                    profil.chargementXML(s.Key);
                    listeProfil = profil.getDonneeProfil();
                    foreach (var v in listeProfil)
                    {
                        this.cBProfil.Items.Add(v.Key.ToString());
                    }
                }
            }
            return listeProfil;
        }
        private void cBProfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            remplirLBImprimante(cBProfil.SelectedItem.ToString());
        }
        public void remplirLBImprimante(string value)
        {
            lBImprimante.Items.Clear();
            foreach (KeyValuePair<String, List<string>> s in dicoProfilImp)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(s.Key.ToString(), value, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    foreach (var v in s.Value)
                    { lBImprimante.Items.Add(v.ToString()); }
                }
            }
        }
        private void btn_SupImp_Click(object sender, EventArgs e)
        {
            string document = dicoProfil.Keys.ElementAt(L1.SelectedIndex).ToString();
            bool result = list.SupprimerImp(document, cBProfil.SelectedItem.ToString(), lBImprimante.SelectedIndex);
            if (result == true)
            {
                MessageBox.Show("Suppression effectué");
            }
            else { MessageBox.Show("Erreur lors de la supression"); }
            this.Update();
        }
        private void btn_ModifImp_Click(object sender, EventArgs e)
        {
            if (lBImprimante.SelectedItem != null)
            {
                tBModifImp.Text = lBImprimante.SelectedItem.ToString();
                label2.Visible = true;
                btn_Valider.Visible = true;
                tBModifImp.Visible = true;
            }
        }
        public void getParentObject(ListBox o)
        {
            L1 = o;
        }
        private void btn_Valider_Click(object sender, EventArgs e)
        {
            string document = dicoProfil.Keys.ElementAt(L1.SelectedIndex).ToString();
            bool result = list.Modifier(document, cBProfil.SelectedItem.ToString(), tBModifImp.Text, lBImprimante.SelectedIndex);
            if (result == true)
            {
                MessageBox.Show("Modification effectué");
            }
            else { MessageBox.Show("Erreur lors de la modification"); }
            tBModifImp.Visible = false;
            btn_Valider.Visible = false;
            label2.Visible = false;
        }
        private void btn_AjoutImp_Click(object sender, EventArgs e)
        {
            string document = dicoProfil.Keys.ElementAt(L1.SelectedIndex).ToString();
            label2.Visible = true;
            btnValidAjout.Visible = true;
            tBModifImp.Visible = true;
        }
        private void btnValidAjout_Click(object sender, EventArgs e)
        {
            string document = dicoProfil.Keys.ElementAt(L1.SelectedIndex).ToString();
            bool result = list.Ajouter(document, cBProfil.SelectedItem.ToString(), tBModifImp.Text);
            if (result == true)
            {
                MessageBox.Show("Ajout effectué");
            }
            else { MessageBox.Show("Erreur lors de l'ajout"); }
            tBModifImp.Visible = false;
            btnValidAjout.Visible = false;
            label2.Visible = false;
            this.Update();
        }

        private void UserControlImprimanteProfil_Load(object sender, EventArgs e)
        {

        }
    }
}
