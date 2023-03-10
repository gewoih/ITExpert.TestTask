using Serilog;
using Serilog.Core;
using System.Text;

namespace ITExpert.Services.TodoManager.Middleware
{
    public class HttpLoggingMiddleware
    {
        private readonly Logger _logger;
        private readonly RequestDelegate _next;

        public HttpLoggingMiddleware(RequestDelegate next, Logger logger)
        {
            _next = next;
            _logger = logger; 
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            var request = await GetRequestAsTextAsync(context.Request);

            _logger.Information(request);

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            var response = await GetResponseAsTextAsync(context.Response);
            _logger.Information(response);

            await responseBody.CopyToAsync(originalBodyStream);
        }


        private async Task<string> GetRequestAsTextAsync(HttpRequest request)
        {
            var body = request.Body;
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> GetResponseAsTextAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }
    }
}
