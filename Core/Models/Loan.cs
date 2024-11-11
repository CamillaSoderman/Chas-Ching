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