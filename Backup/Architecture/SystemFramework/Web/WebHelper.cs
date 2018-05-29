using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CCT.SystemFramework.Text;

namespace CCT.SystemFramework.Web
{
	/// <summary>
	/// WebHelper ��ժҪ˵����
	/// </summary>
	public sealed class WebHelper
	{
		/// ˽�й��캯��
		private WebHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ��Ϣ��ʾ����

		/// <summary>
		/// ˢ��ҳ��
		/// </summary>
		/// <param name="sourcePage">ҳ��</param>
		public static void Refresh(Page page)
		{
			page.RegisterStartupScript(Guid.NewGuid().ToString().Trim(), "<script language='javascript'>window.location.href = window.location.href;</script>");
		}


		/// <summary>
		/// ����ˢ�¸�ҳ��
		/// </summary>
		/// <param name="sourcePage">ҳ��</param>
		public static void SetRefresh(Page page)
		{
			page.RegisterStartupScript(Guid.NewGuid().ToString().Trim(), "<script language='javascript'>window.returnValue=0;</script>");
		}


		/// <summary>
		/// ˢ�¸�ҳ��
		/// </summary>
		/// <param name="sourcePage">ҳ��</param>
		public static void RefreshParent(Page page)
		{
			// �� javascript:ShowMessageAndRefresh �ϲ�ʹ��
			page.RegisterStartupScript(Guid.NewGuid().ToString().Trim(), "<script language='javascript'>opener=null;window.returnValue=0;window.close();</script>");
		}


		/// <summary>
		/// ת����ָ��ҳ��
		/// </summary>
		/// <param name="sourcePage">ҳ��</param>
		/// <param name="location">ת��ҳ��·��</param>
		public static void Redirect(Page page, string location)
		{
			page.RegisterStartupScript(Guid.NewGuid().ToString().Trim(), "<script language='javascript'>window.location.href='" + location + "'</script>");
		}


		/// <summary>
		/// ��Ϣ��ʾ
		/// </summary>
		/// <param name="sourcePage">ҳ��</param>
		/// <param name="message">��Ϣ</param>
		public static void ShowMessage(Page page, string message)
		{
			page.RegisterStartupScript(Guid.NewGuid().ToString().Trim(), "<script language='javascript'>alert('" 
				+ TextHelper.FormatScript(message) + "');</script>" );
		}

		
		/// <summary>
		/// ��Ϣ��ʾ������ˢ�¸�ҳ��
		/// </summary>
		/// <param name="sourcePage">ҳ��</param>
		/// <param name="message">��Ϣ</param>
		public static void ShowMessageAndSetRefresh(Page page, string message)
		{
			WebHelper.ShowMessage(page, message);
			SetRefresh(page);
		}


		/// <summary>
		/// ��Ϣ��ʾ��ˢ��ҳ��
		/// </summary>
		/// <param name="sourcePage">ҳ��</param>
		/// <param name="message">��Ϣ</param>
		public static void ShowMessageAndRefresh(Page page, string message)
		{
			WebHelper.ShowMessage(page, message);
			Refresh(page);			
		}


		/// <summary>
		/// ��Ϣ��ʾˢ�¸�ҳ��
		/// </summary>
		/// <param name="sourcePage">ҳ��</param>
		/// <param name="message">��Ϣ</param>
		public static void ShowMessageAndRefreshParent(Page page, string message)
		{
			WebHelper.ShowMessage(page, message);

			//
			// sourcePage.Response.Write("<script language='javascript'>opener=null;window.returnValue=0;window.close();</script>");			 
			// �� javascript:showModalRefresh(targetURL,width,height) ��������ʹ��
			// �� javascript:showModalAndGetReturnValue(targetURL,width,height) ��������ʹ��
			// �� javascript:showModalRedirect(targetURL,width,height,targetPage) ��������ʹ��
			// 

			RefreshParent(page);
		}


		/// <summary>
		/// ��Ϣ��ʾ��ת��ҳ��
		/// </summary>
		/// <param name="sourcePage">ҳ��</param>
		/// <param name="message">��Ϣ</param>
		/// <param name="location">ת��ҳ��·��</param>
		public static void ShowMessageAndRedirect(Page sourcePage, string message, string location)
		{
			WebHelper.ShowMessage(sourcePage, message);

			Redirect(sourcePage, location);
		}


		/// <summary>
		/// ��Ϣ��ʾ���رմ���
		/// </summary>
		/// <param name="page">ҳ��</param>
		/// <param name="message">��Ϣ</param>
		public static void ShowMessageAndCloseDivDialog(Page page, string message)
		{
			// �� ShowDivDialog(url, dialogWidth, dialogHeight) ��������ʹ��
			page.RegisterClientScriptBlock(Guid.NewGuid().ToString(), String.Format("<script language='javascript'>alert('{0}');CloseDivDialog();</script>", TextHelper.FormatScript(message)));			
		}


		/// <summary>
		/// �رմ��ڲ�ת����ָ��ҳ��
		/// </summary>
		/// <param name="page">ҳ��</param>
		/// <param name="location">ת��ҳ��·��</param>
		public static void CloseDivDialogAndRedirect(Page page, string location)
		{
			// �� ShowDivDialog(url, dialogWidth, dialogHeight) ��������ʹ��
			page.RegisterClientScriptBlock(Guid.NewGuid().ToString(), String.Format("<script language='javascript'>CloseDivDialog();parent.location.href='{0}'</script>", TextHelper.FormatScript(location)));
		}


		/// <summary>
		/// �رմ��ڲ�ˢ��ҳ��
		/// </summary>
		/// <param name="page">ҳ��</param>
		public static void CloseDivDialogAndRefresh(Page page)
		{
			// �� ShowDivDialog(url, dialogWidth, dialogHeight) ��������ʹ��
			page.RegisterClientScriptBlock(Guid.NewGuid().ToString(), "<script language='javascript'>CloseDivDialog();parent.location.href=parent.location.href</script>");
		}

		public static void CloseDialogAndRefresh(Page page)
		{
			page.RegisterClientScriptBlock(Guid.NewGuid().ToString(), "<script language='javascript'>window.parent.$.akModalRemove();parent.location.href=parent.location.href;</script>");
		}
		public static void CloseDialogAndRedirect(Page page, string location)
		{
			page.RegisterClientScriptBlock(Guid.NewGuid().ToString(), String.Format("<script language='javascript'>window.parent.$.akModalHideAndRedirect('{0}');</script>", location));
		}

		#endregion

		#region ���� Excel

		/// <summary>
		/// ����Excel
		/// </summary>
		/// <param name="page">ҳ��</param>
		/// <param name="control">�ؼ�</param>
		public static void Export(Page page, Control control)
		{
			page.Response.Clear();
			page.Response.Buffer= true;
			page.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=gb2312>"); 
			page.Response.AppendHeader("content-disposition", "attachment;filename=\"Export.xls\"");
			page.Response.Charset = "";
			page.Response.ContentEncoding = Encoding.Default;  
			page.EnableViewState = false;

			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			control.RenderControl(htmlTextWriter);

			page.Response.Write(stringWriter.ToString());
			page.Response.End();
		}

		
		/// <summary>
		/// �����ļ���ָ���ļ�
		/// </summary>
		/// <param name="fileName">�ļ�·��</param>
		/// <param name="control">�ؼ�</param>
		public static void ExportToFile(string fileName, Control control)
		{
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			control.RenderControl(htmlTextWriter);

			StreamWriter streamWriter = File.CreateText(fileName);

			try
			{
				streamWriter.Write(stringWriter.ToString());
				streamWriter.Flush();				
			}
			catch
			{}
			finally
			{
				streamWriter.Close();
			}
		}

		#endregion

		#region �ϲ���Ԫ��

		/// <summary>
		/// ����������ͬ����ʱ��ָ���кϲ�
		/// </summary>
		/// <param name="dg">��ʽ����DataGrid</param>
		/// <param name="startRowIndex">��ʼ�����</param>
		/// <param name="length">�ϲ�����</param>
		public static void FormatGrid(DataGrid dataGrid, int startRowIndex, int length)
		{
			int[] rowSpan = new int[length];

			// ��ʼ��
			for ( int i = 0; i < length; i++ )
			{
				rowSpan[i] = 1;
			}
			
			for ( int itemIndex = 1; itemIndex < dataGrid.Items.Count; itemIndex++ )
			{
				for ( int i = 0; i < length; i++ )
				{
					int rowIndex = startRowIndex + i;

					// ��������һ��¼��ͬ��ϲ���Ԫ��
					if ( dataGrid.Items[itemIndex].Cells[rowIndex].Text.Trim().Equals(dataGrid.Items[itemIndex - 1].Cells[rowIndex].Text.Trim()) )
					{						
						rowSpan[i]++;
						dataGrid.Items[itemIndex].Cells[rowIndex].Visible = false;
						dataGrid.Items[itemIndex - rowSpan[i] + 1].Cells[rowIndex].RowSpan = rowSpan[i];
					}
					else
					{
						for ( int j = i; j < length; j++ )
						{
							rowSpan[j] = 1;
						}

						break;
					}
				}
			}
		}
		
		#endregion

		#region ��������ͼ

		/// <summary>
		/// ��������ͼ
		/// </summary>
		/// <param name="uploadImagePath">�ϴ��ļ�·��</param>
		/// <param name="thumbnailImagePath">����ͼ·��</param>
		/// <param name="maxLength">��󳤶�</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		public static bool BuildThumbnailImage(string uploadImagePath, string thumbnailImagePath, int maxLength)
		{
			int thumbWidth = maxLength;
			int thumbHeight = maxLength;
			
			System.Drawing.Image uploadImage = System.Drawing.Image.FromFile(uploadImagePath);
			
			if ( uploadImage.Width > uploadImage.Height )
			{				
				thumbHeight = (int)(thumbWidth * uploadImage.Height) / uploadImage.Width ;
			}
			else
			{				
				thumbWidth = (int)(thumbHeight * uploadImage.Width) / uploadImage.Height;
			}

			// ����ͼ���С�������߶ȶ�����Ϊ0
			System.Drawing.Image thumbnailImage = new Bitmap(thumbWidth, thumbHeight, PixelFormat.Format24bppRgb);
			
			try
			{
				Graphics graphics = Graphics.FromImage(thumbnailImage);

				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.SmoothingMode      = SmoothingMode.HighQuality;
				graphics.InterpolationMode  = InterpolationMode.HighQualityBicubic;
				graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, thumbWidth, thumbHeight);
				
				// ������С
				Rectangle rectangle = new Rectangle(0, 0, thumbWidth, thumbHeight);
				graphics.DrawImage(uploadImage, rectangle);

				// ��������ͼ���洢��ʽ��ԭͼƬһ��
				thumbnailImage.Save(thumbnailImagePath, uploadImage.RawFormat);

				return true;
			}
			catch
			{
				// �˴������쳣�����κδ���
			}
			finally
			{
				thumbnailImage.Dispose();
				uploadImage.Dispose();
			}

			return false;
		}

		#endregion

		#region �ϴ��ļ�

		/// <summary>
		/// �ϴ��ļ�
		/// </summary>		
		/// <param name="httpPostedFile">�ϴ����ļ�</param>
		/// <param name="uploadPhysicalPath">�ϴ��ļ�������·��</param>
		/// <returns>�����ļ�����</returns>
		public static string Upload(HttpPostedFile httpPostedFile, string uploadPhysicalPath)
		{
			string uniqueFileName = string.Empty;

			try
			{
				string uploadFileName = httpPostedFile.FileName;
				string suffix = uploadFileName.Substring(uploadFileName.LastIndexOf(@"."));
				uniqueFileName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + suffix;
				string destinateFile = Path.Combine(uploadPhysicalPath, uniqueFileName);

				if ( ! Directory.Exists(uploadPhysicalPath) )
				{
					Directory.CreateDirectory(uploadPhysicalPath);
				}				

				httpPostedFile.SaveAs(destinateFile);
			}
			catch
			{}

			return uniqueFileName;
		}

		#endregion

		#region �ϴ��ļ�����������ͼ

		/// <summary>
		/// �ϴ��ļ�����������ͼ
		/// </summary>
		/// <param name="httpPostedFile">�ϴ����ļ�</param>
		/// <param name="uploadPhysicalPath">�ϴ��ļ�������·��</param>
		/// <param name="thumbnailPhysicalPath">����ͼ�ļ�������·��</param>
		/// <param name="maxLength">��󳤶�</param>
		/// <returns>�����ļ�����</returns>
		public static string UploadBuildThumbnailImage(HttpPostedFile httpPostedFile, string uploadPhysicalPath, string thumbnailPhysicalPath, int maxLength)
		{
			// �ϴ������ļ�
			string uploadImageName = Upload(httpPostedFile, uploadPhysicalPath);
			
			if ( uploadImageName.Length == 0 )
			{
				return string.Empty;
			}

			string uploadImagePath = Path.Combine(uploadPhysicalPath, uploadImageName);
			string thumbnailImagePath = Path.Combine(thumbnailPhysicalPath, uploadImageName);

			if ( ! Directory.Exists(thumbnailPhysicalPath) )
			{
				Directory.CreateDirectory(thumbnailPhysicalPath);
			}

			// ��������ͼ
			if ( ! BuildThumbnailImage(uploadImagePath, thumbnailImagePath, maxLength) )
			{
				return string.Empty;
			}

			return uploadImageName;
		}

		#endregion

		/// <summary>
		/// �����б����Ϳؼ���ֵ
		/// </summary>
		/// <param name="listControl">�б����Ϳؼ�</param>
		/// <param name="_value">ֵ</param>
		public static void SetListControl(ListControl listControl, string _value)
		{
			foreach ( ListItem listItem in listControl.Items )
			{
				listItem.Selected = listItem.Value.Equals(_value);
			}
		}


		/// <summary>
		/// ��׼У��
		/// </summary>
		/// <param name="validateValue">���˶Ե�ֵ</param>
		/// <param name="trueValue">��ȷֵ</param>
		public static void Authorize(object validateValue, object trueValue)
		{
			if ( ! validateValue.Equals(trueValue) )
			{
				HttpContext.Current.Response.Redirect("/Portal/Error.aspx");
			}
		}
	}
}
