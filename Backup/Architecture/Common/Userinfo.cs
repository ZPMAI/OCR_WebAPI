using System;
using System.Collections.Generic;
using System.Text;

namespace CCT.Common
{
    /// <summary>
    /// Userinfo 的摘要说明。
    /// </summary>
    public static class Userinfo
    {
        private static int id;
        private static string username;
        private static byte[] permission;

        public static int ID
        {
            get { return id; }
            set { id = value; }
        }

        public static string Username
        {
            get { return username; }
            set { username = value; }
        }

        public static byte[] Permission
        {
            get { return permission; }
            set { permission = value; }
        }
    }
}
