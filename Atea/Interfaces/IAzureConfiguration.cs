using System;

namespace Atea.Interfaces
{
    public interface IAzureConfiguration
    {
        public string ConnectionString { get; }
        public string BlobContainerName { get; }
        public string AzureTableName { get; }
        public string FilePrefix { get; }
    }
}