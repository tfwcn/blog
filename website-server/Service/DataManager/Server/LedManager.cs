using Model.Server;

namespace DataManager.Server
{
    public class LedManager : Base.BaseManager
    {
        public LedManager(string baseUrl) : base(baseUrl)
        {
        }
        //更新Led在场人数
        public ServerResponse<object> Update()
        {
            return Action<object>(null, "api/Led/update", 3000);
        }
    }
}
