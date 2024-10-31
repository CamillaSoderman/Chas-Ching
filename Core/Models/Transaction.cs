
using Chas_Ching.Core.Enums;
using Chas_Ching.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models

{
    public class Transaction : ITransactions
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public DateTime Date { get; set; }
        public void CreateTransactions()
        {
            throw new NotImplementedException();
        }
    }
}