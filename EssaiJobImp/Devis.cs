using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace EssaiJobImp
{
    class Devis
    {
        private List<string> baliseEntete = new List<string>();                         //Liste correspondant aux balise lu dans le fichier de conf
        private List<string> baliseBody = new List<string>();
        private List<string> baliseFoot = new List<string>();
        private Dictionary<string, string> donneeEntete = new Dictionary<string, string>();         //Dictionnaire qui contienne les données du document
        private Dictionary<string, string> donneeBody = new Dictionary<string, string>();           //Chaque dictionnaire correspond à une parti du document (Ente, Corps, Pied)
        private Dictionary<string, string> donneeFoot = new Dictionary<string, string>();
        public void chargementXML()
        {
            string bal;
            XmlDocument unxml = new XmlDocument();                  //Un XmlDocument par parti (entete, corps, pied)
            XmlDocument unxmlBody = new XmlDocument();
            XmlDocument unxmlFoot = new XmlDocument();
            try
            {
                unxml.Load("baliseConf.config");                    //Le document de configuration des balises lu doit être découper en trois corps (Entete, Corps, Pied)
                unxmlBody.Load("baliseConf.config");
                unxmlFoot.Load("baliseConf.config");
            }
            catch
            { 
            }
            XmlNode node = unxml.SelectSingleNode("//configuration/BaliseEntete");                  //On lit les noeuds contenu dans le document de conf
            XmlNode nodeBody = unxmlBody.SelectSingleNode("//configuration/BaliseBody");
            XmlNode nodeFoot = unxmlFoot.SelectSingleNode("//configuration/BaliseFoot");
            foreach (XmlNode unNode in node)
            {
                bal = (string)unNode.InnerXml;
                baliseEntete.Add(bal);
            }
            foreach (XmlNode unNode in nodeBody)
            {
                bal = (string)unNode.InnerXml;
                baliseBody.Add(bal);
            }
            foreach (XmlNode unNode in nodeFoot)
            {
                bal = (string)unNode.InnerXml;
                baliseFoot.Add(bal);
            }
        }
        public void lectureDevis(string cheminDoc, string profil)  //<<----CheminDoc contient en paramètre le fichier spool actuellement lu pour le traiter
        {
            chargementXML();
            string fileText = File.ReadAllText(cheminDoc,System.Text.Encoding.Default);
            List<char> charsToSubstitute = new List<char>();
            charsToSubstitute.Add((char)0x0c);                                      //Permet de changer le caractère FF contenu dans le fichier d'origine
            foreach (char c in charsToSubstitute)
            {
                fileText = fileText.Replace(Convert.ToString(c), string.Empty); 
            }
            XmlDocument unxml = new XmlDocument();                                              //Ouverture du document 
            unxml.LoadXml(fileText);
            XmlNode root = unxml.DocumentElement;

            List<string> tempoBody = new List<string>();
            XmlNode doc = unxml.SelectSingleNode("Documents_Rubis");
            XmlNode entete = root.SelectSingleNode("descendant::Document_entete");
            XmlNode lignes = root.SelectSingleNode("descendant::Lignes");
            XmlNode pied = root.SelectSingleNode("descendant::Document_pied");
            foreach (XmlNode noeud in doc)
            {
                foreach (string s in baliseEntete)                                              //--------------------------------
                {                                                                               //
                    if (noeud.Name == s)                                                        //
                    {                                                                           //
                        donneeEntete.Add(s, noeud.InnerText);                                   //          Parseur donnée Doc 
                    }                                                                           //
                }                                                                               //
            }                                                                                   //---------------------------------
            foreach (XmlNode noeud in entete)                                                   //---------------------------------
            {
                foreach (string s in baliseEntete)                                              //
                {                                                                               //
                    if (noeud.Name == s)                                                        //              Parseur En tete
                    {
                        if (noeud.Name == "Duplicata")
                        {
                            if (noeud.Attributes.Count == 0)
                            {
                                donneeEntete.Add(s, noeud.InnerText);
                            }
                            else { donneeEntete.Add("Duplicata", ""); }//                            //
                        }
                        else
                        {
                            donneeEntete.Add(s, noeud.InnerText);
                        }
                    }                                                                      //
                }                                                                               //
            }                                                                                   //
            int iBody = 0; int iFoot; int compt = 0;//                                          //-------------------------------------------------------------------------------------------
            foreach (XmlNode noeud in lignes)                                                   //
            {                                                                                   //
                XmlNode nligne = noeud;                                                         //
                XmlNodeList nligneinfo = noeud.SelectNodes("descendant::Ligne_info");           //
                iBody++;                                                                        //                                                         
                foreach (XmlNode node in nligne)                                                //      La double boucle permet d'atteindre le noeud suivant "Ligne_info"
                {                                                                               //
                    foreach (string s in baliseBody)                                            //
                    {                                                                           //
                        if (node.Name == s)                                                     //
                        {                                                                       //
                            donneeBody.Add(s + iBody, node.InnerText);                          //
                        }                                                                       //
                    }                                                                           //
                }

                foreach (XmlNode node in nligneinfo)                                            //
                {

                    XmlNode nligneinfo2 = node;                                                 //
                    foreach (string s in baliseBody)                                            //                Parseur Body
                    {                                                                           //
                        if (node.Name == s)                                                     //
                        {                                                                       //
                            donneeBody.Add(s + iBody, node.InnerText);                          //
                        }                                                                       //
                    }  //
                    foreach (XmlNode n in nligneinfo2)                                          //
                    {
//
                        foreach (string s in baliseBody)                                        //
                        {                                                                       //
                            if (n.Name == s)                                                    //
                            {                                                                   //
                                                                                                //
                                if (donneeBody.ContainsKey(s + iBody))                          //
                                {
                                    if (donneeBody.ContainsKey(s + iBody + "bis"))              //
                                    {                                                           //
                                        donneeBody.Add(s + iBody + "bis" +compt, n.InnerText);
                                        compt++;                                                //
                                    }
                                    else
                                    {
                                        donneeBody.Add(s + iBody + "bis", n.InnerText);
                                        compt = 0;
                                    }
                                }                                                               //<--- Permet de crée un incrémentation des clés (libelle1, libelle2, libelle3, ect)     
                                else                                                            //
                                {                                                               //
                                    donneeBody.Add(s + iBody, n.InnerText);                     //
                                }                                                               //
                                                                                                //
                            }
                        }
                    }                                                                           //
                }                                                                               //
            }                                                                                   //
            iFoot = 0;                                                                          //-----------------------------------
            foreach (XmlNode noeud in pied)                                                     //-----------------------------------
            {                                                                                   //
                XmlNode npiedBase = noeud;                                                      //
                iFoot++;                                                                        //
                foreach (string s in baliseFoot)                                                //
                {                                                                               //
                    if (noeud.Name == s)                                                        //
                    {                                                                           //
                        donneeFoot.Add(s, noeud.InnerText);                                     //
                    }                                                                           //
                }                                                                               //              Parseur Pied
                if (npiedBase.Name == "Pied_base_tva")                                          //
                {                                                                               //
                    foreach (XmlNode node in npiedBase)                                         //
                    {                                                                           //
                        foreach (string s in baliseFoot)                                        //
                        {                                                                       //                                                               
                                if (node.Name == s)                                             //
                                { 
                                    if (donneeFoot.ContainsKey(s))                              //
                                    {
                                        donneeFoot.Add(s + iFoot, node.InnerText);
                                    }  
                                    else
                                    {                                                           //
                                        donneeFoot.Add(s, node.InnerText);   
                                    }                                                           //
                                }                                                               //                                                                  
                        }                                                                       //
                    }                                                                           //
                }                                                                               //------------------------------------
            }
            string nomDoc = donneeEntete["Document_numero"];
            Parseur p = new Parseur(donneeEntete, donneeBody, donneeFoot, iBody, iFoot, nomDoc, profil);
            //------------------------------------------------------------------------------------------------
            p.miseEnForm("Devis");                               //Mise en forme pdf des données reçu
            //------------------------------------------------------------------------------------------------
        }
        //Getteur dictionnaire --------------------------------------------------------------------------------------------------------NON UTILISE------------------------------------
        public Dictionary<string, string> getDonneeEntete()                                                     
        {
            return donneeEntete;
        }
        public Dictionary<string, string> getDonneeBody()
        {
            return donneeBody;
        }
        public Dictionary<string, string> getDonneeFoot()
        {
            return donneeFoot;
        }
    }
}
