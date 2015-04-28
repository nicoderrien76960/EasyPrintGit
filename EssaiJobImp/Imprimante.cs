﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Printing;
using System.IO;
using System.Windows.Forms;

namespace EssaiJobImp
{
    public struct Spool   //Création d'un object Spool (plan structure)
    {
        public string nomSpool;
        public string nomImprimante;
    }
    class Imprimante : ICloneable
    {
        int nbDoc=0;
        string cheminDoc;
        List<Spool> listFichierSpool = new List<Spool>();//Liste de fichier contenu dans le spool
        public void lectureSpooler(string nomIMP)
        {
            string typeDoc="";
                listFichierSpool.Clear();// Je vide ma liste avant de débuter une nouvelle lecture
                string[] files;
                // pour avoir les noms des fichiers et sous-répertoires 
                files = Directory.GetFiles("C:\\Windows\\System32\\spool\\PRINTERS", "*.SPL");//Repertoire du spool Windows
                PrintServer myPrintServer = new PrintServer();
                PrintQueueCollection myPrintQueues = myPrintServer.GetPrintQueues();
                bool supOk = false;
                foreach (PrintQueue pq in myPrintQueues)//Analyse du spool du pc (Dossier Windows\System32\spool\PRINTERS)
                {
                    if (pq.FullName == nomIMP)//Condition sur imprimante qui a crée l'objet Imprimante
                    {
                        try     
                        {
                            int filecount = files.GetUpperBound(0) + 1;  //Nombre de fichier contenu dans le spool
                            for (int i = 0; i < filecount; i++)//Nombre de fichier dans dossier spool
                            {
                                DateTime temp = Directory.GetLastWriteTime(files[i]).AddMilliseconds(-Directory.GetLastWriteTime(files[i]).Millisecond);//Décalage de lecture des documents
                                DateTime actuelle = DateTime.Now;
                                actuelle = DateTime.Now.AddMilliseconds(-actuelle.Millisecond);
                                string nom_spool = "";
                                string nomFichier = System.IO.Path.GetFileName(files[i]);//Récuperation du nom du fichier lu
                                string sourceFile = System.IO.Path.Combine(@"C:\Windows\System32\spool\PRINTERS", nomFichier);//C:\Windows\System32\spool\PRINTERS
                                string destFile = System.IO.Path.Combine(@"E:\Copie spool", nomFichier);
                                string[] files2 = Directory.GetFiles(@"E:\Copie spool", "*SPL");//Tableau contenant les fichier SPL
                                int file2count = files2.GetUpperBound(0) + 1;
                                int emplacementDoc = 0;
                                if (temp.Second != actuelle.Second)//Condition tempo de temps d'analyse des docs
                                {
                                    for (int j = 0; j < filecount; j++)//Analyse existant dans fichier de destination
                                    {
                                        if (System.IO.File.Exists(@"E:\Copie spool\" + nomFichier))//Condition d'existance
                                        { emplacementDoc = j; }
                                        else
                                        {
                                            System.IO.File.Copy(sourceFile, destFile, true);//Si le fichier n'existe pas, je le copie afin de pouvoir le traiter par la suite
                                            supOk = true;
                                            emplacementDoc = j;
                                        }
                                    }
                                    nbDoc = 1;
                                    string[] text = System.IO.File.ReadAllLines(files[i]);
                                    string patternLectFalse = "(%-12345X@PJL JOB NAME|\\210-SERVIMP)";//Premier caractère qui apparait sur les documents en cours d'impression
                                    string sPattern = "<Spool>";
                                    string sPatternTypeDoc = "<Document_type>"; bool patternOK=true;
                                    foreach (string s in text)//Analyse ligne du document actuel
                                    {
                                        //Premier tri si le document lu est en fait l'impression final d'un doc déjà traité, ne pas le lire et passer au suivant
                                        if ((System.Text.RegularExpressions.Regex.IsMatch(s, patternLectFalse, System.Text.RegularExpressions.RegexOptions.IgnoreCase) == false) && patternOK==true)
                                        {
                                            //Regex type de document
                                            if (System.Text.RegularExpressions.Regex.IsMatch(s, sPatternTypeDoc, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                            {
                                                typeDoc = s.Substring(24, (s.IndexOf(']') - 24));
                                            }
                                            //Regex balise <spool>\ prévoir changement du regex dans les fichiers de conf
                                            if (System.Text.RegularExpressions.Regex.IsMatch(s, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                            {
                                                nom_spool = s.Substring(16, 10);
                                                //textBox1.Text += (nom_spool + " ------> ");
                                                if (supOk == true)//Condition si copie ok donc suppression
                                                {
                                                    System.IO.File.Delete(sourceFile);
                                                    PrintServer myPrintServer2 = new PrintServer();
                                                    PrintQueueCollection myPrintQueues2 = myPrintServer.GetPrintQueues();
                                                    foreach (PrintQueue pq2 in myPrintQueues2)
                                                    {
                                                        pq2.Refresh();
                                                        PrintJobInfoCollection jobs = pq2.GetPrintJobInfoCollection();
                                                        foreach (PrintSystemJobInfo job in jobs)//Lecture des docs dans file d'attente
                                                        {
                                                            if (job.Name == nom_spool)//Condition nom du doc de la file
                                                            {

                                                                job.Cancel();
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        { patternOK = false; typeDoc = null; }
                                    }
                                    cheminDoc = destFile;
                                    switch (typeDoc.TrimStart())
                                    {
                                        case "DEVIS":
                                            Devis dev = new Devis(); dev.lectureDevis(cheminDoc); 
                                            break;
                                        case "BORDEREAU DE LIVRAISON":
                                            BonLivraison BL = new BonLivraison(); BL.lectureBL(cheminDoc);
                                            break;
                                        case "BON D'ENLEVEMENT":
                                            BonLivraison BL2 = new BonLivraison(); BL2.lectureBL(cheminDoc);
                                            break;
                                        case "BON DE PREPARATION":
                                            BonPréparation BP = new BonPréparation(); BP.lectureBP(cheminDoc);
                                            break;
                                        case "COMMANDE ADHERENT":
                                            AccuseReception AR = new AccuseReception(); AR.lectureAR(cheminDoc);
                                            break;
                                        case "BON DE COMMANDE FOURNISSEUR":
                                            CommandeFournisseur CF = new CommandeFournisseur(); CF.lectureCF(cheminDoc);
                                            break;
                                        case null:
                                            break;
                                    }
                                    System.IO.File.Delete(destFile);
                                }
                                else
                                {
                                    break;

                                }
                                supOk = false;
                            }
                        }
                        catch (Exception e) {
                            //Inscrit dans un fichier les differente erreur
                            LogHelper.WriteToFile(e.Message, "Imprimante");
                        }
                    }
                }
        }
        public void setNbDoc(int newNbDoc)
        {
            nbDoc = newNbDoc;
        }
        public int getNbDoc()
        {
            return nbDoc;
        }
        public object Clone()
        {
            return this.MemberwiseClone();      //Clonage
        }
    }
}