using System;
using System.Data.SqlClient;

using CCT.SystemFramework.Configuration;
using CCT.SystemFramework.Data;

namespace CCT.SystemFramework.ShortMessage
{
	/// <summary>
	/// ShortMessageHelper ��ժҪ˵����
	/// </summary>
	public class ShortMessageHelper
	{
		/// ˽�й��캯��
		private ShortMessageHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// ���Ͷ���
		/// </summary>
		/// <param name="handset">�ֻ�����</param>
		/// <param name="content">����Ϣ����</param>
		/// <param name="flat">���ű�־��B02���ԣ�B01��ʽ��</param>
		private static void SendMessage(string handset, string message, string flat)
		{
			string database = ConfigurationHelper.Section["SMS"]["Database"].ToString();
			string connectionString = CCT.SystemFramework.Data.ConnectionHelper.GetMsSqlConnectionString(database,"SMS");

			string sql = "INSERT INTO b_MsgOut (phone, msg_content, opt_flag) VALUES (@phone, @msg_content, @opt_flag)";

			SqlCommand command = new SqlCommand(sql);
			command.Parameters.Add("@phone", handset);
			command.Parameters.Add("@msg_content", message);
			command.Parameters.Add("@opt_flag", flat);

			SqlHelper.SqlServer.ExecuteNonQuery(connectionString, command);
		}


		/// <summary>
		/// ���Ͷ���
		/// </summary>
		/// <param name="handset">�ֻ�����</param>
		/// <param name="content">����Ϣ����</param>
		/// <param name="flat">���ű�־��B02���ԣ�B01��ʽ��</param>
		public static void Send(string handset, string content, string flat)
		{
			// ������ַ���ĿΪ 60
			const int MAXCOUNT = 60;
			int length = content.Length;
			int messageCount = Convert.ToInt32(Math.Ceiling((double)length / MAXCOUNT));

			if ( messageCount > 1 )
			{
				// LP1513�����к�68���Ȱ���ǰ��������ͷ������ECMU0000009�����1��20GP������Ԥ�Ƶִ������ͷǰ60�����ڣ��ظ�����9���
				const int PREVIOUSCOUNT = 8;
				const int NEXTCOUNT = 5;
				const int FINISHCOUNT = 3;
				const string PREVIOUSSTRING = @"������\��һҳ��";
				const string NEXTSTRING = "������ҳ��";
				const string FINISHSTRING = "���꣩";

				int realLength = length + ( messageCount - 1 ) * ( PREVIOUSCOUNT + NEXTCOUNT ) + FINISHCOUNT;
				int realMessageCount = Convert.ToInt32(Math.Ceiling((double)realLength / MAXCOUNT));

				// �ַ����ƶ�ָ��
				int index = 0;

				for ( int i = 0; i < realMessageCount; i++ )
				{
					int contentLength;
					string message;

					if ( i == 0 )
					{
						// ��һ����Ϣ
						contentLength = MAXCOUNT - PREVIOUSCOUNT;
						message = string.Format("{0}{1}", content.Substring(index, contentLength), PREVIOUSSTRING);
						index += contentLength;
					}
					else if ( i == realMessageCount - 1 )
					{
						// ���һ����Ϣ
						message = string.Format("{0}{1}{2}", NEXTSTRING, content.Substring(index), FINISHSTRING);
					}
					else
					{
						// �м���Ϣ
						contentLength = MAXCOUNT - ( PREVIOUSCOUNT + NEXTCOUNT );
						message = string.Format("{0}{1}{2}", NEXTSTRING, content.Substring(index, contentLength) ,NEXTSTRING);
						index += contentLength;
					}

					SendMessage(handset, message, flat);
				}
			}
			else
			{
				SendMessage(handset, content, flat);
			}
		}		
	}
}
