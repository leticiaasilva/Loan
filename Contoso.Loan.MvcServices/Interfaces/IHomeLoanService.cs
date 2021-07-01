using Visma.Loan.MvcServices.RequestResponse;

namespace Visma.Loan.MvcServices.Interfaces
{
    public interface IHomeLoanService
    {
        HomeLoanCreateResponse CreateHomeLoan(HomeLoanCreateRequest request); 
    }
}
