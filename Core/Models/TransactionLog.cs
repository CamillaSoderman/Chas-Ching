namespace Chas_Ching.Core.Models
{
    public static class TransactionLog
    {
        public static List<Transaction> transactions = new List<Transaction>();

        public static void LogTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public static List<Transaction> GetTransactionHistory(Account account)
        {   // Retrieves transactions involving the given account as either sender or receiver.
            return transactions.Where(t =>
                t.FromAccount.AccountId == account.AccountId ||
                t.ToAccount.AccountId == account.AccountId).ToList();
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