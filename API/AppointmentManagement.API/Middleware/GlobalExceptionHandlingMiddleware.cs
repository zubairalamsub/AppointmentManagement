using AppointmentManagement.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace AppointmentManagement.API.Middleware
{
	
	public class GlobalExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public GlobalExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";

			var statusCode = exception switch
			{
				
				UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
				ValidationException => (int)HttpStatusCode.BadRequest,
				_ => (int)HttpStatusCode.InternalServerError
			};

			var response = new ErrorResponse
			{
				StatusCode = statusCode,
				Message = exception.Message,
				Details = exception.InnerException?.Message
			};

			context.Response.StatusCode = statusCode;
			var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

			return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
		}
	}

}
