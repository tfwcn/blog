using Common;
using Model.Server;
using Model.Server.Args;
using Model.Server.Models;
using System;
using System.Collections.Generic;

namespace DataManager.Server
{
    public class PersonManager : Base.BaseManager
    {
        public PersonManager(string baseUrl) : base(baseUrl)
        {
        }
        public ServerResponse<PersonModel> GetModel(PersonGetModelRequest request)
        {
            return Action<PersonModel>(request, "api/Person/model");
        }

        public ServerResponse<List<PersonModel>> GetList(PersonGetListRequest request, out int total)
        {
            total = 0;
            var result = GetCount(JsonHelper.CloneObject<PersonGetCountRequest>(request));
            if (result.Code == ServerResponseType.成功)
            {
                total = result.Data;
            }
            return Action<List<PersonModel>>(request, "api/Person/list");
        }

        public ServerResponse<int> GetCount(PersonGetCountRequest request)
        {
            return Action<int>(request, "api/Person/count");
        }

        public ServerResponse<int> Add(PersonModel request)
        {
            var tmpModel = JsonHelper.CloneObject<PersonModel>(request);
            tmpModel.Id = Guid.NewGuid().ToString();
            var result = Action<int>(tmpModel, "api/Person/add");
            if (result.Code == ServerResponseType.成功)
                request.Id = tmpModel.Id;
            return result;
        }

        public ServerResponse<int> Update(PersonModel request)
        {
            return Action<int>(request, "api/Person/update");
        }

        public ServerResponse<int> Delete(PersonModel request)
        {
            return Action<int>(request, "api/Person/delete");
        }
        /// <summary>
        /// 在场人员
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerResponse<List<PersonModel>> GetInList(PersonGetInListRequest request)
        {
            return Action<List<PersonModel>>(request, "api/Person/in_list");
        }

        /// <summary>
        /// 更新黑白名单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerResponse<int> UpdatePermission(PersonUpdatePermissionRequest request)
        {
            return Action<int>(request, "api/Person/update_permission");
        }
    }
}
