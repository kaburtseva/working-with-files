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
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel;

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
        Dictionary<string, int> accountMapping;
        public Dictionary<string, int> MatchContentToIndex()
        {
            List<string> accountProperties = typeof(Account).GetProperties().Select(p => p.Name).ToList();
            Dictionary<string, int> accountMapping = new Dictionary<string, int>();

            foreach (string propertyName in accountProperties)
            {
                int propertyIndex = GetPropertyIndex(propertyName);
                accountMapping.Add(propertyName, propertyIndex);
            }

            return accountMapping;

        }
        public int GetPropertyIndex(string propertyName)
        {

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(PathToFile);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            Excel.Range row1 = xlRange.Rows["1:1"];

            int colIndex = -1;
            foreach (Microsoft.Office.Interop.Excel.Range cell in row1.Cells)
            {
                if (cell.Text == propertyName)
                {
                    Console.WriteLine(cell.Text);
                    colIndex = cell.Column;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            return colIndex;
        }

        public List<Account> GetAllAccounts()
        {
            accountMapping = MatchContentToIndex();
            List<Account> accountList = new List<Account>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(PathToFile);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            for (int rowIndex = 2; rowIndex <= 3; rowIndex++)
            {
                Account account = new Account();

            foreach (string propertyName in accountMapping.Keys)
                {
                    object rangeObject = xlRange.Cells[rowIndex, accountMapping[propertyName]];
                    Range range = (Range)rangeObject;
                    object rangeValue = range.Value2;
                    string value = rangeValue.ToString();
                    //DateTime dt = DateTime.FromOADate(value);
                    Type accountType = typeof(Account);
                    PropertyInfo myPropertyInfo = accountType.GetProperty(propertyName);
                    var converter = TypeDescriptor.GetConverter(myPropertyInfo.PropertyType);
                    var result = converter.ConvertFrom(value);
                   
                    myPropertyInfo.SetValue(account, result);
                }

                accountList.Add(account);
            }

            return accountList;

        }

        public List<Account> GetAccount(string accountName)
        {
            var accountList = GetAllAccounts();
            var  account = new List<Account>();
            accountList.Where(i => i.AccountName == accountName);
            return account;
        }

        public List<Account> UpdateAccount()
        {
            List<Account> accountList = new List<Account>();
            return accountList;
        }
        public void AddNewAccount(Account account)
        {


        }

        public void DeleteAccount()
        {

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

