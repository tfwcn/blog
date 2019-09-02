using Model.Server.Args;
using Model.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DAL
{
    public class GroupDAL : DALBase<GroupModel>
    {
        public GroupDAL(string connectionString)
        {
            base.SetConnectionString(connectionString, DBHelperBase.DBType.PostgreSql);
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GroupModel GetModel(GroupGetModelRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sql = "select t_group.*,t_company.c_name as c_company_name from t_group ";
            sql += "left join t_company on t_company.c_id=t_group.c_company_id ";
            string sqlWhere = CreateWhereSql(request, paramenters);
            return base.GetModel(sql, sqlWhere, paramenters);
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<GroupModel> GetList(GroupGetListRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sql = "select t_group.*,t_company.c_name as c_company_name from t_group ";
            sql += "left join t_company on t_company.c_id=t_group.c_company_id ";
            string sqlWhere = CreateWhereSql(request, paramenters);
            sqlWhere += " order by t_group.c_create_time desc ";
            return base.GetModels(sql, sqlWhere, paramenters, request.Row, request.Page);
        }
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int GetCount(GroupGetCountRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sqlWhere = CreateWhereSql(request, paramenters);
            return base.GetCount(sqlWhere, paramenters);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override int Add(GroupModel request)
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
        public override int Update(GroupModel request)
        {
            request.UpdateTime = DateTime.Now;
            return base.Update(request);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override int Delete(GroupModel model)
        {
            var tmpModel = GetModel(new GroupGetModelRequest() { Id = model.Id });
            if (tmpModel == null)
                return 0;
            tmpModel.State = 1;
            return Update(tmpModel);
        }
    }
}
