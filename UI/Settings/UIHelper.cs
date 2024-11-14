using Spectre.Console;

namespace Chas_Ching.UI.Settings
{
    public static class UIHelper
    {
        public static void ShowContinuePrompt()
        {   // Responsible for displaying a continue prompt to the user and waiting for user input
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[yellow]------------[/][red]Tryck Enter för att fortsätta[/][yellow]------------[/]");
            Console.WriteLine();
            Console.ReadKey(true);
        }
    }
}