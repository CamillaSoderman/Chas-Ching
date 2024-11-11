using Chas_Ching.UI.Settings;
using Chas_ChingDemo.UI.Display;
using Spectre.Console;

/// <summary>
/// DisplayService is a static class that provides methods for displaying messages and prompts to the user.
/// Show menus, headers, messages and to ask for user inputs
/// </summary>
public static class DisplayService
{
    public static MenuChoice ShowMenu(string title, MenuChoice[] choices)
    {   // This method displays a menu with a title and a list of hardcoded choices ex. "Logga in som Kund", "Logga in som Admin"
        while (true)
        {
            Console.Clear();
            ShowHeader(title); // Display the title of the menu. Ex: "Main Menu", "Customer Menu", "Admin Menu"

            // Display a menu title and prompt user for input from formatted list of choices to the selection menu
            var selectedMenuOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Välj ett alternativ:[/]") // Promt user to select an option
                    .AddChoices(choices.Select(MenuText.GetMenuText)));

            // Check if the selected string matches any menu choice
            foreach (var choice in choices)
            {
                if (MenuText.GetMenuText(choice) == selectedMenuOption)
                    return choice; // Return the matching Menu text Ex. "Logga in som Kund", "Logga in som Admin"
            }
            Console.WriteLine("Ogiltigt val. Vänligen försök igen.");
        }
    }

    public static void ShowHeader(string title)
    {   // Responsible for displaying the title of the menu. Ex: "Main Menu", "Customer Menu", "Admin Menu"
        AsciiArt.PrintBankLogo();
        AnsiConsole.MarkupLine($"[blue]{title}[/]\n");
    }

    public static void ShowMessage(string message, string color = "blue", bool showContinuePrompt = true) // Default bool value is true
    {   // Responsible for displaying messages to the user. Ex: "Login failed! Check email and password."
        Console.Clear();
        AnsiConsole.MarkupLine($"[{color}]{message}[/]"); // Display message in the specified color ex. "red", "green", "blue", "yellow"

        // By default, show a continue prompt after the message. If showContinuePrompt is set to false, the prompt will not be displayed
        if (showContinuePrompt)
        {
            UIHelper.ShowContinuePrompt(); // call the ShowContinuePrompt method from UIHelper
        }
    }

    public static string AskForInput(string prompt)
    {   // Responsible for asking the user for input. Ex: "Enter email address", "Enter password"
        AnsiConsole.MarkupLine($"[blue]{prompt}:[/]");
        Console.ForegroundColor = ConsoleColor.Green;

        // ?? is a null-coalescing operator. If null, return empty string, if not, return the string
        string input = Console.ReadLine() ?? string.Empty; // Read user input and accept null values

        Console.ResetColor(); // Reset the color of the console, previously set to green
        return input;
    }
}