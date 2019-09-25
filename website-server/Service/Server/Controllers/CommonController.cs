using Microsoft.AspNetCore.Mvc;
using Model.Server;
using System;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        // GET: api/Common/new_id
        [HttpGet("new_id")]
        public ServerResponse<string> GetId()
        {
            ServerResponse<string> response = new ServerResponse<string>();
            try
            {
                response.Data = Guid.NewGuid().ToString();
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
