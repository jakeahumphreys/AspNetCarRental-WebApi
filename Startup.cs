using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartupAttribute(typeof(EIRLSS_Data_API.Startup))]
namespace EIRLSS_Data_API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigureAuth(app);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}
