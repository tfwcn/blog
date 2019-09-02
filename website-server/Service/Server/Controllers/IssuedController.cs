using Common;
using DAL;
using DataManager;
using Model.Door;
using Model.Door.Args;
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
    public class IssuedController : ControllerBase
    {
        private static object lockObj = new object();
        private AppSettings Config;
        private static DoorDataManager dataManager;
        private static DoorDAL dalDoor;
        private static PersonDAL dalPerson;
        private static LogDAL dalLog;
        public IssuedController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dataManager == null)
                dataManager = new DoorDataManager();
            if (dalDoor == null)
                dalDoor = new DoorDAL(Config.ConnectionString);
            if (dalPerson == null)
                dalPerson = new PersonDAL(Config.ConnectionString);
            if (dalLog == null)
                dalLog = new LogDAL(Config.ConnectionString);
        }
        // Post: api/Issued/get_config
        [HttpPost("get_config")]
        public ServerResponse<DoorModel> GetConfig(IssuedGetConfigRequest request)
        {
            ServerResponse<DoorModel> response = new ServerResponse<DoorModel>();
            try
            {
                var doorResponse = dataManager.Door.GetConfig(request.BaseUrl, new ConfigGetRequest());
                if (doorResponse != null && doorResponse.code == DoorResponseType.成功)
                {
                    response.Code = ServerResponseType.成功;
                    var result = dalDoor.GetModel(new DoorGetModelRequest() { Ip = request.BaseUrl });
                    if (result != null)
                    {
                        response.Data = result;
                        response.Data.ServerUrl = doorResponse.result.url;
                        response.Data.Id = doorResponse.result.doorId;
                        response.Data.Threshold = doorResponse.result.threshold;
                        response.Data.Type = doorResponse.result.type;
                    }
                    else
                    {
                        response.Data = new DoorModel()
                        {
                            ServerUrl = doorResponse.result.url,
                            Id = doorResponse.result.doorId,
                            Threshold = doorResponse.result.threshold,
                            Type = doorResponse.result.type,
                        };
                    }
                }
                else
                {
                    response.Code = ServerResponseType.调用异常;
                    response.ErrorMsg = doorResponse?.message;
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
        // Post: api/Issued/update_config
        [HttpPost("update_config")]
        public ServerResponse<object> UpdateConfig(IssuedUpdateConfigRequest request)
        {
            ServerResponse<object> response = new ServerResponse<object>();
            try
            {
                //获取原有记录
                var doorGetResponse = dataManager.Door.GetConfig(request.BaseUrl, new ConfigGetRequest());
                doorGetResponse.result.doorId = request.Data.Id;
                doorGetResponse.result.threshold = request.Data.Threshold;
                doorGetResponse.result.type = request.Data.Type;
                doorGetResponse.result.url = request.Data.ServerUrl;
                //更新记录
                var doorResponse = dataManager.Door.UpdateConfig(request.BaseUrl, doorGetResponse.result);
                if (doorResponse != null && doorResponse.code == DoorResponseType.成功)
                {
                    response.Code = ServerResponseType.成功;
                    response.Data = doorResponse.result;
                }
                else
                {
                    response.Code = ServerResponseType.调用异常;
                    response.ErrorMsg = doorResponse?.message;
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
        // Post: api/Issued/get_person
        [HttpPost("get_person")]
        public ServerResponse<List<PersonModel>> GetPerson(IssuedGetPersonRequest request)
        {
            ServerResponse<List<PersonModel>> response = new ServerResponse<List<PersonModel>>();
            try
            {
                var doorResponse = dataManager.Door.GetPerson(request.BaseUrl, new PersonGetRequest() { page = request.page, rows = request.rows });
                //更新记录
                if (doorResponse != null && doorResponse.code == DoorResponseType.成功)
                {
                    List<PersonModel> tmpPersonList = new List<PersonModel>();
                    foreach (var id in doorResponse.result)
                    {
                        //获取人员信息
                        var tmpPerson = dalPerson.GetModel(new PersonGetModelRequest() { Id = id, State = null });
                        if (tmpPerson != null)
                        {
                            tmpPersonList.Add(tmpPerson);
                        }
                    }
                    response.Code = ServerResponseType.成功;
                    response.Data = tmpPersonList;
                }
                else
                {
                    response.Code = ServerResponseType.调用异常;
                    response.ErrorMsg = doorResponse?.message;
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

        // Post: api/Issued/get_person_count
        [HttpPost("get_person_count")]
        public ServerResponse<int> GetCount(IssuedGetPersonCount request)
        {
            ServerResponse<int> response = new ServerResponse<int>();
            try
            {
                var doorResponse = dataManager.Door.GetPersonCount(request.BaseUrl, new PersonGetRequest() { page = request.page, rows = request.rows });
                //更新记录
                if (doorResponse != null && doorResponse.code == DoorResponseType.成功)
                {
                    response.Data = doorResponse.result;
                    response.Code = ServerResponseType.成功;
                }
                else
                {
                    response.Code = ServerResponseType.调用异常;
                    response.ErrorMsg = doorResponse?.message;
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

        // Post: api/Issued/update_person
        [HttpPost("update_person")]
        public ServerResponse<object> UpdatePerson(IssuedUpdatePersonRequest request)
        {
            ServerResponse<object> response = new ServerResponse<object>();
            try
            {
                //获取人员信息
                var tmpPerson = dalPerson.GetModel(new PersonGetModelRequest() { Id = request.Id });
                if (tmpPerson == null)
                {
                    throw new Exception("人员信息不存在");
                }
                //填参数
                string updateImage = null;
                lock (lockObj)
                {
                    updateImage = ImageHelper.BitmapToBase64(ImageHelper.GetImage(String.Format(tmpPerson.FacePath, Config.ImageDir)));
                }
                Model.Door.Models.PersonModel model = new Model.Door.Models.PersonModel()
                {
                    card = tmpPerson.Card,
                    companyId = tmpPerson.CompanyId,
                    createTime = tmpPerson.CreateTime,
                    facePath = updateImage,
                    groupId = tmpPerson.GroupId,
                    name = tmpPerson.Name,
                    permission = tmpPerson.Permission,
                    state = tmpPerson.State,
                    type = tmpPerson.Type,
                    updateTime = tmpPerson.UpdateTime,
                    personId = tmpPerson.Id,
                };
                if (model.facePath == null)
                {
                    throw new Exception("模板图片不存在");
                }
                //更新记录
                var doorResponse = dataManager.Door.UpdatePerson(request.BaseUrl, model);
                if (doorResponse != null && doorResponse.code == DoorResponseType.成功)
                {
                    response.Code = ServerResponseType.成功;
                    response.Data = doorResponse.result;
                }
                else
                {
                    response.Code = ServerResponseType.调用异常;
                    response.ErrorMsg = doorResponse?.message;
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
        // Post: api/Issued/delete_person
        [HttpPost("delete_person")]
        public ServerResponse<object> DeletePerson(IssuedDeletePersonRequest request)
        {
            ServerResponse<object> response = new ServerResponse<object>();
            try
            {
                var doorResponse = dataManager.Door.DeletePerson(request.BaseUrl, new Model.Door.Args.PersonDeleteRequest() { personId = request.Id });
                if (doorResponse != null && doorResponse.code == DoorResponseType.成功)
                {
                    response.Code = ServerResponseType.成功;
                    response.Data = doorResponse.result;
                }
                else
                {
                    response.Code = ServerResponseType.调用异常;
                    response.ErrorMsg = doorResponse?.message;
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
        // Post: api/Issued/clean_person
        [HttpPost("clean_person")]
        public ServerResponse<object> CleanPerson(IssuedCleanPersonRequest request)
        {
            ServerResponse<object> response = new ServerResponse<object>();
            try
            {
                var doorResponse = dataManager.Door.CleanPerson(request.BaseUrl, new PersonCleanRequest());
                if (doorResponse != null && doorResponse.code == DoorResponseType.成功)
                {
                    response.Code = ServerResponseType.成功;
                    response.Data = doorResponse.result;
                }
                else
                {
                    response.Code = ServerResponseType.调用异常;
                    response.ErrorMsg = doorResponse?.message;
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
        // Post: api/Issued/open_door
        [HttpPost("open_door")]
        public ServerResponse<object> OpenDoor(IssuedOpenDoorRequest request)
        {
            ServerResponse<object> response = new ServerResponse<object>();
            try
            {
                var tmpDoor = dalDoor.GetModel(new DoorGetModelRequest() { Ip = request.BaseUrl });
                if (tmpDoor == null)
                    throw new Exception(request.BaseUrl + "门记录不存在");
                var doorResponse = dataManager.Door.OpenDoor(request.BaseUrl, new OpenDoorRequest());
                if (doorResponse != null && doorResponse.code == DoorResponseType.成功)
                {
                    var tmpLog = new LogModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                        DoorId = tmpDoor.Id,
                        PersonType = request.PersonType,
                        Type = request.Type
                    };
                    var resultLog = dalLog.Add(tmpLog);
                    if (resultLog > 0)
                    {
                        response.Code = ServerResponseType.成功;
                        response.Data = doorResponse.result;
                    }
                    else
                    {
                        response.Code = ServerResponseType.空数据;
                    }
                }
                else
                {
                    response.Code = ServerResponseType.调用异常;
                    response.ErrorMsg = doorResponse?.message;
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

