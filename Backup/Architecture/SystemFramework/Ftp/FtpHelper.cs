using System;
using System.Runtime.InteropServices;

using CCT.SystemFramework.Configuration;

namespace CCT.SystemFramework.Ftp
{
    /// <summary>
    /// FtpHelper 的摘要说明。
    /// </summary>
    public sealed class FtpHelper
    {
        [DllImport("wininet.dll", EntryPoint = "InternetOpen", SetLastError = true)]
        private extern static IntPtr InternetOpen(string szAgentName, int dwAccessType, string szProxyName, string szProxyBypass, int dwFlags);

        [DllImport("wininet.dll", EntryPoint = "InternetConnect", SetLastError = true)]
        private extern static IntPtr InternetConnect(IntPtr HINTERNET, string pszServerName, IntPtr nServerPort, string lpszUserName, string lpszPassword, int dwService, int dwFlags, int dwContext);

        [DllImport("wininet.dll", EntryPoint = "InternetCloseHandle", SetLastError = true)]
        private extern static bool InternetCloseHandle(IntPtr hInet);

        [DllImport("wininet.dll", EntryPoint = "FtpGetFile", SetLastError = true)]
        private extern static bool FtpGetFile(IntPtr hFtpSession, string lpszRemoteFile, string lpszNewFile, bool fFailIfExists, int dwFlagsAndAttributes, int dwFlags, int dwContext);

        [DllImport("wininet.dll", EntryPoint = "FtpPutFile", SetLastError = true)]
        private extern static bool FtpPutFile(IntPtr hConnect, string lpszLocalFile, string lpszNewRemoteFile, int dwFlags, int dwContext);

        [DllImport("wininet.dll", EntryPoint = "FtpRenameFile", SetLastError = true)]
        private extern static bool FtpRenameFile(IntPtr hFtpSession, string lpszExistingFile, string lpszNewFile);

        private string serverName;
        private string username;
        private string password;

        /// 构造函数
        public FtpHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.serverName = ConfigurationHelper.Section["FTP"]["ServerName"].ToString();
            this.username = ConfigurationHelper.Section["FTP"]["Username"].ToString();
            this.password = ConfigurationHelper.Section["FTP"]["Password"].ToString();
        }


        /// 构造函数
        public FtpHelper(string serverName, string username, string password)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.serverName = serverName;
            this.username = username;
            this.password = password;
        }


        /// <summary>
        ///上传本地文件
        /// </summary>
        /// <param name="localFile">本地文件</param>
        /// <param name="remoteFile">远程文件</param>
        public void Upload(string localFile, string remoteFile)
        {
            const int accessType = 1;
            const int openFlags = 0;
            const int service = 1;
            const int connectFlags = 0;
            const int connectContext = 0;
            const int putFileFlags = 1;
            const int putFileContext = 0;
            const string agentName = "FTP";
            const string proxyName = null;
            const string proxyBypass = null;

            IntPtr port = (IntPtr)21;
            IntPtr internetOpen = InternetOpen(agentName, accessType, proxyName, proxyBypass, openFlags);
            IntPtr internetConnect = InternetConnect(internetOpen, this.serverName, port, this.username, this.password, service, connectFlags, connectContext);

            try
            {
                FtpPutFile(internetConnect, localFile, remoteFile, putFileFlags, putFileContext);
            }
            catch (Exception e)
            {
                string message = string.Format("FTP 文件上传不成功，文件名：{0}\r\n请核查 FTP 服务器状态，服务器名：{1}。\r\n{2}", localFile, this.serverName, e.Message);
                throw new FtpException(message);
            }
            finally
            {
                InternetCloseHandle(internetConnect);
                InternetCloseHandle(internetOpen);
            }
        }
    }
}
