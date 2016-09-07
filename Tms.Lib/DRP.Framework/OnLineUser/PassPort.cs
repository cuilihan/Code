using System;
using System.Data;
using System.Configuration;
using DRP.Framework.Core;
using DRP.Framework;

namespace DRP.Framework.OnLineUser
{
    public class PassPort
    {
        private static DataTable _activeusers;
        private readonly int _activeTimeout;
        private readonly int _refreshTimeout;
        private object obj = new object();

        /// <summary>
        /// 初始化在线用户表。
        /// </summary> 
        private static void UserstableFormat()
        {
            if (_activeusers != null) return;
            _activeusers = new DataTable("ActiveUsers");
            var mystringtype = Type.GetType("System.String");
            var mytimetype = Type.GetType("System.DateTime");
            var myDataColumn = new DataColumn("Ticket", mystringtype);
            _activeusers.Columns.Add(myDataColumn);
            myDataColumn = new DataColumn("UserName", mystringtype);
            _activeusers.Columns.Add(myDataColumn);
            myDataColumn = new DataColumn("RefreshTime", mytimetype);
            _activeusers.Columns.Add(myDataColumn);
            myDataColumn = new DataColumn("LoginTime", mytimetype);
            _activeusers.Columns.Add(myDataColumn);
            myDataColumn = new DataColumn("ActiveTime", mytimetype);
            _activeusers.Columns.Add(myDataColumn);
            myDataColumn = new DataColumn("ClientIP", mystringtype);
            _activeusers.Columns.Add(myDataColumn);
        }

        public PassPort()
        {
            UserstableFormat(); //初始化在线用户表

            //活动超时时间初始化 单位：分钟
            _activeTimeout = 60;

            //自动刷新超时时间初始化 单位：分钟
            _refreshTimeout = 5;
        }

        /// <summary>
        /// 全部用户列表
        /// </summary>
        public DataTable ActiveUsers
        {
            get { return _activeusers.Copy(); }
        }

        /// <summary>
        /// 在线用户总人数
        /// </summary>
        /// <returns></returns>
        public int GetOnLineUserCounter()
        {
            return _activeusers.Rows.Count;
        }

        /// <summary>
        /// 新用户登陆。
        /// </summary>
        public void Login(ActiveUser user, bool singleLogin = true)
        {
            DelTimeOut();   //清除超时用户
            if (singleLogin)
            {
                //若是单人登陆则注销原来登陆的用户
                Logout(user.UserName, false);
            }
            try
            {
                var myRow = _activeusers.NewRow();
                myRow["Ticket"] = user.Ticket.Trim();
                myRow["UserName"] = user.UserName.Trim();
                myRow["ActiveTime"] = DateTime.Now;
                myRow["RefreshTime"] = DateTime.Now;
                myRow["LoginTime"] = DateTime.Now;
                myRow["ClientIP"] = user.ClientIP.Trim();
                _activeusers.Rows.Add(myRow);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
            _activeusers.AcceptChanges();

        }

        /// <summary>
        ///用户注销，根据Ticket或UserName。
        /// </summary> 
        private void Logout(string strUserKey, bool byTicket)
        {
            DelTimeOut();   //清除超时用户
            strUserKey = strUserKey.Trim();
            var strExpr = byTicket ? "Ticket='" + strUserKey + "'" : "UserName='" + strUserKey + "'";
            var curUser = _activeusers.Select(strExpr);
            if (curUser.Length > 0)
            {
                foreach (var t in curUser)
                {
                    t.Delete();
                }
            }
            _activeusers.AcceptChanges();
        }

        /// <summary>
        ///用户注销，根据Ticket。
        /// </summary>
        /// <param name="strTicket">要注销的用户Ticket</param>
        public void Logout(string strTicket)
        {
            Logout(strTicket, true);
        }

        /// <summary>
        ///清除超时用户。
        /// </summary>
        private void DelTimeOut()
        {
            var strExpr = "ActiveTime < '" + DateTime.Now.AddMinutes(0 - _activeTimeout) + "'or RefreshTime < '" + DateTime.Now.AddMinutes(0 - _refreshTimeout) + "'";
            var curUser = _activeusers.Select(strExpr);
            if (curUser.Length > 0)
            {
                foreach (var t in curUser)
                {
                    t.Delete();
                }
            }
            _activeusers.AcceptChanges();
            return;
        }

        /// <summary>
        ///更新用户活动时间。
        /// </summary>
        private void ActiveTime(string strTicket)
        {
            lock (obj)
            {
                DelTimeOut();
                var strExpr = "Ticket='" + strTicket + "'";
                var curUser = _activeusers.Select(strExpr);
                if (curUser.Length > 0)
                {
                    foreach (var t in curUser)
                    {
                        t["ActiveTime"] = DateTime.Now;
                        t["RefreshTime"] = DateTime.Now;
                    }
                }
                _activeusers.AcceptChanges();
            }
        }

        /// <summary>
        ///更新系统自动刷新时间。
        /// </summary>
        public void RefreshTime(string strTicket)
        {
            ActiveTime(strTicket);
        }

        /// <summary>
        /// 取具体一个在线用户
        /// </summary>
        /// <param name="qryKey"></param>
        /// <param name="isByTicket"></param>
        /// <returns></returns>
        private static ActiveUser SingleUser(string qryKey, bool isByTicket)
        {
            var strExpr = isByTicket ? "Ticket='" + qryKey.Trim() + "'" : "UserName='" + qryKey.Trim() + "'";
            var rows = _activeusers.Select(strExpr);
            if (rows.Length == 0) return null;
            var myTicket = (string)rows[0]["Ticket"];
            var myName = (string)rows[0]["UserName"];
            var myActiveTime = (DateTime)rows[0]["ActiveTime"];
            var myRefreshtime = (DateTime)rows[0]["RefreshTime"];
            var myClientIp = (string)rows[0]["ClientIP"];
            return new ActiveUser(myTicket, myName);
        }

        /// <summary>
        ///按Ticket获取活动用户。
        /// </summary>
        public ActiveUser SingleUserByTicket(string key)
        {
            return SingleUser(key, true);
        }

        /// <summary>
        ///按UserName获取活动用户。
        /// </summary>
        public ActiveUser SingleUserByUserName(string key)
        {
            return SingleUser(key, false);
        }

        /// <summary>
        ///按Ticket判断用户是否在线。
        /// </summary>
        public bool IsOnlineByTicket(string key)
        {
            return SingleUser(key, true) != null;
        }

        /// <summary>
        ///按UserName判断用户是否在线。
        /// </summary>
        public bool IsOnlineByUserName(string key)
        {
            return SingleUser(key, false) != null;
        }
    }
}
