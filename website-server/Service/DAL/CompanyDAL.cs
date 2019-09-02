using Model.Server.Args;
using Model.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DAL
{
    public class CompanyDAL : DALBase<CompanyModel>
    {
        public CompanyDAL(string connectionString)
        {
            base.SetConnectionString(connectionString, DBHelperBase.DBType.PostgreSql);
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CompanyModel GetModel(CompanyGetModelRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sqlWhere = CreateWhereSql(request, paramenters);
            return base.GetModel(sqlWhere, paramenters);
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<CompanyModel> GetList(CompanyGetListRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sqlWhere = CreateWhereSql(request, paramenters);
            sqlWhere += " order by t_company.c_create_time desc ";
            return base.GetModels(sqlWhere, paramenters, request.Row, request.Page);
        }
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int GetCount(CompanyGetCountRequest request)
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
        public override int Add(CompanyModel request)
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
        public override int Update(CompanyModel request)
        {
            request.UpdateTime = DateTime.Now;
            return base.Update(request);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override int Delete(CompanyModel model)
        {
            var tmpModel = GetModel(new CompanyGetModelRequest() { Id = model.Id });
            if (tmpModel == null)
                return 0;
            tmpModel.State = 1;
            return Update(tmpModel);
        }
    }
}
