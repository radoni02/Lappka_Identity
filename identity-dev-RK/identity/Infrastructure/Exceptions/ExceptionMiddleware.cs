﻿using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public sealed class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ProjectException pex)
            {
                _logger.LogError(pex, pex.Message);

                var errorCode = (int)pex.ErrorCode;
                context.Response.StatusCode = errorCode;
                context.Response.Headers.Add("content-type", "application/json");

                var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, pex.Message, Errors = pex.ExceptionData });
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "server error");

                var errorCode = 500;
                context.Response.StatusCode = errorCode;
                context.Response.Headers.Add("content-type", "application/json");

                var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, ex.Message });
                await context.Response.WriteAsync(json);
            }
        }
    }
}
