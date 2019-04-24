using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileProcessors;
using Model;

namespace DataProcessor
{
    public static class FileProcessorFactory
    {
        public static IFileEditor GetFileEditor(string pathToFile, Type type)
        {
            string fileExtension = Path.GetExtension(pathToFile);
            if (fileExtension == ".xlsx" || fileExtension == ".xls")
            {
                return new ExcelEditor(pathToFile);
            }
            else if (fileExtension == ".json")
            {
                return new JsonEditor(pathToFile);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
