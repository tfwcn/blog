﻿using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Model.Server;
using System.Threading.Tasks;

namespace Server.Middlewares
{
    /// <summary>
    /// 验证登录
    /// </summary>
    public class LoginCheckMiddleware
    {
        private readonly RequestDelegate next;

        public LoginCheckMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if ((context.Request.Path.Value.EndsWith("/add")
                    || context.Request.Path.Value.EndsWith("/update")
                    || context.Request.Path.Value.EndsWith("/delete")
                    || context.Request.Path.Value.Contains("/manager"))
                    && context.Request.Method.ToLower() == "post")
            {
                if (context.Request.Host.Host != "localhost" && context.Request.Host.Host != "192.168.1.163" && context.Request.Host.Host != "127.0.0.1")
                {
                    string token = context.Request.Headers["Access-Token"];
                    if (token == null || !CommonData.TokenList.ContainsKey(token))
                    {
                        ServerResponse<object> response = new ServerResponse<object>();
                        response.Code = ServerResponseType.操作未授权;
                        response.ErrorMsg = "操作未授权";
                        Log.LogHelper.WriteErrorLog(GetType(), "操作未授权:" + context.Request.ToString());
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(JsonHelper.SerializeObject(response));
                        return;
                    }
                }
            }
            // 继续执行下一步
            await next(context);
        }
    }
    public static class LoginCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginCheck(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginCheckMiddleware>();
        }
    }
}
