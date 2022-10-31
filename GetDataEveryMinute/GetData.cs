using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Atea;
using Atea.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GetDataEveryMinute
{
    public class GetData
    {
        private IPublicApi _publicApi;
        private readonly IBlobService _blobService;
        private readonly ILogService _logService;

        public GetData(IPublicApi publicApi, IBlobService blobService, ILogService logService)
        {
            _publicApi = publicApi;
            _blobService = blobService;
            _logService = logService;
        }

        [FunctionName("GetData")]
        public async Task RunAsync([TimerTrigger("*/10 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            var result = await _publicApi.GetApi();
            var output = result.entries.First();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            log.LogInformation($"Api: {output.API} Description: {output.Description}");

            var id = _blobService.SaveBlob(result);

            await _logService.LogRequestAsync(result, id);
        }
    }
}
