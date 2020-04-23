using Microsoft.Owin;
using Owin;
using SignalMvc.Connections;

[assembly: OwinStartupAttribute(typeof(SignalMvc.Startup))]
namespace SignalMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //注册PersistentConnection （永久连接类）路由
            app.MapSignalR<ChatConnection>("/Connections/ChatConnection");

            //注册Hubs （集线器类）路由
            app.MapSignalR();

            //MVC默认生成的，不需要在意
            ConfigureAuth(app);
        }
    }
}
