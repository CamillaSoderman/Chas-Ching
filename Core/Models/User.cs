namespace Chas_Ching.Core.Models
{
    public abstract class User
    {   // Access Modifiers. Only code in the same class or in a derived class can access protected internal members.

        protected internal string UserName { get; set; }
        protected internal string UserPassword { get; set; }
        protected internal bool IsLocked { get; set; } = false;

        private int loginAttempts;

        protected User(string userName, string userPassword)
        {
            UserName = userName;
            UserPassword = userPassword;
            loginAttempts = 0;
        }
        //Method to use if the user i locked.
        public bool IsUserLocked() => IsLocked;
       
        //Method to increment the login attempts.
        //Lockinguser account after failed login attempts.
        public void IncrementLoginAttempts()
        {
            loginAttempts = 0;
           
            //If loginAttempts is greater than 3, lock the user account.
            if (loginAttempts >3)
            {
                IsLocked = true;
               
            }
        }

    }
}