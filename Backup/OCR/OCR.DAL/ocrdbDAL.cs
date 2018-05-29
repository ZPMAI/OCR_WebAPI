using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using OCR.Model;

namespace OCR.DAL
{
    /// <summary>
    /// OCR数据库
    /// </summary>
    public class ocrdbDAL
    {
        private static string SQL_OcrDBPmsServer = @"SELECT * FROM public.v_pms_server";
        private static string SQL_OcrDBCnt = @"SELECT c.dock_id, c.container_no, c.container_shape, c.container_size,
       c.container_height, c.plate_no, c.ctype, c.cweigt, c.dock_status,
       c.container_pos, c.cstatus, c.confidence, c.trvalcrane_id,
       t.trvalcrane_no trval_no, c.driver_no, c.lane_no, c.begin_time,
       c.end_time, c.user_id, c.cnam, c.enam, c.ship_code, c.c_voyage,
       c.pic_num, c.msg_index, c.carcont, c.gangs, c.container_dir,
       c.stream_dir, c.loading_port, c.unloading_port, c.dest_port,
       c.bay_horizontal, c.bay_vertical, c.bay, c.ctime, t.pms_id, null COMMEND_ID, null BERTH_NUM, 'N' isarchived,
       '' SERVICECODE, '' SHIPAGENT, '' SHIPOWNER,plc_open_time,plc_close_time,ident_start_time,ident_end_time
  FROM public.v_port_cont_dock c
  LEFT JOIN public.v_port_travelingcrane t
    ON t.trvalcrane_id = c.trvalcrane_id
  where c.dock_id > 
";
        private static string SQL_OcrDBPhoto = @"select * from public.v_port_photoinfo where dock_id=";

        private static string SQL_OcrDBPhoto1 = @"select * from public.v_port_photoinfo where photo_id>";

        private static string SQL_OcrDBTruck = @"select * from public.v_port_cont_temp where temp_dock_id>";
        
        /// <summary>
        /// 图片服务器表
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, OcrDBPmsServer> GetOcrDBPmsServer()
        {
            using (DataSet ds = NpgSqlHelper.ExecuteDataSet2(SQL_OcrDBPmsServer))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IDictionary<int, OcrDBPmsServer> dic = new Dictionary<int, OcrDBPmsServer>();

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        OcrDBPmsServer data = new OcrDBPmsServer();
                        data.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["i_id"]);
                        data.Name = ds.Tables[0].Rows[0]["c_name"].ToString();
                        data.IndexCode = ds.Tables[0].Rows[0]["c_index_code"].ToString();
                        data.Ip = ds.Tables[0].Rows[0]["i_ip"].ToString();
                        data.Port = ds.Tables[0].Rows[0]["i_port"].ToString();
                        dic.Add(data.Id, data);
                    }

                    return dic;
                }
                
            }

            return null;
        }

        /// <summary>
        /// 读取OCR DB新识别数据
        /// </summary>
        /// <param name="maxid">已同步的最大ID</param>
        public static DataSet GetOcrDBCnt(int maxid)
        {
            string sql = string.Format(@"{0}{1} order by c.dock_id asc ", SQL_OcrDBCnt, maxid);
            return NpgSqlHelper.ExecuteDataSet1(sql);
        }

        /// <summary>
        /// 读取OCR DB图片数据
        /// </summary>
        /// <param name="dock_id">箱号ID</param>
        public static DataSet GetOcrDBPhoto(int dock_id)
        {
            string sql = string.Format(@"{0}{1} order by photo_id asc ", SQL_OcrDBPhoto, dock_id);
            return NpgSqlHelper.ExecuteDataSet1(sql);
        }

        /// <summary>
        /// 读取OCR DB图片数据
        /// </summary>
        /// <param name="photo_id">图片ID</param>
        public static DataSet GetOcrDBPhoto1(int photo_id)
        {
            string sql = string.Format(@"{0}{1} order by photo_id asc ", SQL_OcrDBPhoto1, photo_id);
            return NpgSqlHelper.ExecuteDataSet1(sql);
        }

        /// <summary>
        /// 读取OCR DB车号数据
        /// </summary>
        /// <param name="truck_id">车号ID</param>
        public static DataSet GetOcrDBTruck(int truck_id)
        {
            string sql = string.Format(@"{0}{1} order by temp_dock_id asc ", SQL_OcrDBTruck, truck_id);
            return NpgSqlHelper.ExecuteDataSet1(sql);
        }
    }
}
