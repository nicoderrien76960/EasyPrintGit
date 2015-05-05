using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using PrinterForce;

namespace EssaiJobImp
{
    class Parseur
    {
        private Dictionary<string, string> donneEntete;
        private Dictionary<string, string> donneeBody;
        private Dictionary<string, string> donneeFoot;
        int iBody; int iFoot; string nomDoc; string unProfil;
        public Parseur(Dictionary<string, string>donneeEntete, Dictionary<string, string>donneeBody, Dictionary<string,string>donneeFoot, int iBody, int iFoot, string nomDoc, string profil)
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
            string chemin = "E:\\DocFinaux\\DEVIS\\DEVIS_" + nomDoc + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf";
            Document nouveauDocument = new Document(PageSize.A4,20,20,12,20);
            PdfWriter.GetInstance(nouveauDocument, new FileStream(chemin, FileMode.Create));     //Stockage du document
            nouveauDocument.Open();
            Image image4 = Image.GetInstance("E:\\EssaiePatternTot.jpg");
            image4.Alignment = Image.UNDERLYING;
            image4.SetAbsolutePosition(385, 70);
            //----------------------------------------------------------------------------------------------------Filligrane--------
            Image image5 = Image.GetInstance("E:\\FiligraneDevis.png");
            image5.Alignment = Image.UNDERLYING;
            image5.SetAbsolutePosition(200, 250);
            nouveauDocument.Add(image5);
            //----------------------------------------
            //Constitution document PDF
            //----------------------------------------
            PdfPTable tableau = new PdfPTable(2);
            tableau.TotalWidth = 550;
            tableau.LockedWidth = true;
            Paragraph pLogo = new Paragraph();
            Image image = Image.GetInstance("E:\\ABCR 4cm.jpg");
            pLogo.Add(image);                                                                               //Encadré photo
            PdfPCell celulleHauteGauche = new PdfPCell(image);
            celulleHauteGauche.Border = PdfPCell.NO_BORDER;
            tableau.AddCell(celulleHauteGauche);
                                                                                                            //Encadré info devis
            Paragraph pDoc = new Paragraph();
            Image image2 = Image.GetInstance("E:\\EssaiePatternHautDroite.jpg");
            image2.Alignment = Image.UNDERLYING;
            image2.SetAbsolutePosition(335, 740);
            nouveauDocument.Add(image2);
            pDoc.Alignment = Element.ALIGN_RIGHT;
            pDoc.Add(new Phrase("Devis n° "+donneeBody["Bon_numero1"] + "\n",FontFactory.GetFont(FontFactory.HELVETICA,10,Font.BOLD)));
            pDoc.Add(new Phrase("Date : " + donneeBody["Bon_datrcl1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
            pDoc.Add(new Phrase("Référence client : "+donneeBody["Bon_rcl1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8,BaseColor.RED)));
            pDoc.Add(new Phrase("Code client: " + donneEntete["Client_code"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
            pDoc.Add(new Phrase(donneEntete["Tiers_tel"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
            pDoc.Add(new Phrase(donneEntete["Tiers_fax"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
            pDoc.Add(new Phrase(donneEntete["Tiers_adf5"], FontFactory.GetFont(FontFactory.HELVETICA, 8)));

            PdfPCell celulleHauteDroite = new PdfPCell(pDoc);
          
            celulleHauteDroite.Border = PdfPCell.NO_BORDER;
            celulleHauteDroite.HorizontalAlignment = Element.ALIGN_CENTER;
            tableau.AddCell(celulleHauteDroite);
                                                                                        //Encadré "ABCR"
            Paragraph p = new Paragraph();
            p.Add(new Phrase(donneEntete["Adresse_interne_2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
            p.Add(new Phrase(donneEntete["Adresse_interne_3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
            p.Add(new Phrase(donneEntete["Adresse_interne_4"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.BOLD)));
            p.Add(new Phrase(donneEntete["Adresse_interne_5"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.BOLD)));
            p.Add(new Phrase(donneEntete["Adresse_interne_6"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.BOLD)));
            p.Add(new Phrase(donneEntete["Adresse_interne_7"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            p.Add(new Phrase(donneEntete["Adresse_interne_8"] + "\n" + "\n" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
            PdfPCell celluleBasGauche = new PdfPCell(p);
            celluleBasGauche.Border = PdfPCell.NO_BORDER;
            tableau.AddCell(celluleBasGauche);
                                                                                        //Encadré client
            Paragraph pClient = new Paragraph();
            pClient.Add(new Phrase("\n"/*+"\n"*/+donneEntete["Tiers_adf1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA , 10, Font.BOLD)));
            pClient.Add(new Phrase(donneEntete["Tiers_adf2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
            pClient.Add(new Phrase(donneEntete["Tiers_adf3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
            pClient.Add(new Phrase(donneEntete["Tiers_adf4"] + "\n" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
            pClient.Add(new Phrase(donneEntete["Tiers_adfcp"] + "  " + donneEntete["Tiers_adf6"], FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
            PdfPCell celluleBasDroite = new PdfPCell(pClient);
            celluleBasDroite.Border = PdfPCell.NO_BORDER;
            tableau.AddCell(celluleBasDroite);
            nouveauDocument.Add(tableau);
                                                                                                                //Dessus tableau
            Paragraph pRécap = new Paragraph();
            Chunk contact = new Chunk("Votre contact : " + donneEntete["Bon_vendeur_lib"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 7, Font.ITALIC));//FontFactory pour changer la police
            Chunk delai = new Chunk("Livraison : " + donneEntete["Tiers_adf1"] + " - " + donneEntete["Tiers_adf2"] + " - " + donneEntete["Tiers_adfcp"] + "  " + donneEntete["Tiers_adf6"]+"\n"+"\n" , FontFactory.GetFont(FontFactory.HELVETICA,7, Font.ITALIC)).SetUnderline(2,2);
            Chunk ligneEspace = new Chunk("\n", FontFactory.GetFont(FontFactory.HELVETICA, 1, Font.ITALIC));
            pRécap.Add(contact);
            pRécap.Add(delai);
            pRécap.Add(ligneEspace);
            nouveauDocument.Add(pRécap);

            float[] largeurs = { 15, 42, 5, 8, 8, 9, 10, 9, 4 };                              // Dimension tableau body

            PdfPTable table = new PdfPTable(largeurs);
            table.TotalWidth = 555;                                                                                         //Chaque colonne crée ci dessus doit être rempli
            table.LockedWidth = true;
            PdfPCell cellET1 = new PdfPCell(new Phrase("Article", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET1.Border = PdfPCell.NO_BORDER; //cellET1.Border += PdfPCell.BOTTOM_BORDER;
            table.AddCell(cellET1);
            PdfPCell cellET2 = new PdfPCell(new Phrase("Désignation", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET2.Border = PdfPCell.NO_BORDER; //cellET2.Border += PdfPCell.BOTTOM_BORDER;
            table.AddCell(cellET2);
            PdfPCell cellET3 = new PdfPCell(new Phrase("UV", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET3.Border = PdfPCell.NO_BORDER; //cellET3.Border += PdfPCell.BOTTOM_BORDER;
            table.AddCell(cellET3);
            PdfPCell cellET4 = new PdfPCell(new Phrase("Quantité", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET4.Border = PdfPCell.NO_BORDER; //cellET4.Border += PdfPCell.BOTTOM_BORDER;
            table.AddCell(cellET4);
            PdfPCell cellET5 = new PdfPCell(new Phrase("Prix remisé", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET5.Border = PdfPCell.NO_BORDER; //cellET5.Border += PdfPCell.BOTTOM_BORDER;
            table.AddCell(cellET5);
            PdfPCell cellET6 = new PdfPCell(new Phrase("Remise", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET6.Border = PdfPCell.NO_BORDER; //cellET6.Border += PdfPCell.BOTTOM_BORDER;
            table.AddCell(cellET6);
            PdfPCell cellET7 = new PdfPCell(new Phrase("Prix net", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET7.Border = PdfPCell.NO_BORDER; //cellET7.Border += PdfPCell.BOTTOM_BORDER;
            table.AddCell(cellET7);
            PdfPCell cellET8 = new PdfPCell(new Phrase("Montant HT", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET8.Border = PdfPCell.NO_BORDER; //cellET8.Border += PdfPCell.BOTTOM_BORDER;
            table.AddCell(cellET8);
            PdfPCell cellET9 = new PdfPCell(new Phrase("TVA", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET9.Border = PdfPCell.NO_BORDER; //cellET9.Border += PdfPCell.BOTTOM_BORDER;
            table.AddCell(cellET9);
            Image image3 = Image.GetInstance("E:\\EssaiePatternEnteteTableau.jpg");
            image3.Alignment = Image.UNDERLYING;
            image3.SetAbsolutePosition(20, 569);
            nouveauDocument.Add(image3);

            int i; int nbLigne = 0; float resultat = 0; float dimTab = 0; int décrement = 0; int numPage = 0;         //Constitution du tableau d'article
            bool okDési = false; bool okStart = false;
            for (i = 1; i <= iBody; i++)
            {
                //Condition ARTICLE----------------------------------------------------------------------------------------------------------------------
                if (donneeBody["Ligne_type" + i] == "ART")
                {
                    nbLigne++;
                    string sPattern = "libelle" + i + "bis";
                    PdfPCell cell1 = new PdfPCell(new Phrase(donneeBody["Art_code" + i]+"\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
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
                    else { table.AddCell(cell2); }
                    PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell4);
                    PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell5);
                    PdfPCell cell6 = new PdfPCell(new Phrase(donneeBody["Art_remise1" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell6);
                    PdfPCell cell7 = new PdfPCell(new Phrase(donneeBody["Art_remise2" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell7.Border = PdfPCell.NO_BORDER; cell7.Border += PdfPCell.RIGHT_BORDER; cell7.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell7);
                    PdfPCell cell8 = new PdfPCell(new Phrase(donneeBody["Art_prinet" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell8.Border = PdfPCell.NO_BORDER; cell8.Border += PdfPCell.RIGHT_BORDER; cell8.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell8);
                    PdfPCell cell9 = new PdfPCell(new Phrase(donneeBody["Art_monht" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell9.Border = PdfPCell.NO_BORDER; cell9.Border += PdfPCell.RIGHT_BORDER; cell9.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell9);
                    PdfPCell cell10 = new PdfPCell(new Phrase("0"+donneeBody["Art_tva_code" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell10.Border = PdfPCell.NO_BORDER; cell10.Border += PdfPCell.RIGHT_BORDER; cell10.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell10);
                    okDési = false; okStart = false;
                }
                //Condition ARTICLE GRATUIT-----------------------------------------------------------------------------------------------------------------------------
                if (donneeBody["Ligne_type" + i] == "GRA")
                {
                    nbLigne++;
                    string sPattern = "libelle" + i + "bis";
                    PdfPCell cell1 = new PdfPCell(new Phrase(donneeBody["Art_code" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell1.Border = PdfPCell.NO_BORDER; cell1.Border += PdfPCell.RIGHT_BORDER; cell1.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell1);
                    foreach (KeyValuePair<string, string> entry in donneeBody)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(entry.Key, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            PdfPCell cell2 = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n" + donneeBody["Libelle" + i + "bis"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell2.Border = PdfPCell.NO_BORDER; cell2.Border += PdfPCell.RIGHT_BORDER; cell2.Border += PdfPCell.LEFT_BORDER;
                            okDési = true;
                            table.AddCell(cell2);
                        }
                    }
                    if (okDési == false)
                    {
                        PdfPCell cell3 = new PdfPCell(new Phrase(donneeBody["Libelle" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell3.Border = PdfPCell.NO_BORDER; cell3.Border += PdfPCell.RIGHT_BORDER; cell3.Border += PdfPCell.LEFT_BORDER;
                        table.AddCell(cell3);
                    }
                    PdfPCell cell4 = new PdfPCell(new Phrase(donneeBody["Art_unite" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell4.Border = PdfPCell.NO_BORDER; cell4.Border += PdfPCell.RIGHT_BORDER; cell4.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell4);
                    PdfPCell cell5 = new PdfPCell(new Phrase(donneeBody["Art_qte" + i] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell5.Border = PdfPCell.NO_BORDER; cell5.Border += PdfPCell.RIGHT_BORDER; cell5.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell5);
                    PdfPCell cell6 = new PdfPCell(new Phrase("" + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cell6.Border = PdfPCell.NO_BORDER; cell6.Border += PdfPCell.RIGHT_BORDER; cell6.Border += PdfPCell.LEFT_BORDER;
                    table.AddCell(cell6);
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
                    PdfPCell cellVide = new PdfPCell(new Phrase(""+"\n"));
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
                table.AddCell(cellEcartDroite); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart);
                //--------------------------------------------GESTION DU SAUT DE PAGE-------------------------------------------------------------------------------------------

                //float temp = table.GetRowHeight(i-1-décrement);
                float temp=table.TotalHeight;
                dimTab = temp;
                if (dimTab >= 410 && i<iBody)
                {
                    //Saut de page
                    numPage++;
                    PdfPCell cellFin = new PdfPCell(new Phrase(" "));
                    PdfPCell cellBlanche = new PdfPCell(new Phrase(" "));
                    PdfPCell cellBlancheD = new PdfPCell(new Phrase(" "));
                    cellFin.Colspan = 9;
                    cellBlanche.FixedHeight = (450-dimTab);
                    cellBlanche.Border = PdfPCell.NO_BORDER;
                    cellBlanche.Border += PdfPCell.RIGHT_BORDER;
                    cellBlanche.Border += PdfPCell.LEFT_BORDER;
                    cellBlancheD.FixedHeight = (450 - dimTab);
                    cellBlancheD.Border = PdfPCell.NO_BORDER;
                    cellBlancheD.Border += PdfPCell.LEFT_BORDER;
                    cellBlancheD.Border += PdfPCell.RIGHT_BORDER;
                    cellFin.Border = PdfPCell.NO_BORDER;
                    cellFin.Border += PdfPCell.TOP_BORDER;
                    table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlancheD);
                    table.AddCell(cellFin);
                    nouveauDocument.Add(table);//----------------------------------------------------------------------------Repère ligne en dessous--------------------------------------------------
                    Phrase pReport = new Phrase("                                                                                                                                                             A REPORTER\n\n\n\n\n                                                                                                                                                                    Page n° " + numPage, FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD));
                    nouveauDocument.Add(pReport);
                    table.DeleteBodyRows();
                    nouveauDocument.Add(Chunk.NEXTPAGE);
                    nouveauDocument.Add(tableau); 
                    nouveauDocument.Add(pRécap);
                    nouveauDocument.Add(image2); nouveauDocument.Add(image3); nouveauDocument.Add(image5);
                    table.AddCell(cellET1); table.AddCell(cellET2); table.AddCell(cellET3); table.AddCell(cellET4); table.AddCell(cellET5); table.AddCell(cellET6); table.AddCell(cellET7); table.AddCell(cellET8); table.AddCell(cellET9);
                    dimTab = 0;
                    décrement = (i-1);
                }
                //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            }
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
                cellFin.Colspan = 9;
                resultat = 450-resultat;            //<<----------450 correspond au nombre de point de la longueur du tableau, c'est la valeur à modifier pour modifier la taille du tableau

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
                table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlancheD);
                table.AddCell(cellFin);
            }
            nouveauDocument.Add(table);	
                                                                                          // Constitution tableau pied de page
            PdfPTable tableauPied = new PdfPTable(3);
            tableauPied.TotalWidth = 555;
            tableauPied.LockedWidth = true;

            Paragraph pTVA = new Paragraph();
            pTVA.Add(new Phrase("Offre de prix valable jusqu'au : " + donneEntete["Date_val"], FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, BaseColor.RED)));
            int iF=0;
            foreach (KeyValuePair<string, string> entry in donneeFoot)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(entry.Value, "TVA", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    pTVA.Add(new Phrase("\nTVA "+donneeFoot.ElementAt(iF-1).Value+" = "+ entry.Value,FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                }
                iF++;
            }
            PdfPCell cellulePied = new PdfPCell(pTVA);
            cellulePied.Colspan = 2;
            cellulePied.Border = PdfPCell.NO_BORDER;
            tableauPied.AddCell(cellulePied);


            nouveauDocument.Add(image4);
            PdfPTable tableauTot = new PdfPTable(1);
            PdfPCell cellTTot = new PdfPCell(new Phrase("Total HT : " + donneeFoot["Base_tva_mht"] + " €", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellTTot.Border = PdfPCell.NO_BORDER; cellTTot.Border = PdfPCell.BOTTOM_BORDER;
            tableauTot.AddCell(cellTTot);
            PdfPCell cellTVA = new PdfPCell(new Phrase("Total TVA : " + donneeFoot["Base_tva_mtva"] + " €", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellTVA.Border = PdfPCell.NO_BORDER; cellTVA.Border = PdfPCell.BOTTOM_BORDER;
            tableauTot.AddCell(cellTVA);
            PdfPCell cellTTC = new PdfPCell(new Phrase("Total TTC : " + donneeFoot["Base_tva_mttc"] + " €", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellTTC.Border = PdfPCell.NO_BORDER; 
            tableauTot.AddCell(cellTTC);

            PdfPCell cellTot = new PdfPCell(tableauTot);
            cellTot.Border = PdfPCell.NO_BORDER;
            tableauPied.AddCell(cellTot);
            nouveauDocument.Add(tableauPied);
            Phrase maPhrase = new Phrase();
            maPhrase.Add(new Chunk("\nBon pour accord ", FontFactory.GetFont(FontFactory.HELVETICA,9,Font.BOLD)));
            maPhrase.Add(new Chunk("\n                 Fait à:                                le:                              Signature", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
            nouveauDocument.Add(maPhrase);

            if (numPage > 0)
            {
                Phrase PageFinal = new Phrase("                                                                                                                                                                                                                                                                           Page n° " + (1+numPage),FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD));
                nouveauDocument.Add(PageFinal);
            }


            nouveauDocument.Close();

            int nbImp = 0; int nbImpOK = 0;
                string[] printer = new string[20]; // tableau qui contient les imprimantes du profil d'impression
                ProfilImprimante profil = new ProfilImprimante();
                profil.chargementXML("Dev");     // chargement selon le type de doc
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
                    myPrinters.SetDefaultPrinter(printer[nbImpOK]);
                    Process proc = new Process();
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.Verb = "print";

                    proc.StartInfo.FileName =
                     ConfigurationManager.AppSettings["AdrAdobe"];
                    proc.StartInfo.Arguments = String.Format(@"/p /h {0}", chemin);
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.CreateNoWindow = true;

                    proc.Start();
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    if (proc.HasExited == false)
                    {
                        proc.WaitForExit(12000);
                    }

                    proc.EnableRaisingEvents = true;
                    nbImpOK++; 
                    proc.Close();
                    try
                    {
                        foreach (Process clsProcess in Process.GetProcesses().Where(
                                 clsProcess => clsProcess.ProcessName.StartsWith("AcroRd32")))
                        {
                            clsProcess.Kill();
                        }
                        
                    }
                    catch(Exception e)
                    { LogHelper.WriteToFile(e.Message, "Parseur" + donneEntete["Document_numero"].Trim()); }                                                           // incrément à chaque impression terminée
                }
        }
    }
}
