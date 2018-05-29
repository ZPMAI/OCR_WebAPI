using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using CCT.SystemFramework.Data;
using OCR.Model;

namespace OCR.DAL
{
    /// <summary>
    /// OCR自动任务
    /// </summary>
    public class jobDAL
    {
        /// <summary>
        /// 查询待执行作业
        /// </summary>
        /// <param name="execTime">执行时间</param>
        /// <returns></returns>
        public static Job.T_OCR_JOBDataTable SelectAll(DateTime execTime)
        {
            IDbCommand command = SqlHelper.Oracle.CreateDbCommand(Config.ConnectionString, "OCR.pkg_ocr_job.P_Select_Job", CommandType.StoredProcedure);
            ((IDataParameter)command.Parameters["p_nextexectime"]).Value = execTime;

            Job ds = new Job();
            SqlHelper.Oracle.FillDataSet(Config.ConnectionString, command, ds, new string[] { ds.T_OCR_JOB.TableName });
            return ds.T_OCR_JOB;
        }

        /// <summary>
        /// 修改下次执行时间
        /// </summary>
        /// <param name="row"></param>
        public static void Update(Job.T_OCR_JOBRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr_job.P_Update_Job", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// 锁定作业
        /// </summary>
        /// <param name="row"></param>
        public static void LockJob(Job.T_OCR_JOBRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr_job.p_update_jobstate", CommandType.StoredProcedure, row);
        }
    }
}
