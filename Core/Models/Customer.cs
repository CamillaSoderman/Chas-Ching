using Chas_Ching.Core.Enums;
using Chas_Ching.UI.Settings;
using Chas_ChingDemo.UI.Display;
using Spectre.Console;

namespace Chas_Ching.Core.Models
{
    public class Customer : User
    {
        private static Random randomID = new Random();
        public int userRandomId { get; set; }
        public List<Account> Accounts { get; set; }
        public decimal Loan { get; set; }
        public TransactionScheduler TransactionScheduler { get; private set; }

        public Customer(string userEmail, string password) : base(userEmail, password)
        {
            Accounts = new List<Account>();
            Loan = 0; // Initilize loan to 0
            userRandomId = GenerateUserId(); // Generate a random user ID
            TransactionScheduler = new TransactionScheduler();  // Initilize TransactionScheduler class for the customer
        }

        public void OpenAccount()
        {
            CurrencyType selectedCurrency = CurrencyType.SEK;
            decimal initialBalance = 0;
            bool validChoice = false;
            bool validAmount = false;

            while (true)
            {
                Console.Clear();
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]Välj valuta:[/]")
                        // Add Titels for different currencies in a array
                        .AddChoices(new[]
                        {
                    "1. SEK (Svenska kronor)",
                    "2. USD (US Dollar)",
                    "3. EUR (Euro)",
                    "4. Tillbaka"
                        }));

                if (choice == "4. Tillbaka")
                {
                    return;
                }

                switch (choice[0]) // 0 index is a string. Change depending on user input
                {
                    case '1':
                        selectedCurrency = CurrencyType.SEK;
                        validChoice = true;
                        break;

                    case '2':
                        selectedCurrency = CurrencyType.USD;
                        validChoice = true;
                        break;

                    case '3':
                        selectedCurrency = CurrencyType.EUR;
                        validChoice = true;
                        break;

                    default:
                        DisplayService.ShowMessage("Ogiltigt val. Välj 1, 2, 3 eller 4.", "red");
                        continue;
                }

                if (validChoice)
                    break;
            }

            Console.Clear();

            // Ask for initial balance or give the user the option to go back with character 'Q'
            while (!validAmount)
            {
                var amountInput = DisplayService.AskForInput($"Ange initialt saldo i {selectedCurrency} (eller 'Q' för att gå tillbaka)");

                if (amountInput.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                if (decimal.TryParse(amountInput, out initialBalance) && initialBalance >= 0)
                {
                    validAmount = true;
                }
                else
                {
                    DisplayService.ShowMessage("Ogiltigt belopp. Ange ett positivt nummer.", "red");
                }
            }

            Console.Clear();
            Console.WriteLine("Skapar nytt konto...");
            Thread.Sleep(1000);

            // Generate a unique account ID using the existing GenerateUserId() method
            int newAccountId;
            do
            {
                newAccountId = GenerateUserId();
            } while (Accounts.Any(a => a.AccountId == newAccountId));

            // Create a new account and add it to the Accounts list
            var newAccount = new Account(newAccountId, initialBalance, selectedCurrency);
            Accounts.Add(newAccount);

            Console.Clear();
            DisplayService.ShowMessage($"Konto skapat med ID {newAccountId} och saldo {initialBalance} {selectedCurrency}", "green", showContinuePrompt: false);
            AsciiArt.PrintSuccessLogo();
            UIHelper.ShowContinuePrompt();
        }

        // Method to open a new savings account
        public void OpenSavingsAccount()
        {
            CurrencyType selectedCurrency = CurrencyType.SEK; // All savings account are locked to SEK currency
            AccountType accountType = AccountType.SavingsAccount;
            decimal initialBalance = 0;
            decimal interestRate = SavingsAccount.InterestRate;
            
            // Generate unique account ID and add the new account to the Accounts list
            var accountId = GenerateUserId();
            var savingsAccount = new Account(accountId, initialBalance, selectedCurrency, AccountType.SavingsAccount);
            Accounts.Add(savingsAccount);
            
            Console.Clear();
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .Start($"Skapar nytt sparkonto med id {accountId}...", ctx =>
                {
                    Thread.Sleep(2000);
                });
            
            // Confirmation message and continue prompt
            Console.Clear();
            AsciiArt.PrintSuccessLogo();
            AnsiConsole.MarkupLine("[green] Sparkonto skapat! [/]");
            AnsiConsole.MarkupLine($"[yellow]ID:[/][blue] {accountId} [/]");
            AnsiConsole.MarkupLine($"[yellow]Saldo:[/][blue] {initialBalance} {selectedCurrency} [/]");
            AnsiConsole.MarkupLine($"[yellow]Ränta:[/][blue] {interestRate}‰ [/]");
            UIHelper.ShowContinuePrompt();
        }
        
        public int GenerateUserId()
        {   // Method to generate a unique user ID
            int userId = 0;
            do
            {
                userId = randomID.Next(1000, 9999);
            } while (userId == userRandomId);
            return userId;
        }

        public (bool success, Transaction transaction) TransferBetweenOwnAccounts()
        {   // Method to transfer funds between the customer's own accounts. Boolean return type is for showing the success message or not
            // 1. Check if the customer has at least two accounts to transfer between
            if (Accounts.Count < 2)
            {
                DisplayService.ShowMessage("Du behöver minst två konton för att göra överföringar mellan egna konton.", "yellow");
                return (false, null);
            }

            Console.Clear();
            DisplayService.ShowMessage("Tillgängliga konton:", "blue", false);

            // 2. Display all available accounts and their balances
            foreach (var account in Accounts)
            {
                Console.WriteLine($"Konto {account.AccountId}: {account.Balance:F2} {account.Currency}");
            }
            Console.WriteLine();

            // 3. Ask for the source account number and validate it
            var sourceInput = DisplayService.AskForInput("Ange källkontonummer");
            if (!int.TryParse(sourceInput, out int sourceAccountId))
            {
                DisplayService.ShowMessage("Ogiltigt kontonummer.", "red");
                return (false, null);
            }
            
            // 4. Get the transfer amount from the user and validate it
            var amountInput = DisplayService.AskForInput("Ange belopp att överföra");
            if (!decimal.TryParse(amountInput, out decimal transferAmount) || transferAmount <= 0)
            {
                DisplayService.ShowMessage("Ogiltigt belopp. Ange ett positivt nummer.", "red");
                return (false, null);
            }

            // 5. Ask for the destination account number and validate it
            var destInput = DisplayService.AskForInput("Ange mottagarkontonummer");
            if (!int.TryParse(destInput, out int destAccountId))
            {
                DisplayService.ShowMessage("Ogiltigt kontonummer.", "red");
                return (false, null);
            }

            // 6. Retrieve the source and destination accounts based on the provided account IDs
            var fromAccount = Accounts.FirstOrDefault(a => a.AccountId == sourceAccountId);
            var toAccount = Accounts.FirstOrDefault(a => a.AccountId == destAccountId);

            // 7. Check if both accounts exist
            if (fromAccount == null || toAccount == null)
            {
                DisplayService.ShowMessage("Ett eller båda kontona kunde inte hittas.", "red");
                return (false, null);
            }

            // 8. Ensure the source and destination accounts are different
            if (fromAccount.AccountId == toAccount.AccountId)
            {
                DisplayService.ShowMessage("Du kan inte överföra pengar till samma konto.", "red");
                return (false, null);
            }

            // 9. Ensure the source account has enough funds for the transfer
            if (transferAmount > fromAccount.GetBalance())
            {
                DisplayService.ShowMessage("Otillräckligt tillgängligt saldo.", "red");
                return (false, null);
            }

            // 10. Try to reserve funds for the transfer
            if (fromAccount.ReserveFunds(transferAmount))
            {
                var transaction = new Transaction(transferAmount, fromAccount, toAccount);
                TransactionScheduler.EnqueueTransaction(transaction);
                TransactionScheduler.Start();

                // 11. If accounts have different currencies, handle currency conversion
                if (fromAccount.Currency != toAccount.Currency)
                {
                    try
                    {   // Convert the transfer amount to the destination account currency
                        decimal convertedAmount = CurrencyExchange.Convert(transferAmount, fromAccount.Currency, toAccount.Currency);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nKunde inte beräkna konverterat belopp: {ex.Message}");
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Transaktion: {transaction.TransactionId} köad {DateTime.Now}");
                    Console.WriteLine($"\nTransaktionen kommer att genomföras om 15 minuter.");
                    Thread.Sleep(2000);
                }

                return (true, transaction);
            }
            else
            {
                DisplayService.ShowMessage("Kunde inte reservera beloppet för överföring.", "red");
                return (false, null);
            }
        }

        public (bool success, Transaction transaction) TransferFounds()
        {
            // 1. Check if there are accounts available for transfer
            if (Accounts.Count == 0)
            {
                DisplayService.ShowMessage("Du har inga konton att göra överföringar från.", "yellow");
                return (false, null);
            }

            Console.Clear();
            DisplayService.ShowMessage("Tillgängliga konton:", "blue", false);

            // 2. Display all available accounts and their balances
            foreach (var account in Accounts)
            {
                Console.WriteLine($"Konto {account.AccountId}: {account.Balance:F2} {account.Currency}");
            }
            Console.WriteLine();

            // 3. If statments to check if the user input is valid and if the user has enough funds in the source account
            var amountInput = DisplayService.AskForInput("Ange belopp att överföra");
            if (!decimal.TryParse(amountInput, out decimal transferAmount) || transferAmount <= 0)
            {
                DisplayService.ShowMessage("Ogiltigt belopp. Ange ett positivt nummer.", "red");
                return (false, null);
            }

            // Ask for source account number
            var sourceInput = DisplayService.AskForInput("Ange ditt kontonummer");
            if (!int.TryParse(sourceInput, out int sourceAccountId))
            {
                DisplayService.ShowMessage("Ogiltigt kontonummer.", "red");
                return (false, null);
            }

            // Ask for destination account number
            var destInput = DisplayService.AskForInput("Ange mottagarens kontonummer");
            if (!int.TryParse(destInput, out int toAccountId))
            {
                DisplayService.ShowMessage("Ogiltigt kontonummer.", "red");
                return (false, null);
            }

            // Retrieve the source account based on the provided account ID
            var fromAccount = Accounts.FirstOrDefault(a => a.AccountId == sourceAccountId);
            if (fromAccount == null)
            {
                DisplayService.ShowMessage("Källkontot kunde inte hittas.", "red");
                return (false, null);
            }

            // Ensure sufficient funds in the source account
            if (transferAmount > fromAccount.GetBalance())
            {
                DisplayService.ShowMessage("Otillräckligt tillgängligt saldo.", "red");
                return (false, null);
            }

            // Retrieve the destination account based on the provided account ID
            var toAccount = Accounts.FirstOrDefault(a => a.AccountId == toAccountId);
            if (toAccount == null)
            {
                DisplayService.ShowMessage("Mottagarkontot kunde inte hittas.", "red");
                return (false, null);
            }

            // 4. Create the transaction and try to reserve funds
            if (fromAccount.ReserveFunds(transferAmount))
            {
                var transaction = new Transaction(transferAmount, fromAccount, toAccount);
                TransactionScheduler.EnqueueTransaction(transaction);
                TransactionScheduler.Start();

                // Show success message and return the transaction details
                DisplayService.ShowMessage($"Överföring av {transferAmount:F2} {fromAccount.Currency} till konto {toAccount.AccountId} genomförd.", "green");
                return (true, transaction);
            }
            else
            {
                DisplayService.ShowMessage("Kunde inte reservera beloppet för överföring.", "red");
                return (false, null);
            }
        }

        public void RequestLoan()
        {   // Method to loan money from the bank and open a loan account. Boolean return type is for showing the success message or not
            Console.Clear();

            // 1. Check if the user has any accounts to calculate the max loan amount x5
            if (Accounts.Count == 0)
            {
                DisplayService.ShowMessage("Du måste ha minst ett konto för att ansöka om lån.", "yellow");
                return;
            }

            decimal totalBalance = 0;
            Console.WriteLine("Beräknar lånemöjligheter...");
            Thread.Sleep(1000);

            // 2. Sum the total balance of all accounts to calculate the max loan amount
            foreach (var account in Accounts)
            {
                if (account.Balance > 0) // Check if the account balance is greater than 0
                {
                    if (account.Currency != CurrencyType.SEK) // Check if the account currency is not SEK
                    {
                        totalBalance += CurrencyExchange.Convert(account.Balance, account.Currency, CurrencyType.SEK); // Convert the balance to SEK
                    }
                    else
                    {
                        totalBalance += account.Balance;
                    }
                }
            }

            decimal maxLoan = totalBalance * 5; // Maxbelopp = 5x totalt saldo
            Console.Clear();

            // Format the total balance and max loan amount to 2 decimal places. ShowContinuePrompt is set to false
            DisplayService.ShowMessage($"Ditt totala saldo: {totalBalance:F2} SEK", "blue", false);
            DisplayService.ShowMessage($"Du kan låna upp till: {maxLoan:F2} SEK", "blue", false);
            Console.WriteLine();

            var amountInput = DisplayService.AskForInput("Ange önskat lånebelopp i SEK");

            // 3. Check if the user input is a valid decimal number and within the max loan amount
            if (!decimal.TryParse(amountInput, out decimal requestedAmount))  // Kontrollerar att beloppet är giltigt
            {
                DisplayService.ShowMessage("Ogiltigt belopp angivet.", "red");
                return;
            }
            if (requestedAmount <= 0 || requestedAmount > maxLoan)
            {
                DisplayService.ShowMessage($"Ogiltigt lånebelopp.", "red");
                return;
            }

            // 4. Generate a unique account ID for the loan account
            int newAccountId = GenerateUserId();
            while (Accounts.Any(a => a.AccountId == newAccountId))
            {
                newAccountId = GenerateUserId();
            }

            Console.Clear();
            Console.WriteLine("Behandlar låneansökan...");
            Thread.Sleep(1500);

            // 5. Create a new unik loan account.id and add it to the Accounts list. Needed for the enum AccountType.LoanAccount
            var loanAccount = new Account(newAccountId, requestedAmount, CurrencyType.SEK, AccountType.LoanAccount);
            Accounts.Add(loanAccount);
            Loan = requestedAmount;

            Console.Clear();
            DisplayService.ShowMessage($"Lån beviljat: {Loan:F2} SEK", "green", showContinuePrompt: false);
            DisplayService.ShowMessage($"Lånekonto skapat med ID: {newAccountId}", "green", showContinuePrompt: false);
            AsciiArt.PrintSuccessLogo();
            UIHelper.ShowContinuePrompt(); // Show continue prompt
        }

        /* Metoder som inte används idag. ViewTransaction (Styrs med transacrion), ViewAccount(Koppla senare med Admin.CS) och ViewAccountBalance(Sker i CustomerMenu)
        public void ViewTransaction()
        {
            if (Accounts.Count == 0)
            {
                DisplayService.ShowMessage("Inga konton hittades", "yellow");
                return;
            }

            Console.WriteLine("Välj konto att visa transaktioner för:");
            foreach (var account in Accounts)
            {
                Console.WriteLine($"Konto {account.AccountId}");
            }

            if (int.TryParse(Console.ReadLine(), out int accountId))
            {
                var account = Accounts.FirstOrDefault(a => a.AccountId == accountId);
                if (account != null)
                {
                    var transactions = TransactionLog.GetTransactionHistory(account);
                    if (transactions.Any())
                    {
                        foreach (var transaction in transactions)
                        {
                            Console.WriteLine($"Transaktions-ID: {transaction.TransactionId}");
                            Console.WriteLine($"Belopp: {transaction.Amount}");
                            Console.WriteLine($"Datum: {transaction.Date}");
                            Console.WriteLine($"Status: {transaction.Status}");
                            Console.WriteLine("------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Inga transaktioner hittades för detta konto.");
                    }
                }
                else
                {
                    Console.WriteLine("Kontot hittades inte.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt kontonummer.");
            }
        }
        public void ViewAccounts()
        {   //Method displays all accounts and their balances
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
        public void ViewAccountBalance()
        {   // Method to view the balance of a specific account
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
         */
    }
}