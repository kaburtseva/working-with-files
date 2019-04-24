using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileProcessors
    
{
    public class JsonEditor : IFileEditor
    {
        private string PathToFile;
        private string DuplicatePathToFile;
        private static string sourceDir = @"C:\Users\kateryna.burtseva\Documents\working-with-files\working-with-files\DataIntegration\JsonIntegration\";
        public JsonEditor(string pathToFile)
        {
            PathToFile = pathToFile;

        }

        public Account GetAccount(string parameter, string parameterName)
        {
            using (StreamReader r = new StreamReader(PathToFile))
            {
                var jsonString = r.ReadToEnd();
                JArray jarr = JArray.Parse(jsonString);
                var token = $"$.[?(@" + parameter + "== '" + parameterName + "')]";
                try
                {
                    JToken accountData = jarr.SelectToken(token);
                    return accountData.ToObject<Account>();
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException($"Current account isn't exist. Details: {ex.Message}");
                }

            }
        }

        public void UpdateAccount(Account accountToUpdate)
        {
            Account account = GetAccount("AccountName", accountToUpdate.AccountName);
            DeleteAccount(accountToUpdate);
            AddNewRecord(accountToUpdate);

        }

        public void AddNewRecord<T>(T record)
        {
            var jsonString = File.ReadAllText(PathToFile);
            var list = JsonConvert.DeserializeObject<List<T>>(jsonString);
            list.Add(record);
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            File.WriteAllText(PathToFile, convertedJson);
        }

        public void PrintAllData()
        {
            Console.WriteLine(File.ReadAllText(PathToFile));
        }

        public void DeleteAccount(Account account)
            => DeleteAccount(account.AccountName);


        public void DeleteAccount(string accountName)
        {
            var jsonString = File.ReadAllText(PathToFile);
            var list = JsonConvert.DeserializeObject<List<Account>>(jsonString);
            try
            {
                list.RemoveAll(account => account.AccountName == accountName);
                var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(PathToFile, convertedJson);
            }
            catch
            {
                Console.WriteLine("Current account isn't exist");
            }
        }

        public void DulicateExistingFile()
        {
            string sourceDirectory = Path.GetDirectoryName(PathToFile);
            string filenameWithoutExtension = Path.GetFileNameWithoutExtension(PathToFile);
            string fileExtension = Path.GetExtension(PathToFile);
            string destFileName = Path.Combine(sourceDirectory, filenameWithoutExtension + "-dub" + fileExtension);
            DuplicatePathToFile = destFileName;
            File.Copy(PathToFile, destFileName, true);

        }


        public void ResetToOldFile()
        {
            var jsonString = File.ReadAllText(DuplicatePathToFile);
            File.WriteAllText(PathToFile, jsonString);
        }

    }
}

