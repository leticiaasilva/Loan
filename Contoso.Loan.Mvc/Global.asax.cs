using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Visma.Loan.Mvc.Configuration;
using Visma.Loan.MvcServices.Interfaces;
using Visma.Loan.MvcServices.Services;
using Visma.Loan.Services;

namespace Visma.Loan.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IContainer Container { get; set; } 

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Autofac - IOC Registration
            var builder = new ContainerBuilder();
             
            builder.RegisterType<LoanService>().As<ILoanService>().InstancePerRequest();
            builder.RegisterType<HomeLoanService>().As<IHomeLoanService>().InstancePerRequest();
            builder.RegisterType<HomeLoanDefaultConfiguration>().As<ILoanDefaultConfiguration>().SingleInstance();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}
