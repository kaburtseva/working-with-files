using Model;
using System;

namespace DataIntegration
{
    public class DataExcelIntegration
    {

        private static readonly string pathToFile = @"C:\Users\kateryna.burtseva\Documents\working-with-files\working-with-files\DataIntegration\DataIntegration\Accounts_excels4.xlsx";
        static void Main(string[] args)
        {
            ExcelHelper.KillExcell();


            using (ExcelHelper exhelper = new ExcelHelper(pathToFile))
            {
                exhelper.DuplicateCurrentFile();
                Account AlAccount = exhelper.GetAccount("Alonka");
                exhelper.DeleteAccount(AlAccount);
                Account KaterinaAccount = exhelper.GetAccount("Katerina");
                AlAccount.FirstName = "K";
                exhelper.UpdateAccount(KaterinaAccount);
                var acc = new Account()
                {
                    AccountName = "Vasya",
                    Language = "RUS",

                };
                exhelper.AddNewAccount(acc);
                exhelper.DeleteAccount("Vasya");
                exhelper.Dispose();
                exhelper.ResetOldFile();
            }
        }
    }
}