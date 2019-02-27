using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
        public bool Enabled { get; set; }
        public string Language { get; set; }
        private static string date = null;
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow; 
          
        public string AccountName { get; set; }
        
        public Account()
        {
         
        }

        public Account(string accountName, DateTime expirationDate)
        {
            AccountName = accountName;
            ExpirationDate = expirationDate;
        }

        public Account(string accountName)
        {
            AccountName = accountName;
        }

        public override string ToString()
        {
            return (
                $"Account Name: {this.AccountName} " +
                $"Login Name: {this.LoginName} " +
                $"First name: {this.FirstName} " +
                $"Last name: {this.LastName} " +
                $"Password: {this.Password} " +
                $"Enabled: {this.Enabled} " +
                $"Language: {this.Language}" +
                $"IsAdministrator: {this.IsAdministrator}" +
                $"ExpirationDate: {this.ExpirationDate}"
                );

        }
    }
}
