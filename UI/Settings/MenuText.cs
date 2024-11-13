namespace Chas_Ching.UI.Settings
{
    /// <summary>
    /// Enum listing all menu choices in the application.
    /// Each option represents a specific action and make it easier to reference by name
    /// </summary>
    public enum MenuChoice
    {
        // Main Menu
        CustomerLogin,

        AdminLogin,
        CreateNewAccount,
        Exit,

        // Costumer Menu
        ShowAccount,

        OpenNewAccount,
        MakeTransaction,
        ApplyForLoan,
        ExchangeCurrency,
        BackToMainCustomer,

        // Admin Menu
        CreateNewCustomer,
        ShowAllCustomers,

        LockUser,
        UnlockUser,
        BackToMainAdmin
    }

    /// <summary>
    /// Static class to manage the display texts for menu options.
    /// Dictionary that links each MenuChoice option to a specific display text
    /// This allows us to retrieve the correct text for any menu choice
    /// </summary>
    public static class MenuText
    {
        private static readonly Dictionary<MenuChoice, string> Texts = new Dictionary<MenuChoice, string>
        {
            // Main menu
            { MenuChoice.CustomerLogin, "Logga in som Kund" },
            { MenuChoice.AdminLogin, "Logga in som Admin" },
            { MenuChoice.CreateNewAccount, "Skapa nytt konto" },
            { MenuChoice.Exit, "Avsluta" },

            // Costumer menu
            { MenuChoice.ShowAccount, "Visa Konto" },
            { MenuChoice.OpenNewAccount, "Öppna nytt konto" },
            { MenuChoice.MakeTransaction, "Transaktioner" },
            { MenuChoice.ApplyForLoan, "Ansök om Lån" },
            { MenuChoice.ExchangeCurrency, "Valutaväxling" },
            { MenuChoice.BackToMainCustomer, "Återvänd till Huvudmeny" },

            // Admin menu
            { MenuChoice.CreateNewCustomer, "Skapa ny kund" },
            { MenuChoice.ShowAllCustomers, "Lista alla kunder" },
            { MenuChoice.LockUser, "Lås användare" },
            { MenuChoice.UnlockUser, "Lås upp användare" },
            { MenuChoice.BackToMainAdmin, "Återvänd till Huvudmeny" }
        };

        public static string GetMenuText(MenuChoice choice)
        {   // Resonsible for sending the display text associated with a specific menu choice. Ex: "Logga in som Kund", "Logga in som Admin"
            return Texts[choice];
        }

        public static MenuChoice[] GetMainMenuChoices()
        {   // Returns an array of main menu options as MenuChoice items.
            MenuChoice[] choices = new MenuChoice[4]; // Specify the size of the array
            choices[0] = MenuChoice.CustomerLogin;
            choices[1] = MenuChoice.AdminLogin;
            choices[2] = MenuChoice.CreateNewAccount;
            choices[3] = MenuChoice.Exit;
            return choices;
        }

        public static MenuChoice[] GetCustomerMenuChoices()
        {   // Returns an array of customer menu options as MenuChoice items.
            MenuChoice[] choices = new MenuChoice[6]; // Specify the size of the array
            choices[0] = MenuChoice.ShowAccount;
            choices[1] = MenuChoice.OpenNewAccount;
            choices[2] = MenuChoice.MakeTransaction;
            choices[3] = MenuChoice.ApplyForLoan;
            choices[4] = MenuChoice.ExchangeCurrency;
            choices[5] = MenuChoice.BackToMainCustomer;
            return choices;
        }

        public static MenuChoice[] GetAdminMenuChoices()
        {   // Returns an array of admin menu options as MenuChoice items.
            MenuChoice[] choices = new MenuChoice[5]; // Specify the size of the array
            choices[0] = MenuChoice.CreateNewCustomer;
            choices[1] = MenuChoice.ShowAllCustomers;
            choices[2] = MenuChoice.LockUser;
            choices[3] = MenuChoice.UnlockUser;
            choices[4] = MenuChoice.BackToMainAdmin;
            return choices;
        }
    }
}