using Chas_Ching.Core.Models;

namespace Chas_Ching
{
    public class Program
    {
        public static void Main()
        {
            User user = new Customer("mao", "Mao123!"); // Temporary user for debugging
            UserManagement.registeredUsers.Add(user);
            
            var mainMenu = new MainMenu();
            mainMenu.Start();
        }
    }
}