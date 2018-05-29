using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Npgsql;

namespace OCR.DAL
{
    /// <summary>
    /// PostgreSQL
    /// </summary>
    public class NpgSqlHelper
    {
        private static string CONN_STRING1 = "Server=10.1.0.92;Port=5432;User Id=ocr_view;Password=ocr_view;Database=ivms_port;";
        private static string CONN_STRING2 = "Server=10.1.0.92;Port=5432;User Id=ocr_view;Password=ocr_view;Database=cms_db;";

        //private static string CONN_STRING1 = "Server=172.18.15.88;Port=5432;User Id=ocr_view;Password=ocr_view;Database=ivms_port;";
        //private static string CONN_STRING2 = "Server=172.18.15.88;Port=5432;User Id=ocr_view;Password=ocr_view;Database=cms_db;";

        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <returns></returns>
        public static NpgsqlConnection OpenConn(int db)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(db == 1 ? CONN_STRING1 : CONN_STRING2);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// 查询到DATASET ivms_port
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet1(string sql)
        {
            NpgsqlConnection conn = OpenConn(1);
            try
            {
                DataSet ds = new DataSet();
                NpgsqlDataAdapter objAdapter = new NpgsqlDataAdapter(sql, conn);
                objAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行SQL语句 ivms_port
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNoneQuery1(string sql)
        {
            NpgsqlConnection conn = OpenConn(1);
            try
            {
                NpgsqlCommand objCommand = new NpgsqlCommand(sql, conn);
                return objCommand.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 查询到DATASET cms_db
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet2(string sql)
        {
            NpgsqlConnection conn = OpenConn(2);
            try
            {
                DataSet ds = new DataSet();
                NpgsqlDataAdapter objAdapter = new NpgsqlDataAdapter(sql, conn);
                objAdapter.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
