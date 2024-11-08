using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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


        public static void RegisterUser()
        {
            string userEmail;
            string password;

            do
            {
                Console.Write("Enter email: ");
                userEmail = Console.ReadLine();


                if (!IsValidEmail(userEmail))
                {
                    Console.WriteLine("Invalid email address. Please provide a valid email.");

                }
                else if (FindUser(userEmail) != null)
                {
                    Console.WriteLine("Username already exists. Please choose another one.");

                }
                else
                {
                    break;

                }

            } while (true);

            bool isPasswordValid = false;
            do
            {
                Console.Write("Enter password: ");
                password = Console.ReadLine();


                if (IsPasswordValid(password))
                {
                    isPasswordValid = true;

                }

            } while (!isPasswordValid);

            // Create a new user obj based on the user type (If Customer or If Admin) 
            User newUser = new Customer(userEmail, password);
            registeredUsers.Add(newUser);
            Console.WriteLine($"User {userEmail} registered successfully.");


        }

        // Method takes an email string as input and returns true if the email is valid otherwise false.
        public static bool IsValidEmail(string email)
        {
            //Basic regex to validate the email format
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        public static bool IsPasswordValid(string password)
        {
            // Check if the password is less than 5 characters
            if (password.Length < 5)
            {
                Console.WriteLine("Password invalid.\n Must be at least 5 characters long , contain uppercase letters, lowercase letters, and special characters.");
                return false;
            }

            // Check if the password meets the other criteria (uppercase, lowercase, special character)
            if (!(password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(ch => "!@#$%^&*()_+[]{}|/;:,.<>?".Contains(ch))))
            {
                Console.WriteLine("Password invalid. Must contain uppercase letters, lowercase letters, and special characters.");
                return false;
            }

            // If all checks pass, return true
            return true;
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
    }
}


// Note. Every class is static, so no instances of the class can be created. The methods are called directly on the class.