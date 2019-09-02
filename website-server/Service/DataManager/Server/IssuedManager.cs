using Common;
using Model.Server;
using Model.Server.Args;
using Model.Server.Models;
using System.Collections.Generic;

namespace DataManager.Server
{
    public class IssuedManager : Base.BaseManager
    {
        public IssuedManager(string baseUrl) : base(baseUrl)
        {
        }
        /// <summary>
        /// 获取门禁配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerResponse<DoorModel> GetConfig(IssuedGetConfigRequest request)
        {
            return Action<DoorModel>(request, "api/Issued/get_config");
        }
        /// <summary>
        /// 设置门禁配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerResponse<object> UpdateConfig(IssuedUpdateConfigRequest request)
        {
            return Action<object>(request, "api/Issued/update_config");
        }
        /// <summary>
        /// 获取模板ID列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerResponse<List<PersonModel>> GetPerson(IssuedGetPersonRequest request,out int total)
        {
            total = 0;
            var result = GetPersonCount(JsonHelper.CloneObject<IssuedGetPersonRequest>(request));
            if (result.Code == ServerResponseType.成功)
            {
                total = result.Data;
            }
            return Action<List<PersonModel>>(request, "api/Issued/get_person");
        }
        /// <summary>
        /// 获取模板ID列表数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerResponse<int> GetPersonCount(IssuedGetPersonRequest request)
        {
            return Action<int>(request, "api/Issued/get_person_count");
        }
        /// <summary>
        /// 下发模板
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerResponse<object> UpdatePerson(IssuedUpdatePersonRequest request)
        {
            return Action<object>(request, "api/Issued/update_person");
        }
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerResponse<object> DeletePerson(IssuedDeletePersonRequest request)
        {
            return Action<object>(request, "api/Issued/delete_person");
        }
        /// <summary>
        /// 清空模板
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerResponse<object> CleanPerson(IssuedCleanPersonRequest request)
        {
            return Action<object>(request, "api/Issued/clean_person");
        }
        /// <summary>
        /// 远程开门
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ServerResponse<object> OpenDoor(IssuedOpenDoorRequest request)
        {
            return Action<object>(request, "api/Issued/open_door");
        }
    }
}
