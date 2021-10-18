using SimpleBank.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank.Transactions
{
    public class Deposit : Transaction
    {
        public Account Account { get; set; }

        private decimal BeforeDepositBalance { get; set; }
        private decimal AfterDepositBalance { get; set; }

        public Deposit(decimal amount, Account account) : base(amount)
        {
            Account = account;
        }

        public override bool Process()
        {
            BeforeDepositBalance = Account.Balance;
            
            Account.Balance += Amount;
            
            AfterDepositBalance = Account.Balance;
            
            return true;
        }

        public override string ToString()
        {
            return $"Deposit of ${Amount:0.00} has been made to {Account.Owner}. Old Balance was ${BeforeDepositBalance:0.00}. New Balance is ${AfterDepositBalance:0.00}";
        }
    }
}
