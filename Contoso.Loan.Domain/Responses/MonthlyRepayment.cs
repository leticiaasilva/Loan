using System;
using Visma.Loan.Domain.Interfaces;

namespace Visma.Loan.Domain.Responses
{
    public class MonthlyRepayment : IPayment
    {
        public MonthlyRepayment(DateTime paymentDate, decimal principalPaidAmount, decimal interestPaidAmount, decimal remainingBalance)
        {
            PaymentDate = paymentDate;
            PrincipalPaidAmount = principalPaidAmount;
            InterestPaidAmount = interestPaidAmount;
            RemainingBalance = remainingBalance;
        }
        public decimal InterestPaidAmount
        {
            get;set;
        }

        public decimal PrincipalPaidAmount
        {
            get; set;
        }

        public decimal RemainingBalance
        {
            get;set;
        }

        public DateTime PaymentDate
        {
            get;set;
        } 
    }
}
