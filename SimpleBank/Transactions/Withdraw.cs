using SimpleBank.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Transactions
{
    public class Withdraw : Transaction
    {
        public Account Account { get; set; }
        private decimal BeforeWithdrawBalance { get; set; }
        private decimal AfterWithdrawBalance { get; set; }

        public Withdraw(decimal amount, Account account) : base(amount)
        {
            Account = account;
        }

        public override bool Process()
        {
            BeforeWithdrawBalance = Account.Balance;

            if (Account.IsWithdrawalLimitPreserved(Amount))
            {                
                Account.Balance -= Amount;                                
            }

            AfterWithdrawBalance = Account.Balance;
            return BeforeWithdrawBalance != AfterWithdrawBalance;
        }

        public override string ToString()
        {
            return BeforeWithdrawBalance != AfterWithdrawBalance
                ? $"Withdrawl of ${Amount:0.00} has been made to {Account.Owner}. Old Balance was ${BeforeWithdrawBalance:0.00}. New Balance is ${AfterWithdrawBalance:0.00}"
                : $"Withdrawal amount was over the limit. Rollback Transaction Occur.";
        }
    }
}
