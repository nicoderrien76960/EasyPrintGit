using System;
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
                bool supOk = false; string nomDoc = ""; string profil="";
                foreach (PrintQueue pq in myPrintQueues)//Analyse du spool du pc (Dossier Windows\System32\spool\PRINTERS)
                {
                    if (pq.FullName == nomIMP)//Condition sur imprimante qui a créer l'objet Imprimante
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
                                        {   
                                            System.IO.File.Copy(sourceFile, destFile, true);
                                            supOk = true;
                                            emplacementDoc = j;
                                        }
                                        else
                                        {
                                            System.IO.File.Copy(sourceFile, destFile, true);//Si le fichier n'existe pas, je le copie afin de pouvoir le traiter par la suite
                                            supOk = true;
                                            emplacementDoc = j;
                                        }
                                    }
                                    nbDoc = 1;
                                    string[] text = System.IO.File.ReadAllLines(files[i],Encoding.Default);
                                    string patternLectFalse = "(%-12345X@PJL JOB NAME|\\210-SERVIMP|&l26A)";//Premier caractère qui apparait sur les documents en cours d'impression
                                    string sPattern = "<Spool>";
                                    string sPatternTypeDoc = "<Document_type>"; bool patternOK = true; bool patternOK2 = false; bool découpageOK = true; int controle = 0; int test = 0;
                                    string sPatternTypeDoc2 ="<Document type=\"DOC_CLIENT\" doc=\"FACTURE ";
                                    StreamWriter sr = null;
                                    foreach (string s in text)//Analyse ligne du document actuel
                                    {
                                        controle++;
                                        //Premier tri si le document lu est en fait l'impression final d'un doc déjà traité, ne pas le lire et passer au suivant
                                        if ((System.Text.RegularExpressions.Regex.IsMatch(s, patternLectFalse, System.Text.RegularExpressions.RegexOptions.IgnoreCase) == false) && patternOK == true)
                                        {

                                            if (System.Text.RegularExpressions.Regex.IsMatch(s, "<Document type=\"DOC_CLIENT\" doc=\"FACTURE ", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                            { 
                                                découpageOK = false;
                                                sr = new StreamWriter(@"E:\Copie spool\tempo" + test + ".SPL", false, Encoding.GetEncoding("iso-8859-1"));
                                            }
                                            if (System.Text.RegularExpressions.Regex.IsMatch(s, "</Document>", System.Text.RegularExpressions.RegexOptions.IgnoreCase) && découpageOK==false)
                                            {
                                                sr.WriteLine(s);
                                                découpageOK = true; }
                                            if (découpageOK == false)
                                            { sr.WriteLine(s); }
                                            if ((découpageOK == true) && (sr!=null))
                                            {sr.Close(); test++; }
                                            //Regex type de document
                                            if (System.Text.RegularExpressions.Regex.IsMatch(s, sPatternTypeDoc2, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                            {
                                                typeDoc = "FACTURE";
                                                patternOK2 = true;
                                            }
                                            else
                                            {
                                                if (System.Text.RegularExpressions.Regex.IsMatch(s, sPatternTypeDoc, System.Text.RegularExpressions.RegexOptions.IgnoreCase)&& patternOK2==false)
                                                {
                                                    typeDoc = s.Substring(24, (s.IndexOf(']') - 24));
                                                }
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
                                                                profil = job.Submitter;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        { patternOK = false; typeDoc = null; System.IO.File.Delete(sourceFile); patternOK2 = false; }
                                        nomDoc = nomFichier;    
                                    }
                                    cheminDoc = destFile;
                                    if (test != 0)
                                    {
                                        int compteur=0;
                                        while (compteur < (test-3))
                                        {
                                            cheminDoc = @"E:\Copie spool\tempo" + compteur + ".SPL";
                                            destFile = @"E:\Copie spool\tempo" + compteur + ".SPL";
                                            switch (typeDoc.TrimStart())
                                            {
                                                case "DEVIS":
                                                    Devis dev = new Devis(); dev.lectureDevis(cheminDoc, profil);
                                                    break;
                                                case "BORDEREAU DE LIVRAISON":
                                                    BonLivraison BL = new BonLivraison(); BL.lectureBL(cheminDoc, profil);
                                                    break;
                                                case "BON D'ENLEVEMENT":
                                                    BonLivraison BL2 = new BonLivraison(); BL2.lectureBL(cheminDoc, profil);
                                                    break;
                                                case "BON DE PREPARATION":
                                                    BonPréparation BP = new BonPréparation(); BP.lectureBP(cheminDoc, profil);
                                                    break;
                                                case "COMMANDE ADHERENT":
                                                    AccuseReception AR = new AccuseReception(); AR.lectureAR(cheminDoc, profil);
                                                    break;
                                                case "BON DE COMMANDE FOURNISSEUR":
                                                    CommandeFournisseur CF = new CommandeFournisseur(); CF.lectureCF(cheminDoc, profil);
                                                    break;
                                                case "RETOUR FOURNISSEUR":
                                                    CommandeFournisseur rCF = new CommandeFournisseur(); rCF.lectureCF(cheminDoc, profil);
                                                    break;
                                                case "AVOIR":
                                                    BonLivraison BL3 = new BonLivraison(); BL3.lectureBL(cheminDoc, profil);
                                                    break;
                                                case "FACTURE":
                                                    Facturation FA = new Facturation(); FA.lectureFA(cheminDoc, profil);
                                                    break;
                                                case null:
                                                    break;
                                            }
                                            compteur++;
                                            System.IO.File.Delete(destFile);
                                        }
                                    }
                                    else
                                    {
                                        switch (typeDoc.TrimStart())
                                        {
                                            case "DEVIS":
                                                Devis dev = new Devis(); dev.lectureDevis(cheminDoc, profil);
                                                break;
                                            case "BORDEREAU DE LIVRAISON":
                                                BonLivraison BL = new BonLivraison(); BL.lectureBL(cheminDoc, profil);
                                                break;
                                            case "BON D'ENLEVEMENT":
                                                BonLivraison BL2 = new BonLivraison(); BL2.lectureBL(cheminDoc, profil);
                                                break;
                                            case "BON DE PREPARATION":
                                                BonPréparation BP = new BonPréparation(); BP.lectureBP(cheminDoc, profil);
                                                break;
                                            case "COMMANDE ADHERENT":
                                                AccuseReception AR = new AccuseReception(); AR.lectureAR(cheminDoc, profil);
                                                break;
                                            case "BON DE COMMANDE FOURNISSEUR":
                                                CommandeFournisseur CF = new CommandeFournisseur(); CF.lectureCF(cheminDoc, profil);
                                                break;
                                            case "RETOUR FOURNISSEUR":
                                                CommandeFournisseur rCF = new CommandeFournisseur(); rCF.lectureCF(cheminDoc, profil);
                                                break;
                                            case "AVOIR":
                                                BonLivraison BL3 = new BonLivraison(); BL3.lectureBL(cheminDoc, profil);
                                                break;
                                            case "FACTURE":
                                                Facturation FA = new Facturation(); FA.lectureFA(cheminDoc, profil);
                                                break;
                                            case null:
                                                break;
                                        }
                                        System.IO.File.Delete(destFile);
                                    }
                                }
                                else
                                {
                                    System.IO.File.Delete(destFile);
                                    break;
                                }
                                supOk = false;
                            }
                        }
                        catch (Exception e)
                        {
                            //Inscrit dans un fichier les differente erreur
                            LogHelper.WriteToFile(e.Message, "Imprimante " + nomDoc);
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
