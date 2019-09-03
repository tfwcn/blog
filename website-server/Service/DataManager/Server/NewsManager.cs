using Common;
using Model.Server.Models;
using Model.Server.Args;
using Model.Server;
using System;
using System.Collections.Generic;

namespace DataManager.Server
{
    public class NewsManager : Base.BaseManager
    {
        public NewsManager(string baseUrl) : base(baseUrl)
        {
        }
        public ServerResponse<NewsModel> GetModel(NewsGetModelRequest request)
        {
            return Action<NewsModel>(request, "api/News/model");
        }

        public ServerResponse<List<NewsModel>> GetList(NewsGetListRequest request, out int total)
        {
            total = 0;
            var result = GetCount(JsonHelper.CloneObject<NewsGetCountRequest>(request));
            if (result.Code == ServerResponseType.成功)
            {
                total = result.Data;
            }
            return Action<List<NewsModel>>(request, "api/News/list");
        }

        public ServerResponse<int> GetCount(NewsGetCountRequest request)
        {
            return Action<int>(request, "api/News/count");
        }

        public ServerResponse<NewsAddResponse> Add(NewsModel request)
        {
            var result = Action<NewsAddResponse>(request, "api/News/add");
            if (result.Code == ServerResponseType.成功)
                request.Id = result.Data.Id;
            return result;
        }

        public ServerResponse<NewsUpdateResponse> Update(NewsModel request)
        {
            return Action<NewsUpdateResponse>(request, "api/News/update");
        }

        public ServerResponse<NewsDeleteResponse> Delete(NewsModel request)
        {
            return Action<NewsDeleteResponse>(request, "api/News/delete");
        }
    }
}
