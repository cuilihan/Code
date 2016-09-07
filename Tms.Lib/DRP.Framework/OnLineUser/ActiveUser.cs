using System;
using DRP.Framework.Core;

namespace DRP.Framework.OnLineUser
{
    public sealed class ActiveUser
    {
        private readonly string _ticket;     //票据名称
        private readonly string _username;    //登陆用户名 
        private readonly DateTime _refreshtime;   //最新刷新时间
        private readonly DateTime _activetime;   //最新活动时间
        private readonly string _clientip;    //登陆IP
        private readonly DateTime _logintime;

        public ActiveUser(string Ticket, string UserName)
        {
            _ticket = Ticket;
            _username = UserName;            
            _refreshtime = DateTime.Now;
            _activetime = DateTime.Now;
            _logintime = DateTime.Now;
            _clientip = IP.GetClientIp();
        }      

        public string Ticket { get { return _ticket; } }
        public string UserName { get { return _username; } }     
        public DateTime RefreshTime { get { return _refreshtime; } }
        public DateTime ActiveTime { get { return _activetime; } }
        public string ClientIP { get { return _clientip; } }
        public DateTime LoginTime { get { return _logintime; } }
    }
}
