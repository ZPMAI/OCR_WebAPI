using System;
using System.Threading;

namespace CCT.SystemFramework.Threading
{
	/// <summary>
	/// WorkThreadPool 的摘要说明。
	/// </summary>
	public class WorkThreadPool : IDisposable
	{
		private int size;
		private WorkThread[] workThreads;

		/// 构造函数
		public WorkThreadPool(int size)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this.size = size;			
			this.workThreads = new WorkThread[this.size];

			for ( int i = 0; i < this.size; i++ )
			{
				workThreads[i] = new WorkThread();
			}
		}
		

		/// 获取有效的线程
		private WorkThread GetAvailableWorkThread()
		{
			for ( int i = 0; i < workThreads.Length; i++ )
			{
				WorkThread workThread = workThreads[i];

				if ( workThread.IsAvailable )
				{
					return workThread;
				}
			}

			return null;
		}
		

		/// 将方法排入队列以便执行
		public void QueueUserWorkItem(WaitCallback waitCallback, object args, int timeOut)
		{
			DateTime beginTime = DateTime.Now;
			
			// 锁住共享资源，实现线程安全
			lock ( workThreads )
			{
				try
				{
					do
					{						
						WorkThread workThread = GetAvailableWorkThread();

						if ( workThread != null )
						{
							workThread.Start(waitCallback, args);
							return;
						}
						
						// 通知所有的等待线程对象状态的更改
						Monitor.PulseAll(this.workThreads);
						Thread.Sleep(500);
					}
					while ( ((TimeSpan)(DateTime.Now - beginTime)).Milliseconds < timeOut );
				}
				catch ( SynchronizationLockException )
				{}
				finally
				{
					Monitor.Exit(this.workThreads);
				}
			}
		}


		/// 执行与释放获重置非托管资源相关的应用程序定义的任务
		public void Dispose()
		{
			foreach ( WorkThread workThread in this.workThreads )
			{
				workThread.Dispose();
			}

			GC.SuppressFinalize( this );	
		}
	}
}
