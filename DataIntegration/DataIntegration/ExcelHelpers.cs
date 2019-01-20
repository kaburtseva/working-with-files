using System;
using System.Collections.Generic;
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
        public ExcelHelpers()
        {
            
        }
        public void GetAccount()
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(PathToFile);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 2; j++)
                {
                    
                    if (j == 1)
                        Console.Write("\r\n");

                   
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");

                    
                }
            }
        }
        public void UpdateAccount()
        {

        }
        public void AddNewAccount()
        {

        }
        public void DeleteAccount()
        {

        }
        public void PrintAllData()
        {

        }
    }
}
