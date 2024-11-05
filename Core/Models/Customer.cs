using Chas_Ching.Core.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models
{
    public class Customer : User
    {
        Random randomID = new Random();
        int userRandomId;

        //KRAV: Som användare vill jag kunna se en lista med alla mina bankkonton och saldot på dessa.

        // Declare Accounts as a list of type Account
        public List<Account> Accounts { get; set; }

        public decimal Loan { get; set; }

        //Constructor
        public Customer(string userEmail, string password) : base(userEmail, password)
        {
            // Initialize Accounts-list
            Accounts = new List<Account>();

            Loan = Loan;
            userRandomId = randomID.Next(1000, 9999);
        }


        // Promts the user to enter a innitail balance and creates new Account.
        public void OpenAccount()
        {
            Console.WriteLine("Enter the initial balance for the new account: ");
            decimal initialBalance;

            while (!decimal.TryParse(Console.ReadLine(), out initialBalance) || initialBalance < 0)
            {
                Console.WriteLine("Invalid inpuut. Plaese enter a non-negativ number: ");
            }

            var newAccount = new Account(userRandomId, initialBalance, CurrencyType.SEK);
            Accounts.Add(newAccount);
            Console.WriteLine($"Account created with ID {newAccount.AccountId} and balance {newAccount.Balance}{CurrencyType.SEK}");
        }

        //Method displays all accounts and their balances
        public void ViewAccounts()
        {
            Console.WriteLine("Viewing accounts...");

            if (Accounts.Count == 0)
            {
                Console.WriteLine("No accounts found");
            }
            else
            {
                foreach (var account in Accounts)
                    Console.WriteLine($"Account ID: {account.AccountId}, Balance: {account.Balance}");

            }
        }
        //Placeholders for future implementation
        public void TransferFounds()
        {
            throw new NotImplementedException();

        }

        //Placeholders for future implementation
        public void RequestLoan()
        {
            throw new NotImplementedException();
        }

        //Placeholders for future implementation

        public void ViewTransaction()
        {
            throw new NotImplementedException();


        }
    }
}