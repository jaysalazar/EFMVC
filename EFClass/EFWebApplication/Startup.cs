using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EFWebApplication.Startup))]
namespace EFWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
