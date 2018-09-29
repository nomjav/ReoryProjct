using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace AcademyLockSmith.Data.Helpers
{
	/// <summary>
	///     Excel helper
	/// </summary>
	public class ExcelHelper : IDisposable
	{
		#region Fileds

	    private const string _excelObject = "Provider=Microsoft.{0}.OLEDB.{1};Data Source={2};Extended Properties=\"Excel {3};HDR={4};IMEX={5}\"";
	    private OleDbConnection _con;
		private string _filepath = string.Empty;
		private string _hdr = "No";
		private string _imex = "1";

		#endregion

		#region Ctor

		/// <summary>
		///     Ctor
		/// </summary>
		/// <param name="filepath">Excel file path</param>
		public ExcelHelper(string filepath)
		{
			_filepath = filepath;
		}

		#endregion

		#region Methods

		/// <summary>
		///     Dispose
		/// </summary>
		public void Dispose()
		{
			if (_con != null && _con.State == ConnectionState.Open)
				_con.Close();
			if (_con != null)
				_con.Dispose();
			_con = null;
			_filepath = string.Empty;
		}

		/// <summary>
		///     Gets a schema
		/// </summary>
		/// <returns>Schema</returns>
		public DataTable GetSchema()
		{
			DataTable dtSchema = null;
			if (Connection.State != ConnectionState.Open) Connection.Open();
			dtSchema = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] {null, null, null, "TABLE"});
			return dtSchema;
		}

		/// <summary>
		///     Read all table rows
		/// </summary>
		/// <param name="tableName">Table Name</param>
		/// <returns>Table</returns>
		public DataTable ReadTable(string tableName)
		{
			return ReadTable(tableName, ExcelHelperReadTableMode.ReadFromWorkSheet);
		}

		/// <summary>
		///     Read table
		/// </summary>
		/// <param name="tableName">Table Name</param>
		/// <param name="mode">Read mode</param>
		/// <returns>Table</returns>
		public DataTable ReadTable(string tableName, ExcelHelperReadTableMode mode)
		{
			return ReadTable(tableName, mode, "");
		}

		/// <summary>
		///     Read table
		/// </summary>
		/// <param name="tableName">Table Name</param>
		/// <param name="mode">Read mode</param>
		/// <param name="criteria">Criteria</param>
		/// <returns>Table</returns>
		public DataTable ReadTable(string tableName, ExcelHelperReadTableMode mode, string criteria)
		{
			if (Connection.State != ConnectionState.Open)
			{
				Connection.Open();
			}
			string cmdText = "Select * from [{0}]";
			if (!string.IsNullOrEmpty(criteria))
			{
				cmdText += " Where " + criteria;
			}
			string tableNameSuffix = string.Empty;
			if (mode == ExcelHelperReadTableMode.ReadFromWorkSheet)
				tableNameSuffix = "$";

			OleDbCommand cmd = new OleDbCommand(string.Format(cmdText, tableName + tableNameSuffix));
			cmd.Connection = Connection;
			OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

			DataSet ds = new DataSet();

			adpt.Fill(ds, tableName);

			if (ds.Tables.Count >= 1)
			{
				return ds.Tables[0];
			}
		    return null;
		}

		/// <summary>
		///     Drop table
		/// </summary>
		/// <param name="tableName">Table Name</param>
		public void DropTable(string tableName)
		{
			if (Connection.State != ConnectionState.Open)
			{
				Connection.Open();
			}
			string cmdText = "Drop Table [{0}]";
			using (OleDbCommand cmd = new OleDbCommand(string.Format(cmdText, tableName), Connection))
			{
				cmd.ExecuteNonQuery();
			}
			Connection.Close();
		}

		/// <summary>
		///     Write table
		/// </summary>
		/// <param name="tableName">Table Name</param>
		/// <param name="tableDefinition">Table Definition</param>
		public void WriteTable(string tableName, Dictionary<string, string> tableDefinition)
		{
			using (OleDbCommand cmd = new OleDbCommand(GenerateCreateTable(tableName, tableDefinition), Connection))
			{
				if (Connection.State != ConnectionState.Open) Connection.Open();
				cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		///     Add new row
		/// </summary>
		/// <param name="dr">Data Row</param>
		public void AddNewRow(DataRow dr)
		{
			string command = GenerateInsertStatement(dr);
			ExecuteCommand(command);
		}

		/// <summary>
		///     Execute new command
		/// </summary>
		/// <param name="command">Command</param>
		public void ExecuteCommand(string command)
		{
			using (OleDbCommand cmd = new OleDbCommand(command, Connection))
			{
				if (Connection.State != ConnectionState.Open) Connection.Open();
				cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		///     Generates create table script
		/// </summary>
		/// <param name="tableName">Table Name</param>
		/// <param name="tableDefinition">Table Definition</param>
		/// <returns>Create table script</returns>
		private string GenerateCreateTable(string tableName, Dictionary<string, string> tableDefinition)
		{
			StringBuilder sb = new StringBuilder();
			bool firstcol = true;
			sb.AppendFormat("CREATE TABLE [{0}](", tableName);
			firstcol = true;
			foreach (KeyValuePair<string, string> keyvalue in tableDefinition)
			{
				if (!firstcol)
				{
					sb.Append(",");
				}
				firstcol = false;
				sb.AppendFormat("{0} {1}", keyvalue.Key, keyvalue.Value);
			}

			sb.Append(")");
			return sb.ToString();
		}

		/// <summary>
		///     Generates insert statement script
		/// </summary>
		/// <param name="dr">Data row</param>
		/// <returns>Insert statement script</returns>
		private string GenerateInsertStatement(DataRow dr)
		{
			StringBuilder sb = new StringBuilder();
			bool firstcol = true;
			sb.AppendFormat("INSERT INTO [{0}](", dr.Table.TableName);


			foreach (DataColumn dc in dr.Table.Columns)
			{
				if (!firstcol)
				{
					sb.Append(",");
				}
				firstcol = false;

				sb.Append(dc.Caption);
			}

			sb.Append(") VALUES(");
			firstcol = true;
			for (int i = 0; i <= dr.Table.Columns.Count - 1; i++)
			{
				if (!ReferenceEquals(dr.Table.Columns[i].DataType, typeof (int)))
				{
					sb.Append("'");
					sb.Append(dr[i].ToString().Replace("'", "''"));
					sb.Append("'");
				}
				else
				{
					sb.Append(dr[i].ToString().Replace("'", "''"));
				}
				if (i != dr.Table.Columns.Count - 1)
				{
					sb.Append(",");
				}
			}

			sb.Append(")");
			return sb.ToString();
		}

		#endregion

		#region Properties

		/// <summary>
		///     Gets connection string
		/// </summary>
		public string ConnectionString
		{
			get
			{
				string result = string.Empty;
				if (String.IsNullOrEmpty(_filepath))
					return result;

				//Check for File Format
				FileInfo fi = new FileInfo(_filepath);
				if (fi.Extension.Equals(".xls"))
				{
					result = string.Format(_excelObject, "Jet", "4.0", _filepath, "8.0", _hdr, _imex);
				}
				else if (fi.Extension.Equals(".xlsx"))
				{
					result = string.Format(_excelObject, "Ace", "12.0", _filepath, "12.0", _hdr, _imex);
				}
				return result;
			}
		}

		/// <summary>
		///     Gets connection
		/// </summary>
		public OleDbConnection Connection
		{
			get { return _con ?? (_con = new OleDbConnection {ConnectionString = ConnectionString}); }
		}

		/// <summary>
		///     Gets or sets a HDR
		/// </summary>
		public string Hdr
		{
			get { return _hdr; }
			set { _hdr = value; }
		}

		/// <summary>
		///     Gets or sets an IMEX
		/// </summary>
		public string Imex
		{
			get { return _imex; }
			set { _imex = value; }
		}

		#endregion
	}

	/// <summary>
	///     Represents a read table mode for ExcelHelper.ReadTable method
	/// </summary>
	public enum ExcelHelperReadTableMode
	{
		/// <summary>
		///     Read rows from all filled excel worksheet cells
		/// </summary>
		ReadFromWorkSheet,

		/// <summary>
		///     Read rows only from named range
		/// </summary>
		ReadFromNamedRange
	}
}
