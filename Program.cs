using Chas_Ching.Core.Enums;
using Chas_Ching.Core.Models;

namespace Chas_Ching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string email = Console.ReadLine();
            //string password = Console.ReadLine();

            //Customer customer = new Customer(email, password);
            //customer.OpenAccount(); 
            // Create accounts with initial balances
            // Create accounts with CurrencyType
            // Create accounts with CurrencyType
            // Create accounts with CurrencyType
            // Create accounts with CurrencyType

            //TRANSACTION TEST//
        //    Account account1 = new Account(1, 1000m, CurrencyType.USD);
        //    Account account2 = new Account(2, 500m, CurrencyType.USD);
        //    Account account3 = new Account(3, 200m, CurrencyType.USD);
        //    Account account4 = new Account(4, 50m, CurrencyType.USD); // New account with low balance

        //    // Display initial balances
        //    Console.WriteLine("Initial Balances:");
        //    Console.WriteLine($"Account {account1.AccountId}: {account1.Balance} {account1.Currency}");
        //    Console.WriteLine($"Account {account2.AccountId}: {account2.Balance} {account2.Currency}");
        //    Console.WriteLine($"Account {account3.AccountId}: {account3.Balance} {account3.Currency}");
        //    Console.WriteLine($"Account {account4.AccountId}: {account4.Balance} {account4.Currency}");
        //    Console.WriteLine();

        //    // Create transactions with GUID transaction IDs
        //    Transaction transaction1 = new Transaction(
        //        Guid.NewGuid(), CurrencyType.USD, 300m, account1, account2);

        //    Transaction transaction2 = new Transaction(
        //        Guid.NewGuid(), CurrencyType.USD, 150m, account2, account3);

        //    Transaction transaction3 = new Transaction(
        //        Guid.NewGuid(), CurrencyType.USD, 100m, account3, account1);

        //    Transaction transaction4 = new Transaction(
        //        Guid.NewGuid(), CurrencyType.USD, 200m, account4, account1); // This should fail

        //    Transaction transaction5 = new Transaction(
        //        Guid.NewGuid(), CurrencyType.USD, 50m, account2, account4);

        //    Transaction transaction6 = new Transaction(
        //        Guid.NewGuid(), CurrencyType.USD, 25m, account1, account3);

        //    // Create TransactionScheduler instance
        //    TransactionScheduler scheduler = new TransactionScheduler();

        //    // Enqueue transactions
        //    scheduler.EnqueueTransaction(transaction1);
        //    scheduler.EnqueueTransaction(transaction2);
        //    scheduler.EnqueueTransaction(transaction3);
        //    scheduler.EnqueueTransaction(transaction4); // Failing transaction
        //    scheduler.EnqueueTransaction(transaction5);
        //    scheduler.EnqueueTransaction(transaction6);

        //    // Start the scheduler
        //    scheduler.Start();

        //    // Wait for transactions to process
        //    Console.WriteLine("Press Enter to stop the scheduler and exit.");
        //    Console.ReadLine();

        //    // Stop the scheduler
        //    scheduler.Stop();

        //    // Display final balances
        //    Console.WriteLine("\nFinal Balances:");
        //    Console.WriteLine($"Account {account1.AccountId}: {account1.Balance} {account1.Currency}");
        //    Console.WriteLine($"Account {account2.AccountId}: {account2.Balance} {account2.Currency}");
        //    Console.WriteLine($"Account {account3.AccountId}: {account3.Balance} {account3.Currency}");
        //    Console.WriteLine($"Account {account4.AccountId}: {account4.Balance} {account4.Currency}");
        }
    
    
    }
}

/*    För att köra UI i consolen
 *    Skapa en ny instanse av MainMenu.cs och kalla på Start-metoden
 *    
 *    var mainMenu = new MainMenu();
 *    mainMenu.Start();
 *    
 */

// Ladda ner i Package Manager Console (Nugget)
//  Tools
//    >NuGet
//     >Packet Manager
//       >Öppna Package Manager Console

//   Skriv in följande kommandon;
// > dotnet add package Spectre.Console
// > dotnet add package Spectre.Console.Cli

// Referera i varje klass som använder Spectre.Console
// using Spectre.Console;

/* Referenser
 * https://mdbouk.com/how-to-create-beautiful-console-applications-with-spectre-console/
 * https://spectreconsole.net/prompts/text
 *
 */

/*
BankApp
├── Program.cs
├── UI
│   ├── Menus
│   │   ├── MainMenu.cs
│   │   ├── CustomerMenu.cs
│   │   ├── AdminMenu.cs
│   │   └── BaseMenu.cs
│   ├── Services
│   │   ├── MenuService.cs
│   │   └── DisplayService.cs
│   ├── Display
│   │   └── AsciiArt.cs
│   └── Settings
│       ├── AppSettings.cs
│       ├── MenuOptions.cs
│       └── UIHelper.cs
*/