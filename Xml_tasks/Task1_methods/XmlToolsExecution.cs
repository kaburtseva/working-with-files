using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlTools
{
    public class XmlToolsExecution
    {
        private static string nodeName = "book";
        private static string attribute = "genre";
        private static string value = "detective";
        private static string attributeReadValue = "genre";
        private static string xpathReadValue = "/bookstore/book[@publicationdate='1967']";
        private static string xpathModify = "bookstore/book[@publicationdate='2000']/author";
        private static string innerText = "Vasya Test";
        private static string xpathReadNode = "/bookstore/book[@publicationdate='1981']/title";
        private static string path = "C:\\Users\\kate\\Documents\\CharpTrivial\\Xml_tasks\\Task1_methods\\example.xml";

        static void Main(string[] args)
        {
            XmlTools tools = new XmlTools(path);
            tools.AddNodeByXpath(path, nodeName, attribute, value);
            tools.ReadValueByXpath(attributeReadValue, xpathReadValue);
            tools.ModifyValueByXpath(path, xpathModify, innerText);
            tools.ReadNodeXpath(xpathReadNode);
            Console.ReadLine();
        }

    }
}

