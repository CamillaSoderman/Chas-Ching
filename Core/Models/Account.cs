using Chas_Ching.Core.Enums;
using Chas_Ching.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models
{
    public class Account : IAccount
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public CurrencyType Currency { get; set; }

        public Account(int accountNumber, decimal initialBalance, CurrencyType currency)
        {
            AccountId = accountNumber;
            Balance = initialBalance;
            Currency = currency;
        }
        public void GetBalance()
        {
            throw new NotImplementedException();
        }
        public void Deposit()
        {
            throw new NotImplementedException();
        }
        public void Withdraw()
        {
            throw new NotImplementedException();
        }
        public void Transfer()
        {
            throw new NotImplementedException();
        }
        public void Saldo()
        {
            throw new NotImplementedException();
        }
    }
}