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
        // Instance of Random to generate random IDs

        static Random randomID = new Random();

        public int userRandomId { get; set; }

        // List to store accounts associated with the customer
        public List<Account> Accounts { get; set; }

        // Property to store the loan amount for the customer
        public decimal Loan { get; set; }

        // Constructor to initialize Customer object with email and password
        public Customer(string userEmail, string password) : base(userEmail, password)
        {
            // Initialize Accounts-list
            Accounts = new List<Account>();

            Loan = Loan;
            // Generate a unique ID for the customer
            userRandomId = GenerateUserId();
        }


        // Prompts the user to enter an initial balance and creates a new Account
        public void OpenAccount()
        {
            Console.WriteLine("\nVälj valuta:");
            Console.WriteLine("1. SEK");
            Console.WriteLine("2. USD");
            Console.WriteLine("3. EUR");

            CurrencyType selectedCurrency = CurrencyType.SEK; // Default till SEK
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    selectedCurrency = CurrencyType.SEK;
                    break;
                case "2":
                    selectedCurrency = CurrencyType.USD;
                    break;
                case "3":
                    selectedCurrency = CurrencyType.EUR;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val. SEK väljs som standard.");
                    break;
            }
            // Prompt user to enter initial balance and validate input
            Console.WriteLine("Ange initialt saldo: ");
            decimal initialBalance;
            while (!decimal.TryParse(Console.ReadLine(), out initialBalance) || initialBalance < 0)
            {
                Console.WriteLine("Ogiltigt belopp. Ange ett positivt nummer: ");
            }

            // Generate a unique account ID using the existing GenerateUserId() method
            int newAccountId;
            do
            {
                newAccountId = GenerateUserId();
            } while (Accounts.Any(a => a.AccountId == newAccountId));


            // Create a new account and add it to the Accounts list
            var newAccount = new Account(newAccountId, initialBalance, selectedCurrency);
            Accounts.Add(newAccount);
            Console.WriteLine($"Konto skapat med ID {newAccount.AccountId} och saldo {newAccount.Balance} {selectedCurrency}");
        }


        // Method to generate a unique user ID
        public int GenerateUserId()
        {
            int userId = 0;
            do
            {
                userId = randomID.Next(1000, 9999);

            } while (userId == userRandomId);
            return userId;
        }


        //Method displays all accounts and their balances
        public void ViewAccounts()
        {

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
        
        // Method to view the balance of a specific account
        public void ViewAccountBalance()
        {
            // Prompt user to enter the account ID and validate input
            Console.Write("Enter account ID to view balance: ");
            if (int.TryParse(Console.ReadLine(), out int accountId))
            {
                var account = Accounts.FirstOrDefault(a => a.AccountId == accountId);
                if (account != null)
                {
                    Console.WriteLine($"Account ID: {account.AccountId}, Balance: {account.Balance} {account.Currency}");
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid account ID.");
            }
        }


        // Method to transfer funds between the customer's own accounts
        public void TransferBetweenOwnAccounts()
        {
            // Prompt user to enter the amount to transfer and validate input
            Console.Write("Enter amount to transfer: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount entered. Please try again.");
                return;
            }
            // Prompt user to enter source and destination account IDs and validate input
            Console.Write("Enter source account ID: ");
            if (!int.TryParse(Console.ReadLine(), out int fromAccountId))
            {
                Console.WriteLine("Invalid source account ID. Please try again.");
                return;
            }

            Console.Write("Enter destination account ID: ");
            if (!int.TryParse(Console.ReadLine(), out int toAccountId))
            {
                Console.WriteLine("Invalid destination account ID. Please try again.");
                return;
            }

            var fromAccount = Accounts.FirstOrDefault(a => a.AccountId == fromAccountId);
            var toAccount = Accounts.FirstOrDefault(a => a.AccountId == toAccountId);

            if (fromAccount == null || toAccount == null)
            {
                Console.WriteLine("One or both accounts not found.");
                return;
            }
            // Attempt to transfer funds between the accounts
            if (fromAccount.TransferOwnAccounts(amount, toAccount))
            {
                Console.WriteLine($"Transfer successful: {amount} {fromAccount.Currency} from Account ID {fromAccount.AccountId} to Account ID {toAccount.AccountId}.");
            }
            else
            {
                Console.WriteLine("Transfer failed due to insufficient funds.");
            }
        }



        //Placeholders for future implementation
        public void RequestLoan()
        {
            decimal totalBalance = 0;
            foreach (var account in Accounts)   // Summerar totalt saldo på alla konton för att räkna ut maxbelopp för lån 
            {
                totalBalance += account.Balance; // Summerar valuta men konventerar inte valuta??? 
            }

            decimal maxLoan = totalBalance * 5; // Maxbelopp = 5x totalt saldo

            Console.WriteLine($"Ditt totala saldo: {totalBalance}");
            Console.WriteLine($"Du kan låna upp till: {maxLoan} SEK");

            Console.WriteLine("Ange önskat lånebelopp: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal requestedAmount))  // Kontrollerar att beloppet är giltigt
            {
                Console.WriteLine("Ogiltigt belopp angivet.");
                return;
            }

            if (requestedAmount <= 0 || requestedAmount > maxLoan)
            {
                Console.WriteLine("Ogiltigt lånebelopp.");
                return;
            }

            Loan = requestedAmount;         // Sätter lånebeloppet efter godkänd ansökan/kontroll i ovanstående if-satser
            Console.WriteLine($"Lån beviljat: {Loan} SEK");     // Skriver ut beviljat lånebelopp
        }

        //Placeholders for future implementation

        public void ViewTransaction()
        {
            throw new NotImplementedException();


        }
    }
}