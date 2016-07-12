﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using PrinterForce;
using Ghostscript.NET.Processor;
using IBM.Data.DB2.iSeries;
using System.Data.Odbc;
using System.Text.RegularExpressions; /*regex le 100316*/

namespace Ireport_Rubis
{
    class ParseurCF : CommandeFournisseur
    {
        
        private Dictionary<string, string> donneEntete;
        private Dictionary<string, string> donneeBody;
        private Dictionary<string, string> donneeFoot;
        private Dictionary<string, string> valeurTemplate;
        int iBody; int iFoot; string nomDoc; string unProfil;
        public ParseurCF(Dictionary<string, string>donneeEntete, Dictionary<string, string>donneeBody, Dictionary<string,string>donneeFoot, int iBody, int iFoot, string nomDoc, string profil)
        {
            this.donneEntete = donneeEntete;
            this.donneeBody = donneeBody;           //Constructeur qui récupère les données de l'objet qui l'appel
            this.donneeFoot = donneeFoot;
            this.iBody = iBody;
            this.iFoot = iFoot;
            this.nomDoc = nomDoc;
            this.unProfil = profil;
        }
        public void miseEnForm(string typeDoc)
        {
            int incCopie = 0;
            int nbCopie = int.Parse(donneEntete["Nombre_copies"]);
            string cheminDocFinaux = ConfigurationManager.AppSettings["CheminDocFinaux"].ToString();
            string cheminRessources = ConfigurationManager.AppSettings["CheminRessources"].ToString();
            while (incCopie < nbCopie)
            {
                string chemin = cheminDocFinaux+"\\DocFinaux\\CF\\CF_" + nomDoc + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf";
                Document nouveauDocument = new Document(PageSize.A4, 20, 20, 12, 20);
                PdfWriter.GetInstance(nouveauDocument, new FileStream(chemin, FileMode.Create));     //Stockage du document
                //----------------------------------------
                //Constitution document PDF
                //----------------------------------------
                nouveauDocument.Open();
                PdfPTable tableau = new PdfPTable(2);
                tableau.TotalWidth = 550;
                tableau.LockedWidth = true;
                //-----------------Ajout Pattern/Image--------------------------------------------------------
                Image image2 = Image.GetInstance(ConfigurationManager.AppSettings["CheminPatternHautDroiteBp"]);
                image2.Alignment = Image.UNDERLYING;

                float x2 = float.Parse(ConfigurationManager.AppSettings["rectangleReferenceCFX"]);
                float y2 = float.Parse(ConfigurationManager.AppSettings["rectangleReferenceCFY"]);
                image2.SetAbsolutePosition(x2, y2);
                //image2.SetAbsolutePosition(325, 760);
                nouveauDocument.Add(image2);




                Image image5 = Image.GetInstance(ConfigurationManager.AppSettings["CheminFilligraneCf"]);
                image5.Alignment = Image.UNDERLYING;
                float x5 = float.Parse(ConfigurationManager.AppSettings["filigraneCFX"]);
                float y5 = float.Parse(ConfigurationManager.AppSettings["filigraneCFY"]);
                image5.SetAbsolutePosition(x5, y5);
                //image5.SetAbsolutePosition(200, 250);
                nouveauDocument.Add(image5);
                //-------------------------------------------------------------------------------------------------



                Image image = Image.GetInstance(ConfigurationManager.AppSettings["CheminLogoABCR_CF"]);
                image.ScaleAbsolute(PageSize.A4);
                float x = float.Parse(ConfigurationManager.AppSettings["LargeurLogoABCR_CF"]);
                float y = float.Parse(ConfigurationManager.AppSettings["HauteurLogoABCR_CF"]);
                image.ScaleAbsolute(x, y);
                Paragraph pLogo = new Paragraph();
                //Image image = Image.GetInstance(ConfigurationManager.AppSettings["CheminLogoABCR"]);
                pLogo.Add(image);       
                
                //Encadré photo
                PdfPCell celulleHauteGauche = new PdfPCell(image);
                celulleHauteGauche.Border = PdfPCell.NO_BORDER;
                tableau.AddCell(celulleHauteGauche);
                //Celulle de droite contenant l'adresse de livraison
                Paragraph pAdl = new Paragraph();
                pAdl.Add(new Phrase("Adresse de dépot fournisseur\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                pAdl.Add(new Phrase(donneEntete["Tiers_adl1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdl.Add(new Phrase(donneEntete["Tiers_adl2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdl.Add(new Phrase(donneEntete["Tiers_adl3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdl.Add(new Phrase(donneEntete["Tiers_adl4"] + "   " + donneEntete["Tiers_adl5"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdl.Add(new Phrase("\n\nAdresse de facturation\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                PdfPCell celulleFinDroite = new PdfPCell(pAdl);
                celulleFinDroite.Rowspan = 2;
                celulleFinDroite.Border = PdfPCell.NO_BORDER;
                celulleFinDroite.PaddingLeft = 35;
                tableau.AddCell(celulleFinDroite);
                /*---------------------------*/
                //Adresse ABCR
                /*  string tel=""; string fax="";
                  if (donneEntete["Adresse_interne_5"] == "")
                  { tel = donneEntete["Adresse_interne_7"]; fax = donneEntete["Adresse_interne_8"]; }
                  else
                  { tel = donneEntete["Adresse_interne_5"]; fax = donneEntete["Adresse_interne_6"]; }
                  tel = tel.Substring(3, 15); fax = fax.Substring(3, 15);
                  Paragraph p = new Paragraph();
                  p.Add(new Phrase(donneEntete["Adresse_interne_2"] + "      Tél  " + tel + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                  p.Add(new Phrase(donneEntete["Adresse_interne_3"] + "    Fax " + fax + "\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                  */
                /*bug recup tel et fax 100316 nd*/

                //Adresse ABCR
                string tel = ""; string fax = "";
                if (donneEntete["Adresse_interne_5"] == "")
                {
                    tel = donneEntete["Adresse_interne_7"];
                    fax = donneEntete["Adresse_interne_8"];

                }
                else
                {
                    tel = donneEntete["Adresse_interne_5"];
                    fax = donneEntete["Adresse_interne_6"];

                }
                /*ND utilisation des regex pour supprimer fax et tel*/
                var fax2 = Regex.Split(fax, @"(?<=FAX)");
                var tel2 = Regex.Split(tel, @"(?<=TEL)");

                Paragraph p = new Paragraph();
                p.Add(new Phrase(donneEntete["Adresse_interne_2"] + "      Tél  " + tel2[1] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                p.Add(new Phrase(donneEntete["Adresse_interne_3"] + "    Fax " + fax2[1] + "\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));


                PdfPCell celulleMilieuGauche = new PdfPCell(p);
                celulleMilieuGauche.Border = PdfPCell.NO_BORDER;
                tableau.AddCell(celulleMilieuGauche);
                
                //Tableau dans celulle bas gauche du tableau d'entete
                PdfPCell celulleBasGauche = new PdfPCell();
                PdfPTable tabCell = new PdfPTable(3);
                tabCell.TotalWidth = 230;
                tabCell.LockedWidth = true;
                tabCell.AddCell(new Phrase("Fournisseur", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                tabCell.AddCell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                tabCell.AddCell(new Phrase("N° CDE", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                //Condition si code client dans doc ou code tiers---------------------------------------------------------------------------------
                if (donneEntete.ContainsKey("Tiers_code"))
                { tabCell.AddCell(new Phrase(donneEntete["Tiers_code"], FontFactory.GetFont(FontFactory.HELVETICA, 9))); }
                else
                { tabCell.AddCell(new Phrase(donneEntete["Client_code"], FontFactory.GetFont(FontFactory.HELVETICA, 9))); }
                //Fin de test condition------------------------------------------------------------------------------------------------------------------
                tabCell.AddCell(new Phrase(donneEntete["Document_date"], FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                tabCell.AddCell(new Phrase(donneEntete["Document_numero"], FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                tabCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                celulleBasGauche.AddElement(tabCell);
                celulleBasGauche.Border = PdfPCell.NO_BORDER;
                tableau.AddCell(celulleBasGauche);
                //Adresse de facturation
                Paragraph pAdf = new Paragraph();
                pAdf.Add(new Phrase(donneEntete["Tiers_adf1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adf2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adf3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adfcp"] + "   " + donneEntete["Tiers_adf4"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                PdfPCell celulleHauteDroite = new PdfPCell(pAdf);
                celulleHauteDroite.Border = PdfPCell.NO_BORDER;
                celulleHauteDroite.HorizontalAlignment = Element.ALIGN_LEFT;
                celulleHauteDroite.PaddingLeft = 35;
                tableau.AddCell(celulleHauteDroite);
                nouveauDocument.Add(tableau);
                //Récap ref client et numéro de téléphone
                Paragraph refCli = new Paragraph();
                refCli.Add(new Phrase(donneEntete["Lib_datliv"] +" "+ donneEntete["Ent_datliv"] +"       Age : "+donneEntete["Agence_libelle"]+"          \n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                nouveauDocument.Add(refCli);
                //Recap dessus tableau
                Chunk c = new Chunk("Ser : "+donneEntete["Service_libelle"]+"                                                                                                                            Exprimé en : EUR\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.ITALIC));  
                nouveauDocument.Add(c);
                Phrase pPage1 = new Phrase("                       " + donneEntete["Document_type"] + "                 " + donneEntete["Duplicata"] + "                                                     Page n° 1           \n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD));
                pPage1.Leading = 10; 
                nouveauDocument.Add(pPage1);
                //--------------------------------------------------------------------------------------------------------
                //                                      TABLEAU
                //----------------------------------------------------------------------------------------------------
                CurseurTemplate ct = new CurseurTemplate();
                valeurTemplate = ct.chercher("CF");

                float[] largeurs = { 
                                   int.Parse(valeurTemplate["Dimension1"]),
                                   int.Parse(valeurTemplate["Dimension2"]),
                                   int.Parse(valeurTemplate["Dimension3"]),
                                   int.Parse(valeurTemplate["Dimension4"]),
                                   int.Parse(valeurTemplate["Dimension5"]),
                                   int.Parse(valeurTemplate["Dimension6"]),
                                   int.Parse(valeurTemplate["Dimension7"]),
                                   int.Parse(valeurTemplate["Dimension8"]),
                               };  
                PdfPTable table = new PdfPTable(largeurs);
                table.TotalWidth = 555;                                                                                         //Chaque colonne crée ci dessus doit être rempli
                table.LockedWidth = true;
                PdfPCell cellET1 = new PdfPCell(new Phrase("Article\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET1.Border = PdfPCell.NO_BORDER; //cellET1.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET1);
                PdfPCell cellET2 = new PdfPCell(new Phrase("Désignation\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET2.Border = PdfPCell.NO_BORDER; //cellET2.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET2);
                PdfPCell cellET3 = new PdfPCell(new Phrase("UV\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET3.Border = PdfPCell.NO_BORDER; //cellET3.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET3);
                PdfPCell cellET4 = new PdfPCell(new Phrase("Quantité\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET4.Border = PdfPCell.NO_BORDER; //cellET4.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET4);
                PdfPCell cellET5 = new PdfPCell(new Phrase("Prix brut\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET5.Border = PdfPCell.NO_BORDER; //cellET5.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET5);
                PdfPCell cellET6 = new PdfPCell(new Phrase("Remise\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET6.Border = PdfPCell.NO_BORDER; //cellET6.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET6);
                PdfPCell cellET7 = new PdfPCell(new Phrase("Prix net\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET7.Border = PdfPCell.NO_BORDER; //cellET7.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET7);
                PdfPCell cellET8 = new PdfPCell(new Phrase("Montant HT\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET8.Border = PdfPCell.NO_BORDER; //cellET8.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET8);

                Image image3 = Image.GetInstance(ConfigurationManager.AppSettings["CheminPatternTableau"]);
                image3.Alignment = Image.UNDERLYING;
                float x3 = float.Parse(ConfigurationManager.AppSettings["bandeauTableauCFX"]);
                float y3 = float.Parse(ConfigurationManager.AppSettings["bandeauTableauCFY"]);
                image3.SetAbsolutePosition(x3, y3);
                //image3.SetAbsolutePosition(20, 610);
                nouveauDocument.Add(image3);


                int i; int nbLigne = 0; float resultat = 0; float dimTab = 0; int décrement = 0; int numPage = 0;         //Constitution du tableau d'article
                bool okDési = false; bool okStart = false;
                for (i = 1; i <= iBody; i++)
                {
                    //Condition ARTICLE----------------------------------------------------------------------------------------------------------------------
                    if (donneeBody["Ligne_type" + i] == "ART")
                    {
                        nbLigne++;
                        string sPattern = "Info_type" + i +"bis";
                        PdfPCell cell1 = new PdfPCell(new Phrase(donneeBody["Art_code" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell1);
                        Paragraph pCell2 = new Paragraph();
                        PdfPCell cell2 = new PdfPCell(pCell2); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                        pCell2.Add(new Phrase(donneeBody["Designation" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); /*cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;*/
                        //table.AddCell(cell3);
                        string patternLib = "(SPE|DIM|CDI)";
                        foreach (KeyValuePair<string, string> entry in donneeBody)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(entry.Key, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                if (okStart == false)
                                {
                                    if (false == System.Text.RegularExpressions.Regex.IsMatch(entry.Value, patternLib, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                    {
                                        pCell2.Add(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                        okStart = true;
                                    }          
                                }
                                else
                                {
                                    if (System.Text.RegularExpressions.Regex.IsMatch(entry.Value, "DE", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                    {
                                        string clé = entry.Key;
                                        pCell2.Add(new Phrase(donneeBody["Libelle" + i + "bis"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                    }
                                }
                                okDési = true;
                            }
                            if ((System.Text.RegularExpressions.Regex.IsMatch(entry.Value, "DIM", System.Text.RegularExpressions.RegexOptions.IgnoreCase))&& System.Text.RegularExpressions.Regex.IsMatch(entry.Key, "info_type"+i+"bis", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                if (donneeBody["Longueur" + i] != "") { pCell2.Add(new Phrase("Longueur : " + donneeBody["Longueur" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                                if (donneeBody["Largeur" + i] != "") { pCell2.Add(new Phrase("Largeur : " + donneeBody["Largeur" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                                if (donneeBody["Diametre" + i] != "") { pCell2.Add(new Phrase("Diametre : " + donneeBody["Diametre" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                                if (donneeBody["Hauteur" + i] != "") { pCell2.Add(new Phrase("Hauteur : " + donneeBody["Hauteur" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                            }
                            if ((System.Text.RegularExpressions.Regex.IsMatch(entry.Value, "SPE", System.Text.RegularExpressions.RegexOptions.IgnoreCase)) && System.Text.RegularExpressions.Regex.IsMatch(entry.Key, "info_type" + i + "bis", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                pCell2.Add(new Phrase("Client "+donneeBody["Spe_nom" + i] + " Bon n°" + donneeBody["Spe_cde"+i] + " Réf : " + donneeBody["Spe_ref"+i], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                            }
                        }
                        if (okDési == false)
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Designation" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell3);
                        }
                        else { table.AddCell(cell2); }
                        PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase(donneeBody["Art_pubrut" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell(new Phrase(donneeBody["Art_remise1" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        PdfPCell cell8 = new PdfPCell(new Phrase(donneeBody["Art_prinet" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell8);
                        PdfPCell cell9 = new PdfPCell(new Phrase(donneeBody["Art_montant" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell9.Border = PdfPCell.NO_BORDER; cell9.Border += PdfPCell.RIGHT_BORDER; cell9.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell9);
                        okDési = false; okStart = false;
                    }
                    if (donneeBody["Ligne_type" + i] == "CDE") // <------- Insérer gestion Dimension (voir longueur/hauteur/ect...)
                    {
                        nbLigne++;
                        string sPattern = "designation" + i + "bis";
                        PdfPCell cell1 = new PdfPCell(new Phrase(donneeBody["Art_code" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell1);
                        Paragraph pCell2 = new Paragraph();
                        PdfPCell cell2 = new PdfPCell(pCell2); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                        foreach (KeyValuePair<string, string> entry in donneeBody)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(entry.Key, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                if (okStart == false)
                                {
                                    pCell2.Add(new Phrase(donneeBody["Designation" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                    string clé = entry.Key;
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                    okStart = true;
                                }
                                else
                                {
                                    string clé = entry.Key;
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                }
                                okDési = true;
                            }
                        }
                        if (okDési == false)
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Designation" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell3);
                        }
                        else { { table.AddCell(cell2); } }
                        PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase("" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        table.AddCell(cell6);
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell((new Phrase("En Commande\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        okDési = false; okStart = false;
                    }
                    //Condition ARTICLE GRATUIT-----------------------------------------------------------------------------------------------------------------------------
                    if (donneeBody["Ligne_type" + i] == "GRA")
                    {
                        nbLigne++;
                        string sPattern = "designation" + i + "bis";
                        PdfPCell cell1 = new PdfPCell(new Phrase(donneeBody["Art_code" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell1);
                        foreach (KeyValuePair<string, string> entry in donneeBody)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(entry.Key, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                PdfPCell cell2 = new PdfPCell(new Phrase(donneeBody["Designation" + i] + "\n" + donneeBody["Designation" + i + "bis"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                                okDési = true;
                                table.AddCell(cell2);
                            }
                        }
                        if (okDési == false)
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Designation" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell3);
                        }
                        PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase("" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell((new Phrase(donneeBody["Lib_rempl_mt" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        PdfPCell cell8 = new PdfPCell(new Phrase("" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.LEFT_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER;
                        table.AddCell(cell8);
                    }
                    //Condition COMMENTAIRE--------------------------------------------------------------------------------------------------------------------------------
                    if (donneeBody["Ligne_type" + i] == "COM")
                    {
                        nbLigne++;
                        PdfPCell cellVide = new PdfPCell(new Phrase("" + "\n"));
                        PdfPCell cell = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                        PdfPCell cellFin = new PdfPCell();
                        cellVide.Border = PdfPCell.NO_BORDER;
                        cellVide.Border += PdfPCell.RIGHT_BORDER;
                        cellVide.Border += PdfPCell.LEFT_BORDER;
                        cell.Border = PdfPCell.NO_BORDER;
                        cell.Border += PdfPCell.RIGHT_BORDER;
                        cell.Border += PdfPCell.LEFT_BORDER;
                        cellFin.Border = PdfPCell.NO_BORDER;
                        cellFin.Border += PdfPCell.LEFT_BORDER;
                        cellFin.Border += PdfPCell.RIGHT_BORDER;
                        table.AddCell(cellVide);
                        table.AddCell(cell);
                        table.AddCell(cellVide);
                        table.AddCell(cellVide);
                        table.AddCell(cellVide);
                        table.AddCell(cellVide);
                        table.AddCell(cellVide);
                        table.AddCell(cellFin);
                    }
                    PdfPCell cellEcartDroite = new PdfPCell(new Phrase(" " + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 2, Font.BOLD)));

                    PdfPCell cellEcart = new PdfPCell(new Phrase(" " + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 2, Font.BOLD)));
                    cellEcart.Border = PdfPCell.NO_BORDER;
                    cellEcart.Border += PdfPCell.LEFT_BORDER;
                    cellEcart.Border += PdfPCell.RIGHT_BORDER;
                    cellEcartDroite.Border = PdfPCell.NO_BORDER;
                    cellEcartDroite.Border += PdfPCell.RIGHT_BORDER;
                    cellEcartDroite.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cellEcartDroite); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart);

                    //--------------------------------------------GESTION DU SAUT DE PAGE-------------------------------------------------------------------------------------------
                    float temp = table.TotalHeight;
                    dimTab = temp;
                    if (dimTab >= 410 && i<iBody)
                    {
                        //Saut de page
                        numPage++;
                        PdfPCell cellFin = new PdfPCell(new Phrase(" "));
                        PdfPCell cellBlanche = new PdfPCell(new Phrase(" "));
                        PdfPCell cellBlancheD = new PdfPCell(new Phrase(" "));
                        cellFin.Colspan = 8;
                        cellBlanche.FixedHeight = (450 - dimTab);
                        cellBlanche.Border = PdfPCell.NO_BORDER;
                        cellBlanche.Border += PdfPCell.RIGHT_BORDER;
                        cellBlanche.Border += PdfPCell.LEFT_BORDER;
                        cellBlancheD.FixedHeight = (450 - dimTab);
                        cellBlancheD.Border = PdfPCell.NO_BORDER;
                        cellBlancheD.Border += PdfPCell.LEFT_BORDER;
                        cellBlancheD.Border += PdfPCell.RIGHT_BORDER;
                        cellFin.Border = PdfPCell.NO_BORDER;
                        cellFin.Border += PdfPCell.TOP_BORDER;
                        table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlancheD);
                        table.AddCell(cellFin);
                        nouveauDocument.Add(table);//----------------------------------------------------------------------------Repère ligne en dessous--------------------------------------------------
                        Phrase pReport = new Phrase("                                                                                                                                                             A REPORTER\n\n\n\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD));
                        Phrase pPage = new Phrase("                       " + donneEntete["Document_type"] + "                 " + donneEntete["Duplicata"] + "                                                          Page n° " + (numPage+1)+"\n\n        ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD));
                        nouveauDocument.Add(pReport);
                        table.DeleteBodyRows();
                        nouveauDocument.Add(Chunk.NEXTPAGE);
                        nouveauDocument.Add(tableau);
                        //nouveauDocument.Add(p);
                        nouveauDocument.Add(refCli);
                        nouveauDocument.Add(c);
                        nouveauDocument.Add(pPage);
                        nouveauDocument.Add(image5);
                        image3.SetAbsolutePosition(x3, y3-5);
                        nouveauDocument.Add(image3);
                        nouveauDocument.Add(image2);
                        table.AddCell(cellET1); table.AddCell(cellET2); table.AddCell(cellET3); table.AddCell(cellET4); table.AddCell(cellET5); table.AddCell(cellET6); table.AddCell(cellET7); table.AddCell(cellET8);
                        dimTab = 0;
                        décrement = (i - 1);
                    }
                    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                }
                //----------------------------------------Gestion des commentaires de bon----------------------------------------------------------------------------------------------------
                if (donneEntete.ContainsKey("Commentaire_texte"))
                {
                    PdfPCell cellComBlanche = new PdfPCell(new Phrase(" "));
                    PdfPCell cellComBlancheD = new PdfPCell(new Phrase(" "));
                    PdfPCell cellCommentaireBon = new PdfPCell(new Phrase(donneEntete["Commentaire_texte"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));

                    cellComBlanche.Border = PdfPCell.NO_BORDER;
                    cellComBlanche.Border += PdfPCell.RIGHT_BORDER;
                    cellComBlanche.Border += PdfPCell.LEFT_BORDER;
                    cellCommentaireBon.Border = PdfPCell.NO_BORDER;
                    cellCommentaireBon.Border += PdfPCell.RIGHT_BORDER;
                    cellComBlancheD.Border = PdfPCell.NO_BORDER;
                    cellComBlancheD.Border += PdfPCell.LEFT_BORDER;
                    cellComBlancheD.Border += PdfPCell.RIGHT_BORDER;
                    table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                    table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                    table.AddCell(cellComBlanche); table.AddCell(cellCommentaireBon); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                }
                //----------------------------------------------Gestion Code Camion--------------------------------------------------------------------------------------------------------------------------------
                if (donneEntete.ContainsKey("Camion_code") && donneEntete["Camion_code"] != "ENV")
                {
                    PdfPCell cellComBlanche = new PdfPCell(new Phrase(" "));
                    PdfPCell cellComBlancheD = new PdfPCell(new Phrase(" "));
                    PdfPCell cellCommentaireBon = new PdfPCell(new Phrase("Code Camion              " + donneEntete["Camion_code"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));

                    cellComBlanche.Border = PdfPCell.NO_BORDER;
                    cellComBlanche.Border += PdfPCell.RIGHT_BORDER;
                    cellComBlanche.Border += PdfPCell.LEFT_BORDER;
                    cellCommentaireBon.Border = PdfPCell.NO_BORDER;
                    cellCommentaireBon.Border += PdfPCell.RIGHT_BORDER;
                    cellComBlancheD.Border = PdfPCell.NO_BORDER;
                    cellComBlancheD.Border += PdfPCell.LEFT_BORDER;
                    cellComBlancheD.Border += PdfPCell.RIGHT_BORDER;
                    table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                    table.AddCell(cellComBlanche); table.AddCell(cellCommentaireBon); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                }
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                int b;                                          //----------------------------------------------------------------
                for (b = 0; b <= i; b++)                        //
                {                                               //              Compteur dimension du tableau
                    float temp = table.TotalHeight;             //
                    resultat = temp;                            //
                }                                               //-------------------------------------------------------------------
                if (i > iBody)
                {
                    PdfPCell cellFin = new PdfPCell(new Phrase(" "));
                    PdfPCell cellBlanche = new PdfPCell(new Phrase(" "));
                    PdfPCell cellBlancheD = new PdfPCell(new Phrase(" "));
                    cellFin.Colspan = 8;
                    resultat = 450 - resultat;            //<<----------450 correspond au nombre de point de la longueur du tableau, c'est la valeur à modifier pour modifier la taille du tableau

                    cellBlanche.FixedHeight = resultat;
                    cellBlanche.Border = PdfPCell.NO_BORDER;
                    cellBlanche.Border += PdfPCell.RIGHT_BORDER;
                    cellBlanche.Border += PdfPCell.LEFT_BORDER;
                    cellBlancheD.FixedHeight = resultat;
                    cellBlancheD.Border = PdfPCell.NO_BORDER;
                    cellBlancheD.Border += PdfPCell.LEFT_BORDER;
                    cellBlancheD.Border += PdfPCell.RIGHT_BORDER;
                    cellFin.Border = PdfPCell.NO_BORDER;
                    cellFin.Border += PdfPCell.TOP_BORDER;
                    table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlancheD);
                    table.AddCell(cellFin);
                }
                //-----------------Ajout Pattern bas de page---------------------------------------------------------
                Image image4 = Image.GetInstance(ConfigurationManager.AppSettings["CheminPatternTotBl"]);
                image4.Alignment = Image.UNDERLYING;

                float x4 = float.Parse(ConfigurationManager.AppSettings["rectangleTotalCFX"]);
                float y4 = float.Parse(ConfigurationManager.AppSettings["rectangleTotalCFY"]);

                image4.SetAbsolutePosition(x4, y4);
                //image4.SetAbsolutePosition(385, 130);
                nouveauDocument.Add(image4);
                nouveauDocument.Add(table);
                //-----------------------------------------------------------------------------------------------------
                //                          PIED DE PAGE
                //-------------------------------------------------------------------------------------------------------
                PdfPTable tableauPied = new PdfPTable(3);
                tableauPied.TotalWidth = 555;
                tableauPied.LockedWidth = true;

                Chunk chrono = new Chunk("Mode de réglement : " + donneeFoot["Mode_reglement"] + "\nDate d'échéance   : " + donneeFoot["Lib_echeance"]+"\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
                PdfPCell cellulePied = new PdfPCell(new Phrase(chrono));
                cellulePied.Colspan = 2;
                cellulePied.Border = PdfPCell.NO_BORDER;
                tableauPied.AddCell(cellulePied);

                string euro = "€";
                if (donneeFoot["Montant_bon"] == "") { euro = " "; }
                PdfPTable tableauTot = new PdfPTable(1);
                PdfPCell cellTTot = new PdfPCell(new Phrase("Montant HT : " + donneeFoot["Montant_bon"] + " " + euro, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellTTot.Border = PdfPCell.NO_BORDER; 
                tableauTot.AddCell(cellTTot);
                PdfPCell cellTot = new PdfPCell(tableauTot);
                cellTot.Border = PdfPCell.NO_BORDER;
                tableauPied.AddCell(cellTot);
                nouveauDocument.Add(tableauPied);
                //-------------------------------------Cartouche bas de page---------------------------------------------------
                float[] largeursCartouche = { 12, 50, 25 };
                PdfPTable cartouche = new PdfPTable(largeursCartouche);
                cartouche.TotalWidth = 555;
                cartouche.LockedWidth = true;
                PdfPCell cellFoot1 = new PdfPCell(new Phrase(donneeFoot["Lieu_liv_Lib"] + " : ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD))); cellFoot1.Border = PdfPCell.NO_BORDER;
                PdfPCell cellFoot2 = new PdfPCell(new Phrase(donneeFoot["Lieu_liv_ad1"] + "\n" + donneeFoot["Lieu_liv_ad2"] + donneeFoot["Lieu_liv_ad3"] + "\n" + donneeFoot["Lieu_liv_ad4"] + donneeFoot["Lieu_liv_ad5"] + "\n" + donneeFoot["Lieu_liv_adcp"] + " " + donneeFoot["Lieu_liv_ad6"] + "\nTél . " + donneeFoot["Lieu_liv_tel"] + "\nFax . " + donneeFoot["Lieu_liv_fax"], FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD))); cellFoot2.Border = PdfPCell.NO_BORDER;
                PdfPCell cellFoot3 = new PdfPCell(new Phrase("")); cellFoot3.Border = PdfPCell.NO_BORDER;
                cartouche.AddCell(cellFoot1);
                cartouche.AddCell(cellFoot2);
                cartouche.AddCell(cellFoot3);
               
                nouveauDocument.Add(cartouche);
                Phrase phraseFin = new Phrase("\n"+ConfigurationManager.AppSettings["TexteBasCartouche"], FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD));
                nouveauDocument.Add(phraseFin);
                //--------------------------------------------------------------------------------------------------------------
                nouveauDocument.Close();
                incCopie++;

                //Copie Doc dans GED
                try
                {
                    String connectionString = ConfigurationManager.AppSettings["ChaineDeConnexionBase"];
                    OdbcConnection conn = new OdbcConnection(connectionString);
                    conn.Open();
                    string requete = "select T1.CFCLI c1 , T1.CFBON c2 , T2.NOMCL c3 from (B00C0ACR.AMAGESTCOM.ADETBOP1 T3 LEFT OUTER JOIN B00C0ACR.AMAGESTCOM.ACLIENL1 T2 on T3.NOCLI = T2.NOCLI) LEFT OUTER JOIN B00C0ACR.AMAGESTCOM.ACFDETP1 T1 on T3.NOFOU = T1.NOFOU and T3.SFOUR = T1.SFOUR and T3.NBOFO = T1.CFBON and T3.NLIFO = T1.CFLIG where T1.CFBON = '" + donneEntete["Document_numero"] + "'";
                    OdbcCommand act = new OdbcCommand(requete, conn);
                    OdbcDataReader act0 = act.ExecuteReader();
                    string nomADH = ""; 
                    string codeClient = "";
                    while (act0.Read())
                    {
                        nomADH = (act0.GetString(2));
                        codeClient = (act0.GetString(0));
                    }
                    conn.Close();
                    if (nomADH != "")
                    {
                        if (!System.IO.Directory.Exists(ConfigurationManager.AppSettings["cheminGEDCF"] + "\\" + codeClient + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\CF\\"))
                        {
                            System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["cheminGEDCF"] + "\\" + codeClient + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM").ToUpperInvariant() + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\CF\\");
                            System.IO.File.Copy(chemin, ConfigurationManager.AppSettings["cheminGEDCF"] + "\\" + codeClient + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM").ToUpperInvariant() + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\CF\\" + "\\CF_" + nomDoc + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf");
                        }
                        else
                        {
                            System.IO.File.Copy(chemin, ConfigurationManager.AppSettings["cheminGEDCF"] + "\\" + codeClient + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM").ToUpperInvariant() + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\CF\\" + "\\CF_" + nomDoc + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf");
                        }
                    }
                }
                catch
                {
                    LogHelper.WriteToFile("Erreur d'écriture dans la GED", "PARSEURCF");
                }

                //--------------------------------------------------------FIN COPIE------------------------------------------------------
           
                int nbImp = 0; int nbImpOK = 0;
                string[] printer = new string[20]; // tableau qui contient les imprimantes du profil d'impression
                ProfilImprimante profil = new ProfilImprimante();
                profil.chargementXML("CF");     // chargement selon le type de doc
                string vendeur = unProfil.Substring(2, 3);
                vendeur = vendeur.TrimEnd();
                var listeProfil = profil.getDonneeProfil();
                try
                {
                    foreach (string v in listeProfil[vendeur])      //lecture des imprimantes liée à un profil
                    {
                        printer[nbImp] = v.ToString();
                        nbImp++;                                    //on incrémente le nombre d'impression à executer
                    }
                }
                catch
                {
                    printer[nbImp] = ConfigurationManager.AppSettings["ImpDef"];                 //Imprimante par defaut (essai)
                    nbImp++;
                }
                nbImp = nbImp - 1;
                while (nbImpOK <= nbImp)                        // boucle tant que le nombre d'impression fait n'à pas atteint le nombre d'impression demander
                {
                    string printerName = printer[nbImpOK];
                    string inputFile = String.Format(@"{0}", chemin);
                    try
                    {
                        //Envoi de l'ordre d'impression vers l'imprimante, les "switches" sont des arguments de la ligne de script "processor" de type GhostscriptProcessor
                        using (GhostscriptProcessor processor = new GhostscriptProcessor())
                        {
                             List<string> switches = new List<string>();
                             switches.Add("-empty");
                             switches.Add("-dPrinted");
                             switches.Add("-dBATCH");
                             switches.Add("-dNOPAUSE");
                             switches.Add("-dNOSAFER");
                             switches.Add("-dNumCopies="+ConfigurationManager.AppSettings["NbCopieGC"]);
                             switches.Add("-sDEVICE="+ConfigurationManager.AppSettings["PiloteImpressionGC"]);
                             switches.Add("-sOutputFile=%printer%" + printerName);
                             switches.Add("-f");
                             switches.Add(inputFile);

                             processor.StartProcessing(switches.ToArray(), null);

                           

                        }
                        nbImpOK++;
                    }
                    catch (Exception e)
                    { LogHelper.WriteToFile(e.Message, "Parseur CF" + donneEntete["Document_numero"].Trim()); }
                    // incrément à chaque impression terminée
                }
            }
        }
    }
}
