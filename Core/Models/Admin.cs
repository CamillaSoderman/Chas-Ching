using Chas_Ching.Core.Enums;

namespace Chas_Ching.Core.Models
{
    public class Admin : User
    {
        
        // Deklarera Accounts som en lista från klassen Account
       // public Dictionary<CurrencyType, decimal> CurrentExchangeRates { get; set; }

        public Admin(string userName, string userPassword) : base(userName, userPassword)
        {
            

        }

        public static void CreateAdmin()
        {
                Admin admin1 = new Admin("Admin@chasching.se", "Admin123!");         //Admin login credentials
                UserManagement.registeredUsers.Add(admin1);
            
        }

        //Create new customer as Admin
        public static void CreateUserCustomer( string userName, string userPassword)
        {

            if (UserManagement.FindUser(userName) == null)
            {
                Customer customer = new Customer( userName, userPassword);
                UserManagement.registeredUsers.Add(customer);   // Add customer in list found in UserManagement
                customer.OpenAccount();
            }

        }

    }
}