using Chas_Ching.UI.Display;
using Chas_Ching.UI.Settings;
using Spectre.Console;

public class MainMenu
{
    public void Start()
    {
        while (true)
        {
            var choice = DisplayService.ShowMenu("Huvudmeny", MenuText.GetMainMenuChoices());

            // Handle the appropriate action based on the user's choice
            // Ex. Call Enum MenuChoice and switch case to customerMenu to open the customer menu
            switch (choice)
            {
                case MenuChoice.CustomerLogin:
                    var customerMenu = new CustomerMenu();
                    customerMenu.Start();
                    break;
                case MenuChoice.AdminLogin:
                    var adminMenu = new AdminMenu();
                    adminMenu.Start();
                    break;
                case MenuChoice.CreateNewAccount:
                    HandleCreateAccount();
                    break;
                case MenuChoice.Exit:
                    DisplayService.ShowMessage(AppSettings.Messages.ExitMessage, "green");
                    return;
            }
        }
    }
    /// <summary>
    /// Just for demo perpouse. Handle the process of creating a new account.
    /// Prompts the user for account details, simulates a loading screen, and displays a success message.
    /// </summary>

    private void HandleCreateAccount()
    {
        string userEmail = DisplayService.AskForInput("Ange emailadress");
        string userPassword = DisplayService.AskForInput("Ange lösenord");
        bool isSuccess = true;

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("Skapar konto...", ctx =>
            {
                Thread.Sleep(1000);
            });

        DisplayService.ShowMessage(isSuccess ? "Skapande lyckades" : "Skapande misslyckades!", isSuccess ? "green" : "red");

        if (isSuccess)
        {
            AsciiArt.PrintSuccessLogo();
        }
        else
        {
            AsciiArt.PrintErrorLogo();
        }
    }
}
/// <summary>
/// Task.Run(...) starts a new task on a separate thread.
/// async() => await ... handles asynchronous operations.
/// await pause the execution of HandleCreateAccount() until the ShowLoadingAsync method completes
/// .Wait() blocks the current thread until the task is complete.
/// </summary>