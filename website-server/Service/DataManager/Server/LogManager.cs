using Common;
using Model.Server;
using Model.Server.Args;
using Model.Server.Models;
using System;
using System.Collections.Generic;

namespace DataManager.Server
{
    public class LogManager : Base.BaseManager
    {
        public LogManager(string baseUrl) : base(baseUrl)
        {
        }
        public ServerResponse<LogModel> GetModel(LogGetModelRequest request)
        {
            return Action<LogModel>(request, "api/Log/model");
        }

        public ServerResponse<List<LogModel>> GetList(LogGetListRequest request, out int total)
        {
            total = 0;
            var result = GetCount(JsonHelper.CloneObject<LogGetCountRequest>(request));
            if (result.Code == ServerResponseType.成功)
            {
                total = result.Data;
            }
            return Action<List<LogModel>>(request, "api/Log/list");
        }

        public ServerResponse<int> GetCount(LogGetCountRequest request)
        {
            return Action<int>(request, "api/Log/count");
        }

        public ServerResponse<int> Add(LogModel request)
        {
            var tmpModel = JsonHelper.CloneObject<LogModel>(request);
            tmpModel.Id = Guid.NewGuid().ToString();
            var result = Action<int>(tmpModel, "api/Log/add");
            if (result.Code == ServerResponseType.成功)
                request.Id = tmpModel.Id;
            return result;
        }

        public ServerResponse<int> Update(LogModel request)
        {
            return Action<int>(request, "api/Log/update");
        }

        public ServerResponse<int> Delete(LogModel request)
        {
            return Action<int>(request, "api/Log/delete");
        }
        public ServerResponse<int> DeleteAll()
        {
            return Action<int>(null, "api/Log/delete_all");
        }
        public ServerResponse<int> DeleteIn(LogDeleteInRequest request)
        {
            return Action<int>(request, "api/Log/delete_in");
        }
    }
}
