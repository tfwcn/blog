using DAL;
using Model.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedController : ControllerBase
    {
        private AppSettings Config;
        private static ViewDAL dal;
        public LedController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new ViewDAL(Config.ConnectionString);
        }
        // Post: api/Led/update
        [HttpPost("update")]
        public ServerResponse<object> Update()
        {
            ServerResponse<object> response = new ServerResponse<object>();
            try
            {
                var list = dal.GetPersonNum();
                var num0 = 0;
                var num1 = 0;
                var num2 = 0;
                if (list.Exists(m => m.Type == 0))
                {
                    num0 = list.Find(m => m.Type == 0).Num.Value >= 0 ? list.Find(m => m.Type == 0).Num.Value : 0;
                }
                if (list.Exists(m => m.Type == 1))
                {
                    num1 = list.Find(m => m.Type == 1).Num.Value >= 0 ? list.Find(m => m.Type == 1).Num.Value : 0;
                }
                if (list.Exists(m => m.Type == 2))
                {
                    num2 = list.Find(m => m.Type == 2).Num.Value >= 0 ? list.Find(m => m.Type == 2).Num.Value : 0;
                }
                TcpClient tcp = new TcpClient();
                tcp.Client.ReceiveTimeout = 3000;
                tcp.Client.SendTimeout = 3000;
                tcp.Connect(Config.LedIp, Config.LedPort);
                var gb2312 = Encoding.GetEncoding("GB2312");
                var sendContent = String.Format(Config.LedFormat, num0 + num1 + num2, num0, num1, num2);
                List<byte> data = new List<byte>();
                data.AddRange(new byte[] { 0x55, 0xAA, 0x00, 0x00, 0x01, 0x01, 0x00, 0xD9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                data.AddRange(gb2312.GetBytes(sendContent));
                data.AddRange(new byte[] { 0x00, 0x00, 0x0D, 0x0A });
                var sendData = data.ToArray();
                tcp.Client.Send(sendData);
                tcp.Close();
                response.Code = ServerResponseType.成功;
            }
            catch (Exception ex)
            {
                response.Code = ServerResponseType.调用异常;
                response.ErrorMsg = ex.ToString();
                Log.LogHelper.WriteErrorLog(GetType(), ex);
            }
            return response;
        }
    }
}
