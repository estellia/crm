using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAPI.Test.Startup))]
namespace WebAPI.Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
