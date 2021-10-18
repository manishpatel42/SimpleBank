using SimpleBank.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank
{
    public class Bank
    {
        public string Name { get; set; }

        public List<Account> Accounts { get; set; }

        public Bank(string name)
        {
            Name = name;
        }

        public Account GetAccountByOwnerName(string ownerName)
            => Accounts.FirstOrDefault(a => a.Owner == ownerName);
    }
}
