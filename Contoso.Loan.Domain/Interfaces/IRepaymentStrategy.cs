using System;
using System.Collections.Generic;
using Visma.Loan.Domain.Loans;

namespace Visma.Loan.Domain.Interfaces
{ 
    public interface IRepaymentStrategy
    {
        decimal GetRepaymentsFor(decimal principal, decimal interestRate, int term);
        IEnumerable<IPayment> GetRepaymentSchedule(DateTime firstPaymentDate, decimal principal, decimal interestRatePercentage, int term);
    }
}
