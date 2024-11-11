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
                    HandleAdminLogin();
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
    {   // Resnponsible for handling the customer login. Ask for email and password, verify user and start customer menu
        string userEmail = DisplayService.AskForInput("Skriv in din email-address:");
        string password = DisplayService.AskForInput("Skriv in ditt lösenord:");

        var user = UserManagement.FindUser(userEmail); // Find user by email

        if (user != null && UserManagement.VerifyUser(userEmail, password))
        {
            if (user is Customer customer)
            {
                var customerMenu = new CustomerMenu(customer); // Create new instance of CustomerMenu
                customerMenu.Start();
            }
        }
        else
        {   // Display error message if login fails
            DisplayService.ShowMessage($"Login misslyckades! Kontrollera din {userEmail} och lösenord.", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
            UIHelper.ShowContinuePrompt();
        }
    }

    private void HandleCreateAccount()
    {   // Responsible for handling the account creation. Ask for email and password, validate and create account
        string userEmail;
        string userPassword;

        // Get and validate email
        do
        {
            Console.Clear();
            userEmail = DisplayService.AskForInput("Skriv in din emailadress (eller 'Q' för att gå tillbaka)");

            if (userEmail.ToLower() == "q")
            {
                return;
            }

            var (isValid, errorMessage) = UserManagement.isValidEmail(userEmail);

            if (!isValid)
            {
                DisplayService.ShowMessage("Felaktig e-postadress! E-postadressen måste innehålla @ och en domän (exempel@domain.com)", "red", showContinuePrompt: false);
                AsciiArt.PrintErrorLogo();
                UIHelper.ShowContinuePrompt();
            }
        } while (!UserManagement.isValidEmail(userEmail).isValid);

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

        // Check if user already exists. If exists, display error message and return to main menu without calling CreateAccountWithAnimation
        if (UserManagement.FindUser(userEmail) != null)
        {
            DisplayService.ShowMessage("En användare med den e-postadressen är redan registrerad.", "yellow", showContinuePrompt: false);
            return;
        }

        // Add a delay and loading animation to simulate account creation process to a database
        CreateAccountWithAnimation(userEmail, userPassword);
    }

    private void CreateAccountWithAnimation(string email, string password)
    {   // Simulate account creation with a delay and loading animation ex. contacting a database
        bool isSuccess = false;

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("Skapar konto....", ctx =>
            {
                Thread.Sleep(2000); // Simulate a delay of 2 seconds
                try
                {
                    UserManagement.RegisterUser(email, password);
                    isSuccess = true;
                }
                catch
                {
                    isSuccess = false;
                }
            });

        if (isSuccess)
        {
            DisplayService.ShowMessage("Ditt konto har skapat! Välkommen till Chas Ching Bank!", "green", showContinuePrompt: false);
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
        
        string adminUserEmail;
        string adminPassword;


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