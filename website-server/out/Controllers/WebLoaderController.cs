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
    public class WebLoaderController : ControllerBase
    {
        private AppSettings Config;
        private static WebLoaderDAL dal;
        public WebLoaderController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new WebLoaderDAL(Config.ConnectionString);
        }
        // Post: api/WebLoader/model
        [HttpPost("model")]
        public ServerResponse<WebLoaderModel> GetModel(WebLoaderGetModelRequest request)
        {
            ServerResponse<WebLoaderModel> response = new ServerResponse<WebLoaderModel>();
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

        // Post: api/WebLoader/list
        [HttpPost("list")]
        public ServerResponse<WebLoaderGetListResponse> GetList(WebLoaderGetListRequest request)
        {
            ServerResponse<WebLoaderGetListResponse> response = new ServerResponse<WebLoaderGetListResponse>();
            try
            {
                //查数据
                var list = dal.GetList(request);
                //查总记录数
                var count = dal.GetCount(JsonHelper.CloneObject<WebLoaderGetCountRequest>(request));
                response.Data = new WebLoaderGetListResponse() { DataList = list, Count = count };
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

        // Post: api/WebLoader/count
        [HttpPost("count")]
        public ServerResponse<int> GetCount(WebLoaderGetCountRequest request)
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

        // POST: api/WebLoader/add
        [HttpPost("add")]
        public ServerResponse<WebLoaderAddResponse> Add(WebLoaderModel request)
        {
            ServerResponse<WebLoaderAddResponse> response = new ServerResponse<WebLoaderAddResponse>();
            try
            {
                var num = dal.Add(request);
                response.Data = new WebLoaderAddResponse { Id = request.Id, Num = num };
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

        // POST: api/WebLoader/update
        [HttpPost("update")]
        public ServerResponse<WebLoaderUpdateResponse> Update(WebLoaderModel request)
        {
            ServerResponse<WebLoaderUpdateResponse> response = new ServerResponse<WebLoaderUpdateResponse>();
            try
            {
                var num = dal.Update(request);
                response.Data = new WebLoaderUpdateResponse { Id = request.Id, Num = num };
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

        // POST: api/WebLoader/delete
        [HttpPost("delete")]
        public ServerResponse<WebLoaderDeleteResponse> Delete(WebLoaderModel request)
        {
            ServerResponse<WebLoaderDeleteResponse> response = new ServerResponse<WebLoaderDeleteResponse>();
            try
            {
                var num = dal.Delete(request);
                response.Data = new WebLoaderDeleteResponse { Id = request.Id, Num = num };
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

