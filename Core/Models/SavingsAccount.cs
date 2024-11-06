using Chas_Ching.Core.Enums;
using Chas_Ching.Core.Interfaces;



// Purpose: Model for Savings Account. Inherits from Account class. Contains Interest Rate property and Deposit method.
namespace Chas_Ching.Core.Models;

public class SavingsAccount : Account
{
    public decimal InterestRate { get; set; } = 2.5m; // Annual Interest Rate in % (Changeable)
    
    public SavingsAccount(string accountId, decimal balance, CurrencyType currency) : base(accountId, balance, currency)
    {
       
    }
    
    // Method to calculate interest on a deposit into a savings account
    public decimal CalculateInterest(decimal amount)
    {
        return (amount * InterestRate) / 100;
    }
}