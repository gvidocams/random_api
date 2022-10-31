using System.Threading.Tasks;
using Atea.Interfaces;
using Atea.Models;

namespace Atea.Services
{
    public class BlobService : IBlobService
    {
        private readonly IBlobStorageProvider _blobStorageProvider;

        public BlobService(IBlobStorageProvider blobStorageProvider)
        {
            _blobStorageProvider = blobStorageProvider;
        }
            
        public string SaveBlob(Root response)
        {
            return _blobStorageProvider.SaveBlob(response);
        }

        public Task<string> GetBlobAsync(string id)
        {
            return _blobStorageProvider.GetBlobAsync(id);
        }
    }
}