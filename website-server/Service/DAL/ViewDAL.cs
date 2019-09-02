using Model.Server.Models;
using System.Collections.Generic;
using System.Data.Common;

namespace DAL
{
    public class ViewDAL : DALBase<object>
    {
        public ViewDAL(string connectionString)
        {
            base.SetConnectionString(connectionString, DBHelperBase.DBType.PostgreSql);
        }
        /// <summary>
        /// 获取在场人数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<PersonNumModel> GetPersonNum()
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sql = "select CAST(sum((case when t2.c_type=0 then 1 else -1 end)) as int) as c_num, ";
            sql += "(case when t3.c_type is null then t1.c_person_type else t3.c_type end) as c_type ";
            sql += "from t_log as t1 ";
            sql += "inner join t_door as t2 on t1.c_door_id=t2.c_id ";
            sql += "left join t_person as t3 on t1.c_person_id=t3.c_id ";
            sql += "where t1.c_type in(0,2,4) and t1.c_state=0 and (t3.c_id is null or (t3.c_id is not null and t3.c_state=0)) ";
            sql += "group by (case when t3.c_type is null then t1.c_person_type else t3.c_type end) ";
            sql += "order by (case when t3.c_type is null then t1.c_person_type else t3.c_type end) ";
            return base.dbHelper.GetDataTable(sql, null, null).DataTableToList<PersonNumModel>();
        }
    }
}
