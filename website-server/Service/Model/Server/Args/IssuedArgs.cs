using Model.Server.Models;

namespace Model.Server.Args
{
    /// <summary>
    /// 获取配置
    /// </summary>
    public class IssuedGetConfigRequest
    {
        public string BaseUrl { get; set; }
    }

    /// <summary>
    /// 更新配置
    /// </summary>
    public class IssuedUpdateConfigRequest
    {
        public string BaseUrl { get; set; }
        public DoorModel Data { get; set; }
    }

    /// <summary>
    /// 获取模板
    /// </summary>
    public class IssuedGetPersonRequest
    {
        public string BaseUrl { get; set; }
        public int? page { get; set; }
        public int? rows { get; set; }
    }

    /// <summary>
    /// 获取模板数量
    /// </summary>
    public class IssuedGetPersonCount
    {
        public string BaseUrl { get; set; }
        public int? page { get; set; }
        public int? rows { get; set; }
    }

    /// <summary>
    /// 更新模板
    /// </summary>
    public class IssuedUpdatePersonRequest
    {
        public string BaseUrl { get; set; }
        public string Id { get; set; }
    }
    /// <summary>
    /// 删除模板
    /// </summary>
    public class IssuedDeletePersonRequest
    {
        public string BaseUrl { get; set; }
        public string Id { get; set; }
    }
    /// <summary>
    /// 清空模板
    /// </summary>
    public class IssuedCleanPersonRequest
    {
        public string BaseUrl { get; set; }
    }
    /// <summary>
    /// 远程开门
    /// </summary>
    public class IssuedOpenDoorRequest
    {
        public string BaseUrl { get; set; }
        public int? Type { get; set; }
        public int? PersonType { get; set; }
    }
}
