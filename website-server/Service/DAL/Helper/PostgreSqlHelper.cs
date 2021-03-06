﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Npgsql;
using System.Data;
using DAL;
using DAL.Base;

namespace DAL.Helper
{
    /// <summary>
    /// 数据库操作基类(for PostgreSQL)
    /// </summary>
    public class PostgreSqlHelper : DBHelperBase
    {
        public PostgreSqlHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public override DbConnection GetConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            return conn;
        }
        protected override DbCommand GetCommand(string cmdText, DbConnection connection)
        {
            NpgsqlConnection conn = connection as NpgsqlConnection;
            NpgsqlCommand com = new NpgsqlCommand(cmdText, conn);
            return com;
        }
        protected override DbCommand GetCommand(string cmdText, DbConnection connection, DbTransaction transaction)
        {
            NpgsqlConnection conn = connection as NpgsqlConnection;
            NpgsqlCommand com = new NpgsqlCommand(cmdText, conn, transaction as NpgsqlTransaction);
            return com;
        }
        public override DbParameter NewDbParameter(string ParameterName, DbType DbType, object Value)
        {
            return NewDbParameter(ParameterName, DbType, Value, -1);
        }
        public override DbParameter NewDbParameter(string ParameterName, DbType DbType, object Value, int Size)
        {
            return new NpgsqlParameter() { ParameterName = ParameterName, DbType = DbType, Value = Value, Size = Size };
        }
        public override DbParameter NewDbParameterByColumn(string ParameterName, DbType DbType, string SourceColumn)
        {
            return new NpgsqlParameter() { ParameterName = ParameterName, DbType = DbType, SourceColumn = SourceColumn };
        }
        protected override DbDataAdapter GetDbDataAdapter()
        {
            return new NpgsqlDataAdapter();
        }
        protected override DbCommandBuilder GetDbCommandBuilder(DbDataAdapter adapter)
        {
            return new NpgsqlCommandBuilder(adapter as NpgsqlDataAdapter);
        }
        /// <summary>
        /// 创建查询条记录sql
        /// </summary>
        /// <param name="tableName"></param>
        public override string CreateSelectOneSql(string tableName)
        {
            string sql = String.Format("select * from {0} ", tableName);
            sql += "{0} ";//预留where语句位置
            sql += "limit 1";
            return sql;
        }
    }
}
