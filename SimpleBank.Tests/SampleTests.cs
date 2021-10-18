using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBank.Accounts;
using SimpleBank.Transactions;
using AutoFixture;

using Moq;

using NUnit.Framework;

namespace SimpleBank.Tests
{
    [TestFixture]
    public class SampleTests
    {
        private Fixture _fixture;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Deposit_SuccessfulTest()
        {
            var bank = InitializeBankData();
            var account = bank.GetAccountByOwnerName("Mike's Account");
            var startingBalance = account.Balance;

            var deposit = new Deposit(1000, account);
            bool isDepositSuccessful = deposit.Process();

            Assert.That(isDepositSuccessful == true);
            Assert.That(account.Balance > startingBalance);
            Assert.That(account.Balance == 11000);
        }

        [Test]
        public void WithdrawlBelowLimit_SuccessfulTest()
        {
            var bank = InitializeBankData();
            var account = bank.GetAccountByOwnerName("Mike's Account");
            var startingBalance = account.Balance;

            var withdraw = new Withdraw(400, account);
            bool isWithdrawSuccessful = withdraw.Process();

            Assert.That(isWithdrawSuccessful == true);
            Assert.That(startingBalance != account.Balance);
            Assert.That(account.Balance == 9600);
        }

        [Test]
        public void WithdrawlOverLimit_UnSuccessfulTest()
        {
            var bank = InitializeBankData();
            var account = bank.GetAccountByOwnerName("Mike's Account");
            var startingBalance = account.Balance;

            var withdraw = new Withdraw(600, account);
            bool isWithdrawSuccessful = withdraw.Process();

            Assert.That(isWithdrawSuccessful == false);
            Assert.That(startingBalance == account.Balance);
            Assert.That(account.Balance == 10000);
        }

        [Test]
        public void TransferBelowWithdrawalLimit_SuccessfulTest()
        {
            var bank = InitializeBankData();
            var fromAccount = bank.GetAccountByOwnerName("Riley's Account");
            var toAccount = bank.GetAccountByOwnerName("Carl's Account");
            var startingFromBalance = fromAccount.Balance;
            var startingToBalance = toAccount.Balance;

            var transfer = new Transfer(2000, fromAccount, toAccount);
            var isTransferSuccessful = transfer.Process();
            
            Assert.That(isTransferSuccessful == true);
            Assert.That(startingFromBalance != fromAccount.Balance);
            Assert.That(startingToBalance != toAccount.Balance);
            Assert.That(startingFromBalance + startingToBalance == fromAccount.Balance + toAccount.Balance);
        }

        [Test]
        public void TransferOverWithdrawalLimit_UnSuccessfulTest()
        {
            var bank = InitializeBankData();
            var fromAccount = bank.GetAccountByOwnerName("Mike's Account");
            var toAccount = bank.GetAccountByOwnerName("Carl's Account");
            var startingFromBalance = fromAccount.Balance;
            var startingToBalance = toAccount.Balance;

            var transfer = new Transfer(2000, fromAccount, toAccount);
            var isTransferSuccessful = transfer.Process();

            Assert.That(isTransferSuccessful == false);
            Assert.That(startingFromBalance == fromAccount.Balance);
            Assert.That(startingToBalance == toAccount.Balance);
        }

        [Test]
        public void TrackTransactionsTest()
        {
            var bank = InitializeBankData();
            var account = bank.GetAccountByOwnerName("Mike's Account");

            var deposit1 = new Deposit(1000, account);
            deposit1.Process();
            account.Transactions.Add(deposit1);

            var deposit2 = new Deposit(2000, account);
            deposit2.Process();
            account.Transactions.Add(deposit2);

            var withdraw1 = new Withdraw(500, account);
            withdraw1.Process();
            account.Transactions.Add(withdraw1);

            var withdraw2 = new Withdraw(700, account);
            withdraw2.Process();
            account.Transactions.Add(withdraw2);

            var transaction1Str = "Deposit of $1000.00 has been made to Mike's Account. Old Balance was $10000.00. New Balance is $11000.00";
            var transaction2Str = "Deposit of $2000.00 has been made to Mike's Account. Old Balance was $11000.00. New Balance is $13000.00";
            var transaction3Str = "Withdrawl of $500.00 has been made to Mike's Account. Old Balance was $13000.00. New Balance is $12500.00";
            var transaction4Str = "Withdrawal amount was over the limit. Rollback Transaction Occur.";
            
            Assert.That(account.Transactions[0].ToString() == transaction1Str);
            Assert.That(account.Transactions[1].ToString() == transaction2Str);
            Assert.That(account.Transactions[2].ToString() == transaction3Str);
            Assert.That(account.Transactions[3].ToString() == transaction4Str);
        }

        private Bank InitializeBankData()
        {
            Bank bank = _fixture.Build<Bank>().With(x => x.Name, "Simple Bank").Create();
            bank.Accounts.Add(_fixture.Build<Individual>().With(i => i.Owner, "Mike's Account").With(i => i.Balance, 10000).Create());
            bank.Accounts.Add(_fixture.Build<Corporate>().With(c => c.Owner, "Riley's Account").With(c => c.Balance, 10000).Create());
            bank.Accounts.Add(_fixture.Build<Checking>().With(c => c.Owner, "Carl's Account").With(c => c.Balance, 10000).Create());
            return bank;
        }       
    }
}
