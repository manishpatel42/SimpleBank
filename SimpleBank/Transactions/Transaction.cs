using SimpleBank.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Transactions
{
    public class Transaction
    {
        public decimal Amount { get; set; }

        public Transaction(decimal amount)
        {
            Amount = amount;
        }

        public virtual bool Process()
        {
            return true;
        }        

        public override string ToString()
        {
            return "Transaction has been made";
        }
    }
}