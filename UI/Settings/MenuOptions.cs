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
        MakeTransaction,
        ApplyForLoan,
        ExchangeCurrency,
        BackToMain,

        // Admin Menu
        ShowAllAccounts,
        LockUser,
        UnlockUser
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
    { MenuChoice.MakeTransaction, "Gör en Transaktion" },
    { MenuChoice.ApplyForLoan, "Ansök om Lån" },
    { MenuChoice.ExchangeCurrency, "Valutaväxling" },
    { MenuChoice.BackToMain, "Återvänd till Huvudmeny" },

    // Admin menu  
    { MenuChoice.ShowAllAccounts, "Lista alla konton" },
    { MenuChoice.LockUser, "Lås användare" },
    { MenuChoice.UnlockUser, "Lås upp användare" }
};

        // Retrieves the display text associated with a specific menu choice.
        public static string GetText(MenuChoice choice) => Texts[choice];

        // Returns an array of main menu options as MenuChoice items.
        // Returns an array of main menu options as MenuChoice items.
        public static MenuChoice[] GetMainMenuChoices()
        {
            MenuChoice[] choices = new MenuChoice[4]; // Specify the size of the array
            choices[0] = MenuChoice.CustomerLogin;
            choices[1] = MenuChoice.AdminLogin;
            choices[2] = MenuChoice.CreateNewAccount;
            choices[3] = MenuChoice.Exit;
            return choices;
        }

        // Returns an array of customer menu options as MenuChoice items.
        public static MenuChoice[] GetCustomerMenuChoices()
        {
            MenuChoice[] choices = new MenuChoice[5]; // Specify the size of the array
            choices[0] = MenuChoice.ShowAccount;
            choices[1] = MenuChoice.MakeTransaction;
            choices[2] = MenuChoice.ApplyForLoan;
            choices[3] = MenuChoice.ExchangeCurrency;
            choices[4] = MenuChoice.BackToMain;
            return choices;
        }

        // Returns an array of admin menu options as MenuChoice items.
        public static MenuChoice[] GetAdminMenuChoices()
        {
            MenuChoice[] choices = new MenuChoice[4]; // Specify the size of the array
            choices[0] = MenuChoice.ShowAllAccounts;
            choices[1] = MenuChoice.LockUser;
            choices[2] = MenuChoice.UnlockUser;
            choices[3] = MenuChoice.BackToMain; // Add BackToMain for admin menu options as well
            return choices;
        }
    }
}