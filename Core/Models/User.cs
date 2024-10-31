using System;

namespace Chas_Ching.Core.Models
{
    public abstract class User
    {
        public string UserEmail { get; protected set; }
        protected internal string Password { get; set; }
        protected internal bool IsLocked { get; set; } = false;

        private int loginAttempts;

        protected User(string userName, string password)
        {
            UserEmail = userName;
            Password = password;
            loginAttempts = 0;
        }

        public bool IsUserLocked() => IsLocked;

        public void IncrementLoginAttempts()
        {
            loginAttempts++;
            if (loginAttempts >= 3)
            {
                IsLocked = true;
            }
        }
    }
}
