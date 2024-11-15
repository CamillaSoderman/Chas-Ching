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

    public AdminMenu(Admin admin)
    {
    }

    public void Start()
    {
        while (true)
        {
            var choice = DisplayService.ShowMenu("Adminmeny", MenuText.GetAdminMenuChoices());

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
        DisplayService.ShowHeader("Skapa ny kund");

        var userName = DisplayService.AskForInput("Mata in användarnamn");
        var userPassword = DisplayService.AskForInput("Mata in lösenord");
        Admin.CreateUserCustomer(userName, userPassword);
        //var userPassword = DisplayService.AskForInput("Enter user Password");
       


    }

    private void ShowAllCustomers(bool showPrompt = true)
    {
        Console.Clear();

        DisplayService.ShowHeader("Visa alla kunder");

      


        var table = new Table()
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
                    DisplayService.ShowMessage("Det finns inga öppna konton i banken", "yellow");
                    return;
                }

                foreach (var account in _customer.Accounts)
                {

                    //  Check if account is a Loan Account or Bank Account and set the account type
                    string accountType = account.Type == AccountType.LoanAccount ?
                            "[blue]Lånkonto[/]" :
                            "[green]Bankkonto[/]";

                    // Make PendingAmount in yellow
                    string pendingAmountText = account.PendingAmount > 0
                        ? $"[yellow]{account.PendingAmount:F2}[/]"
                        : account.PendingAmount.ToString("F2");


                    table.AddRow(
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
        UIHelper.ShowContinuePrompt();

        if (showPrompt)
        {
            UIHelper.ShowContinuePrompt(); // Show a continue prompt after the table
        }
    
    }

    private void HandleLockUser()
    {
        HandleUserLockStatus("Låst", "Låser konto...");
    }

    private void HandleUnlockUser()
    {
        HandleUserLockStatus("Upplåst", "Låser upp konto...");
    }

    private void HandleUserLockStatus(string action, string progressMessage)
    {
        var userId = DisplayService.AskForInput("Ange kund ID");
        bool isSuccess = true; // For demo purposes

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start(progressMessage, ctx =>
            {
                Thread.Sleep(1000);
            });

        Console.Clear();
        if (isSuccess)
        {
            DisplayService.ShowMessage($"Kontot är nu {action.ToLower()}!", "green", showContinuePrompt: false);
            AsciiArt.PrintSuccessLogo();
        }
        else
        {
            DisplayService.ShowMessage($"Kontots försök att bli {action.ToLower()} misslyckades !", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
        }
        UIHelper.ShowContinuePrompt();
    }
}