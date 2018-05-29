using System;
using System.Web;

namespace CCT.SystemFramework.Web
{
	/// <summary>
	/// CookieHelper 的摘要说明。
	/// </summary>
	public sealed class CookieHelper
	{
		/// 构造函数
		private CookieHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		/// <summary>
		/// 获取一个值，该值指示客户端浏览器是否支持 Cookie
		/// </summary>
		/// <returns>是/否</returns>
		public static bool IsSupportCookie()
		{
			HttpBrowserCapabilities httpBrowserCapabilities = HttpContext.Current.Request.Browser;
			
			return httpBrowserCapabilities.Cookies;
		}


		/// <summary>
		/// 设置 Cookie 值
		/// </summary>
		/// <param name="name">Cookie 名称</param>
		/// <param name="cookieValue">Cookie 值</param>
		public static void SetCookie(string name, string cookieValue)
		{
			HttpContext.Current.Response.Cookies[name].Value = HttpContext.Current.Server.UrlEncode(cookieValue);

			// Cookie 过期时间默认是浏览器关闭后15分钟
			// HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddHours(6);
		}


		/// <summary>
		/// 设置 Cookie 值
		/// </summary>
		/// <param name="name">Cookie 名称</param>
		/// <param name="key">键</param>
		/// <param name="cookieValue">Cookie 值</param>
		public static void SetCookie(string name, string key, string cookieValue)
		{
			HttpContext.Current.Response.Cookies[name][key] = HttpContext.Current.Server.UrlEncode(cookieValue);

			// Cookie 过期时间默认是浏览器关闭后15分钟
			// HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddHours(6);
		}


		/// <summary>
		/// 获取 Cookie 值
		/// </summary>
		/// <param name="name">Cookie 名称</param>
		/// <returns>Cookie 值</returns>
		public static string GetCookie(string name)
		{
			if ( HttpContext.Current.Request.Cookies[name] == null )
			{
				return null;
			}

			return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[name].Value);
		}


		/// <summary>
		/// 获取 Cookie 值
		/// </summary>
		/// <param name="name">Cookie 名称</param>
		/// <param name="key">键</param>
		/// <returns>Cookie 值</returns>
		public static string GetCookie(string name, string key)
		{
			if ( HttpContext.Current.Request.Cookies[name] == null )
			{
				return null;
			}

			return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[name][key]);
		}


		/// <summary>
		/// 从集会中移除具有指定名称的 Cookie
		/// </summary>
		/// <param name="name">Cookie 名称</param>
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
