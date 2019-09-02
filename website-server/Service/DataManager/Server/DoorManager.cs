using Common;
using Model.Server.Models;
using Model.Server.Args;
using Model.Server;
using System;
using System.Collections.Generic;

namespace DataManager.Server
{
    public class DoorManager : Base.BaseManager
    {
        public DoorManager(string baseUrl) : base(baseUrl)
        {
        }
        public ServerResponse<DoorModel> GetModel(DoorGetModelRequest request)
        {
            return Action<DoorModel>(request, "api/Door/model");
        }

        public ServerResponse<List<DoorModel>> GetList(DoorGetListRequest request, out int total)
        {
            total = 0;
            var result = GetCount(JsonHelper.CloneObject<DoorGetCountRequest>(request));
            if (result.Code == ServerResponseType.成功)
            {
                total = result.Data;
            }
            return Action<List<DoorModel>>(request, "api/Door/list");
        }

        public ServerResponse<int> GetCount(DoorGetCountRequest request)
        {
            return Action<int>(request, "api/Door/count");
        }

        public ServerResponse<int> Add(DoorModel request)
        {
            var tmpModel = JsonHelper.CloneObject<DoorModel>(request);
            tmpModel.Id = Guid.NewGuid().ToString();
            var result = Action<int>(tmpModel, "api/Door/add");
            if (result.Code == ServerResponseType.成功)
                request.Id = tmpModel.Id;
            return result;
        }

        public ServerResponse<int> Update(DoorModel request)
        {
            return Action<int>(request, "api/Door/update");
        }

        public ServerResponse<int> Delete(DoorModel request)
        {
            return Action<int>(request, "api/Door/delete");
        }
    }
}
