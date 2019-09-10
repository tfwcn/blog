using Model.Server.Models;
using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// 共享数据
    /// </summary>
    public static class CommonData
    {
        /// <summary>
        /// 登录授权信息
        /// </summary>
        public static Dictionary<string, UserModel> TokenList { get; set; }
        static CommonData()
        {
            TokenList = new Dictionary<string, UserModel>();
        }
    }
}
