using DAL;
using Model.Server;
using Model.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private AppSettings Config;
        private static ViewDAL dal;
        public ViewController(IOptions<AppSettings> setting)
        {
            Config = setting.Value;
            if (dal == null)
                dal = new ViewDAL(Config.ConnectionString);
        }
        // Post: api/View/person_num
        [HttpPost("person_num")]
        public ServerResponse<List<PersonNumModel>> GetPersonNum()
        {
            ServerResponse<List<PersonNumModel>> response = new ServerResponse<List<PersonNumModel>>();
            try
            {
                var list = dal.GetPersonNum();
                if (list != null)
                {
                    if (!list.Exists(m => m.Type == 0))
                        list.Add(new PersonNumModel() { Type = 0, Num = 0 });
                    if (!list.Exists(m => m.Type == 1))
                        list.Add(new PersonNumModel() { Type = 1, Num = 0 });
                    if (!list.Exists(m => m.Type == 2))
                        list.Add(new PersonNumModel() { Type = 2, Num = 0 });
                    response.Data = list;
                    response.Code = ServerResponseType.成功;
                }
                else
                {
                    response.Code = ServerResponseType.空数据;
                }
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

