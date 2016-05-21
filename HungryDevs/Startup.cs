using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HungryDevs.Startup))]
namespace HungryDevs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
