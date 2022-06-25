using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AGDiamond.Startup))]
namespace AGDiamond
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
