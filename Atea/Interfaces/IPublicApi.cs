using System.Threading.Tasks;
using Atea.Models;
using Refit;

namespace Atea.Services
{
    public interface IPublicApi
    {
        [Get("/random?auth=null")]
        Task<Root> GetApi();
    }
}