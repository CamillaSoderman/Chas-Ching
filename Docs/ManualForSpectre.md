
# Ladda ner i Package Manager Console (Nugget):
- Tools
  - NuGet
    - Packet Manager
      - Öppna Package Manager Console

Skriv in följande kommandon:
```bash
dotnet add package Spectre.Console
dotnet add package Spectre.Console.Cli
```

Referera i varje klass som använder Spectre.Console:
```csharp
using Spectre.Console;
```

Referenser:
- https://mdbouk.com/how-to-create-beautiful-console-applications-with-spectre-console/
- https://spectreconsole.net/prompts/text

Kommandon:
- AnsiConsole.MarkupLine = Skriver ut texten likt console.writeline med markup language. Ex. i den färg som anges i markupen
- AnsiConsole.Prompt = Skapar en prompt som låter användaren välja ett alternativ från en lista
- AnsiConsole.markup = Skapar en markup string som kan användas för att skriva ut text i olika färger
- AnsiConsole.Render = Renderar en markup string
- AnsiColor = En enum som innehåller alla färger som kan användas i Spectre.Console
- Ansiformat = En enum som innehåller alla format som kan användas i Spectre.Console
- Ansisymbols = En enum som innehåller alla symboler som kan användas i Spectre.Console