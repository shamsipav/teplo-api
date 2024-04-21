using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using TeploAPI.Exceptions;
using TeploAPI.Models;

namespace TeploAPI.Filters;

public class CustomExceptionFilterAttribute: Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        string actionName = context.ActionDescriptor.DisplayName;
        string controllerName = context.ActionDescriptor.RouteValues["controller"];
        string requestPath = context.HttpContext.Request.Path;
        string exceptionStack = context.Exception.StackTrace;
        string exceptionMessage = context.Exception.Message;
        
        Log.Error($"An exception occurred in controller {controllerName}, action {actionName}, for request path {requestPath}: \n {exceptionMessage} \n {exceptionStack}");
        
        context.Result = new ContentResult
        {
            Content = $"An exception occurred in controller {controllerName}, action {actionName}, for request path {requestPath}: \n {exceptionMessage} \n {exceptionStack}"
        };
        context.ExceptionHandled = true;

        int statusCode = 500;

        switch (context.Exception)
        {
            case NotFoundException _:
                statusCode = 404;
                break;
            case BadRequestException _:
                statusCode = 400;
                break;
            case BusinessLogicException _:
                statusCode = 500;
                break;
        }
        
        context.Result = new ObjectResult(new Response { Status = statusCode, ErrorMessage = exceptionMessage })
        {
            StatusCode = statusCode
        };
    }
}