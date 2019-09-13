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
    public class UserController : ControllerBase
    {
        private AppSettings Config;
        private static UserDAL dal;
        private static RSAHelper res;
        private static string privateKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0CphbdXDbX31ZIQWUmcvcvAX+yZYxs0g0E70ANLTMoJPQaLTj7rrrvmlMEtwt4AFVVBABFC5vOsZgSo840svEO77R4J9Z3gIQekAKiwKIEznL5tva78K8Tl9rR61iEExu4HQEpY/Udh3zowKlOf9Xw9cAU53rOHYbjXmeK+MZgNWYKv0uWJSdWdK0Ci0puRB3KDgtG4pzBen/fPSgmkLSSnl66y25YQ/WUHGM2u2PBKDAv24Ir95q2kwYgk7P/cuH5OOxpNg2q5h5dtSJXTHEysL5au4jLIfXsrSwdhwqo2vtdlnxfa7pe8NvuOkJefxlDIZkT8wsk7ASH2xBC64OwIDAQAB";
        private static string publicKey = @"MIIEpAIBAAKCAQEA0CphbdXDbX31ZIQWUmcvcvAX+yZYxs0g0E70ANLTMoJPQaLTj7rrrvmlMEtwt4AFVVBABFC5vOsZgSo840svEO77R4J9Z3gIQekAKiwKIEznL5tva78K8Tl9rR61iEExu4HQEpY/Udh3zowKlOf9Xw9cAU53rOHYbjXmeK+MZgNWYKv0uWJSdWdK0Ci0puRB3KDgtG4pzBen/fPSgmkLSSnl66y25YQ/WUHGM2u2PBKDAv24Ir95q2kwYgk7P/cuH5OOxpNg2q5h5dtSJXTHEysL5au4jLIfXsrSwdhwqo2vtdlnxfa7pe8NvuOkJefxlDIZkT8wsk7ASH2xBC64OwIDAQABAoIBAHciOh5E/5JzSvSaz7ebTEGIfQEEYjxse0IcXXL3NV2rzDYxPMj+XhIG1+46zqiGQQchZXHSzWn8Vt1gUfZ3OdmEHBVB7glAXeFyuuBn0efIe2r4lxzf+iAyGeXxNE1lmlPIqE9q45QwwehYp1mvJ4e2353zzXQI0PfKKzuYpE5gwwhpHnc0TJ/KZLZEpUPX2tkyWSvQTWKQjbGMwTtyIP9se++GntpHBFVSpNbvgyfksgHZ1gOyJrq8AJwHKTN2Rd6IxU0wjF00PUPI3roKtk1JNSpGQvOb6kZ2FGlMmT+ecoe0nLGfs1kAWNr6Nxz2j7dk4bxbm2z1sFxO74NzXBkCgYEA/ypEgirl80QleuSMMVjr5FWLH+6D0FgHIyXKUr3Gi8maKG3tDZW+H6TZG/rKi1WgExsSX0+kS/srlorRIzoNvUhCs9Nx5xPlp2CKltoKZovdmS6+QsTs1rOVopzeB1oTPDpggHBDGnb80qjOI8wzgYr0xq2sCVMxmg7ocXzX6a0CgYEA0Ni+uYEcEtRNS7+6fkObwPIsjdyElNsNpMeZw79lsAXnA5hBNGnvwSu0BeHc3mDAky0Diz18UIapg87hSzPUc1As7zGxfpjPmvr/c4vyVidou6I+ZN/u9eNKItUUfooz5x+3sc02LBpGMOFNgMr3tf1mdaRpCSiSRVeH4qAoNocCgYEA8fpdeavA4g+wE3kF0g5ntePByghhDIVOT3CZDBpYXVxUSx7j/UwSPuQP2E7fIX+UDEpSA/z86+lHjr4aUvPM78HFL8/HZsIhubb99szTrCfbgFcpqxwhFgK8Vre4fvRW5Xje5y6PFFveqs/WnXAbMDBcrMUqLrWshlK48FbaUwkCgYAZEdubMwdmrzt0G1jMrVr2B1wXz1/O6pixrhAkMkaHob3AbbduDkVsf82FYz57J0wWnrGtNj1FAVU58EyVFWysRvSN5f4zfy50oSqm+Sam9uYYl/o7a7IorBcLJV7nbbmbRfBsFIErPCAu3+zIyBSCMR/qgUjmg4tDbaVvK+CH4wKBgQDVE/ry4Sl7XsI0lecqziTNEsoSwsKcOZsWP+vEk9FmPunWn287m60V6XfmEsqz5iJn1yEwVLPXAt7MV+RNh9t8KcNw4DdJxF9YOeZpVO/Q9tgWldFsO4GspRVXtW4eC63akEtUOiEZUon5VXQ+UpBDAUlmEUJDZxfwjWi6gi0j5A==";
        public UserController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new UserDAL(Config.ConnectionString);
            if (res == null)
                res = new RSAHelper(RSAHelper.RSAType.RSA2, System.Text.Encoding.UTF8, privateKey, publicKey);
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
        public StatusCodeResult Check(UserCheckRequest request)
        {
            StatusCodeResult response;
            try
            {
                if (request.Token != null)
                {
                    string tmpId = res.Decrypt(request.Token);
                    //保存token
                    if (CommonData.TokenList.ContainsKey(tmpId))
                    {
                        response = new StatusCodeResult(200);
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

