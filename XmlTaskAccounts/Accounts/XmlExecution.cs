using Accounts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts
{
    public class XmlExecution
    {

        private static string path ="C:\\Users\\kate\\Documents\\CharpTrivial\\XmlTaskAccounts\\Accounts\\Account.xml";

        static void Main(string[] args)
        {
            XmlTools xmlTools = new XmlTools();
            xmlTools.GetXMLAsString("Account.xml");
           // xmlTools.CreateAccountAttribute();
            var katerina = xmlTools.GetAccount("Katerina", path);
            Account Anton = new Account() { FirstName = "Anton" };
           xmlTools.SaveAccount(Anton);
           // xmlTools.EditAndAddNewNode();
           // xmlTools.EditAndUpdateNode();
            Console.ReadLine();
        }
    }
}
