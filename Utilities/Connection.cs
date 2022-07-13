using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Utilities
{
	public class Connection
	{
		public string ConnectionString { get; set; }
		protected SqlConnection DataConnection { get; set; }
		public Connection(string connectionString)
		{
			ConnectionString = connectionString;
			GetConnection();
		}
		public SqlConnection GetConnection()
		{
			if (DataConnection == null)
			{
				DataConnection = new SqlConnection(ConnectionString);
				DataConnection.Open();
			}
			return DataConnection;
		}

		public SqlParameter GetParameter(string parameter, object value)
		{
			SqlParameter parameterObject = new SqlParameter(parameter, value != null ? value : DBNull.Value)
			{
				Direction = ParameterDirection.Input
			};

			return parameterObject;
		}

		public DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
		{
			SqlCommand command = new SqlCommand(commandText, connection as SqlConnection)
			{
				CommandType = commandType
			};
			return command;
		}

		public DataTable ExecuteDataTableSqlDA(CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
		{
			DataTable dt = new DataTable();

			using (SqlConnection connection = GetConnection())
			{
				SqlCommand comando = new SqlCommand(cmdText, connection)
				{
					CommandType = CommandType.StoredProcedure
				};
				comando.Parameters.AddRange(cmdParms);
				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = comando;
				da.Fill(dt);
			}

			return dt;
		}
		public int ExecuteNonQuery(string procedureName, SqlParameter[] parameters, CommandType commandType = CommandType.StoredProcedure)
		{
			int returnValue = -1;

				using (SqlConnection connection = GetConnection())
				{
					DbCommand cmd = GetCommand(connection, procedureName, commandType);

					if (parameters != null && parameters.Length > 0)
					{
						cmd.Parameters.AddRange(parameters);
					}

					returnValue = cmd.ExecuteNonQuery();
				}

			return returnValue;
		}
	}
}
