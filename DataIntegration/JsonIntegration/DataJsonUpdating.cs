using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonIntegration
{
    public class DataJsonUpdating
    {
        private static string pathToFile = "E:\\WorkWithFiles\\DataIntegration\\JsonIntegration\\Accounts1.json";

        static void Main(string[] args)
        {
            JsonHelpers jsonHelper = new JsonHelpers(pathToFile);

            Account KateAccount = jsonHelper.GetAccount("AccountName", "Katerina");
            KateAccount.FirstName = "Kate2";
            jsonHelper.UpdateAccount(KateAccount);
            Console.ReadLine();
        }
    }
}
