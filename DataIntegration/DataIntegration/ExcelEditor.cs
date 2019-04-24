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

namespace FileProcessors

{
    public class ExcelEditor : IDisposable, IFileEditor

    {
        private static string PathToFile;
        private string DuplicatePathToFile = null;
        private string BackUp = null;
        public static Excel.Application xlApp;
        public static Excel.Workbook xlWorkbook;
        public static Excel._Worksheet xlWorksheet;
        Excel.Range xlRange;
        Dictionary<string, int> accountMapping;

        public ExcelEditor(string pathToFile)
        {
            if (File.Exists(pathToFile))
            {
                PathToFile = pathToFile;
                InitializeExcel();
                accountMapping = MatchContentToIndex();
            }
            else
                throw new FileNotFoundException($"File '{pathToFile}' does not exist");
        }

        public void InitializeExcel()
        {
            xlApp = new Excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(PathToFile);
            xlWorksheet = xlWorkbook.Sheets[1];
            xlRange = xlWorksheet.UsedRange;
        }

        public Dictionary<string, int> MatchContentToIndex<T>()
        {
            List<string> accountProperties = typeof(T).GetProperties().Select(p => p.Name).ToList();
            return GetPropertiesIndexes(accountProperties);
        }

        public Dictionary<string, int> GetPropertiesIndexes(List<string> accountProperties)
        {
            Dictionary<string, int> accountMapping = new Dictionary<string, int>();

            Excel.Range row1 = xlRange.Rows["1:1"];

            foreach (string propertyName in accountProperties)
            {
                int colIndex = -1;
                foreach (Microsoft.Office.Interop.Excel.Range cell in row1.Cells)
                {
                    if (cell.Text == propertyName)
                    {
                        colIndex = cell.Column;
                        Console.WriteLine($"Property '{cell.Text}' has index '{colIndex}'");
                    }
                }

                accountMapping.Add(propertyName, colIndex);
            }
            return accountMapping;

        }

        public void AddNewRecord<T>(T record)
        {
            var accountList = GetAllAccounts();
            int newRowIndex = accountList.Count + 2;

            try
            {
                foreach (string propertyName in accountMapping.Keys)
                {
                    Type accountType = typeof(Account);
                    PropertyInfo myPropertyInfo = accountType.GetProperty(propertyName);

                    string value = myPropertyInfo.GetValue(record, null)?.ToString() ?? string.Empty;
                    int newColumn = accountMapping[propertyName];
                    xlWorksheet.Cells[newRowIndex, newColumn].Value = value;
                }

                xlWorkbook.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add new record. {ex.ToString()}");
                this.Dispose();
                throw ex;
            }
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> accountList = new List<Account>();
            int lastUsedRow = xlWorksheet.Cells.Find("*", System.Reflection.Missing.Value,
                               System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                               Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious,
                               false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

            for (int rowIndex = 2; rowIndex <= lastUsedRow; rowIndex++)
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

        public Account GetAccount(string parameter, string accountName)
        {
            var accountList = GetAllAccounts();
            //TODO: Add string parameter name
            var acc = accountList.Where(i => i.AccountName == accountName).FirstOrDefault();
            return acc;
        }

        public void UpdateAccount(Account accountToUpdate)
        {
            Account account = GetAccount("AccountName", accountToUpdate.AccountName);
            DeleteAccount(accountToUpdate);
            AddNewRecord(accountToUpdate);
        }

        public void DeleteAccount(Account account)
            => DeleteAccount(account.AccountName);
        public void DeleteAccount(string accountName)
        {
            var accountList = GetAllAccounts();
            int rowIndex = accountList.IndexOf(accountList.Where(i => i.AccountName == accountName).FirstOrDefault());
            rowIndex = rowIndex + 2;
            xlWorksheet.Rows[rowIndex].Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
            xlWorkbook.Save();
        }

        public void PrintAllData()
        {

        }

        public void Dispose()
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
            Dispose();

        }

        public void ResetOldFile()
        {
            //var exString = File.ReadAllText(DuplicatePathToFile);
            //File.WriteAllText(PathToFile, exString);
            File.Replace(DuplicatePathToFile, PathToFile, BackUp);
        }
    }
}

