using System;

using CCT.SystemFramework.Text;

namespace CCT.SystemFramework.Web
{
	/// <summary>
	/// PageTemplate ��ժҪ˵����
	/// </summary>
	public class PageTemplate : System.Web.UI.Page
	{
		/// ��˾ID
		public int CompanyID
		{			
			get { return TextHelper.ConvertToInt(CookieHelper.GetCookie(Constants.COOKIENAME, "CompanyID")); }
		}

		/// ��˾����
		public string CompanyNo
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "CompanyNo"); }
		}

		/// ��˾����
		public string CompanyName
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "CompanyName"); }
		}

		/// ��˾���� ID������˾���ϳ���˾��������
		public byte CompanyTypeID
		{
			get { return TextHelper.ConvertToByte(CookieHelper.GetCookie(Constants.COOKIENAME, "CompanyTypeID")); }
		}

		/// �û�ID
		public int UserinfoID
		{
			get { return TextHelper.ConvertToInt(CookieHelper.GetCookie(Constants.COOKIENAME, "UserinfoID")); }
		}

		/// �û���
		public string Username
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "Username"); }
		}

		/// ������
		public string ChineseName
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "ChineseName"); }
		}

		/// ����
		public string Email
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "Email"); }
		}

		/// �ֻ�
		public string Handset
		{
			get { return CookieHelper.GetCookie(Constants.COOKIENAME, "Handset"); }
		}

		/// �û�Ȩ��
		public byte[] Permission
		{			
			get { return Convert.FromBase64String(CookieHelper.GetCookie(Constants.COOKIENAME, "Permission")); }
		}

		/// �û����� ID������Ա����ͨ�û���
		public byte UserTypeID
		{
			get { return TextHelper.ConvertToByte(CookieHelper.GetCookie(Constants.COOKIENAME, "UserTypeID")); }
		}


		/// ���캯��
		public PageTemplate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		protected override void OnLoad(EventArgs e)
		{
			if ( ! IsLogin() )
			{
				Response.Redirect(String.Format("/Portal/CN/Login.aspx?Location={0}", Request.RawUrl));
			}

			if ( ! CheckModuleRight() )
			{
				Response.Redirect("/Portal/Error.aspx");
			}

			base.OnLoad (e);
		}


		/// <summary>
		/// ����û��Ƿ��½
		/// </summary>
		/// <returns>��½���</returns>
		public virtual bool IsLogin()
		{
			return ( ( CookieHelper.GetCookie(Constants.COOKIENAME, "CompanyID") != null )
				&& ( CookieHelper.GetCookie(Constants.COOKIENAME, "UserinfoID") != null ) );
		}


		/// <summary>
		/// ���ģ��Ȩ��
		/// </summary>
		/// <returns>��/��</returns>
		public virtual bool CheckModuleRight()
		{
			return true;
		}
	}
}
