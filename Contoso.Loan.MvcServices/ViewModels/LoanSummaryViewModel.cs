using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visma.Loan.Domain.Enums;

namespace Visma.Loan.MvcServices.ViewModels
{
    public class LoanSummaryViewModel
    {
        public LoanSummaryViewModel()
        {
            ScheduledPayments = new List<ScheduledPaymentViewModel>();
        }

        [Display(Name = "Loan Amount:")]
        public decimal LoanAmount { get; set; }

        [Display(Name = "Interest Rate:")]
        public decimal InterestRate { get; set; }

        [Display(Name = "Loan Term:")]
        public int Term { get; set; }

        [Display(Name = "Interest Repayable:")]
        public decimal TotalInterestAmount { get; set; }

        [Display(Name = "Total Repayable:")]
        public decimal TotalRepaymentAmount { get; set; }

        [Display(Name = "Number of Repayments:")]
        public int NumberOfRepayments { get; set; }

        [Display(Name = "Type Loan name:")]
        public LoanType TypeLoan { get; set; }

        public List<ScheduledPaymentViewModel> ScheduledPayments { get; set; }
    }
}