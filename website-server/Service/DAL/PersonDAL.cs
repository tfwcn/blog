using Model.Server.Args;
using Model.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DAL
{
    public class PersonDAL : DALBase<PersonModel>
    {
        public PersonDAL(string connectionString)
        {
            base.SetConnectionString(connectionString, DBHelperBase.DBType.PostgreSql);
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PersonModel GetModel(PersonGetModelRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sql = "select t_person.*,t_company.c_name as c_company_name,t_group.c_name as c_group_name,null as c_in_time,null as c_log_id from t_person ";
            sql += "left join t_company on t_company.c_id=t_person.c_company_id ";
            sql += "left join t_group on t_group.c_id=t_person.c_group_id ";
            string sqlWhere = CreateWhereSql(request, paramenters);
            return base.GetModel(sql, sqlWhere, paramenters);
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<PersonModel> GetList(PersonGetListRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sql = "select t_person.*,t_company.c_name as c_company_name,t_group.c_name as c_group_name,null as c_in_time,null as c_log_id from t_person ";
            sql += "left join t_company on t_company.c_id=t_person.c_company_id ";
            sql += "left join t_group on t_group.c_id=t_person.c_group_id ";
            string sqlWhere = CreateWhereSql(request, paramenters);
            if (!String.IsNullOrEmpty(request.Name))
            {
                sqlWhere += " and t_person.c_name like '%'||@name||'%' ";
                paramenters.Add(dbHelper.NewDbParameter("@name", System.Data.DbType.String, request.Name));
            }
            sqlWhere += " order by t_person.c_update_time desc ";
            return base.GetModels(sql, sqlWhere, paramenters, request.Row, request.Page);
        }
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int GetCount(PersonGetCountRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sqlWhere = CreateWhereSql(request, paramenters);
            if (!String.IsNullOrEmpty(request.Name))
            {
                sqlWhere += " and t_person.c_name like '%'||@name||'%' ";
                paramenters.Add(dbHelper.NewDbParameter("@name", System.Data.DbType.String, request.Name));
            }
            return base.GetCount(sqlWhere, paramenters);
        }
        /// <summary>
        /// 查询在场人员列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<PersonModel> GetInList(PersonGetInListRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sql = "select t_person.*,t_company.c_name as c_company_name,t_group.c_name as c_group_name ";
            sql += ",(select t_log.c_create_time from t_log inner join t_door on t_log.c_door_id=t_door.c_id where t_person.c_id=t_log.c_person_id order by t_log.c_create_time desc limit 1) as c_in_time ";
            sql += ",(select t_log.c_id from t_log inner join t_door on t_log.c_door_id=t_door.c_id where t_person.c_id=t_log.c_person_id order by t_log.c_create_time desc limit 1) as c_log_id ";
            sql += "from t_person ";
            sql += "left join t_company on t_company.c_id=t_person.c_company_id ";
            sql += "left join t_group on t_group.c_id=t_person.c_group_id ";
            string sqlWhere = CreateWhereSql(request, paramenters);
            sqlWhere += "and (select t_door.c_type from t_log inner join t_door on t_log.c_door_id=t_door.c_id where t_person.c_id=t_log.c_person_id and t_log.c_state=0 order by t_log.c_create_time desc limit 1)=0 ";
            sqlWhere += " order by (select t_log.c_create_time from t_log inner join t_door on t_log.c_door_id=t_door.c_id where t_person.c_id=t_log.c_person_id order by t_log.c_create_time desc limit 1) desc ";
            return base.GetModels(sql, sqlWhere, paramenters, null, null);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override int Add(PersonModel request)
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
        public override int Update(PersonModel request)
        {
            request.UpdateTime = DateTime.Now;
            return base.Update(request);
        }
        /// <summary>
        /// 更新黑白名单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int UpdatePermission(PersonUpdatePermissionRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            var sql = "update t_person set c_permission=@permission, c_update_time=now() where c_state=0";
            paramenters.Add(dbHelper.NewDbParameter("@permission", System.Data.DbType.Int32, request.Permission));
            string sqlWhere = "";
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
            if (request.Ids != null && request.Ids.Count > 0)
            {
                string tmpIds = "";
                foreach (var id in request.Ids)
                {
                    tmpIds += "'" + id + "',";
                }
                tmpIds = tmpIds.Substring(0, tmpIds.Length - 1);
                sqlWhere += " and t_person.c_id in(" + tmpIds + ") ";
            }
            return dbHelper.ExecuteNonQuery(sql + sqlWhere, paramenters.ToArray(), null);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override int Delete(PersonModel model)
        {
            var tmpModel = GetModel(new PersonGetModelRequest() { Id = model.Id });
            if (tmpModel == null)
                return 0;
            tmpModel.State = 1;
            return Update(tmpModel);
        }
    }
}
