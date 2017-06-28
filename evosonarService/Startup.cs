using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(evosonarService.Startup))]

namespace evosonarService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}