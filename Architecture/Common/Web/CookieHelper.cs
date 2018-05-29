using System;
using System.Web;

namespace CCT.Common.Web
{
	/// <summary>
	/// CookieHelper ��ժҪ˵����
	/// </summary>
	public static class CookieHelper
	{
		/// <summary>
		/// ��ȡһ��ֵ����ֵָʾ�ͻ���������Ƿ�֧�� Cookie
		/// </summary>
		/// <returns>��/��</returns>
		public static bool IsSupportCookie()
		{
			HttpBrowserCapabilities httpBrowserCapabilities = HttpContext.Current.Request.Browser;
			
			return httpBrowserCapabilities.Cookies;
		}


		/// <summary>
		/// ���� Cookie ֵ
		/// </summary>
		/// <param name="name">Cookie ����</param>
		/// <param name="cookieValue">Cookie ֵ</param>
		public static void SetCookie(string name, string cookieValue)
		{
			HttpContext.Current.Response.Cookies[name].Value = HttpContext.Current.Server.UrlEncode(cookieValue);

			// Cookie ����ʱ��Ĭ����������رպ�15����
			// HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddHours(6);
		}


		/// <summary>
		/// ���� Cookie ֵ
		/// </summary>
		/// <param name="name">Cookie ����</param>
		/// <param name="key">��</param>
		/// <param name="cookieValue">Cookie ֵ</param>
		public static void SetCookie(string name, string key, string cookieValue)
		{
			HttpContext.Current.Response.Cookies[name][key] = HttpContext.Current.Server.UrlEncode(cookieValue);

			// Cookie ����ʱ��Ĭ����������رպ�15����
			// HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddHours(6);
		}


		/// <summary>
		/// ��ȡ Cookie ֵ
		/// </summary>
		/// <param name="name">Cookie ����</param>
		/// <returns>Cookie ֵ</returns>
		public static string GetCookie(string name)
		{
			if ( HttpContext.Current.Request.Cookies[name] == null )
			{
				return null;
			}

			return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[name].Value);
		}


		/// <summary>
		/// ��ȡ Cookie ֵ
		/// </summary>
		/// <param name="name">Cookie ����</param>
		/// <param name="key">��</param>
		/// <returns>Cookie ֵ</returns>
		public static string GetCookie(string name, string key)
		{
			if ( HttpContext.Current.Request.Cookies[name] == null )
			{
				return null;
			}

			return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[name][key]);
		}


		/// <summary>
		/// �Ӽ������Ƴ�����ָ�����Ƶ� Cookie
		/// </summary>
		/// <param name="name">Cookie ����</param>
		public static void Remove(string name)
		{
			if ( HttpContext.Current.Request.Cookies[name] != null )
			{				
				HttpContext.Current.Request.Cookies[name].Expires = DateTime.Now.AddSeconds(-1);
				HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddSeconds(-1);
			}
		}
	}
}
