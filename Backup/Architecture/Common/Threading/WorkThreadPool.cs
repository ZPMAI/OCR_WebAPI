using System;
using System.Threading;

namespace CCT.Common.Threading
{
    /// <summary>
    /// WorkThreadPool ��ժҪ˵����
    /// </summary>
    public class WorkThreadPool : IDisposable
    {
        private int size;
        private WorkThread[] workThreads;

        /// ���캯��
        public WorkThreadPool(int size)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            this.size = size;
            this.workThreads = new WorkThread[this.size];

            for (int i = 0; i < this.size; i++)
            {
                workThreads[i] = new WorkThread();
            }
        }


        /// ��ȡ��Ч���߳�
        private WorkThread GetAvailableWorkThread()
        {
            for (int i = 0; i < workThreads.Length; i++)
            {
                WorkThread workThread = workThreads[i];

                if (workThread.IsAvailable)
                {
                    return workThread;
                }
            }

            return null;
        }


        /// ��������������Ա�ִ��
        public void QueueUserWorkItem(WaitCallback waitCallback, object args, int timeOut)
        {
            DateTime beginTime = DateTime.Now;

            // ��ס������Դ��ʵ���̰߳�ȫ
            lock (workThreads)
            {
                try
                {
                    do
                    {
                        WorkThread workThread = this.GetAvailableWorkThread();

                        if (workThread != null)
                        {
                            workThread.Execute(waitCallback, args);
                            return;
                        }

                        //Delete By RQ 2010-10-28 12:11 �Ӳ�ͬ���Ĵ�����е����˶���ͬ��������
                        //Monitor.PulseAll(this.workThreads);
                        Thread.Sleep(500);
                    }
                    while (((TimeSpan)(DateTime.Now - beginTime)).Milliseconds < timeOut);

                    throw new ThreadStateException("�̳߳��������޷���ȡ���̣߳�");
                }
                catch (SynchronizationLockException)
                { }
                finally
                {
                    //Delete By RQ 2010-10-28 12:11 �Ӳ�ͬ���Ĵ�����е����˶���ͬ��������
                    //Monitor.Exit(this.workThreads);
                }
            }
        }


        /// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
        public void Dispose()
        {
            foreach (WorkThread workThread in this.workThreads)
            {
                workThread.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
