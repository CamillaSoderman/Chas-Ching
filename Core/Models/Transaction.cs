
using Chas_Ching.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models

{
    public class Transaction 
    {
        public enum TransactionStatus
        {
            Pending,
            Completed,
            Failed
        }
        public TransactionStatus Status { get; set; }
        CurrencyType currency = new CurrencyType();
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public DateTime Date { get; set; }
        public CurrencyType FromCurrency { get; private set; }
        public CurrencyType ToCurrency { get; private set; }
        public int RetryCount { get; set; } = 0;
        public const int MaxRetries = 3;

        public Transaction(decimal amount, Account fromAccount, Account toAccount)
        {
            Amount = amount;
            FromAccount = fromAccount;
            ToAccount = toAccount;

            TransactionId = Guid.NewGuid();
            Date = DateTime.Now;
            Status = TransactionStatus.Pending;
            FromCurrency = fromAccount.Currency;
            ToCurrency = toAccount.Currency;
        }
        public void ProcessTransaction()
        {
            //Check if accounts exist
            if (FromAccount == null || ToAccount == null)
            {
                Console.WriteLine("Invalid Account, Transaction failed.");
                Status = TransactionStatus.Failed;
                return;
            }

            decimal amountToDeduct = Amount;
            decimal amountToCredit = Amount;

            if (FromCurrency != ToCurrency)
            {
                amountToCredit = CurrencyExchange.Convert(Amount, FromCurrency, ToCurrency);
            }

            if (FromAccount.Balance >= amountToDeduct)
            {
                FromAccount.Balance -= amountToDeduct;
                ToAccount.Balance += amountToCredit;
                Status = TransactionStatus.Completed;
                Console.WriteLine($"Transaction of {Amount} {FromCurrency} from Account {FromAccount.AccountId} to account {ToAccount.AccountId}  successfully. ");
            }
            else
            {
                Console.WriteLine("Transaction failed: Insufficient funds.");
                Status = TransactionStatus.Failed;
            }
            TransactionLog.LogTransaction(this);
        }
    }
}