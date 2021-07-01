using Visma.Loan.Domain.Requests;
using Visma.Loan.Domain.Responses;

namespace Visma.Loan.Services
{
    public interface ILoanService
    {
        HomeLoanResponse NewHomeLoan(HomeLoanServiceRequest serviceRequest);
    }
}
