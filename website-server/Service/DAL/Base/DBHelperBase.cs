using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DAL.Helper;

namespace DAL.Base
{
    public abstract class DBHelperBase : DBTransactionHelper.DBTransactionBase
    {
        public enum DBType { PostgreSql }
        #region 靜態方法
        private static Dictionary<string, DBHelperBase> commonDBHelper = new Dictionary<string, DBHelperBase>();
        /// <summary>
        /// 创建数据库连接类
        /// </summary>
        public static void CreateDBHelper(string connectionString, DBType dbType)
        {
            if (dbType == DBType.PostgreSql)
            {
                commonDBHelper.Add(connectionString, new PostgreSqlHelper(connectionString));
            }
        }
        /// <summary>
        /// 获得数据库连接类
        /// </summary>
        public static DBHelperBase GetDBHelper(string connectionString, DBType dbType)
        {
            if (commonDBHelper.ContainsKey(connectionString) == false)
                CreateDBHelper(connectionString, dbType);
            return commonDBHelper[connectionString];
        }
        #endregion
        protected string connectionString;
        public DBHelperBase()
        {
        }
        public DBHelperBase(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected abstract DbCommand GetCommand(string cmdText, DbConnection connection);
        protected abstract DbCommand GetCommand(string cmdText, DbConnection connection, DbTransaction transaction);
        protected abstract DbDataAdapter GetDbDataAdapter();
        protected abstract DbCommandBuilder GetDbCommandBuilder(DbDataAdapter adapter);
        public abstract DbParameter NewDbParameter(string ParameterName, DbType DbType, object Value);
        public abstract DbParameter NewDbParameter(string ParameterName, DbType DbType, object Value, int Size);
        public abstract DbParameter NewDbParameterByColumn(string ParameterName, DbType DbType, string SourceColumn);
        /// <summary>
        /// 是否存在事务
        /// </summary>
        public virtual bool HasDBTransactionHelper()
        {
            return dbTransaction != null;
        }
        /// <summary>
        /// 创建事务
        /// </summary>
        public virtual DBTransactionHelper.DBTransactionHelper CreateDBTransactionHelper()
        {
            if (dbTransaction == null)
                return new DBTransactionHelper.DBTransactionHelper(this);
            else
                throw new Exception("不能重复创建事务！");
        }
        public virtual DbType GetDbType(Type t)
        {
            DbType dbType;
            switch (t.Name.ToLower())
            {
                case "int32":
                    dbType = DbType.Int32;
                    break;
                case "string":
                    dbType = DbType.String;
                    break;
                case "boolean":
                    dbType = DbType.Boolean;
                    break;
                case "datetime":
                    dbType = DbType.DateTime;
                    break;
                case "decimal":
                    dbType = DbType.Decimal;
                    break;
                case "float":
                    dbType = DbType.Decimal;
                    break;
                default:
                    if (t.IsEnum)
                    {
                        dbType = DbType.Int32;
                        break;
                    }
                    throw new Exception("DbType转换,未定义类型：" + t.Name);
            }
            return dbType;
        }
        public virtual void AddDbParameters(DbCommand com, DbParameter[] paramenters)
        {
            if (paramenters != null && paramenters.Length > 0)
            {
                foreach (var p in paramenters)
                {
                    if (p.Value == null)
                        p.Value = DBNull.Value;
                    com.Parameters.Add(p);
                }
            }
        }
        public virtual DbDataReader ExecuteReader(string cmdText, DbParameter[] paramenters, int? timeOut = 10000)
        {
            try
            {
                DbConnection conn = null;
                if (dbTransaction != null)
                {
                    conn = dbTransaction.Connection;
                    DbCommand com = GetCommand(cmdText, conn, dbTransaction);
                    AddDbParameters(com, paramenters);
                    if (timeOut != null)
                        com.CommandTimeout = timeOut.Value;
                    return com.ExecuteReader();
                }
                else
                {
                    conn = GetConnection();
                    conn.Open();
                    DbCommand com = GetCommand(cmdText, conn);
                    AddDbParameters(com, paramenters);
                    if (timeOut != null)
                        com.CommandTimeout = timeOut.Value;
                    return com.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public virtual int ExecuteNonQuery(string cmdText, DbParameter[] paramenters, int? timeOut = 10000)
        {
            DbConnection conn = null;
            if (dbTransaction != null)
            {
                try
                {
                    conn = dbTransaction.Connection;
                    DbCommand com = GetCommand(cmdText, conn, dbTransaction);
                    AddDbParameters(com, paramenters);
                    if (timeOut != null)
                        com.CommandTimeout = timeOut.Value;
                    return com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            else
            {
                try
                {
                    conn = GetConnection();
                    conn.Open();
                    DbCommand com = GetCommand(cmdText, conn);
                    AddDbParameters(com, paramenters);
                    if (timeOut != null)
                        com.CommandTimeout = timeOut.Value;
                    return com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        public virtual object ExecuteScalar(string cmdText, DbParameter[] paramenters, int? timeOut = 10000)
        {
            DbConnection conn = null;
            if (dbTransaction != null)
            {
                try
                {
                    conn = dbTransaction.Connection;
                    DbCommand com = GetCommand(cmdText, conn, dbTransaction);
                    AddDbParameters(com, paramenters);
                    if (timeOut != null)
                        com.CommandTimeout = timeOut.Value;
                    return com.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            else
            {
                try
                {
                    conn = GetConnection();
                    conn.Open();
                    DbCommand com = GetCommand(cmdText, conn);
                    AddDbParameters(com, paramenters);
                    if (timeOut != null)
                        com.CommandTimeout = timeOut.Value;
                    return com.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <param name="com"></param>
        /// <param name="dt"></param>
        public virtual DateTime GetDBDateTime()
        {
            return Convert.ToDateTime(ExecuteScalar("select getdate()", null, null));
        }
        public virtual DataTable GetDataTable(string cmdText, DbParameter[] paramenters, int? timeOut)
        {
            try
            {
                using (DbDataReader dr = ExecuteReader(cmdText, paramenters, timeOut))
                {
                    DataTable dt = new DataTable("T1");
                    dt.Load(dr);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="timeOut"></param>
        public virtual int AddDataTable(DataTable dt, int? timeOut)
        {
            DbConnection conn = null;
            if (dbTransaction != null)
            {
                try
                {
                    conn = dbTransaction.Connection;
                    using (DbDataAdapter da = GetDbDataAdapter())
                    {
                        da.InsertCommand = GetCommand(null, conn, dbTransaction);
                        CreateInsertSql(da.InsertCommand, dt);
                        if (timeOut != null)
                            da.InsertCommand.CommandTimeout = timeOut.Value;//秒
                        da.InsertCommand.UpdatedRowSource = UpdateRowSource.None;//批量更新必须
                        //da.UpdateBatchSize = 0;//批量更新最大值
                        dt.SetRowsAdd();
                        return da.Update(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            else
            {
                try
                {
                    conn = GetConnection();
                    conn.Open();
                    int addNum = 0;
                    using (DbTransaction t = conn.BeginTransaction())
                    {
                        using (DbDataAdapter da = GetDbDataAdapter())
                        {
                            da.InsertCommand = GetCommand(null, conn, t);
                            CreateInsertSql(da.InsertCommand, dt);
                            if (timeOut != null)
                                da.InsertCommand.CommandTimeout = timeOut.Value;//秒
                            da.InsertCommand.UpdatedRowSource = UpdateRowSource.None;//批量更新必须
                            //da.UpdateBatchSize = 0;//批量更新最大值
                            dt.SetRowsAdd();
                            addNum = da.Update(dt);
                        }
                        t.Commit();
                    }
                    return addNum;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="timeOut"></param>
        public virtual int UpdateDataTable(DataTable dt, int? timeOut)
        {
            DbConnection conn = null;
            if (dbTransaction != null)
            {
                try
                {
                    conn = dbTransaction.Connection;
                    using (DbDataAdapter da = GetDbDataAdapter())
                    {
                        da.UpdateCommand = GetCommand(null, conn, dbTransaction);
                        CreateUpdateSql(da.UpdateCommand, dt);
                        if (timeOut != null)
                            da.UpdateCommand.CommandTimeout = timeOut.Value;//秒
                        da.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;//批量更新必须
                        //da.UpdateBatchSize = 0;//批量更新最大值
                        dt.SetRowsUpdate();
                        return da.Update(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            else
            {
                try
                {
                    conn = GetConnection();
                    conn.Open();
                    int updateNum = 0;
                    using (DbTransaction t = conn.BeginTransaction())
                    {
                        using (DbDataAdapter da = GetDbDataAdapter())
                        {
                            da.UpdateCommand = GetCommand(null, conn, t);
                            CreateUpdateSql(da.UpdateCommand, dt);
                            if (timeOut != null)
                                da.UpdateCommand.CommandTimeout = timeOut.Value;//秒
                            da.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;//批量更新必须
                            //da.UpdateBatchSize = 0;//批量更新最大值
                            dt.SetRowsUpdate();
                            updateNum = da.Update(dt);
                        }
                        t.Commit();
                    }
                    return updateNum;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="timeOut"></param>
        public virtual int DeleteDataTable(DataTable dt, int? timeOut)
        {
            DbConnection conn = null;
            if (dbTransaction != null)
            {
                try
                {
                    conn = dbTransaction.Connection;
                    using (DbDataAdapter da = GetDbDataAdapter())
                    {
                        da.DeleteCommand = GetCommand(null, conn, dbTransaction);
                        CreateDeleteSql(da.DeleteCommand, dt);
                        if (timeOut != null)
                            da.DeleteCommand.CommandTimeout = timeOut.Value;//秒
                        da.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;//批量更新必须
                        //da.UpdateBatchSize = 0;//批量更新最大值
                        dt.SetRowsDelete();
                        return da.Update(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            else
            {
                try
                {
                    conn = GetConnection();
                    conn.Open();
                    int deleteNum = 0;
                    using (DbTransaction t = conn.BeginTransaction())
                    {
                        using (DbDataAdapter da = GetDbDataAdapter())
                        {
                            da.DeleteCommand = GetCommand(null, conn, t);
                            CreateDeleteSql(da.DeleteCommand, dt);
                            if (timeOut != null)
                                da.DeleteCommand.CommandTimeout = timeOut.Value;//秒
                            da.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;//批量更新必须
                            //da.UpdateBatchSize = 0;//批量更新最大值
                            dt.SetRowsDelete();
                            deleteNum = da.Update(dt);
                        }
                        t.Commit();
                    }
                    return deleteNum;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        /// <summary>
        /// 创建查询sql
        /// </summary>
        /// <param name="tableName"></param>
        public virtual string CreateSelectSql(string tableName)
        {
            string sql = String.Format("select * from {0} ", tableName);
            return sql;
        }
        /// <summary>
        /// 创建查询条记录sql
        /// </summary>
        /// <param name="tableName"></param>
        public virtual string CreateSelectOneSql(string tableName)
        {
            string sql = String.Format("select top 1 * from {0} ", tableName);
            sql += "{0}";//预留where语句位置
            return sql;
        }
        /// <summary>
        /// 创建查询条件sql
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paramenters"></param>
        public virtual string CreateWhereSql(object obj, List<DbParameter> paramenters)
        {
            return CreateWhereSql(obj.ModelToDataTableHasValue(true), paramenters);
        }
        /// <summary>
        /// 创建查询sql
        /// </summary>
        /// <param name="tableName"></param>
        public virtual string CreateCountSql(string tableName)
        {
            string sql = String.Format("select count(0) from {0} ", tableName);
            return sql;
        }
        /// <summary>
        /// 创建查询条件sql
        /// </summary>
        /// <param name="com"></param>
        /// <param name="dt"></param>
        public virtual string CreateWhereSql(DataTable dt, List<DbParameter> paramenters)
        {
            string sqlWhere = "where 1=1 and ";
            foreach (DataColumn col in dt.Columns)
            {
                if (dt.Rows[0][col.ColumnName] == DBNull.Value)
                    continue;
                sqlWhere += dt.TableName + "." + col.ColumnName + "=@" + col.ColumnName + " and ";
                paramenters.Add(NewDbParameter("@" + col.ColumnName, GetDbType(col.DataType), dt.Rows[0][col.ColumnName]));
            }
            if (sqlWhere.Length > 0)
                sqlWhere = sqlWhere.Substring(0, sqlWhere.Length - 4);
            return sqlWhere;
        }
        /// <summary>
        /// 创建插入sql
        /// </summary>
        /// <param name="com"></param>
        /// <param name="dt"></param>
        protected virtual void CreateInsertSql(DbCommand com, DataTable dt)
        {
            string sql = "insert into {0}({1}) values({2})";
            string sqlCol = "";
            string sqlVal = "";
            List<DbParameter> paramenters = new List<DbParameter>();
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName == "CCreateTime")//自动记录创建时间
                {
                    sqlCol += col.ColumnName + ",";
                    sqlVal += "GETDATE(),";
                }
                else
                {
                    sqlCol += col.ColumnName + ",";
                    sqlVal += "@" + col.ColumnName + ",";
                    paramenters.Add(NewDbParameterByColumn("@" + col.ColumnName, GetDbType(col.DataType), col.ColumnName));
                }
            }
            sqlCol = sqlCol.Substring(0, sqlCol.Length - 1);
            sqlVal = sqlVal.Substring(0, sqlVal.Length - 1);
            com.CommandText = String.Format(sql, dt.TableName, sqlCol, sqlVal);
            AddDbParameters(com, paramenters.ToArray());
        }
        /// <summary>
        /// 创建更新sql
        /// </summary>
        /// <param name="com"></param>
        /// <param name="dt"></param>
        protected virtual void CreateUpdateSql(DbCommand com, DataTable dt)
        {
            string sql = "update {0} set {1} where {2}";
            string sqlColVal = "";
            string sqlWhere = "";
            List<DbParameter> paramenters = new List<DbParameter>();
            foreach (DataColumn col in dt.Columns)
            {
                bool ispk = false;
                foreach (DataColumn colpk in dt.PrimaryKey)
                {
                    if (col.ColumnName == colpk.ColumnName)
                    {
                        ispk = true;
                        break;
                    }
                }
                if (ispk == false)
                {
                    sqlColVal += col.ColumnName + "=@" + col.ColumnName + ",";
                }
                else
                {
                    sqlWhere += col.ColumnName + "=@" + col.ColumnName + " and ";
                }
                paramenters.Add(NewDbParameterByColumn("@" + col.ColumnName, GetDbType(col.DataType), col.ColumnName));
            }
            sqlColVal = sqlColVal.Substring(0, sqlColVal.Length - 1);
            sqlWhere = sqlWhere.Substring(0, sqlWhere.Length - 4);
            com.CommandText = String.Format(sql, dt.TableName, sqlColVal, sqlWhere);
            AddDbParameters(com, paramenters.ToArray());
        }
        /// <summary>
        /// 创建删除sql
        /// </summary>
        /// <param name="com"></param>
        /// <param name="dt"></param>
        protected virtual void CreateDeleteSql(DbCommand com, DataTable dt)
        {
            string sql = "delete from {0} where {1}";
            string sqlWhere = "";
            List<DbParameter> paramenters = new List<DbParameter>();
            foreach (DataColumn col in dt.Columns)
            {
                bool ispk = false;
                foreach (DataColumn colpk in dt.PrimaryKey)
                {
                    if (col.ColumnName == colpk.ColumnName)
                    {
                        ispk = true;
                        break;
                    }
                }
                if (ispk)
                {
                    sqlWhere += col.ColumnName + "=@" + col.ColumnName + " and ";
                }
                paramenters.Add(NewDbParameterByColumn("@" + col.ColumnName, GetDbType(col.DataType), col.ColumnName));
            }
            sqlWhere = sqlWhere.Substring(0, sqlWhere.Length - 4);
            com.CommandText = String.Format(sql, dt.TableName, sqlWhere);
            AddDbParameters(com, paramenters.ToArray());
        }
    }
}
