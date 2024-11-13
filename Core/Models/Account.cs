using Chas_Ching.Core.Enums;

namespace Chas_Ching.Core.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public CurrencyType Currency { get; set; }
        public AccountType Type { get; set; } // AccountType is an enum with BankAccount, LoanAccount and SavingsAccount
        public decimal PendingAmount { get; set; } = 0; // Reserved amount to avoid overdraft when a pending transaction is ongoing
        public decimal InterestRate { get; set; } // Annual Interest Rate in %

        public Account(int accountNumber, decimal initialBalance, CurrencyType currency, AccountType type = AccountType.BankAccount)
        {
            AccountId = accountNumber;
            Balance = initialBalance;
            Currency = currency;
            Type = type;
            InterestRate = 0;
        }

        public decimal GetBalance()
        {   // Return the balance minus the pending amount
            return Balance - PendingAmount;
        }
        
        public void Deposit(decimal amount)
        {   // Method to deposit money into an account
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {   // Method to withdraw money from an account
            if (amount <= Balance)
            {
                Balance -= amount;
                // When the withdrawal is complete, also decrease the PendingAmount
                PendingAmount = Math.Max(0, PendingAmount - amount);
            }
        }

        public bool ReserveFunds(decimal amount)
        {   // Reerv funds to avoid overdraft when a pending transaction is ongoing
            if (GetBalance() >= amount)
            {
                PendingAmount += amount;
                return true;
            }
            return false;
        }

        public void ReleaseFunds(decimal amount)
        {   // Release reserved funds when a transaction is completed
            PendingAmount -= amount;
        }

        /* Borttagna metoder. Transfer sker via Transaction-klassen. Deposit används för nuvarande inte
        public void Deposit(decimal amount)
        {   // Method to deposit money into an account
            if (amount <= 0)
            {
                Console.WriteLine("Invalid amount");
            }
            else
            {
                Balance += amount;
                Console.WriteLine($"Deposit successful. New balance: {Balance}");
            }
        }

        public bool Transfer(decimal amount, Account toAccount, TransactionScheduler scheduler)
        {   // Method to transfer money from one account to another
            if (amount <= 0 || amount > Balance)
            {
                return false;
            }

            // Skapa transaktionen och lägg den i kön istället för att genomföra den direkt
            var transaction = new Transaction(amount, this, toAccount);
            scheduler.EnqueueTransaction(transaction);
            return true;
        }

        public bool TransferOwnAccounts(decimal amount, Account toAccount)
        {   // Ändrar från direktöverföring till väntande överföring
            if (amount <= 0 || amount > GetBalance())
            {
                return false;
            }

            decimal convertedAmount = amount;
            if (Currency != toAccount.Currency)
            {
                try
                {
                    convertedAmount = CurrencyExchange.Convert(amount, Currency, toAccount.Currency);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            if (!ReserveFunds(amount))
            {
                return false;
            }

            return true;
        }
         */
    }
}