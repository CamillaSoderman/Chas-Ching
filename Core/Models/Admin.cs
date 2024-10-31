using Chas_Ching.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models
{
    public class Admin : User
    {
        // Deklarera Accounts som en lista från klassen Account
        public Dictionary<CurrencyType, decimal> CurrentExchangeRates { get; set; }

        public Admin(string userName, string password) : base(userName, password)
        {
            // Initiera Accounts-listan
            CurrentExchangeRates = new Dictionary<CurrencyType, decimal>();
        }

        public override void Login()
        {
            throw new NotImplementedException();
        }

        public override void Logout()
        {
            throw new NotImplementedException();
        }
        public void CreateUser()
        {
            throw new NotImplementedException();
        }

        public void UpdateExchangeRates()
        {
            throw new NotImplementedException();
        }
    }
}