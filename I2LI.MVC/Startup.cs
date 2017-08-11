using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(I2LI.MVC.Startup))]
namespace I2LI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
