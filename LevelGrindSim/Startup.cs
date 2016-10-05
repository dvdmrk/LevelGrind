using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LevelGrindSim.Startup))]
namespace LevelGrindSim
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
