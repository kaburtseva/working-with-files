using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonIntegration
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
        public DateTime ExpirationDate { get; set; }
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
            new Account()
            {
                AccountName = AccountName


            };
            return ("Account name is {0}, first name is {2}") ;
        }
    }
}
