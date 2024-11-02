
using Chas_Ching.Core.Enums;
using Chas_Ching.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models

{
    public class Transaction : ITransactions
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public DateTime Date { get; set; }
        public Transaction(int transactionId, decimal amount, Account fromAccount, Account toAccount)
        {
            TransactionId = transactionId;
            Amount = amount;
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Date = DateTime.Now;
        }
        public void CreateTransactions()
        {
            if (FromAccount.Balance >= Amount)
            {
                FromAccount.Balance -= Amount;
                ToAccount.Balance += Amount;

                Console.WriteLine($"Transaktionen på: {Amount} kronor är nu genomförd.");
            }
            else
            {
                Console.WriteLine("Transaktionen kunde ej genomföras, för lite pengar på kontot.");
            }

            //throw new NotImplementedException();
        }
    }
}