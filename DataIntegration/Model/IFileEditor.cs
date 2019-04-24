using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IFileEditor
    {
        Account GetAccount(string parameter, string accountName);
        void AddNewRecord<T>(T record);
        void DeleteAccount(Account account);
        void UpdateAccount(Account accountToUpdate);

    }
}
