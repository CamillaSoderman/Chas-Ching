using Chas_Ching.UI.Settings;
using Chas_ChingDemo.UI.Display;
using System.Text.RegularExpressions;

namespace Chas_Ching.Core.Models
{
    public class UserManagement
    {   // List of registered users
        public static readonly List<User> registeredUsers = new List<User>();

        public static User? FindUser(string userName)
        {   // Find a user by username. StringComparison.OrdinalIgnoreCase ignores case and returns the first match. Returns null if not found.
            return registeredUsers.Find(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        public static bool VerifyUser(string userName, string userPassword)
        {   // Method to verify if the user exists and is locked out. Increment login attempts if userPassword is incorrect
            var user = FindUser(userName); // Sets user to the user object if found, otherwise null
            if (user == null)
            {
                Console.WriteLine("Använder finns inte");
                return false;
            }

            if (user.IsUserLocked())
            {
                Console.WriteLine("Användern är låste pga. 3 misslyckade inloggningsförsök");
                return false;
            }

            // Validate userPassword and handle attempts
            if (user.UserPassword != userPassword)
            {

                Console.WriteLine("Ogiltig lösenord");
                user.IncrementLoginAttempts(); // Increment login attempts
                return false;
            }
            return true; // Credentials are valid
        }

        public static void RegisterUser(string userName, string userPassword)
        {   // Registers a new user with a unique username and userPassword
            // Validate email and userPassword
            var (isEmailValid, emailErrorMessage) = isValidEmail(userName);
            var (isPasswordValid, passwordErrorMessage) = UserManagement.isPasswordValid(userPassword);

            if (!isUserNameValid)
            {
                DisplayService.ShowMessage(userNameErrorMessage, "red", showContinuePrompt: false);
                return;
            }

            if (!isPasswordValid)
            {
                DisplayService.ShowMessage(passwordErrorMessage, "red", showContinuePrompt: false);
                return;
            }

            // This function just return the user without execute registerUsers.Add. Error message displays in MainMenu
            if (FindUser(userName) != null)
            {
                return;
            }

            // Create a new user obj based
            User newUser = new Customer(userName, userPassword);
            registeredUsers.Add(newUser);
            DisplayService.ShowMessage($"Användaren {userName} registrerades.", "green", showContinuePrompt: false);
        }

        public static (bool isValid, string errorMessage) isUserNameValid(string userName)
        {   // Method takes an userName string as input and returns true if the userName is valid otherwise false.
            if (string.IsNullOrWhiteSpace(userName))
            {
                DisplayService.ShowMessage($"Användarnamn får inte vara tomt", "red", showContinuePrompt: false);
                return (false, string.Empty);
            }
            // Check if userName matches the pattern
            else if (userName.Length < 5)
            {
                DisplayService.ShowMessage($"Användarnamn måste vara minst 5 tecken lång", "red", showContinuePrompt: false);
                return (false, string.Empty);
            }
            // Check if user already exists. If exists, display error message and return to main menu without calling CreateAccountWithAnimation
            else if (UserManagement.FindUser(userName) != null)
            {
                DisplayService.ShowMessage($"Användare {userName} är upptaget", "red", showContinuePrompt: false);
                return (false, string.Empty);
            }
            else
            {
                return (true, string.Empty);
            }
        }

        public static (bool isValid, string errorMessage) isPasswordValid(string userPassword)
        {   // Check if the userPassword is less than 5 characters
            if (userPassword.Length < 5)
            {
                return (false, "Lösenordet måste vara minst 5 tecken långt.");
            }
            // Check if the userPassword meets the other criteria (uppercase, lowercase, special character)
            if (!(userPassword.Any(char.IsUpper) && userPassword.Any(char.IsLower) && userPassword.Any(ch => "!@#$%^&*()_+[]{}|/;:,.<>?".Contains(ch))))
            {
                return (false, "Lösenord måste innehålla minst 1 stor och små bokstav med minst ett specialtecken.");
            }
            // Check if the userPassword is empty
            if (string.IsNullOrWhiteSpace(userPassword))
            {
                return (false, "Lösenordet får inte vara tomt.");
            }
            return (true, string.Empty); // Return true and an empty string if the userPassword is valid
        }

        /* Metoderna används inte i programmet
                public static void LockAccount(string userName)
        {
            var user = FindUser(userName);
            if (user != null)
            {
                user.IsLocked = true;
                Console.WriteLine($"User {userName} has been locked out.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        public static void UnlockAccount(string userName)
        {
            var user = FindUser(userName);
            if (user != null)
            {
                user.IsLocked = false;
                Console.WriteLine($"User {userName} has been unlocked.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public static void Login(string userName, string userPassword)
        {   // Attempts to log in a user by checking if the provided username exists
            if (VerifyUser(userName, userPassword))
            {
                Console.WriteLine($"Login successful for user: {userName}");
            }
        }
        public static void Logout(User user)
        {   // Logout metoden behöver färdigställas. Ska programmet avslutas när en användare loggar ut?
            Console.WriteLine($"Thank you for using Chas-Ching bank {user.userName}!");
        }
         */
    }
}

// Note. Every class is static, so no instances of the class can be created. The methods are called directly on the class.