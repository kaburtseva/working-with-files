using ExcelIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIntegration
{
    public class DataExcelIntegration
    {

        private static readonly string pathToFile = @"E:\WorkWithFiles\DataIntegration\DataIntegration\Accounts_excel.xlsx";
        static void Main(string[] args)
        {
            ExcelHelpers exhelpers = new ExcelHelpers(pathToFile);

            Account KateAccount = exhelpers.GetAccount("Katerina");

           

        }
    }
}