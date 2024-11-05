using Chas_Ching.Core.Enums;
using Chas_Ching.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models
{
    public class Account : IAccount
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public CurrencyType Currency { get; set; }

        // Method to get the balance of an account
        public void GetBalance()
        {
            Console.WriteLine($"Current balance: {Balance}");
        }
        
        // Method to deposit money into an account
        public void Deposit()
        {
            Console.Write("Enter amount to deposit: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Balance += amount;
            Console.WriteLine($"Deposit successful. New balance: {Balance}");
        }
        
        // Method to withdraw money from an account
        public void Withdraw()
        {
            Console.Write("Enter amount to withdraw: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds");
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawal successful. New balance: {Balance}");
            }
        }
        
        // Method to transfer money from one account to another
        public void Transfer()
        {
            Console.Write("Enter amount to transfer: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds");
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"Transfer successful. New balance: {Balance}");
            }
            
        }
        
        public void Saldo()
        {
            throw new NotImplementedException();
        }
    }
}