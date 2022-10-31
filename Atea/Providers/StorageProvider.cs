using Atea.Interfaces;
using Atea.Models;
using Azure.Storage.Blobs;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TableOperation = Microsoft.Azure.Cosmos.Table.TableOperation;

namespace Atea.Providers
{
    public class StorageProvider : ILogStorageProvider, IBlobStorageProvider
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly CloudTable _cloudTable;
        private readonly IAzureConfiguration _config;

        public StorageProvider(IAzureConfiguration config)
        {
            _config = config;

            var client = new BlobServiceClient(config.ConnectionString);

            try
            {
                _blobContainerClient = client.CreateBlobContainer(config.BlobContainerName);
            }
            catch (Exception ex)
            {
                _blobContainerClient = client.GetBlobContainerClient(config.BlobContainerName);
            }

            var account = CloudStorageAccount.Parse(config.ConnectionString);

            var tableClient = account.CreateCloudTableClient(new TableClientConfiguration());

            _cloudTable = tableClient.GetTableReference(config.AzureTableName);
            _cloudTable.CreateIfNotExists();
        }

        public string SaveBlob(Root response)
        {
            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);

            var content = JsonSerializer.Serialize(response);

            streamWriter.Write(content);

            streamWriter.Flush();

            stream.Seek(0, SeekOrigin.Begin);
            var name = DateTime.Now.ToString("yyyyMMdd-hhmmss");
            var fileName = $"{_config.FilePrefix}{name}.json";

            _blobContainerClient.UploadBlob(fileName, stream);

            return name;
        }

        public async Task LogRequestAsync(Root response, string id)
        {
            var serialized = JsonSerializer.Serialize(response);
            var entry = new ApiResponseEntity(id, serialized, DateTime.Now);

            var operation = TableOperation.Insert(entry);

            await _cloudTable.ExecuteAsync(operation);
        }

        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to)
        {
            var items = _cloudTable.ExecuteQuery(new TableQuery<ApiResponseEntity>())
                .Where(x => x.Timestamp >= from && x.Timestamp <= to);

            return items;
        }

        public async Task<string> GetBlobAsync(string id)
        {
            var fileName = $"{_config.FilePrefix}{id}.json";

            var blobClient = _blobContainerClient.GetBlobClient(fileName);

            using var stream = new MemoryStream();

            await blobClient.DownloadToAsync(stream);

            stream.Position = 0;

            using var streamReader = new StreamReader(stream);

            var response = await streamReader.ReadToEndAsync();

            return response;
        }
    }
}