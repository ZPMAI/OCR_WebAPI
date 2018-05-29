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
	/// WebHelper 的摘要说明。
	/// </summary>
	public sealed class WebHelper
	{
		/// 私有构造函数
		private WebHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		#region 信息显示操作

		/// <summary>
		/// 刷新页面
		/// </summary>
		/// <param name="sourcePage">页面</param>
		public static void Refresh(Page page)
		{
			page.RegisterStartupScript(Guid.NewGuid().ToString().Trim(), "<script language='javascript'>window.location.href = window.location.href;</script>");
		}


		/// <summary>
		/// 设置刷新父页面
		/// </summary>
		/// <param name="sourcePage">页面</param>
		public static void SetRefresh(Page page)
		{
			page.RegisterStartupScript(Guid.NewGuid().ToString().Trim(), "<script language='javascript'>window.returnValue=0;</script>");
		}


		/// <summary>
		/// 刷新父页面
		/// </summary>
		/// <param name="sourcePage">页面</param>
		public static void RefreshParent(Page page)
		{
			// 与 javascript:ShowMessageAndRefresh 合并使用
			page.RegisterStartupScript(Guid.NewGuid().ToString().Trim(), "<script language='javascript'>opener=null;window.returnValue=0;window.close();</script>");
		}


		/// <summary>
		/// 转移至指定页面
		/// </summary>
		/// <param name="sourcePage">页面</param>
		/// <param name="location">转移页面路径</param>
		public static void Redirect(Page page, string location)
		{
			page.RegisterStartupScript(Guid.NewGuid().ToString().Trim(), "<script language='javascript'>window.location.href='" + location + "'</script>");
		}


		/// <summary>
		/// 信息显示
		/// </summary>
		/// <param name="sourcePage">页面</param>
		/// <param name="message">信息</param>
		public static void ShowMessage(Page page, string message)
		{
			page.RegisterStartupScript(Guid.NewGuid().ToString().Trim(), "<script language='javascript'>alert('" 
				+ TextHelper.FormatScript(message) + "');</script>" );
		}

		
		/// <summary>
		/// 信息显示并设置刷新父页面
		/// </summary>
		/// <param name="sourcePage">页面</param>
		/// <param name="message">信息</param>
		public static void ShowMessageAndSetRefresh(Page page, string message)
		{
			WebHelper.ShowMessage(page, message);
			SetRefresh(page);
		}


		/// <summary>
		/// 信息显示并刷新页面
		/// </summary>
		/// <param name="sourcePage">页面</param>
		/// <param name="message">信息</param>
		public static void ShowMessageAndRefresh(Page page, string message)
		{
			WebHelper.ShowMessage(page, message);
			Refresh(page);			
		}


		/// <summary>
		/// 信息显示刷新父页面
		/// </summary>
		/// <param name="sourcePage">页面</param>
		/// <param name="message">信息</param>
		public static void ShowMessageAndRefreshParent(Page page, string message)
		{
			WebHelper.ShowMessage(page, message);

			//
			// sourcePage.Response.Write("<script language='javascript'>opener=null;window.returnValue=0;window.close();</script>");			 
			// 与 javascript:showModalRefresh(targetURL,width,height) 函数联合使用
			// 与 javascript:showModalAndGetReturnValue(targetURL,width,height) 函数联合使用
			// 与 javascript:showModalRedirect(targetURL,width,height,targetPage) 函数联合使用
			// 

			RefreshParent(page);
		}


		/// <summary>
		/// 信息显示并转移页面
		/// </summary>
		/// <param name="sourcePage">页面</param>
		/// <param name="message">信息</param>
		/// <param name="location">转移页面路径</param>
		public static void ShowMessageAndRedirect(Page sourcePage, string message, string location)
		{
			WebHelper.ShowMessage(sourcePage, message);

			Redirect(sourcePage, location);
		}


		/// <summary>
		/// 信息显示并关闭窗口
		/// </summary>
		/// <param name="page">页面</param>
		/// <param name="message">信息</param>
		public static void ShowMessageAndCloseDivDialog(Page page, string message)
		{
			// 与 ShowDivDialog(url, dialogWidth, dialogHeight) 函数联合使用
			page.RegisterClientScriptBlock(Guid.NewGuid().ToString(), String.Format("<script language='javascript'>alert('{0}');CloseDivDialog();</script>", TextHelper.FormatScript(message)));			
		}


		/// <summary>
		/// 关闭窗口并转移至指定页面
		/// </summary>
		/// <param name="page">页面</param>
		/// <param name="location">转移页面路径</param>
		public static void CloseDivDialogAndRedirect(Page page, string location)
		{
			// 与 ShowDivDialog(url, dialogWidth, dialogHeight) 函数联合使用
			page.RegisterClientScriptBlock(Guid.NewGuid().ToString(), String.Format("<script language='javascript'>CloseDivDialog();parent.location.href='{0}'</script>", TextHelper.FormatScript(location)));
		}


		/// <summary>
		/// 关闭窗口并刷新页面
		/// </summary>
		/// <param name="page">页面</param>
		public static void CloseDivDialogAndRefresh(Page page)
		{
			// 与 ShowDivDialog(url, dialogWidth, dialogHeight) 函数联合使用
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

		#region 导出 Excel

		/// <summary>
		/// 导出Excel
		/// </summary>
		/// <param name="page">页面</param>
		/// <param name="control">控件</param>
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
		/// 导出文件到指定文件
		/// </summary>
		/// <param name="fileName">文件路径</param>
		/// <param name="control">控件</param>
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

		#region 合并单元格

		/// <summary>
		/// 相邻行有相同内容时对指定列合并
		/// </summary>
		/// <param name="dg">格式化的DataGrid</param>
		/// <param name="startRowIndex">起始列序号</param>
		/// <param name="length">合并长度</param>
		public static void FormatGrid(DataGrid dataGrid, int startRowIndex, int length)
		{
			int[] rowSpan = new int[length];

			// 初始化
			for ( int i = 0; i < length; i++ )
			{
				rowSpan[i] = 1;
			}
			
			for ( int itemIndex = 1; itemIndex < dataGrid.Items.Count; itemIndex++ )
			{
				for ( int i = 0; i < length; i++ )
				{
					int rowIndex = startRowIndex + i;

					// 内容与上一记录相同则合并单元格
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

		#region 生成缩略图

		/// <summary>
		/// 生成缩略图
		/// </summary>
		/// <param name="uploadImagePath">上传文件路径</param>
		/// <param name="thumbnailImagePath">缩略图路径</param>
		/// <param name="maxLength">最大长度</param>
		/// <returns>操作是否成功</returns>
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

			// 缩略图像大小，宽度与高度都不能为0
			System.Drawing.Image thumbnailImage = new Bitmap(thumbWidth, thumbHeight, PixelFormat.Format24bppRgb);
			
			try
			{
				Graphics graphics = Graphics.FromImage(thumbnailImage);

				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.SmoothingMode      = SmoothingMode.HighQuality;
				graphics.InterpolationMode  = InterpolationMode.HighQualityBicubic;
				graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, thumbWidth, thumbHeight);
				
				// 画布大小
				Rectangle rectangle = new Rectangle(0, 0, thumbWidth, thumbHeight);
				graphics.DrawImage(uploadImage, rectangle);

				// 保存缩略图，存储格式与原图片一致
				thumbnailImage.Save(thumbnailImagePath, uploadImage.RawFormat);

				return true;
			}
			catch
			{
				// 此处不对异常进行任何处理
			}
			finally
			{
				thumbnailImage.Dispose();
				uploadImage.Dispose();
			}

			return false;
		}

		#endregion

		#region 上传文件

		/// <summary>
		/// 上传文件
		/// </summary>		
		/// <param name="httpPostedFile">上传的文件</param>
		/// <param name="uploadPhysicalPath">上传文件夹物理路径</param>
		/// <returns>保存文件名称</returns>
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

		#region 上传文件并生成缩略图

		/// <summary>
		/// 上传文件并生成缩略图
		/// </summary>
		/// <param name="httpPostedFile">上传的文件</param>
		/// <param name="uploadPhysicalPath">上传文件夹物理路径</param>
		/// <param name="thumbnailPhysicalPath">缩略图文件夹物理路径</param>
		/// <param name="maxLength">最大长度</param>
		/// <returns>保存文件名称</returns>
		public static string UploadBuildThumbnailImage(HttpPostedFile httpPostedFile, string uploadPhysicalPath, string thumbnailPhysicalPath, int maxLength)
		{
			// 上传正常文件
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

			// 生成缩略图
			if ( ! BuildThumbnailImage(uploadImagePath, thumbnailImagePath, maxLength) )
			{
				return string.Empty;
			}

			return uploadImageName;
		}

		#endregion

		/// <summary>
		/// 设置列表类型控件的值
		/// </summary>
		/// <param name="listControl">列表类型控件</param>
		/// <param name="_value">值</param>
		public static void SetListControl(ListControl listControl, string _value)
		{
			foreach ( ListItem listItem in listControl.Items )
			{
				listItem.Selected = listItem.Value.Equals(_value);
			}
		}


		/// <summary>
		/// 批准校验
		/// </summary>
		/// <param name="validateValue">被核对的值</param>
		/// <param name="trueValue">正确值</param>
		public static void Authorize(object validateValue, object trueValue)
		{
			if ( ! validateValue.Equals(trueValue) )
			{
				HttpContext.Current.Response.Redirect("/Portal/Error.aspx");
			}
		}
	}
}
