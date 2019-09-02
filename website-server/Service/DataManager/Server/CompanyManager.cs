using Common;
using Model.Server;
using Model.Server.Args;
using Model.Server.Models;
using System;
using System.Collections.Generic;

namespace DataManager.Server
{
    public class CompanyManager : Base.BaseManager
    {
        public CompanyManager(string baseUrl) : base(baseUrl)
        {
        }
        public ServerResponse<CompanyModel> GetModel(CompanyGetModelRequest request)
        {
            return Action<CompanyModel>(request, "api/Company/model");
        }

        public ServerResponse<List<CompanyModel>> GetList(CompanyGetListRequest request, out int total)
        {
            total = 0;
            var result = GetCount(JsonHelper.CloneObject<CompanyGetCountRequest>(request));
            if (result.Code == ServerResponseType.成功)
            {
                total = result.Data;
            }
            return Action<List<CompanyModel>>(request, "api/Company/list");
        }

        public ServerResponse<int> GetCount(CompanyGetCountRequest request)
        {
            return Action<int>(request, "api/Company/count");
        }

        public ServerResponse<int> Add(CompanyModel request)
        {
            var tmpModel = JsonHelper.CloneObject<CompanyModel>(request);
            tmpModel.Id = Guid.NewGuid().ToString();
            var result = Action<int>(tmpModel, "api/Company/add");
            if (result.Code == ServerResponseType.成功)
                request.Id = tmpModel.Id;
            return result;
        }

        public ServerResponse<int> Update(CompanyModel request)
        {
            return Action<int>(request, "api/Company/update");
        }

        public ServerResponse<int> Delete(CompanyModel request)
        {
            return Action<int>(request, "api/Company/delete");
        }
    }
}
