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
    public class LogController : ControllerBase
    {
        private AppSettings Config;
        private static LogDAL dal;
        private static DoorDAL dalDoor;
        public LogController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new LogDAL(Config.ConnectionString);
            if (dalDoor == null)
                dalDoor = new DoorDAL(Config.ConnectionString);
        }
        // Post: api/Log/model
        [HttpPost("model")]
        public ServerResponse<LogModel> GetModel(LogGetModelRequest request)
        {
            ServerResponse<LogModel> response = new ServerResponse<LogModel>();
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

        // Post: api/Log/list
        [HttpPost("list")]
        public ServerResponse<List<LogModel>> GetList(LogGetListRequest request)
        {
            ServerResponse<List<LogModel>> response = new ServerResponse<List<LogModel>>();
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

        // Post: api/Log/count
        [HttpPost("count")]
        public ServerResponse<int> GetCount(LogGetCountRequest request)
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

        // POST: api/Log/add
        [HttpPost("add")]
        public ServerResponse<int> Add(LogModel request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                DoorModel tmpDoorModel = dalDoor.GetModel(new DoorGetModelRequest() { Id = request.DoorId });
                LogModel tmpModel = null;
                if (request.PersonId != null)
                    tmpModel = dal.GetModel(new LogGetModelRequest() { PersonId = request.PersonId });
                //进去的门，同时这个人已经进场，则异常
                if (tmpModel == null && tmpDoorModel != null && tmpDoorModel.Type == 1 && request.Type == 0)//第一次出来限制
                {
                    response.Code = ServerResponseType.人员进出记录重复;
                }
                else if (tmpDoorModel != null && tmpModel != null && tmpModel.DoorType == tmpDoorModel.Type && request.Type == 0)
                {
                    response.Code = ServerResponseType.人员进出记录重复;
                }
                else
                {
                    //插入数据库
                    request.FacePath = ImageSaveHelper.CheckAndSavePath(Config.ImageDir, "log", request.Id, request.FacePath);
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
            }
            catch (Exception ex)
            {
                response.Code = ServerResponseType.调用异常;
                response.ErrorMsg = ex.ToString();
                Log.LogHelper.WriteErrorLog(GetType(), ex);
            }
            return response;
        }

        // POST: api/Log/update
        [HttpPost("update")]
        public ServerResponse<int> Update(LogModel request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                request.FacePath = ImageSaveHelper.CheckAndSavePath(Config.ImageDir, "log", request.Id, request.FacePath);
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

        // POST: api/Log/delete
        [HttpPost("delete")]
        public ServerResponse<int> Delete(LogModel request)
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

        // POST: api/Log/delete_all
        [HttpPost("delete_all")]
        public ServerResponse<int> DeleteAll()
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                var num = dal.DeleteAll();
                response.Data = num;
                if (num >= 1)
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
        /// 根据进场记录生成出场记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // POST: api/Log/delete_in
        [HttpPost("delete_in")]
        public ServerResponse<int> DeleteIn(LogDeleteInRequest request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                var tmpLogModel = dal.GetModel(new LogGetModelRequest() { Id = request.Id });
                tmpLogModel.Id = Guid.NewGuid().ToString();
                var tmpDoorModel = dalDoor.GetModel(new DoorGetModelRequest() { Type = 1 });
                if (tmpDoorModel != null)
                {
                    tmpLogModel.DoorId = tmpDoorModel.Id;
                    response = Add(tmpLogModel);
                }
                else
                {
                    response.Code = ServerResponseType.空数据;
                    response.ErrorMsg = "无出闸门禁";
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

