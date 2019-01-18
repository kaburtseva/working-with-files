using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Model
{
    [Serializable]
    public class Account
    {
        public string FirstName { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Account() {
        }

        public Account(string firstname, DateTime expirationDate)
        {
            FirstName = firstname;
            ExpirationDate = expirationDate;
        }
        
        
}
