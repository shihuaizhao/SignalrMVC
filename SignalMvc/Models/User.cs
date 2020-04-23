using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalMvc.Models
{
    public class User
    {
        // 连接ID
        public string ConnectionID { get; set; }

        // 用户名称
        public string Name { get; set; }

        public User(string connectionId,string name)
        {
            this.Name = name;
            this.ConnectionID = connectionId;
        }
    }
}