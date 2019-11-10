﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SakartveloSoft.API.Core;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using SakartveloSoft.API.Core.Logging;

namespace SakartveloSoft.API.Framework.Adapters
{
    public static class RequestAPIContextMiddleware
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();
        public static IApplicationBuilder UseAPIContext(this IApplicationBuilder app)
        {
            bool isSessionEnabled = true;
            app.Use(async (HttpContext ctx, Func<Task> next)  => {
                var apiCtx = ctx.RequestServices.GetService<IAPIContext>();
                apiCtx.StartTime = DateTime.UtcNow;
                apiCtx.RequestId = GenerateRequestId();
                ctx.TraceIdentifier = apiCtx.RequestId;
                apiCtx.RequestMethod = ctx.Request.Method;
                apiCtx.RequestUrl = ctx.Request.Path;
                if (isSessionEnabled)
                try
                {
                    if (ctx.Session.IsAvailable)
                    {
                        apiCtx.SessionId = ctx.Session.Id;
                    }
                } catch
                    {
                        isSessionEnabled = false;
                    }
                if (ctx.User.Identity.IsAuthenticated)
                {
                    var idClaim = ctx.User.Claims.FirstOrDefault(claim => claim.Type == "userId");
                    if (idClaim != null) {
                        apiCtx.UserId = idClaim.Value;
                    }
                }
                ctx.Response.OnStarting(() =>
                {
                    apiCtx.ProcessingCompletedAt = DateTime.UtcNow;
                    apiCtx.TimeSpent = apiCtx.ProcessingCompletedAt - apiCtx.StartTime;
                    ctx.Response.Headers.Add("X-Request-Id", apiCtx.RequestId);
                    ctx.Response.Headers.Add("X-Time-Spent", apiCtx.TimeSpent.Value.ToString());
                    apiCtx.StatusCode = ctx.Response.StatusCode;
                    apiCtx.ResponseContentType = ctx.Response.ContentType;
                    return Task.CompletedTask;
                });
                var logger = ctx.RequestServices.GetService<IScopedLogger<IAPIContext>>();
                try
                {
                    await next();
                    if (apiCtx.StatusCode == 0)
                    {
                        ctx.Response.StatusCode = 404;
                        apiCtx.StatusCode = 404;
                        apiCtx.ProcessingCompletedAt = DateTime.UtcNow;
                        apiCtx.TimeSpent = apiCtx.ProcessingCompletedAt - apiCtx.StartTime;
                        logger.Error($@"{apiCtx.RequestMethod} to {apiCtx.RequestUrl} failed to process at all in {apiCtx.TimeSpent} with status {apiCtx.StatusCode} returning {apiCtx.ResponseContentType}");
                    }
                    else if (apiCtx.StatusCode < 400)
                    {
                        logger.Information($@"{apiCtx.RequestMethod} to {apiCtx.RequestUrl} processed in {apiCtx.TimeSpent} with status {apiCtx.StatusCode} returning {apiCtx.ResponseContentType}");
                    }
                    else if (apiCtx.StatusCode < 500)
                    {
                        logger.Warning($@"{apiCtx.RequestMethod} to {apiCtx.RequestUrl} got invalid request and replied in {apiCtx.TimeSpent} with status {apiCtx.StatusCode} returning {apiCtx.ResponseContentType}");
                    }
                    else
                    {
                        logger.Error($@"{apiCtx.RequestMethod} to {apiCtx.RequestUrl} encountered a problem in {apiCtx.TimeSpent} with status {apiCtx.StatusCode} returning {apiCtx.ResponseContentType}");
                    }
                } catch
                {
                    if (apiCtx.StatusCode == 0)
                    {
                        apiCtx.StatusCode = 404;
                    }
                    apiCtx.ProcessingCompletedAt = DateTime.UtcNow;
                    apiCtx.TimeSpent = apiCtx.ProcessingCompletedAt - apiCtx.StartTime;
                    logger.Error($@"{apiCtx.RequestMethod} to {apiCtx.RequestUrl} failed to process at all in {apiCtx.TimeSpent} with status {apiCtx.StatusCode} returning {apiCtx.ResponseContentType}");
                    throw;
                }
            });
            return app;
        }

        private static string GenerateRequestId()
        {
            var bytes = new byte[16];
            rng.GetBytes(bytes);
            var str = Convert.ToBase64String(bytes);
            return str.Replace('/', '_').Replace('+', '-');
        }

    }
}
