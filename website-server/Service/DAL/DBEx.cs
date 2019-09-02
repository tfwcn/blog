using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace DAL
{
    public static class DBEx
    {
        /// <summary>
        /// 对象转DataTable
        /// </summary>
        public static DataTable ModelToDataTable(this object obj, bool? dbCanWrite)
        {
            Type objType = obj.GetType();
            //获取表名
            string tableName = objType.Name;
            TableAttribute tableAttribute = Attribute.GetCustomAttribute(objType, typeof(TableAttribute)) as TableAttribute;
            if (tableAttribute != null && !String.IsNullOrEmpty(tableAttribute.Name))
            {
                tableName = tableAttribute.Name;
            }
            DataTable dt = new DataTable(tableName);
            List<DataColumn> pkList = new List<DataColumn>();
            foreach (var propertieInfo in objType.GetPropertiesPGS(dbCanWrite))
            {
                //获取字段名
                string colName = propertieInfo.Name;
                KeyAttribute keyAttribute = Attribute.GetCustomAttribute(propertieInfo, typeof(KeyAttribute)) as KeyAttribute;
                ColumnAttribute columnAttribute = Attribute.GetCustomAttribute(propertieInfo, typeof(ColumnAttribute)) as ColumnAttribute;
                if (columnAttribute != null && !String.IsNullOrEmpty(columnAttribute.Name))
                {
                    colName = columnAttribute.Name;
                }
                DataColumn col = dt.Columns.Add(colName, (propertieInfo.PropertyType.IsGenericType && propertieInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? propertieInfo.PropertyType.GetGenericArguments()[0] : propertieInfo.PropertyType);
                if (keyAttribute != null)
                {
                    pkList.Add(col);
                }
            }
            if (pkList.Count > 0)
            {
                dt.PrimaryKey = pkList.ToArray();
            }
            return dt;
        }
        /// <summary>
        /// 对象转DataTable(含值)
        /// </summary>
        public static DataTable ModelToDataTableHasValue(this object obj, bool? dbCanWrite)
        {
            Type objType = obj.GetType();
            DataTable dt = ModelToDataTable(obj, dbCanWrite);
            DataRow dr = dt.NewRow();
            obj.ModelToDataRow(dr, objType.GetPropertiesPGS(dbCanWrite));
            dt.Rows.Add(dr);
            return dt;
        }
        /// <summary>
        /// 获取可读写公共属性
        /// </summary>
        public static PropertyInfo[] GetPropertiesPGS(this Type objType, bool? dbCanWrite)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (var property in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty))
            {
                NotMappedAttribute notMappedAttribute = Attribute.GetCustomAttribute(property, typeof(NotMappedAttribute)) as NotMappedAttribute;
                if (notMappedAttribute != null)
                    continue;
                //关联字段不写近数据库
                DisplayAttribute displayColumnAttribute = Attribute.GetCustomAttribute(property, typeof(DisplayAttribute)) as DisplayAttribute;
                if (dbCanWrite == null || (dbCanWrite == true && displayColumnAttribute == null))
                {
                    properties.Add(property);
                }
            }
            return properties.ToArray();
        }
        /// <summary>
        /// 获取属性值
        /// </summary>
        public static object GetPropertyValue(this object obj, string name)
        {
            Type objType = obj.GetType();
            return objType.GetProperty(name).GetValue(obj);
        }
        /// <summary>
        /// 获取属性标识
        /// </summary>
        public static T GetAttribute<T>(this object obj, string name) where T : class
        {
            Type objType = obj.GetType();
            var property = objType.GetProperty(name);
            if (property == null)
                return null;
            return Attribute.GetCustomAttribute(property, typeof(T)) as T;
        }
        /// <summary>
        /// 获取主键字段名称
        /// </summary>
        public static string GetPKName(this Type objType)
        {
            foreach (var property in objType.GetPropertiesPGS(false))
            {
                KeyAttribute keyAttribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;
                ColumnAttribute columnAttribute = Attribute.GetCustomAttribute(property, typeof(ColumnAttribute)) as ColumnAttribute;
                if (keyAttribute != null)
                {
                    if (columnAttribute != null && !String.IsNullOrEmpty(columnAttribute.Name))
                        return columnAttribute.Name;
                    else
                        return property.Name;
                }
            }
            return null;
        }
        /// <summary>
        /// 对象转DataRow
        /// </summary>
        public static DataRow ModelToDataRow(this object obj, DataRow dr, PropertyInfo[] properties)
        {
            Type objType = obj.GetType();
            foreach (var propertieInfo in properties)
            {
                //获取字段名
                string colName = propertieInfo.Name;
                ColumnAttribute attribute = Attribute.GetCustomAttribute(propertieInfo, typeof(ColumnAttribute)) as ColumnAttribute;
                if (attribute != null && !String.IsNullOrEmpty(attribute.Name))
                {
                    colName = attribute.Name;
                }
                dr[colName] = propertieInfo.GetValue(obj) == null ? DBNull.Value : propertieInfo.GetValue(obj);
            }
            return dr;
        }
        /// <summary>
        /// 对象转DataTable
        /// </summary>
        public static DataTable ListToDataTable<T>(this List<T> list, bool? dbCanWrite) where T : new()
        {
            if (list == null || list.Count <= 0)
            {
                return null;
            }
            T t = list[0];
            DataTable dt = t.ModelToDataTable(dbCanWrite);
            Type objType = t.GetType();
            PropertyInfo[] properties = objType.GetPropertiesPGS(dbCanWrite);
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                item.ModelToDataRow(dr, properties);
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// DataRow转对象
        /// </summary>
        public static T DataRowToModel<T>(this DataRow dr) where T : new()
        {
            PropertyInfo[] properties = typeof(T).GetPropertiesPGS(null);
            return dr.DataRowToModel<T>(properties);
        }
        /// <summary>
        /// DataRow转对象
        /// </summary>
        public static T DataRowToModel<T>(this DataRow dr, PropertyInfo[] properties) where T : new()
        {
            T t = new T();
            foreach (var propertieInfo in properties)
            {
                //获取字段名
                string colName = propertieInfo.Name;
                ColumnAttribute attribute = Attribute.GetCustomAttribute(propertieInfo, typeof(ColumnAttribute)) as ColumnAttribute;
                if (attribute != null && !String.IsNullOrEmpty(attribute.Name))
                {
                    colName = attribute.Name;
                }
                if (dr[colName] == DBNull.Value)
                    propertieInfo.SetValue(t, null);
                else
                    propertieInfo.SetValue(t, dr[colName]);
            }
            return t;
        }
        /// <summary>
        /// DataTable转对象
        /// </summary>
        public static List<T> DataTableToList<T>(this DataTable dt) where T : new()
        {
            if (dt == null)
            {
                return null;
            }
            List<T> list = new List<T>();
            PropertyInfo[] properties = typeof(T).GetPropertiesPGS(null);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row.DataRowToModel<T>(properties));
            }
            return list;
        }
        /// <summary>
        /// 设置行状态为新增
        /// </summary>
        public static void SetRowsAdd(this DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                row.SetAdded();
            }
        }
        /// <summary>
        /// 设置行状态为更新
        /// </summary>
        public static void SetRowsUpdate(this DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                row.SetModified();
            }
        }
        /// <summary>
        /// 设置行状态为刪除
        /// </summary>
        public static void SetRowsDelete(this DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                row.Delete();
            }
        }
    }
}
