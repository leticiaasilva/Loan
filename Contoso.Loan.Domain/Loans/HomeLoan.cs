using System;
using System.Collections.Generic;
using Visma.Loan.Domain.Enums;
using Visma.Loan.Domain.Interfaces;
using Visma.Loan.Domain.Messages;
using Visma.Loan.Domain.RepaymentStrategies;
using Visma.Loan.Domain.Rules;

namespace Visma.Loan.Domain.Loans
{
    public class HomeLoan : BankLoanBase
    {           
        public HomeLoan(decimal principal, int term, decimal interestRatePercentage, CurrencyType currency, LoanType loanType) : base(principal, term, interestRatePercentage, currency, loanType)
        {
        }
         
        protected override IRepaymentStrategy CreateRepaymentStrategy()
        {
           return new MonthlyRepaymentStrategy();
        }

        public decimal PropertyValue { get; set; }

        public decimal Deposit { get; set; }

        protected override void CheckForBrokenBusinessRulesOnThisLoan()
        {
            if (Principal <= 0)
            {
                _businessRules.Add(new BusinessRule() { Name = "Principal", Description = ValidationMessages.InvalidHomeLoanAmount });
            }

            if (Term <= 0)
            {
                _businessRules.Add(new BusinessRule() { Name = "Term", Description = ValidationMessages.InvalidHomeLoanTerm });
            }

            if (InterestRatePercentage < 0)
            {
                _businessRules.Add(new BusinessRule() { Name = "Interest Rate", Description = ValidationMessages.InvalidHomeLoanInterestRate });
            } 

        }
    }
}
