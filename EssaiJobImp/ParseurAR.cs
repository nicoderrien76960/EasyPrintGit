using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PrinterForce;

namespace EssaiJobImp
{
    class ParseurAR
    {
        private Dictionary<string, string> donneEntete;
        private Dictionary<string, string> donneeBody;
        private Dictionary<string, string> donneeFoot;
        int iBody; int iFoot; string nomDoc; string unProfil;
        public ParseurAR(Dictionary<string, string>donneeEntete, Dictionary<string, string>donneeBody, Dictionary<string,string>donneeFoot, int iBody, int iFoot, string nomDoc, string profil)
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
                string chemin = "E:\\DocFinaux\\AR\\AR_" + nomDoc + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".pdf";
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
                Image image5 = Image.GetInstance("E:\\FiligraneAR.png");
                image5.Alignment = Image.UNDERLYING;
                image5.SetAbsolutePosition(185, 250);
                nouveauDocument.Add(image5);
                //-------------------------------------------------------------------------------------------------
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
                        pAdl.Add(new Phrase(donneEntete["Tiers_adfcp"] + "   " + donneEntete["Tiers_adf6"] , FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase("\n\nAdresse de facturation\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
                    }
                    else
                    {
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl2"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl3"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adl4"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase(donneEntete["Tiers_adlcp"] + "   " + donneEntete["Tiers_adl6"] , FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                        pAdl.Add(new Phrase("\n\nAdresse de facturation\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 11, Font.BOLD)));
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
                PdfPCell celulleBasGauche = new PdfPCell();
                
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
                celulleBasGauche.AddElement(tabCell);
                celulleBasGauche.Border = PdfPCell.NO_BORDER;
                tableau.AddCell(celulleBasGauche);
                //Adresse facturation
                Paragraph pAdf = new Paragraph();
                pAdf.Add(new Phrase(donneEntete["Tiers_adf1"]+"\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
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
                Paragraph refCli = new Paragraph();
                refCli.Add(new Phrase("Référence client " + donneeBody["Bon_rcl1"] + " du " + donneeBody["Bon_datrcl1"] + "\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD)));
                nouveauDocument.Add(refCli);
                //Recap dessus tableau
                Chunk c = new Chunk("Vous avez été servi par : " + donneEntete["Bon_vendeur_lib"] + "                              Livrée le " + donneEntete["Ent_datliv"] + "               "+donneEntete["Tiers_tel"]+" \n\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.ITALIC));
                nouveauDocument.Add(c);
                Phrase pPage1 = new Phrase("                       " + donneEntete["Document_type"] + "                 " + donneEntete["Duplicata"] +"                                                                            Page n° 1           \n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD));
                pPage1.Leading = 10;
                nouveauDocument.Add(pPage1);
                //--------------------------------------------------------------------------------------------------------
                //                                      TABLEAU
                //-------------------------------------------------------------------------------------------------------
                float[] largeurs = { 15, 42, 6, 9, 8, 9, 10, 11 };
                PdfPTable table = new PdfPTable(largeurs);
                table.TotalWidth = 555;                                                                                         //Chaque colonne crée ci dessus doit être rempli
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
                Image image3 = Image.GetInstance("E:\\EssaiePatternEnteteTableau.jpg");
                image3.Alignment = Image.UNDERLYING;
                image3.SetAbsolutePosition(20, 585);
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
                        if (donneeBody["Art_prinet" +i]!="")
                        { prixnet = double.Parse(donneeBody["Art_prinet" + i]); }  
                        if (prixnet!=-99999)
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
                    //Condition ARTICLE GRATUIT-----------------------------------------------------------------------------------------------------------------------------
                    if (donneeBody["Ligne_type" + i] == "GRA")
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
                    if (dimTab >= 410 && i < iBody)
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
                        Phrase pPage = new Phrase("                       " + donneEntete["Document_type"] + "                 " + donneEntete["Duplicata"] + "                                                                            Page n° " + (numPage + 1) +"            \n", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD));
                        nouveauDocument.Add(pReport);
                        table.DeleteBodyRows();
                        nouveauDocument.Add(Chunk.NEXTPAGE);
                        nouveauDocument.Add(tableau);
                        //nouveauDocument.Add(p);
                        nouveauDocument.Add(refCli);
                        nouveauDocument.Add(c);
                        nouveauDocument.Add(pPage);
                        //Image image5 = Image.GetInstance("E:\\FiligraneAR.png");//Changer lien pattern
                        image5.Alignment = Image.UNDERLYING;
                        image5.SetAbsolutePosition(200, 250);
                        nouveauDocument.Add(image5);
                        image3.SetAbsolutePosition(20, 578);
                        nouveauDocument.Add(image2); nouveauDocument.Add(image3);
                        table.AddCell(cellET1); table.AddCell(cellET2); table.AddCell(cellET3); table.AddCell(cellET4); table.AddCell(cellET5); table.AddCell(cellET6); table.AddCell(cellET7); table.AddCell(cellET8);
                        dimTab = 0;
                        décrement = (i - 1);
                    }
                    //FinGestionSautPage
                }
                //Gestion Commentaires de bon
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
                }
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
                Image image4 = Image.GetInstance("E:\\EssaiePatternTotBL.jpg");
                image4.Alignment = Image.UNDERLYING;
                image4.SetAbsolutePosition(385, 105);
                nouveauDocument.Add(image4);
                nouveauDocument.Add(table);
                //-----------------------------------------------------------------------------------------------------
                //                          PIED DE PAGE
                //-----------------------------------------------------------------------------------------------------
                PdfPTable tableauPied = new PdfPTable(3);
                tableauPied.TotalWidth = 555;
                tableauPied.LockedWidth = true;

                PdfPCell cellulePied = new PdfPCell();
                cellulePied.Colspan = 2;
                cellulePied.Border = PdfPCell.NO_BORDER;
                tableauPied.AddCell(cellulePied);

                string euro = "€";
                if (donneeFoot["Pied_montant_ttc"] == " ") { euro = " "; }
                PdfPTable tableauTot = new PdfPTable(1);
                PdfPCell cellTTot = new PdfPCell(new Phrase("Montant HT : " + donneeFoot["Pied_montant_ht"] + " " + euro, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellTTot.Border = PdfPCell.NO_BORDER; cellTTot.Border = PdfPCell.BOTTOM_BORDER;
                tableauTot.AddCell(cellTTot);
                PdfPCell cellTTC = new PdfPCell(new Phrase("Montant TTC : " + donneeFoot["Pied_montant_ttc"] + " " + euro + "\n\n\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD))); cellTTC.Border = PdfPCell.NO_BORDER;
                tableauTot.AddCell(cellTTC);
                PdfPCell cellTot = new PdfPCell(tableauTot);
                cellTot.Border = PdfPCell.NO_BORDER;
                tableauPied.AddCell(cellTot);

                nouveauDocument.Add(tableauPied);

                nouveauDocument.Close();
                incCopie++;

                int nbImp = 0; int nbImpOK = 0;
                string[] printer = new string[20]; // tableau qui contient les imprimantes du profil d'impression
                ProfilImprimante profil = new ProfilImprimante();
                profil.chargementXML("AR");     // chargement selon le type de doc
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
                    { LogHelper.WriteToFile(e.Message, "ParseurAR" + donneEntete["Document_numero"].Trim()); }
                                                            // incrément à chaque impression terminée
                }
            }
        }
    }
}
