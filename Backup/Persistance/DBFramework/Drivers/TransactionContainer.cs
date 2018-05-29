using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DBFramework.Drivers
{
    /// <summary>
    /// ��̬ DBContext.Runner.Driver��ʹ��ͬһ Transaction ���������߳�����»���� Transaction �������߳�����
    /// ʹ�� TramsactionContainer ��ֵΪ CurrentThread.ManagedThreadId �Ը��̵߳� Transaction ���б���
    /// ����ʹ�þ�̬�࣬����ͬһ�߳��ж�� Context �໥����
    /// </summary>
    public class TransactionContainer
    {
        private Dictionary<int, IDbTransaction> dictionary;

        /// ���캯��
        public TransactionContainer()
        {
            dictionary = new Dictionary<int, IDbTransaction>();
        }

        public IDbTransaction GetCurrentTransaction()
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;

            if (dictionary.ContainsKey(threadID))
            {
                IDbTransaction transaction = dictionary[threadID];

                if (transaction != null
                    && transaction.Connection != null
                    && transaction.Connection.State == ConnectionState.Open)
                {
                    return transaction;
                }
            }

            return null;
        }

        public void Add(IDbTransaction transaction)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;

            if (!dictionary.ContainsKey(threadID))
            {
                lock (dictionary)
                {
                    dictionary.Add(threadID, transaction);
                }
            }
        }

        public void Remove()
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;

            if (dictionary.ContainsKey(threadID))
            {
                lock (dictionary)
                {
                    IDbTransaction transactioin = dictionary[threadID];

                    if (transactioin.Connection != null
                        && transactioin.Connection.State != ConnectionState.Closed)
                    {
                        transactioin.Connection.Close();
                    }

                    transactioin.Dispose();

                    dictionary.Remove(threadID);
                }
            }
        }
    }
}
