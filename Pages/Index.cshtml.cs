using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using NLog;

namespace HTTPReceiver.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            string query = Request.QueryString.ToString();

            string log_query = $"Request.QueryString: {query}";
            _logger.Log(LogLevel.Information, log_query);

        }

        public void OnPost()
        {

            var stream = Request.Body;
            var reader = new StreamReader(stream);
            var body = reader.ReadToEndAsync().Result;

            string log_body = $"Request.Body: {body}";
            _logger.Log(LogLevel.Information, log_body);

        }

    }
}