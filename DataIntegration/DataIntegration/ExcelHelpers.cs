using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Excel.Application;
using System.Data.OleDb;

namespace DataIntegration
{
    public class ExcelHelpers
    {
        private string PathToFile;
        private string DuplicatePathToFile = null;

        public ExcelHelpers(string pathToFile)
        {
            PathToFile = pathToFile;
        }
        private int columnSize = 9;
        private int rowSize = 3;

        public void MatchContentToIndex()
        {
            List<string> accountProperties = typeof(Account).GetProperties().Select(p => p.Name).ToList();
            Dictionary<string, int> accountMapping = new Dictionary<string, int>();

            foreach (string propertyName in accountProperties)
            {
                int propertyIndex = GetPropertyIndex(propertyName);
                accountMapping.Add(propertyName, propertyIndex);
            }
        }
        public int GetPropertyIndex(string propertyName)
        {

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(PathToFile);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            Excel.Range row1 = xlRange.Rows["1:1"];
            int colIndex = 0;
            foreach (Microsoft.Office.Interop.Excel.Range cell in row1.Cells)
            {
                if (cell.Text == propertyName)
                {
                    Console.WriteLine(cell.Text);
                    colIndex = cell.Column;
                }
            }

            return colIndex;
        }




        public Account GetAccount(string accountName)
        {

            return new Account();
        }

        public void UpdateAccount()
        {

        }
        public void AddNewAccount(Account account)
        {
            //   Workbook wb = new Workbook(PathToFile);
            /// Worksheet worksheet = wb.Worksheets[0];
            //  Cells cells = worksheet.Cells;
            // List<string> myList = new List<string>();
            // int col = 9;
            // int last_row = worksheet.Cells.GetLastDataRow(col);

            //  for (int i = 8; i <= last_row; i++)
            //  {
            //      myList.Add(cells[i, col].Value.ToString());
            //  }
            // List<Account> oList = myList.Cast<Account>().ToList();
            //  oList.Add(account);
            //  wb.Save(DuplicatePathToFile);

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
            string sourceDirectory = Path.GetDirectoryName(PathToFile);
            string filenameWithoutExtension = Path.GetFileNameWithoutExtension(PathToFile);
            string fileExtension = Path.GetExtension(PathToFile);
            string destFileName = Path.Combine(sourceDirectory, filenameWithoutExtension + "-dub" + fileExtension);
            DuplicatePathToFile = destFileName;
            File.Copy(PathToFile, destFileName, true);
        }

        public void ResetOldFile()
        {
            var exString = File.ReadAllText(DuplicatePathToFile);
            File.WriteAllText(PathToFile, exString);
        }
    }
}

