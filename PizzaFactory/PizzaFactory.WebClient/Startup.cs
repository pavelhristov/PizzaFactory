using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzaFactory.WebClient.Startup))]
namespace PizzaFactory.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
