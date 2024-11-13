using Chas_Ching.Core.Enums;

namespace Chas_Ching.Core.Models
{
    public class Admin : User
    {

        public Admin(string userName, string userPassword) : base(userName, userPassword)
        {
            

        }

        public static void CreateAdmin()
        {
                Admin admin1 = new Admin("Admin", "123!");         //Admin login credentials
                UserManagement.registeredUsers.Add(admin1);
            
        }

        //Create new customer as Admin
        public static void CreateUserCustomer( string userName, string userPassword)
        {

            if (UserManagement.FindUser(userName) == null)      // Check if username is taken
            {
                Customer customer = new Customer( userName, userPassword);
                UserManagement.registeredUsers.Add(customer);   // Add customer in list found in UserManagement
                customer.OpenAccount();
            }

        }

    }
}