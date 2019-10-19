using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SheilaWard_FinancialPortal.Startup))]
namespace SheilaWard_FinancialPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
