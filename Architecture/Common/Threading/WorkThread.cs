using System;
using System.Threading;

namespace CCT.Common.Threading
{
    /// <summary>
    /// WorkThread ��ժҪ˵����
    /// </summary>
    public class WorkThread : IDisposable
    {
        //private Mutex mutex = new Mutex();
        private Thread thread = null;
        private WaitCallback waitCallback;
        private object args;

        private bool isSuspended;
        private bool isStarted;

        /// �Ƿ����
        public bool IsAvailable
        {
            get
            {
                return !isStarted || isSuspended;
            }
        }


        /// ���캯��
        public WorkThread()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            isSuspended = false;
            isStarted = false;
        }


        /// �߳�ִ��
        private void ExecuteProcess()
        {
            do
            {
                if (!isSuspended)
                {
                    //��ѭ����ʹ�̻߳��Ѻ����˳������Ǽ���ͨ��ί��ִ�лص�����
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
            //    //Add By RQ 2010-10-28 10:04 NET2.0 �� Thread.Suspend �����Ѿ���ʱ
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

                //�Ӳ�ͬ���Ĵ�����е����˶���ͬ������
                //mutex.WaitOne();
                //mutex.ReleaseMutex();
            }
        }

        /// �������̻߳�����ѹ�����߳�ִ�лص�����
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
            ////����һ�����̲߳�ִ��
            //if ( this.thread == null )
            //{				
            //    ThreadStart threadStart = new ThreadStart(this.ExecuteProcess);
            //    this.thread = new Thread(threadStart);
            //    this.thread.Start();
            //}
            //else
            //{
            //    // �����ѹ�����߳�
            //    if ( this.thread.ThreadState == ThreadState.Suspended )
            //    {
            //        //Add By RQ 2010-10-28 10:04 NET2.0 �� Thread.Suspend �����Ѿ���ʱ
            //        //this.thread.Resume();
            //    }
            //}
        }


        /// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
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
