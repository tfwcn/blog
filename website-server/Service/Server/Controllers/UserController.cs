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
    public class UserController : ControllerBase
    {
        private AppSettings Config;
        private static UserDAL dal;
        public UserController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new UserDAL(Config.ConnectionString);
        }
        // Post: api/User/model
        [HttpPost("model")]
        public ServerResponse<UserModel> GetModel(UserGetModelRequest request)
        {
            ServerResponse<UserModel> response = new ServerResponse<UserModel>();
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

        // Post: api/User/list
        [HttpPost("list")]
        public ServerResponse<List<UserModel>> GetList(UserGetListRequest request)
        {
            ServerResponse<List<UserModel>> response = new ServerResponse<List<UserModel>>();
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

        // Post: api/User/count
        [HttpPost("count")]
        public ServerResponse<int> GetCount(UserGetCountRequest request)
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

        // POST: api/User/add
        [HttpPost("add")]
        public ServerResponse<int> Add(UserModel request)
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

        // POST: api/User/update
        [HttpPost("update")]
        public ServerResponse<int> Update(UserModel request)
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

        // POST: api/User/delete
        [HttpPost("delete")]
        public ServerResponse<int> Delete(UserModel request)
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

