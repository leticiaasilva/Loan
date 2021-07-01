using System;
using Visma.Loan.Domain.Enums;

namespace Visma.Loan.Domain.Requests
{
    public class HomeLoanServiceRequest
    { 
        public CurrencyType Currency { get; set; }
        public LoanType LoanType { get; set; }
        public decimal Amount { get; set; }
        public int Term { get; set; }
        public decimal InterestRate { get; set; }

        public DateTime FirstRepaymentDate { get; set; }
    }
}
