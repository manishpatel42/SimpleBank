using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Accounts
{
    public class Individual : Investment
    {
        public decimal WithdrawlLimit { get; private set; }

        public Individual(string owner, decimal balance) : base(owner, balance)
        {
            WithdrawlLimit = 500;
        }

        public override bool IsWithdrawalLimitPreserved(decimal amount) 
            => amount <= Balance && amount <= WithdrawlLimit;
    }
}
