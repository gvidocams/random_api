using Atea.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GetDataEveryMinute
{
    public class LogProvider
    {
        private readonly ILogService _logService;

        public LogProvider(ILogService logService)
        {
            _logService = logService;
        }

        [FunctionName("LogProvider")]
        public async Task<IActionResult> Run(
            [HttpTrigger("get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            string from = req.Query["from"];
            string to = req.Query["to"];

            var dateFrom = DateTime.Parse(from);
            var dateTo = DateTime.Parse(to);

            var logs = _logService.GetLogs(dateFrom, dateTo);

            return new OkObjectResult(logs);
        }
    }
}
