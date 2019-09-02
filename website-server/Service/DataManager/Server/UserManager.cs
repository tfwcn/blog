using Common;
using Model.Server.Models;
using Model.Server.Args;
using Model.Server;
using System;
using System.Collections.Generic;

namespace DataManager.Server
{
    public class UserManager : Base.BaseManager
    {
        public UserManager(string baseUrl) : base(baseUrl)
        {
        }
        public ServerResponse<UserModel> GetModel(UserGetModelRequest request)
        {
            return Action<UserModel>(request, "api/User/model");
        }

        public ServerResponse<List<UserModel>> GetList(UserGetListRequest request, out int total)
        {
            total = 0;
            var result = GetCount(JsonHelper.CloneObject<UserGetCountRequest>(request));
            if (result.Code == ServerResponseType.成功)
            {
                total = result.Data;
            }
            return Action<List<UserModel>>(request, "api/User/list");
        }

        public ServerResponse<int> GetCount(UserGetCountRequest request)
        {
            return Action<int>(request, "api/User/count");
        }

        public ServerResponse<int> Add(UserModel request)
        {
            var tmpModel = JsonHelper.CloneObject<UserModel>(request);
            tmpModel.Id = Guid.NewGuid().ToString();
            var result = Action<int>(tmpModel, "api/User/add");
            if (result.Code == ServerResponseType.成功)
                request.Id = tmpModel.Id;
            return result;
        }

        public ServerResponse<int> Update(UserModel request)
        {
            return Action<int>(request, "api/User/update");
        }

        public ServerResponse<int> Delete(UserModel request)
        {
            return Action<int>(request, "api/User/delete");
        }
    }
}
