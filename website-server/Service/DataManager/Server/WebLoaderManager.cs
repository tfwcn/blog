using Model.Server;
using Model.Server.Args;
using Model.Server.Models;

namespace DataManager.Server
{
    public class WebLoaderManager : Base.BaseManager
    {
        public WebLoaderManager(string baseUrl) : base(baseUrl)
        {
        }
        public ServerResponse<WebLoaderModel> GetModel(WebLoaderGetModelRequest request)
        {
            return Action<WebLoaderModel>(request, "api/WebLoader/model");
        }

        public ServerResponse<WebLoaderGetListResponse> GetList(WebLoaderGetListRequest request)
        {
            return Action<WebLoaderGetListResponse>(request, "api/WebLoader/list");
        }

        public ServerResponse<int> GetCount(WebLoaderGetCountRequest request)
        {
            return Action<int>(request, "api/WebLoader/count");
        }

        public ServerResponse<WebLoaderAddResponse> Add(WebLoaderModel request)
        {
            var result = Action<WebLoaderAddResponse>(request, "api/WebLoader/add");
            if (result.Code == ServerResponseType.成功)
                request.Id = result.Data.Id;
            return result;
        }

        public ServerResponse<WebLoaderUpdateResponse> Update(WebLoaderModel request)
        {
            return Action<WebLoaderUpdateResponse>(request, "api/WebLoader/update");
        }

        public ServerResponse<WebLoaderDeleteResponse> Delete(WebLoaderModel request)
        {
            return Action<WebLoaderDeleteResponse>(request, "api/WebLoader/delete");
        }
    }
}
