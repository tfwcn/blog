using Common;
using Model.Server;
using Model.Server.Args;
using Model.Server.Models;
using System;
using System.Collections.Generic;

namespace DataManager.Server
{
    public class GateManager : Base.BaseManager
    {
        public GateManager(string baseUrl) : base(baseUrl)
        {
        }
        public ServerResponse<GateModel> GetModel(GateGetModelRequest request)
        {
            return Action<GateModel>(request, "api/Gate/model");
        }

        public ServerResponse<List<GateModel>> GetList(GateGetListRequest request, out int total)
        {
            total = 0;
            var result = GetCount(JsonHelper.CloneObject<GateGetCountRequest>(request));
            if (result.Code == ServerResponseType.成功)
            {
                total = result.Data;
            }
            return Action<List<GateModel>>(request, "api/Gate/list");
        }

        public ServerResponse<int> GetCount(GateGetCountRequest request)
        {
            return Action<int>(request, "api/Gate/count");
        }

        public ServerResponse<int> Add(GateModel request)
        {
            var tmpModel = JsonHelper.CloneObject<GateModel>(request);
            tmpModel.Id = Guid.NewGuid().ToString();
            var result = Action<int>(tmpModel, "api/Gate/add");
            if (result.Code == ServerResponseType.成功)
                request.Id = tmpModel.Id;
            return result;
        }

        public ServerResponse<int> Update(GateModel request)
        {
            return Action<int>(request, "api/Gate/update");
        }

        public ServerResponse<int> Delete(GateModel request)
        {
            return Action<int>(request, "api/Gate/delete");
        }
    }
}
