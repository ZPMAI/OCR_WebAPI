using System;

namespace CCT.SystemFramework.Office
{
	/// <summary>
	/// OutlookHelper ��ժҪ˵����
	/// </summary>
	public class OutlookHelper
	{
		/// ���캯��
		public OutlookHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
