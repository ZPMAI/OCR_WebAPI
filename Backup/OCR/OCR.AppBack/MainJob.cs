using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

using OCR.Model;
using OCR.BLL;

namespace OCR.AppBack
{
    /// <summary>
    /// Ö÷½ø³Ì
    /// </summary>
    public class MainJob
    {
        public void Start()
        {
            try
            {
                //OCR.Model.Job.T_OCR_JOBDataTable j = jobBLL.SelectAll(DateTime.Now); 

                //if (j.Count == 0)
                //{
                //    return;
                //}

                //State state = new State(j.Count, new AutoResetEvent(false));

                //foreach (OCR.Model.Job.T_OCR_JOBRow r in j)
                //{
                //    IJob job = JobFactory.CreateInstance(r);

                //    ThreadPool.QueueUserWorkItem(new WaitCallback(job.DealJobAsy), state);
                //}

                //state.AsyncOpIsDone.WaitOne();

            }
            catch 
            {
               
            }
        }
    }
}
