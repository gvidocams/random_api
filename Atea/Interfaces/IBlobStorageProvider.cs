using System.Threading.Tasks;
using Atea.Models;

namespace Atea.Interfaces
{
    public interface IBlobStorageProvider
    {
        string SaveBlob(Root response);
        Task<string> GetBlobAsync(string id);
    }
}