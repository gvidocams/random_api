using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atea.Models;

namespace Atea.Interfaces
{
    public interface ILogStorageProvider
    {
        Task LogRequestAsync(Root response, string id);
        IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to);
    }
}