using DAL.Base;
using Model.Server.Args;
using Model.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DAL
{
    public class NewsDAL : DALBase<NewsModel>
    {
        public NewsDAL(string connectionString)
        {
            base.SetConnectionString(connectionString, DBHelperBase.DBType.PostgreSql);
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public NewsModel GetModel(NewsGetModelRequest request)
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
        public List<NewsModel> GetList(NewsGetListRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            string sqlWhere = CreateWhereSql(request, paramenters);
            sqlWhere += " order by t_news.c_create_time desc ";
            return base.GetModels(sqlWhere, paramenters, request.Rows, request.Page);
        }
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int GetCount(NewsGetCountRequest request)
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
        public override int Add(NewsModel request)
        {
            request.Id = Guid.NewGuid().ToString();
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
        public override int Update(NewsModel request)
        {
            request.UpdateTime = DateTime.Now;
            return base.Update(request);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override int Delete(NewsModel model)
        {
            var tmpModel = GetModel(new NewsGetModelRequest() { Id = model.Id });
            if (tmpModel == null)
                return 0;
            tmpModel.State = 1;
            return Update(tmpModel);
        }
    }
}
