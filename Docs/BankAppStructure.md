BankApp
├── Program.cs
├── Core
│   ├── Models
│   │   ├── Account.cs
│   │   ├── Admin.cs
│   │   ├── Customer.cs
│   │   ├── Loan.cs
│   │   ├── SavingsAccount.cs
│   │   ├── Transaction.cs
│   │   ├── TransactionLog.cs
│   │   ├── TransactionScheduler.cs
│   │   ├── User.cs
│   │   └── UserManagement.cs
│   └── Enums
│       ├── AccountType.cs
│       ├── CurrencyType.cs
│       └── TransactionStatus.cs
├── UI
│   ├── Menus
│   │   ├── MainMenu.cs
│   │   ├── CustomerMenu.cs
│   │   ├── AdminMenu.cs
│   │   └── BaseMenu.cs
│   ├── Services
│   │   ├── MenuService.cs
│   │   └── DisplayService.cs
│   ├── Display
│   │   └── AsciiArt.cs
│   └── Settings
│       ├── AppSettings.cs
│       ├── MenuOptions.cs
│       └── UIHelper.cs