using System;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Drawing;

namespace CCT.SystemFramework.Grid
{
	/// <summary>
	/// ��ʽ��DataGrid
	/// </summary>
	public class DataGridStyle
	{
		public DataGridStyle()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����DataGrid��ʽ��ʹ�п���ڸ������Ԫ��
		/// <summary>
		/// ʹ�п���ڸ������Ԫ�񳤣�����������ʾ������
		/// </summary>
		/// <param name="dg"></param>
		/// <param name="Len">Ϊ0��ʾ��ʾ��Ϊ1��ʾ����</param>
		public static void SetStyle(DataGrid dg, params int[] Len)
		{  
			if (dg.DataSource == null) return;
			
			DataTable dt;
			switch (dg.DataSource.GetType().ToString())
			{
				case "System.Data.DataSet":
					dt = ((DataSet)dg.DataSource).Tables[dg.DataMember];
					break;
				case "System.Data.DataView":
					dt = ((DataView)dg.DataSource).Table;
					break;
				case "System.Data.DataTable":
					dt = (DataTable)dg.DataSource;
					break;
				default:
					dt = (DataTable)dg.DataSource;
					break;
			}

			DataGridTableStyle style = new DataGridTableStyle();
			style.MappingName = dg.DataSource.ToString();
			style.AlternatingBackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			style.BackColor = System.Drawing.SystemColors.Info;
			style.GridLineColor = System.Drawing.SystemColors.HotTrack;
			style.SelectionBackColor = Color.Red;

			ColumnInfo[] mColumnInfo = new ColumnInfo[dt.Columns.Count];
			for (int i = 0; i < dt.Columns.Count; i++)
			{
				ColumnInfo ci = new ColumnInfo();
				ci.Name = dt.Columns[i].ColumnName;
				ci.DataType = dt.Columns[i].DataType.ToString();
				ci.CompareStringLength(ci.Name);
				mColumnInfo[i] = ci;
			}
			foreach (DataRow dr in dt.Rows)
			{
				for (int i = 0; i < dt.Columns.Count; i++)
				{
					if (!dr.IsNull(i))
					{
						mColumnInfo[i].CompareStringLength(dr[i].ToString());
					}
				}

			}

			DataGridColumnStyle col;
			for(int i = 0; i < dt.Columns.Count; i++) 
			{
				if (dt.Columns[i].DataType.FullName == "System.Boolean") 
				{
					col = new DataGridBoolColumn(); 
				}
				else 
				{
					col = new DataGridTextBoxColumn(); 
				}
				col.MappingName = mColumnInfo[i].Name;
				col.HeaderText = mColumnInfo[i].Name; 
				col.Width = (int)mColumnInfo[i].Width(dg, Len[i]);
				col.Alignment = HorizontalAlignment.Left;
				col.NullText = string.Empty;
				style.GridColumnStyles.Add(col); 
			}

			dg.TableStyles.Clear(); 
			dg.TableStyles.Add(style);
			dg.ReadOnly = true;
			dg.BackgroundColor = Color.White;
			dg.AllowSorting = true;
		}

        //�����ֶ���
        private const string regEx = @"^[\u4e00-\u9fa5]+[\w\W\u4e00-\u9fa5]*$";

        /// <summary>
        /// ����Caption�Ƿ������ģ������Ƿ���ʾ����
        /// </summary>
        /// <param name="dg"></param>
        public static void SetStyleByCaption(DataGrid dg)
        {
            if (dg.DataSource == null) return;

            DataTable dt;
            switch (dg.DataSource.GetType().ToString())
            {
                case "System.Data.DataSet":
                    dt = ((DataSet)dg.DataSource).Tables[dg.DataMember];
                    break;
                case "System.Data.DataView":
                    dt = ((DataView)dg.DataSource).Table;
                    break;
                case "System.Data.DataTable":
                    dt = (DataTable)dg.DataSource;
                    break;
                default:
                    dt = (DataTable)dg.DataSource;
                    break;
            }

            DataGridTableStyle style = new DataGridTableStyle();
            style.MappingName = dg.DataSource.ToString();
            style.AlternatingBackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
            style.BackColor = System.Drawing.SystemColors.Info;
            style.GridLineColor = System.Drawing.SystemColors.HotTrack;
            style.SelectionBackColor = Color.Red;

            ColumnInfo[] mColumnInfo = new ColumnInfo[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ColumnInfo ci = new ColumnInfo();
                ci.Name = dt.Columns[i].ColumnName;                
                ci.DataType = dt.Columns[i].DataType.ToString();
                ci.CompareStringLength(ci.Name);
                ci.Caption = dt.Columns[i].Caption;
                mColumnInfo[i] = ci;
            }
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (!dr.IsNull(i))
                    {
                        mColumnInfo[i].CompareStringLength(dr[i].ToString());
                    }
                }

            }

            DataGridColumnStyle col;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].DataType.FullName == "System.Boolean")
                {
                    col = new DataGridBoolColumn();
                }
                else
                {
                    col = new DataGridTextBoxColumn();
                }
                col.MappingName = mColumnInfo[i].Name;
                col.HeaderText = mColumnInfo[i].Caption;
                col.Alignment = HorizontalAlignment.Left;
                col.NullText = string.Empty;
                
                //���������������������
                bool isChinese = System.Text.RegularExpressions.Regex.IsMatch(col.HeaderText, regEx);
                if (!isChinese)
                {
                    col.Width = (int)mColumnInfo[i].Width(dg, 1);
                }
                else
                {
                    col.Width = (int)mColumnInfo[i].Width(dg, 0);
                }
                style.GridColumnStyles.Add(col);
            }

            dg.TableStyles.Clear();
            dg.TableStyles.Add(style);
            dg.ReadOnly = true;
            dg.BackgroundColor = Color.White;
            dg.AllowSorting = true;
        }

        /// <summary>
        /// ����Caption�Ƿ������ģ������Ƿ���ʾ����
        /// </summary>
        /// <param name="dg"></param>
        public static void SetStyleByCaption(DataGridView dg)
        {
            if (dg.DataSource == null) return;

            foreach (DataGridViewColumn col in dg.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                //���������������������
                bool isChinese = System.Text.RegularExpressions.Regex.IsMatch(col.HeaderText, regEx);
                if (!isChinese)
                {
                    col.Visible = false;
                }
            }

            dg.ReadOnly = true;
            dg.AllowUserToAddRows = false;
        }

		/// <summary>
		/// ʹ�п���ڸ������Ԫ��,��������
		/// </summary>
		/// <param name="dg"></param>
		public static void SetStyle(DataGrid dg)
		{  
			if (dg.DataSource == null) return;
			
			DataTable dt;
			switch (dg.DataSource.GetType().ToString())
			{
				case "System.Data.DataSet":
					dt = ((DataSet)dg.DataSource).Tables[dg.DataMember];
					break;
				case "System.Data.DataView":
					dt = ((DataView)dg.DataSource).Table;
					break;
				case "System.Data.DataTable":
					dt = (DataTable)dg.DataSource;
					break;
				default:
					dt = (DataTable)dg.DataSource;
					break;
			}

			DataGridTableStyle style = new DataGridTableStyle();
			style.MappingName = dg.DataSource.ToString();
			style.AlternatingBackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			style.BackColor = System.Drawing.SystemColors.Info;
			style.GridLineColor = System.Drawing.SystemColors.HotTrack;
			style.SelectionBackColor = Color.Red;

			ColumnInfo[] mColumnInfo = new ColumnInfo[dt.Columns.Count];
			for (int i = 0; i < dt.Columns.Count; i++)
			{
				ColumnInfo ci = new ColumnInfo();
				ci.Name = dt.Columns[i].ColumnName;
				ci.DataType = dt.Columns[i].DataType.ToString();
				ci.CompareStringLength(ci.Name);
				mColumnInfo[i] = ci;
			}
			foreach (DataRow dr in dt.Rows)
			{
				for (int i = 0; i < dt.Columns.Count; i++)
				{
					if (!dr.IsNull(i))
					{
						mColumnInfo[i].CompareStringLength(dr[i].ToString());
					}
				}

			}

			DataGridColumnStyle col;
			for(int i = 0; i < dt.Columns.Count; i++) 
			{
				if (dt.Columns[i].DataType.FullName == "System.Boolean") 
				{
					col = new DataGridBoolColumn(); 
				}
				else 
				{
					col = new DataGridTextBoxColumn(); 
				}
				col.MappingName = mColumnInfo[i].Name;
				col.HeaderText = mColumnInfo[i].Name; 
				col.Alignment = HorizontalAlignment.Left;
				col.NullText = string.Empty;
				style.GridColumnStyles.Add(col); 
			}

			dg.TableStyles.Clear(); 
			dg.TableStyles.Add(style);
			dg.ReadOnly = true;
			dg.BackgroundColor = Color.White;
			dg.AllowSorting = true;
		}

		#endregion

		#region ColumnInfo

		private class ColumnInfo
		{
			public string Name;
			public string DataType;
            public string Caption;
			public int MaxWidth = 0;
			public void CompareStringLength(string mString)
			{
				int mLength = System.Text.Encoding.Default.GetBytes(mString).Length;
				MaxWidth = MaxWidth < mLength ? mLength : MaxWidth;
				
			}
			public float Width(DataGrid dg,int len)
			{
				Graphics mGraphics = dg.CreateGraphics();
				
				StringBuilder str = new StringBuilder();
				for(int i = 0; i < MaxWidth; i++)
				{
					str.Append("A");
				}

				float mColWidth = mGraphics.MeasureString(str.ToString(), dg.Font).Width + 5;
				if (len == 0)
				{
					return (int)mColWidth;
				}
				else if (len > mColWidth)
				{
					return (int)mColWidth;
				}
				else
				{
					return len;
				}
			}
			public float Width(DataGrid dg)
			{
				Graphics mGraphics = dg.CreateGraphics();
				StringBuilder str = new StringBuilder();
				for(int i = 0; i < MaxWidth; i++)
				{
					str.Append("A");
				}
				float mColWidth = mGraphics.MeasureString(str.ToString(), dg.Font).Width + 5;
				return (int)mColWidth;
			}
		}

		#endregion
	}
}
