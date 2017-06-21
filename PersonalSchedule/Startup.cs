using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonalSchedule.Startup))]
namespace PersonalSchedule
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
