using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Helpr.Startup))]
namespace Helpr
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
