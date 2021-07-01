using System;
using System.Linq;
using Visma.Loan.Domain.Enums;
using Visma.Loan.Domain.Loans;
using Visma.Loan.Domain.Requests;
using Visma.Loan.Domain.Responses;  

namespace Visma.Loan.Services
{
    public class LoanService : ILoanService
    {
        public HomeLoanResponse NewHomeLoan(HomeLoanServiceRequest request)
        {
            var response = new HomeLoanResponse();
 
            var newLoan = new HomeLoan(request.Amount, request.Term, request.InterestRate, request.Currency, request.LoanType);

            if (newLoan.IsValidLoan())
            { 
                response.Loan = newLoan;
                response.PaymentPlan = newLoan.GetRepaymentSchedule(request.FirstRepaymentDate).ToList();
                response.TotalRepaymentAmount = (Decimal.Round(newLoan.GetRepaymentAmount(), 2) * request.Term * 12);
                response.Status = ServiceStatus.Success;
            }
            else
            {
                response.Status = ServiceStatus.Failure;
                response.BrokenBusinessRules = newLoan.GetBrokenBusinessRulesOnThisLoan();
            }

            return response;
        }
    }
}
