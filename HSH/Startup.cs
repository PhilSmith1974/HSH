using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HSH.Startup))]
namespace HSH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
