using System;
using System.Data;
using System.Collections;

namespace CCT.SystemFramework.Data
{
	/// <summary>
	/// IRunner ��ժҪ˵����
	/// </summary>
	public interface IDbRunner
	{
		#region ���ݴ洢�����������ɴ������� IDbCommand ����

		/// <summary>
		/// ���ݴ洢�����������ɴ������� IDbCommand ����
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandText">���ݿ��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <returns>IDbCommand ����</returns>
		IDbCommand CreateDbCommand(string connectionString, string commandText, CommandType commandType);

		#endregion


		#region ��� DataSet ����

		/// <summary>
		/// ��� DataSet ����
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <param name="dataSet">DataSet ����</param>
		void FillDataSet(string connectionString, string commandText, CommandType commandType, DataSet dataSet);
		

		/// <summary>
		/// ��� DataSet ����
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="command">IDbCommand ����</param>
		/// <param name="dataSet">DataSet ����</param>
		void FillDataSet(string connectionString, IDbCommand command, DataSet dataSet);
		
		/// <summary>
		/// ��� DataSet ����
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="command">IDbCommand ����</param>
		/// <param name="dataSet">DataSet ����</param>
		/// <param name="tableNames">����</param>
		void FillDataSet(string connectionString, IDbCommand command, DataSet dataSet, string[] tableNames);

		/// <summary>
		/// ��� DataSet ����
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <param name="dataSet">DataSet ����</param>
		void FillDataSet(IDbTransaction transaction, string commandText, CommandType commandType, DataSet dataSet);		


		/// <summary>
		/// ��� DataSet ����
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="command">IDbCommand ����</param>
		/// <param name="dataSet">DataSet ����</param>
		void FillDataSet(IDbTransaction transaction, IDbCommand command, DataSet dataSet);

		#endregion

		#region ִ�����ݿ������������κβ���

		/// <summary>
		/// ִ�����ݿ������������κβ���
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		bool ExecuteNonQuery(string connectionString, string commandText, CommandType commandType);


		/// <summary>
		/// ִ�����ݿ������������κβ���
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="command">IDbCommand ����</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		bool ExecuteNonQuery(string connectionString, IDbCommand command);


		/// <summary>
		/// ִ�����ݿ������������κβ���
		/// </summary>
		/// <param name="connection">���ݿ����Ӷ���</param>
		/// <param name="command">IDbCommand ����</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		bool ExecuteNonQuery(IDbConnection connection, IDbCommand command);


		/// <summary>
		/// ִ�����ݿ������������κβ���
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		bool ExecuteNonQuery(IDbTransaction transaction, string commandText, CommandType commandType);


		/// <summary>
		/// ִ�����ݿ������������κβ���
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="command">IDbCommand ����</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		bool ExecuteNonQuery(IDbTransaction transaction, IDbCommand command);		

		#endregion

		#region ִ�����ݿ����������� DataSet ����

		/// <summary>
		/// ִ�����ݿ����������� DataSet ����
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <returns>DataSet ����</returns>
		DataSet ExecuteDataSet(string connectionString, string commandText, CommandType commandType);		


		/// <summary>
		/// ִ�����ݿ����������� DataSet ����
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="command">IDbCommand ����</param>
		/// <returns>DataSet ����</returns>
		DataSet ExecuteDataSet(string connectionString, IDbCommand command);


        /// <summary>
        /// ִ�����ݿ����������� DataSet ����
        /// </summary>
        /// <param name="command">IDbCommand ����</param>
        /// <returns>DataSet ����</returns>
        DataSet ExecuteDataSet(IDbCommand command);
		

		/// <summary>
		/// ִ�����ݿ����������� DataSet ����
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <returns>DataSet ����</returns>
		DataSet ExecuteDataSet(IDbTransaction transaction, string commandText, CommandType commandType);


		/// <summary>
		/// ִ�����ݿ����������� DataSet ����
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="command">IDbCommand ����</param>
		/// <returns>DataSet ����</returns>
		DataSet ExecuteDataSet(IDbTransaction transaction, IDbCommand command);	
		
		#endregion

		#region ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��

		/// <summary>
		/// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <returns>������еĵ�һ�еĵ�һ��</returns>
		object ExecuteScalar(string connectionString, string commandText, CommandType commandType);


		/// <summary>
		/// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="command">IDbCommand ����</param>
		/// <returns>������еĵ�һ�еĵ�һ��</returns>
		object ExecuteScalar(string connectionString, IDbCommand command);


		/// <summary>
		/// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <returns>������еĵ�һ�еĵ�һ��</returns>
		object ExecuteScalar(IDbTransaction transaction, string commandText, CommandType commandType);
		

		/// <summary>
		/// ִ�����ݿ����������ؽ�����еĵ�һ�еĵ�һ��
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="command">IDbCommand ����</param>
		/// <returns>������еĵ�һ�еĵ�һ��</returns>
		object ExecuteScalar(IDbTransaction transaction, IDbCommand command);		


		/// <summary>
		/// ִ�����ݿ����������� IDataReader ����
		/// </summary>		
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <returns>IDataReader ����</returns>
		IDataReader ExecuteReader(string connectionString, string commandText, CommandType commandType);
		
		#endregion

		#region ִ�����ݿ����������� IDataReader ����

		/// <summary>
		/// ִ�����ݿ����������� IDataReader ����
		/// </summary>		
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="command">Command����</param>
		/// <returns>IDataReader ����</returns>
		IDataReader ExecuteReader(string connectionString, IDbCommand command);


		/// <summary>
		/// ִ�����ݿ����������� IDataReader ����
		/// </summary>		
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="command">Command����</param>
		/// <param name="commandBehavior">�ṩ�Բ�ѯ����Ͳ�ѯ�����ݿ��Ӱ���˵��</param>
		/// <returns>IDataReader ����</returns>
		IDataReader ExecuteReader(string connectionString, IDbCommand command, CommandBehavior commandBehavior);		
		

		/// <summary>
		/// ִ�����ݿ����������� IDataReader ����
		/// </summary>		
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <returns>IDataReader ����</returns>
		IDataReader ExecuteReader(IDbTransaction transaction, string commandText, CommandType commandType);		


		/// <summary>
		/// ִ�����ݿ����������� IDataReader ����
		/// </summary>		
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="oleDbCommand">OleDbCommand����</param>
		/// <returns>IDataReader ����</returns>
		IDataReader ExecuteReader(IDbTransaction transaction, IDbCommand command);


		/// <summary>
		/// ִ�����ݿ����������� IDataReader ����
		/// </summary>		
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="oleDbCommand">OleDbCommand����</param>
		/// <param name="commandBehavior">�ṩ�Բ�ѯ����Ͳ�ѯ�����ݿ��Ӱ���˵��</param>
		/// <returns>IDataReader ����</returns>
		IDataReader ExecuteReader(IDbTransaction transaction, IDbCommand command, CommandBehavior commandBehavior);
		
		#endregion


		#region ���ݴ洢���������� DataTable ����ִ�����ݿ��������

		/// <summary>
		/// ���ݴ洢���������� DataTable ����ִ�����ݿ��������
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandText">���ݿ��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <param name="dataTable">DataTable ����</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		bool ExecuteDataTableTypedParams(string connectionString, string commandText, CommandType commandType, DataTable dataTable);
		

		/// <summary>
		/// ���ݴ洢���������� DataTable ����ִ�����ݿ��������
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="commandText">���ݿ��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <param name="dataTable">DataTable ����</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		bool ExecuteDataTableTypedParams(IDbTransaction transaction, string commandText, CommandType commandType, DataTable dataTable);

		#endregion

		#region ��ҳ��ѯ���������� DataSet��

		/// <summary>
		/// ��ҳ��ѯ���������� DataSet��
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="execSql">T-SQL ���ݿ���������ַ���</param>
		/// <param name="currentPage">��ǰҳ��</param>
		/// <param name="pageSize">ҳ���С</param>
		/// <param name="recordCount">�ܼ�¼��</param>
		/// <param name="pageCount">��ҳ��</param>
		/// <param name="dataSet">DataSet ����</param>
		void SplitPage(string connectionString, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, DataSet dataSet);


		/// <summary>
		/// ��ҳ��ѯ���������� DataSet��
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="execSql">T-SQL ���ݿ���������ַ���</param>
		/// <param name="currentPage">��ǰҳ��</param>
		/// <param name="pageSize">ҳ���С</param>
		/// <param name="recordCount">�ܼ�¼��</param>
		/// <param name="pageCount">��ҳ��</param>
		/// <param name="dataSet">DataSet ����</param>
		void SplitPage(IDbTransaction transaction, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, DataSet dataSet);
		
		#endregion


		#region ���ݴ洢����������ʵ�����ִ�����ݿ��������

		/// <summary>
		/// ���ݴ洢����������ʵ�����ִ�����ݿ��������
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandText">���ݿ��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <param name="model">ʵ�����</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		bool ExecuteObjectTypedParams(string connectionString, string commandText, CommandType commandType, object model);

		/// <summary>
		/// ���ݴ洢����������ʵ�����ִ�����ݿ��������
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="commandText">���ݿ��������</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <param name="model">ʵ�����</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		bool ExecuteObjectTypedParams(IDbTransaction transaction, string commandText, CommandType commandType, object model);
		
		#endregion

		#region ��ҳ��ѯ����������ʵ�����

		/// <summary>
		/// ��ҳ��ѯ����������ʵ�����
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="execSql">T-SQL ���ݿ���������ַ���</param>
		/// <param name="currentPage">��ǰҳ��</param>
		/// <param name="pageSize">ҳ���С</param>
		/// <param name="pageCount">��ҳ��</param>
		/// <param name="dataSet">DataSet ����</param>
		IList SplitPage(string connectionString, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, Type type);

		/// <summary>
		/// ��ҳ��ѯ����������ʵ�����
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="execSql">T-SQL ���ݿ���������ַ���</param>
		/// <param name="currentPage">��ǰҳ��</param>
		/// <param name="pageSize">ҳ���С</param>
		/// <param name="pageCount">��ҳ��</param>
		/// <param name="dataSet">DataSet ����</param>
		IList SplitPage(IDbTransaction transaction, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, Type type);

		#endregion

		#region ִ�����ݿ�����������ʵ����󼯺�

		/// <summary>
		/// ִ�����ݿ�����������ʵ����󼯺�
		/// </summary>		
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="type">����ģ�Ͷ�������</param>
		/// <returns>ʵ����󼯺�</returns>
		IList ExecuteModel(string connectionString, string commandText, CommandType commandType, Type type);

		
		/// <summary>
		/// ִ�����ݿ�����������ʵ����󼯺�
		/// </summary>		
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="command">IDbCommand ����</param>
		/// <param name="type">����ģ�Ͷ�������</param>
		/// <returns>ʵ����󼯺�</returns>
		IList ExecuteModel(string connectionString, IDbCommand command, Type type);

		
		/// <summary>
		/// ִ�����ݿ�����������ʵ����󼯺�
		/// </summary>		
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="commandType">���ݿ������������</param>
		/// <param name="commandText">T-SQL ���ݿ���������ַ������ߴ洢��������</param>
		/// <param name="type">����ģ�Ͷ�������</param>
		/// <returns>ʵ����󼯺�</returns>
		IList ExecuteModel(IDbTransaction transaction, string commandText, CommandType commandType, Type type);


		/// <summary>
		/// ִ�����ݿ�����������ʵ����󼯺�
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="command">IDbCommand ����</param>
		/// <param name="type">����ģ�Ͷ�������</param>
		/// <returns>ʵ����󼯺�</returns>
		IList ExecuteModel(IDbTransaction transaction, IDbCommand command, Type type);

		#endregion


		#region ��ȡ���ݿ�����

		/// <summary>
		/// ��ȡ���ݿ�����
		/// </summary>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="transaction">���ݿ�����</param>
		IDbTransaction BeginDbTransaction(string connectionString);

		#endregion

		#region �ύ���ݿ�����

		/// <summary>
		/// �ύ���ݿ�����
		/// </summary>		
		/// <param name="transaction">���ݿ�����</param>
		/// <returns>�����Ƿ�ɹ�</returns>
		bool CommitDbTransaction(IDbTransaction transaction);

		#endregion

		#region �ع����ݿ�����

		/// <summary>
		/// �ع����ݿ�����
		/// </summary>		
		void RollBackDbTransaction(IDbTransaction transaction);

		#endregion
	}
}
