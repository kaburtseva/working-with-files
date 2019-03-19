using Model;
using System;

namespace DataIntegration
{
    public class DataExcelIntegration
    {

        private static readonly string pathToFile = @"E:\WorkWithFiles\DataIntegration\DataIntegration\Accounts_excels2.xlsx";
        static void Main(string[] args)
        {
            ExcelHelper.KillExcell();

            using (ExcelHelper exhelper = new ExcelHelper(pathToFile))
            {
                Account KaterinaAccount = exhelper.GetAccount("Katerina");
            }
        }
    }
}