using Model.Server.Args;
using Model.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DAL
{
    public class LogDAL : DALBase<LogModel>
    {
        public LogDAL(string connectionString)
        {
            base.SetConnectionString(connectionString, DBHelperBase.DBType.PostgreSql);
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public LogModel GetModel(LogGetModelRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sql = "select t_log.*,t_door.c_name as c_door_name,t_door.c_type as c_door_type, ";
            sql += "t_company.c_name as c_company_name,t_group.c_name as c_group_name,t_person.c_name as c_person_name,t_person.c_card as c_person_card ";
            sql += "from t_log ";
            sql += "left join t_door on t_door.c_id=t_log.c_door_id ";
            sql += "left join t_person on t_person.c_id=t_log.c_person_id ";
            sql += "left join t_company on t_company.c_id=t_person.c_company_id ";
            sql += "left join t_group on t_group.c_id=t_person.c_group_id ";
            string sqlWhere = CreateWhereSql(request, paramenters);
            sqlWhere += " order by t_log.c_create_time desc ";
            return base.GetModel(sql, sqlWhere, paramenters);
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<LogModel> GetList(LogGetListRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sql = "select t_log.*,t_door.c_name as c_door_name,t_door.c_type as c_door_type, ";
            sql += "t_company.c_name as c_company_name,t_group.c_name as c_group_name,t_person.c_name as c_person_name,t_person.c_card as c_person_card ";
            sql += "from t_log ";
            sql += "left join t_door on t_door.c_id=t_log.c_door_id ";
            sql += "left join t_person on t_person.c_id=t_log.c_person_id ";
            sql += "left join t_company on t_company.c_id=t_person.c_company_id ";
            sql += "left join t_group on t_group.c_id=t_person.c_group_id ";
            string sqlWhere = CreateWhereSql(request, paramenters);
            if (request.StartTime != null)
            {
                sqlWhere += " and t_log.c_create_time>=@start_time ";
                paramenters.Add(dbHelper.NewDbParameter("@start_time", System.Data.DbType.DateTime, request.StartTime));
            }
            if (request.EndTime != null)
            {
                sqlWhere += " and t_log.c_create_time<=@end_time ";
                paramenters.Add(dbHelper.NewDbParameter("@end_time", System.Data.DbType.DateTime, request.EndTime));
            }
            if (request.DoorType != null)
            {
                sqlWhere += " and t_door.c_type=@door_type ";
                paramenters.Add(dbHelper.NewDbParameter("@door_type", System.Data.DbType.Int32, request.DoorType));
            }
            if (request.PersonName != null)
            {
                sqlWhere += " and t_person.c_name like '%'||@person_name||'%' ";
                paramenters.Add(dbHelper.NewDbParameter("@person_name", System.Data.DbType.String, request.PersonName));
            }
            if (request.CompanyId != null)
            {
                sqlWhere += " and t_person.c_company_id=@person_company_id ";
                paramenters.Add(dbHelper.NewDbParameter("@person_company_id", System.Data.DbType.String, request.CompanyId));
            }
            if (request.GroupId != null)
            {
                sqlWhere += " and t_person.c_group_id=@person_group_id ";
                paramenters.Add(dbHelper.NewDbParameter("@person_group_id", System.Data.DbType.String, request.GroupId));
            }
            if (!String.IsNullOrEmpty(request.Types))
            {
                sqlWhere += " and t_log.c_type in(" + request.Types + ") ";
            }
            if (request.IsDel == true)
            {
                sqlWhere += " and (t_person.c_id is null or (t_person.c_id is not null and t_person.c_state=0)) ";
            }
            sqlWhere += " order by t_log.c_create_time desc ";
            return base.GetModels(sql, sqlWhere, paramenters, request.Row, request.Page);
        }
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int GetCount(LogGetCountRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sql = "select count(0) ";
            sql += "from t_log ";
            sql += "left join t_person on t_person.c_id=t_log.c_person_id ";
            sql += "left join t_door on t_door.c_id=t_log.c_door_id ";
            sql += "left join t_company on t_company.c_id=t_person.c_company_id ";
            sql += "left join t_group on t_group.c_id=t_person.c_group_id ";
            string sqlWhere = CreateWhereSql(request, paramenters);
            if (request.StartTime != null)
            {
                sqlWhere += " and t_log.c_create_time>=@start_time ";
                paramenters.Add(dbHelper.NewDbParameter("@start_time", System.Data.DbType.DateTime, request.StartTime));
            }
            if (request.EndTime != null)
            {
                sqlWhere += " and t_log.c_create_time<=@end_time ";
                paramenters.Add(dbHelper.NewDbParameter("@end_time", System.Data.DbType.DateTime, request.EndTime));
            }
            if (request.DoorType != null)
            {
                sqlWhere += " and t_door.c_type=@door_type ";
                paramenters.Add(dbHelper.NewDbParameter("@door_type", System.Data.DbType.Int32, request.DoorType));
            }
            if (request.PersonName != null)
            {
                sqlWhere += " and t_person.c_name like '%'||@person_name||'%' ";
                paramenters.Add(dbHelper.NewDbParameter("@person_name", System.Data.DbType.String, request.PersonName));
            }
            if (request.CompanyId != null)
            {
                sqlWhere += " and t_person.c_company_id=@person_company_id ";
                paramenters.Add(dbHelper.NewDbParameter("@person_company_id", System.Data.DbType.String, request.CompanyId));
            }
            if (request.GroupId != null)
            {
                sqlWhere += " and t_person.c_group_id=@person_group_id ";
                paramenters.Add(dbHelper.NewDbParameter("@person_group_id", System.Data.DbType.String, request.GroupId));
            }
            if (!String.IsNullOrEmpty(request.Types))
            {
                sqlWhere += " and t_log.c_type in(" + request.Types + ") ";
            }
            if (request.IsDel == true)
            {
                sqlWhere += " and (t_person.c_id is null or (t_person.c_id is not null and t_person.c_state=0)) ";
            }
            return base.GetCount(sql, sqlWhere, paramenters);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override int Add(LogModel request)
        {
            request.State = 0;
            request.CreateTime = DateTime.Now;
            request.UpdateTime = DateTime.Now;
            return base.Add(request);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override int Update(LogModel request)
        {
            request.UpdateTime = DateTime.Now;
            return base.Update(request);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override int Delete(LogModel model)
        {
            var tmpModel = GetModel(new LogGetModelRequest() { Id = model.Id });
            if (tmpModel == null)
                return 0;
            tmpModel.State = 1;
            return Update(tmpModel);
        }
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteAll()
        {
            var sql = "update t_log set c_state=1,c_update_time=now() where c_state=0";
            return dbHelper.ExecuteNonQuery(sql, null, null);
        }
    }
}
