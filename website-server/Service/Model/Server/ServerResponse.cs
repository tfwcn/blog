namespace Model.Server
{
    public enum ServerResponseType
    {
        成功 = 0,
        调用异常 = -1,
        空数据 = -2,
        调用服务异常 = -3,
        客户端异常 = -4,
        密码错误 = -5,
        人员进出记录重复 = -100,
        下发失败 = 1002,
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
