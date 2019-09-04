using Common;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.Server;
using Model.Server.Args;
using Model.Server.Models;
using System;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private AppSettings Config;
        private static NewsDAL dal;
        public NewsController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new NewsDAL(Config.ConnectionString);
        }
        // Post: api/News/model
        [HttpPost("model")]
        public ServerResponse<NewsModel> GetModel(NewsGetModelRequest request)
        {
            ServerResponse<NewsModel> response = new ServerResponse<NewsModel>();
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

        // Post: api/News/list
        [HttpPost("list")]
        public ServerResponse<NewsGetListResponse> GetList(NewsGetListRequest request)
        {
            ServerResponse<NewsGetListResponse> response = new ServerResponse<NewsGetListResponse>();
            try
            {
                //查数据
                var list = dal.GetList(request);
                //查总记录数
                var count = dal.GetCount(JsonHelper.CloneObject<NewsGetCountRequest>(request));
                response.Data = new NewsGetListResponse() { DataList = list, Count = count };
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

        // Post: api/News/count
        [HttpPost("count")]
        public ServerResponse<int> GetCount(NewsGetCountRequest request)
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

        // POST: api/News/add
        [HttpPost("add")]
        public ServerResponse<NewsAddResponse> Add(NewsModel request)
        {
            ServerResponse<NewsAddResponse> response = new ServerResponse<NewsAddResponse>();
            try
            {
                var num = dal.Add(request);
                response.Data = new NewsAddResponse { Id = request.Id, Num = num };
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

        // POST: api/News/update
        [HttpPost("update")]
        public ServerResponse<NewsUpdateResponse> Update(NewsModel request)
        {
            ServerResponse<NewsUpdateResponse> response = new ServerResponse<NewsUpdateResponse>();
            try
            {
                var num = dal.Update(request);
                response.Data = new NewsUpdateResponse { Id = request.Id, Num = num };
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

        // POST: api/News/delete
        [HttpPost("delete")]
        public ServerResponse<NewsDeleteResponse> Delete(NewsModel request)
        {
            ServerResponse<NewsDeleteResponse> response = new ServerResponse<NewsDeleteResponse>();
            try
            {
                var num = dal.Delete(request);
                response.Data = new NewsDeleteResponse { Id = request.Id, Num = num };
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

