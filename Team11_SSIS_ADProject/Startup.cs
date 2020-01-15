using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Team11_SSIS_ADProject.Startup))]
namespace Team11_SSIS_ADProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
