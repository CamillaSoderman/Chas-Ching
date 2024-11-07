using Chas_Ching.UI.Display;
using Chas_Ching.UI.Settings;
using Spectre.Console;
using System;
using System.Runtime.CompilerServices;
using System.Threading;


/// <summary>
/// The 'choice' variable stores the user's selected option from the customer menu.
/// It is of type 'MenuChoice', which is an enumeration (enum) that defines all the possible menu options.
/// When the user makes a selection from the menu, the corresponding enum value is assigned to 'choice'.
/// The 'switch' statement then checks the value of 'choice' to determine which action to take 
/// based on the user's selection, allowing the program to execute the appropriate method for the chosen option.
/// </summary>
public class CustomerMenu
{
    public void Start()
    {
        while (true)
        {
            var choice = DisplayService.ShowMenu("Kundmeny", MenuText.GetCustomerMenuChoices());

            switch (choice)
            {
                case MenuChoice.ShowAccount:
                    ShowAccountDetails();
                    break;
                case MenuChoice.MakeTransaction:
                    HandleTransaction();
                    break;
                case MenuChoice.ApplyForLoan:
                    HandleLoanApplication();
                    break;
                case MenuChoice.ExchangeCurrency:
                    HandleCurrencyExchange();
                    break;
                case MenuChoice.BackToMain:
                    return;
            }
        }
    }

    private void ShowAccountDetails()
    {
        Console.Clear();
        DisplayService.ShowHeader("Kontoöversikt");
        var table = new Table()
            .AddColumn(new TableColumn("Kontonummer").Centered())
            .AddColumn(new TableColumn("Saldo").RightAligned())
            .AddColumn(new TableColumn("Valuta").Centered());
        table.AddRow("12345", "5000.00", "SEK");
        AnsiConsole.Write(table);
        UIHelper.ShowContinuePrompt();
    }

    private void HandleTransaction()
    {
        var amount = DisplayService.AskForInput("Ange belopp");
        bool isSuccess = true;

        // Använd AnsiConsole.Status för att visa spinner och statusmeddelande under processen
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Star)
            .Start("Genomför transaktion...", ctx =>
            {
                Thread.Sleep(1000); // Simulerar transaktionsprocessen
            });

        Console.Clear();

        if (isSuccess)
        {
            DisplayService.ShowMessage("Transaktion lyckades!", "green", showContinuePrompt: false);
            AsciiArt.PrintSuccessLogo();
        }
        else
        {
            DisplayService.ShowMessage("Transaktion misslyckades!", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
        }

        UIHelper.ShowContinuePrompt();

    }

    private void HandleLoanApplication()
    {
        var amount = DisplayService.AskForInput("Ange lånebelopp");
        bool isSuccess = true;
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("Skickar låneansökan...", ctx =>
            {
                Thread.Sleep(1000);
            });

        Console.Clear();

        if (isSuccess)
        {
            DisplayService.ShowMessage("Låneansökan lyckades!", "green", showContinuePrompt: false);
            AsciiArt.PrintSuccessLogo();
        }
        else
        {
            DisplayService.ShowMessage("Låneansökan misslyckades!", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
        }
        UIHelper.ShowContinuePrompt();
    }
    private void HandleCurrencyExchange()
    {
        bool isSuccess = false; // Set to false just for testing the if-statment and PrintErrorLogo

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("Utför valutaväxling...", ctx =>
            {
                Thread.Sleep(1000);
            });

        Console.Clear();

        if (isSuccess)
        {
            DisplayService.ShowMessage("Valutaväxling lyckades!", "green", showContinuePrompt: false);
            AsciiArt.PrintSuccessLogo();
        }
        else
        {
            DisplayService.ShowMessage("Valutaväxling misslyckades!", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
        }
        UIHelper.ShowContinuePrompt();
    }
}

/// <summary>
/// Task.Run(...) starts a new task on a separate thread for executing ShowLoadingAsync asynchronously.
/// async() => await ... handles asynchronous operations, allowing the UI to remain responsive while loading.
/// await pauses the execution of the current method until the ShowLoadingAsync method completes.
/// .Wait() blocks the current thread until the task is complete, ensuring that the success message is shown only after loading finishes.
/// </summary>