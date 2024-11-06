﻿using Chas_Ching.Core.Enums;
using Chas_Ching.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models
{
    public class Account
    {
        public string AccountId { get; set; }
        public decimal Balance { get; set; }
        public CurrencyType Currency { get; set; }


        // Method to get the balance of an account

        public Account(string accountId, decimal balance, CurrencyType currency)
        {
            AccountId = accountId;
            Balance = balance;
            Currency = currency;
        }

        public void GetBalance(List<Account> accounts)
        {
            foreach (var account in accounts )
            {
                Console.WriteLine($"Current balance: {Balance}");
            }
        }
        
        // Method to deposit money into an account
        public void Deposit(decimal amount)
        {
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
        
        // Method to withdraw money from an account
        public void Withdraw(decimal amount)
        {
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
        public void Transfer(decimal amount)
        {
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
    }
}