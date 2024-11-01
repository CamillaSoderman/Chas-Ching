using Chas_Ching.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models
{
    public class Customer : User
    {
        // Deklarera Accounts som en lista av typen Account
        public List<Account> Accounts { get; set; }
        public decimal Loan { get; set; }

        public Customer(string userName, string password) : base(userName, password)
        {
            // Initiera Accounts-listan
            Accounts = new List<Account>();
            Loan = Loan;
        }
        public void ViewAccounts()
        {
            throw new NotImplementedException();
        }
        public void TransferFounds()
        {
            throw new NotImplementedException();
        }
        public void OpenAccount()
        {
            throw new NotImplementedException();
        }

        public void RequestLoan()
        {
            throw new NotImplementedException();
        }
        public void ViewTransaction()
        {
            throw new NotImplementedException();
        }
    }
}