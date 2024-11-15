# Chas-Ching
Chas-Ching/
├── Chas-Ching.sln             # Solution file
├── Program.cs                 # Entry point of the application
├── Dockerfile                 # Docker configuration
├── .dockerignore
├── .gitattributes
├── .gitignore
├── README.md                  # Project documentation
├── bin/                       # Compiled binaries
├── obj/                       # Object files
├── Dependencies/              # Project dependencies
├── Imports/                   # External imports
├── Properties/
│   └── launchSettings.json    # Launch configurations
├── Core/
│   ├── Enums/
│   │   ├── AccountType.cs         # Types of bank accounts
│   │   ├── CurrencyType.cs        # Supported currencies
│   │   └── TransactionStatus.cs   # Status of transactions
│   └── Models/
│       ├── Account.cs             # Account base class
│       ├── Admin.cs               # Admin user model
│       ├── CurrencyExchange.cs    # Currency exchange logic
│       ├── Customer.cs            # Customer user model
│       ├── SavingsAccount.cs      # Savings account model
│       ├── Transaction.cs         # Transaction model
│       ├── TransactionLog.cs      # Logging transactions
│       ├── TransactionScheduler.cs# Scheduling future transactions
│       ├── User.cs                # User base class
│       └── UserManagement.cs      # Managing user accounts
├── Docs/
│   ├── BankAppStructure.md        # Documentation on app structure
│   ├── ManualForSpectre.md        # Manual for Spectre.Console usage
│   └── Utvecklingschecklista.md   # Development checklist
├── UI/
│   ├── Display/
│   │   └── AsciiArt.cs            # ASCII art for UI
│   ├── Menus/
│   │   ├── AdminMenu.cs           # Menu for admin users
│   │   ├── CustomerMenu.cs        # Menu for customer users
│   │   └── MainMenu.cs            # Main menu interface
│   ├── Services/
│   │   └── DisplayService.cs      # Services for display operations
│   ├── Settings/
│   │   ├── AppSettings.cs         # Application settings
│   │   ├── MenuText.cs            # Text content for menus
│   │   └── UIHelper.cs            # Helper methods for UI
└── README.md                      # This file
