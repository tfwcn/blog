using DAL;
using Model.Server.Models;
using Model.Server.Args;
using Model.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private AppSettings Config;
        private static GroupDAL dal;
        public GroupController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new GroupDAL(Config.ConnectionString);
        }
        // Post: api/Group/model
        [HttpPost("model")]
        public ServerResponse<GroupModel> GetModel(GroupGetModelRequest request)
        {
            ServerResponse<GroupModel> response = new ServerResponse<GroupModel>();
            try
            {
                var model = dal.GetModel(request);
                response.Data = model;
                if (model != null)
                {
                    response.Code = ServerResponseType.成功;
                }
                else
                {
                    response.Code = ServerResponseType.空数据;
                }
            }
            catch (Exception ex)
            {
                response.Code = ServerResponseType.调用异常;
                response.ErrorMsg = ex.ToString();
                Log.LogHelper.WriteErrorLog(GetType(), ex);
            }
            return response;
        }

        // Post: api/Group/list
        [HttpPost("list")]
        public ServerResponse<List<GroupModel>> GetList(GroupGetListRequest request)
        {
            ServerResponse<List<GroupModel>> response = new ServerResponse<List<GroupModel>>();
            try
            {
                var model = dal.GetList(request);
                response.Data = model;
                response.Code = ServerResponseType.成功;
            }
            catch (Exception ex)
            {
                response.Code = ServerResponseType.调用异常;
                response.ErrorMsg = ex.ToString();
                Log.LogHelper.WriteErrorLog(GetType(), ex);
            }
            return response;
        }

        // Post: api/Group/count
        [HttpPost("count")]
        public ServerResponse<int> GetCount(GroupGetCountRequest request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                var model = dal.GetCount(request);
                response.Data = model;
                response.Code = ServerResponseType.成功;
            }
            catch (Exception ex)
            {
                response.Code = ServerResponseType.调用异常;
                response.ErrorMsg = ex.ToString();
                Log.LogHelper.WriteErrorLog(GetType(), ex);
            }
            return response;
        }

        // POST: api/Group/add
        [HttpPost("add")]
        public ServerResponse<int> Add(GroupModel request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                request.CreateTime = DateTime.Now;
                request.UpdateTime = DateTime.Now;
                request.State = 0;
                var num = dal.Add(request);
                response.Data = num;
                if (num == 1)
                {
                    response.Code = ServerResponseType.成功;
                }
                else
                {
                    response.Code = ServerResponseType.空数据;
                }
            }
            catch (Exception ex)
            {
                response.Code = ServerResponseType.调用异常;
                response.ErrorMsg = ex.ToString();
                Log.LogHelper.WriteErrorLog(GetType(), ex);
            }
            return response;
        }

        // POST: api/Group/update
        [HttpPost("update")]
        public ServerResponse<int> Update(GroupModel request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                request.UpdateTime = DateTime.Now;
                var num = dal.Update(request);
                response.Data = num;
                if (num == 1)
                {
                    response.Code = ServerResponseType.成功;
                }
                else
                {
                    response.Code = ServerResponseType.空数据;
                }
            }
            catch (Exception ex)
            {
                response.Code = ServerResponseType.调用异常;
                response.ErrorMsg = ex.ToString();
                Log.LogHelper.WriteErrorLog(GetType(), ex);
            }
            return response;
        }

        // POST: api/Group/delete
        [HttpPost("delete")]
        public ServerResponse<int> Delete(GroupModel request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                var num = dal.Delete(request);
                response.Data = num;
                if (num == 1)
                {
                    response.Code = ServerResponseType.成功;
                }
                else
                {
                    response.Code = ServerResponseType.空数据;
                }
            }
            catch (Exception ex)
            {
                response.Code = ServerResponseType.调用异常;
                response.ErrorMsg = ex.ToString();
                Log.LogHelper.WriteErrorLog(GetType(), ex);
            }
            return response;
        }
    }
}

