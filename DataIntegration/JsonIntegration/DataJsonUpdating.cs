using Model;
using System;

namespace JsonIntegration
{
    public class DataJsonUpdating
    {
        private static string pathToFile = @"C:\Users\kateryna.burtseva\Documents\working-with-files\working-with-files\DataIntegration\JsonIntegration\Accounts1.json";

        static void Main(string[] args)
        {
            JsonHelpers jsonHelper = new JsonHelpers(pathToFile);
            jsonHelper.DulicateExistingFile();
            Account KateAccount = jsonHelper.GetAccount("AccountName", "Katerina");
            Console.WriteLine(KateAccount.ToString());
            jsonHelper.PrintAllData();
            KateAccount.FirstName = "Kate2";
            jsonHelper.UpdateAccount(KateAccount);
            jsonHelper.PrintAllData();
            var acc = new Account() { AccountName = "abc", Language = "RUS" };
            jsonHelper.AddNewAccount(acc);
            jsonHelper.DeleteAccount("abc");
            jsonHelper.DeleteAccount("Alonka");
            jsonHelper.ResetToOldFile();
            Console.ReadLine();
        }
    }
}
