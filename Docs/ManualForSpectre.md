
# Ladda ner i Package Manager Console (Nugget):
- Tools
  - NuGet
    - Packet Manager
      - �ppna Package Manager Console

Skriv in f�ljande kommandon:
```bash
dotnet add package Spectre.Console
dotnet add package Spectre.Console.Cli
```

Referera i varje klass som anv�nder Spectre.Console:
```csharp
using Spectre.Console;
```

Referenser:
- https://mdbouk.com/how-to-create-beautiful-console-applications-with-spectre-console/
- https://spectreconsole.net/prompts/text

Kommandon:
- AnsiConsole.MarkupLine = Skriver ut texten likt console.writeline med markup language. Ex. i den f�rg som anges i markupen
- AnsiConsole.Prompt = Skapar en prompt som l�ter anv�ndaren v�lja ett alternativ fr�n en lista
- AnsiConsole.markup = Skapar en markup string som kan anv�ndas f�r att skriva ut text i olika f�rger
- AnsiConsole.Render = Renderar en markup string
- AnsiColor = En enum som inneh�ller alla f�rger som kan anv�ndas i Spectre.Console
- Ansiformat = En enum som inneh�ller alla format som kan anv�ndas i Spectre.Console
- Ansisymbols = En enum som inneh�ller alla symboler som kan anv�ndas i Spectre.Console