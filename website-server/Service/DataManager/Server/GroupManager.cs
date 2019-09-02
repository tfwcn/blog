using Common;
using Model.Server.Models;
using Model.Server.Args;
using Model.Server;
using System;
using System.Collections.Generic;

namespace DataManager.Server
{
    public class GroupManager : Base.BaseManager
    {
        public GroupManager(string baseUrl) : base(baseUrl)
        {
        }
        public ServerResponse<GroupModel> GetModel(GroupGetModelRequest request)
        {
            return Action<GroupModel>(request, "api/Group/model");
        }

        public ServerResponse<List<GroupModel>> GetList(GroupGetListRequest request, out int total)
        {
            total = 0;
            var result = GetCount(JsonHelper.CloneObject<GroupGetCountRequest>(request));
            if (result.Code == ServerResponseType.成功)
            {
                total = result.Data;
            }
            return Action<List<GroupModel>>(request, "api/Group/list");
        }

        public ServerResponse<int> GetCount(GroupGetCountRequest request)
        {
            return Action<int>(request, "api/Group/count");
        }

        public ServerResponse<int> Add(GroupModel request)
        {
            var tmpModel = JsonHelper.CloneObject<GroupModel>(request);
            tmpModel.Id = Guid.NewGuid().ToString();
            var result = Action<int>(tmpModel, "api/Group/add");
            if (result.Code == ServerResponseType.成功)
                request.Id = tmpModel.Id;
            return result;
        }

        public ServerResponse<int> Update(GroupModel request)
        {
            return Action<int>(request, "api/Group/update");
        }

        public ServerResponse<int> Delete(GroupModel request)
        {
            return Action<int>(request, "api/Group/delete");
        }
    }
}
