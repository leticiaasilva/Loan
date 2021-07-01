using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma.Loan.Domain.Enums;
using Visma.Loan.Domain.Messages;
using Visma.Loan.Domain.Requests;
using Visma.Loan.MvcServices.Extensions;
using Visma.Loan.MvcServices.Interfaces;
using Visma.Loan.MvcServices.RequestResponse;
using Visma.Loan.MvcServices.ViewModels;
using Visma.Loan.Services;

namespace Visma.Loan.MvcServices.Services
{
    public class HomeLoanService : IHomeLoanService
    {
        private readonly ILoanService _loanService = null;

        public HomeLoanService(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public HomeLoanCreateResponse CreateHomeLoan(HomeLoanCreateRequest request)
        {
            var response = new HomeLoanCreateResponse();
 
            var newHomeLoanResponse = _loanService.NewHomeLoan(new HomeLoanServiceRequest()
            {
                Amount = request.LoanViewModel.Amount,
                InterestRate = request.LoanViewModel.InterestRate,
                Term = request.LoanViewModel.Term,
                FirstRepaymentDate = request.LoanViewModel.FirstRepaymentDate
            });

            if (newHomeLoanResponse.Status != ServiceStatus.Success)
            {
                response.ServiceStatus = ServiceStatus.Failure;

                foreach (var message in newHomeLoanResponse.BrokenBusinessRules)
                {
                    response.ValidationMessages.Add(new LoanValidationMessage() {Message = message.Description });
                }
                return response;
            }
             
            response.ServiceStatus = ServiceStatus.Success; 
            response.LoanSummary = new LoanSummaryViewModel
            {
                Term = newHomeLoanResponse.Loan.Term,
                InterestRate = newHomeLoanResponse.Loan.InterestRatePercentage,
                LoanAmount = newHomeLoanResponse.Loan.Principal,
                TotalInterestAmount = newHomeLoanResponse.PaymentPlan.Sum(t => t.InterestPaidAmount),
                TotalRepaymentAmount = newHomeLoanResponse.TotalRepaymentAmount,
                ScheduledPayments = newHomeLoanResponse.PaymentPlan.PaymentPlanToScheduledPaymentPlanViewModel(),
                NumberOfRepayments = newHomeLoanResponse.PaymentPlan != null ? newHomeLoanResponse.PaymentPlan.Count : 0
            };

            return response;
        }
    }
}
