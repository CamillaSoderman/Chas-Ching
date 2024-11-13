using Chas_Ching.Core.Enums;

// Purpose: Model for Savings Account. Inherits from Account class. Contains Interest Rate property and Deposit method.
namespace Chas_Ching.Core.Models;

public class SavingsAccount : Account
{
    public SavingsAccount(int accountID, decimal balance, CurrencyType currency)
        : base(accountID, balance, currency, AccountType.SavingsAccount)
    {
        InterestRate = 2.5m; // Default interest rate for a savings account
    }
    
    // Placeholder for future implementation (Mao)
    public virtual void GetInterest(decimal amount)
    {
        decimal interest = CalculateInterest(amount);
        Deposit(interest);
    }

    // Method to calculate interest on a deposit into a savings account
    public decimal CalculateInterest(decimal amount)
    {
        return (amount * InterestRate) / 100;
    }
}