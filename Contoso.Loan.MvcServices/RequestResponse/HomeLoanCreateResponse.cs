using System;
using System.Collections.Generic;
using Visma.Loan.Domain.Enums;
using Visma.Loan.Domain.Messages;
using Visma.Loan.MvcServices.ViewModels;

namespace Visma.Loan.MvcServices.RequestResponse
{
    public class HomeLoanCreateResponse
    {
        public HomeLoanCreateResponse()
        {
            ServiceStatus = new ServiceStatus();
            ValidationMessages = new List<LoanValidationMessage>();
        }
        public LoanSummaryViewModel LoanSummary { get; set; }

        public Guid LoanId { get; set; }

        public ServiceStatus ServiceStatus { get; set; }
        public List<LoanValidationMessage> ValidationMessages { get; set; }
    } 
}
