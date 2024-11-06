using Spectre.Console;

namespace Chas_Ching.UI.Services
{
    public static class MenuService
    {
        /// <summary>
        /// Static service class that handles displaying account information in a table-spectra format.
        /// Currently, the methods is a test display with hardcoded data
        /// </summary>
        public static void ShowAccountDetails()
        {
            var table = new Table()
                .AddColumn("Kontonummer")
                .AddColumn("Saldo")
                .AddColumn("Valuta")
                .AddColumn("Status");
            table.AddRow("12345", "5000.00", "SEK", "[green]Aktiv[/]");
            AnsiConsole.Write(table);
        }
        public static void ShowAllAccounts()
        {
            var table = new Table()
                .AddColumn("Kontonummer")
                .AddColumn("Saldo")
                .AddColumn("Status");
            table.AddRow("12345", "5000.00", "[green]Aktiv[/]");
            table.AddRow("67890", "1000.00", "[green]Aktiv[/]");
            AnsiConsole.Write(table);
        }
    }
}