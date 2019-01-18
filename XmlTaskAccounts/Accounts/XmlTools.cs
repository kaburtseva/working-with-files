using Accounts.Model;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Accounts
{
    public class XmlTools
    {
        private XmlDocument doc;
        public static string str =null;
        public string GetXMLAsString(XmlDocument doc)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            doc.WriteTo(tx);
            str = sw.ToString();
            return str;
        }

        public Account GetAccount(string nameAttribute, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Account),new XmlRootAttribute(nameAttribute));
            StringReader stringReader = new StringReader(str);
            Account ac = (Account)serializer.Deserialize(stringReader);
            Console.Write(
                ac.FirstName + "\t" +
                ac.ExpirationDate);
            return new Account();

        }

        public void SaveAccount(Account account, string path)
        {
             XmlSerializer serializer = new XmlSerializer(typeof(Account));
            account = new Account();
            account.Columns.Add(FirstName);

            

        }


        public void CreateAccountAttribute()
        {
            XmlNode node = doc.CreateNode(XmlNodeType.Element, "Account", null);
            XmlAttribute name = doc.CreateAttribute("name");
            name.Value = "Test";
            XmlNode nodeFirstName = doc.CreateElement("FistName");
            nodeFirstName.InnerText = "Merry";
            node.Attributes.Append(name);
            node.AppendChild(nodeFirstName);
            doc.DocumentElement.AppendChild(node);
            doc.Save("Account.xml");
            doc.Save(Console.Out);
        }

        public void AddNewNode(string path, string newNode)
        {
            XmlSerializer serialize = new XmlSerializer(typeof(XmlNode));
            XmlNode myNode = new XmlDocument().
            CreateNode(XmlNodeType.Element, newNode, null);
            myNode.InnerText = "Hello Node";
            TextWriter writer = new StreamWriter(path);
            serialize.Serialize(writer, myNode);
            writer.Close();

        
        }

        public void EditAndUpdateNode()
        {
            XmlNode node = doc.SelectSingleNode("Accounts/Account[@name='Katerina']/Job");
            node.InnerText = "Software Developer";
            doc.Save("Account.xml");
            doc.Save(Console.Out);
        }
    }
}
