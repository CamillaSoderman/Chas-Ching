using System.Text.RegularExpressions;

namespace Chas_Ching.Core.Models
{
    public class UserManagement
    {   // List of registered users
        public static readonly List<User> registeredUsers = new List<User>();

        public static User? FindUser(string userEmail)
        {   // Find a user by username. StringComparison.OrdinalIgnoreCase ignores case and returns the first match. Returns null if not found.
            return registeredUsers.Find(user => user.UserEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase));
        }

        public static bool VerifyUser(string userEmail, string password)
        {   // Method to verify if the user exists and is locked out. Increment login attempts if password is incorrect
            var user = FindUser(userEmail); // Sets user to the user object if found, otherwise null
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return false;
            }

            if (user.IsUserLocked())
            {
                Console.WriteLine("User is locked out due to 3 failed login attempts.");
                return false;
            }

            // Validate password and handle attempts
            if (user.Password != password)
            {
                Console.WriteLine("Invalid password.");
                user.IncrementLoginAttempts(); // Increment login attempts
                return false;
            }
            return true; // Credentials are valid
        }

        public static void RegisterUser(string userEmail, string password)
        {   // Registers a new user with a unique username and password
            // Validate email and password
            var (isEmailValid, emailErrorMessage) = isValidEmail(userEmail);
            var (isPasswordValid, passwordErrorMessage) = UserManagement.isPasswordValid(password);

            if (!isEmailValid)
            {
                DisplayService.ShowMessage(emailErrorMessage, "red", showContinuePrompt: false);
                return;
            }

            if (!isPasswordValid)
            {
                DisplayService.ShowMessage(passwordErrorMessage, "red", showContinuePrompt: false);
                return;
            }

            // This function just return the user without execute registerUsers.Add. Error message displays in MainMenu
            if (FindUser(userEmail) != null)
            {
                return;
            }

            // Create a new user obj based
            User newUser = new Customer(userEmail, password);
            registeredUsers.Add(newUser);
            DisplayService.ShowMessage($"Användaren {userEmail} registrerades.", "green", showContinuePrompt: false);
        }

        public static (bool isValid, string errorMessage) isValidEmail(string email)
        {   // Method takes an email string as input and returns true if the email is valid otherwise false.
            if (string.IsNullOrWhiteSpace(email))
            {
                return (false, "E-postadressen får inte vara tom.");
            }

            // Basic regex to validate the email format
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);

            // Check if email matches the pattern
            if (regex.IsMatch(email))
            {
                return (true, string.Empty); // Valid email, no error message
            }
            else
            {
                return (false, "Felaktig e-postadress! E-postadressen måste innehålla @ och en domän (exempel@domain.com)");
            }
        }

        public static (bool isValid, string errorMessage) isPasswordValid(string password)
        {   // Check if the password is less than 5 characters
            if (password.Length < 5)
            {
                return (false, "Lösenordet måste vara minst 5 tecken långt.");
            }
            // Check if the password meets the other criteria (uppercase, lowercase, special character)
            if (!(password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(ch => "!@#$%^&*()_+[]{}|/;:,.<>?".Contains(ch))))
            {
                return (false, "Lösenord måste innehålla minst 1 stor och små bokstav med minst ett specialtecken.");
            }
            // Check if the password is empty
            if (string.IsNullOrWhiteSpace(password))
            {
                return (false, "Lösenordet får inte vara tomt.");
            }
            return (true, string.Empty); // Return true and an empty string if the password is valid
        }

        /* Metoderna används inte i programmet
                public static void LockAccount(string userEmail)
        {
            var user = FindUser(userEmail);
            if (user != null)
            {
                user.IsLocked = true;
                Console.WriteLine($"User {userEmail} has been locked out.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        public static void UnlockAccount(string userEmail)
        {
            var user = FindUser(userEmail);
            if (user != null)
            {
                user.IsLocked = false;
                Console.WriteLine($"User {userEmail} has been unlocked.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public static void Login(string userEmail, string password)
        {   // Attempts to log in a user by checking if the provided username exists
            if (VerifyUser(userEmail, password))
            {
                Console.WriteLine($"Login successful for user: {userEmail}");
            }
        }
        public static void Logout(User user)
        {   // Logout metoden behöver färdigställas. Ska programmet avslutas när en användare loggar ut?
            Console.WriteLine($"Thank you for using Chas-Ching bank {user.UserEmail}!");
        }
         */
    }
}

// Note. Every class is static, so no instances of the class can be created. The methods are called directly on the class.