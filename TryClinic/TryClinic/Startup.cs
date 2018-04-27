using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TryClinic.Startup))]
namespace TryClinic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
