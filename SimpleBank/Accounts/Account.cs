using SimpleBank.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Accounts
{
    public class Account
    {
        public string Owner { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; private set; }

        public Account(string owner, decimal balance)
        {
            Owner = owner;
            Balance = balance;
            Transactions = new List<Transaction>();
        }

        public virtual bool IsWithdrawalLimitPreserved(decimal amount) 
            => amount <= Balance;
    }
}
