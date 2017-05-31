using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Votaciones.Startup))]
namespace Votaciones
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
