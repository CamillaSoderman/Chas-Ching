using Chas_Ching.Core.Models;

namespace Chas_Ching
{
    public class Program
    {
        public static void Main()
        {
            User user1 = new Customer("a@a.se", "Mao123!"); // Temporary user for testing
            UserManagement.RegisterUser(user1.UserEmail, user1.Password); // Register user
                
            var mainMenu = new MainMenu();
            mainMenu.Start();
        }
    }
}