using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;

namespace DAL
{
    public abstract class DALBase<T> where T : new()
    {
        public string ConnectionString { get; set; }
        protected DBHelperBase dbHelper;
        public DALBase()
        {
        }
        /// <summary>
        /// 设置连接字符串
        /// </summary>
        public void SetConnectionString(string connectionString, DBHelperBase.DBType dbType)
        {
            ConnectionString = connectionString;
            dbHelper = DBHelperBase.GetDBHelper(connectionString, dbType);
            //获取表名
            TableAttribute tableAttribute = Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute)) as TableAttribute;
            if (tableAttribute != null && !String.IsNullOrEmpty(tableAttribute.Name))
            {
                tableName = tableAttribute.Name;
            }
        }
        protected string tableName = typeof(T).Name;
        /// <summary>
        /// 获取数据
        /// </summary>
        public virtual T GetModel(string id)
        {
            //获取主键名
            string pkName = typeof(T).GetPKName();
            List<DbParameter> paramenters = new List<DbParameter>();
            paramenters.Add(dbHelper.NewDbParameter(String.Format("@{0}", pkName), DbType.String, id, 36));
            return GetModel(String.Format("where {0}=@{0}", pkName), paramenters);
        }
        /// <summary>
        /// 获取数据(抽象方法)
        /// </summary>
        /// <param name="whereStr">格式：where ... order by ...</param>
        protected virtual T GetModel(string whereStr, List<DbParameter> paramenters)
        {
            return GetModel(null, whereStr, paramenters, null);
        }
        /// <summary>
        /// 获取数据(抽象方法)
        /// </summary>
        /// <param name="selectStr">格式：select ... from ...</param>
        /// <param name="whereStr">格式：where ... order by ...</param>
        protected virtual T GetModel(string selectStr, string whereStr, List<DbParameter> paramenters)
        {
            return GetModel(selectStr, whereStr, paramenters, null);
        }
        protected virtual T GetModel(string selectStr, string whereStr, List<DbParameter> paramenters, int? timeOut)
        {
            string sqlStr;
            if (selectStr == null)
            {
                selectStr = dbHelper.CreateSelectOneSql(tableName);
                sqlStr = String.Format(selectStr, whereStr);
            }
            else
            {
                sqlStr = String.Format("{0} {1} ", selectStr, whereStr);
                sqlStr += "limit 1";
            }
            if (paramenters == null)
                paramenters = new List<DbParameter>();
            DataTable dt = dbHelper.GetDataTable(sqlStr, paramenters.ToArray(), timeOut);
            dt.TableName = tableName;
            if (dt.Rows.Count == 0)
                return default(T);
            T model = dt.Rows[0].DataRowToModel<T>();
            return model;
        }
        /// <summary>
        /// 获取数据列表(抽象方法)
        /// </summary>
        /// <param name="whereStr">格式：where ... order by ...</param>
        protected virtual List<T> GetModels(string whereStr, List<DbParameter> paramenters, int? row, int? page)
        {
            return GetModels(null, whereStr, paramenters, row, page, null);
        }
        /// <summary>
        /// 获取数据列表(抽象方法)
        /// </summary>
        /// <param name="selectStr">格式：select ... from ...</param>
        /// <param name="whereStr">格式：where ... order by ...</param>
        protected virtual List<T> GetModels(string selectStr, string whereStr, List<DbParameter> paramenters, int? row, int? page)
        {
            return GetModels(selectStr, whereStr, paramenters, row, page, null);
        }
        protected virtual List<T> GetModels(string selectStr, string whereStr, List<DbParameter> paramenters, int? row, int? page, int? timeOut)
        {
            if (selectStr == null)
                selectStr = dbHelper.CreateSelectSql(tableName);
            string sqlStr = String.Format("{0} {1} ", selectStr, whereStr);
            if (row != null && page != null && row > 0 && page > 0)
            {
                sqlStr += String.Format("limit {0} offset {1}", row, (page - 1) * row);
            }
            if (paramenters == null)
                paramenters = new List<DbParameter>();
            DataTable dt = dbHelper.GetDataTable(sqlStr, paramenters.ToArray(), timeOut);
            dt.TableName = tableName;
            List<T> list = dt.DataTableToList<T>();
            return list;
        }
        /// <summary>
        /// 查询数量(抽象方法)
        /// </summary>
        /// <param name="whereStr">格式：where ... order by ...</param>
        protected virtual int GetCount(string whereStr, List<DbParameter> paramenters)
        {
            return GetCount(null, whereStr, paramenters, null);
        }
        /// <summary>
        /// 查询数量(抽象方法)
        /// </summary>
        /// <param name="selectStr">格式：select ... from ...</param>
        /// <param name="whereStr">格式：where ... order by ...</param>
        protected virtual int GetCount(string selectStr, string whereStr, List<DbParameter> paramenters)
        {
            return GetCount(selectStr, whereStr, paramenters, null);
        }
        /// <summary>
        /// 查询数量(抽象方法)
        /// </summary>
        /// <param name="selectStr">格式：select ... from ...</param>
        /// <param name="whereStr">格式：where ... order by ...</param>
        protected virtual int GetCount(string selectStr, string whereStr, List<DbParameter> paramenters, int? timeOut)
        {
            if (selectStr == null)
                selectStr = dbHelper.CreateCountSql(tableName);
            string sqlStr = String.Format("{0} {1} ", selectStr, whereStr);
            if (paramenters == null)
                paramenters = new List<DbParameter>();
            int count = Convert.ToInt32(dbHelper.ExecuteScalar(sqlStr, paramenters.ToArray(), timeOut));
            return count;
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        public virtual int Add(T model)
        {
            List<T> list = new List<T>();
            list.Add(model);
            return Add(list);
        }
        public virtual int Add(List<T> list)
        {
            return dbHelper.AddDataTable(list.ListToDataTable<T>(true), null);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public virtual int Update(T model)
        {
            List<T> list = new List<T>();
            list.Add(model);
            return Update(list);
        }
        public virtual int Update(List<T> list)
        {
            return dbHelper.UpdateDataTable(list.ListToDataTable<T>(true), null);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public virtual int Delete(T model)
        {
            List<T> list = new List<T>();
            list.Add(model);
            return Delete(list);
        }
        public virtual int Delete(List<T> list)
        {
            return dbHelper.DeleteDataTable(list.ListToDataTable<T>(true), null);
        }
        /// <summary>
        /// 创建事务
        /// </summary>
        public DBTransactionHelper.DBTransactionHelper CreateDBTransactionHelper()
        {
            return dbHelper.CreateDBTransactionHelper();
        }
        /// <summary>
        /// 创建查询条件sql
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paramenters"></param>
        protected virtual string CreateWhereSql(object request, List<DbParameter> paramenters)
        {
            return dbHelper.CreateWhereSql(request, paramenters);
        }
    }
}
