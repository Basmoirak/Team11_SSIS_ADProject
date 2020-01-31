using IdentityManager.Configuration;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(Team11_SSIS_ADProject.Startup))]
namespace Team11_SSIS_ADProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureOAuth(app);
        }
    }
}
