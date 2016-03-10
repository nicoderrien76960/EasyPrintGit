using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections;
//using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PrinterForce;
using Ghostscript.NET.Processor;
using IBM.Data.DB2.iSeries;
using System.Data.Odbc;

namespace EssaiJobImp
{
    class ParseurFacturation : Facturation
    {
        private Dictionary<string, string> donneEntete;
        private Dictionary<string, string> donneeBody;
        private Dictionary<string, string> donneeFoot;
        private Dictionary<string, string> valeurTemplate;
        int iBody; int iFoot; string nomDoc; string unProfil;
        public ParseurFacturation(Dictionary<string, string> donneeEntete, Dictionary<string, string> donneeBody, Dictionary<string, string> donneeFoot, int iBody, int iFoot, string nomDoc, string profil)
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
            int nbCopie = 1;
            string cheminDocFinaux = ConfigurationManager.AppSettings["CheminDocFinaux"].ToString();
            string cheminRessources = ConfigurationManager.AppSettings["CheminRessources"].ToString();
            while (incCopie < nbCopie)
            {
                string chemin = "";
                if (!System.IO.Directory.Exists(cheminDocFinaux+"\\DocFinaux\\Facturation\\"+donneEntete["Client_code"]))
                {
                    System.IO.Directory.CreateDirectory(cheminDocFinaux + "\\DocFinaux\\Facturation\\" + donneEntete["Client_code"]);
                    chemin = cheminDocFinaux + "\\DocFinaux\\Facturation\\" + donneEntete["Client_code"] + "\\" + nomDoc + "_" + donneEntete["Client_code"] + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf";
                }else
                {
                    chemin = cheminDocFinaux + "\\DocFinaux\\Facturation\\" + donneEntete["Client_code"] + "\\" + nomDoc + "_" + donneEntete["Client_code"] + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf";
                }               
                Document nouveauDocument = new Document(PageSize.A4, 20, 20, 12, 2);
                PdfWriter writer = PdfWriter.GetInstance(nouveauDocument, new FileStream(chemin, FileMode.Create));     //Stockage du document
                //----------------------------------------
                //Constitution document PDF
                //----------------------------------------
                nouveauDocument.Open();
                PdfPTable tableau = new PdfPTable(2);
                tableau.TotalWidth = 550;
                tableau.LockedWidth = true;
                //-----------------Ajout Pattern/Image--------------------------------------------------------

                Image image5 = Image.GetInstance(ConfigurationManager.AppSettings["CheminPatternFondPageFacturation"]);
               // image5.ScaleAbsolute(PageSize.A4);
                image5.ScaleToFit(420, 800);
                image5.SetAbsolutePosition(90, 165);
                nouveauDocument.Add(image5);

                Image image6 = Image.GetInstance(ConfigurationManager.AppSettings["CheminLogoABCRDevis"]);
                //image5.ScaleAbsolute(PageSize.A4);
                image6.ScaleToFit(180, 190);
                image6.SetAbsolutePosition(10, 710);
                nouveauDocument.Add(image6); 
                
                
                
                
                /*Image image5 = Image.GetInstance(ConfigurationManager.AppSettings["CheminPatternFondPageFacturation"]);
                image5.Alignment = Image.UNDERLYING;
                image5.ScaleAbsolute(PageSize.A4);
                image5.ScalePercent(35, 36);
                image5.SetAbsolutePosition(-8, -9);
                nouveauDocument.Add(image5);*/
                //-------------------------------------------------------------------------------------------------
                //Encadré photo
                PdfPCell celulleHauteGauche = new PdfPCell(new Phrase("\n\n\n"));
                celulleHauteGauche.Border = PdfPCell.NO_BORDER;
                tableau.AddCell(celulleHauteGauche);

                //Celulle de droite contenant l'adresse de livraison
                Paragraph pAdl = new Paragraph();
                if (donneEntete.ContainsKey("Tiers_adl1"))
                {
                    if (donneEntete["Tiers_adl1"] == "")
                    {
                        pAdl.Add(new Phrase("\n    \n"));
                        pAdl.Add(new Phrase("\n" + donneEntete["Tiers_adf1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adf2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adf3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adf4"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adfcp"] + "   " + donneEntete["Tiers_adf6"], FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        //pAdl.Add(new Phrase("\n\nAdresse de facturation\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                    }
                    else
                    {
                        pAdl.Add(new Phrase("\n    \n"));
                        pAdl.Add(new Phrase("\n" + donneEntete["Tiers_adl1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl4"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adlcp"] + "   " + donneEntete["Tiers_adl6"], FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        //pAdl.Add(new Phrase("\n\nAdresse de facturation\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                    }
                }
                else
                {

                   /* pAdl.Add(new Phrase("\n    \n"));
                    pAdl.Add(new Phrase("\n" + " " + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase(" " + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase(" " + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase(" " + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase(" " + "   " + " ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    */
                    pAdl.Add(new Phrase("\n\n  \n"));
                    pAdl.Add(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                }
                PdfPCell celulleFinDroite = new PdfPCell(pAdl);
                celulleFinDroite.Bottom = PdfPCell.ALIGN_BOTTOM;
                celulleFinDroite.Rowspan = 2;
                celulleFinDroite.Border = PdfPCell.NO_BORDER;
                celulleFinDroite.PaddingLeft = 35;
                tableau.AddCell(celulleFinDroite);

                //Adresse ABCR
                Paragraph p = new Paragraph();
                PdfPCell celulleMilieuGauche = new PdfPCell(p);
                celulleMilieuGauche.Border = PdfPCell.NO_BORDER;
                tableau.AddCell(celulleMilieuGauche);


                //Tableau dans celulle bas gauche du tableau d'entete
                PdfPCell celulleBasGauche = new PdfPCell();
                Paragraph ptest = new Paragraph();
                ptest.Add(new Phrase("\n\n"));
                celulleBasGauche.AddElement(ptest);
                PdfPTable tabCell = new PdfPTable(3);
                tabCell.TotalWidth = 230;
                tabCell.LockedWidth = true;
                
                /*ND-14-10-15-deb*/
/*              tabCell.AddCell(new Phrase("Client", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                tabCell.AddCell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                tabCell.AddCell(new Phrase("Numéro", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));*/

                tabCell.AddCell(new Phrase("Client", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                tabCell.AddCell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                tabCell.AddCell(new Phrase("Numéro", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));

                /*ND-14-10-15-fin*/

                
               


                tabCell.AddCell(new Phrase(donneEntete["Client_code"], FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                tabCell.AddCell(new Phrase(donneEntete["Document_date"], FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                tabCell.AddCell(new Phrase(donneEntete["Document_numero"], FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                tabCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                celulleBasGauche.AddElement(tabCell);
                celulleBasGauche.Border = PdfPCell.NO_BORDER;
                celulleBasGauche.Bottom = PdfPCell.ALIGN_BOTTOM;
                Paragraph pCell2Entete = new Paragraph();
                int iEntete = 0;
                if (donneEntete.ContainsKey("Commentaire_texte0"))
                {
                    Phrase maPhrase = new Phrase();
                    pCell2Entete.Leading = 10;
                    while (donneEntete.ContainsKey("Commentaire_texte" + iEntete))
                    {
                        maPhrase = new Phrase(donneEntete["Commentaire_texte" + iEntete] + "\n", FontFactory.GetFont(FontFactory.COURIER, 7, Font.NORMAL)); 
                        pCell2Entete.Add(maPhrase);
                        iEntete++;
                    }
                }
                pCell2Entete.FirstLineIndent = 0;
                celulleBasGauche.AddElement(pCell2Entete);
                tableau.AddCell(celulleBasGauche);

                //Adresse facturation
                Paragraph pAdf = new Paragraph();
                pAdf.Add(new Phrase("\n\n\n\n"+donneEntete["Tiers_adf1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adf2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adf3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adf4"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adfcp"] + "   " + donneEntete["Tiers_adf6"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                PdfPCell celulleHauteDroite = new PdfPCell(pAdf);
                celulleHauteDroite.Border = PdfPCell.NO_BORDER;
                celulleHauteDroite.HorizontalAlignment = Element.ALIGN_LEFT;
                celulleHauteDroite.PaddingLeft = 35;
                tableau.AddCell(celulleHauteDroite);

                nouveauDocument.Add(tableau);      
                //Récap ref client et numéro de téléphone
                /*Paragraph refCli = new Paragraph();
                refCli.Add(new Phrase("Référence client " + donneeBody["Bon_rcl1"] + " du " + donneeBody["Bon_datrcl1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                nouveauDocument.Add(refCli);*/
                //Recap dessus tableau
                Phrase c = null;
                if (donneEntete.ContainsKey("Bon_vendeur_lib"))
                {
                    c = new Phrase("Vous avez été servi par : " + donneEntete["Bon_vendeur_lib"] + "                                                                                                                                       \n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.ITALIC));
                    nouveauDocument.Add(c);
                }
                else { c = new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.ITALIC)); nouveauDocument.Add(c); }
                Phrase pPage1 = new Phrase("\n                                                   " + donneEntete["Document_type"] + "                 " + donneEntete["Duplicata"] + "                                                                         Page n° 1           \n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD));
                pPage1.Leading = 10;
                nouveauDocument.Add(pPage1);
                //--------------------------------------------------------------------------------------------------------
                //                                      TABLEAU
                //-------------------------------------------------------------------------------------------------------
                CurseurTemplate ct = new CurseurTemplate();
                valeurTemplate = ct.chercher("FACTURE");

                float[] largeurs = { 
                                   int.Parse(valeurTemplate["Dimension1"]),
                                   int.Parse(valeurTemplate["Dimension2"]),
                                   int.Parse(valeurTemplate["Dimension3"]),
                                   int.Parse(valeurTemplate["Dimension4"]),
                                   int.Parse(valeurTemplate["Dimension5"]),
                                   int.Parse(valeurTemplate["Dimension6"]),
                                   int.Parse(valeurTemplate["Dimension7"]),
                                   int.Parse(valeurTemplate["Dimension8"])
                               };
                PdfPTable table = new PdfPTable(largeurs);
                table.TotalWidth = 565;                                                                                         //Chaque colonne crée ci dessus doit être rempli
                table.LockedWidth = true;
                PdfPCell cellET1 = new PdfPCell(new Phrase(donneEntete["Colonne_art"], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET1.Border = PdfPCell.NO_BORDER; //cellET1.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET1);
                PdfPCell cellET2 = new PdfPCell(new Phrase(donneEntete["Colonne_des"], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET2.Border = PdfPCell.NO_BORDER; //cellET2.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET2);
                PdfPCell cellET3 = new PdfPCell(new Phrase(donneEntete["Colonne_un"], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET3.Border = PdfPCell.NO_BORDER; //cellET3.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET3);
                PdfPCell cellET4 = new PdfPCell(new Phrase(donneEntete["Colonne_qt"], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET4.Border = PdfPCell.NO_BORDER; //cellET4.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET4);
                PdfPCell cellET5 = new PdfPCell(new Phrase("Prix remisé", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET5.Border = PdfPCell.NO_BORDER; //cellET5.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET5);
                PdfPCell cellET6 = new PdfPCell(new Phrase("Remise", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET6.Border = PdfPCell.NO_BORDER; //cellET6.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET6);
                PdfPCell cellET7 = new PdfPCell(new Phrase("Prix net", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET7.Border = PdfPCell.NO_BORDER; //cellET7.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET7);
                PdfPCell cellET8 = new PdfPCell(new Phrase("Montant HT", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET8.Border = PdfPCell.NO_BORDER; //cellET8.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET8);
                Image image3 = Image.GetInstance(ConfigurationManager.AppSettings["CheminPatternEnteteTableauFacture"]);
                image3.Alignment = Image.UNDERLYING;
                image3.SetAbsolutePosition(12.5f, 595);
                nouveauDocument.Add(image3);
                int i; int nbLigne = 0; float resultat = 0; float dimTab = 0; int décrement = 0; int numPage = 0;         //Constitution du tableau d'article
                bool okDési = false; bool okStart = false; double tempoTOT = 0; 
                
                /*if (donneeBody.ContainsKey("Bon_datliv1"))
                {
                    PdfPCell cell1 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell1);
                    Paragraph pCell2 = new Paragraph();
                    pCell2.Add(new Phrase("\nReference client "+donneeBody["Bon_rcl1"] +"           du "+donneeBody["Bon_datrcl1"]+ "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC)));
                    pCell2.Add(new Phrase("Bon n°" + donneeBody["Bon_numero1"] + " du " + donneeBody["Bon_date1"] +"  "+donneeBody["Bon_typvte1"]+"  "+donneeBody["Bon_datliv1"]+ "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC)));
                    pCell2.Add(new Phrase("Adresse de livraison " + donneEntete["Tiers_adf1"] + "  " + donneEntete["Tiers_adf2"] +"   "+donneEntete["Tiers_adf6"]+"   "+donneEntete["Tiers_adfcp"]+ "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC)));
                    PdfPCell cell2 = new PdfPCell(pCell2); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell2);
                    PdfPCell cell3 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell3);
                    PdfPCell cell4 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell4);
                    PdfPCell cell5 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell5);
                    PdfPCell cell6 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell6);
                    PdfPCell cell7 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell7);
                    PdfPCell cell8 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell8);
                }*/
                for (i = 1; i <= iBody; i++)
                {


                    /*ND-14-10-15-deb*/
                    /*if (donneeBody["Ligne_type" +i]=="BON")
                    {
                        nbLigne++;
                        PdfPCell cell1 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell1);
                        Paragraph pCell2 = new Paragraph();
                        pCell2.Add(new Phrase("\nReference client " + donneeBody["Bon_rcl"+i] + "           du " + donneeBody["Bon_datrcl"+i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC)));
                        pCell2.Add(new Phrase("Bon n°" + donneeBody["Bon_numero"+i] + " du " + donneeBody["Bon_date"+i] + "  " + donneeBody["Bon_typvte"+i] + "  " + donneeBody["Bon_datliv"+i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC)));
                        if (donneeBody.ContainsKey("Tiers_adl1"+i)) { pCell2.Add(new Phrase("Adresse de livraison " + donneeBody["Tiers_adl1" + i] + "  " + donneeBody["Tiers_adl2" + i] + "   " + donneeBody["Tiers_adl6" + i] + "   " + donneeBody["Tiers_adlcp" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC))); }
                        else { pCell2.Add(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8 , Font.BOLDITALIC))); }
                        PdfPCell cell2 = new PdfPCell(pCell2); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell2);
                        PdfPCell cell3 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell3);
                        PdfPCell cell4 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        PdfPCell cell8 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell8);
                    }*/
                    if (donneeBody["Ligne_type" + i] == "BON")
                    {
                        nbLigne++;
                        PdfPCell cell1 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell1);
                        Paragraph pCell2 = new Paragraph();
                        pCell2.Add(new Phrase("\nReference client " + donneeBody["Bon_rcl" + i] + "           du " + donneeBody["Bon_datrcl" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.ITALIC)));
                        pCell2.Add(new Phrase("Bon n°" + donneeBody["Bon_numero" + i] + " du " + donneeBody["Bon_date" + i] + "  " + donneeBody["Bon_typvte" + i] + "  " + donneeBody["Bon_datliv" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.ITALIC)));
                        if (donneeBody.ContainsKey("Tiers_adl1" + i)) { pCell2.Add(new Phrase("Adresse de livraison " + donneeBody["Tiers_adl1" + i] + "  " + donneeBody["Tiers_adl2" + i] + "   " + donneeBody["Tiers_adl6" + i] + "   " + donneeBody["Tiers_adlcp" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.ITALIC))); }
                        else { pCell2.Add(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.ITALIC))); }
                        PdfPCell cell2 = new PdfPCell(pCell2); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell2);
                        PdfPCell cell3 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell3);
                        PdfPCell cell4 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        PdfPCell cell8 = new PdfPCell(new Phrase(" \n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell8);
                    }

                    /*ND-14-10-15-fin*/


                    //Condition ARTICLE----------------------------------------------------------------------------------------------------------------------
                    /*ND-14-10-15-deb*/ 

                    /*if (donneeBody["Ligne_type" + i] == "ART")
                    {
                        nbLigne++;
                        string sPattern = "libelle" + i + "bis";
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
                                    pCell2.Add(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                    string clé = entry.Key;
                                    if (donneeBody.ContainsKey("Art_lot" + i)) { pCell2.Add(new Phrase("Numéro de lot : " + donneeBody["Art_lot" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7.5F, Font.BOLDITALIC)));
                                    okStart = true;
                                }
                                else
                                {
                                    string clé = entry.Key;
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7.5F, Font.BOLDITALIC)));
                                }
                                okDési = true;
                            }
                        }
                        if (okDési == false)
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell3);
                        }
                        else { table.AddCell(cell2); }
                        PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase(donneeBody["Art_remise2" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell(new Phrase(donneeBody["Art_remise1" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        double prixnet = -99999;
                        if (donneeBody["Art_prinet" + i] != "")
                        { prixnet = double.Parse(donneeBody["Art_prinet" + i]); }
                        if (prixnet != -99999)
                        {
                            PdfPCell cell8 = new PdfPCell(new Phrase(prixnet.ToString("N2") + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell8);
                        }
                        else
                        {
                            PdfPCell cell8 = new PdfPCell(new Phrase("" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell8);
                        }
                        PdfPCell cell9 = new PdfPCell(new Phrase(donneeBody["Art_monht" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell9.Border = PdfPCell.NO_BORDER; cell9.Border += PdfPCell.RIGHT_BORDER; cell9.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell9);
                        if (donneeBody["Art_monht" + i] != "")
                        {
                            tempoTOT = tempoTOT + double.Parse(donneeBody["Art_monht" + i]);
                        }
                        else { tempoTOT = tempoTOT + 0; }
                        okDési = false; okStart = false;
                    }
                    if (donneeBody["Ligne_type" + i] == "CDE")
                    {
                        nbLigne++;
                        string sPattern = "libelle" + i + "bis";
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
                                    pCell2.Add(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                    string clé = entry.Key;
                                    if (donneeBody.ContainsKey("Art_lot" + i)) { pCell2.Add(new Phrase("Numéro de lot : " + donneeBody["Art_lot" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
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
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
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
                    */

                    if (donneeBody["Ligne_type" + i] == "ART")
                    {
                        nbLigne++;
                        string sPattern = "libelle" + i + "bis";
                        PdfPCell cell1 = new PdfPCell(new Phrase(donneeBody["Art_code" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell1);
                        Paragraph pCell2 = new Paragraph();
                        PdfPCell cell2 = new PdfPCell(pCell2); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                        foreach (KeyValuePair<string, string> entry in donneeBody)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(entry.Key, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                if (okStart == false)
                                {
                                    pCell2.Add(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                                    string clé = entry.Key;
                                    if (donneeBody.ContainsKey("Art_lot" + i)) { pCell2.Add(new Phrase("Numéro de lot : " + donneeBody["Art_lot" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); }
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7.5F, Font.ITALIC)));
                                    okStart = true;
                                }
                                else
                                {
                                    string clé = entry.Key;
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7.5F, Font.ITALIC)));
                                }
                                okDési = true;
                            }
                        }
                        if (okDési == false)
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell3);
                        }
                        else { table.AddCell(cell2); }
                        PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase(donneeBody["Art_remise2" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell(new Phrase(donneeBody["Art_remise1" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        double prixnet = -99999;
                        if (donneeBody["Art_prinet" + i] != "")
                        { prixnet = double.Parse(donneeBody["Art_prinet" + i]); }
                        if (prixnet != -99999)
                        {
                            PdfPCell cell8 = new PdfPCell(new Phrase(prixnet.ToString("N2") + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell8);
                        }
                        else
                        {
                            PdfPCell cell8 = new PdfPCell(new Phrase("" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell8);
                        }
                        PdfPCell cell9 = new PdfPCell(new Phrase(donneeBody["Art_monht" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell9.Border = PdfPCell.NO_BORDER; cell9.Border += PdfPCell.RIGHT_BORDER; cell9.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell9);
                        if (donneeBody["Art_monht" + i] != "")
                        {
                            tempoTOT = tempoTOT + double.Parse(donneeBody["Art_monht" + i]);
                        }
                        else { tempoTOT = tempoTOT + 0; }
                        okDési = false; okStart = false;
                    }
                    if (donneeBody["Ligne_type" + i] == "CDE")
                    {
                        nbLigne++;
                        string sPattern = "libelle" + i + "bis";
                        PdfPCell cell1 = new PdfPCell(new Phrase(donneeBody["Art_code" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell1);
                        Paragraph pCell2 = new Paragraph();
                        PdfPCell cell2 = new PdfPCell(pCell2); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                        foreach (KeyValuePair<string, string> entry in donneeBody)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(entry.Key, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                if (okStart == false)
                                {
                                    pCell2.Add(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                                    string clé = entry.Key;
                                    if (donneeBody.ContainsKey("Art_lot" + i)) { pCell2.Add(new Phrase("Numéro de lot : " + donneeBody["Art_lot" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); }
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                                    okStart = true;
                                }
                                else
                                {
                                    string clé = entry.Key;
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                                }
                                okDési = true;
                            }
                        }
                        if (okDési == false)
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell3);
                        }
                        else { { table.AddCell(cell2); } }
                        PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase("" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        table.AddCell(cell6);
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell((new Phrase("En Commande\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        okDési = false; okStart = false;
                    }


                    /*ND-14-10-15-FIN*/

                    /*ND-19-10-15-deb*/ 
                    //Condition ARTICLE GRATUIT-----------------------------------------------------------------------------------------------------------------------------
                    /*if (donneeBody["Ligne_type" + i] == "GRA")
                    {
                        nbLigne++;
                        string sPattern = "libelle" + i + "bis";
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
                                    pCell2.Add(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                    string clé = entry.Key;
                                    if (donneeBody.ContainsKey("Art_lot" + i)) { pCell2.Add(new Phrase("Numéro de lot : " + donneeBody["Art_lot" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
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
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
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
                        PdfPCell cell7 = new PdfPCell(new Phrase("" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        PdfPCell cell8 = new PdfPCell((new Phrase(donneeBody["Lib_rempl_mt" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.LEFT_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER;
                        table.AddCell(cell8);
                    }
                     */


                    if (donneeBody["Ligne_type" + i] == "GRA")
                    {
                        nbLigne++;
                        string sPattern = "libelle" + i + "bis";
                        PdfPCell cell1 = new PdfPCell(new Phrase(donneeBody["Art_code" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell1);
                        Paragraph pCell2 = new Paragraph();
                        PdfPCell cell2 = new PdfPCell(pCell2); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                        foreach (KeyValuePair<string, string> entry in donneeBody)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(entry.Key, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                if (okStart == false)
                                {
                                    pCell2.Add(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                                    string clé = entry.Key;
                                    if (donneeBody.ContainsKey("Art_lot" + i)) { pCell2.Add(new Phrase("Numéro de lot : " + donneeBody["Art_lot" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); }
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                                    okStart = true;
                                }
                                else
                                {
                                    string clé = entry.Key;
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                                }
                                okDési = true;
                            }
                        }
                        if (okDési == false)
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell3);
                        }
                        else { { table.AddCell(cell2); } }
                        PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase("" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell(new Phrase("" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        PdfPCell cell8 = new PdfPCell((new Phrase(donneeBody["Lib_rempl_mt" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.LEFT_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER;
                        table.AddCell(cell8);
                    }
                    /*ND-FIN-19102015*/

                    /*ND-19-10-15-deb*/ 

                    //Condition COMMENTAIRE--------------------------------------------------------------------------------------------------------------------------------
                    /*if (donneeBody["Ligne_type" + i] == "COM")
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
                    if (donneeBody["Ligne_type" +i] =="ESC")
                    {
                        nbLigne++;
                        string sPattern = "libelle" + i + "bis";
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
                                    pCell2.Add(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                    string clé = entry.Key;
                                    if (donneeBody.ContainsKey("Art_lot" + i)) { pCell2.Add(new Phrase("Numéro de lot : " + donneeBody["Art_lot" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7.5F, Font.BOLDITALIC)));
                                    okStart = true;
                                }
                                else
                                {
                                    string clé = entry.Key;
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7.5F, Font.BOLDITALIC)));
                                }
                                okDési = true;
                            }
                        }
                        if (okDési == false)
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell3);
                        }
                        else { table.AddCell(cell2); }
                        PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        PdfPCell cell8 = new PdfPCell(new Phrase(" " + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell8);
                        PdfPCell cell9 = new PdfPCell(new Phrase(donneeBody["Art_monht" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell9.Border = PdfPCell.NO_BORDER; cell9.Border += PdfPCell.RIGHT_BORDER; cell9.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell9);
                        if (donneeBody["Art_monht" + i] != "")
                        {
                            tempoTOT = tempoTOT + double.Parse(donneeBody["Art_monht" + i]);
                        }
                        else { tempoTOT = tempoTOT + 0; }
                        okDési = false; okStart = false;
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
*/
                    if (donneeBody["Ligne_type" + i] == "COM")
                    {
                        nbLigne++;
                        PdfPCell cellVide = new PdfPCell(new Phrase("" + "\n"));
                        PdfPCell cell = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
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
                    if (donneeBody["Ligne_type" + i] == "ESC")
                    {
                        nbLigne++;
                        string sPattern = "libelle" + i + "bis";
                        PdfPCell cell1 = new PdfPCell(new Phrase(donneeBody["Art_code" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell1);
                        Paragraph pCell2 = new Paragraph();
                        PdfPCell cell2 = new PdfPCell(pCell2); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                        foreach (KeyValuePair<string, string> entry in donneeBody)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(entry.Key, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                if (okStart == false)
                                {
                                    pCell2.Add(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                                    string clé = entry.Key;
                                    if (donneeBody.ContainsKey("Art_lot" + i)) { pCell2.Add(new Phrase("Numéro de lot : " + donneeBody["Art_lot" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); }
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7.5F, Font.ITALIC)));
                                    okStart = true;
                                }
                                else
                                {
                                    string clé = entry.Key;
                                    pCell2.Add(new Phrase(donneeBody[clé] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7.5F, Font.ITALIC)));
                                }
                                okDési = true;
                            }
                        }
                        if (okDési == false)
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                            table.AddCell(cell3);
                        }
                        else { table.AddCell(cell2); }
                        PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell4);
                        PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell5);
                        PdfPCell cell6 = new PdfPCell(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell6);
                        PdfPCell cell7 = new PdfPCell(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell7);
                        PdfPCell cell8 = new PdfPCell(new Phrase(" " + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell8);
                        PdfPCell cell9 = new PdfPCell(new Phrase(donneeBody["Art_monht" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8))); cell9.Border = PdfPCell.NO_BORDER; cell9.Border += PdfPCell.RIGHT_BORDER; cell9.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell9);
                        if (donneeBody["Art_monht" + i] != "")
                        {
                            tempoTOT = tempoTOT + double.Parse(donneeBody["Art_monht" + i]);
                        }
                        else { tempoTOT = tempoTOT + 0; }
                        okDési = false; okStart = false;
                    }
                    PdfPCell cellEcartDroite = new PdfPCell(new Phrase(" " + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 2)));

                    PdfPCell cellEcart = new PdfPCell(new Phrase(" " + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 2)));
                    cellEcart.Border = PdfPCell.NO_BORDER;
                    cellEcart.Border += PdfPCell.LEFT_BORDER;
                    cellEcart.Border += PdfPCell.RIGHT_BORDER;
                    cellEcartDroite.Border = PdfPCell.NO_BORDER;
                    cellEcartDroite.Border += PdfPCell.RIGHT_BORDER;
                    cellEcartDroite.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cellEcartDroite); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart);

                    /*Fin ND 19 10 2015*/



                    //--------------------------------------------GESTION DU SAUT DE PAGE-------------------------------------------------------------------------------------------
                    float temp = table.TotalHeight;
                    dimTab = temp;
                    if (dimTab >= 380 && i < iBody)
                    {
                        //Saut de page  
                        numPage++;
                        PdfPCell cellFin = new PdfPCell(new Phrase(" "));
                        PdfPCell cellBlanche = new PdfPCell(new Phrase(" "));
                        PdfPCell cellBlancheDA = new PdfPCell(new Phrase("\n\nA Reporter", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.ITALIC)));  
                        PdfPCell cellBlancheD = new PdfPCell(new Phrase("\n\n"+tempoTOT.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.ITALIC)));
                        cellFin.Colspan = 8;
                        cellBlancheDA.Bottom = PdfPCell.ALIGN_BOTTOM;
                        cellBlancheD.Bottom = PdfPCell.ALIGN_BOTTOM;
                        cellBlanche.FixedHeight = (440 - dimTab);
                        cellBlanche.Border = PdfPCell.NO_BORDER;
                        cellBlanche.Border += PdfPCell.RIGHT_BORDER;
                        cellBlanche.Border += PdfPCell.LEFT_BORDER;
                        cellBlancheDA.FixedHeight = (440 - dimTab);
                        cellBlancheDA.Border = PdfPCell.NO_BORDER;
                        cellBlancheDA.Border += PdfPCell.LEFT_BORDER;
                        cellBlancheDA.Border += PdfPCell.RIGHT_BORDER;
                        cellBlancheD.FixedHeight = (440 - dimTab);
                        cellBlancheD.Border = PdfPCell.NO_BORDER;
                        cellBlancheD.Border += PdfPCell.LEFT_BORDER;
                        cellBlancheD.Border += PdfPCell.RIGHT_BORDER;
                        cellFin.Border = PdfPCell.NO_BORDER;
                        cellFin.Border += PdfPCell.TOP_BORDER;
                        table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlancheDA); table.AddCell(cellBlancheD);
                        table.AddCell(cellFin);
                        nouveauDocument.Add(table);//----------------------------------------------------------------------------Repère ligne en dessous--------------------------------------------------
                        Phrase pReport = new Phrase("                                                                                                                                                             A REPORTER\n\n\n\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD));
                        Phrase pPage = new Phrase("\n                                                   " + donneEntete["Document_type"] + "                 " + donneEntete["Duplicata"] + "                                                                         Page n° " + (numPage + 1) + "            ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD));
                        pPage.Leading = 15;
                        nouveauDocument.Add(pReport);
                        table.DeleteBodyRows();
                        nouveauDocument.Add(Chunk.NEXTPAGE);
                        nouveauDocument.Add(tableau);
                        //nouveauDocument.Add(p);                                         
                        //nouveauDocument.Add(refCli);
                        if (donneEntete.ContainsKey("Bon_vendeur_lib"))
                        {
                            c = new Phrase("Vous avez été servi par : " + donneEntete["Bon_vendeur_lib"] + "                                                                                                                                       ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.ITALIC));
                            nouveauDocument.Add(c);
                        }
                        else { c = new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.ITALIC)); nouveauDocument.Add(c); }
                        nouveauDocument.Add(pPage);
                        //Image image5 = Image.GetInstance("E:\\FiligraneAR.png");//Changer lien pattern
                        nouveauDocument.Add(image5);
                        nouveauDocument.Add(image6); 
                        image3.SetAbsolutePosition(12.5f, 595);
                        nouveauDocument.Add(image3);
                        table.AddCell(cellET1); table.AddCell(cellET2); table.AddCell(cellET3); table.AddCell(cellET4); table.AddCell(cellET5); table.AddCell(cellET6); table.AddCell(cellET7); table.AddCell(cellET8);
                        dimTab = 0;
                        décrement = (i - 1);
                    }
                    //FinGestionSautPage
                }
                //Gestion Commentaires de bon

                /*DEB ND 19 10 2015*/
                /*
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
                    cellCommentaireBon.Border += PdfPCell.LEFT_BORDER;
                    cellComBlancheD.Border = PdfPCell.NO_BORDER;
                    cellComBlancheD.Border += PdfPCell.LEFT_BORDER;
                    cellComBlancheD.Border += PdfPCell.RIGHT_BORDER;
                    table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                    table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                    table.AddCell(cellComBlanche); table.AddCell(cellCommentaireBon); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                }*/

                if (donneEntete.ContainsKey("Commentaire_texte"))
                {
                    PdfPCell cellComBlanche = new PdfPCell(new Phrase(" "));
                    PdfPCell cellComBlancheD = new PdfPCell(new Phrase(" "));
                    PdfPCell cellCommentaireBon = new PdfPCell(new Phrase(donneEntete["Commentaire_texte"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));

                    cellComBlanche.Border = PdfPCell.NO_BORDER;
                    cellComBlanche.Border += PdfPCell.RIGHT_BORDER;
                    cellComBlanche.Border += PdfPCell.LEFT_BORDER;
                    cellCommentaireBon.Border = PdfPCell.NO_BORDER;
                    cellCommentaireBon.Border += PdfPCell.RIGHT_BORDER;
                    cellCommentaireBon.Border += PdfPCell.LEFT_BORDER;
                    cellComBlancheD.Border = PdfPCell.NO_BORDER;
                    cellComBlancheD.Border += PdfPCell.LEFT_BORDER;
                    cellComBlancheD.Border += PdfPCell.RIGHT_BORDER;
                    table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                    table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                    table.AddCell(cellComBlanche); table.AddCell(cellCommentaireBon); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                }

                /*FIN ND 19 10 2015*/
                //----------------------------------------------Gestion Code Camion--------------------------------------------------------------------------------------------------------------------------------
                /*if (donneEntete.ContainsKey("Camion_code") && donneEntete["Camion_code"] != "ENV")
                {
                    PdfPCell cellComBlanche = new PdfPCell(new Phrase(" "));
                    PdfPCell cellComBlancheD = new PdfPCell(new Phrase(" "));
                    PdfPCell cellCommentaireBon = new PdfPCell(new Phrase("Code Camion              " + donneEntete["Camion_code"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));

                    cellComBlanche.Border = PdfPCell.NO_BORDER;
                    cellComBlanche.Border += PdfPCell.RIGHT_BORDER;
                    cellCommentaireBon.Border = PdfPCell.NO_BORDER;
                    cellCommentaireBon.Border += PdfPCell.RIGHT_BORDER;
                    cellComBlancheD.Border = PdfPCell.NO_BORDER;
                    cellComBlancheD.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                    table.AddCell(cellComBlanche); table.AddCell(cellCommentaireBon); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlanche); table.AddCell(cellComBlancheD);
                }*/
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
                    resultat = 410 - resultat;            //<<----------450 correspond au nombre de point de la longueur du tableau, c'est la valeur à modifier pour modifier la taille du tableau

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
                table.SpacingAfter = -16;
                nouveauDocument.Add(table);
                //-----------------------------------------------------------------------------------------------------
                //                          PIED DE PAGE
                //-----------------------------------------------------------------------------------------------------

                /*DEBUT ND 19 10 2015 */


               /* float[] largeursPied = { 15 , 20, 16, 16, 16, 16, 11 };
                PdfPTable tableauPied = new PdfPTable(largeursPied);
                tableauPied.TotalWidth = 565;
                tableauPied.LockedWidth = true;
                tableauPied.SpacingBefore = 0;

                int dimDocPied = 5; int dimactuDocPied=0;
                tableauPied.AddCell(new Phrase("NATURE", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                if (donneeFoot.ContainsKey("Base_tva_nature2")) { tableauPied.AddCell(new Phrase("TVA 20%", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC))); dimactuDocPied++; }
                if (donneeFoot.ContainsValue("FDG")) { tableauPied.AddCell(new Phrase("FDG", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC))); dimactuDocPied++; }
                if (donneeFoot.ContainsValue("EXO")) { tableauPied.AddCell(new Phrase("EXO", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC))); dimactuDocPied++; } 
                while(dimactuDocPied<dimDocPied)
                {
                    tableauPied.AddCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                    dimactuDocPied++;
                }

                if (donneeFoot.ContainsValue("TOT")) { tableauPied.AddCell(new Phrase("Total", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                PdfPCell cellP = new PdfPCell();
                cellP.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cellP.AddElement(new Phrase("BASE\nTAUX\nMONTANT\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                tableauPied.AddCell(cellP);
                int iTempo = 2; int iTotal = 0; 
                while (iTempo<(dimDocPied+2))
                {
                    if (donneeFoot.ContainsKey("Base_tva_mht" + iTempo))
                    {
                        if (donneeFoot["Base_tva_code"+iTempo] != "TOT")
                        {
                            PdfPCell celltempo = new PdfPCell();
                            celltempo.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                            celltempo.AddElement(new Phrase(donneeFoot["Base_tva_mht" + iTempo] + "\n" + donneeFoot["Base_tva_taux"+iTempo] + "\n" + donneeFoot["Base_tva_mtva"+iTempo], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC)));
                            tableauPied.AddCell(celltempo);
                        }
                        else
                        {
                            iTotal = iTempo;
                            tableauPied.AddCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                        }
                    }
                    else  {tableauPied.AddCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                    iTempo++;
                }
                PdfPCell cellTotal = new PdfPCell();
                cellTotal.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cellTotal.AddElement(new Phrase(donneeFoot["Base_tva_mht"+iTotal] +"\n"+ donneeFoot["Base_tva_taux"+iTotal] + "\n" + donneeFoot["Base_tva_mtva"+iTotal], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                tableauPied.AddCell(cellTotal);
                tableauPied.AddCell(new PdfPCell(new Phrase("")));
                iTempo = 2; iTotal = 0;
                while (iTempo < (dimDocPied + 2))
                {
                    if (donneeFoot.ContainsKey("Base_tva_mht" + iTempo))
                    {
                        if (donneeFoot["Base_tva_code" + iTempo] != "TOT")
                        {
                            PdfPCell celltempo = new PdfPCell();
                            celltempo.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                            celltempo.AddElement(new Phrase(donneeFoot["Base_tva_mttc" + iTempo], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLDITALIC)));
                            tableauPied.AddCell(celltempo);
                        }
                        else
                        {
                            iTotal = iTempo;
                            tableauPied.AddCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                        }
                    }
                    else { tableauPied.AddCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                    iTempo++;
                }
                PdfPCell cellTotal2 = new PdfPCell();
                cellTotal2.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cellTotal2.AddElement(new Phrase(donneeFoot["Base_tva_mttc" + iTotal] , FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                tableauPied.AddCell(cellTotal2);
                Paragraph echeance = new Paragraph();
                echeance.SpacingBefore = 2f;
                echeance.MultipliedLeading = 0.75f;
                if (donneeFoot.ContainsKey("Echeance_date" + (iTotal + 1)))
                {
                    echeance.Add(new Phrase("Echéance   : " + donneeFoot["Echeance_date" + (iTotal + 1)], FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    echeance.Add(new Phrase("\t \t \t                                                                                          Net à payer : " + donneeFoot["Base_tva_mttc" + iTotal] + "€\n", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                    if (donneeFoot["Loi_sapin"]!=" "){echeance.Add(new Phrase(donneeFoot["Loi_sapin"], FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));}
                    if (donneeFoot.ContainsKey("Reglement_mode"))
                    {
                        if (donneeFoot["Reglement_mode"] != "Traite")
                        {
                            echeance.Add(new Phrase("% à régler : " + donneeFoot["Echeance_pour" + (iTotal + 1)] + "\n ", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                        }
                        else
                        {
                            echeance.Add(new Phrase("\nRéglement par   " + donneeFoot["Traite"] + "        " + donneeFoot["Acceptation"] + "        " + donneeFoot["Domiciliation"], FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                        }
                    }
                    nouveauDocument.Add(tableauPied);
                    nouveauDocument.Add(echeance);
                }
                else
                {
                    echeance.Add(new Phrase(donneeFoot["Loi_sapin"]+"\n ", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    echeance.Add(new Phrase("\t \t \t                                                                                                                            Votre avoir " + donneeFoot["Pied_net"] + "€\n", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                    nouveauDocument.Add(tableauPied);
                    nouveauDocument.Add(echeance);
                } 

                //PAPILLON
                float[] largeursPapillon = { 55, 55 };
                PdfPTable tabPapillon = new PdfPTable(largeursPapillon);
                tabPapillon.TotalWidth = 595;
                tabPapillon.LockedWidth = true;
                PdfPCell cellPapillonB = new PdfPCell();
                cellPapillonB.AddElement(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA,2,Font.NORMAL)));
                cellPapillonB.Border = PdfPCell.NO_BORDER;
                tabPapillon.AddCell(cellPapillonB);
                //tabPapillon.AddCell(new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA,2,Font.NORMAL)))).Border=PdfPCell.NO_BORDER;
                PdfPCell cellClientPapillon = new PdfPCell();
                cellClientPapillon.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cellClientPapillon.HorizontalAlignment = PdfPCell.ALIGN_BOTTOM;
                cellClientPapillon.Border = PdfPCell.NO_BORDER;
                cellClientPapillon.AddElement(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.BOLD)));
                tabPapillon.AddCell(cellClientPapillon);
                tabPapillon.AddCell(new PdfPCell(new Phrase(" "))).Border = PdfPCell.NO_BORDER;
                PdfPTable papillon = new PdfPTable(4);
                papillon.TotalWidth = 280;
                papillon.SpacingBefore = 0;
                papillon.LockedWidth = true;
                papillon.AddCell(new Phrase("Client", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                papillon.AddCell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                papillon.AddCell(new Phrase("N° Facture", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                papillon.AddCell(new Phrase("Montant TTC", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                papillon.AddCell(new Phrase(donneEntete["Client_code"], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                papillon.AddCell(new Phrase(donneEntete["Document_date"], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                papillon.AddCell(new Phrase(donneEntete["Document_numero"], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                papillon.AddCell(new Phrase(donneeFoot["Pied_net"], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                PdfPCell cellpapillon = new PdfPCell();
                cellpapillon.Colspan = 4;
                cellpapillon.AddElement(new Phrase("       PAPILLON A JOINDRE A VOTRE REGLEMENT", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLDITALIC)));
                PdfPCell cp = new PdfPCell();
                cp.Border = PdfPCell.NO_BORDER;
                if (donneeFoot.ContainsKey("Echeance_date" + (iTotal + 1)))
                {
                    cp.AddElement(new Phrase("\n                          " + donneEntete["Tiers_adf1"], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                }
                else { cp.AddElement(new Phrase("\n\n                          " + donneEntete["Tiers_adf1"], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                //cp.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cp.AddElement(papillon);
                papillon.AddCell(cellpapillon);
                papillon.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                
                tabPapillon.AddCell(cp);
                nouveauDocument.Add(tabPapillon);
                nouveauDocument.Close();
                incCopie++;
                */
                float[] largeursPied = { 15, 20, 16, 16, 16, 16, 11 };
                PdfPTable tableauPied = new PdfPTable(largeursPied);
                tableauPied.TotalWidth = 565;
                tableauPied.LockedWidth = true;
                tableauPied.SpacingBefore = 0;

                int dimDocPied = 5; int dimactuDocPied = 0;
                tableauPied.AddCell(new Phrase("NATURE", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                if (donneeFoot.ContainsKey("Base_tva_nature2")) { tableauPied.AddCell(new Phrase("TVA 20%", FontFactory.GetFont(FontFactory.HELVETICA, 8))); dimactuDocPied++; }
                if (donneeFoot.ContainsValue("FDG")) { tableauPied.AddCell(new Phrase("FDG", FontFactory.GetFont(FontFactory.HELVETICA, 8 ))); dimactuDocPied++; }
                if (donneeFoot.ContainsValue("EXO")) { tableauPied.AddCell(new Phrase("EXO", FontFactory.GetFont(FontFactory.HELVETICA, 8))); dimactuDocPied++; }
                while (dimactuDocPied < dimDocPied)
                {
                    tableauPied.AddCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                    dimactuDocPied++;
                }

                if (donneeFoot.ContainsValue("TOT")) { tableauPied.AddCell(new Phrase("Total", FontFactory.GetFont(FontFactory.HELVETICA, 8))); }
                PdfPCell cellP = new PdfPCell();
                cellP.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cellP.AddElement(new Phrase("BASE\nTAUX\nMONTANT\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                tableauPied.AddCell(cellP);
                int iTempo = 2; int iTotal = 0;
                while (iTempo < (dimDocPied + 2))
                {
                    if (donneeFoot.ContainsKey("Base_tva_mht" + iTempo))
                    {
                        if (donneeFoot["Base_tva_code" + iTempo] != "TOT")
                        {
                            PdfPCell celltempo = new PdfPCell();
                            celltempo.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                            celltempo.AddElement(new Phrase(donneeFoot["Base_tva_mht" + iTempo] + "\n" + donneeFoot["Base_tva_taux" + iTempo] + "\n" + donneeFoot["Base_tva_mtva" + iTempo], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tableauPied.AddCell(celltempo);
                        }
                        else
                        {
                            iTotal = iTempo;
                            tableauPied.AddCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                        }
                    }
                    else { tableauPied.AddCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8))); }
                    iTempo++;
                }
                PdfPCell cellTotal = new PdfPCell();
                cellTotal.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cellTotal.AddElement(new Phrase(donneeFoot["Base_tva_mht" + iTotal] + "\n" + donneeFoot["Base_tva_taux" + iTotal] + "\n" + donneeFoot["Base_tva_mtva" + iTotal], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                tableauPied.AddCell(cellTotal);
                tableauPied.AddCell(new PdfPCell(new Phrase("")));
                iTempo = 2; iTotal = 0;
                while (iTempo < (dimDocPied + 2))
                {
                    if (donneeFoot.ContainsKey("Base_tva_mht" + iTempo))
                    {
                        if (donneeFoot["Base_tva_code" + iTempo] != "TOT")
                        {
                            PdfPCell celltempo = new PdfPCell();
                            celltempo.VerticalAlignment = PdfPCell.ALIGN_LEFT;
                            celltempo.AddElement(new Phrase(donneeFoot["Base_tva_mttc" + iTempo], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            tableauPied.AddCell(celltempo);
                        }
                        else
                        {
                            iTotal = iTempo;
                            tableauPied.AddCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                        }
                    }
                    else { tableauPied.AddCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8))); }
                    iTempo++;
                }
                PdfPCell cellTotal2 = new PdfPCell();
                cellTotal2.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cellTotal2.AddElement(new Phrase(donneeFoot["Base_tva_mttc" + iTotal], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                tableauPied.AddCell(cellTotal2);
                Paragraph echeance = new Paragraph();
                echeance.SpacingBefore = 2f;
                echeance.MultipliedLeading = 0.75f;
                if (donneeFoot.ContainsKey("Echeance_date" + (iTotal + 1)))
                {
                    echeance.Add(new Phrase("Echéance   : " + donneeFoot["Echeance_date" + (iTotal + 1)], FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    echeance.Add(new Phrase("\t \t \t                                                                                          Net à payer : " + donneeFoot["Base_tva_mttc" + iTotal] + "€\n", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                    if (donneeFoot["Loi_sapin"] != " ") { echeance.Add(new Phrase(donneeFoot["Loi_sapin"], FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL))); }
                    if (donneeFoot.ContainsKey("Reglement_mode"))
                    {
                        if (donneeFoot["Reglement_mode"] != "Traite")
                        {
                            echeance.Add(new Phrase("% à régler : " + donneeFoot["Echeance_pour" + (iTotal + 1)] + "\n ", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                        }
                        else
                        {
                            echeance.Add(new Phrase("\nRéglement par   " + donneeFoot["Traite"] + "        " + donneeFoot["Acceptation"] + "        " + donneeFoot["Domiciliation"], FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                        }
                    }
                    nouveauDocument.Add(tableauPied);
                    nouveauDocument.Add(echeance);
                }
                else
                {
                    echeance.Add(new Phrase(donneeFoot["Loi_sapin"] + "\n ", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    echeance.Add(new Phrase("\t \t \t                                                                                                                            Votre avoir " + donneeFoot["Pied_net"] + "€\n", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                    nouveauDocument.Add(tableauPied);
                    nouveauDocument.Add(echeance);
                }

                //PAPILLON
                float[] largeursPapillon = { 55, 55 };
                PdfPTable tabPapillon = new PdfPTable(largeursPapillon);
                tabPapillon.TotalWidth = 595;
                tabPapillon.LockedWidth = true;
                PdfPCell cellPapillonB = new PdfPCell();
                cellPapillonB.AddElement(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 2, Font.NORMAL)));
                cellPapillonB.Border = PdfPCell.NO_BORDER;
                tabPapillon.AddCell(cellPapillonB);
                //tabPapillon.AddCell(new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA,2,Font.NORMAL)))).Border=PdfPCell.NO_BORDER;
                PdfPCell cellClientPapillon = new PdfPCell();
                cellClientPapillon.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cellClientPapillon.HorizontalAlignment = PdfPCell.ALIGN_BOTTOM;
                cellClientPapillon.Border = PdfPCell.NO_BORDER;
                cellClientPapillon.AddElement(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.BOLD)));
                tabPapillon.AddCell(cellClientPapillon);
                tabPapillon.AddCell(new PdfPCell(new Phrase(" "))).Border = PdfPCell.NO_BORDER;
                PdfPTable papillon = new PdfPTable(4);
                papillon.TotalWidth = 280;
                papillon.SpacingBefore = 0;
                papillon.LockedWidth = true;
                papillon.AddCell(new Phrase("Client", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                papillon.AddCell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                papillon.AddCell(new Phrase("N° Facture", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                papillon.AddCell(new Phrase("Montant TTC", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                papillon.AddCell(new Phrase(donneEntete["Client_code"], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                papillon.AddCell(new Phrase(donneEntete["Document_date"], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                papillon.AddCell(new Phrase(donneEntete["Document_numero"], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                papillon.AddCell(new Phrase(donneeFoot["Pied_net"], FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                PdfPCell cellpapillon = new PdfPCell();
                cellpapillon.Colspan = 4;
                cellpapillon.AddElement(new Phrase("       PAPILLON A JOINDRE A VOTRE REGLEMENT", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLDITALIC)));
                PdfPCell cp = new PdfPCell();
                cp.Border = PdfPCell.NO_BORDER;
                if (donneeFoot.ContainsKey("Echeance_date" + (iTotal + 1)))
                {
                    cp.AddElement(new Phrase("\n                          " + donneEntete["Tiers_adf1"], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                }
                else { cp.AddElement(new Phrase("\n\n                          " + donneEntete["Tiers_adf1"], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); }
                //cp.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cp.AddElement(papillon);
                papillon.AddCell(cellpapillon);
                papillon.HorizontalAlignment = PdfPCell.ALIGN_LEFT;

                tabPapillon.AddCell(cp);
                nouveauDocument.Add(tabPapillon);
                nouveauDocument.Close();
                incCopie++;
                
                /*FIN ND 19 10 2015*/
                //--------------------------------------------------COPIE GED--------------------------------------------------------
                try
                {
                    String connectionString = ConfigurationManager.AppSettings["ChaineDeConnexionBase"];
                    OdbcConnection conn = new OdbcConnection(connectionString);
                    conn.Open();
                    string requete = "select T1.NOCLI c1 , T1.NOMCL c2 from B00C0ACR.AMAGESTCOM.ACLIENL1 T1 where T1.NOCLI = '" + donneEntete["Client_code"] + "'";
                    OdbcCommand act = new OdbcCommand(requete, conn);
                    OdbcDataReader act0 = act.ExecuteReader();
                    string nomADH = "";
                    while (act0.Read())
                    {
                        nomADH = (act0.GetString(1));
                    }
                    conn.Close();
                    if (!System.IO.Directory.Exists(ConfigurationManager.AppSettings["cheminGED"] + "\\" + donneEntete["Client_code"] + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\Facturation\\"))
                    {
                        System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["cheminGED"] + "\\" + donneEntete["Client_code"] + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM").ToUpperInvariant() + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\Facturation\\");
                        System.IO.File.Copy(chemin, ConfigurationManager.AppSettings["cheminGED"] + "\\" + donneEntete["Client_code"] + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM").ToUpperInvariant() + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\Facturation\\" + "\\"+ nomDoc + "_" + donneEntete["Client_code"] + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf");
                    }
                    else
                    {
                        System.IO.File.Copy(chemin, ConfigurationManager.AppSettings["cheminGED"] + "\\" + donneEntete["Client_code"] + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM").ToUpperInvariant() + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\Facturation\\" + nomDoc + "_" + donneEntete["Client_code"] + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf");
                    }
                }
                catch (Exception e)
                {
                    LogHelper.WriteToFile(e.Message, "ENVOI GED Facture");
                }
                //----------------------------------------------FIN COPIE----------------------------------------------------------
                //Requette qui retourne le champ "OUI/NON" envoi mail facture
                String connectionString2 = ConfigurationManager.AppSettings["ChaineDeConnexionBase"];
                OdbcConnection conn2 = new OdbcConnection(connectionString2);
                conn2.Open();
                //Requete de séléction sur le champ "envoi facture par mail"
                string requete2 = "select T1.NOCLI c1 , T1.CLID5 c2 , T1.RENDI c3 , T1.PROFE c4 from B00C0ACR.AMAGESTCOM.ACLIENL1 T1 where T1.CLID5 = 'OUI'";
                OdbcCommand act2 = new OdbcCommand(requete2, conn2);
                OdbcDataReader act20 = null;
                bool effectuerImpression=true;
                try
                {
                    act20 = act2.ExecuteReader();
                    while (act20.Read())
                    {
                        if(act20.GetString(0)==donneEntete["Client_code"])// Si le code client est égale au résultat de la requete sur la ligne lu "NOCLI"
                        {
                            if(act20.GetString(1)=="OUI")                                               //Si la ligne de l'enrigistrement dans la base est à OUI pour cet ID, alors ne pas imprimer
                            { effectuerImpression = false; }
                        }
                    }
                }
                catch (Exception e) { LogHelper.WriteToFile(e.Message, "Erreur de connexion à la base, rupture d'impression"); }
                if (effectuerImpression == true)
                {
                    int nbImp = 0; int nbImpOK = 0;
                    string[] printer = new string[20]; // tableau qui contient les imprimantes du profil d'impression
                    ProfilImprimante profil = new ProfilImprimante();
                    profil.chargementXML("Facture");     // chargement selon le type de doc
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
                                switches.Add("-dNumCopies=" + ConfigurationManager.AppSettings["NbCopieGC"]);
                                switches.Add("-sDEVICE=" + ConfigurationManager.AppSettings["PiloteImpressionFacture"]);
                                //switches.Add("-r360x360");//Pilote d'impression
                                switches.Add("-sOutputFile=%printer%" + printerName);
                                switches.Add("-f");
                                switches.Add(inputFile);

                                processor.StartProcessing(switches.ToArray(), null);
                            }
                            nbImpOK++;
                        }
                        catch (Exception e)
                        { LogHelper.WriteToFile(e.Message, "ParseurBP" + donneEntete["Document_numero"].Trim()); }
                        // incrément à chaque impression terminée
                    }
                }
                /*Mail m = new Mail();
                m.remplirDictionnaire();
                m.comparerDocument(donneEntete["Client_code"]);*/
            }
        }
    }
}
