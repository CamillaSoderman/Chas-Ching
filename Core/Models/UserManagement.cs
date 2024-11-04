using System;
using System.Collections.Generic;

namespace Chas_Ching.Core.Models
{
    public class UserManagement
    {   // List of registered users
        private static List<User> registeredUsers = new List<User>();

        // Find a user by username. StringComparison.OrdinalIgnoreCase ignores case and returns the first match. Returns null if not found.
        public static User? FindUser(string userEmail)
        {
            return registeredUsers.Find(user => user.UserEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase)); 
        }

        // Method to verify if the user exists and is locked out. Increment login attempts if password is incorrect
        public static bool VerifyUser(string userEmail, string password)
        {
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

        // Attempts to log in a user by checking if the provided username exists
        public static void Login(string userEmail, string password)
        {
            if (VerifyUser(userEmail, password))
            {
                Console.WriteLine($"Login successful for user: {userEmail}");
            }
        }

        // Logout metoden behöver färdigställas. Ska programmet avslutas när en användare loggar ut?
        public static void Logout(User user)
        {
            Console.WriteLine($"Thank you for using Chas-Ching bank {user.UserEmail}!");
        }

        // Registers a new user with a unique username and password 
        // The boolean is optional argument to determine if the user is a customer or admin (default is customer)
        public static void RegisterUser(string userEmail, string password, bool isCustomer = true)
        {
            if (FindUser(userEmail) != null)
            {
                Console.WriteLine("Username already exists. Please choose another one.");
                return;
            }
            // Create a new user obj based on the user type (If Customer or If Admin) 
            User newUser = isCustomer ? new Customer(userEmail, password) : new Admin(userEmail, password);
            registeredUsers.Add(newUser);
            Console.WriteLine($"User {userEmail} registered successfully.");
        }
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
        // Ska metoden för att öppna konto ligga här eller i Customer-klassen?
        public static void OpenAccount(User user)
        {

            // If (user == nul)
            // print "User not found" and return

            // promt user to choose a currency type
            // If (invalid currency type)
            // print "Invalid currency type" and return

            // prompt user to enter an initial deposit amount
            // if (invalid amount)
            // print "Invalid amount" and return

            // create a new account object with
            // the selected currency type
            // initial deposit amount
            // unique account number (with random numbers?)

            // Print "Account created with ID [AccountId] and balance [Balance] [Currency]"
        }
    }
}


// Note. Every class is static, so no instances of the class can be created. The methods are called directly on the class.