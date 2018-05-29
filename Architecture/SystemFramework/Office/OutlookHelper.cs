using System;

namespace CCT.SystemFramework.Office
{
	/// <summary>
	/// OutlookHelper 的摘要说明。
	/// </summary>
	public class OutlookHelper
	{
		/// 构造函数
		public OutlookHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        public static bool Send(string To, string CC, string BCC, string Subject, string Body, string Attachment)
        {            
            try
            {
                if (To == string.Empty && CC == string.Empty && BCC == string.Empty) return false;

                Outlook._Application OlApp = new Outlook.ApplicationClass();
                Outlook.MailItem OlNewMail = (Outlook.MailItem)OlApp.CreateItem(Outlook.OlItemType.olMailItem);

                OlNewMail.To = To;
                OlNewMail.CC = CC;
                OlNewMail.BCC = BCC;
                OlNewMail.Subject = Subject;
                OlNewMail.Body = Body;                

                if (Attachment != string.Empty)
                {
                    OlNewMail.Attachments.Add(Attachment, Type.Missing, Type.Missing, Type.Missing);
                }
                OlNewMail.Send();                

                return true;

            }
            catch (System.Exception ex)
            {                
                System.Windows.Forms.MessageBox.Show(ex.Message);               
                return false;
            }           

        } 
	}
}
