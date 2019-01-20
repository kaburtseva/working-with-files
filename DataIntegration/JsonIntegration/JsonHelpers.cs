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

        string readResult = string.Empty;
        string writeResult = string.Empty;

        public Account GetAccount(string parameter, string parameterName)
        {
            using (StreamReader r = new StreamReader(PathToFile))
            {
                var jsonString = r.ReadToEnd();
                JArray jarr = JArray.Parse(jsonString);
                var token = $"$.[?(@" + parameter + "== '" + parameterName + "')]";
                //if (Array.Exists(jarr, element => element == parameterName))
                try
                {

                    JToken accountData = jarr.SelectToken(token);
                    return accountData.ToObject<Account>();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Current account isn't exist. Details: {ex.Message}");
                }

            }
        }

        public void UpdateAccount(Account account)
        {
            var jsonString = File.ReadAllText(PathToFile);
            try
            {
                string jsonData = JsonConvert.SerializeObject(account, Formatting.Indented);
                File.WriteAllText(PathToFile, jsonData);
            }
            catch
            {
                Console.WriteLine("Account isn't exist");
            }

        }
              
        public void AddNewAccount(string accountName)
        {
            var jsonString = File.ReadAllText(PathToFile);
            var list = JsonConvert.DeserializeObject<List<Account>>(jsonString);
            list.Add(new Account(accountName));
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(PathToFile, convertedJson);

        }

        public void PrintAllData()
        {
            Console.WriteLine(File.ReadAllText(PathToFile));
        }
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



        public void EditAndUpdateNode(string old = "Kate", string newValue = "T")
        {
            using (StreamReader r = new StreamReader(PathToFile))
            {
                var jsonString = r.ReadToEnd();
                var jobj = JObject.Parse(jsonString);
                readResult = jobj.ToString();
                foreach (var item in jobj.Properties())
                {
                    item.Value = item.Value.ToString().Replace("Kate", "Katyusha");
                }
                writeResult = jobj.ToString();
                Console.WriteLine(jobj);
            }
            Console.WriteLine(readResult);
            File.WriteAllText(PathToFile, writeResult);
        }
    }
}
