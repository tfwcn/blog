using Model.Server;
using Model.Server.Models;
using System.Collections.Generic;

namespace DataManager.Server
{
    public class ViewManager : Base.BaseManager
    {
        public ViewManager(string baseUrl) : base(baseUrl)
        {
        }
        /// <summary>
        /// 获取在场人数
        /// </summary>
        /// <returns></returns>
        public ServerResponse<List<PersonNumModel>> GetPersonNum()
        {
            return Action<List<PersonNumModel>>(null, "api/View/person_num");
        }
    }
}
