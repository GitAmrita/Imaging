using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FaceDetectionWeb.Startup))]
namespace FaceDetectionWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
