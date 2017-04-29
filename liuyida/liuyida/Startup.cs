using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(liuyida.Startup))]
namespace liuyida
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
