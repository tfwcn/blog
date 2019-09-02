using Model.Server.Args;
using Model.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DAL
{
    public class DoorDAL : DALBase<DoorModel>
    {
        public DoorDAL(string connectionString)
        {
            base.SetConnectionString(connectionString, DBHelperBase.DBType.PostgreSql);
        }
        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DoorModel GetModel(DoorGetModelRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            //string sql = "select t_door.*,t_gate.c_name as c_gate_name from t_door ";
            //sql += "left join t_gate on t_gate.c_id=t_door.c_gate_id ";
            string sqlWhere = CreateWhereSql(request, paramenters);
            return base.GetModel(sqlWhere, paramenters);
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<DoorModel> GetList(DoorGetListRequest request)
        {
            List<DbParameter> paramenters = new List<DbParameter>();
            //string sql = "select t_door.*,t_gate.c_name as c_gate_name from t_door ";
            //sql += "left join t_gate on t_gate.c_id=t_door.c_gate_id ";
            string sqlWhere = CreateWhereSql(request, paramenters);
            sqlWhere += " order by t_door.c_create_time desc ";
            return base.GetModels(sqlWhere, paramenters, request.Row, request.Page);
        }
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int GetCount(DoorGetCountRequest request)
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
        public override int Add(DoorModel request)
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
        public override int Update(DoorModel request)
        {
            request.UpdateTime = DateTime.Now;
            return base.Update(request);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override int Delete(DoorModel model)
        {
            var tmpModel = GetModel(new DoorGetModelRequest() { Id = model.Id });
            if (tmpModel == null)
                return 0;
            tmpModel.State = 1;
            return Update(tmpModel);
        }
    }
}
