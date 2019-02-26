using Model;

namespace DataIntegration
{
    public class DataExcelIntegration
    {

        private static readonly string pathToFile = @"C:\Users\kateryna.burtseva\Documents\working-with-files\working-with-files\DataIntegration\DataIntegration\Accounts_excel.xlsx";
        static void Main(string[] args)
        {
            ExcelHelpers exhelpers = new ExcelHelpers(pathToFile);

             exhelpers.GetAllAccounts();
           


        }
    }
}