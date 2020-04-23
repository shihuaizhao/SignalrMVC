using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using SignalMvc.Models;
using Newtonsoft.Json;

namespace SignalMvc.ChatHubs
{
    [HubName("serverHub")]
    public class ServerHub : Hub
    {
        //初始化用户列表
        private static List<User> userList = new List<User>();

        // 重写连接事件
        public override Task OnConnected()
        {
            //查询用户
            var user = userList.Where(u => u.ConnectionID == Context.ConnectionId).SingleOrDefault();
            //判断用户是否存在，否则添加集合
            if (user == null)
            {
                user = new User( Context.ConnectionId,"");
                userList.Add(user);
            }
            //返回连接状态
            Clients.Client(Context.ConnectionId).status(user.ConnectionID);
            GetUserList();//获取所有用户的列表
            return base.OnConnected();
        }

        // 重写退出事件
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = userList.Where(u => u.ConnectionID == Context.ConnectionId).FirstOrDefault();
            //判断用户是否存在，存在则删除
            if (user != null)
            {
                //删除用户
                userList.Remove(user);
            }
            GetUserList();//获取所有用户的列表
            return base.OnDisconnected(stopCalled);
        }

        //获取所有用户在线列表
        public void GetUserList()
        {
            var list = userList.Select(u => new { u.ConnectionID,u.Name }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            //调用客户端方法
            Clients.All.getusers(jsonList);
        }


        //供客户端调用的服务器端代码
        [HubMethodName("Send")]
        public void Send(string message)
        {
            var name = DateTime.Now+ " 对大家说：";

            // 调用所有客户端的sendMessage方法
            //单推
            Clients.Client(Context.ConnectionId).showMessage("连接ID:"+ Context.ConnectionId);
            //群推
            Clients.All.sendMessage(name, message);
        }

        //供客户端调用的服务器端代码
        [HubMethodName("SendOne")]
        public void SendOne(string message, string connectionid)
        {
            var user = userList.Where(s => s.ConnectionID == connectionid).FirstOrDefault();
            if (user != null)
            {
                //回调客户端方法（单发）
                Clients.Client(connectionid).SendMessage("ID:" + connectionid, message + " 时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //给自己发送，把用户的ID传给自己
                //Clients.Client(Context.ConnectionId).addMessage(message + "" + DateTime.Now, connectionid);
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("该用户已离线...");
            }
        }

        //供客户端调用的服务器端代码
        [HubMethodName("SendAll")]
        public void SendAll(string message, string connectionid)
        {

            if (!string.IsNullOrEmpty(connectionid))
            {
                //回调客户端方法（广播）
                Clients.All.SendMessage("ID:" + connectionid, message + " 时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
    }
}