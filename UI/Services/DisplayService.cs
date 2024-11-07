using Chas_Ching.UI.Display;
using Chas_Ching.UI.Settings;
using Spectre.Console;

public static class DisplayService
{
    /// <summary>
    /// Displays a selection menu with a given title and list of choices.
    /// Clears the console, shows the header with the title, and prompts the user to select an option.
    /// Converts the selected text back to its corresponding MenuChoice enum value for easy handling in the program.
    /// </summary>
    public static MenuChoice ShowMenu(string title, MenuChoice[] choices)
    {
        while (true) // Loop until a valid choice is made
        {
            Console.Clear();
            ShowHeader(title); // Display the title header

            // Get display text for each menu choice and prompt user for selection
            var selectedText = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Välj ett alternativ:[/]")
                    .AddChoices(choices.Select(MenuText.GetText)));

            // Check if the selected text matches any menu choice
            foreach (var choice in choices)
            {
                if (MenuText.GetText(choice) == selectedText)
                    return choice; // Return the matching MenuChoice
            }

            // Inform the user of the invalid selection
            Console.WriteLine("Ogiltigt val. Vänligen försök igen.");
        }
    }

    public static void ShowHeader(string title)
    {
        AsciiArt.PrintBankLogo();
        AnsiConsole.MarkupLine($"[blue]{title}[/]\n");
    }
    /// <summary>
    /// Displays a message in the specified color.
    /// Clears the console, shows the message, and prompts the user to press Enter to continue.
    /// </summary>
    public static void ShowMessage(string message, string color = "blue", bool showContinuePrompt = true)
    {
        Console.Clear();
        AnsiConsole.MarkupLine($"[{color}]{message}[/]"); // Display message in the specified color

        // Visa continue prompt bara om det efterfrågas
        if (showContinuePrompt)
        {
            UIHelper.ShowContinuePrompt();
        }
    }
    /// <summary>
    /// Prompts the user for input with a specified prompt message.
    /// Displays the prompt in blue and styles the user input in green.
    /// </summary>
    public static string AskForInput(string prompt)
    {
        Console.Clear();
        return AnsiConsole.Prompt(
            new TextPrompt<string>($"[blue]{prompt}:[/]")
                .PromptStyle("green"));
    }
}