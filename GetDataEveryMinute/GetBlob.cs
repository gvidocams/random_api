using System;
using System.IO;
using System.Threading.Tasks;
using Atea.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GetDataEveryMinute
{
    public class GetBlob
    {
        private readonly IBlobService _blobService;

        public GetBlob(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [FunctionName("GetBlob")]
        public async Task<IActionResult> Run(
            [HttpTrigger("get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var id = req.Query["id"];

            var result = await _blobService.GetBlobAsync(id);

            return new OkObjectResult(result);
        }
    }
}
