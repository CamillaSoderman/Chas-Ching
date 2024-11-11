using Chas_Ching.Core.Enums;

namespace Chas_Ching.Core.Models
{
    public class Transaction
    {
        public TransactionStatus Status { get; set; }
        private CurrencyType currency = new CurrencyType();
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
                FromAccount?.ReleaseFunds(Amount); // Release reserved funds in case of error
                return;
            }

            decimal amountToDeduct = Amount;
            decimal amountToCredit = Amount;

            // Take care of currency conversion if needed
            if (FromCurrency != ToCurrency)
            {
                try
                {
                    amountToCredit = CurrencyExchange.Convert(Amount, FromCurrency, ToCurrency);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Currency conversion failed: {ex.Message}");
                    FromAccount.ReleaseFunds(Amount); // Release reserved funds in case of error
                    Status = TransactionStatus.Failed;
                    return;
                }
            }
            else
            {
                amountToCredit = Amount;
            }

            // Check if there money is still reserved
            if (FromAccount.PendingAmount >= amountToDeduct)
            {
                FromAccount.Withdraw(amountToDeduct);
                ToAccount.Balance += amountToCredit;
                Status = TransactionStatus.Completed;
                TransactionLog.LogTransaction(this);
                Console.WriteLine($"Transaction of {Amount} {FromCurrency} from Account {FromAccount.AccountId} to account {ToAccount.AccountId}  successfully. ");
            }
            else
            {
                Console.WriteLine("Transaction failed: Insufficient funds.");
                Status = TransactionStatus.Failed;
                FromAccount.ReleaseFunds(Amount); // Släpp reserverade pengar vid fel
            }
            TransactionLog.LogTransaction(this);
        }
    }
}