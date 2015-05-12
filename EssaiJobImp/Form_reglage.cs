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
        Dictionary<String,List<String>> dicoProfil = new Dictionary<string,List<string>>();
        Dictionary<String, List<String>> dicoProfilImp = new Dictionary<string, List<string>>();
        public Form_reglage()
        {
            InitializeComponent();
            dicoProfil=remplirLB();
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
        public Dictionary<String, List<string>> remplirCB(string value)
        {
            cBProfil.Items.Clear();
            Dictionary<String, List<string>> listeProfil = new Dictionary<string,List<string>>();
            foreach (KeyValuePair<String,List<string>> s in dicoProfil)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(s.Value[0].ToString(), value, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    ProfilImprimante profil = new ProfilImprimante();
                    profil.chargementXML(s.Key);
                    listeProfil = profil.getDonneeProfil();
                    foreach(var v in listeProfil)
                    {
                        cBProfil.Items.Add(v.Key.ToString());
                    }
                    cBProfil.SelectedIndex=1;
                }
            }
            return listeProfil;
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
        private void lBProfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            dicoProfilImp=remplirCB(lBProfil.SelectedItem.ToString().TrimEnd());
            cBProfil.SelectedIndex = 0;
        }

        private void cBProfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            remplirLBImprimante(cBProfil.SelectedItem.ToString());
        }
    }
}
