using Chas_Ching.UI.Display;
using Chas_Ching.UI.Settings;
using Spectre.Console;


/// <summary>
/// The 'choice' variable stores the user's selected option from the admin menu.
/// It is of type 'MenuChoice', which is an enumeration (enum) that defines all the possible menu options.
/// When the user makes a selection from the menu, the corresponding enum value is assigned to 'choice'.
/// The 'switch' statement checks the value of 'choice' to determine which action to take,
/// allowing the program to execute the appropriate method for the chosen option. fdc
/// </summary>
public class AdminMenu
{
    public void Start()
    {
        while (true)
        {
            var choice = DisplayService.ShowMenu("Adminmeny", MenuText.GetAdminMenuChoices());

            switch (choice)
            {
                case MenuChoice.ShowAllAccounts:
                    ShowAllAccounts();
                    break;
                case MenuChoice.LockUser:
                    HandleLockUser();
                    break;
                case MenuChoice.UnlockUser:
                    HandleUnlockUser();
                    break;
                case MenuChoice.BackToMain:
                    return;
            }
        }
    }

    private void ShowAllAccounts()
    {
        Console.Clear();
        DisplayService.ShowHeader("Alla Konton"); // Display header for account overview
        var table = new Table()
            .AddColumn(new TableColumn("Kontonummer").Centered()) // Add a column for the account number
            .AddColumn(new TableColumn("Ägare")) // Add a column for the account owner's name
            .AddColumn(new TableColumn("Status").Centered()); // Add a column for the account status

        // Hardcoded example data for demonstration purposes
        table.BorderColor(Color.Blue);
        table.AddRow("12345", "Anna Andersson", "[green]Aktiv[/]");
        table.AddRow("67890", "Bengt Bengtsson", "[green]Aktiv[/]");

        AnsiConsole.Write(table); // Display the table in the console
        UIHelper.ShowContinuePrompt(); // Call the UIHelper method to prompt the user to continue
    }

    private void HandleLockUser()
    {
        var userId = DisplayService.AskForInput("Ange användar-ID");
        bool isSuccess = true;

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("Låser konto...", ctx =>
            {
                Thread.Sleep(1000);
            });

        Console.Clear();

        if (isSuccess)
        {
            DisplayService.ShowMessage("Konto låst!", "green", showContinuePrompt: false);
            AsciiArt.PrintSuccessLogo();
        }
        else
        {
            DisplayService.ShowMessage("Konto låsning misslyckades!", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
        }
        UIHelper.ShowContinuePrompt();
    }

    private void HandleUnlockUser()
    {
        var userId = DisplayService.AskForInput("Ange användar-ID");
        bool isSuccess = true;

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("Låser upp konto...", ctx =>
            {
                Thread.Sleep(1000);
            });

        Console.Clear();

        if (isSuccess)
        {
            DisplayService.ShowMessage("Konto upplåst!", "green", showContinuePrompt: false);
            AsciiArt.PrintSuccessLogo();
        }
        else
        {
            DisplayService.ShowMessage("Konto upplåsning misslyckades!", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
        }

        UIHelper.ShowContinuePrompt();
    }
}