using Chas_Ching.Core.Models;
using Chas_Ching.UI.Settings;
using Chas_ChingDemo.UI.Display;
using Spectre.Console;

public class MainMenu
{
    private readonly UserManagement _userManagement = new UserManagement(); // Field - instance of UserManagement

    public void Start()
    {   // Main menu loop, set title and display menu choices by calling ShowMenu method from DisplayService
        while (true)
        {   // Save user choice in variable. User choice is returned from ShowMenu method in DisplayService
            var choice = DisplayService.ShowMenu("Huvudmeny", MenuText.GetMainMenuChoices());

            switch (choice)
            {
                case MenuChoice.CustomerLogin:
                    HandleCustomerLogin();
                    break;

                case MenuChoice.AdminLogin:
                    //var adminMenu = new AdminMenu();
                    //adminMenu.Start();
                    break;

                case MenuChoice.CreateNewAccount:
                    HandleCreateAccount();
                    break;

                case MenuChoice.Exit:
                    DisplayService.ShowMessage(AppSettings.Messages.ExitMessage, "green");
                    return;
            }
        }
    }

    private void HandleCustomerLogin()
    {   // Resnponsible for handling the customer login. Ask for userName and password, verify user and start customer menu
        string userName = DisplayService.AskForInput("Skriv in ditt användarnamn: ");
        string password = DisplayService.AskForInput("Skriv in ditt lösenord:");
        userName = userName.ToLower(); // Convert userName to lowercase

        var user = UserManagement.FindUser(userName); // Find user by userName

        if (user != null && UserManagement.VerifyUser(userName, password))
        {
            if (user is Customer customer)
            {
                var customerMenu = new CustomerMenu(customer); // Create new instance of CustomerMenu
                customerMenu.Start();
            }
        }
        else
        {   // Display error message if login fails
            DisplayService.ShowMessage($"Login misslyckades! Kontrollera din {userName} och lösenord.", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
            UIHelper.ShowContinuePrompt();
        }
    }

    private void HandleCreateAccount()
    {   // Responsible for handling the account creation. Ask for userName and password, validate and create account
        string userName;
        string userPassword;

        // Get and validate userName
        do
        {
            Console.Clear();
            userName = DisplayService.AskForInput("Skriv in ett användarnamn minst 5 tecken (eller 'Q' för att gå tillbaka)");

            if (userName.ToLower() == "q")
            {
                return;
            }

            var (isValid, errorMessage) = UserManagement.isUserNameValid(userName);

            if (!isValid)
            {
                AsciiArt.PrintErrorLogo();
                UIHelper.ShowContinuePrompt();
            }
        } while (!UserManagement.isUserNameValid(userName).isValid);

        userName = userName.ToLower(); // Convert userName to lowercase

        // Get and validate password
        do
        {
            userPassword = DisplayService.AskForInput("Skriv in ditt lösenord password (eller 'Q' för att gå tillbaka)");

            if (userPassword.ToLower() == "q")
            {
                return;
            }

            var (isValid, errorMessage) = UserManagement.isPasswordValid(userPassword);

            if (!isValid)
            {
                DisplayService.ShowMessage(errorMessage, "red", showContinuePrompt: false);
                AsciiArt.PrintErrorLogo();
            }
        } while (!UserManagement.isPasswordValid(userPassword).isValid);

        // Add a delay and loading animation to simulate account creation process to a database
        CreateAccountWithAnimation(userName, userPassword);
    }

    private void CreateAccountWithAnimation(string userName, string password)
    {   // Simulate account creation with a delay and loading animation ex. contacting a database
        bool isSuccess = false;

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("Skapar konto....", ctx =>
            {
                Thread.Sleep(2000); // Simulate a delay of 2 seconds
                try
                {
                    UserManagement.RegisterUser(userName, password);
                    isSuccess = true;
                }
                catch
                {
                    isSuccess = false;
                }
            });

        if (isSuccess)
        {
            DisplayService.ShowMessage($"Ditt konto har skapat! Välkommen till Chas Ching Bank! {userName}", "green", showContinuePrompt: false);
            AsciiArt.PrintSuccessLogo();
        }
        else
        {
            DisplayService.ShowMessage("Nytt konto misslyckades", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
        }

        UIHelper.ShowContinuePrompt();
    }

    private void HandleAdminLogin()
    {   // Resnponsible for handling the customer login. Ask for email and password, verify user and start customer menu
        string userEmail = DisplayService.AskForInput("Skriv in din email-address:");
        string password = DisplayService.AskForInput("Skriv in ditt lösenord:");
        
        Admin.CreateAdmin();



        var user = UserManagement.FindUser(userEmail); // Find user by email

        if (user != null && UserManagement.VerifyUser(userEmail, password))
        {
            if (user is Admin admin)
            {
                var adminMenu = new AdminMenu(admin); // Create new instance of AdminMenu
                adminMenu.Start();
                
            }
        }
        else
        {   // Display error message if login fails
            DisplayService.ShowMessage($"Login misslyckades! Kontrollera din {userEmail} och lösenord.", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
            UIHelper.ShowContinuePrompt();
        }
    }
}