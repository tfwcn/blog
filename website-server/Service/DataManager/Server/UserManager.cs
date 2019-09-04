using Model.Server;
using Model.Server.Args;
using Model.Server.Models;

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

        public ServerResponse<UserGetListResponse> GetList(UserGetListRequest request)
        {
            return Action<UserGetListResponse>(request, "api/User/list");
        }

        public ServerResponse<int> GetCount(UserGetCountRequest request)
        {
            return Action<int>(request, "api/User/count");
        }

        public ServerResponse<UserAddResponse> Add(UserModel request)
        {
            var result = Action<UserAddResponse>(request, "api/User/add");
            if (result.Code == ServerResponseType.成功)
                request.Id = result.Data.Id;
            return result;
        }

        public ServerResponse<UserUpdateResponse> Update(UserModel request)
        {
            return Action<UserUpdateResponse>(request, "api/User/update");
        }

        public ServerResponse<UserDeleteResponse> Delete(UserModel request)
        {
            return Action<UserDeleteResponse>(request, "api/User/delete");
        }
    }
}
