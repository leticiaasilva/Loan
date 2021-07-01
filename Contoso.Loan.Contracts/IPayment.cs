using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Loan.Contracts
{
    public interface IPayment
    {
        DateTime PaymentDate { get; set; }
        Decimal InterestPaidAmount { get; set; }
        Decimal PrincipalPaidAmount { get; set; }
        Decimal RemainingBalance { get; set; }
    }
}
