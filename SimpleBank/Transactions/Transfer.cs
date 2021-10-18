using SimpleBank.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Transactions
{
    public class Transfer : Transaction
    {
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        
        public Transfer(decimal amount, Account fromAccount, Account toAccount) : base(amount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
        }

        public override bool Process()
        {
            if (FromAccount.IsWithdrawalLimitPreserved(Amount))
            {
                FromAccount.Balance -= Amount;
                ToAccount.Balance += Amount;

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Transfer of ${Amount:0.00} has been made from {FromAccount.Owner} to {ToAccount.Owner}";
        }
    }
}
