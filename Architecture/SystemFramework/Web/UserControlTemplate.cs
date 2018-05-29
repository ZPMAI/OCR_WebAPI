using System;

using CCT.SystemFramework.Text;

namespace CCT.SystemFramework.Web
{
	/// <summary>
	/// UserControlTemplate ��ժҪ˵����
	/// </summary>
	public class UserControlTemplate : System.Web.UI.UserControl
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
		public UserControlTemplate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
