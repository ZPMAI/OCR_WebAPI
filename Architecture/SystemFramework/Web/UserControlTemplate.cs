using System;

using CCT.SystemFramework.Text;

namespace CCT.SystemFramework.Web
{
	/// <summary>
	/// UserControlTemplate 的摘要说明。
	/// </summary>
	public class UserControlTemplate : System.Web.UI.UserControl
	{
		/// 公司ID
		public int CompanyID
		{			
			get { return TextHelper.ConvertToInt(CookieHelper.GetCookie(Constants.COOKIENAME, "CompanyID")); }
		}

		/// 公司代码
		public string CompanyNo
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "CompanyNo"); }
		}

		/// 公司名称
		public string CompanyName
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "CompanyName"); }
		}

		/// 公司类型 ID（船公司、拖车公司、货主）
		public byte CompanyTypeID
		{
			get { return TextHelper.ConvertToByte(CookieHelper.GetCookie(Constants.COOKIENAME, "CompanyTypeID")); }
		}

		/// 用户ID
		public int UserinfoID
		{
			get { return TextHelper.ConvertToInt(CookieHelper.GetCookie(Constants.COOKIENAME, "UserinfoID")); }
		}

		/// 用户名
		public string Username
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "Username"); }
		}

		/// 中文名
		public string ChineseName
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "ChineseName"); }
		}

		/// 邮箱
		public string Email
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "Email"); }
		}

		/// 手机
		public string Handset
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "Handset"); }
		}

		/// 用户权限
		public byte[] Permission
		{
			get { return Convert.FromBase64String(CookieHelper.GetCookie(Constants.COOKIENAME, "Permission")); }
		}

		/// 用户类型 ID（管理员、普通用户）
		public byte UserTypeID
		{
			get { return TextHelper.ConvertToByte(CookieHelper.GetCookie(Constants.COOKIENAME, "UserTypeID")); }
		}


		/// 构造函数
		public UserControlTemplate()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
