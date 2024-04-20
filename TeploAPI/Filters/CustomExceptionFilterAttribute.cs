using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
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

        context.Result = new StatusCodeResult(500);
        context.Result = new ObjectResult(new Response { ErrorMessage = exceptionMessage });
    }
}