namespace Chas_Ching.Core.Models
{
    public abstract class User
    {   // Access Modifiers. Only code in the same class or in a derived class can access protected internal members.
        protected internal string UserEmail { get; set; }
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