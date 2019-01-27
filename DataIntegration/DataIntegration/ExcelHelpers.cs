using Aspose.Cells;
using Aspose.Cells.Tables;
using ExcelIntegration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataIntegration
{
    public class ExcelHelpers
    {
        private string PathToFile;
        public ExcelHelpers(string pathToFile)
        {
            PathToFile = pathToFile;
        }

        private string DuplicatePathToFile = @"E:\WorkWithFiles\DataIntegration\DataIntegration\Accounts_excelDuplicate.xlsx";
        public ExcelHelpers()
        {
            
        }
        public void SelectAccount(string accountName)
        {
            FileStream fstream = new FileStream(PathToFile, FileMode.Open);
            Workbook workbook = new Workbook(fstream);
            Worksheet worksheet = workbook.Worksheets["Sheet1"];
            worksheet.AutoFilter.Custom(0, FilterOperatorType.Contains, accountName);
            worksheet.AutoFilter.Refresh();
            workbook.Save(DuplicatePathToFile);

            
        }

        public Account GetAccount(string accountName)
        {
            SelectAccount(accountName);
            FileStream fstream = new FileStream(DuplicatePathToFile, FileMode.Open);
            Workbook workbook = new Workbook(fstream);
            Worksheet worksheet = workbook.Worksheets["Sheet1"];
            Aspose.Cells.Tables.ListObjectCollection listObjects = workbook.Worksheets[0].ListObjects;
            List<Account> oList = listObjects.Cast<Account>().ToList();
            return new Account();
        }

        public void UpdateAccount()
        {

        }
        public void AddNewAccount(Account account)
        {
            Workbook wb = new Workbook(PathToFile);           
            Worksheet worksheet = wb.Worksheets[0];            
            Cells cells = worksheet.Cells;           
            List<string> myList = new List<string>();
            int col = 9;  
            int last_row = worksheet.Cells.GetLastDataRow(col);
            
            for (int i = 8; i <= last_row; i++)
            {
                myList.Add(cells[i, col].Value.ToString());
            }           
            List<Account> oList = myList.Cast<Account>().ToList();
            oList.Add(account);
            wb.Save(DuplicatePathToFile);

        }
    
        public void DeleteAccount()
        {
            //workbook.Worksheets.RemoveAt("Sheet1");
        }
        public void PrintAllData()
        {

        }

        public void DuplicateCurrentFile()
        {
            var exString = File.ReadAllText(PathToFile);
            File.WriteAllText(DuplicatePathToFile, exString);
        }

        public void ResetOldFile()
        {
            var exString = File.ReadAllText(DuplicatePathToFile);
            File.WriteAllText(PathToFile, exString);
        }
    }
}
