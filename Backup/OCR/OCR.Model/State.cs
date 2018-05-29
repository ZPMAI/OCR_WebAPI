using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OCR.Model
{
    /// <summary>
    /// 多线程计数器
    /// </summary>
    public class State
    {
        public State(int max, AutoResetEvent asyncOpIsDone)
        {
            _max = max;
            _asyncOpIsDone = asyncOpIsDone;
            cur = 0;
        }

        private int _max;
        public int Max
        {
            get
            {
                return _max;
            }
        }

        public int cur;

        private AutoResetEvent _asyncOpIsDone;
        public AutoResetEvent AsyncOpIsDone
        {
            get
            {
                return _asyncOpIsDone;
            }
        }
    }
}
