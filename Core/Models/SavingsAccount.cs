using Chas_Ching.Core.Enums;

// Purpose: Model for Savings Account. Inherits from Account class. Contains Interest Rate property and Deposit method.
namespace Chas_Ching.Core.Models;

public class SavingsAccount : Account
{
    public static decimal InterestRate { get; set; } = 2.5m; // Annual Interest Rate in % (Changeable)

    public SavingsAccount(int accountID, decimal balance, CurrencyType currency)
        : base(accountID, balance, currency)
    {
        
    }

    // Method to calculate interest on a deposit into a savings account
    public decimal CalculateInterest(decimal amount)
    {
        return (amount * InterestRate) / 100;
    }
}