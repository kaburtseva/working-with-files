using Model;
using System;

namespace DataIntegration
{
    public class DataExcelIntegration
    {

        private static readonly string pathToFile = @"E:\WorkWithFiles\DataIntegration\DataIntegration\Accounts_excels2.xlsx";
        static void Main(string[] args)
        {
            ExcelHelpers.KillExcell();
            ExcelHelpers exhelpers = new ExcelHelpers(pathToFile);
            Account KaterinaAccount = exhelpers.GetAccount("Katerina");
            KaterinaAccount.FirstName = "Kate2";
            exhelpers.UpdateAccount(KaterinaAccount);            
            var acc = new Account() { AccountName = "abc", Language = "RUS" };
            exhelpers.AddNewAccount(acc);
            exhelpers.DeleteAccount("abc");
            exhelpers.DeleteAccount("Alonka");
            Console.ReadLine();

        }
    }
}