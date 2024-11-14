namespace Chas_Ching.Core.Models
{
    public abstract class User
    {   // Access Modifiers. Only code in the same class or in a derived class can access protected internal members.
        protected internal string UserName { get; set; }
        protected internal string UserPassword { get; set; }
        protected internal bool IsLocked { get; set; } = false;

        public int LoginAttempts { get; set; }

        protected User(string userName, string userPassword)
        {
            UserName = userName;
            UserPassword = userPassword;
            LoginAttempts = 0;
        }

        public bool IsUserLocked()
        {   // Method to check if the user is locked
            return IsLocked;
        }

        public void ResetLoginAttempts()
        {   // Reset login attempts after successful login or unlock by admin
            LoginAttempts = 0;
            IsLocked = false;
        }

        public void IncrementLoginAttempts()
        {
            // Increment login attempts every time this method is called
            if (IsLocked) return;
            LoginAttempts++;

            //If LoginAttempts is greater or equal than 3, lock the user account
            if (LoginAttempts >= 3)
            {
                IsLocked = true;
            }
        }
    }
}