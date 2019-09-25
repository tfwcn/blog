using DataManager.Server;

namespace DataManager
{
    /// <summary>
    /// 服务根路径
    /// </summary>
    public class ServerDataManager
    {
        public UserManager User { get; set; }
        public NewsManager News { get; set; }
        public WebLoaderManager WebLoader { get; set; }

        public ServerDataManager(string baseUrl)
        {
            User = new UserManager(baseUrl);
            News = new NewsManager(baseUrl);
            WebLoader = new WebLoaderManager(baseUrl);
        }
    }
}
