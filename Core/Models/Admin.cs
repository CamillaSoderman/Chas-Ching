using Chas_Ching.Core.Enums;

namespace Chas_Ching.Core.Models
{
    public class Admin : User
    {
        // Deklarera Accounts som en lista från klassen Account
        public Dictionary<CurrencyType, decimal> CurrentExchangeRates { get; set; }

        public Admin(string userEmail, string password) : base(userEmail, password)
        {
            // Initiera Accounts-listan
            CurrentExchangeRates = new Dictionary<CurrencyType, decimal>();
        }

        //Create new customer as Admin
        public void CreateUserCustomer()
        {

            if (UserManagement.FindUser(UserEmail) == null)
            {
                Customer customer = new Customer(UserEmail, Password);
                UserManagement.registeredUsers.Add(customer);   // Add customer in list found in UserManagement
                customer.OpenAccount();
            }

        }


        public void UpdateExchangeRates()
        {
            throw new NotImplementedException();
        }
    }
}