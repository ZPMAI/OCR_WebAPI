using System;
using System.IO;
using System.Reflection;
using System.Resources;

using CCT.SystemFramework;

namespace CCT.SystemFramework
{
	/// <summary>
	/// Constants ��ժҪ˵����
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


		/// ���캯��
		public Constants()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//			
		}
	}
}
