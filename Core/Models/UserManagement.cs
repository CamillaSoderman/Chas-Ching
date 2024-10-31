using System;
using System.Collections.Generic;

namespace Chas_Ching.Core.Models
{
    public class UserManagement
    {   // List of registered users
        private static List<User> registeredUsers = new List<User>();

        // Find a user by username
        public static User? FindUser(string userName)
        {
            return registeredUsers.Find(user => user.UserEmail.Equals(userName, StringComparison.OrdinalIgnoreCase)); // Ignore case
        }

        // Method to verify if the user exists and is locked out. Increment login attempts if password is incorrect
        public static bool VerifyUser(string userName, string password)
        {
            var user = FindUser(userName);
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
            Console.WriteLine($"User {user.UserEmail} logged out successfully.");
        }

        // Registers a new user with a unique username and password and
        // optional boolean to determine if the user is a customer or admin (default is customer)
        public static void RegisterUser(string userName, string password, bool isCustomer = true)
        {
            if (FindUser(userName) != null)
            {
                Console.WriteLine("Username already exists. Please choose another one.");
                return;
            }
            // Create a new user obj based on the user type (If Customer or If Admin) 
            User newUser = isCustomer ? new Customer(userName, password) : new Admin(userName, password);
            registeredUsers.Add(newUser);
            Console.WriteLine($"User {userName} registered successfully.");
        }
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
        // Logik för att öppna ett konto
        public static void OpenAccount(User user)
        {
            Console.WriteLine($"");
        }
    }
}
