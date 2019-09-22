using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.CommonHelpers
{
	public class GlobalExceptionMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<GlobalExceptionMiddleware> logger;

		public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
		{
			this.next = next;
			this.logger = logger;
		}

		public ILogger<GlobalExceptionMiddleware> Logger { get; }

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "There was an Exeption.");
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

			// Get the actual Exception
			var errorMessage = exception.Message;
			while (exception.InnerException != null)
			{
				exception = exception.InnerException;
				errorMessage = exception.Message;
			}

			var response = JsonConvert.SerializeObject(new
			{
				StatusCode = context.Response.StatusCode,
				Message = errorMessage
			});

			return context.Response.WriteAsync(response);
		}
	}
}
