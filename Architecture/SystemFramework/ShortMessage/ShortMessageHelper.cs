using System;
using System.Data.SqlClient;

using CCT.SystemFramework.Configuration;
using CCT.SystemFramework.Data;

namespace CCT.SystemFramework.ShortMessage
{
	/// <summary>
	/// ShortMessageHelper 的摘要说明。
	/// </summary>
	public class ShortMessageHelper
	{
		/// 私有构造函数
		private ShortMessageHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		/// <summary>
		/// 发送短信
		/// </summary>
		/// <param name="handset">手机号码</param>
		/// <param name="content">短信息内容</param>
		/// <param name="flat">短信标志（B02测试，B01正式）</param>
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
		/// 发送短信
		/// </summary>
		/// <param name="handset">手机号码</param>
		/// <param name="content">短信息内容</param>
		/// <param name="flat">短信标志（B02测试，B01正式）</param>
		public static void Send(string handset, string content, string flat)
		{
			// 最大发送字符数目为 60
			const int MAXCOUNT = 60;
			int length = content.Length;
			int messageCount = Convert.ToInt32(Math.Ceiling((double)length / MAXCOUNT));

			if ( messageCount > 1 )
			{
				// LP1513，序列号68调度安排前往赤湾码头，还柜：ECMU0000009，提柜：1个20GP柜，请于预计抵达赤湾码头前60分钟内，回复数字9激活。
				const int PREVIOUSCOUNT = 8;
				const int NEXTCOUNT = 5;
				const int FINISHCOUNT = 3;
				const string PREVIOUSSTRING = @"（待续\下一页）";
				const string NEXTSTRING = "（接上页）";
				const string FINISHSTRING = "（完）";

				int realLength = length + ( messageCount - 1 ) * ( PREVIOUSCOUNT + NEXTCOUNT ) + FINISHCOUNT;
				int realMessageCount = Convert.ToInt32(Math.Ceiling((double)realLength / MAXCOUNT));

				// 字符串移动指针
				int index = 0;

				for ( int i = 0; i < realMessageCount; i++ )
				{
					int contentLength;
					string message;

					if ( i == 0 )
					{
						// 第一条信息
						contentLength = MAXCOUNT - PREVIOUSCOUNT;
						message = string.Format("{0}{1}", content.Substring(index, contentLength), PREVIOUSSTRING);
						index += contentLength;
					}
					else if ( i == realMessageCount - 1 )
					{
						// 最后一条信息
						message = string.Format("{0}{1}{2}", NEXTSTRING, content.Substring(index), FINISHSTRING);
					}
					else
					{
						// 中间信息
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
