using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models
{
    public class Loan
    {
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }

        public void CalculateInterest()
        {
            throw new NotImplementedException();
        }
    }
}