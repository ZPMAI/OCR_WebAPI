using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using OCR.Model;

namespace OCR.AppBack
{
    /// <summary>
    /// 任务实例生成器
    /// </summary>
    public class JobFactory
    {
        public static IJob CreateInstance(OCR.Model.Job.T_OCR_JOBRow row)
        {
            Type job = Type.GetType(row.JOBNAME);
            ConstructorInfo ci = job.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                new Type[] { typeof(OCR.Model.Job.T_OCR_JOBRow)},
                null);
            IJob iJob = (IJob)ci.Invoke(new object[] { row });

            return iJob;
        }
    }
}
