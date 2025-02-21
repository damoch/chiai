namespace chiai.Server.Middleware
{
    public class LanguageFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LanguageFilterMiddleware> _logger;
        private readonly List<string> _forbiddenWords;

        public LanguageFilterMiddleware(RequestDelegate next, ILogger<LanguageFilterMiddleware> logger)
        {
            _next = next;
            _logger = logger;

            _forbiddenWords = ["badword1", "badword2", "badword3"]; //Example words
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethod.Post.Method && context.Request.Path.StartsWithSegments("/chat/send"))
            {
                var request = context.Request;

                if (request.ContentType != null && request.ContentType.Contains("application/json"))
                {
                    var bodyStream = new StreamReader(request.Body);
                    var body = await bodyStream.ReadToEndAsync();//very naive solution

                    if (_forbiddenWords.Any(word => body.Contains(word, StringComparison.OrdinalIgnoreCase)))
                    {
                        _logger.LogWarning("Forbidden language detected in message.");
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("Message contains forbidden language.");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }

}
