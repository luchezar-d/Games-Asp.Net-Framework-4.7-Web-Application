using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewUniversityProejct.Startup))]
namespace NewUniversityProejct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
