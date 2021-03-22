using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebShopping.Application.Startup))]
namespace WebShopping.Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
