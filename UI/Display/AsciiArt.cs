using Spectre.Console;

/// <summary>
/// Contains ASCII art for visual feedback in the application
/// </summary>

namespace Chas_ChingDemo.UI.Display
{
    public static class AsciiArt
    {
        private static readonly string BankLogo = @"
   ***                                           ***
  (   )----------------------------------------(   )
  |   |                                        |   |
  |   | ░█▀▀░█░█░█▀█░█▀▀░░░█▀▀░█░█░▀█▀░█▀█░█▀▀ |   |
  |   | ░█░░░█▀█░█▀█░▀▀█░░░█░░░█▀█░░█░░█░█░█░█ |   |
  |   | ░▀▀▀░▀░▀░▀░▀░▀▀▀░░░▀▀▀░▀░▀░▀▀▀░▀░▀░▀▀▀ |   |
  |   |                                        |   |
  (___)----------------------------------------(___)
";

        private static readonly string SuccessLogo = """
       **                      **
      (  )--------------------(  )
      |  |     SUCCESS!       |  |
      |  |       ==)          |  |
      |  |                    |  |
      (__)--------------------(__)
    """;

        private static readonly string ErrorLogo = """
       **                      **
      (  )--------------------(  )
      |  |      ERROR!        |  |
      |  |       ==(          |  |
      |  |                    |  |
      (__)--------------------(__)
    """;

        public static void PrintBankLogo()
        {
            AnsiConsole.MarkupLine($"[green]{BankLogo}[/]");
        }

        public static void PrintSuccessLogo()
        {
            Console.WriteLine();
            AnsiConsole.MarkupLine($"[green]{SuccessLogo}[/]");
            Console.WriteLine();
        }

        public static void PrintErrorLogo()
        {
            Console.WriteLine();
            AnsiConsole.MarkupLine($"[red]{ErrorLogo}[/]");
            Console.WriteLine();
        }
    }
}