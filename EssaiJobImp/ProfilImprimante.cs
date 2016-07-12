using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Ireport_Rubis
{
    class ProfilImprimante
    {
        string bal; //string bal2; INUTILISE
        private Dictionary<string,List<string>> donneeProfil = new Dictionary<string,List<string>>();   //Dictionnaire de clé chaine, valeur Imprimante (object)
        public void chargementXML(string typeDoc)
        {
            XmlDocument unxml = new XmlDocument();
            try
            {
                unxml.Load("profilImp"+typeDoc+".config");
            }
            catch (Exception)
            {
                throw;
            }
            XmlNodeList node = unxml.SelectNodes("Profils/Profil");//Création d'une liste de noeud contenant la valeur "Profil" en balise
            foreach (XmlNode unNode in node)
            {
                XmlNodeList nodeEnfant = unNode.ChildNodes;//Récupération du contenu du noeud enfant de chaque profil (balise parent)
                List<string> imprimante = new List<string>();//Création de l'object imprimante
                foreach (XmlNode unNode2 in nodeEnfant)
                {
                    if (unNode2.Name == "Imprimante")// Si le noeud imprimante est vu, alors on récupère son contenu
                    {
                        imprimante.Add(unNode2.InnerText);
                    }
                }
                bal = (string)unNode.FirstChild.InnerText;//Récupération de la valeur du noeud parent( qui contient le nom du profil)
                donneeProfil.Add(bal.Trim(), imprimante);// Ajout dans le dictionnaire
            }
        }
        public Dictionary<string, List<string>> getDonneeProfil()//Getteur d'accès au dictionnaire
        {
            return donneeProfil;
        }
    }
}
