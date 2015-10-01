using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EssaiJobImp
{
    struct Imp
    {
        string nomProfil;
        string imp;

        public Form1 Form1
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
    class GestionListeDoc
    {
        string bal;
        private Dictionary<string,List<string>> donneeListDoc = new Dictionary<string,List<string>>();   //Dictionnaire de clé chaine, valeur Imprimante (object)
        public void chargementXML()
        {
            donneeListDoc.Clear();
            XmlDocument unxml = new XmlDocument();
            try
            {
                unxml.Load("ListeDocument.config");
            }
            catch (Exception)
            {
                throw;
            }
            XmlNodeList node = unxml.SelectNodes("//configuration/Document");//Création d'une liste de noeud contenant la valeur "Profil" en balise
            foreach (XmlNode unNode in node)
            {
                XmlNodeList nodeEnfant = unNode.ChildNodes;//Récupération du contenu du noeud enfant de chaque profil (balise parent)
                List<string> Nom = new List<string>();//Création de l'object Nom
                foreach (XmlNode unNode2 in nodeEnfant)
                {
                    if (unNode2.Name == "NomComplet")// Si le noeud NomComplet est vu, alors on récupère son contenu
                    {
                        Nom.Add(unNode2.InnerText);
                    }
                }
                bal = (string)unNode.FirstChild.InnerText;//Récupération de la valeur du noeud parent( qui contient le nom du profil)
                donneeListDoc.Add(bal.Trim(), Nom);// Ajout dans le dictionnaire
            }
        }
        public Dictionary<string, List<string>> getDonneeProfil()//Getteur d'accès au dictionnaire
        {
            return donneeListDoc;
        }
        public bool Ajouter(string doc, string profil, string newValue)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            XmlDoc.Load("profilImp" + doc + ".config");
            Navigator = XmlDoc.CreateNavigator();
            string ExpXPath = "//Profil[@nom='" + profil + "']";
            Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
            if (Nodes.Count != 0)
            {
                Nodes.MoveNext();
                Nodes.Current.AppendChildElement("", "Imprimante", "", newValue);
                XmlDoc.Save("profilImp" + doc + ".config");
                return true;
            }
            return true;
        }
        public bool AjouteUser(string doc, string profil, string imp)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            XmlDoc.Load("profilImp" + doc + ".config");
            Navigator = XmlDoc.CreateNavigator();
            string ExpXPath = "//Profil";
            Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
            if (Nodes.Count != 0)
            {
                Nodes.MoveNext();
                Nodes.Current.InsertElementAfter("", "Profil", "", "");
                Nodes.Current.MoveToNext(XPathNodeType.Element);
                Nodes.Current.CreateAttribute("", "nom", "", profil);
                Nodes.Current.AppendChildElement("", "Code", "", profil);
                Nodes.Current.AppendChildElement("", "Imprimante", "", imp);
                XmlDoc.Save("profilImp" + doc + ".config");
                return true;
            }
            return true;
        }
        public bool Modifier(string doc, string profil, string newValue, int selectedIndex)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            XmlDoc.Load("profilImp" + doc + ".config");
            Navigator = XmlDoc.CreateNavigator();
            string ExpXPath = "//Profil[@nom='" + profil + "']";
            Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
            if (Nodes.Count != 0)
            {
                Nodes.MoveNext();
                Nodes.Current.MoveToFirstChild();
                for (int i = 0; i <= selectedIndex; i++)
                {
                    Nodes.Current.MoveToNext();
                }
                Nodes.Current.SetValue(newValue);
                XmlDoc.Save("profilImp" + doc + ".config");
                return true;
            }
            return true;
        }
        public bool Supprimer(string doc, string profil)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            XmlDoc.Load("profilImp" + doc + ".config");
            Navigator = XmlDoc.CreateNavigator();
            string ExpXPath = "//Profil[@nom='" + profil + "']";
            Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
            if (Nodes.Count != 0)
            {
                Nodes.MoveNext();
                Nodes.Current.DeleteSelf();
                XmlDoc.Save("profilImp" + doc + ".config");
            }
            return true;
        }
        public bool SupprimerImp(string doc, string profil, int selectedIndex)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            XmlDoc.Load("profilImp" + doc + ".config");
            Navigator = XmlDoc.CreateNavigator();
            string ExpXPath = "//Profil[@nom='" + profil + "']";
            Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
            if (Nodes.Count != 0)
            {
                Nodes.MoveNext();
                Nodes.Current.MoveToFirstChild();
                for (int i = 0; i <= selectedIndex; i++)
                {
                    Nodes.Current.MoveToNext();
                }
                Nodes.Current.DeleteSelf();
                XmlDoc.Save("profilImp" + doc + ".config");
            }
            return true;
        }
    }
}
