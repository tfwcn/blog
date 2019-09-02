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
    public class CompanyController : ControllerBase
    {
        private AppSettings Config;
        private static CompanyDAL dal;
        public CompanyController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new CompanyDAL(Config.ConnectionString);
        }
        // Post: api/Company/model
        [HttpPost("model")]
        public ServerResponse<CompanyModel> GetModel(CompanyGetModelRequest request)
        {
            ServerResponse<CompanyModel> response = new ServerResponse<CompanyModel>();
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

        // Post: api/Company/list
        [HttpPost("list")]
        public ServerResponse<List<CompanyModel>> GetList(CompanyGetListRequest request)
        {
            ServerResponse<List<CompanyModel>> response = new ServerResponse<List<CompanyModel>>();
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

        // Post: api/Company/count
        [HttpPost("count")]
        public ServerResponse<int> GetCount(CompanyGetCountRequest request)
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

        // POST: api/Company/add
        [HttpPost("add")]
        public ServerResponse<int> Add(CompanyModel request)
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

        // POST: api/Company/update
        [HttpPost("update")]
        public ServerResponse<int> Update(CompanyModel request)
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

        // POST: api/Company/delete
        [HttpPost("delete")]
        public ServerResponse<int> Delete(CompanyModel request)
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

