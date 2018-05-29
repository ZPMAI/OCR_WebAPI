using System;
using System.Threading;

namespace CCT.Common.Threading
{
    /// <summary>
    /// WorkThread 的摘要说明。
    /// </summary>
    public class WorkThread : IDisposable
    {
        //private Mutex mutex = new Mutex();
        private Thread thread = null;
        private WaitCallback waitCallback;
        private object args;

        private bool isSuspended;
        private bool isStarted;

        /// 是否可用
        public bool IsAvailable
        {
            get
            {
                return !isStarted || isSuspended;
            }
        }


        /// 构造函数
        public WorkThread()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            isSuspended = false;
            isStarted = false;
        }


        /// 线程执行
        private void ExecuteProcess()
        {
            do
            {
                if (!isSuspended)
                {
                    //死循环，使线程唤醒后不是退出，而是继续通过委托执行回调方法
                    this.waitCallback(this.args);
                    this.isSuspended = true;
                }

                //Release CPU here
                Thread.Sleep(2000);

                //mutex.WaitOne();
                //mutex.ReleaseMutex();
            }
            while (true);

            //do
            //{
            //    //Add By RQ 2010-10-28 10:04 NET2.0 里 Thread.Suspend 方法已经过时
            //    //this.thread.Suspend();
            //}
            //while (true);
        }

        private void Start()
        {
            ThreadStart threadStart = new ThreadStart(ExecuteProcess);
            this.thread = new Thread(threadStart);

            isStarted = true;

            this.thread.Start();
        }

        private void Resume()
        {
            if (isSuspended)
            {
                isSuspended = false;

                //从不同步的代码块中调用了对象同步方法
                //mutex.WaitOne();
                //mutex.ReleaseMutex();
            }
        }

        /// 开启新线程或继续已挂起的线程执行回调方法
        public void Execute(WaitCallback waitCallback, object args)
        {
            this.waitCallback = waitCallback;
            this.args = args;

            if (!isStarted)
            {
                Start();
            }
            else
            {
                Resume();
            }

            //Delete By RQ 2010-10-28 12:32
            ////创建一个新线程并执行
            //if ( this.thread == null )
            //{				
            //    ThreadStart threadStart = new ThreadStart(this.ExecuteProcess);
            //    this.thread = new Thread(threadStart);
            //    this.thread.Start();
            //}
            //else
            //{
            //    // 继续已挂起的线程
            //    if ( this.thread.ThreadState == ThreadState.Suspended )
            //    {
            //        //Add By RQ 2010-10-28 10:04 NET2.0 里 Thread.Suspend 方法已经过时
            //        //this.thread.Resume();
            //    }
            //}
        }


        /// 执行与释放获重置非托管资源相关的应用程序定义的任务
        public void Dispose()
        {
            if (isStarted || isSuspended)
            {
                this.thread.Join();
            }

            GC.SuppressFinalize(this);
        }
    }
}
