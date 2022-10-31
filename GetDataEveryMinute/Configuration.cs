using System;
using Atea;

namespace GetDataEveryMinute;

public class AzureConfiguration : IAzureConfiguration
{
    public string ConnectionString => Environment.GetEnvironmentVariable("ConnectionString");
    public string BlobContainerName => Environment.GetEnvironmentVariable("BlobContainerName");
    public string AzureTableName => Environment.GetEnvironmentVariable("AzureTableName");
    public string FilePrefix => Environment.GetEnvironmentVariable("FilePrefix");
}