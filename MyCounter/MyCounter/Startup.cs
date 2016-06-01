using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCounter.Startup))]
namespace MyCounter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
