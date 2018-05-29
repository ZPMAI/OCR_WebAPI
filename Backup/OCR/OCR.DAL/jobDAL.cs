using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using CCT.SystemFramework.Data;
using OCR.Model;

namespace OCR.DAL
{
    /// <summary>
    /// OCR�Զ�����
    /// </summary>
    public class jobDAL
    {
        /// <summary>
        /// ��ѯ��ִ����ҵ
        /// </summary>
        /// <param name="execTime">ִ��ʱ��</param>
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
        /// �޸��´�ִ��ʱ��
        /// </summary>
        /// <param name="row"></param>
        public static void Update(Job.T_OCR_JOBRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr_job.P_Update_Job", CommandType.StoredProcedure, row);
        }

        /// <summary>
        /// ������ҵ
        /// </summary>
        /// <param name="row"></param>
        public static void LockJob(Job.T_OCR_JOBRow row)
        {
            SqlHelper.Oracle.ExecuteObjectTypedParams(Config.ConnectionString, "OCR.pkg_ocr_job.p_update_jobstate", CommandType.StoredProcedure, row);
        }
    }
}
