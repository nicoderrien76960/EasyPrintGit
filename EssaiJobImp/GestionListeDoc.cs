using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EssaiJobImp
{
    class GestionListeDoc
    {
        string bal;
        private Dictionary<string,List<string>> donneeListDoc = new Dictionary<string,List<string>>();   //Dictionnaire de clé chaine, valeur Imprimante (object)
        public void chargementXML()
        {
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
    }
}
