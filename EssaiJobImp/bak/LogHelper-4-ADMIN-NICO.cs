﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ireport_Rubis
{
    class LogHelper
    {
        public static void WriteToFile(string errorMessage, string className)
        {
            DateTime nowDate = DateTime.Now;
            string shortDate = String.Format("{0:yyyy-MM-dd}", nowDate);
            string filename = string.Format("{0}.log", shortDate);
            //création du nom de fichier .log
            filename = filename.Replace("/", "-");
            //récupérer le path complet, application PC
            string rootPath = Path.GetFullPath("./Data/Log/");
            //ou le path pour une application serveur
            //System.Web.HttpContext.Current.Server.MapPath("~/Data/Log/");
            string fullFilename = string.Format(@"{0}{1}", rootPath, filename);
            //vérifier les dossiers Data & Log
            if (!Directory.Exists(rootPath))
            {
                //création du dossier
                Directory.CreateDirectory(rootPath);
            }
            //vérifier le fichier
            if (!System.IO.File.Exists(fullFilename))
            {
                //création du fichier log du jour
                System.IO.FileStream f = System.IO.File.Create(fullFilename);
                f.Close();
            }

            using (StreamWriter writer = new StreamWriter(fullFilename, true))
            {
                //écriture dans le fichier log du jour
                writer.WriteLine(string.Format(
                                       "[{0} ON {1}] : {2}",
                                       DateTime.Now,
                                       className,
                                       errorMessage));
            }
        }
    }
}
