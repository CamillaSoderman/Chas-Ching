using Spectre.Console;
using System;

namespace Chas_Ching.UI.Settings
{
    public static class UIHelper
    {
        public static void ShowContinuePrompt()
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[yellow]------------[/][red]Tryck Enter för att fortsätta[/][yellow]------------[/]");
            Console.ReadLine();
        }
    }
}