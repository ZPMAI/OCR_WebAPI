using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DBFramework.Drivers
{
    /// <summary>
    /// 静态 DBContext.Runner.Driver，使用同一 Transaction 变量，多线程情况下会出现 Transaction 被其他线程窃用
    /// 使用 TramsactionContainer 键值为 CurrentThread.ManagedThreadId 对各线程的 Transaction 进行保护
    /// 不能使用静态类，以免同一线程有多个 Context 相互干扰
    /// </summary>
    public class TransactionContainer
    {
        private Dictionary<int, IDbTransaction> dictionary;

        /// 构造函数
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
