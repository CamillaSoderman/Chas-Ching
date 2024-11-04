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
        }


        // Promts the user to enter a innitail balance and creates new Account.
        public void OpenAccount()
        {
            Console.WriteLine("Enter the initial balance for the new account: ");
            decimal initialBalamce;

            while (!decimal.TryParse(Console.ReadLine(), out initialBalamce) || initialBalamce < 0)
            {
                Console.WriteLine("Invalid inpuut. Plaese enter a non-negativ number: ");
            }

            var newAccount = new Account(Accounts.Count + 1, initialBalamce);
            Accounts.Add(newAccount);
            Console.WriteLine¤($"Account created with ID {newAccount.ID} and balance {newAccount.Balance}");
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
                    Console.WriteLine($"Account ID: {account.ID}, Balance: {account.Balance}");

            }

            //Placeholders for future implementation
            public void TransferFounds()
            {
           

            }

            //Placeholders for future implementation
            public void RequestLoan()
            {
          
            }

            //Placeholders for future implementation

            public void ViewTransaction()
            {
                throw new NotImplementedException();
            }

      
        }

        
        //Tillfälligt Account class för att om det funkar
        // Kanske en bra ide att använda Guid för ID?
        public class Account
        {
        public ID { get; set; }
        public decimal Balance { get; set; }

        public Account()
        {
           ID = id;
           Balance = Balance;

        }
    }
}