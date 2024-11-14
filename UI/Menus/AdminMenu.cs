using Chas_Ching.Core.Enums;
using Chas_Ching.Core.Models;
using Chas_Ching.UI.Settings;
using Chas_ChingDemo.UI.Display;
using Spectre.Console;

/// <summary>
/// Handles administrative functions of the banking application
/// </summary>
public class AdminMenu
{
    private Customer _customer;
    private int _customerID;
    private bool isFirstRow = true;

    public AdminMenu(Admin admin)
    {
    }

    public void Start()
    {
        while (true)
        {
            var choice = DisplayService.ShowMenu("Admin Menu", MenuText.GetAdminMenuChoices());

            switch (choice)
            {
                case MenuChoice.CreateNewCustomer:
                    CreateNewCustomer();
                    break;

                case MenuChoice.ShowAllCustomers:
                    ShowAllCustomers();
                    break;

                case MenuChoice.LockUser:
                    HandleLockUser();
                    break;

                case MenuChoice.UnlockUser:
                    HandleUnlockUser();
                    break;

                case MenuChoice.BackToMainAdmin:
                    return;
            }
        }
    }

    private void CreateNewCustomer()
    {
        Console.Clear();
        DisplayService.ShowHeader("Create new Customer");

        var userName = DisplayService.AskForInput("Enter user Name");
        var userPassword = DisplayService.AskForInput("Enter user Password");
        Admin.CreateUserCustomer(userName, userPassword);
        //var userPassword = DisplayService.AskForInput("Enter user Password");
    }

    private void ShowAllCustomers(bool showPrompt = true)
    {
        Console.Clear();
        DisplayService.ShowHeader("Visa alla kundkonton (Bankkonto/Sparkonto/Lånekonto)");

        var table = new Table()
        .AddColumn(new TableColumn("Låst/Upplåst").Centered())
        .AddColumn(new TableColumn("Kund namn").RightAligned())
        .AddColumn(new TableColumn("Kund.nr").RightAligned())
        .AddColumn(new TableColumn("Konto.nr").Centered())
        .AddColumn(new TableColumn("Totalt Saldo").RightAligned())
        .AddColumn(new TableColumn("Reserverat").RightAligned())
        .AddColumn(new TableColumn("Tillgängligt").RightAligned())
        .AddColumn(new TableColumn("Valuta").Centered())
        .AddColumn(new TableColumn("Konto Typ").Centered());

        table.BorderColor(Color.Blue);

        // Loop genom lista registeredUsers som en customer för att sen kolla konton hos denna customer
        foreach (var user in UserManagement.registeredUsers)
        {
            if (user is Customer customer)
            {
                _customer = customer;
                _customerID = customer.userRandomId;

                if (_customer.Accounts.Count == 0)
                {   // If account list is empty, display a error message
                    DisplayService.ShowMessage("Det finns inga öppna konton i banken", "yellow", true);
                    continue; // Skip customers without any accounts
                }

                // Set color depending on lock status
                string lockStatus = _customer.IsLocked ? "[red]Låst[/]" : "[green]Upplåst[/]";

                foreach (var account in _customer.Accounts)
                {
                    //  Check if account is a Loan Account or Bank Account and set the account type
                    string accountType = account.Type switch
                    {
                        AccountType.LoanAccount => "[blue]Lånkonto[/]",
                        AccountType.SavingsAccount => "[yellow]Sparkonto[/]",
                        _ => "[green]Bankkonto[/]"
                    };

                    // Make PendingAmount in yellow
                    string pendingAmountText = account.PendingAmount > 0
                        ? $"[yellow]{account.PendingAmount:F2}[/]"
                        : account.PendingAmount.ToString("F2");

                    table.AddRow(
                            lockStatus,
                            customer.UserName.ToString(),
                            customer.userRandomId.ToString(),
                            account.AccountId.ToString(),
                            account.Balance.ToString("F2"),
                            account.PendingAmount.ToString("F2"),
                            account.GetBalance().ToString("F2"),
                            account.Currency.ToString(),
                            accountType);
                }
            }
        }

        AnsiConsole.Write(table);

        if (showPrompt)
        {
            UIHelper.ShowContinuePrompt(); // Show a continue prompt after the table
        }
    }

    private void HandleLockUser()
    {
        HandleUserLockStatus("Lock", "Låser konto...");
    }

    private void HandleUnlockUser()
    {
        HandleUserLockStatus("Unlock", "Låser upp konto...");
    }

    private void HandleUserLockStatus(string action, string progressMessage)
    {
        string userId = DisplayService.AskForInput("Skriv in användarnamn:");
        User user = UserManagement.FindUser(userId);

        if (user == null)
        {
            DisplayService.ShowMessage("Användaren finns inte", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
            UIHelper.ShowContinuePrompt();
            return;
        }

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start(progressMessage, ctx =>
            {
                Thread.Sleep(1000);

                if (action == "Lock" && user.IsLocked)
                {
                    DisplayService.ShowMessage($"Användare {user.UserName} är redan låst", "yellow", showContinuePrompt: false);
                    AsciiArt.PrintErrorLogo();
                }
                else if (action == "Unlock" && !user.IsLocked)
                {
                    DisplayService.ShowMessage($"Användare {user.UserName} är redan aktiv.", "yellow", showContinuePrompt: false);
                    AsciiArt.PrintErrorLogo();
                }
                else
                {
                    if (action == "Lock")
                    {
                        user.IsLocked = true;
                        DisplayService.ShowMessage($"Användare {user.UserName} är spärrad.", "green", showContinuePrompt: false);
                        AsciiArt.PrintSuccessLogo();
                    }
                    else if (action == "Unlock")
                    {
                        user.IsLocked = false;
                        user.ResetLoginAttempts(); // Reset login attempts
                        DisplayService.ShowMessage($"Användare {user.UserName} är aktiv", "green", showContinuePrompt: false);
                        AsciiArt.PrintSuccessLogo();
                    }
                }
            });

        UIHelper.ShowContinuePrompt();
    }
}