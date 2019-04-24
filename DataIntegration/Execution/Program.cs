using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessor;
using Model;

namespace Execution
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataProcessor = FileProcessorFactory.GetFileEditor(@"C:\Users\kateryna.burtseva\Documents\working-with-files\working-with-files\DataIntegration\JsonIntegration\Accounts1.json", typeof(Account));
            var kateAcc = dataProcessor.GetAccount("AccountName", "Alonka");

            Person person = new Person();
            dataProcessor.AddNewRecord<Person>(person);



        }
    }
}
