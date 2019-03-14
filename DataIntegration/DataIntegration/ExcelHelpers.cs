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
using System.Diagnostics;

namespace DataIntegration
{
    public class ExcelHelpers
    {
        //TODO: Initiatilize excel method(open) - use in constructor        
        //TODO: Update, delete, add new accounts method

        private static string PathToFile;
        private string DuplicatePathToFile = null;
        public static Excel.Application xlApp;
        public static Excel.Workbook xlWorkbook;
        public static Excel._Worksheet xlWorksheet;
        Excel.Range xlRange;
        Dictionary<string, int> accountMapping;

        public ExcelHelpers(string pathToFile)
        {
            PathToFile = pathToFile;
            //InitializeExcel();
            //xlWorkbook = xlApp.Workbooks.Open(PathToFile);
        }

      
        public void InitializeExcel()
        {
            xlApp = new Excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(PathToFile);
            xlWorksheet = xlWorkbook.Sheets[1];
            xlRange = xlWorksheet.UsedRange;
        }
        public Dictionary<string, int> MatchContentToIndex()
        {
            List<string> accountProperties = typeof(Account).GetProperties().Select(p => p.Name).ToList();
            return GetPropertiesIndexes(accountProperties);
        }

        public Dictionary<string, int> GetPropertiesIndexes(List<string> accountProperties)
        {
            InitializeExcel();

            Dictionary<string, int> accountMapping = new Dictionary<string, int>();

            Excel.Range row1 = xlRange.Rows["1:1"];

            foreach (string propertyName in accountProperties)
            {
                int colIndex = -1;
                foreach (Microsoft.Office.Interop.Excel.Range cell in row1.Cells)
                {
                    if (cell.Text == propertyName)
                    {
                        Console.WriteLine(cell.Text);
                        colIndex = cell.Column;
                    }
                }

                accountMapping.Add(propertyName, colIndex);
            }
            DisposeExcel();
            return accountMapping;

        }


        public List<Account> GetAllAccounts()
        {
            accountMapping = MatchContentToIndex();
            List<Account> accountList = new List<Account>();
            InitializeExcel();
            for (int rowIndex = 2; rowIndex <= 3; rowIndex++)
            {
                Account account = new Account();

                foreach (string propertyName in accountMapping.Keys)
                {
                    object rangeObject = xlRange.Cells[rowIndex, accountMapping[propertyName]];
                    Range range = (Range)rangeObject;
                    object rangeValue = range.Value2;
                    string value = rangeValue.ToString();
                    Type accountType = typeof(Account);
                    PropertyInfo myPropertyInfo = accountType.GetProperty(propertyName);
                    var converter = TypeDescriptor.GetConverter(myPropertyInfo.PropertyType);
                    var result = converter.ConvertFromInvariantString(value);
                    myPropertyInfo.SetValue(account, result);
                }

                accountList.Add(account);
            }
            DisposeExcel();
            return accountList;

        }

        public static void KillExcell()
        {
            try
            {
                Process[] procList = Process.GetProcessesByName("EXCEL");

                foreach (Process proc in procList)
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public Account GetAccount(string accountName)
        {
            var accountList = GetAllAccounts();
            var acc = accountList.Where(i => i.AccountName == accountName).FirstOrDefault();
            return acc;
        }

        public Account UpdateAccount(string parameter)
        {
            var accountList = GetAllAccounts();
            return new Account();
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
        public void DisposeExcel()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
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

