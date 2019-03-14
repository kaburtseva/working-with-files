using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace XmlTools
{
    public class XmlTools
    {
        private XmlDocument doc;

        public XmlTools(string path)
        {
            this.doc = new XmlDocument();
            doc.Load(path);
        }

        public void ReadNodeXpath(string xpath)
        {
            XmlNodeList nodeList = doc.SelectNodes(xpath);
            foreach (XmlNode node in nodeList)
            {
                Console.WriteLine(node.InnerText);
            }


        }
        public void ReadValueByXpath(string attribute, string xpath)
        {
            XmlNodeList nodeList = doc.DocumentElement.SelectNodes(xpath);
            string myVal = "";
            foreach (XmlNode node in nodeList)
            {
                myVal = node.Attributes[attribute].Value.ToString();
                Console.WriteLine(myVal);
            }
        }

        public void AddNodeByXpath(string path, string nodeName, string attribute, string value)
        {
            XmlNode node = doc.CreateNode(XmlNodeType.Element, nodeName, null);
            XmlAttribute genre = doc.CreateAttribute(attribute);
            genre.Value = value;

            //XmlNode nodeTitle = doc.CreateElement("title");
            //  nodeTitle.InnerText = "Snowman";
            //  XmlNode nodeAuthor = doc.CreateElement("author");
            //  nodeAuthor.InnerText = "Jo Nesbø";
            node.Attributes.Append(genre);

            // node.AppendChild(nodeTitle);
            //node.AppendChild(nodeAuthor);
            doc.DocumentElement.AppendChild(node);
            doc.Save(path);
            doc.Save(Console.Out);
        }

        public void ModifyValueByXpath(string path, string xpath, string innerText)
        {
            XmlNode node = doc.SelectSingleNode(xpath);
            node.InnerText = innerText;
            doc.Save(path);
            doc.Save(Console.Out);
        }
    }
}

