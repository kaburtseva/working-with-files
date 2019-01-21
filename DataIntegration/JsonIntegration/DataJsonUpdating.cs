using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonIntegration
{
    public class DataJsonUpdating
    {
        private static string pathToFile = "C:\\Users\\kateryna.burtseva\\Documents\\working-with-files\\DataIntegration\\JsonIntegration\\Accounts1.json";

        static void Main(string[] args)
        {
            JsonHelpers jsonHelper = new JsonHelpers(pathToFile);
            Account KateAccount = jsonHelper.GetAccount("AccountName", "Katerina");
            Console.WriteLine(KateAccount.ToString());
            jsonHelper.PrintAllData();
            KateAccount.FirstName = "Kate2";
            jsonHelper.UpdateAccount(KateAccount);
            jsonHelper.PrintAllData();
            var acc = new Account() { AccountName = "abc", Language = "RUS" };
            jsonHelper.AddNewAccount(acc);
            jsonHelper.DeleteAccount("abc");
            Console.ReadLine();
        }
    }
}
