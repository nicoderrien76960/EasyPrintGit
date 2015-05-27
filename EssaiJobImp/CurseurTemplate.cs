using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EssaiJobImp
{
    class CurseurTemplate
    {
        Dictionary<String, String> valeurTemplate = new Dictionary<string, string>();
        public Dictionary<String,String> chercher(string typeDoc)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XPathNavigator Navigator;
            XPathNodeIterator Nodes;
            XmlDoc.Load("TemplateSetting.config");
            Navigator = XmlDoc.CreateNavigator();
            string ExpXPath = "//P[@nom='" + typeDoc + "']";
            Nodes = Navigator.Select(Navigator.Compile(ExpXPath));
            if (Nodes.Count != 0)
            {
                string key, value;
                while (Nodes.CurrentPosition < Nodes.Count)
                {
                    Nodes.MoveNext();
                    Nodes.Current.MoveToFirstChild();
                    key = Nodes.Current.Value;
                    Nodes.Current.MoveToNext();
                    value = Nodes.Current.Value;
                    valeurTemplate.Add(key, value);
                }
            }
            return valeurTemplate;
        }
    }
}
