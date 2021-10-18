using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Accounts
{
    public class Checking : Account
    {
        public Checking(string owner, decimal balance) : base(owner, balance)
        {

        }

        public override bool IsWithdrawalLimitPreserved(decimal amount) 
            => amount <= Balance;
    }
}
