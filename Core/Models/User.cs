using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models
{
    public abstract class User
    {
        protected string UserName { get; set; }
        protected string Password { get; set; }
        protected bool IsLocked { get; set; } // True = låser konto

        private int loginAttempts = 0; // Räknare för inloggningsförsök

        protected User(string userName, string password)
        {
            UserName = userName;
            Password = password;
            IsLocked = false; //  False är default värde dvs. anv.konto är öppet
        }

        public abstract void Login();
        public abstract void Logout();
    }
}