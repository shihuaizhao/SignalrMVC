using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalMvc.Connections
{
    public class ChatConnection : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Send(connectionId, "连接成功，ID:"+ connectionId);
        }

        //连接断开
        //protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        //{
        //    return Connection.Broadcast("ID：" + connectionId + "已退出");
        //}
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var name = "连接ID:" + connectionId + " "+DateTime.Now + " 对大家说：";
            return Connection.Broadcast(name+data);
        }
    }
}