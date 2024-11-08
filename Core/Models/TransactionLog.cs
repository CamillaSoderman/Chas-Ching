using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models
{
    internal class TransactionLog
    {
        private static List<Transaction> transactions = new List<Transaction>();

        public static void LogTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public static List<Transaction> GetTransactionHistory()
        {
            return new List<Transaction>(transactions);
        }

        public static List<Transaction> GetTransactionsForAccount(int accountId)
        {
            return transactions.Where(t => t.FromAccount.AccountId == accountId || t.ToAccount.AccountId == accountId).ToList();
        }
    }
}
