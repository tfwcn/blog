using DAL;
using Model.Server;
using Model.Server.Args;
using Model.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private AppSettings Config;
        private static PersonDAL dal;
        public PersonController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new PersonDAL(Config.ConnectionString);
        }
        // Post: api/Person/model
        [HttpPost("model")]
        public ServerResponse<PersonModel> GetModel(PersonGetModelRequest request)
        {
            ServerResponse<PersonModel> response = new ServerResponse<PersonModel>();
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

        // Post: api/Person/list
        [HttpPost("list")]
        public ServerResponse<List<PersonModel>> GetList(PersonGetListRequest request)
        {
            ServerResponse<List<PersonModel>> response = new ServerResponse<List<PersonModel>>();
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

        // Post: api/Person/count
        [HttpPost("count")]
        public ServerResponse<int> GetCount(PersonGetCountRequest request)
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

        // POST: api/Person/add
        [HttpPost("add")]
        public ServerResponse<int> Add(PersonModel request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                request.FacePath = ImageSaveHelper.CheckAndSavePath(Config.ImageDir, "person", request.Id, request.FacePath);
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

        // POST: api/Person/update
        [HttpPost("update")]
        public ServerResponse<int> Update(PersonModel request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                request.FacePath = ImageSaveHelper.CheckAndSavePath(Config.ImageDir, "person", request.Id, request.FacePath);
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

        // POST: api/Person/delete
        [HttpPost("delete")]
        public ServerResponse<int> Delete(PersonModel request)
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
        /// <summary>
        /// 在场人员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // Post: api/Person/in_list
        [HttpPost("in_list")]
        public ServerResponse<List<PersonModel>> GetInList(PersonGetInListRequest request)
        {
            ServerResponse<List<PersonModel>> response = new ServerResponse<List<PersonModel>>();
            try
            {
                var model = dal.GetInList(request);
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

        /// <summary>
        /// 批量更新黑名单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // POST: api/Person/update_permission
        [HttpPost("update_permission")]
        public ServerResponse<int> UpdatePermission(PersonUpdatePermissionRequest request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                var num = dal.UpdatePermission(request);
                response.Data = num;
                if (num > 0)
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

