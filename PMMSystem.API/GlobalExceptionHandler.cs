using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using PMMSystem.Application;
using PMMSystem.Domain.Entities.ErrorModels;
using PMMSystem.Domain.Exceptions;

namespace PMMSystem.API
{
  public class GlobalExceptionHandler(ILoggerManager logger) : IExceptionHandler
  {

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
      httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      httpContext.Response.ContentType = "application/json";

      var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
      if (contextFeature != null)
      {
        httpContext.Response.StatusCode = contextFeature.Error switch
        {
          MaintenanceNotFoundException => StatusCodes.Status404NotFound,
          MockAuthException => StatusCodes.Status401Unauthorized,
          _ => StatusCodes.Status500InternalServerError,
        };
        logger.LogError($"Somthing went wrong {exception.Message}");
        await httpContext.Response.WriteAsync(new ErrorDetails
        {
          StatusCode = httpContext.Response.StatusCode,
          Message = contextFeature.Error.Message
        }.ToString());
      }

      return true;
    }
  }
}
