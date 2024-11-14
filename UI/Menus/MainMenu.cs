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

    {
        // Resnponsible for handling the customer login. Ask for userName and userPassword, verify user and start customer menu
        const int maxAttempts = 3;

        bool isValidUser = false; // Flag to control the loop and ensure valid user login

        // Loop until a valid user logs in or is locked out
        while (!isValidUser)
        {
            string userName = DisplayService.AskForInput("Skriv in ett användarnamn minst 5 tecken (eller 'Q' för att gå tillbaka)");
            if (userName.ToLower() == "q")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(userName)) // Validate username length and non-whitespace
            {
                DisplayService.ShowMessage("Användarnamn får inte vara tomt.", "red", showContinuePrompt: false);
                AsciiArt.PrintErrorLogo();
                UIHelper.ShowContinuePrompt();
                continue; // Re-ask for username if validation fails
            }

            userName = userName.ToLower(); // Convert userName to lowercase
            var user = UserManagement.FindUser(userName);

            // If the user doesn't exist, inform the user and ask for the username again
            if (user == null)
            {
                DisplayService.ShowMessage($"Användaren {userName} finns inte.", "red", showContinuePrompt: false);
                AsciiArt.PrintErrorLogo();
                UIHelper.ShowContinuePrompt();

                continue; // Skip password prompts and re-ask for username
            }

            // If the user is locked, show lock message and exit
            if (user.IsUserLocked())
            {
                DisplayService.ShowMessage($"Användaren {userName} är låst pga. för många misslyckade försök.", "red", showContinuePrompt: false);
                AsciiArt.PrintErrorLogo();
                UIHelper.ShowContinuePrompt();
                return; // Exit after showing the lock message
            }

            // Loop for login attempts until max attempts are reached
            while (user.LoginAttempts < maxAttempts && !isValidUser)
            {
                string userPassword = DisplayService.AskForInput("Skriv in ditt lösenord:");

                // If password is correct, proceed to the customer menu
                if (UserManagement.VerifyUser(userName, userPassword))
                {
                    user.ResetLoginAttempts(); // Reset attempts on successful login

                    if (user is Customer customer)
                    {
                        var customerMenu = new CustomerMenu(customer);
                        customerMenu.Start();
                        isValidUser = true; // Set the flag to true to exit the outer loop
                        return; // Successful login, exit the method
                    }
                }
                else
                {
                    // Password is incorrect, increment login attempts
                    user.IncrementLoginAttempts();

                    // Check if the user is now locked after incrementing login attempts
                    if (user.IsUserLocked())
                    {
                        DisplayService.ShowMessage($"Du har misslyckats 3 gånger. Vänligen kontakta oss.", "red", showContinuePrompt: false);
                        AsciiArt.PrintErrorLogo();
                        UIHelper.ShowContinuePrompt();
                        return; // Exit after locking the user
                    }

                    // Display error message for incorrect password with remaining attempts
                    int remainingAttempts = maxAttempts - user.LoginAttempts; // Calculate remaining attempts
                    DisplayService.ShowMessage($"Login misslyckades! Försök igen. ({remainingAttempts} försök kvar)", "red", showContinuePrompt: false);
                    AsciiArt.PrintErrorLogo();
                    UIHelper.ShowContinuePrompt();
                }
            }

            // If the loop exits after max attempts, inform the user
            DisplayService.ShowMessage("Du har misslyckats 3 gånger. Vänligen kontakta oss.", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
            UIHelper.ShowContinuePrompt();
            return; // Lock the user after 3 failed attempts and exit the method
        }
    }

    private void HandleCreateAccount()
    {   // Responsible for handling the account creation. Ask for userName and userPassword, validate and create account
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

            var (isValid, errorMessage) = UserManagement.IsUserNameValid(userName);

            if (!isValid)
            {
                AsciiArt.PrintErrorLogo();
                UIHelper.ShowContinuePrompt();
            }
        } while (!UserManagement.IsUserNameValid(userName).isValid);

        userName = userName.ToLower(); // Convert userName to lowercase

        // Get and validate userPassword
        do
        {
            userPassword = DisplayService.AskForInput("Skriv in ditt lösenord (eller 'Q' för att gå tillbaka)");

            if (userPassword.ToLower() == "q")
            {
                return;
            }

            var (isValid, errorMessage) = UserManagement.IsPasswordValid(userPassword);

            if (!isValid)
            {
                DisplayService.ShowMessage(errorMessage, "red", showContinuePrompt: false);
                AsciiArt.PrintErrorLogo();
            }
        } while (!UserManagement.IsPasswordValid(userPassword).isValid);

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
    {   // Resnponsible for handling the customer login. Ask for email and userPassword, verify user and start customer menu
        string userName = DisplayService.AskForInput("Skriv in ditt användarnamn:");
        string password = DisplayService.AskForInput("Skriv in ditt lösenord:");

        Admin.CreateAdmin();

        var user = UserManagement.FindUser(userName); // Find user by email

        if (user != null && UserManagement.VerifyUser(userName, password))
        {
            if (user is Admin admin)
            {
                var adminMenu = new AdminMenu(admin); // Create new instance of AdminMenu
                adminMenu.Start();
            }
        }
        else
        {   // Display error message if login fails
            DisplayService.ShowMessage($"Login misslyckades! Kontrollera din {userName} och lösenord.", "red", showContinuePrompt: false);
            AsciiArt.PrintErrorLogo();
            UIHelper.ShowContinuePrompt();
        }
    }
}