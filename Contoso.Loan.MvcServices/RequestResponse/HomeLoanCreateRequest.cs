using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visma.Loan.MvcServices.ViewModels;

namespace Visma.Loan.MvcServices.RequestResponse
{
    public class HomeLoanCreateRequest
    {
        public HomeLoanViewModel LoanViewModel { get; set; }
    }
}
