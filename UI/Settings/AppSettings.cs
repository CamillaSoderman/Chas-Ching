namespace Chas_Ching.UI.Settings
{
    /// <summary>
    /// Nested Class (Colors and Messages) to hold application-wide settings for colors and messages used in the UI.
    /// Storing constants makes it easy to change colors or messages in one place and keeps the code organized.
    /// Static class belong to the class itself. No instance of the class is needed to access its members.
    /// </summary>
    public static class AppSettings
    {
        public static class Colors
        {
            public const string Primary = "blue";
            public const string Success = "green";
            public const string Error = "red";
            public const string Warning = "yellow";
            public const string Neutral = "grey";
        }
        public static class Messages
        {
            public const string ContinuePrompt = "Tryck Enter för att fortsätta...";
            public const string ExitMessage = "Avslutar... Tack för att du använde Chas Ching!";
            public const string ReturnMessage = "Återvänder till huvudmenyn...";
        }
    }
}