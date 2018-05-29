using System;
using System.IO;
using System.Reflection;
using System.Resources;

using CCT.SystemFramework;

namespace CCT.SystemFramework
{
	/// <summary>
	/// Constants 的摘要说明。
	/// </summary>
	internal sealed class Constants
	{
		public const string COOKIENAME = "CCT";

		//	public static string T
		//	{
		//		get
		//		{
		//			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
		//			string relativeSearchPath = AppDomain.CurrentDomain.RelativeSearchPath;
		//			string binPath = ( relativeSearchPath == null ) ? baseDirectory : Path.Combine( baseDirectory, relativeSearchPath );
		//
		//			return Path.Combine( binPath, "hibernate.cfg.xml" );
		//
		//			return Assembly.GetExecutingAssembly().Location; 
		//			Assembly assembly = Assembly.GetExecutingAssembly();
		//			return assembly.GetFiles()[0].Name;
		//			ResourceManager resourceManager = new ResourceManager(
		//		}
		//	}


		/// 构造函数
		public Constants()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//			
		}
	}
}
