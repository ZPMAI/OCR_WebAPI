using System;
using System.Data;
using System.Collections;

namespace CCT.SystemFramework.Data
{
	/// <summary>
	/// IRunner 的摘要说明。
	/// </summary>
	public interface IDbRunner
	{
		#region 根据存储过程名称生成带参数的 IDbCommand 对象

		/// <summary>
		/// 根据存储过程名称生成带参数的 IDbCommand 对象
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandText">数据库操作命令</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <returns>IDbCommand 对象</returns>
		IDbCommand CreateDbCommand(string connectionString, string commandText, CommandType commandType);

		#endregion


		#region 填充 DataSet 对象

		/// <summary>
		/// 填充 DataSet 对象
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <param name="dataSet">DataSet 对象</param>
		void FillDataSet(string connectionString, string commandText, CommandType commandType, DataSet dataSet);
		

		/// <summary>
		/// 填充 DataSet 对象
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <param name="dataSet">DataSet 对象</param>
		void FillDataSet(string connectionString, IDbCommand command, DataSet dataSet);
		
		/// <summary>
		/// 填充 DataSet 对象
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <param name="dataSet">DataSet 对象</param>
		/// <param name="tableNames">表名</param>
		void FillDataSet(string connectionString, IDbCommand command, DataSet dataSet, string[] tableNames);

		/// <summary>
		/// 填充 DataSet 对象
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <param name="dataSet">DataSet 对象</param>
		void FillDataSet(IDbTransaction transaction, string commandText, CommandType commandType, DataSet dataSet);		


		/// <summary>
		/// 填充 DataSet 对象
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <param name="dataSet">DataSet 对象</param>
		void FillDataSet(IDbTransaction transaction, IDbCommand command, DataSet dataSet);

		#endregion

		#region 执行数据库操作命令不返回任何参数

		/// <summary>
		/// 执行数据库操作命令不返回任何参数
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <returns>操作是否成功</returns>
		bool ExecuteNonQuery(string connectionString, string commandText, CommandType commandType);


		/// <summary>
		/// 执行数据库操作命令不返回任何参数
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <returns>操作是否成功</returns>
		bool ExecuteNonQuery(string connectionString, IDbCommand command);


		/// <summary>
		/// 执行数据库操作命令不返回任何参数
		/// </summary>
		/// <param name="connection">数据库连接对象</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <returns>操作是否成功</returns>
		bool ExecuteNonQuery(IDbConnection connection, IDbCommand command);


		/// <summary>
		/// 执行数据库操作命令不返回任何参数
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <returns>操作是否成功</returns>
		bool ExecuteNonQuery(IDbTransaction transaction, string commandText, CommandType commandType);


		/// <summary>
		/// 执行数据库操作命令不返回任何参数
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <returns>操作是否成功</returns>
		bool ExecuteNonQuery(IDbTransaction transaction, IDbCommand command);		

		#endregion

		#region 执行数据库操作命令并返回 DataSet 对象

		/// <summary>
		/// 执行数据库操作命令并返回 DataSet 对象
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <returns>DataSet 对象</returns>
		DataSet ExecuteDataSet(string connectionString, string commandText, CommandType commandType);		


		/// <summary>
		/// 执行数据库操作命令并返回 DataSet 对象
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <returns>DataSet 对象</returns>
		DataSet ExecuteDataSet(string connectionString, IDbCommand command);


        /// <summary>
        /// 执行数据库操作命令并返回 DataSet 对象
        /// </summary>
        /// <param name="command">IDbCommand 对象</param>
        /// <returns>DataSet 对象</returns>
        DataSet ExecuteDataSet(IDbCommand command);
		

		/// <summary>
		/// 执行数据库操作命令并返回 DataSet 对象
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <returns>DataSet 对象</returns>
		DataSet ExecuteDataSet(IDbTransaction transaction, string commandText, CommandType commandType);


		/// <summary>
		/// 执行数据库操作命令并返回 DataSet 对象
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <returns>DataSet 对象</returns>
		DataSet ExecuteDataSet(IDbTransaction transaction, IDbCommand command);	
		
		#endregion

		#region 执行数据库操作命令并返回结果集中的第一行的第一列

		/// <summary>
		/// 执行数据库操作命令并返回结果集中的第一行的第一列
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <returns>结果集中的第一行的第一列</returns>
		object ExecuteScalar(string connectionString, string commandText, CommandType commandType);


		/// <summary>
		/// 执行数据库操作命令并返回结果集中的第一行的第一列
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <returns>结果集中的第一行的第一列</returns>
		object ExecuteScalar(string connectionString, IDbCommand command);


		/// <summary>
		/// 执行数据库操作命令并返回结果集中的第一行的第一列
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <returns>结果集中的第一行的第一列</returns>
		object ExecuteScalar(IDbTransaction transaction, string commandText, CommandType commandType);
		

		/// <summary>
		/// 执行数据库操作命令并返回结果集中的第一行的第一列
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <returns>结果集中的第一行的第一列</returns>
		object ExecuteScalar(IDbTransaction transaction, IDbCommand command);		


		/// <summary>
		/// 执行数据库操作命令并返回 IDataReader 对象
		/// </summary>		
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <returns>IDataReader 对象</returns>
		IDataReader ExecuteReader(string connectionString, string commandText, CommandType commandType);
		
		#endregion

		#region 执行数据库操作命令并返回 IDataReader 对象

		/// <summary>
		/// 执行数据库操作命令并返回 IDataReader 对象
		/// </summary>		
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="command">Command对象</param>
		/// <returns>IDataReader 对象</returns>
		IDataReader ExecuteReader(string connectionString, IDbCommand command);


		/// <summary>
		/// 执行数据库操作命令并返回 IDataReader 对象
		/// </summary>		
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="command">Command对象</param>
		/// <param name="commandBehavior">提供对查询结果和查询对数据库的影响的说明</param>
		/// <returns>IDataReader 对象</returns>
		IDataReader ExecuteReader(string connectionString, IDbCommand command, CommandBehavior commandBehavior);		
		

		/// <summary>
		/// 执行数据库操作命令并返回 IDataReader 对象
		/// </summary>		
		/// <param name="transaction">数据库事务</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <returns>IDataReader 对象</returns>
		IDataReader ExecuteReader(IDbTransaction transaction, string commandText, CommandType commandType);		


		/// <summary>
		/// 执行数据库操作命令并返回 IDataReader 对象
		/// </summary>		
		/// <param name="transaction">数据库事务</param>
		/// <param name="oleDbCommand">OleDbCommand对象</param>
		/// <returns>IDataReader 对象</returns>
		IDataReader ExecuteReader(IDbTransaction transaction, IDbCommand command);


		/// <summary>
		/// 执行数据库操作命令并返回 IDataReader 对象
		/// </summary>		
		/// <param name="transaction">数据库事务</param>
		/// <param name="oleDbCommand">OleDbCommand对象</param>
		/// <param name="commandBehavior">提供对查询结果和查询对数据库的影响的说明</param>
		/// <returns>IDataReader 对象</returns>
		IDataReader ExecuteReader(IDbTransaction transaction, IDbCommand command, CommandBehavior commandBehavior);
		
		#endregion


		#region 根据存储过程名称与 DataTable 对象执行数据库操作命令

		/// <summary>
		/// 根据存储过程名称与 DataTable 对象执行数据库操作命令
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandText">数据库操作命令</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <param name="dataTable">DataTable 对象</param>
		/// <returns>操作是否成功</returns>
		bool ExecuteDataTableTypedParams(string connectionString, string commandText, CommandType commandType, DataTable dataTable);
		

		/// <summary>
		/// 根据存储过程名称与 DataTable 对象执行数据库操作命令
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="commandText">数据库操作命令</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <param name="dataTable">DataTable 对象</param>
		/// <returns>操作是否成功</returns>
		bool ExecuteDataTableTypedParams(IDbTransaction transaction, string commandText, CommandType commandType, DataTable dataTable);

		#endregion

		#region 分页查询操作（返回 DataSet）

		/// <summary>
		/// 分页查询操作（返回 DataSet）
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="execSql">T-SQL 数据库操作命令字符串</param>
		/// <param name="currentPage">当前页码</param>
		/// <param name="pageSize">页面大小</param>
		/// <param name="recordCount">总记录数</param>
		/// <param name="pageCount">总页数</param>
		/// <param name="dataSet">DataSet 对象</param>
		void SplitPage(string connectionString, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, DataSet dataSet);


		/// <summary>
		/// 分页查询操作（返回 DataSet）
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="execSql">T-SQL 数据库操作命令字符串</param>
		/// <param name="currentPage">当前页码</param>
		/// <param name="pageSize">页面大小</param>
		/// <param name="recordCount">总记录数</param>
		/// <param name="pageCount">总页数</param>
		/// <param name="dataSet">DataSet 对象</param>
		void SplitPage(IDbTransaction transaction, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, DataSet dataSet);
		
		#endregion


		#region 根据存储过程名称与实体对象执行数据库操作命令

		/// <summary>
		/// 根据存储过程名称与实体对象执行数据库操作命令
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandText">数据库操作命令</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <param name="model">实体对象</param>
		/// <returns>操作是否成功</returns>
		bool ExecuteObjectTypedParams(string connectionString, string commandText, CommandType commandType, object model);

		/// <summary>
		/// 根据存储过程名称与实体对象执行数据库操作命令
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="commandText">数据库操作命令</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <param name="model">实体对象</param>
		/// <returns>操作是否成功</returns>
		bool ExecuteObjectTypedParams(IDbTransaction transaction, string commandText, CommandType commandType, object model);
		
		#endregion

		#region 分页查询操作（返回实体对象）

		/// <summary>
		/// 分页查询操作（返回实体对象）
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="execSql">T-SQL 数据库操作命令字符串</param>
		/// <param name="currentPage">当前页码</param>
		/// <param name="pageSize">页面大小</param>
		/// <param name="pageCount">总页数</param>
		/// <param name="dataSet">DataSet 对象</param>
		IList SplitPage(string connectionString, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, Type type);

		/// <summary>
		/// 分页查询操作（返回实体对象）
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="execSql">T-SQL 数据库操作命令字符串</param>
		/// <param name="currentPage">当前页码</param>
		/// <param name="pageSize">页面大小</param>
		/// <param name="pageCount">总页数</param>
		/// <param name="dataSet">DataSet 对象</param>
		IList SplitPage(IDbTransaction transaction, string execSql, int currentPage, int pageSize, out int recordCount, out int pageCount, Type type);

		#endregion

		#region 执行数据库操作命令并返回实体对象集合

		/// <summary>
		/// 执行数据库操作命令并返回实体对象集合
		/// </summary>		
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="type">数据模型对象类型</param>
		/// <returns>实体对象集合</returns>
		IList ExecuteModel(string connectionString, string commandText, CommandType commandType, Type type);

		
		/// <summary>
		/// 执行数据库操作命令并返回实体对象集合
		/// </summary>		
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <param name="type">数据模型对象类型</param>
		/// <returns>实体对象集合</returns>
		IList ExecuteModel(string connectionString, IDbCommand command, Type type);

		
		/// <summary>
		/// 执行数据库操作命令并返回实体对象集合
		/// </summary>		
		/// <param name="transaction">数据库事务</param>
		/// <param name="commandType">数据库操作命令类型</param>
		/// <param name="commandText">T-SQL 数据库操作命令字符串或者存储过程名称</param>
		/// <param name="type">数据模型对象类型</param>
		/// <returns>实体对象集合</returns>
		IList ExecuteModel(IDbTransaction transaction, string commandText, CommandType commandType, Type type);


		/// <summary>
		/// 执行数据库操作命令并返回实体对象集合
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="command">IDbCommand 对象</param>
		/// <param name="type">数据模型对象类型</param>
		/// <returns>实体对象集合</returns>
		IList ExecuteModel(IDbTransaction transaction, IDbCommand command, Type type);

		#endregion


		#region 获取数据库事务

		/// <summary>
		/// 获取数据库事务
		/// </summary>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="transaction">数据库事务</param>
		IDbTransaction BeginDbTransaction(string connectionString);

		#endregion

		#region 提交数据库事务

		/// <summary>
		/// 提交数据库事务
		/// </summary>		
		/// <param name="transaction">数据库事务</param>
		/// <returns>操作是否成功</returns>
		bool CommitDbTransaction(IDbTransaction transaction);

		#endregion

		#region 回滚数据库事务

		/// <summary>
		/// 回滚数据库事务
		/// </summary>		
		void RollBackDbTransaction(IDbTransaction transaction);

		#endregion
	}
}
