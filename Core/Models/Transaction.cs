
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
        CurrencyType Currency { get; set; }
        public decimal Amount { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public DateTime Date { get; set; }

        public Transaction(Guid transactionId, CurrencyType currency, decimal amount, Account fromAccount, Account toAccount)
        {
            TransactionId = transactionId;
            Currency = currency;
            Amount = amount;
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Date = DateTime.Now;
            Status = TransactionStatus.Pending;
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
            if (FromAccount.Balance >= Amount)
            {
                FromAccount.Balance -= Amount;
                ToAccount.Balance += Amount;
                Status = TransactionStatus.Completed;
                Console.WriteLine($"Transaction of {Amount} {Currency} completed successfully. ");
            }
            else
            {
                Console.WriteLine("Transaction failed: Insufficient funds.");
                Status = TransactionStatus.Failed;
            }

        }
    }
}