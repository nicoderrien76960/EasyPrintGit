﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2.iSeries;
using System.Data.Odbc;
using System.Net.Mail;

namespace EssaiJobImp
{
    class Mail
    {
        Dictionary<string, string> dicoMailADH = new Dictionary<string, string>();
        public Mail()
        { }
        public void remplirDictionnaire()
        {
            String connectionString = "Driver={iSeries Access ODBC Driver};System=10.211.200.1;Uid=AMAD;Pwd=AMAD5678;";
            OdbcConnection conn = new OdbcConnection(connectionString);
            conn.Open();
            string requete = "select T1.NOCLI c1 , T1.CLID5 c2 , T1.RENDI c3 from B00C0ACR.AMAGESTCOM.ACLIENL1 T1 where T1.CLID5 = 'OUI'";
            OdbcCommand act = new OdbcCommand(requete, conn);
            OdbcDataReader act0 = act.ExecuteReader();
            while (act0.Read())
            {
                dicoMailADH.Add(act0.GetString(0), act0.GetString(2));
            }
            conn.Close();
        }
        public void comparerDocument(string id)
        {
            foreach (string key in dicoMailADH.Keys)
            {
                if ((System.IO.Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["DossierFacturation"] + "\\" + key)) && (key.TrimEnd() == id))
                {
                    string[] doc = System.IO.Directory.GetFiles(System.Configuration.ConfigurationManager.AppSettings["DossierFacturation"] + "\\" + key);
                    for (int i = 0; i < doc.Length; i++)
                    {
                        DateTime date = System.IO.Directory.GetLastWriteTime(doc[i]).Date;
                        DateTime datePlus1;
                        try
                        {
                            datePlus1 = System.IO.Directory.GetLastWriteTime(doc[i + 1]).Date;                       //   <--- EN REFLEXION
                        }catch{ datePlus1 = date; }
                        
                        DateTime dateActuelle = DateTime.Today;
                        if (date == dateActuelle)
                        {
                            envoiDocument(doc[i], key, dicoMailADH[key]);  //Fonction envoi de doc
                        }
                    }
                } 
            }
        }
        public void envoiDocument(string path, string id, string mail)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.abcr76.fr",587))
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress("alexis.duboc@abcr76.fr", "ABCR - Facturation");
                message.To.Add("alexis.duboc@abcrorcab.com");//mail.TrimEnd()
                message.Subject = "Votre Facturation du Mois de "+DateTime.Now.ToString("MMMM");
                //message.Body = "Bonjour,\n\nVous trouverez en pièce jointe de ce mail votre facture du mois de "+DateTime.Now.ToString("MMMM")+"\n\n\nCordialement, la coopérative ABCR";
                message.Body = "<p><span style=\"font-size: small;\"><font face=\"helvetica\">Bonjour,</font></span></p><p>&nbsp;</p><p>Veuillez trouver en pi&egrave;ce jointe de ce mail votre facture du mois de "+DateTime.Now.ToString("MMMM")+"</p><p>&nbsp;</p><p>Cordialement, la coop&eacute;rativa ABCR.</p><p>&nbsp;</p><p>&nbsp;</p>";
                message.IsBodyHtml = true;
                Attachment data = new Attachment(path);
                message.Attachments.Add(data);
                smtpClient.Send(message);
            }
        }
    }
}