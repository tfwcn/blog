using Common;
using Model.Server;
using System;
using System.Net.Http;

namespace DataManager.Server.Base
{
    public class BaseManager
    {
        /// <summary>
        /// 服务根路径
        /// </summary>
        protected string baseUrl;
        public BaseManager(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }
        protected ServerResponse<T> Action<T>(object request, string subUrl, int timeout = 10000)
        {
            ServerResponse<T> response = new ServerResponse<T>();
            try
            {
                using (var client = new HttpClient())
                {
                    var input = JsonHelper.SerializeObject(request);
                    if (!string.IsNullOrEmpty(input))
                    {
                        Log.LogHelper.WriteDebugLog(GetType(), $"Calling Action[{subUrl}][{(input.Length > 1024 ? input.Substring(0, 1024) : input)}]");
                    }
                    string jsonArg = JsonHelper.SerializeObject(request);
                    var content = new StringContent(jsonArg, System.Text.Encoding.UTF8, "application/json");
                    client.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
                    var httpResponse = client.PostAsync(baseUrl + subUrl, content).GetAwaiter().GetResult();

                    var jsonResult = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    if (!string.IsNullOrEmpty(jsonResult))
                    {
                        Log.LogHelper.WriteDebugLog(GetType(), $"Return [{subUrl}][{(jsonResult?.Length > 1024 ? jsonResult.Substring(0, 1024) : jsonResult)}]");
                        response = JsonHelper.DeserializeObject<ServerResponse<T>>(jsonResult);
                    }
                    else
                    {
                        response.Code = ServerResponseType.调用服务异常;
                        response.ErrorMsg = "null json";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Code = ServerResponseType.调用服务异常;
                response.ErrorMsg = ex.ToString();
                Log.LogHelper.WriteErrorLog(GetType(), ex);
            }
            return response;
        }
    }
}
