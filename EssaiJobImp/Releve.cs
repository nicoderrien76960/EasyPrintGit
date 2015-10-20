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
    class Releve
    {
        /// <summary>
        /// Classe récupérant les informations d'un document Facturation selon les choix fait dans la selection des balise (chargementXML)
        /// </summary>
        private List<string> baliseEntete = new List<string>();                         //Liste correspondant aux balise lu dans le fichier de conf
        private List<string> baliseBody = new List<string>();
        private List<string> baliseFoot = new List<string>();
        private Dictionary<string, string> donneeEntete = new Dictionary<string, string>();         //Dictionnaire qui contienne les données du document
        private Dictionary<string, string> donneeBody = new Dictionary<string, string>();           //Chaque dictionnaire correspond à une parti du document (Ente, Corps, Pied)
        private Dictionary<string, string> donneeFoot = new Dictionary<string, string>();
        /// <summary>
        /// Méthode de chargement du fichier de config XML
        /// </summary>
        public void chargementXML()
        {
            string bal;
            XmlDocument unxml = new XmlDocument();                  //Un XmlDocument par parti (entete, corps, pied)
            XmlDocument unxmlBody = new XmlDocument();
            XmlDocument unxmlFoot = new XmlDocument();
            try
            {
                unxml.Load("baliseConfR.config");                    //Le document de configuration des balises lu doit être découper en trois corps (Entete, Corps, Pied)
                unxmlBody.Load("baliseConfR.config");
                unxmlFoot.Load("baliseConfR.config");
            }
            catch
            { }
            XmlNode node = unxml.SelectSingleNode("//configuration/BaliseEntete");                  //On lis les noeuds contenu dans le document de conf
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
        /// <summary>
        /// Lit la facture selon et compare les balises selon les demandes inscrite dans le fichier XML
        /// </summary>
        /// <param name="cheminDoc">Chemin du document path</param>
        /// <param name="profil">Profil de l'utilisateur de la facture</param>
        public void lectureR(string cheminDoc, string profil)//<<----CheminDoc contient en paramètre le fichier spool actuellement lu pour le traiter
        {
            chargementXML();
            string fileText = File.ReadAllText(cheminDoc, System.Text.Encoding.Default);
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
            XmlNodeList lignes = root.SelectNodes("descendant::Document_detail");
            XmlNode total_rel = root.SelectSingleNode("descendant::Document_total_rel");
            XmlNode pied = null; 
            try
            {
                pied = root.SelectSingleNode("descendant::Document_traite");
            }
            catch { }
            int iEntete = 0;//-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            foreach (XmlNode noeud in entete)                                                   //-------------------------------------------------------------------------------------------
            {                                                                                   //
                //
                foreach (string s in baliseEntete)                                              //
                {                                                                               //
                    if (noeud.Name == s)                                                        //              Parseur En tete
                    {                                                                           //
                        if (noeud.Name == "Duplicata")                                          //
                        {                                                                       //
                            if (noeud.Attributes.Count == 0)                                    //
                            {                                                                   //
                                donneeEntete.Add(s, noeud.InnerText);                           //
                            }                                                                   //
                            else { donneeEntete.Add("Duplicata", "\t                   "); }    //                      
                        }                                                                       //
                        else                                                                    //
                        {                                                                       //
                            donneeEntete.Add(s, noeud.InnerText);                               //
                        }                                                                       //
                    }                                                                           ////
                }                                                                               //
                if (noeud.Name == "Commentaire_entete")                                         //
                {                                                                               //
                    XmlNode nCommentaireBon = noeud;                                            //
                    foreach (XmlNode n in nCommentaireBon)                                      //
                    {                                                                           //
                        foreach (string s in baliseEntete)                                      ////
                        {                                                                       //
                            if (n.Name == s)                                                    //              Parseur En tete
                            {                                                                   //
                                donneeEntete.Add(s + iEntete, n.InnerText);                       //
                                iEntete++;                                                      //
                            }                                                                   ////
                        }                                                                       //
                    }                                                                           //
                }                                                                               //
            }                                                                                   //
            int iBody = 0; int iFoot; 
            
            /*ND DEBUT 19 10 2015*/
            //int compt = 0;                                            //                                          
            /* ND FIN 19 10 2015*/
            foreach (XmlNode noeud in lignes)                                                   //----------------------------------
            {
                XmlNode nDetail = noeud;
                foreach (XmlNode n in nDetail)
                {                                                                                //                                                                           //
                    foreach (string s in baliseBody)                                            //
                    {                                                                           //
                        if (n.Name == s)                                                     //
                        {                                                                       //
                            donneeBody.Add(s + iBody, n.InnerText);                          //
                        }                                                                       //
                    }
                }
                iBody++;//                                                                             //
            }                                                   //
            iFoot = 0;
            try
            {
                foreach (XmlNode noeud in total_rel)
                {
                    foreach (string s in baliseFoot)                                            //
                    {                                                                           //
                        if (noeud.Name == s)                                                     //
                        {                                                                       //
                            donneeFoot.Add(s, noeud.InnerText);                          //
                        }                                                                       //
                    }
                }                                                                                       //-----------------------------------
                foreach (XmlNode noeud in pied)                                                     //-----------------------------------
                {                                                                                   //
                    XmlNode npiedBase = noeud;                                                      //
                    iFoot++;                                                                        //
                    foreach (XmlNode n in npiedBase)                                                //
                    {                                                                               //
                        foreach (string s in baliseFoot)                                            //
                        {                                                                           //
                            if (n.Name == s)                                                        //
                            {                                                                       //
                                donneeFoot.Add(s + iFoot, n.InnerText);                               //
                            }                                                                       //
                        }                                                                           //
                    }                                                                               //
                    foreach (string s in baliseFoot)                                                //
                    {                                                                               //
                        if (noeud.Name == s)                                                        //
                        {                                                                           //
                            donneeFoot.Add(s, noeud.InnerText);                                     //
                        }                                                                           //
                    }                                                                               //              Parseur Pied                                                                          //------------------------------------
                }
            }
            catch { }
            string nomDoc = donneeEntete["Document_numero"];
            ParseurReleve p = new ParseurReleve(donneeEntete, donneeBody, donneeFoot, iBody, iFoot, nomDoc, profil);
            //------------------------------------------------------------------------------------------------
            p.miseEnForm("Releve");                               //Mise en forme pdf des données reçu
            //------------------------------------------------------------------------------------------------
        }
    }
}
