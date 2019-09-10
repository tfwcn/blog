namespace Model.Server
{
    public enum ServerResponseType
    {
        成功 = 0,
        调用异常 = -1,
        空数据 = -2,
        调用服务异常 = -3,
        操作未授权 = -4,
    }
    public class ServerResponse<T>
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public ServerResponseType Code { get; set; }
        /// <summary>
        /// 错误码名称
        /// </summary>
        public string CodeName { get { return Code.ToString(); } }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
