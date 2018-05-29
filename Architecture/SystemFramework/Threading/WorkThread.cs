using System;
using System.Threading;

namespace CCT.SystemFramework.Threading
{
	/// <summary>
	/// WorkThread ��ժҪ˵����
	/// </summary>
	public class WorkThread : IDisposable
	{
		private Thread thread;
		private WaitCallback waitCallback;
		private object args;

			/// �Ƿ����
		public bool IsAvailable
		{
			get { return ( this.thread == null ) || ( this.thread.ThreadState == ThreadState.Suspended ); }
		}


		/// ���캯��
		public WorkThread()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// �߳�ִ��
		private void ThreadProcess()
		{
			// ��ѭ����ʹ�̻߳��Ѻ����˳������Ǽ���ͨ��ί��ִ�лص�����
			while ( true )
			{
				this.waitCallback(this.args);
				// Add By RQ 2007-07-04 11:18 Ϊʲôִֻ�������ί�У��¼��������ʲô���⣿
				this.thread.Suspend();
			}
		}


		/// �������̻߳�����ѹ������߳�ִ�лص�����
		public void Start(WaitCallback waitCallback, object args)
		{			
			this.waitCallback = waitCallback;
			this.args = args;
			
			// ����һ�����̲߳�ִ��
			if ( this.thread == null )
			{				
				ThreadStart threadStart = new ThreadStart(this.ThreadProcess);
				this.thread = new Thread(threadStart);
				this.thread.Start();
			}
			else
			{
				// �����ѹ�����߳�
				if ( this.thread.ThreadState == ThreadState.Suspended )
				{
					this.thread.Resume();
				}
			}
		}


		/// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
		public void Dispose()
		{
			GC.SuppressFinalize( this );
		}
	}
}
