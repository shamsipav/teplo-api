using Serilog;

namespace TeploAPI.Middlewares
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var method = httpContext.Request.Method;
            var path = httpContext.Request.Path;
            var queryString = httpContext.Request.QueryString.ToString();
            //var headers = FormatHeaders(httpContext.Request.Headers);
            var scheme = httpContext.Request.Scheme;
            var host = httpContext.Request.Host.ToString();
            var body = await ReadBodyFromRequest(httpContext.Request);

            Log.Information($"Request: HTTP {method} {path}, {(queryString == String.Empty ? "" : "QueryString: " + queryString + ",")} Scheme: {scheme}, Host: {host}, Body: {body}");

            var originalResponseBody = httpContext.Response.Body;
            using var newResponseBody = new MemoryStream();
            httpContext.Response.Body = newResponseBody;

            await _next(httpContext);

            newResponseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

            var statusCode = httpContext.Response.StatusCode.ToString();
            var contentType = httpContext.Response.ContentType;
            //var responseHeaders = FormatHeaders(httpContext.Response.Headers);
            var responseBody = responseBodyText;

            Log.Information($"Response: {statusCode} {contentType}, Body: {responseBody}");

            newResponseBody.Seek(0, SeekOrigin.Begin);
            await newResponseBody.CopyToAsync(originalResponseBody);
        }

        //private static string FormatHeaders(IHeaderDictionary headers) => string.Join(", ", headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value)}}}"));

        private static async Task<string> ReadBodyFromRequest(HttpRequest request)
        {
            request.EnableBuffering();

            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();

            request.Body.Position = 0;
            return requestBody;
        }
    }
}
