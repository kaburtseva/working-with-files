using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonIntegration
{
    class JsonHelpers
    {
        private string PathToFile;
        public JsonHelpers(string pathToFile)
        {
            PathToFile = pathToFile;
        }
        private string DuplicatePathToFile = @"E:\WorkWithFiles\DataIntegration\JsonIntegration\AccountsDuplicate.json";
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
            AddNewAccount(accountToUpdate);

        }

        public void AddNewAccount(Account account)
        {
            var jsonString = File.ReadAllText(PathToFile);
            var list = JsonConvert.DeserializeObject<List<Account>>(jsonString);
            list.Add(account);
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
            var jsonString = File.ReadAllText(PathToFile);
            File.WriteAllText(DuplicatePathToFile, jsonString);
        }

        public void ResetToOldFile()
        {
            var jsonString = File.ReadAllText(DuplicatePathToFile);
            File.WriteAllText(PathToFile, jsonString);
        }

    }
}
