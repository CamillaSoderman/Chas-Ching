using Chas_Ching.Core.Models;
using Chas_Ching.UI.Settings;
using Chas_ChingDemo.UI.Display;
using Spectre.Console;

/// <summary>
/// Handles administrative functions of the banking application
/// </summary>
public class AdminMenu
{
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

                case MenuChoice.ShowAllAccounts:
                    ShowAllAccounts();
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

    private void ShowAllAccounts()
    {
        Console.Clear();
        DisplayService.ShowHeader("All Accounts");

        var table = new Table()
            .AddColumn(new TableColumn("Account Number").Centered())
            .AddColumn(new TableColumn("Owner"))
            .AddColumn(new TableColumn("Status").Centered());

        table.BorderColor(Color.Blue);

        // Demo data for display purposes
        table.AddRow("12345", "Anna Andersson", "[green]Active[/]");
        table.AddRow("67890", "Bengt Bengtsson", "[green]Active[/]");

        AnsiConsole.Write(table);
        UIHelper.ShowContinuePrompt();
    }

    private void HandleLockUser()
    {
        HandleUserLockStatus("Lock", "Locking account...");
    }

    private void HandleUnlockUser()
    {
        HandleUserLockStatus("Unlock", "Unlocking account...");
    }

    private void HandleUserLockStatus(string action, string progressMessage)
    {
        var userId = DisplayService.AskForInput("Enter user ID");
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
            DisplayService.ShowMessage($"Account {action.ToLower()}ed!", "green", showContinuePrompt: false);
            AsciiArt.PrintSuccessLogo();
        }
        else
        {
            DisplayService.ShowMessage($"Account {action.ToLower()}ing failed!", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
        }
        UIHelper.ShowContinuePrompt();
    }
}