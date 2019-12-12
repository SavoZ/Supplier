using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Common.Models;

namespace SupplierApp.Filters {
	public class ErrorFilter : IExceptionFilter {
		public void OnException(ExceptionContext context) {
			var status = HttpStatusCode.InternalServerError;
			var error = new ErrorResult();

			error.Message = context.Exception.Message;
			error.InnerMessage = context.Exception.InnerException?.Message;

			context.ExceptionHandled = true;
			var response = context.HttpContext.Response;
			response.StatusCode = (int) status;
			response.ContentType = "application/json";

			response.WriteJsonAsync(error);
		}
	}
}
