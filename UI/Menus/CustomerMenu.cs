using Chas_Ching.Core.Enums;
using Chas_Ching.Core.Models;
using Chas_Ching.UI.Settings;
using Chas_ChingDemo.UI.Display;
using Spectre.Console;

/// <summary>
/// Handles the customer menu interface and operations
/// Sense customer has now login from Main menu, we can use the current customer object to perform operations
/// Current customer object is passed to the constructor of this class
/// Current customer is login as long as the main menu is open
/// </summary>
public class CustomerMenu
{
    private readonly Customer _currentCustomer; // Field to store the current customer

    public CustomerMenu(Customer customer)
    {   // New constructor that takes a Customer object as a parameter
        _currentCustomer = customer;
    }

    public void Start()
    {
        while (true)
        {   // Customer menu loop, set title and display menu choices by calling ShowMenu method from DisplayService
            var choice = DisplayService.ShowMenu("Kundmeny", MenuText.GetCustomerMenuChoices());

            switch (choice)
            {
                case MenuChoice.ShowAccount:
                    ShowAccountDetails();
                    break;

                case MenuChoice.OpenNewAccount:
                    HandleOpenNewAccount();
                    break;

                case MenuChoice.OpenSavingsAccount:
                    HandleOpenSavingsAccount();
                    break;

                case MenuChoice.MakeDeposit:
                    HandleMakeDeposit();
                    break;

                case MenuChoice.MakeTransaction:
                    HandleTransactionMenu();
                    break;

                case MenuChoice.ApplyForLoan:
                    HandleLoanApplication();
                    break;

                case MenuChoice.ExchangeCurrency:
                    HandleCurrencyExchange();
                    break;

                case MenuChoice.BackToMainCustomer:
                    return;
            }
        }
    }

    private void ShowAccountDetails(bool showPrompt = true)
    {   // Responsible for displaying the account details of the current customer with optional prompt parameter
        Console.Clear();
        DisplayService.ShowHeader("Kontoöversikt");

        if (_currentCustomer.Accounts.Count == 0)
        {   // If account list is empty, display a error message
            DisplayService.ShowMessage("Du har inga öppna konton hos banken", "yellow");
            return;
        }

        // Create a new table with columns for Account Number, Balance, Currency and Account Type
        var table = new Table()
        .AddColumn(new TableColumn("Konto.nr").Centered())
        .AddColumn(new TableColumn("Totalt Saldo").RightAligned())
        .AddColumn(new TableColumn("Reserverat").RightAligned())
        .AddColumn(new TableColumn("Tillgängligt").RightAligned())
        .AddColumn(new TableColumn("Valuta").Centered())
        .AddColumn(new TableColumn("Ränta").Centered())
        .AddColumn(new TableColumn("Konto Typ").Centered());

        // Loop through all accounts of the current customer and add them to the table
        foreach (var account in _currentCustomer.Accounts)
        {
            // Check which type the account is and set the accountType variable accordingly
            string accountType;

            if (account.Type == AccountType.SavingsAccount)
            {
                accountType = "[yellow]Sparkonto[/]";
            }
            else if (account.Type == AccountType.LoanAccount)
            {
                accountType = "[blue]Lånkonto[/]";
            }
            else
            {
                accountType = "[green]Bankkonto[/]";
            }

            // Make PendingAmount in yellow
            string pendingAmountText = account.PendingAmount > 0
                ? $"[yellow]{account.PendingAmount:F2}[/]"
                : account.PendingAmount.ToString("F2");

            // Add a row to the table with the account details
            table.AddRow(
            account.AccountId.ToString(),
            account.Balance.ToString("F2"),
            account.PendingAmount.ToString("F2"),
            account.GetBalance().ToString("F2"),
            account.Currency.ToString(),
            account.InterestRate.ToString("F2"),
            accountType);
        }

        AnsiConsole.Write(table); // Display the table with columns and rows (account details)

        if (showPrompt)
        {
            UIHelper.ShowContinuePrompt(); // Show a continue prompt after the table
        }
    }

    private void HandleTransactionMenu()
    {   // Responsible for handling the transaction menu
        Console.Clear();
        DisplayService.ShowHeader("Transaktioner");

        if (_currentCustomer.Accounts.Count == 0)
        {
            DisplayService.ShowMessage("Du behöver ett bankkonton för att göra en transakion", "yellow");
            return;
        }

        ShowAccountDetails(false); // Display account details without prompt

        // Prompt user to select a transaction type Ex. "Transfer between own accounts"
        var transactionType = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[blue]Välj en av tre alternativ:[/]")
                .AddChoices(new[] {
                    "Överför mellan egna konton",
                    "Överför till ett annat konto",
                    "Visa kommande överföringar",
                    "Visa transaktionshistorik",
                    "Tillbaka"
                }));

        // When transaction type is selected, call the appropriate method based on the selection
        switch (transactionType)
        {
            case "Överför mellan egna konton":
                // Call the TransferBetweenOwnAccounts method in Customer class and store the result
                var (success, transaction) = _currentCustomer.TransferBetweenOwnAccounts();

                // If the transaction was successful and the transaction object is not null, display a success message
                if (success && transaction != null)
                {
                    ShowTransactionCreatedInfo(transaction);
                }
                break;

            case "Överför till ett annat konto":

                // Call the TransferFounds method in Customer class and store the result
                var (extSuccess, extTransaction) = _currentCustomer.TransferFounds();

                // If the transaction was successful and the transaction object is not null, display a success message
                if (extSuccess && extTransaction != null)
                {
                    ShowTransactionCreatedInfo(extTransaction);
                }
                break;

            case "Visa kommande överföringar":
                ShowPendingTransactions();
                break;

            case "Visa transaktionshistorik":
                ShowHistoryTransactions();
                break;
        }
    }

    private void ShowTransactionCreatedInfo(Transaction transaction)
    {
        Console.Clear();
        DisplayService.ShowHeader("En ny transaktion är skapad");
        AnsiConsole.MarkupLine($"Transaktion-ID: [yellow]{transaction.TransactionId}[/] Köad:[green]{transaction.Date:yyyy-MM-dd}[/] kl:[green]{transaction.Date:HH:mm:ss}[/]");
        AnsiConsole.WriteLine($"\nTransaktionen kommer att genomföras om {TransactionScheduler.TransactionDelayMinutes} minuter.");
        UIHelper.ShowContinuePrompt();
        ShowPendingTransactions();
    }

    private void ShowHistoryTransactions()
    {
        Console.Clear();
        DisplayService.ShowHeader("Historik över transaktioner");

        // Get the transaction history of the current customer and store it in a variable from the TransactionLog class
        var transactions = TransactionLog.GetTransactionHistory(_currentCustomer.Accounts.FirstOrDefault());

        // Filter the transactions to only show the pending transactions
        var completedTransactions = transactions.Where(t => t.Status == TransactionStatus.Completed).ToList();

        // If there are no transactions made, display a message and return
        if (!completedTransactions.Any())
        {
            DisplayService.ShowMessage("Här var det tomt!", "yellow", true);
            return;
        }

        // Create a new table with columns for Transaction ID, From Account, To Account, Amount, Status and Expected Time
        var table = new Table()
            .AddColumn(new TableColumn("Transaktions ID").Centered())
            .AddColumn(new TableColumn("Från konto").Centered())
            .AddColumn(new TableColumn("Till konto").Centered())
            .AddColumn(new TableColumn("Belopp").RightAligned())
            .AddColumn(new TableColumn("Status").Centered())
            .AddColumn(new TableColumn("Transaktionsdatum").Centered());

        // Loop through all transactions and add them to the table
        foreach (var transaction in completedTransactions)
        {
            var transactionProcessedTime = transaction.Date;

            table.AddRow(
                transaction.TransactionId.ToString(),
                transaction.FromAccount.AccountId.ToString(),
                transaction.ToAccount.AccountId.ToString(),
                transaction.Amount.ToString("F2"),
                "[green]Utförd[/]",
                transactionProcessedTime.ToString("yyyy-MM-dd HH:mm:ss")
            );
        }

        AnsiConsole.Write(table);
        UIHelper.ShowContinuePrompt();
    }

    private void ShowPendingTransactions()
    {
        // Responsible for displaying the pending transactions of the current customer
        Console.Clear();
        DisplayService.ShowHeader("Kommande transaktioner");

        // Get the transaction history of the current customer and store it in a variable from the TransactionLog class
        var transactions = TransactionLog.GetTransactionHistory(_currentCustomer.Accounts.FirstOrDefault());
        // Filter the transactions to only show the pending transactions
        var pendingTransactions = transactions.Where(t => t.Status == TransactionStatus.Pending).ToList();

        // If there are no pending transactions, display a message and return
        if (!pendingTransactions.Any())
        {
            DisplayService.ShowMessage("Här var det tomt!", "yellow", true);
            return;
        }

        // Create a new table with columns for Transaction ID, From Account, To Account, Amount, Status and Expected Time
        var table = new Table()
            .AddColumn(new TableColumn("Transaktions ID").Centered())
            .AddColumn(new TableColumn("Från konto").Centered())
            .AddColumn(new TableColumn("Till konto").Centered())
            .AddColumn(new TableColumn("Belopp").RightAligned())
            .AddColumn(new TableColumn("Status").Centered())
            .AddColumn(new TableColumn("Förväntad transaktionkörning").Centered());

        // Loop through all pending transactions and add them to the table
        foreach (var transaction in pendingTransactions)
        {
            var expectedTime = TransactionScheduler.GetExpectedCompletionTime(transaction.Date); // Add {GetExpectedCompletionTime} minutes to the transaction time

            table.AddRow(
                transaction.TransactionId.ToString(),
                transaction.FromAccount.AccountId.ToString(),
                transaction.ToAccount.AccountId.ToString(),
                transaction.Amount.ToString("F2"),
                "[yellow]Pågående[/]",
                expectedTime.ToString("yyyy-MM-dd HH:mm:ss")
            );
        }

        AnsiConsole.Write(table);
        UIHelper.ShowContinuePrompt();
    }

    private void HandleOpenNewAccount()
    {   // Responsible for handling the account creation
        if (_currentCustomer == null)
        {
            DisplayService.ShowMessage("Ingen användare är inloggad", "red");
            return;
        }

        // Call the OpenAccount method in Customer class
        _currentCustomer.OpenAccount();
    }

    private void HandleOpenSavingsAccount()
    {   // Responsible for handling the savings account creation
        if (_currentCustomer == null)
        {
            DisplayService.ShowMessage("Ingen användare är inloggad", "red");
            return;
        }

        // Call the OpenSavingsAccount method in Customer class
        _currentCustomer.OpenSavingsAccount();
    }

    private void HandleMakeDeposit()
    {   // Responsible for handling the deposit
        if (_currentCustomer.Accounts.Count == 0)
        {
            DisplayService.ShowMessage("Du har inga öppna konton hos banken", "yellow");
            return;
        }

        Console.Clear();
        DisplayService.ShowHeader("Insättning");
        ShowAccountDetails(false);
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[blue]Välj alternativ:[/]")
                .AddChoices(new[] { "Välj Konto", "Tillbaka" }));

        switch (choice)
        {
            case "Välj Konto":
                _currentCustomer.DepositToAccount();
                break;

            case "Tillbaka":
                return;
        }
    }

    private void HandleLoanApplication()
    {   // Responsible for handling the loan application
        if (_currentCustomer == null)
        {
            DisplayService.ShowMessage("Ingen användare är inloggad", "red");
            return;
        }

        // Call the RequestLoan method in Customer class
        _currentCustomer.RequestLoan();
    }

    private void HandleCurrencyExchange()
    {   // Responsible for handling the currency exchange
        Console.Clear();
        DisplayService.ShowHeader("Valutaväxling");

        // Check if the current customer is null or has no accounts
        if (_currentCustomer == null || _currentCustomer.Accounts.Count == 0)
        {
            DisplayService.ShowMessage("Du behöver ett konto hos banken för att växla pengar", "yellow");
            return;
        }

        // Display the current account details of the current customer
        ShowAccountDetails(false); // Display account details without prompt

        // fromAccount calls a method that prompts the user to select an account to exchange from. Stor the result in variable fromAccount
        var fromAccount = SelectAccountWithBack("\n Vilket konto vill du växla ifrån:");

        // If fromAccount is null, return
        if (fromAccount == null)
        {
            return;
        }

        // targetCurrency calls a method that prompts the user to select a currency to exchange to. Store the result in variable targetCurrency
        var targetCurrency = SelectCurrencyWithBack("Välj valuta att växla till");

        // If targetCurrency is null, return
        if (!targetCurrency.HasValue)
        {
            return;
        }

        // Check if the target currency is the same as the from account currency
        if (targetCurrency.Value == fromAccount.Currency)
        {
            DisplayService.ShowMessage($"Det går inte att växla {targetCurrency} till {targetCurrency}", "yellow");
            return;
        }

        // amount is set
        decimal amount = GetExchangeAmountWithBackOption(fromAccount.Currency);

        if (amount == -1)
        {
            return;
        }
        if (amount == 0)
        {
            return;
        }

        // Find the account with the target currency to avoid creating unnecessary accounts
        Account toAccount = _currentCustomer.Accounts.FirstOrDefault(a => a.Currency == targetCurrency.Value);

        // If the account with the target currency is null, create a new account with the target currency
        if (toAccount == null)
        {
            // Generate a new account ID and create a new account with the target currency
            int newAccountId = _currentCustomer.GenerateUserId();
            toAccount = new Account(newAccountId, 0, targetCurrency.Value);
            _currentCustomer.Accounts.Add(toAccount);
        }

        // If the amount is valid, call the ProcessExchange method with the fromAccount, toAccount and amount as parameters
        // And perform the currency exchange
        ProcessExchange(fromAccount, toAccount, amount);
    }

    private Account SelectAccountWithBack(string prompt)
    {    // Presents a list of accounts, each with its ID, currency, and balance, along with an option to go back (denoted by "Tillbaka").
        while (true)
        {
            // Display a selection prompt to the user with the list of accounts and the "Tillbaka/back" option
            var accountChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[blue]{prompt}[/]")
                    .AddChoices(_currentCustomer.Accounts.Select(a =>
                        $"Konto {a.AccountId} ({a.Currency}): {a.Balance:F2}")
                        .Concat(new[] { "Tillbaka" })));

            // If the user selects the "Q" option (to go back), return null. Ignore case when comparing the selection
            if (accountChoice == "Tillbaka")
            {
                return null;
            }

            // Extract the account ID from the selected string
            if (int.TryParse(accountChoice.Split(' ')[1], out int id))
            {
                // Return the account with the matching ID
                return _currentCustomer.Accounts.FirstOrDefault(a => a.AccountId == id);
            }
            else
            {
                DisplayService.ShowMessage("Ogiltigt kontonummer.", "red");
            }
        }
    }

    private CurrencyType? SelectCurrencyWithBack(string prompt)
    {   // Method to prompt the user to select a currency, allowing them to go back if needed
        var choices = Enum.GetNames<CurrencyType>().ToList();
        choices.Add("Tillbaka");

        // Display a selection prompt to the user with the list of currencies and the "Tillbaka/back" option
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[blue]{prompt}[/]")
                .AddChoices(choices));

        // Return null if the user selects the "Tillbaka/back" option
        if (selection == "Tillbaka")
        {
            return null;
        }
        // If a valid currency is selected, convert the selected string into the corresponding CurrencyType enum value.
        if (Enum.TryParse<CurrencyType>(selection, out CurrencyType selectedCurrency))
        {
            return selectedCurrency;
        }
        // If the selected currency is invalid, display an error message and return null
        else
        {
            DisplayService.ShowMessage("Ogiltig valuta.", "red");
            return null;
        }
    }

    private decimal GetExchangeAmountWithBackOption(CurrencyType currency)
    {   // Prompts the user to enter an amount. Take in currency as a argument for the prompt. Display the amount in the specified currency
        while (true)
        {   // Prompts the user to enter an amount to exchange, allowing them to go back by entering 'Q' instead of a number
            string amountStr = DisplayService.AskForInput($"Hur mycket vill du växla? {currency} (eller skriv 'Q' för att gå tillbaka)");

            // If the user entered 'Q', return -1 to indicate they want to go back
            if (amountStr.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                return -1;
            }

            // If the input is a valid positive decimal, return the amount
            if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
            {
                return amount;
            }

            DisplayService.ShowMessage($"Summan måste vara positiv. Din inmatning: {amount}", "red");
        }
    }

    private void ProcessExchange(Account fromAccount, Account toAccount, decimal amount)
    {   // Responsible for processing the currency exchange between two accounts
        // The amount cannot user wish to exchange cannot be greater than the balance of the fromAccount
        if (amount > fromAccount.Balance)
        {
            DisplayService.ShowMessage("Otillräckligt saldo", "red");
            return;
        }

        try
        {
            Console.Clear();
            DisplayService.ShowHeader("Valutaväxling pågår");

            AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .Start("Bearbetar valutaväxling...", ctx =>
                {
                    // Simulate processing time
                    Thread.Sleep(2000);

                    decimal convertedAmount = CurrencyExchange.Convert(amount, fromAccount.Currency, toAccount.Currency);
                    fromAccount.Balance -= amount;
                    toAccount.Balance += convertedAmount;
                });

            Console.Clear();
            DisplayService.ShowHeader("Valutaväxling");

            // Success message with exchange details
            AnsiConsole.MarkupLine($"[green]Växling genomförd![/]");
            AnsiConsole.MarkupLine($"\n[blue]Detaljer:[/]");
            AnsiConsole.MarkupLine($"Från: {amount:F2} {fromAccount.Currency}");
            AnsiConsole.MarkupLine($"Till: {CurrencyExchange.Convert(amount, fromAccount.Currency, toAccount.Currency):F2} {toAccount.Currency}");
            Console.WriteLine();

            // Show updated account overview
            ShowAccountDetails(false);
            AsciiArt.PrintSuccessLogo();
            UIHelper.ShowContinuePrompt();

            // Return to currency exchange menu by calling HandleCurrencyExchange again
            HandleCurrencyExchange();
        }
        catch (Exception ex)
        {
            DisplayService.ShowMessage($"Valutaväxling misslyckades med meddelande: {ex.Message}", "red");
        }
    }
}