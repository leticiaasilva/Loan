using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Visma.Loan.Domain.Messages;
using Visma.Loan.Domain.Requests;
using Visma.Loan.Domain.Responses;
using Visma.Loan.MvcServices.Services;
using Visma.Loan.Services;
using Visma.Loan.MvcServices.RequestResponse;
using Visma.Loan.Domain.Enums;
using Visma.Loan.Domain.Interfaces;
using Visma.Loan.Domain.Loans;
using Visma.Loan.Domain.Rules;

namespace Visma.Loan.MvcServices.Tests
{
    [TestClass]
    public class HomeLoanServiceTests
    {
        private static Fixture _fixture;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void CreateHomeLoan_WhenInterestRateIsLessThanZero_ReturnsServiceStatusFailure()
        {
            //Arrange  
            var mockLoanServiceResponse = new HomeLoanResponse()
            {
                BrokenBusinessRules = new List<BusinessRule>() { new BusinessRule() { Description = ValidationMessages.InvalidHomeLoanInterestRate } },
                Status = ServiceStatus.Failure
            };

            var homeLoanCreateRequest = _fixture.Build<HomeLoanCreateRequest>().Create();

            var mockLoanService = new Mock<ILoanService>();
            mockLoanService.Setup(s => s.NewHomeLoan(It.IsAny<HomeLoanServiceRequest>())).Returns(mockLoanServiceResponse);

            var homeLoanService = new HomeLoanService(mockLoanService.Object);

            //Act
            var response = homeLoanService.CreateHomeLoan(homeLoanCreateRequest);

            //Assert
            var expectedStatus = ServiceStatus.Failure;
            Assert.AreEqual(expectedStatus, response.ServiceStatus);
            Assert.AreEqual(1, response.ValidationMessages.Count);
            Assert.AreEqual(ValidationMessages.InvalidHomeLoanInterestRate, response.ValidationMessages[0].Message);
        }

        [TestMethod]
        public void CreateHomeLoan_WhenValidLoanRequestCriteriaSupplied_ReturnsServiceStatusSuccessWithPaymentPlan()
        {
            //Arrange  
            var mockLoanServiceResponse = new HomeLoanResponse()
            { 
                Status = ServiceStatus.Success,
                InterestTotal = 100,
                Principle = 2000,
                Loan = _fixture.Build<HomeLoan>().Create(),
                PaymentPlan = new List<IPayment>() { _fixture.Build<MonthlyRepayment>().Create() }
            };

            var homeLoanCreateRequest = _fixture.Build<HomeLoanCreateRequest>().Create();

            var mockLoanService = new Mock<ILoanService>();
            mockLoanService.Setup(s => s.NewHomeLoan(It.IsAny<HomeLoanServiceRequest>())).Returns(mockLoanServiceResponse);

            var homeLoanService = new HomeLoanService(mockLoanService.Object);

            //Act
            var response = homeLoanService.CreateHomeLoan(homeLoanCreateRequest);

            //Assert
            var expectedStatus = ServiceStatus.Success;
            Assert.AreEqual(expectedStatus, response.ServiceStatus);
            Assert.AreEqual(0, response.ValidationMessages.Count);
            Assert.IsNotNull(response.LoanSummary, "Expected to receive details of payment plan for the loan");
            Assert.AreEqual(1, response.LoanSummary.ScheduledPayments.Count, "Expected number of scheduled repayments to be 1");
        }

        [TestMethod]
        public void CreateHomeLoan_WhenLoanTermIsLessThanOrEqualToZero_ReturnsServiceStatusFailure()
        {
            //Arrange  
            var mockLoanServiceResponse = new HomeLoanResponse()
            {
                BrokenBusinessRules = new List<BusinessRule>() { new BusinessRule() {Description = ValidationMessages.InvalidHomeLoanTerm} },
                Status = ServiceStatus.Failure
            };

            var homeLoanCreateRequest = _fixture.Build<HomeLoanCreateRequest>().Create();

            var mockLoanService = new Mock<ILoanService>();
            mockLoanService.Setup(s => s.NewHomeLoan(It.IsAny<HomeLoanServiceRequest>())).Returns(mockLoanServiceResponse);

            var homeLoanService = new HomeLoanService(mockLoanService.Object);  

            //Act
            var response = homeLoanService.CreateHomeLoan(homeLoanCreateRequest);

            //Assert
            var expectedStatus = ServiceStatus.Failure;
            Assert.AreEqual(expectedStatus, response.ServiceStatus);
            Assert.AreEqual(1, response.ValidationMessages.Count);
            Assert.AreEqual(ValidationMessages.InvalidHomeLoanTerm, response.ValidationMessages[0].Message);
        }


        [TestMethod]
        public void CreateHomeLoan_WhenPrincipalAmountIsLessThanOrEqualToZero_ReturnsServiceStatusFailure()
        {
            //Arrange  
            var mockLoanServiceResponse = new HomeLoanResponse()
            {
                BrokenBusinessRules = new List<BusinessRule>() { new BusinessRule() { Description = ValidationMessages.InvalidHomeLoanAmount } },
                Status = ServiceStatus.Failure
            };

            var homeLoanCreateRequest = _fixture.Build<HomeLoanCreateRequest>().Create();

            var mockLoanService = new Mock<ILoanService>();
            mockLoanService.Setup(s => s.NewHomeLoan(It.IsAny<HomeLoanServiceRequest>())).Returns(mockLoanServiceResponse);

            var homeLoanService = new HomeLoanService(mockLoanService.Object);

            //Act
            var response = homeLoanService.CreateHomeLoan(homeLoanCreateRequest);

            //Assert
            var expectedStatus = ServiceStatus.Failure;
            Assert.AreEqual(expectedStatus, response.ServiceStatus);
            Assert.AreEqual(1, response.ValidationMessages.Count);
            Assert.AreEqual(ValidationMessages.InvalidHomeLoanAmount, response.ValidationMessages[0].Message);
        }
    }
}
