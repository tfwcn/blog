using Common;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.Server;
using Model.Server.Args;
using Model.Server.Models;
using System;
using System.IO;
using Encrypt;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AppSettings Config;
        private static UserDAL dal;
        private static Encrypt.RSAHelper res;
        private static string privateKey = @"";
        private static string publicKey = @"";
        public UserController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new UserDAL(Config.ConnectionString);
            if (res == null)
            {
                using (StreamReader streamReader = new StreamReader("public.pem"))
                {
                    publicKey = "";
                    string tmpKey = streamReader.ReadToEnd();
                    tmpKey = tmpKey.Replace("\r", "");
                    foreach (var item in tmpKey.Split("\n"))
                    {
                        if (item != "-----BEGIN RSA PUBLIC KEY-----" && item != "-----END RSA PUBLIC KEY-----")
                            publicKey += item;
                    }
                }
                using (StreamReader streamReader = new StreamReader("private.pem"))
                {
                    privateKey = "";
                    string tmpKey = streamReader.ReadToEnd();
                    tmpKey = tmpKey.Replace("\r", "");
                    foreach (var item in tmpKey.Split("\n"))
                    {
                        if (item != "-----BEGIN RSA PRIVATE KEY-----" && item != "-----END RSA PRIVATE KEY-----")
                            privateKey += item;
                    }
                }
                res = new Encrypt.RSAHelper(privateKey, publicKey);
            }
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
        public ServerResponse<UserGetListResponse> GetList(UserGetListRequest request)
        {
            ServerResponse<UserGetListResponse> response = new ServerResponse<UserGetListResponse>();
            try
            {
                //查数据
                var list = dal.GetList(request);
                //查总记录数
                var count = dal.GetCount(JsonHelper.CloneObject<UserGetCountRequest>(request));
                response.Data = new UserGetListResponse() { DataList = list, Count = count };
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
        public ServerResponse<UserAddResponse> Add(UserModel request)
        {
            ServerResponse<UserAddResponse> response = new ServerResponse<UserAddResponse>();
            try
            {
                var num = dal.Add(request);
                response.Data = new UserAddResponse { Id = request.Id, Num = num };
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
        public ServerResponse<UserUpdateResponse> Update(UserModel request)
        {
            ServerResponse<UserUpdateResponse> response = new ServerResponse<UserUpdateResponse>();
            try
            {
                var num = dal.Update(request);
                response.Data = new UserUpdateResponse { Id = request.Id, Num = num };
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
        public ServerResponse<UserDeleteResponse> Delete(UserModel request)
        {
            ServerResponse<UserDeleteResponse> response = new ServerResponse<UserDeleteResponse>();
            try
            {
                var num = dal.Delete(request);
                response.Data = new UserDeleteResponse { Id = request.Id, Num = num };
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

        // Post: api/User/login
        [HttpPost("login")]
        public ServerResponse<UserModel> Login(UserGetModelRequest request)
        {
            ServerResponse<UserModel> response = new ServerResponse<UserModel>();
            try
            {
                var model = dal.GetModel(request);
                if (model != null)
                {
                    string tmpId = res.Encrypt(model.Id);
                    //保存token
                    if (CommonData.TokenList.ContainsKey(tmpId))
                    {
                        CommonData.TokenList[tmpId] = model;
                    }
                    else
                    {
                        CommonData.TokenList.Add(tmpId, model);
                    }
                    response.Data = new UserModel() { Id = tmpId };
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

        // Post: api/User/check
        [HttpPost("check")]
        public ActionResult Check(UserCheckRequest request)
        {
            ActionResult response;
            try
            {
                if (request.Token != null)
                {
                    //string tmpId = res.Decrypt(request.Token);
                    string tmpId = request.Token;
                    //保存token
                    if (CommonData.TokenList.ContainsKey(tmpId))
                    {
                        ServerResponse<object> responseObj = new ServerResponse<object>();
                        responseObj.Code = ServerResponseType.成功;
                        response = new JsonResult(responseObj);
                    }
                    else
                    {
                        response = new StatusCodeResult(401);
                    }
                }
                else
                {
                    response = new StatusCodeResult(401);
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.WriteErrorLog(GetType(), ex);
                response = new StatusCodeResult(500);
            }
            return response;
        }
    }
}

