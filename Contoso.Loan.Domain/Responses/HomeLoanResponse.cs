using System;
using System.Collections.Generic;
using Visma.Loan.Domain.Enums;
using Visma.Loan.Domain.Interfaces;
using Visma.Loan.Domain.Loans;
using Visma.Loan.Domain.Rules;

namespace Visma.Loan.Domain.Responses
{
    public class HomeLoanResponse
    {
        public HomeLoanResponse()
        {
            BrokenBusinessRules = new List<BusinessRule>();
            PaymentPlan = new List<IPayment>();
        }

        public ServiceStatus Status { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal Principle { get; set; }
        public decimal InterestTotal { get; set; }
        public decimal TotalRepaymentAmount { get; set; }
        public HomeLoan Loan { get; set; }
        public List<IPayment> PaymentPlan { get; set; }
        public List<BusinessRule> BrokenBusinessRules { get; set; }
    }
}
