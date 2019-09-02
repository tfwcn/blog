using DataManager.Server;

namespace DataManager
{
    /// <summary>
    /// 服务根路径
    /// </summary>
    public class ServerDataManager
    {
        public UserManager User { get; set; }
        public PersonManager Person { get; set; }
        public CompanyManager Company { get; set; }
        public DoorManager Door { get; set; }
        public LogManager Log { get; set; }
        public GroupManager Group { get; set; }
        public IssuedManager Issued { get; set; }
        public ViewManager View { get; set; }
        public LedManager Led { get; set; }
        public GateManager Gate { get; set; }

        public ServerDataManager(string baseUrl)
        {
            User = new UserManager(baseUrl);
            Person = new PersonManager(baseUrl);
            Company = new CompanyManager(baseUrl);
            Door = new DoorManager(baseUrl);
            Log = new LogManager(baseUrl);
            Group = new GroupManager(baseUrl);
            Issued = new IssuedManager(baseUrl);
            View = new ViewManager(baseUrl);
            Led = new LedManager(baseUrl);
            Gate = new GateManager(baseUrl);
        }
    }
}
