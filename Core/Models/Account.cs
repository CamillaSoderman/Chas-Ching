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
    }
}