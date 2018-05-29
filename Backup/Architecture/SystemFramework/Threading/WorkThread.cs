using System;
using System.Threading;

namespace CCT.SystemFramework.Threading
{
	/// <summary>
	/// WorkThread 的摘要说明。
	/// </summary>
	public class WorkThread : IDisposable
	{
		private Thread thread;
		private WaitCallback waitCallback;
		private object args;

			/// 是否可用
		public bool IsAvailable
		{
			get { return ( this.thread == null ) || ( this.thread.ThreadState == ThreadState.Suspended ); }
		}


		/// 构造函数
		public WorkThread()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		/// 线程执行
		private void ThreadProcess()
		{
			// 死循环，使线程唤醒后不是退出，而是继续通过委托执行回调方法
			while ( true )
			{
				this.waitCallback(this.args);
				// Add By RQ 2007-07-04 11:18 为什么只执行上面的委托？事件里面出了什么问题？
				this.thread.Suspend();
			}
		}


		/// 开启新线程或继续已挂气的线程执行回调方法
		public void Start(WaitCallback waitCallback, object args)
		{			
			this.waitCallback = waitCallback;
			this.args = args;
			
			// 创建一个新线程并执行
			if ( this.thread == null )
			{				
				ThreadStart threadStart = new ThreadStart(this.ThreadProcess);
				this.thread = new Thread(threadStart);
				this.thread.Start();
			}
			else
			{
				// 继续已挂起的线程
				if ( this.thread.ThreadState == ThreadState.Suspended )
				{
					this.thread.Resume();
				}
			}
		}


		/// 执行与释放获重置非托管资源相关的应用程序定义的任务
		public void Dispose()
		{
			GC.SuppressFinalize( this );
		}
	}
}
