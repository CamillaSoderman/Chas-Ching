using Chas_Ching.Core.Enums;
using Chas_Ching.Core.Models;

namespace Chas_Ching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string email = Console.ReadLine();
            //string password = Console.ReadLine();

            //Customer customer = new Customer(email, password);
            //customer.OpenAccount(); 
            
        }
    }
}

/*    För att köra UI i consolen
 *    Skapa en ny instanse av MainMenu.cs och kalla på Start-metoden
 *    
 *    var mainMenu = new MainMenu();
 *    mainMenu.Start();
 *    
 */

// Ladda ner i Package Manager Console (Nugget)
//  Tools
//    >NuGet
//     >Packet Manager
//       >Öppna Package Manager Console

//   Skriv in följande kommandon;
// > dotnet add package Spectre.Console
// > dotnet add package Spectre.Console.Cli

// Referera i varje klass som använder Spectre.Console
// using Spectre.Console;

/* Referenser
 * https://mdbouk.com/how-to-create-beautiful-console-applications-with-spectre-console/
 * https://spectreconsole.net/prompts/text
 *
 */

/*
BankApp
├── Program.cs
├── UI
│   ├── Menus
│   │   ├── MainMenu.cs
│   │   ├── CustomerMenu.cs
│   │   ├── AdminMenu.cs
│   │   └── BaseMenu.cs
│   ├── Services
│   │   ├── MenuService.cs
│   │   └── DisplayService.cs
│   ├── Display
│   │   └── AsciiArt.cs
│   └── Settings
│       ├── AppSettings.cs
│       ├── MenuOptions.cs
│       └── UIHelper.cs
*/