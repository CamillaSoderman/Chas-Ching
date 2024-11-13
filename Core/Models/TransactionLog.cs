namespace Chas_Ching.Core.Models
{
    public static class TransactionLog
    {
        private static readonly List<Transaction> transactions = new List<Transaction>();
        private static readonly object _lock = new object();

        public static void LogTransaction(Transaction transaction)
        {
            lock (_lock)
            {
                // Check if transaction already exists in the log
                if (!transactions.Any(t => t.TransactionId == transaction.TransactionId))
                {
                    transactions.Add(transaction);
                }
            }
        }
        public static List<Transaction> GetTransactionHistory(Account account)
        {
            lock (_lock)
            {
                return transactions
                    .Where(t => t.FromAccount.AccountId == account.AccountId ||
                               t.ToAccount.AccountId == account.AccountId)
                    .ToList();
            }
        }
        /* // Metoden används inte. Logiken är flyttad till GetTransactionHistory(Account account)
        public static List<Transaction> GetTransactionsForAccount(int accountId)
        {
            return transactions.Where(t => t.FromAccount.AccountId == accountId || t.ToAccount.AccountId == accountId).ToList();
        }
         */
    }
}

/// <summary>
/// Get TransactionHistory
/// Retrieves the transaction history for a given account, filtering transactions where the account
/// is either the source (FromAccount) or destination (ToAccount).
/// </summary>