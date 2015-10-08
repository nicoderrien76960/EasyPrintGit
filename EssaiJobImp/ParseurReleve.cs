using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PrinterForce;
using Ghostscript.NET.Processor;
using IBM.Data.DB2.iSeries;
using System.Data.Odbc;
using System.Configuration;
using System.IO;

namespace EssaiJobImp
{
    class ParseurReleve
    {
        private Dictionary<string, string> donneEntete;
        private Dictionary<string, string> donneeBody;
        private Dictionary<string, string> donneeFoot;
        private Dictionary<string, string> valeurTemplate;
        int iBody; int iFoot; string nomDoc; string unProfil;
        public ParseurReleve(Dictionary<string, string> donneeEntete, Dictionary<string, string> donneeBody, Dictionary<string, string> donneeFoot, int iBody, int iFoot, string nomDoc, string profil)
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
                if (!System.IO.Directory.Exists(cheminDocFinaux + "\\DocFinaux\\Releve\\" + donneEntete["Tiers_nocpt"].Substring(3,6)))
                {
                    System.IO.Directory.CreateDirectory(cheminDocFinaux + "\\DocFinaux\\Releve\\" + donneEntete["Tiers_nocpt"].Substring(3, 6));
                    chemin = cheminDocFinaux + "\\DocFinaux\\Releve\\" + donneEntete["Tiers_nocpt"].Substring(3, 6) + "\\" + nomDoc + "_" + donneEntete["Tiers_nocpt"].Substring(3, 6) + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf";
                }
                else
                {
                    chemin = cheminDocFinaux + "\\DocFinaux\\Releve\\" + donneEntete["Tiers_nocpt"].Substring(3, 6) + "\\" + nomDoc + "_" + donneEntete["Tiers_nocpt"].Substring(3, 6) + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf";
                }
                Document nouveauDocument = new Document(PageSize.A4, 20, 20, 12, 2);
                PdfWriter writer = PdfWriter.GetInstance(nouveauDocument, new FileStream(chemin, FileMode.Create));     //Stockage du document
                //---------------------------------------------------------------------------------------------------------------------------
                //Constitution document PDF
                //---------------------------------------------------------------------------------------------------------------------------
                nouveauDocument.Open();
                PdfPTable tableau = new PdfPTable(2);
                tableau.TotalWidth = 550;
                tableau.LockedWidth = true;
                //-------------------------------------------------------------------------------------------------
                //Encadré ABCR
                Paragraph p = new Paragraph();
                p.Add(new Phrase(donneEntete["Adresse_interne_1"] + "\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                p.Add(new Phrase(donneEntete["Adresse_interne_2"] + "\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                p.Add(new Phrase(donneEntete["Adresse_interne_3"] + "\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                p.Add(new Phrase(donneEntete["Adresse_interne_4"] + "\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                PdfPCell celulleHauteGauche = new PdfPCell(p);
                celulleHauteGauche.Border = PdfPCell.NO_BORDER;
                tableau.AddCell(celulleHauteGauche);

                //Celulle de droite contenant l'adresse de livraison
                Paragraph pAdl = new Paragraph();
                pAdl.Add(new Phrase("\n\n  \n"));
                pAdl.Add(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdl.Add(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdl.Add(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdl.Add(new Phrase("\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdl.Add(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                PdfPCell celulleFinDroite = new PdfPCell(pAdl);
                celulleFinDroite.Bottom = PdfPCell.ALIGN_BOTTOM;
                celulleFinDroite.Border = PdfPCell.NO_BORDER;
                celulleFinDroite.PaddingLeft = 35;
                tableau.AddCell(celulleFinDroite);


                //Tableau dans celulle bas gauche du tableau d'entete
                PdfPCell celulleBasGauche = new PdfPCell();
                Paragraph ptest = new Paragraph();
                ptest.Add(new Phrase("\n        Relevé de facture(s)\n\n",FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                celulleBasGauche.AddElement(ptest);
                PdfPTable tabCell = new PdfPTable(3);
                tabCell.TotalWidth = 230;
                tabCell.LockedWidth = true;
                tabCell.AddCell(new Phrase("COMPTE", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                tabCell.AddCell(new Phrase("DATE", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                tabCell.AddCell(new Phrase("NO RELEVE", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                tabCell.AddCell(new Phrase(donneEntete["Tiers_nocpt"], FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                tabCell.AddCell(new Phrase(donneEntete["Rel_datrel"], FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                tabCell.AddCell(new Phrase(donneEntete["Document_numero"], FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                tabCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                celulleBasGauche.AddElement(tabCell);
                celulleBasGauche.Border = PdfPCell.NO_BORDER;
                celulleBasGauche.Bottom = PdfPCell.ALIGN_BOTTOM;
                tableau.AddCell(celulleBasGauche);

                //Adresse facturation
                Paragraph pAdf = new Paragraph();                                                                           // REGLER INDENTATION DU PARAGRAPHE
                pAdf.Add(new Phrase("\n\n\n\n" + donneEntete["Tiers_adf1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adf2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adf3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adf4"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                pAdf.Add(new Phrase(donneEntete["Tiers_adfcp"] + "   " + donneEntete["Tiers_adf6"] + "\n\n\n\n ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                PdfPCell celulleHauteDroite = new PdfPCell(pAdf);
                celulleHauteGauche.HorizontalAlignment = Element.ALIGN_RIGHT;
                celulleHauteDroite.Border = PdfPCell.NO_BORDER;
                celulleHauteDroite.PaddingLeft = 80;
                tableau.AddCell(celulleHauteDroite);

                nouveauDocument.Add(tableau);
                //--------------------------------------------------------------------------------------------------------
                //                                      TABLEAU
                //-------------------------------------------------------------------------------------------------------
                CurseurTemplate ct = new CurseurTemplate();
                valeurTemplate = ct.chercher("RELEVE");

                float[] largeurs = { 
                                   int.Parse(valeurTemplate["Dimension1"]),
                                   int.Parse(valeurTemplate["Dimension2"]),
                                   int.Parse(valeurTemplate["Dimension3"]),
                                   int.Parse(valeurTemplate["Dimension4"]),
                                   int.Parse(valeurTemplate["Dimension5"]),
                                   int.Parse(valeurTemplate["Dimension6"]),
                               };
                PdfPTable table = new PdfPTable(largeurs);
                table.TotalWidth = 565;                                                                                         //Chaque colonne crée ci dessus doit être rempli
                table.LockedWidth = true;
                PdfPCell cellET1 = new PdfPCell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD))); //cellET1.Border = PdfPCell.NO_BORDER; //cellET1.Border += PdfPCell.BOTTOM_BORDER;
                cellET1.SetLeading(2f, 1.2f);
                cellET1.PaddingBottom = 7f;
                table.AddCell(cellET1);
                PdfPCell cellET2 = new PdfPCell(new Phrase("Type Pièce", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD))); //cellET2.Border = PdfPCell.NO_BORDER; //cellET2.Border += PdfPCell.BOTTOM_BORDER;
                cellET2.SetLeading(2f, 1.2f);
                cellET2.PaddingBottom = 7f;
                table.AddCell(cellET2);
                PdfPCell cellET3 = new PdfPCell(new Phrase("Référence", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD))); //cellET3.Border = PdfPCell.NO_BORDER; //cellET3.Border += PdfPCell.BOTTOM_BORDER;
                cellET3.SetLeading(2f, 1.2f);
                cellET3.PaddingBottom = 7f;
                table.AddCell(cellET3);
                PdfPCell cellET4 = new PdfPCell(new Phrase("T.V.A", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD))); //cellET4.Border = PdfPCell.NO_BORDER; //cellET4.Border += PdfPCell.BOTTOM_BORDER;
                cellET4.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellET4.PaddingBottom = 7f;
                cellET4.SetLeading(2f, 1.2f); 
                table.AddCell(cellET4);
                PdfPCell cellET5 = new PdfPCell(new Phrase("Avoir", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD))); //cellET5.Border = PdfPCell.NO_BORDER; //cellET5.Border += PdfPCell.BOTTOM_BORDER;
                cellET5.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellET5.PaddingBottom = 7f;
                cellET5.SetLeading(2f, 1.2f);
                table.AddCell(cellET5);
                PdfPCell cellET6 = new PdfPCell(new Phrase("Facture", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD))); //cellET6.Border = PdfPCell.NO_BORDER; //cellET6.Border += PdfPCell.BOTTOM_BORDER;
                cellET6.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellET6.PaddingBottom = 7f;
                cellET6.SetLeading(2f, 1.2f);
                table.AddCell(cellET6);
                int i; int nbLigne = 0; float resultat = 0; float dimTab = 0; int décrement = 0; int numPage = 0;         //Constitution du tableau
                bool okDési = false; bool okStart = false; double tempoTOT = 0;
                for (i = 0; i < iBody; i++)
                {
                    nbLigne++;
                    PdfPCell cell1 = new PdfPCell(new Phrase(donneeBody["Rel_datfac" + i], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                    cell1.SetLeading(5f, 1.2f);
                    table.AddCell(cell1);
                    PdfPCell cell2 = new PdfPCell(new Phrase(donneeBody["Rel_libavofac" + i], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL))); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                    cell2.SetLeading(5f, 1.2f);
                    table.AddCell(cell2);
                    PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Rel_nofac" + i], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                    cell3.SetLeading(5f, 1.2f);
                    table.AddCell(cell3);
                    PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Rel_mtva" + i], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                    cell4.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell4.SetLeading(5f, 1.2f);
                    table.AddCell(cell4);
                    PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Rel_mttavo"+i], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                    cell5.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell5.SetLeading(5f, 1.2f);
                    table.AddCell(cell5);
                    PdfPCell cell6 = new PdfPCell(new Phrase(donneeBody["Rel_mtfac" + i], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                    cell6.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell6.SetLeading(5f, 1.2f);
                    table.AddCell(cell6);
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
                        PdfPCell cellBlancheD = new PdfPCell(new Phrase("\n\n" + tempoTOT.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.ITALIC)));
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
                        table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche);  table.AddCell(cellBlanche); table.AddCell(cellBlancheDA); table.AddCell(cellBlancheD);
                        table.AddCell(cellFin);
                        nouveauDocument.Add(table);//----------------------------------------------------------------------------Repère ligne en dessous--------------------------------------------------
                        Phrase pReport = new Phrase("                                                                                                                                                             A REPORTER\n\n\n\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD));
                        Phrase pPage = new Phrase("\n                                                   " + donneEntete["Document_type"] + "                 " + donneEntete["Duplicata"] + "                                                                         Page n° " + (numPage + 1) + "            ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD));
                        pPage.Leading = 15;
                        nouveauDocument.Add(pReport);
                        table.DeleteBodyRows();
                        nouveauDocument.Add(Chunk.NEXTPAGE);
                        nouveauDocument.Add(tableau);
                        nouveauDocument.Add(pPage);
                        table.AddCell(cellET1); table.AddCell(cellET2); table.AddCell(cellET3); table.AddCell(cellET4); table.AddCell(cellET5); table.AddCell(cellET6); 
                        dimTab = 0;
                        décrement = (i - 1);
                    }
                    //FinGestionSautPage
                }
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                int b;                                          //----------------------------------------------------------------
                for (b = 0; b <= i; b++)                        //
                {                                               //              Compteur dimension du tableau
                    float temp = table.TotalHeight;             //
                    resultat = temp;                            //
                }                                               //-------------------------------------------------------------------
                if (i >= iBody)
                {
                    PdfPCell cellFin = new PdfPCell(new Phrase(" "));
                    PdfPCell cellBlanche = new PdfPCell(new Phrase(" "));
                    PdfPCell cellBlancheD = new PdfPCell(new Phrase(" "));
                    cellFin.Colspan = 8;
                    resultat = 300 - resultat;            //<<----------450 correspond au nombre de point de la longueur du tableau, c'est la valeur à modifier pour modifier la taille du tableau

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
                    table.AddCell(cellBlanche); table.AddCell(cellBlanche);  table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlancheD);
                    table.AddCell(cellFin);
                }
                table.SpacingAfter = -15F;
                nouveauDocument.Add(table);

                //----------------------------------------------------Tableau récap-----------------------------------------
                PdfPTable tabTot = new PdfPTable(3);
                tabTot.TotalWidth = 565;
                tabTot.LockedWidth = true;
                PdfPCell cellHT = new PdfPCell(new Phrase("HT           " + donneeFoot["Tot_ht"], FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD))); cellHT.Border = PdfPCell.NO_BORDER;
                PdfPCell cellTVA = new PdfPCell(new Phrase("TVA             " + donneeFoot["Tot_tva"], FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD))); cellTVA.Border = PdfPCell.NO_BORDER;
                PdfPCell cellTot = new PdfPCell(new Phrase(donneeFoot["Tot_rel"], FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD))); cellTot.Border = PdfPCell.NO_BORDER; 
                cellHT.HorizontalAlignment = Element.ALIGN_RIGHT; cellTVA.HorizontalAlignment = Element.ALIGN_RIGHT; cellTot.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabTot.AddCell(cellHT); tabTot.AddCell(cellTVA); tabTot.AddCell(cellTot);
                PdfPCell cellType = new PdfPCell(new Phrase("\nDate d'échéance : " + donneeFoot["Tot_datech"]+"\n\n"+donneeFoot["Tot_libtraite"], FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.NORMAL))); cellType.Border = PdfPCell.NO_BORDER;
                cellType.Colspan = 2;
                PdfPTable tabReleve = new PdfPTable(1);
                tabReleve.TotalWidth = 138;
                tabReleve.LockedWidth = true;
                tabReleve.AddCell(new PdfPCell(new Phrase("TOTAL RELEVE", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD))));
                PdfPCell tabCellTot = new PdfPCell(new Phrase("\n"+donneeFoot["Tot_rel"]+" EUR", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD))); tabCellTot.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabReleve.AddCell(tabCellTot);
                PdfPCell cellTabReleve = new PdfPCell(); cellTabReleve.Border = PdfPCell.NO_BORDER; cellTabReleve.PaddingLeft= 50; cellTabReleve.VerticalAlignment = Element.ALIGN_BOTTOM;
                cellTabReleve.AddElement(tabReleve);
                tabTot.AddCell(cellType);
                tabTot.AddCell(cellTabReleve);
                nouveauDocument.Add(tabTot);
                Phrase pTiré = new Phrase("\n------------------------------------------------------------------------------------------------------------------------------------------");
                pTiré.SetLeading(0f, 0.5f);
                nouveauDocument.Add(pTiré);
                //--------------------------------------FIN TABLEAU RECAP-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                if (donneeFoot["Tot_libreg"] == "")  // Condition si réglement par traites
                {
                    PdfPTable tableauTraiteHaut = new PdfPTable(3);
                    tableauTraiteHaut.TotalWidth = 565;
                    tableauTraiteHaut.LockedWidth = true;
                    PdfPCell cellBlancheTraite = new PdfPCell(); cellBlancheTraite.Border = PdfPCell.NO_BORDER;
                    tableauTraiteHaut.AddCell(cellBlancheTraite);
                    PdfPCell cellTraiteHaut = new PdfPCell();
                    cellTraiteHaut.SetLeading(1f, 0.5f);
                    PdfPCell cellTraiteHautD = new PdfPCell();
                    cellTraiteHautD.SetLeading(1f, 0.5f); cellTraiteHaut.Border = PdfPCell.NO_BORDER; cellTraiteHautD.Border = PdfPCell.NO_BORDER;
                    cellTraiteHaut.AddElement(new Phrase("Contre cette LETTRE de CHANGE stipulée SANS FRAIS veuillez payer la somme indiquée ci-dessous à l'ordre de :", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    tableauTraiteHaut.AddCell(cellTraiteHaut);
                    cellTraiteHautD.AddElement(new Phrase("      " + donneeFoot["Tra_libacc"] + "   " + donneeFoot["Tra_lcr"], FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    tableauTraiteHaut.AddCell(cellTraiteHautD);
                    tableauTraiteHaut.SpacingAfter = -10f;
                    nouveauDocument.Add(tableauTraiteHaut);
                    //--------------------------Tableau Traite principal-------------------------------------------------------------------------------------------------------
                    PdfPTable tableauTraite = new PdfPTable(3);
                    //tableauTraite.SpacingBefore = -10f;
                    tableauTraite.TotalWidth = 565; tableauTraite.LockedWidth = true;
                    PdfPCell cellTraiteMiG = new PdfPCell(); 
                    cellTraiteMiG.Border = PdfPCell.NO_BORDER;
                    Paragraph pcellTraiteHG = new Paragraph();
                    pcellTraiteHG.Add(new Phrase("\nA " + donneeFoot["Tra_lieucr"]+"\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL)));
                    pcellTraiteHG.Add(new Phrase("Montant pour controle      Date de création\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL)));
                    pcellTraiteHG.Add(new Phrase("|"+donneeFoot["Tra_montae"] + "|            |" + donneeFoot["Tra_datrel"]+"|", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    pcellTraiteHG.Add(new Phrase("\n|                                                                          ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.UNDERLINE)));
                    pcellTraiteHG.Add(new Phrase("|\n                     RIB du tiré                                           .", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.UNDERLINE)));
                    cellTraiteMiG.AddElement(pcellTraiteHG); cellTraiteMiG.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    PdfPCell cellTraiteMiM = new PdfPCell();
                    cellTraiteMiM.Border = PdfPCell.NO_BORDER;
                    Paragraph pcellTraiteHM = new Paragraph();
                    pcellTraiteHM.Add(new Phrase("\n\nEchéance\n ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL)));
                    pcellTraiteHM.Add(new Phrase("|   " + donneeFoot["Tra_datech"] + " |", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    pcellTraiteHM.Add(new Phrase("     |" + donneeFoot["Tra_cocpt"] + "|\n", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    pcellTraiteHM.Add(new Phrase("                                                     Ref.tiré     ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL)));
                    cellTraiteMiM.AddElement(pcellTraiteHM); cellTraiteMiM.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    PdfPCell cellTraiteMiD = new PdfPCell();
                    cellTraiteMiD.Border = PdfPCell.NO_BORDER;
                    cellTraiteMiD.AddElement(new Phrase("\n CODE MONNAIE ISO\n",FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    cellTraiteMiD.AddElement(new Phrase("      EUR\n", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    cellTraiteMiD.AddElement(new Phrase("Montant\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL)));
                    cellTraiteMiD.AddElement(new Phrase("               *****" + donneeFoot["Tra_montae"] + "***", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    cellTraiteMiD.PaddingLeft = 70;
                    tableauTraite.AddCell(cellTraiteMiG); tableauTraite.AddCell(cellTraiteMiM); tableauTraite.AddCell(cellTraiteMiD);
                    //------------FIN LIGNE MILEU TABLEAU---------------
                    PdfPCell cellTraiteBasG = new PdfPCell();
                    cellTraiteBasG.Border = PdfPCell.NO_BORDER; 
                    cellTraiteBasG.AddElement(new Phrase("|" + donneeFoot["Tra_banque"] + " |  " + donneeFoot["Tra_guichet"] + " | " + donneeFoot["Tra_cptco"] + " |    " + donneeFoot["Tra_rib"]+"|\n", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    cellTraiteBasG.AddElement(new Phrase("Code établ.     Code guichet              N°de Compte             Clé RIB\n", FontFactory.GetFont(FontFactory.HELVETICA, 6, Font.NORMAL)));
                    cellTraiteBasG.AddElement(new Phrase("Valeur en:", FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.NORMAL)));
                    cellTraiteBasG.AddElement(new Phrase("ACCEPTATION OU AVAL", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    cellTraiteBasG.AddElement(new Phrase("                                    NOM\n                                et ADRESSE", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL)));
                    cellTraiteBasG.AddElement(new Phrase("\nN° SIREN du TIRE    "+donneeFoot["Tra_siren"],FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    PdfPCell cellTraiteBasM = new PdfPCell();
                    cellTraiteBasM.Border = PdfPCell.NO_BORDER;
                    cellTraiteBasM.AddElement(new Phrase("  "+donneeFoot["Tra_tiers_adf1"]+"\n",FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL)));
                    cellTraiteBasM.AddElement(new Phrase("  " + donneeFoot["Tra_tiers_adf2"] + "\n\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL)));
                    cellTraiteBasM.AddElement(new Phrase("  " + donneeFoot["Tra_tiers_adfcp"] + "  " + donneeFoot["Tra_tiers_adf6"], FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL)));
                    PdfPCell cellTraiteBasD = new PdfPCell();
                    cellTraiteBasD.Border = PdfPCell.NO_BORDER;
                    cellTraiteBasD.AddElement(new Phrase("                        DOMICILIATION                  .\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.UNDERLINE)));
                    cellTraiteBasD.AddElement(new Phrase("|           " + donneeFoot["Tra_domic1"] + "                  |\n", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    cellTraiteBasD.AddElement(new Phrase("|           " + donneeFoot["Tra_domic2"] + "                |\n", FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    cellTraiteBasD.AddElement(new Phrase("                      Signature                                 ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL)));
                    cellTraiteBasD.AddElement(new Phrase("\n\nCLIENT N° " + donneeFoot["Tra_nocli"] + "  RELEVE N°      " + donneeFoot["Tra_numrel"], FontFactory.GetFont(FontFactory.COURIER, 8, Font.NORMAL)));
                    tableauTraite.AddCell(cellTraiteBasG);tableauTraite.AddCell(cellTraiteBasM);tableauTraite.AddCell(cellTraiteBasD);
                    nouveauDocument.Add(tableauTraite);
                }
                nouveauDocument.Close();
                incCopie++;
                //--------------------------------------------------COPIE GED--------------------------------------------------------
                try
                {
                    String connectionString = ConfigurationManager.AppSettings["ChaineDeConnexionBase"];
                    OdbcConnection conn = new OdbcConnection(connectionString);
                    conn.Open();
                    string requete = "select T1.NOCLI c1 , T1.NOMCL c2 from B00C0ACR.AMAGESTCOM.ACLIENL1 T1 where T1.NOCLI = '" + donneeFoot["Tra_nocli"] + "'";
                    OdbcCommand act = new OdbcCommand(requete, conn);
                    OdbcDataReader act0 = act.ExecuteReader();
                    string nomADH = "";
                    while (act0.Read())
                    {
                        nomADH = (act0.GetString(1));
                    }
                    conn.Close();
                    if (!System.IO.Directory.Exists(ConfigurationManager.AppSettings["cheminGED"] + "\\" + donneeFoot["Tra_nocli"] + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\Releve\\"))
                    {
                        System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["cheminGED"] + "\\" + donneeFoot["Tra_nocli"] + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM").ToUpperInvariant() + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\Releve\\");
                        System.IO.File.Copy(chemin, ConfigurationManager.AppSettings["cheminGED"] + "\\" + donneeFoot["Tra_nocli"] + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM").ToUpperInvariant() + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\Releve\\" + "\\" + nomDoc + "_" + donneeFoot["Tra_nocli"] + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf");
                    }
                    else
                    {
                        System.IO.File.Copy(chemin, ConfigurationManager.AppSettings["cheminGED"] + "\\" + donneeFoot["Tra_nocli"] + " - " + nomADH + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.ToString("MM").ToUpperInvariant() + "-" + DateTime.Now.ToString("MMMM").First().ToString().ToUpper() + String.Join("", DateTime.Now.ToString("MMMM").Skip(1)) + "\\Releve\\" + nomDoc + "_" + donneeFoot["Tra_nocli"] + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf");
                    }
                }
                catch (Exception e)
                {
                    LogHelper.WriteToFile(e.Message, "ENVOI GED Releve");
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
                bool effectuerImpression = true;
                try
                {
                    act20 = act2.ExecuteReader();
                    while (act20.Read())
                    {
                        if (act20.GetString(0) == donneEntete["Client_code"])// Si le code client est égale au résultat de la requete sur la ligne lu "NOCLI"
                        {
                            if (act20.GetString(1) == "OUI")                                               //Si la ligne de l'enrigistrement dans la base est à OUI pour cet ID, alors ne pas imprimer
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
            }
        }
    }
}
