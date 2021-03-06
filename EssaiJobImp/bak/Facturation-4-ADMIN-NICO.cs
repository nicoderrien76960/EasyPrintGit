﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Ireport_Rubis
{
    class Facturation
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
                unxml.Load("baliseConfFacturation.config");                    //Le document de configuration des balises lu doit être découper en trois corps (Entete, Corps, Pied)
                unxmlBody.Load("baliseConfFacturation.config");
                unxmlFoot.Load("baliseConfFacturation.config");
            }
            catch(Exception e)
            { 
             
                //à tester  
                LogHelper.WriteToFile(e.Message, "test fichier à supprimer si porblème facuration.cs");
               
            }
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
        /// 
        public void lectureFA(string cheminDoc, string profil)//<<----CheminDoc contient en paramètre le fichier spool actuellement lu pour le traiter
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
            XmlNode lignes = root.SelectSingleNode("descendant::Lignes");
            XmlNode pied = root.SelectSingleNode("descendant::Document_pied");
            int iEntete=0;//-------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
                                donneeEntete.Add(s+iEntete, n.InnerText);                       //
                                iEntete++;                                                      //
                            }                                                                   ////
                        }                                                                       //
                    }                                                                           //
                }                                                                               //
            }                                                                                   //
           
            
            int iBody = 0; int iFoot; int compt = 0;                                            //                                          
            foreach (XmlNode noeud in lignes)                                                   //----------------------------------
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
                }                                                                               //
                                                                                                //
             
                foreach (XmlNode node in nligneinfo)                                            //
                {                                                                               //                                                                                             //
                    XmlNode nligneinfo2 = node;                                                 //
                    foreach (string s in baliseBody)                                            //                Parseur Body
                    {                                                                           //
                        if (node.Name == s)                                                     //
                        {                                                                       //
                            donneeBody.Add(s + iBody, node.InnerText);                          //
                        }                                                                       //
                    }                                                                           //
                 
                    foreach (XmlNode n in nligneinfo2)                                          //
                    {                                                                           //
                                                                                                //
                        foreach (string s in baliseBody)                                        //
                        {                                                                       //
                            if (n.Name == s)                                                    //
                            {                                                                   //
                                                                                                //
                                if (donneeBody.ContainsKey(s + iBody))                          //
                                {                                                               //
                                    if (donneeBody.ContainsKey(s + iBody + "bis"))              //
                                    {                                                           //
                                        compt++;                                                //
                                        donneeBody.Add(s + iBody + "bis" + compt, n.InnerText); //
                                    }                                                           //
                                    else                                                        //
                                    {                                                           //
                                        donneeBody.Add(s + iBody + "bis", n.InnerText);         //
                                    }                                                           //
                                }                                                               //<--- Permet de crée un incrémentation des clés (libelle1, libelle2, libelle3, ect)     
                                else                                                            //
                                {                                                               //
                                    donneeBody.Add(s + iBody, n.InnerText);                     //
                                }                                                               //                                                                                               //
                            }                                                                   //
                        }                                                                       //
                    }                                                                           //
                }                                                                               //
            }                                                                                   //
            
            
            
            //iFoot = 0;                                                                          //-----------------------------------pied
            iFoot = 0; 
            foreach (XmlNode noeud in pied)                                                     //-----------------------------------
            {                                                                                   //
                XmlNode npiedBase = noeud;                                                      //
                iFoot++;
                //si npiedBase contient "Recap_DEEE_liste" retirer 1 à ifoot

                if (npiedBase.Name == "Recap_DEEE_liste") {
                    iFoot = iFoot - 1;
                    // bug facturation suite ajout Deee à gérer plus tard
                }


                foreach (XmlNode n in npiedBase)                                                //
                {                                                                               //
                    foreach (string s in baliseFoot)                                            //
                    {                                                                           //

                        //si npiedBase contient "Recap_DEEE_liste" retirer 1 à ifoot
                        
                        
                        if (n.Name == s)                                                        //
                        {                                                                       //
                            donneeFoot.Add(s+iFoot, n.InnerText);                               //
                        }                                                                       //
                    }                                                                           //
                }           
                //
                foreach (string s in baliseFoot)                                                //
                {                                                                               //
                    if (noeud.Name == s)                                                        //
                    {                                                                           //
                        donneeFoot.Add(s, noeud.InnerText);                                     //
                    }                                                                           //
                }                                                                               //              Parseur Pied                                                                          //------------------------------------
            }
            string nomDoc = donneeEntete["Document_numero"];
            ParseurFacturation p = new ParseurFacturation(donneeEntete, donneeBody, donneeFoot, iBody, iFoot, nomDoc, profil);
            //------------------------------------------------------------------------------------------------
            p.miseEnForm("Facture");                               //Mise en forme pdf des données reçu
            //------------------------------------------------------------------------------------------------
        }
    }
}
