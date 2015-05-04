using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PrinterForce;
using System.Threading;
using Amyuni.PDFCreator;

namespace EssaiJobImp
{
    class ParseurBP
    {
        private Dictionary<string, string> donneEntete;
        private Dictionary<string, string> donneeBody;
        private Dictionary<string, string> donneeFoot;
        int iBody; int iFoot; string nomDoc; string unProfil;
        public ParseurBP(Dictionary<string, string>donneeEntete, Dictionary<string, string>donneeBody, Dictionary<string,string>donneeFoot, int iBody, int iFoot, string nomDoc, string profil)
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
            while (incCopie < nbCopie)
            {
                bool drapReliquat = false;
                string chemin = "E:\\DocFinaux\\BP\\BP_" + nomDoc + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf";
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
                Image image2 = Image.GetInstance("E:\\EssaiePatternHautDroiteBP.jpg");
                image2.Alignment = Image.UNDERLYING;
                image2.SetAbsolutePosition(325, 755);
                nouveauDocument.Add(image2);
                Image image5 = Image.GetInstance("E:\\FiligraneBP.png");
                image5.Alignment = Image.UNDERLYING;
                image5.SetAbsolutePosition(180, 270);
                nouveauDocument.Add(image5);
                //------------------------------------------------------------------------------------------------------
                Paragraph pLogo = new Paragraph();
                Image image = Image.GetInstance("E:\\ABCR 3cm.jpg");
                pLogo.Add(image);                                                                               //Encadré photo
                PdfPCell celulleHauteGauche = new PdfPCell(image);
                celulleHauteGauche.Border = PdfPCell.NO_BORDER;
                tableau.AddCell(celulleHauteGauche);

                //Celulle de droite contenant l'adresse de livraison
                Paragraph pAdl = new Paragraph();
                pAdl.Add(new Phrase("Adresse de livraison\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                if (donneEntete["Bon_typvte"] == "EMPORTEE")
                {
                    pAdl.Add(new Phrase("EMPORTEE\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    /*pAdl.Add(new Phrase(donneEntete["Tiers_adl3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase(donneEntete["Tiers_adl4"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase(donneEntete["Tiers_adl5"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    pAdl.Add(new Phrase(donneEntete["Tiers_adlcp"] + "   " + donneEntete["Tiers_adl6"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));*/
                }
                else
                {
                    if (donneEntete["Tiers_adl1"] == "")
                    {
                        pAdl.Add(new Phrase(donneEntete["Tiers_adf1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adf2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adf3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adf4"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        //pAdl.Add(new Phrase(donneEntete["Tiers_adf6"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adfcp"] + "   " + donneEntete["Tiers_adl6"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    }
                    else
                    {
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl4"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        //pAdl.Add(new Phrase(donneEntete["Tiers_adl5"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adlcp"] + "   " + donneEntete["Tiers_adl6"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                    }
                }
                PdfPCell celulleFinDroite = new PdfPCell(pAdl);
                celulleFinDroite.Rowspan = 2;
                celulleFinDroite.Border = PdfPCell.NO_BORDER;
                celulleFinDroite.PaddingLeft = 35;
                tableau.AddCell(celulleFinDroite);

                //Adresse ABCR
                string tel = donneEntete["Adresse_interne_7"]; string fax = donneEntete["Adresse_interne_8"];
                tel = tel.Substring(3, 15); fax = fax.Substring(3, 15);
                Paragraph p = new Paragraph();
                p.Add(new Phrase(donneEntete["Adresse_interne_2"] + "      Tél  " + tel + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                p.Add(new Phrase(donneEntete["Adresse_interne_3"] + "    Fax " + fax + "\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                PdfPCell celulleMilieuGauche = new PdfPCell(p);
                celulleMilieuGauche.Border = PdfPCell.NO_BORDER;
                tableau.AddCell(celulleMilieuGauche);

                //Tableau dans celulle bas gauche du tableau d'entete
                PdfPTable tabCell = new PdfPTable(3);
                tabCell.TotalWidth = 230;
                tabCell.LockedWidth = true;
                tabCell.AddCell(new Phrase("Client", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                tabCell.AddCell(new Phrase("Date", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                tabCell.AddCell(new Phrase("N° CDE", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                tabCell.AddCell(new Phrase(donneEntete["Client_code"], FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                tabCell.AddCell(new Phrase(donneEntete["Document_date"], FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                tabCell.AddCell(new Phrase(donneEntete["Document_numero"], FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                tabCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PdfPCell celulleBasGauche = new PdfPCell(tabCell);
                celulleBasGauche.Border = PdfPCell.NO_BORDER;
                tableau.AddCell(celulleBasGauche);

                //Adresse de facturation
                Paragraph pAdf = new Paragraph();
                pAdf.Add(new Phrase("Adresse de facturation\n" + donneEntete["Tiers_adf1"], FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                PdfPCell celulleHauteDroite = new PdfPCell(pAdf);
                celulleHauteDroite.Border = PdfPCell.NO_BORDER;
                celulleHauteDroite.HorizontalAlignment = Element.ALIGN_LEFT;
                celulleHauteDroite.PaddingLeft = 35;
                tableau.AddCell(celulleHauteDroite);
                nouveauDocument.Add(tableau);

                //Récap ref client et numéro de téléphone
                Paragraph refCli = new Paragraph();
                refCli.Add(new Phrase("Référence client " + donneeBody["Bon_rcl1"] + " du " + donneeBody["Bon_datrcl1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                nouveauDocument.Add(refCli);
                //Recap dessus tableau

                //Condition de fonctionnement si dictionnaire contient clé commentaire ou non
                Chunk c;
                if (donneEntete.ContainsKey("Commentaire_texteentete"))
                {
                    c = new Chunk("Vous avez été servi par : " + donneEntete["Bon_vendeur_lib"] + "            Livrée le " + donneeBody["Bon_datliv1"] + "              " + donneEntete["Commentaire_texteentete"] + "\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.ITALIC));
                    nouveauDocument.Add(c);
                    Phrase pPage1;
                    if (donneEntete.ContainsKey("Commentaire_texteentete0"))
                    { pPage1 = new Phrase(donneEntete["Commentaire_texteentete0"] + "                                                                                      Page n° 1\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)); }
                    else { pPage1 = new Phrase("                       " + donneEntete["Document_type"] + "                 " + donneEntete["Duplicata"] + "                                         Page n° 1           \n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)); }
                    nouveauDocument.Add(pPage1);
                }
                else
                {
                    c = new Chunk("Vous avez été servi par : " + donneEntete["Bon_vendeur_lib"] + "            Livrée le " + donneeBody["Bon_datliv1"] + "\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.ITALIC));
                    nouveauDocument.Add(c);
                    Phrase pPage1;
                    pPage1 = new Phrase("                       " + donneEntete["Document_type"] + "                 " + donneEntete["Duplicata"] +"                                           Page n° 1           \n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD));
                    nouveauDocument.Add(pPage1);
                }
                //--------------------------------------------------------------------------------------------------------
                //                                      TABLEAU
                //---------------------------------------------------------------------------------------------------------
                float[] largeurs = { 15, 44, 7, 9, 35 };
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
                PdfPCell cellET5 = new PdfPCell(new Phrase("Localisation", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellET5.Border = PdfPCell.NO_BORDER; //cellET5.Border += PdfPCell.BOTTOM_BORDER;
                table.AddCell(cellET5);
                PdfPCell cellvideDebut = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 6, Font.BOLD)));
                cellvideDebut.Colspan = 5;
                cellvideDebut.Border = PdfPCell.NO_BORDER;
                table.AddCell(cellvideDebut);
                Image image3 = Image.GetInstance("E:\\EssaiePatternEnteteTableau.jpg");
                image3.Alignment = Image.UNDERLYING;
                image3.SetAbsolutePosition(20, 597);
                nouveauDocument.Add(image3);
                List<string> locPrecedent = new List<string>(); //Liste qui récupère les anciennes clés de localisation afin de ne pas les réintégrer au doc
                int i; int nbLigne = 0; float resultat = 0; float dimTab = 0; int décrement = 0; int numPage = 0;         //Constitution du tableau d'article
                bool okDési = false; bool okStart = false;
                for (i = 1; i <= iBody; i++)
                {
                    //Condition ARTICLE----------------------------------------------------------------------------------------------------------------------
                    if (donneeBody["Ligne_type" + i] == "ART")
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
                                if (System.Text.RegularExpressions.Regex.IsMatch(entry.Value, "Localisations secondaires", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                { }//Empeche de faire apparaitre la localisation secondaire dans la désignation
                                else
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
                        bool drapLoc = false;
                        string reliquat = "";
                        foreach (KeyValuePair<string, string> entry in donneeBody)
                        {  
                            //string patternReliquat = "Art_reliquat";
                            if (donneeBody.ContainsKey("Art_reliquat"+i))
                            {
                                reliquat = "Reliquat";
                                drapReliquat = true;
                            }
                            //Condition si l'article à une loca secondaire
                            if (drapLoc == false && locPrecedent.Contains(entry.Key) == false && System.Text.RegularExpressions.Regex.IsMatch(entry.Value, "Localisations secondaires", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                string rackSub = donneeBody[entry.Key];
                                rackSub = rackSub.Substring(27, 2); 
                                string etagereSub = donneeBody[entry.Key];
                                etagereSub = etagereSub.Substring(29, 2);
                                PdfPCell cell6 = new PdfPCell(new Phrase("Zone : " + donneeBody["Art_localisation" + i] + "    Rack : " + rackSub + "    Etagère : " + etagereSub+"            /n"+reliquat, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                cell6.Border = PdfPCell.NO_BORDER;
                                cell6.Border += PdfPCell.LEFT_BORDER;
                                cell6.Border += PdfPCell.RIGHT_BORDER;
                                table.AddCell(cell6);
                                okStart = true; drapLoc = true;
                                reliquat = "";
                                locPrecedent.Add(entry.Key);
                            }
                            //Si il n'en a pas, afficher uniquement la zone
                            if (drapLoc == false && locPrecedent.Contains(entry.Key) == false && donneeBody["Art_type_cde" + i] != "S" && donneeBody["Art_type_cde" + i] != "D")
                            {
                                PdfPCell cell6 = new PdfPCell(new Phrase("Zone : " + donneeBody["Art_localisation" + i] + "    " + reliquat, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                cell6.Border = PdfPCell.NO_BORDER;
                                cell6.Border += PdfPCell.LEFT_BORDER;
                                cell6.Border += PdfPCell.RIGHT_BORDER;
                                table.AddCell(cell6);
                                okStart = true; drapLoc = true;
                                reliquat = "";
                                locPrecedent.Add(entry.Key);
                            }
                            //Condition si article est spécial, pas de localisation
                            if (drapLoc == false && locPrecedent.Contains(entry.Key) == false && donneeBody["Art_type_cde" + i] == "S")
                            {
                                PdfPCell cell6 = new PdfPCell(new Phrase("Spécial" + "    " + reliquat, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                cell6.Border = PdfPCell.NO_BORDER;
                                cell6.Border += PdfPCell.LEFT_BORDER;
                                cell6.Border += PdfPCell.RIGHT_BORDER;
                                table.AddCell(cell6);
                                okStart = true; drapLoc = true;
                                reliquat = "";
                                locPrecedent.Add(entry.Key);
                            }
                            //Condition si article est direct, pas de localisation
                            if (drapLoc == false && locPrecedent.Contains(entry.Key) == false && donneeBody["Art_type_cde" + i] == "D")
                            {
                                PdfPCell cell6 = new PdfPCell(new Phrase("Direct" + "    " + reliquat, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                                cell6.Border = PdfPCell.NO_BORDER;
                                cell6.Border += PdfPCell.LEFT_BORDER;
                                cell6.Border += PdfPCell.RIGHT_BORDER;
                                table.AddCell(cell6);
                                okStart = true; drapLoc = true;
                                reliquat = "";
                                locPrecedent.Add(entry.Key);
                            }
                        }
                        if (drapLoc == false)
                        {
                            PdfPCell cell7 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                            cell7.Border = PdfPCell.NO_BORDER;
                            cell7.Border += PdfPCell.LEFT_BORDER;
                            cell7.Border += PdfPCell.RIGHT_BORDER;
                            table.AddCell(cell7);
                        }
                        okDési = false; okStart = false; drapLoc = false;
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
                    table.AddCell(cellEcartDroite); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart); table.AddCell(cellEcart);
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
                        cellFin.Colspan = 5;

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
                        table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlancheD);
                        table.AddCell(cellFin);
                        nouveauDocument.Add(table);//----------------------------------------------------------------------------Repère ligne en dessous--------------------------------------------------
                        Phrase pReport = new Phrase("                                                                                                                                                             A REPORTER\n\n\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD));
                        Phrase pPage;
                        if (donneEntete.ContainsKey("Commentaire_texteentete0"))
                        { pPage = new Phrase(donneEntete["Commentaire_texteentete0"] + "                                                                                      Page n° " + (numPage + 1) + "\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)); }
                        else {pPage = new Phrase("                                                                                                                                                                    Page n° "+(numPage+1)+"\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD));}
                        nouveauDocument.Add(pReport);
                        table.DeleteBodyRows();
                        nouveauDocument.Add(Chunk.NEXTPAGE);
                        nouveauDocument.Add(tableau);
                        //nouveauDocument.Add(p);
                        nouveauDocument.Add(refCli);
          
                        nouveauDocument.Add(c);
                        nouveauDocument.Add(pPage);
                        nouveauDocument.Add(image2); nouveauDocument.Add(image3); nouveauDocument.Add(image5);
                        table.AddCell(cellET1); table.AddCell(cellET2); table.AddCell(cellET3); table.AddCell(cellET4); table.AddCell(cellET5);
                        table.AddCell(cellvideDebut);
                        dimTab = 0;
                        décrement = (i - 1);
                    }
                }
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                                              //-------------------------------------------------------------------
                if (i > iBody)
                {
                    PdfPCell cellFin = new PdfPCell(new Phrase(" "));
                    PdfPCell cellBlanche = new PdfPCell(new Phrase(" "));
                    PdfPCell cellBlancheD = new PdfPCell(new Phrase(" "));
                    cellFin.Colspan = 5;
                    resultat = 450 - dimTab;            //<<----------450 correspond au nombre de point de la longueur du tableau, c'est la valeur à modifier pour modifier la taille du tableau
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
                    table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlanche); table.AddCell(cellBlancheD);
                    table.AddCell(cellFin);
                }
                nouveauDocument.Add(table);
                //----------------------------------------Gestion des commentaires de bon-------------------------------------
                if (donneEntete.ContainsKey("Commentaire_texte"))
                {
                    Paragraph pComBon = new Paragraph(new Phrase("                                       " + donneEntete["Commentaire_texte"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)));
                    nouveauDocument.Add(pComBon);
                }
       

                nouveauDocument.Close();
                incCopie++;

                #region ImpressionOld
                /*myPrinters.SetDefaultPrinter("Imp204");
                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Verb = "print";

                //Define location of adobe reader/command line
                //switches to launch adobe in "print" mode
                proc.StartInfo.FileName =
                  @"C:\Program Files (x86)\Adobe\Reader 11.0\Reader\AcroRd32.exe";
                proc.StartInfo.Arguments = String.Format(@"/p /h {0}", chemin);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (proc.HasExited == false)
                {
                    proc.WaitForExit(10000);
                }

                proc.EnableRaisingEvents = true;

                proc.Close();
                foreach (Process clsProcess in Process.GetProcesses().Where(
                         clsProcess => clsProcess.ProcessName.StartsWith("AcroRd32")))
                {
                    clsProcess.Kill();
                }*/
                #endregion
                //Solution d'impression fonctionnel, API payante----------------------------------------------------------------------------------------------
                acPDFCreatorLib.Initialize();
                acPDFCreatorLib.SetLicenseKey("Amyuni PDF Creator .NET Evaluation", "07EFCDAB0100010025C3B7B3A2579FF94C49112EAF736861254446237C2F6A215A53E83AF4CCFFE54C52063CB05334BDE555773D7B1B"); 
                IacDocument doc = new IacDocument();
                System.IO.FileStream file1 = new System.IO.FileStream(chemin, FileMode.Open, FileAccess.Read, FileShare.Read);
                doc.Open(file1, "");

                doc.StartPrint("Imp204", false);
                for (int index = 1; index <= doc.PageCount; index++)
                {

                    doc.PrintPage(doc.GetPage(index));

                }
                
                doc.EndPrint();
                //--------------------------------------------------------------------------------------------------------------------------------------------------
                
                int nbImp = 0; int nbImpOK = 0;
                string[] printer = new string[20]; // tableau qui contient les imprimantes du profil d'impression
                ProfilImprimante profil = new ProfilImprimante();
                profil.chargementXML("BP");     // chargement selon le type de doc
                string vendeur = unProfil.Substring(2, 3);
                vendeur = vendeur.TrimEnd();
                var listeProfil = profil.getDonneeProfil();
                try
                {
                    if (drapReliquat)
                    {
                        printer[nbImp] = ConfigurationManager.AppSettings["ImpDefBL"];
                        nbImp++;
                    }
                    else
                    {
                        foreach (string v in listeProfil[vendeur])      //lecture des imprimantes liée à un profil
                        {
                            printer[nbImp] = v.ToString();
                            nbImp++;                                    //on incrémente le nombre d'impression à executer
                        }
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
                    catch (Exception e)
                    { LogHelper.WriteToFile(e.Message, "ParseurBP" + donneEntete["Document_numero"].Trim()); }
                                                         // incrément à chaque impression terminée
                }
            }
        }
    }
}
